Imports System.IO
Imports DevExpress.Utils
Imports DevExpress.Web
Imports DevExpress.Web.ASPxPivotGrid
Imports DevExpress.Web.Mvc
Imports DevExpress.XtraPivotGrid
Imports DevExpress.XtraPrinting
Imports Microsoft.AspNet.SignalR
Imports Newtonsoft.Json
Imports WinSCP


Namespace SIG.Areas.Contraloria.Controllers
    Public Class CargarArchivoController
        Inherits Controller

#Region "variables"

        Dim Carga As Models.Carga = New SIG.Areas.Contraloria.Models.Carga
        Dim login As New Cl_Login

        ' lista para guardar cada número de línea en que se encuentran un registro bueno
        Dim regBuenos As New List(Of archivo)
        Dim montos(3) As Integer
        'montos(0) monto total
        'montos(1) monto pagado
        'montos(2) monto no pagado
        Dim registros(3) As Integer
        'registros(0) registros totales
        'registros(1) registros pagados
        'registros(2) registros no pagados
        'registros(3) registros pagados fuera de las fechas de programación
        Dim countEstados(10) As Integer
        'countEstados(0) No existe información de la planilla relaciondad al pago
        'countEstados(1) Monto distinto al de la planilla
        'countEstados(2) Formato de fecha incorrecto
        'countEstados(3) Fecha en el archivo no coincide con la fecha en el registro de pago
        'countEstados(4) Estado del pago en el archivo es distinto de 0 o 1
        'countEstados(5) Registro como no pagado en el archivo de texto y existe información del pago en la base de datos
        'countEstados(6) Planilla Cerrada
        'countEstados(7) Planilla Justificada
        'countEstados(8) Planilla Anulada
        'countEstados(9) Registro Correcto, la información del pago ya esta en la base de datos
        'countEstados(10) Registro Correcto, no existe información del pago en la base de datos

#End Region

        <HttpGet>
        Function Index() As ActionResult

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(4) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then

                        If Not IsNothing(Session("ds")) Then
                            Session("ds") = Nothing
                            Session("path") = Nothing
                            Session("fileName") = Nothing
                        End If

                        Dim ds As DataSet = Carga.getDataSetPagos()
                        Session("ds") = ds

                        ViewData("periodos") = Carga.getAllPeriodos()
                        ViewData("bancos") = Carga.getAllBancos()

                        Return View()
                    Else
                        Return Redirect("/Contraloria/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Sub recibirSignalRId(ByVal id As String)
            Session("signalrId") = id
        End Sub

#Region "PreCarga"

        Function UploadControlCallback() As ActionResult
            UploadControlExtension.GetUploadedFiles("uc", SIG.Areas.Contraloria.Controllers.Upload.UploadControlValidationSettings, AddressOf Upload.ucCallbacks_FileUploadComplete)
            Dim banco As Integer = EditorExtension.GetValue(Of Integer)("cbxBanco")
            Dim periodo As Integer = EditorExtension.GetValue(Of Integer)("cbxPeriodo")
            Dim nombre As String = EditorExtension.GetValue(Of String)("txtNombre")
            Dim tipoFecha As String = EditorExtension.GetValue(Of String)("rbtTipoFecha")
            Dim tipoCarga As String = EditorExtension.GetValue(Of String)("rbtTipoCarga")
            Session("banco") = banco
            Session("periodo") = periodo
            guardarPreCarga(banco, periodo, nombre, tipoFecha, tipoCarga)
            Return Nothing
        End Function

        Sub guardarPreCarga(ByVal codBanco As Integer, ByVal codPeriodo As Integer, ByVal nombreCarga As String, ByVal tipoFecha As String, ByVal tipoCarga As String)

            Dim path As String = Convert.ToString(Session("path"))
            Dim nombre As String = Session("fileName")
            Dim _tempByte() As Byte = Nothing

            Try
                Dim _fileInfo As New FileInfo(path)
                Dim _NumBytes As Long = _fileInfo.Length
                Dim _FStream As New FileStream(path, IO.FileMode.Open, IO.FileAccess.Read)
                Dim _BinaryReader As New BinaryReader(_FStream)

                _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
                _fileInfo = Nothing
                _NumBytes = 0
                _FStream.Close()
                _FStream.Dispose()
                _BinaryReader.Close()
            Catch ex As Exception
            End Try

            Dim codPreCarga As Integer = Carga.insertPreCarga(codPeriodo, codBanco, nombre, _tempByte, nombreCarga, Session("usuario"))

            If tipoCarga = "Rapida" Then
                Carga.preCargarArchivo(path, codPreCarga, tipoFecha)
            Else
                preload(codPreCarga, tipoFecha)
            End If

            Session("codPreCarga") = codPreCarga
            Session("cargado") = 0
            Session("permitirFueraProgramacion") = 0

            'actualiza el registro de la carga, para colocar la fecha de finalización
            Carga.actulizarFinPreCarga(codPreCarga)

            'borra el archivo de la carpeta en que se almaceno temporalmente
            FileIO.FileSystem.DeleteFile(path)

            If tipoCarga = "Rapida" Then
                Dim pbconexion As New progressClass
                pbconexion.finPrecarga()
            End If

        End Sub

        Sub preload(ByVal codPreCarga As Integer, ByVal tipoFecha As String)

            Dim path As String = Convert.ToString(Session("path"))
            Dim lineas As Integer = 0
            Dim fila As String = ""
            Dim numRegistro As Integer = 0
            Dim cont As Integer = 0

            'Campos de archivo de banadesa
            Dim referencia, pagina, monto, fecha, estado As String

            'cuenta la cantidad de registros del archivo...
            Using sr As StreamReader = New StreamReader(path)
                Dim line As String
                Do
                    lineas += 1
                    line = sr.ReadLine()

                Loop Until line Is Nothing
                Session("totalReg") = lineas - 1
            End Using

            Using myReader As New FileIO.TextFieldParser(path)

                myReader.TextFieldType = FileIO.FieldType.Delimited
                myReader.SetDelimiters(",")

                Dim currentRow As String()
                While Not myReader.EndOfData

                    Try
                        currentRow = myReader.ReadFields()
                        Dim currentField As String

                        cont = 1
                        For Each currentField In currentRow
                            Select Case cont
                                Case 1
                                    pagina = currentField
                                Case 2
                                    referencia = currentField
                                Case 3
                                    monto = currentField
                                Case 4
                                    fecha = currentField
                                Case 5
                                    estado = currentField
                                Case Else
                                    'trae algun registro extra
                            End Select

                            cont += 1
                        Next
                        numRegistro += 1

                        'condición para verificar que el registro posea solo 5 registro, si posee más o menos no entrara en la condición
                        'el 6 es porque con ese valor debe de terminar el cont si el registro posee los 5 valores
                        If cont = 6 Then
                            verificar(numRegistro, pagina, referencia, monto, fecha, estado, codPreCarga, tipoFecha)
                        End If

                    Catch ex As Exception
                    End Try
                End While
            End Using

            Session("cargado") = 0
            Session("permitirFueraProgramacion") = 0
            'actualiza el registro de la carga, para colocar la fecha de finalización
            Carga.actulizarFinPreCarga(codPreCarga)

            'borra el archivo de la carpeta en que se almaceno temporalmente
            FileIO.FileSystem.DeleteFile(path)

        End Sub

        'realia la operación de verificar los pagos en un procedimiento almacenado
        Sub verificar(ByVal num As String, ByVal pag As String, ByVal reg As String, ByVal monto As String, ByVal fecha As String, ByVal estado As String, ByVal codPreCarga As Integer, ByVal tipoFecha As String)

            Carga.verificarPago(num, pag, reg, monto, fecha, estado, codPreCarga, tipoFecha)
            Session("actual") = num

            '////////////////////////////////////////////////////////////////
            ' llama a la función en SignalR, para informar del progreso de la carga
            Dim pbconexion As New progressClass
            pbconexion.enviarIncremento()
            '////////////////////////////////////////////////////////////////

        End Sub
#End Region

#Region "Carga"

        <HttpPost>
        Sub guardarCarga(ByVal tipoFecha As String, ByVal PermitirFueraProgramacion As Integer)
            Dim codPreCarga As Integer = Session("codPreCarga")
            Dim codCarga As Integer

            'inserta el registro de la carga y recibo el id con el que se ingreso 
            codCarga = Carga.insertCarga(codPreCarga)
            insertarPagos(codCarga, tipoFecha, PermitirFueraProgramacion)

        End Sub

        Sub insertarPagos(ByVal codCarga As Integer, ByVal tipoFecha As String, ByRef PermitirFueraProgramacion As Integer)

            Dim banco As Integer = Session("banco")
            Dim periodo As Integer = Session("periodo")
            Dim regGuardar As DataTable = Carga.getRegistrosParaGuardar(Session("codPreCarga"))

            Try
                regGuardar = Carga.getRegistrosParaGuardar(Session("codPreCarga"))
                Session("totalReg") = regGuardar.Rows.Count
                If Not IsNothing(regGuardar) Then
                    For Each row As DataRow In regGuardar.Rows
                        If (row.Item(7) = 2 And PermitirFueraProgramacion = 0) Or row.Item(7) = 1 Then
                            Carga.insertPagosCarga(codCarga, row.Item(2), row.Item(0), row.Item(6), banco, Session("usuario"), tipoFecha)
                            Session("actual") = regGuardar.Rows.IndexOf(row) + 1

                            '////////////////////////////////////////////////////////////////
                            ' llama a la función en SignalR, para informar del progreso de la carga
                            Dim pbconexion As New progressClass
                            pbconexion.enviarIncremento()
                            '////////////////////////////////////////////////////////////////
                        End If
                    Next
                End If
            Catch ex As Exception
            End Try

            Session("cargado") = 1
            Session("permitirFueraProgramacion") = PermitirFueraProgramacion
            Carga.actulizarFinCarga(codCarga)
        End Sub

#End Region

        Function PagosGridViewPartial() As ActionResult
            Return PartialView(Carga.getDetalleRegistrosPreCarga(Session("codPreCarga"), Session("cargado"), Session("permitirFueraProgramacion")))
        End Function

        Function PartialMemo() As ActionResult

            If Not IsNothing(Session("fileName")) Then

                Dim table As DataTable
                Try

                    table = Carga.getTotalEstadosMontos(Session("codPreCarga"))

                    ViewData("nombre") = Session("fileName")
                    ViewData("totalRegistros") = table.Rows(0).Item(0)
                    ViewData("totalPagados") = table.Rows(0).Item(1)
                    ViewData("totalNoPagados") = table.Rows(0).Item(2)
                    ViewData("FchfueraProgramacion") = table.Rows(0).Item(3)
                    ViewData("montoTotal") = Convert.ToInt32(table.Rows(0).Item(4)).ToString("C")
                    ViewData("montoPagado") = Convert.ToInt32(table.Rows(0).Item(5)).ToString("C")
                    ViewData("montoNoPagado") = Convert.ToInt32(table.Rows(0).Item(6)).ToString("C")
                    ViewData("montoFchNoProgramado") = Convert.ToInt32(table.Rows(0).Item(7)).ToString("C")

                    If table.Rows(0).Item(1) > 0 Or table.Rows(0).Item(3) > 0 Then
                        ViewData("PoderCargar") = 1
                    Else
                        ViewData("PoderCargar") = 0
                    End If

                    table = Carga.getContadorEstadosPreCarga(Session("codPreCarga"))
                    'hacer un for each y almacenarlas filas en viewdata para leerlas en la vista parcial
                    For i = 1 To table.Rows.Count
                        Dim str As String = table.Rows(i - 1).Item(0).ToString() + ": " + table.Rows(i - 1).Item(1).ToString()
                        ViewData(i) = str
                    Next

                    ViewData("num") = table.Rows.Count

                Catch ex As Exception
                End Try
            End If

            Return PartialView("PartialMemo")
        End Function

        Function exportToExcel()

            Carga.insertLog(Session("usuario"), "Descargo exporto a excel el contenido del Grid View de los pagos.", "Ninguna", 3, 0)
            Return GridViewExtension.ExportToXlsx(exportGridViewCarga.ExportGridViewSettings, Session("ds"))

        End Function

#Region "funciones para el historial de cargas"

        Function ViewHistorial() As ActionResult

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(4) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        ViewData("periodos") = Carga.getAllPeriodos()
                        ViewData("bancos") = Carga.getAllBancos()
                        Return View("ViewHistorial")
                    Else
                        Return Redirect("/Contraloria/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        <HttpPost>
        Function retornarCargas(ByVal tipo As String, ByVal periodo As String, ByVal banco As String)
            ViewData("tipo") = tipo
            ViewData("periodo") = periodo
            ViewData("banco") = banco
            Return PartialView("PartialGridViewCargas", Carga.getCargas(tipo, periodo, banco))
        End Function

        <HttpGet>
        Function ExportarHistorialCargas(ByVal tipo As String, ByVal periodo As String, ByVal banco As String)
            Dim cl_export As New Cl_ClaseExport()
            Return GridViewExtension.ExportToXlsx(Cl_ClaseExport.Fnc_ExportHistorialCarga(), Carga.getCargas(tipo, periodo, banco))
        End Function

        <HttpPost>
        Function retornarRegistrosPreCargas(ByVal tipo As String, ByVal cod As String)

            Dim dt As DataTable

            If tipo = "preCarga" Then
                dt = Carga.getRegistrosPreCargas(cod)
            Else
                dt = Carga.getRegistrosCargas(cod)
            End If

            Session("ds") = dt
            Return PartialView("PagosGridViewPartial", dt)
        End Function

        Function descargarArchivo(ByVal tipo As String, ByVal cod As String) As ActionResult

            'obtengo la información del archivo en la base de datos
            Dim codPreCarga As String
            If tipo = "Carga" Then
                codPreCarga = Carga.getCodPreCarga(cod)
                Carga.insertLog(Session("usuario"), "Descargo una copia del archivo que fue cargado en el sistema", "t_cnt_pre_cargas", 3, cod)
            Else
                codPreCarga = cod
                Carga.insertLog(Session("usuario"), "Descargo una copia del archivo que fue pre cargado en el sistema", "t_cnt_cargas", 3, cod)
            End If

            Dim FilaArchivo As DataRow = Carga.getArchivoPreCarga(codPreCarga)
            Dim nombre As String = FilaArchivo.Item(0)
            Dim data() As Byte = CType(FilaArchivo.Item(1), Byte())

            Dim path As String = Server.MapPath("~/Areas/Contraloria/Content/Uploaded/Archivos/" + nombre)
            Dim intru As Object
            Dim archivo As Object

            intru = CreateObject("Scripting.FileSystemObject")
            archivo = intru.CreateTextFile(path, True)
            archivo.Close()

            Try
                Dim fs As FileStream = New FileStream(path, IO.FileMode.OpenOrCreate, IO.FileAccess.Write)
                Dim bw As BinaryWriter = New BinaryWriter(fs)
                bw.Write(data)
                bw.Flush()
                bw.Close()
                fs.Close()
                bw = Nothing
                fs.Dispose()

                Dim _fileInfo As New FileInfo(path)
                Session("path") = path

                Return File(data, "text/plain", nombre)

            Catch ex As Exception

            End Try

            Return Nothing

        End Function

        Sub borrarArchivo()

            Try
                Dim path As String = Session("path")
                Dim file As FileInfo = New FileInfo(path)
                file.Delete()
            Catch ex As Exception
            End Try
        End Sub
#End Region

#Region "funciones para la verificación y carga de los archivos de bancarización"

        Function v_cargaBancarizacion() As ActionResult

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(4) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        ViewData("periodos") = Carga.getAllPeriodos()
                        ViewData("bancos") = Carga.getAllBancos()
                        Return View()
                    Else
                        Return Redirect("/Contraloria/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function pv_gdvArchivosBancarizacion(ByVal periodo As String, ByVal banco As String) As ActionResult
            ViewData("periodo") = periodo
            ViewData("banco") = banco
            Return PartialView(Carga.fnc_obtener_archivos_bancarizacion(periodo, banco))
        End Function

        Function pv_pvgConsolidadoCargaBancarizacion(ByVal periodo As String, ByVal banco As String, ByVal archivo As String) As ActionResult
            ViewData("periodo") = periodo
            ViewData("banco") = banco
            ViewData("archivo") = archivo
            Return PartialView(Carga.fnc_obtener_consolidado_carga_bancarizacion(periodo, banco, archivo))
        End Function

        Function cargarPagosBancarizacion(ByVal pago As String, ByVal banco As String, ByVal archivo As String) As Integer
            Return Carga.fnc_cargar_pagos_bancarizacion(pago, banco, archivo)
        End Function

        Function exportCargaBancarizacion() As ActionResult

            Dim periodo As String = EditorExtension.GetValue(Of String)("cbxPeriodo")
            Dim banco As String = EditorExtension.GetValue(Of String)("cbxBanco")
            Dim archivo As String = EditorExtension.GetValue(Of String)("txtPagoSeleccionado")

            Return PivotGridExtension.ExportToXlsx(exportPvgConsolidadoCargaBancarizacion.ExportPivotGridSettings, Carga.fnc_obtener_consolidado_carga_bancarizacion(periodo, banco, archivo), "CONSOLIDADO BANCARIZACION " + Carga.fnc_obtener_nombre_banco(banco) + " " + Carga.fnc_obtener_nombre_pago(periodo))

        End Function

        Function pv_memoBancarizacion(ByVal archivo As String) As ActionResult

            ViewData("total_registros") = Carga.fnc_obtener_total_registros_arch_bancarizacion(archivo).ToString()

            Dim table As DataTable = Carga.fnc_obtener_datos_archivo(archivo)

            ViewData("pago") = table.Rows(0).Item(0)
            ViewData("banco") = table.Rows(0).Item(1)
            ViewData("tipo") = table.Rows(0).Item(2)
            ViewData("estado") = table.Rows(0).Item(3)
            ViewData("nombre") = table.Rows(0).Item(4)
            ViewData("fecha") = table.Rows(0).Item(5)

            Dim tablaEstados = Carga.fnc_contador_est_reg_bancarizacion(archivo)

            For i = 1 To tablaEstados.Rows.Count
                Dim str As String = tablaEstados.Rows(i - 1).Item(0).ToString() + ": " + tablaEstados.Rows(i - 1).Item(1).ToString()
                ViewData(i) = str
            Next

            ViewData("num") = tablaEstados.Rows.Count

            Return PartialView()
        End Function

        Function verificarArchDetallePagos(ByVal archivo As String)
            Return Carga.fnc_verificar_arch_detalle_pago(archivo)
        End Function

        Function pv_gdvDetalleArchivoBancarizacion(ByVal archivo As String) As ActionResult
            ViewData("archivo") = archivo
            Return PartialView(Carga.fnc_obtener_detalle_registro_archivo(archivo))

        End Function

        Function exportListaArchivos(ByVal pago As String, ByVal banco As String) As ActionResult

            Dim settings As New GridViewSettings

            settings.Name = "gdvArchivosBancarizacion"
            settings.Caption = "Archivos de Bancarización"

            settings.SettingsExport.ReportHeader = "Archivos de Bancarización "
            settings.SettingsExport.FileName = "Archivos de Bancarización " + Carga.fnc_obtener_nombre_banco(banco) + " " + Carga.fnc_obtener_nombre_pago(pago)

            settings.Columns.Add("desc_tipo_arch_banc", "Tipo de Archivo")
            settings.Columns.Add("nombre_arch_bancarizacion", "Nombre Archivo")
            settings.Columns.Add("desc_estado_arch_bancarizacion", "Estado")
            settings.Columns.Add("cod_arch_bancarizacion").Visible = False

            Return GridViewExtension.ExportToXlsx(settings, Carga.fnc_obtener_archivos_bancarizacion(pago, banco))

        End Function

        Function exportDetalleGrid(ByVal archivo As String) As ActionResult

            Dim settings As New GridViewSettings

            settings.Name = "gdvArchivosBancarizacion"
            settings.Caption = "Archivos de Bancarización"

            settings.SettingsExport.ReportHeader = "Detalle del archivo " + Carga.fnc_obtener_nombre_archivo(archivo)
            settings.SettingsExport.FileName = "Detalle del archivo " + Carga.fnc_obtener_nombre_archivo(archivo)

            Return GridViewExtension.ExportToXlsx(settings, Carga.fnc_obtener_detalle_registro_archivo(archivo))

        End Function

        Function exportListadoTitulares(ByVal pago As String, ByVal banco As String, ByVal archivo As String) As ActionResult

            Dim settings As New GridViewSettings

            settings.Name = "gdvListadoTitularesBancarizacion"
            'settings.SettingsExport.FileName = "Listado de Titulares Pagados en " + Carga.fnc_obtener_nombre_archivo(archivo)
            settings.SettingsExport.FileName = "Listado de Titulares " + Carga.fnc_obtener_nombre_pago(pago)

            Return GridViewExtension.ExportToXlsx(settings, Carga.fnc_obtener_listado_titulares_pagados(pago, banco, archivo))

        End Function

#End Region

#Region "Funciones para la verificación de archivos de bancarización"

        Function v_verificacionBancarizacion() As ActionResult

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(4) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Session("path") = "C:\inetpub\wwwroot\Core\Areas\Contraloria\Content\Bancarizacion\SFTP_BANRURAL\"
                        ViewData("data") = JsonConvert.SerializeObject(Carga.getAllPeriodos(), Formatting.None)
                        Return View()
                    Else
                        Return Redirect("/Contraloria/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function sincronizar() As JsonResult

            Try

                Dim sessionOptions As New SessionOptions
                With sessionOptions
                    .Protocol = Protocol.Sftp
                    .HostName = "190.5.69.169"
                    .UserName = "SSIS"
                    .SshHostKeyFingerprint = "ssh-rsa 2048 1e:ef:af:ca:14:35:5f:58:57:b7:b9:b1:20:0a:69:f4"
                    .SshPrivateKeyPath = "C:\inetpub\wwwroot\Core\Areas\Contraloria\Content\Bancarizacion\SSIS_PK.ppk"
                    .PortNumber = "53125"
                End With

                Using session As New Session

                    ' Connect
                    session.Open(sessionOptions)

                    ' Synchronize files
                    Dim synchronizationResult As SynchronizationResult
                    synchronizationResult = session.SynchronizeDirectories(SynchronizationMode.Local, "C:\inetpub\wwwroot\Core\Areas\Contraloria\Content\Bancarizacion\SFTP_BANRURAL\Recepcion", "/cygdrive/e/SSIS/Envio", False)

                    ' Throw on any error
                    ' synchronizationResult.Check()

                    If synchronizationResult.IsSuccess Then
                        Return Json("1")
                    Else
                        Return Json("0")
                    End If

                End Using
                Return Json("1")


            Catch ex As Exception
                Return Json("0")
            End Try

        End Function

        Function pv_fmArchivos() As ActionResult
            Return PartialView("pv_fmArchivos", Session("path"))
        End Function

        Function downloadFiles() As ActionResult

            Dim settings As New Mvc.FileManagerSettings
            settings.SettingsEditing.AllowDownload = True
            settings.Name = "fmArchivos"

            Return FileManagerExtension.DownloadFiles(settings, Session("path"))
        End Function

        Function procesar() As JsonResult

            Dim ruta_banrural As String = "C:\inetpub\wwwroot\Core\Areas\Contraloria\Content\Bancarizacion\SFTP_BANRURAL\Recepcion\"
            Dim listaArchivos As List(Of String) = obtenerArchivosDirectorio(ruta_banrural + "*.txt")
            Dim strArchivos As String = ""
            Dim inicio As String = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            Dim fin As String = ""
            'variables para anular archivos anteriores
            Dim listaPagos As New List(Of Contador)
            Dim listaBancos As New List(Of Contador)
            Dim pagoFinal As New Contador
            Dim bancoFinal As New Contador
            Dim tmpNombre As String

            If (listaArchivos.Count > 0) Then

                For i = 0 To listaArchivos.Count - 1

                    If (listaArchivos(i).Contains("CONF_PRE_APERTURA") And i > 0) Then

                        tmpNombre = listaArchivos(0)
                        listaArchivos(0) = listaArchivos(i)
                        listaArchivos(i) = tmpNombre

                    End If

                    If (listaArchivos(i).Contains("CONF_DETALLE_PAGOS") And i <> 1) Then

                        tmpNombre = listaArchivos(1)
                        listaArchivos(1) = listaArchivos(i)
                        listaArchivos(i) = tmpNombre

                    End If

                    If (listaArchivos(i).Contains("CIERRE_APERT") And i <> listaArchivos.Count - 2) Then

                        tmpNombre = listaArchivos(listaArchivos.Count - 2)
                        listaArchivos(listaArchivos.Count - 2) = listaArchivos(i)
                        listaArchivos(i) = tmpNombre

                    End If

                    If (listaArchivos(i).Contains("CIERRE_DETALLE_PAGOS") And i <> listaArchivos.Count - 1) Then

                        tmpNombre = listaArchivos(listaArchivos.Count - 1)
                        listaArchivos(listaArchivos.Count - 1) = listaArchivos(i)
                        listaArchivos(i) = tmpNombre

                    End If

                Next


                For i = 0 To listaArchivos.Count - 1

                    'tamaño del array según el nombre del archivo
                    '1  1:7 2:8
                    '2  1:7 2:8
                    '3  1:7 2:8
                    '4  1:6 2:7
                    '5  1:7 2:8

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'proceso para obtener el tipo de archivo recibido
                    Dim numero_pago As Integer = 0
                    Dim año_pago As Integer = 0
                    Dim cod_pago As Integer = 0
                    Dim fecha As String = ""
                    Dim archivo_original As String = ""
                    Dim pagador As String = ""
                    Dim cod_banco As Integer = 0
                    Dim flag_original As Boolean = False
                    Dim cod_estado As Integer = 0
                    Dim tipo_archivo As Integer = 0
                    Dim arrayNombre() As String = listaArchivos(i).Split("_")

                    tipo_archivo = Carga.fnc_obtener_tipo_archivo(arrayNombre(0), arrayNombre(1))

                    If Not tipo_archivo = 0 Then

                        If tipo_archivo = 3 Then
                            numero_pago = Convert.ToInt16(arrayNombre(arrayNombre.Length - 4))
                            año_pago = Convert.ToInt16(arrayNombre(arrayNombre.Length - 3))
                            fecha = arrayNombre(arrayNombre.Length - 2).Substring(0, 8)
                        Else
                            numero_pago = Convert.ToInt16(arrayNombre(arrayNombre.Length - 3))
                            año_pago = Convert.ToInt16(arrayNombre(arrayNombre.Length - 2))
                            fecha = arrayNombre(arrayNombre.Length - 1).Substring(0, 8)
                        End If

                        cod_pago = Carga.fnc_obtener_codigo_pago(numero_pago, año_pago)

                        'para obtener pago de los archivos y anular archivos anteriores
                        If i = 0 Then
                            'meter el primer código a la lista
                            Dim tmp As New Contador()
                            tmp.codigo = cod_pago
                            tmp.contador = 1

                            listaPagos.Add(tmp)
                        Else
                            'recorrer la lista para ver si existe y en caso de que ya exista sumar uno

                            For Each item In listaPagos
                                If item.codigo = cod_pago Then
                                    listaPagos.Find(Function(x) x.codigo = item.codigo).contador += 1
                                Else
                                    Dim tmp As New Contador()
                                    tmp.codigo = cod_pago
                                    tmp.contador = 1

                                    listaPagos.Add(tmp)
                                End If
                            Next
                        End If

                        If Not cod_pago = 0 Then

                            If tipo_archivo = 4 Or tipo_archivo = 3 Then
                                pagador = arrayNombre(2)
                            Else
                                pagador = arrayNombre(3)
                            End If

                            cod_banco = Carga.fnc_obtener_codigo_banco(pagador)

                            'para obtener banco de los archivos y anular archivos anteriores
                            If i = 0 Then
                                'meter el primer código a la lista
                                Dim tmp As New Contador()
                                tmp.codigo = cod_banco
                                tmp.contador = 1

                                listaBancos.Add(tmp)
                            Else
                                'recorrer la lista para ver si existe y en caso de que ya exista sumar uno

                                For Each item In listaBancos
                                    If item.codigo = cod_banco Then
                                        listaBancos.Find(Function(x) x.codigo = item.codigo).contador += 1
                                    Else
                                        Dim tmp As New Contador()
                                        tmp.codigo = cod_banco
                                        tmp.contador = 1

                                        listaBancos.Add(tmp)
                                    End If
                                Next
                            End If

                            If Not cod_banco = 0 Then

                                If tipo_archivo = 2 Or tipo_archivo = 5 Then
                                    archivo_original = "C:\inetpub\wwwroot\Core\Areas\Contraloria\Content\Bancarizacion\SFTP_BANRURAL\Envio\" + "DETALLE_PAGOS_" + pagador + "_" + numero_pago.ToString() + "_" + año_pago.ToString() + "_" + fecha + ".txt"
                                Else
                                    archivo_original = "C:\inetpub\wwwroot\Core\Areas\Contraloria\Content\Bancarizacion\SFTP_BANRURAL\Envio\" + "PRE_APERTURA_" + pagador + "_" + numero_pago.ToString() + "_" + año_pago.ToString() + "_" + fecha + ".txt"
                                End If

                                If obtenerArchivosDirectorio(archivo_original).Count = 1 Then
                                    cod_estado = 5 'listo para procesar
                                Else
                                    cod_estado = 4 'no se encontro el archivo original
                                End If

                            Else
                                cod_estado = 3 'no se pudo identificar el banco que envio el archivo
                            End If
                        Else
                            cod_estado = 2 'no se pudo identificar el pago al que pertenece el archivo
                        End If
                    Else
                        cod_estado = 1 'no de pudo identificar el tipo de archivo recibido
                    End If

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Dim path As String = Convert.ToString(ruta_banrural + listaArchivos(i))
                    'pasarArchivo1252(path)
                    Dim _tempByte() As Byte = Nothing

                    Try
                        Dim _fileInfo As New FileInfo(path)
                        Dim _NumBytes As Long = _fileInfo.Length
                        Dim _FStream As New FileStream(path, FileMode.Open, FileAccess.Read)
                        Dim _BinaryReader As New BinaryReader(_FStream)

                        _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
                        _fileInfo = Nothing
                        _NumBytes = 0
                        _FStream.Close()
                        _FStream.Dispose()
                        _BinaryReader.Close()
                    Catch ex As Exception
                    End Try

                    'leer ultima linea del archivo y agrega el salto de linea si es necesario
                    Dim textoArchivo As String = IO.File.ReadAllText(path)
                    Dim last As Char = textoArchivo(textoArchivo.Length - 1)

                    If last <> Chr(10) Then
                        Using sw As StreamWriter = IO.File.AppendText(path)
                            sw.WriteLine("")
                        End Using
                    End If

                    Carga.fnc_guardar_archivo(cod_pago, cod_banco, tipo_archivo, listaArchivos(i), _tempByte, cod_estado, path, archivo_original)
                    'File.Delete(path)

                    If i = 0 Then
                        strArchivos = "'" + listaArchivos(i)
                    Else
                        strArchivos += "','" + listaArchivos(i)
                    End If

                Next
                fin = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                strArchivos += "'"

                'recorro las lista para obtener el pago y banco que mas se repitio en los archivos
                'en teoría todos los archivos debén de ser del mismo pago y banco
                'pero por las dudas busco el que mas se repite y anulo los archivos de dichos pagos y bancos

                pagoFinal = listaPagos.First()
                If listaPagos.Count > 1 Then
                    For i = 1 To listaPagos.Count
                        If listaPagos(i).contador > pagoFinal.contador Then
                            pagoFinal = listaPagos(i)
                        End If
                    Next
                End If

                bancoFinal = listaBancos.First()
                If listaBancos.Count > 1 Then
                    For i = 1 To listaBancos.Count
                        If listaBancos(i).contador > bancoFinal.contador Then
                            bancoFinal = listaBancos(i)
                        End If
                    Next
                End If

                Carga.fnc_anular_archivos_anteriores(pagoFinal.codigo, bancoFinal.codigo, inicio)

                Session("strArchivos") = strArchivos
                Session("inicio") = inicio
                Session("fin") = fin

                Return Json("1")

            End If

            Return Json("0")

        End Function

        Public Function pv_gdvArchivosProcesados() As ActionResult
            Return PartialView(Carga.fnc_obtener_archivos_procesados(Session("strArchivos"), Session("inicio"), Session("fin")))
        End Function

        Public Function moverArchivosHistorial(ByVal año As String, ByVal pago As String) As ActionResult

            Dim path As String = "C:\inetpub\wwwroot\Core\Areas\Contraloria\Content\Bancarizacion\SFTP_BANRURAL\Historial\" + año
            Dim listaArchivos As List(Of String) = obtenerArchivosDirectorio("C:\inetpub\wwwroot\Core\Areas\Contraloria\Content\Bancarizacion\SFTP_BANRURAL\Recepcion\*.txt")
            Dim now As Date = Date.Now()
            Dim fecha = If(now.Day < 10, "0" + now.Day.ToString(), now.Day.ToString()) + If(now.Month < 10, "0" + now.Month.ToString(), now.Month.ToString()) + now.Year.ToString()

            Try

                If Not Directory.Exists(path) Then
                    Directory.CreateDirectory(path)
                End If

                path += "\" + pago

                If Not Directory.Exists(path) Then
                    Directory.CreateDirectory(path)
                End If

                path += "\"

                If Not Directory.Exists(path + fecha) Then
                    path += fecha
                    Directory.CreateDirectory(path)
                Else
                    Dim directorio As New DirectoryInfo(path)
                    Dim mayor As Integer = 1

                    For Each dir As DirectoryInfo In directorio.GetDirectories()

                        If dir.Name.Substring(0, 8) = fecha And dir.Name.Length > 8 Then
                            If Convert.ToInt16(dir.Name.Substring(9)) > mayor Then
                                mayor = Convert.ToInt16(dir.Name.Substring(9))
                            End If
                        End If
                    Next

                    path += fecha + "_" + Convert.ToString(mayor + 1)
                    Directory.CreateDirectory(path)

                End If

                path += "\"

                For Each curf In listaArchivos
                    Dim DirInfo As New DirectoryInfo("C:\inetpub\wwwroot\Core\Areas\Contraloria\Content\Bancarizacion\SFTP_BANRURAL\Recepcion\" + curf)
                    Directory.Move("C:\inetpub\wwwroot\Core\Areas\Contraloria\Content\Bancarizacion\SFTP_BANRURAL\Recepcion\" + curf, path + DirInfo.Name)
                Next

            Catch ex As Exception
            End Try

            Return Nothing

        End Function

        Public Function obtenerArchivosDirectorio(ByRef rutaArchivo As String) As List(Of String)

            Dim listaNombre As List(Of String) = New List(Of String)
            Dim ruta As String = rutaArchivo

            Dim nombre As String = Dir(ruta)

            Do While nombre <> ""
                listaNombre.Add(nombre)
                nombre = Dir()
            Loop

            If listaNombre.Count > 1 Then

                'ordenamiento por método burbufa
                Dim aux As String
                Dim i, j As Integer

                For i = 1 To listaNombre.Count - 1
                    For j = 0 To listaNombre.Count - i - 1
                        If valor_tipo_archivo(listaNombre(j)) > valor_tipo_archivo(listaNombre(j + 1)) Then
                            aux = listaNombre(j + 1)
                            listaNombre(j + 1) = listaNombre(j)
                            listaNombre(j) = aux
                        End If
                    Next
                Next
            End If

            Return listaNombre

        End Function

        Function valor_tipo_archivo(nombre)

            Dim arrayNombre() As String = nombre.Split("_")

            Select Case arrayNombre(0) + "_" + arrayNombre(1)
                Case "CONF_PRE_APERTURA"
                    Return 1
                Case "CONF_DETALLE"
                    Return 2
                Case "CONF_APERTURA"
                    Return 3
                Case "CIERRE_APERTURA"
                    Return 4
                Case "CIERRE_DETALLE"
                    Return 5
                Case Else
                    Return 0
            End Select

        End Function

        Sub pasarArchivo1252(ByVal nombre As String)

            Dim file As FileInfo = New FileInfo(nombre)
            Dim sr As StreamReader = file.OpenText()

            Dim arr = Encoding.GetEncoding(1252).GetBytes(sr.ReadToEnd())   'funciona si el archivo es utf8
            'Dim arr = Encoding.Convert(Encoding.Unicode, Encoding.GetEncoding(1252), Encoding.Unicode.GetBytes(sr.ReadToEnd()))
            'Dim arr = Encoding.Convert(Encoding.ASCII, Encoding.GetEncoding(1252), Encoding.ASCII.GetBytes(sr.ReadToEnd()))

            sr.Close()
            file.Delete()
            Dim sw As StreamWriter = New StreamWriter(New FileStream(nombre, FileMode.OpenOrCreate), Encoding.GetEncoding(1252))
            sw.Close()
            IO.File.WriteAllBytes(nombre, arr)

        End Sub

#End Region

    End Class

#Region "Clase para subir el archivo"

    Public Class Upload

        Public Shared ReadOnly UploadControlValidationSettings As New UploadControlValidationSettings() With {.AllowedFileExtensions = New String() {".txt"}, .MaxFileSize = 20971520}

        Public Shared Sub ucCallbacks_FileUploadComplete(sender As Object, e As FileUploadCompleteEventArgs)
            If e.UploadedFile.IsValid Then
                If e.UploadedFile IsNot Nothing AndAlso e.UploadedFile.ContentLength > 0 Then
                    Dim fileName = Path.GetFileName(e.UploadedFile.FileName)
                    Dim fullPath As String = "C:\inetpub\wwwroot\Core\Areas\Contraloria\Content\Uploaded\Archivos\" + fileName
                    e.UploadedFile.SaveAs(fullPath + "_2.txt")
                    e.CallbackData = fullPath
                    HttpContext.Current.Session("path") = fullPath
                    HttpContext.Current.Session("fileName") = fileName

                    Dim file As FileInfo = New FileInfo(fullPath + "_2.txt")
                    Dim sr As StreamReader = file.OpenText()
                    Dim sw As StreamWriter = New StreamWriter(New FileStream(fullPath, FileMode.OpenOrCreate), Encoding.UTF8)
                    Dim arr = Encoding.UTF8.GetBytes(sr.ReadToEnd())
                    sr.Close()
                    file.Delete()
                    sw.Close()
                    IO.File.WriteAllBytes(fullPath, arr)

                End If
            End If
        End Sub


    End Class
#End Region

#Region "clase para exportar gridview"

    Public NotInheritable Class exportGridViewCarga

        Private Shared exportCargaSettings As GridViewSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportGridViewSettings() As GridViewSettings
            Get
                If exportCargaSettings Is Nothing Then
                    exportCargaSettings = CreateExportGridViewSettings()
                End If
                Return exportCargaSettings
            End Get
        End Property

        Private Shared Function CreateExportGridViewSettings() As GridViewSettings
            Dim settings As New GridViewSettings()

            settings.Name = "gvPagosGridView"
            settings.Width = Unit.Pixel(1650)

            settings.SettingsPager.Position = PagerPosition.Bottom
            settings.SettingsPager.FirstPageButton.Visible = True
            settings.SettingsPager.LastPageButton.Visible = True
            settings.SettingsPager.PageSizeItemSettings.Visible = True
            settings.SettingsPager.PageSizeItemSettings.Items = New String() {"10", "20", "50"}
            settings.SettingsBehavior.AllowGroup = True
            settings.SettingsBehavior.AllowSort = True
            settings.Settings.ShowGroupPanel = True

            settings.Settings.ShowHeaderFilterButton = True
            settings.SettingsPopup.HeaderFilter.Height = 200

            settings.SettingsExport.ReportHeader = "PAGOS EN EL ARCHIVO " + HttpContext.Current.Session("fileName")
            settings.SettingsExport.FileName = System.Web.HttpContext.Current.Session("fileName")

            settings.KeyFieldName = "Nro"

            settings.Columns.Add("Nro")
            settings.Columns.Add(
                Sub(col)
                    col.FieldName = "Estado"
                    col.GroupIndex = 0
                    col.SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                End Sub)
            settings.Columns.AddBand(
                Sub(at)
                    at.Caption = "Archivo de Texto"
                    at.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    at.Columns.Add("Página Archivo", "Página").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    at.Columns.Add("Página Archivo", "Página").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    at.Columns.Add("Linea Archivo", "Linea").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    at.Columns.Add("Monto Archivo", "Monto").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    at.Columns.Add("Fecha Archivo", "Fecha").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    at.Columns.Add("Estado Archivo", "Estado").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                End Sub)
            settings.Columns.AddBand(
                Sub(ag)
                    ag.Caption = "Área Geográfica"
                    ag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    ag.Columns.Add("Departamento").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    ag.Columns.Add("Municipio").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    ag.Columns.Add("Aldea").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                End Sub)
            settings.Columns.AddBand(
                Sub(ban)
                    ban.Caption = "Datos Banco"
                    ban.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    ban.Columns.Add("Fondo").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    ban.Columns.Add("Banco", "Nombre").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    ban.Columns.Add("Sucursal").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList

                End Sub)
            settings.Columns.AddBand(
                Sub(tit)
                    tit.Caption = "Titular"
                    tit.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    tit.Columns.Add("Nombre Titular", "Nombre").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    tit.Columns.Add("Identidad").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                End Sub)
            settings.Columns.AddBand(
                Sub(pag)
                    pag.Caption = "Pago"
                    pag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    pag.Columns.Add("Página").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    pag.Columns.Add("Linea").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    pag.Columns.Add("Monto").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    pag.Columns.Add("Fecha de Pago", "Fecha").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                End Sub)
            Return settings
        End Function
    End Class

    Public NotInheritable Class exportPvgConsolidadoCargaBancarizacion

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportPivotGridSettings() As PivotGridSettings

            Get
                exportSettings = CreateExportPvgConsolidadoPago()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgConsolidadoPago() As PivotGridSettings

            Dim settings As New PivotGridSettings()

            settings.Name = "pvgConsolidado"
            settings.Width = Unit.Percentage(120)

            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False

            settings.EnableCallbackAnimation = True
            settings.EnablePagingCallbackAnimation = True

            settings.OptionsPager.RowsPerPage = 20

            settings.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Auto
            settings.OptionsView.DataHeadersDisplayMode = PivotDataHeadersDisplayMode.Popup
            settings.OptionsView.EnableFilterControlPopupMenuScrolling = True
            settings.OptionsView.RowTreeOffset = 3
            settings.OptionsView.RowTotalsLocation = PivotRowTotalsLocation.Far

            settings.OptionsData.DataFieldUnboundExpressionMode = DataFieldUnboundExpressionMode.UseSummaryValues


            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 0
                    field.Caption = "Fondo"
                    field.FieldName = "fond_nombre"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 1
                    field.Caption = "Municipio"
                    field.FieldName = "desc_municipio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.RowArea
                    field.Index = 0
                    field.Caption = "Pagador"
                    field.FieldName = "Nombre_Pagador"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.RowArea
                    field.Index = 1
                    field.Caption = "Departamento"
                    field.FieldName = "desc_departamento"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 0
                    field.Caption = "Hogares Programados"
                    field.FieldName = "hogares_programados"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 1
                    field.Caption = "Monto Programado"
                    field.FieldName = "monto_programado"
                    field.CellFormat.FormatString = "c"
                    field.CellFormat.FormatType = FormatType.Custom
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 2
                    field.Caption = "Hogares Pagados"
                    field.FieldName = "hogares_pagados"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 3
                    field.Caption = "Monto Pagado"
                    field.FieldName = "monto_pagado"
                    field.CellFormat.FormatString = "c"
                    field.CellFormat.FormatType = FormatType.Custom
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 4
                    field.Caption = "Hogares No Pagados"
                    field.FieldName = "hogares_no_pagados"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 5
                    field.Caption = "Monto No Pagado"
                    field.FieldName = "monto_no_pagado"
                    field.CellFormat.FormatString = "c"
                    field.CellFormat.FormatType = FormatType.Custom
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 6
                    field.Caption = "% Cumplimiento"
                    field.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                    field.UnboundExpression = String.Format("([{0}] * 100)/[{1}]", settings.Fields("hogares_pagados").ID, settings.Fields("hogares_programados").ID)
                    field.UnboundFieldName = "cumplimiento"
                    field.CellFormat.FormatType = FormatType.Numeric
                    field.CellFormat.FormatString = "n2"
                End Sub)

            Return settings

        End Function

    End Class

#End Region

#Region "clase archivo"

    ' clase con atributos que contiene el archivo de texto
    Public Class archivo

        Private _idTitular As String
        Public Property idTitular As String
            Get
                Return _idTitular
            End Get
            Set(value As String)
                _idTitular = value
            End Set
        End Property

        Private _codRegistro As String
        Public Property codRegistro As String
            Get
                Return _codRegistro
            End Get
            Set(value As String)
                _codRegistro = value
            End Set
        End Property

        Private _linea As String
        Public Property linea() As String
            Get
                Return _linea
            End Get
            Set(value As String)
                _linea = value
            End Set
        End Property

        Private _pagina As String
        Public Property pagina() As String
            Get
                Return _pagina
            End Get
            Set(value As String)
                _pagina = value
            End Set
        End Property

        Private _registro As String
        Public Property registro() As String
            Get
                Return _registro
            End Get
            Set(value As String)
                _registro = value
            End Set
        End Property

        Private _monto As String
        Public Property monto() As String
            Get
                Return _monto
            End Get
            Set(value As String)
                _monto = value
            End Set
        End Property

        Private _fecha As String
        Public Property fecha() As String
            Get
                Return _fecha
            End Get
            Set(value As String)
                _fecha = value
            End Set
        End Property

        Private _guardar As Integer
        Public Property Guardar() As String
            Get
                Return _guardar
            End Get
            Set(ByVal value As String)
                _guardar = value
            End Set
        End Property


    End Class

#End Region

#Region "clase para contadores de pagos y bancos"

    Public Class Contador
        Public codigo As Integer
        Public contador As Integer
    End Class

#End Region

#Region "clase de signalR para el progress bar"

    Public Class progressClass
        Inherits Hub

        Public Sub enviarIncremento()

            Dim total As Integer = System.Web.HttpContext.Current.Session("totalReg")
            Dim actual As Integer = System.Web.HttpContext.Current.Session("actual")
            Dim connectionID As String = HttpContext.Current.Session("signalrId")

            Dim conexion As IHubContext = GlobalHost.ConnectionManager.GetHubContext(Of progressClass)()
            Dim progreso As Integer = (actual / total) * 100

            If progreso = 100 And actual < total Then
                progreso = 99
            End If

            conexion.Clients.Client(connectionID).incrementarProgressBar(progreso)
        End Sub

        Public Sub finPrecarga()
            Dim connectionID As String = HttpContext.Current.Session("signalrId")
            Dim conexion As IHubContext = GlobalHost.ConnectionManager.GetHubContext(Of progressClass)()
            conexion.Clients.Client(connectionID).finPrecarga()
        End Sub
    End Class

#End Region

#Region "Clase Parcial"

    Partial Public Class Cl_ClaseExport

        Public Shared Function Fnc_ExportHistorialCarga()
            Dim settings As New GridViewSettings()
            settings.Name = "gvCargas"
            settings.Caption = "Historial de Cargas y Pre cargas"
            settings.SettingsPager.PageSize = 10
            settings.KeyFieldName = "codigo"
            settings.CommandColumn.Visible = True
            settings.CommandColumn.ShowSelectCheckbox = True
            settings.CommandColumn.Caption = "Slc"
            settings.SettingsBehavior.AllowSelectSingleRowOnly = True
            settings.Columns.AddBand(
                Sub(band)
                    band.Caption = "Filtros"
                    band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    band.Columns.Add("Tipo")
                    band.Columns.Add("Periodo")
                    band.Columns.Add("Banco")
                End Sub)
            settings.Columns.AddBand(
                Sub(band)
                    band.Caption = "Archivo"
                    band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    band.Columns.Add("Nombre Archivo")
                End Sub)
            settings.Columns.AddBand(
                Sub(band)
                    band.Caption = "Carga"
                    band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    band.Columns.Add("Nombre Carga")
                    band.Columns.Add("Usuario")
                    band.Columns.Add("Inicio de Carga")
                    band.Columns.Add("Fin de Carga")
                End Sub)
            settings.Columns.Add("codigo").Visible = False
            Return settings
        End Function

    End Class

#End Region

End Namespace