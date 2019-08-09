Imports DevExpress.Web
Imports DevExpress.Web.Mvc
Imports System.Web
Imports System.IO

Namespace SIG.Areas.Contraloria.Controllers

    Public Class EscaneoController
        Inherits System.Web.Mvc.Controller

        Dim Escaneo As Models.Escaneo = New SIG.Areas.Contraloria.Models.Escaneo
        Dim login As New Global.SIG.Cl_Login

#Region "funciones generales"

        Function PartialCbxPeriodo()
            Return PartialView(Escaneo.getAllPeriodos())
        End Function

        Function PartialCbxFondo() As ActionResult
            Dim periodo As String = Request.Params("periodo")
            Return PartialView(Escaneo.getFondosByPeriodo(periodo))
        End Function

#End Region

#Region "funciones para la pantalla de escaneo"

        Function ViewEscaneo() As ActionResult

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(4) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        ViewData("dpto") = Escaneo.getAllDptos()
                        ViewData("documentos") = Escaneo.getTiposDocumento()
                        ViewData("banco") = Escaneo.getAllBancos()
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

        Function datosPeriodo()
            Return Escaneo.getAllPeriodos()
        End Function

        Function PartialCbxLeitz() As ActionResult
            Dim periodo As String = Request.Params("periodo")
            Dim fondo As String = Request.Params("fondo")
            Return PartialView(Escaneo.getInfoLeitz(periodo, fondo))
        End Function

        '<HttpPost> _
        'Function AgregarLeitz(ByVal codPeriodo As String, ByVal codFondo As String, ByVal desc As String)

        '    If Escaneo.insertNewLeitz(codPeriodo, codFondo, desc) Then
        '        Return "true"
        '    Else
        '        Return "false"
        '    End If
        'End Function

        Function UploadControlCallback() As ActionResult
            UploadControlExtension.GetUploadedFiles("uc", UploadDoc.UploadControlValidationSettings, AddressOf UploadDoc.ucCallbacks_FileUploadComplete)
            Return Nothing
        End Function

        Function PartialVisorDocumento() As ActionResult
            'ViewData("path") = "~\Areas\Contraloria\Content\Uploaded\Archivos\" + Session("fileName")
            ViewData("path") = "C:\inetpub\wwwroot\Core\Areas\Contraloria\Content\Uploaded\Archivos\" + Session("fileName")
            ViewData("nombre") = Session("fileName")
            Return PartialView("PartialVisorDocumento")
        End Function

        'Function insertarDocumento(ByVal leitz As String, ByVal dpto As String, ByVal banco As String, ByVal tipo As String, ByVal inicio As String, ByVal fin As String)

        '    Dim path As String = Convert.ToString(Session("path"))
        '    Dim nombre As String = Session("fileName")
        '    Dim _tempByte() As Byte = Nothing

        '    Try
        '        Dim _fileInfo As New FileInfo(path)
        '        Dim _NumBytes As Long = _fileInfo.Length
        '        Dim _FStream As New FileStream(path, IO.FileMode.Open, IO.FileAccess.Read)
        '        Dim _BinaryReader As New BinaryReader(_FStream)

        '        _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
        '        _fileInfo = Nothing
        '        _NumBytes = 0
        '        _FStream.Close()
        '        _FStream.Dispose()
        '        _BinaryReader.Close()
        '    Catch ex As Exception
        '    End Try

        '    Dim _inicio As Date
        '    If Not inicio = "" Then
        '        _inicio = CDate(inicio)
        '    End If

        '    Dim _fin As Date
        '    If Not fin = "" Then
        '        _fin = CDate(fin)
        '    End If

        '    Dim num As Integer = Escaneo.insertDocumento(leitz, dpto, banco, tipo, nombre, _tempByte, _inicio, _fin)

        '    If num >= 1 Then
        '        FileIO.FileSystem.DeleteFile(path)
        '        Return "true"
        '    Else
        '        FileIO.FileSystem.DeleteFile(path)
        '        Return "false"
        '    End If

        'End Function

        Function guardarDocumento(ByVal leitz As String, ByVal dpto As String, ByVal banco As String, ByVal tipo As String, ByVal inicio As String, ByVal fin As String)

            Dim path As String = Convert.ToString(Session("path"))
            Dim nombre As String = Session("fileName")
            Dim url As String = "C:\inetpub\wwwroot\Core\Areas\Contraloria\Content\Digitalizacion\" + leitz + "\" + Session("fileName")

            Dim _inicio As Date
            If Not inicio = "" Then
                _inicio = CDate(inicio)
            End If

            Dim _fin As Date
            If Not fin = "" Then
                _fin = CDate(fin)
            End If

            Dim num As Integer = Escaneo.guardarDocumento(leitz, dpto, banco, tipo, nombre, url, _inicio, _fin)

            If num >= 1 Then
                FileIO.FileSystem.MoveFile(path, url, True)
                Return "true"
            Else
                FileIO.FileSystem.DeleteFile(path)
                Return "false"
            End If

        End Function

#End Region

#Region "funciones para la busqueda"

        Function ViewBusquedaDocumentos() As ActionResult

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(4) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        ViewData("dpto") = Escaneo.getAllDptos()
                        ViewData("documentos") = Escaneo.getTiposDocumento()
                        ViewData("banco") = Escaneo.getAllBancos()

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

        Function PartialGridMaestroLeitz(ByVal periodo As String, ByVal fondo As String, ByVal dpto As String, ByVal banco As String, ByVal tipo As String) As ActionResult

            ViewData("periodo") = periodo
            ViewData("fondo") = fondo
            ViewData("dpto") = dpto
            ViewData("tipo") = tipo

            Return PartialView(Escaneo.getLeitz(periodo, fondo, dpto, banco, tipo))
        End Function

        Function PartialGridDetalleDocumentosLeitz(ByVal periodo As String, ByVal fondo As String, ByVal dpto As String, ByVal banco As String, ByVal tipo As String, ByVal leitz As String)

            ViewData("periodo") = periodo
            ViewData("fondo") = fondo
            ViewData("dpto") = dpto
            ViewData("tipo") = tipo
            ViewData("leitz") = leitz

            Return PartialView(Escaneo.getDocumentosLeitz(periodo, fondo, dpto, banco, tipo, leitz))
        End Function

        'Function ViewDocumento(ByVal cod As String)

        '    Dim FilaArchivo As DataRow = Escaneo.getDocumento(cod)
        '    Dim nombre As String = FilaArchivo.Item(0)
        '    Dim data() As Byte = CType(FilaArchivo.Item(1), Byte())

        '    Dim path As String = Server.MapPath("~/Areas/Contraloria/Content/Uploaded/Archivos/" + nombre)
        '    Dim intru As Object
        '    Dim archivo As Object

        '    intru = CreateObject("Scripting.FileSystemObject")
        '    archivo = intru.CreateTextFile(path, True)
        '    archivo.Close()

        '    Try
        '        Dim fs As IO.FileStream = New IO.FileStream(path, IO.FileMode.OpenOrCreate, IO.FileAccess.Write)
        '        Dim bw As IO.BinaryWriter = New IO.BinaryWriter(fs)
        '        bw.Write(data)
        '        bw.Flush()
        '        bw.Close()
        '        fs.Close()
        '        bw = Nothing
        '        fs.Dispose()

        '        Dim _fileInfo As New IO.FileInfo(path)

        '        Session("path") = path

        '    Catch ex As Exception
        '        Return Nothing
        '    End Try

        '    Return New FilePathResult(path, "application/pdf")
        'End Function

        Function ViewDocumento(ByVal cod As String)
            Return New FilePathResult(Escaneo.getUrlDocumento(cod), "application/pdf")
        End Function

        'Sub borrarArchivo()

        '    Try
        '        Dim path As String = Session("path")
        '        Dim file As FileInfo = New FileInfo(path)
        '        file.Delete()
        '    Catch ex As Exception

        '    End Try
        'End Sub

#End Region

#Region "funciones para la administración de Leitz"

        Function v_administracionLeitz() As ActionResult
            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(4) Then
                    'login.Fnc_MenuModulo()
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
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

        Function pv_dgvAdministrarLeitz() As ActionResult

            Dim datos As DataSet = Escaneo.getDatosAdministrarLeitz()

            ViewData("pagos") = datos.Tables(1)
            ViewData("fondos") = datos.Tables(2)

            Return PartialView(datos.Tables(0))
        End Function

        Function nuevoLeitz(ByVal cod_leitz As String, ByVal cod_pago As String, ByVal cod_fondo As String, ByVal numero_leitz As String, ByVal descripcion_leitz As String) As ActionResult

            Dim codigo As Integer = Escaneo.guardarLeitz(cod_pago, cod_fondo, numero_leitz, descripcion_leitz)

            If codigo > 0 Then

                Dim path As String = "C:\inetpub\wwwroot\Core\Areas\Contraloria\Content\Digitalizacion\" + codigo.ToString()

                If Not Directory.Exists(path) Then
                    Directory.CreateDirectory(path)
                End If

            End If

            Dim datos As DataSet = Escaneo.getDatosAdministrarLeitz()

            ViewData("pagos") = datos.Tables(1)
            ViewData("fondos") = datos.Tables(2)
            Return PartialView("pv_dgvAdministrarLeitz", datos.Tables(0))
        End Function

        Function actualizarLeitz(ByVal cod_leitz As String, ByVal cod_pago As String, ByVal cod_fondo As String, ByVal numero_leitz As String, ByVal descripcion_leitz As String) As ActionResult

            Escaneo.actualizarLeitz(cod_leitz, cod_pago, cod_fondo, numero_leitz, descripcion_leitz)

            Dim datos As DataSet = Escaneo.getDatosAdministrarLeitz()

            ViewData("pagos") = datos.Tables(1)
            ViewData("fondos") = datos.Tables(2)
            Return PartialView("pv_dgvAdministrarLeitz", datos.Tables(0))

        End Function


#End Region

    End Class


#Region "Clase para subir el archivo"

    Public Class UploadDoc

        Public Const UploadDirectory As String = "C:\inetpub\wwwroot\Core\Areas\Contraloria\Content\Uploaded\Archivos\"
        Public Shared ReadOnly UploadControlValidationSettings As New UploadControlValidationSettings() With {.AllowedFileExtensions = New String() {".pdf"}, .MaxFileSize = 20971520}

        Public Shared Sub ucCallbacks_FileUploadComplete(sender As Object, e As FileUploadCompleteEventArgs)
            If e.UploadedFile.IsValid Then
                If e.UploadedFile IsNot Nothing AndAlso e.UploadedFile.ContentLength > 0 Then
                    Dim fileName = Path.GetFileName(e.UploadedFile.FileName)
                    'Dim fullPath = Path.Combine(HttpContext.Current.Server.MapPath(UploadDirectory), fileName)
                    Dim fullPath = UploadDirectory + fileName
                    e.UploadedFile.SaveAs(fullPath)
                    e.CallbackData = fullPath
                    HttpContext.Current.Session("path") = fullPath
                    HttpContext.Current.Session("fileName") = fileName
                End If
            End If
        End Sub

    End Class
#End Region

End Namespace