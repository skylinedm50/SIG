Namespace SIG.Areas.Planilla.Controllers
    Public Class ParametrosController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Planilla/Parametros
        Private login As New Global.SIG.Cl_Login

        Dim objPago As New SIG.Areas.Planilla.Models.cl_pago
        Dim objEsquema As New SIG.Areas.Planilla.Models.cl_esquema
        Dim objCobertura As New SIG.Areas.Planilla.Models.cl_cobertura_planilla
        Dim objEstableFondo As New SIG.Areas.Planilla.Models.cl_establecimiento_fondo
        Dim objMecaniPago As New SIG.Areas.Planilla.Models.cl_mecanismo_pago
        Dim objTrasnfeActual As New SIG.Areas.Planilla.Models.cl_transferencia_actual
        Dim objAprobacion As New SIG.Areas.Planilla.Models.cl_aprobacion_esquema
        Dim objCorresTrans As New SIG.Areas.Planilla.Models.cl_corresponsabilidad_transferencia
        Dim objCorresAper As New SIG.Areas.Planilla.Models.cl_corresponsabilidad_apercibimiento
        Dim objTransAcumu As New SIG.Areas.Planilla.Models.cl_transferencia_acumulada
        Dim objOtraVerifi As New SIG.Areas.Planilla.Models.cl_otra_verificacion

#Region "Pago"
        Function ViewPago() As ActionResult
            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(3) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View()
                    Else
                        Return Redirect("/Planilla/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        <ValidateInput(False)> _
        Public Function GridViewPartialPagos() As ActionResult

            Return PartialView("Pago/_GridViewPartialPago", objPago.fnc_obtener_pagos())
        End Function

        <HttpPost, ValidateInput(False)> _
        Public Function GridViewPartialNewPago(ByVal intAñoPago As Integer, ByVal strNombrePago As String, ByVal strDescripcionPago As String) As Integer
            If login.Fnc_loggeado() IsNot Nothing Then
                objPago.pag_anyo = intAñoPago
                objPago.pag_nombre = strNombrePago
                objPago.pag_descripcion = strDescripcionPago
                objPago.pag_usuario_accion = HttpContext.Session("usuario")

                If objPago.fnc_verificar_nombre_repetido() = 0 Then 'Verificar que el nombre del pago no este repetido.
                    Return objPago.fnc_ingresar_pago()
                Else
                    Return 3 'El nombre del pago ya lo posee otro registro.
                End If
            Else
                Return 2 'El usuario no está logiado.
            End If
        End Function

        <HttpPost, ValidateInput(False)> _
        Public Function GridViewPartialDeletePago(ByVal pag_codigo As Integer) As Integer
            Try
                If login.Fnc_loggeado() IsNot Nothing Then
                    objPago.pag_codigo = pag_codigo
                    objPago.pag_usuario_accion = HttpContext.Session("usuario")

                    If objPago.fnc_pago_pertence_esquemas = 0 Then
                        If objPago.fnc_pagos_menores_mismo_año = 0 Then
                            Return objPago.fnc_borrar_pago()
                        Else
                            Return 3
                        End If
                    Else
                        Return 5
                    End If
                Else
                    Return 2
                End If
            Catch e As Exception
                Return 0
            End Try
        End Function

        <HttpPost, ValidateInput(False)> _
        Public Function GridViewPartialUpdatePago(ByVal objPago As SIG.Areas.Planilla.Models.cl_pago) As ActionResult
            Try
                objPago.pag_nombre = Replace(objPago.pag_nombre, Chr(34), "")
                objPago.pag_descripcion = Replace(objPago.pag_descripcion, Chr(34), "")

                If objPago.fnc_verificar_nombre_repetido = 0 Then
                    If ModelState.IsValid Then
                        objPago.pag_usuario_accion = HttpContext.Session("usuario")
                        objPago.fnc_actualizar_pago()
                    Else
                        ViewData("EditError") = "*Por favor, corrija todos los errores."
                    End If
                Else
                    ViewData("EditError") = "*Imposible actualizar el pago, existe otro registro con el mismo nombre."
                End If
                
            Catch e As Exception
                ViewData("EditError") = e.Message
            End Try
            Return PartialView("Pago/_GridViewPartialPago", objPago.fnc_obtener_pagos())
        End Function
#End Region

#Region "Esquema"
        Function ViewEsquema() As ActionResult
            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(3) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View()
                    Else
                        Return Redirect("/Planilla/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        <ValidateInput(False)> _
        Public Function GridViewPartialEsquema() As ActionResult

            Return PartialView("Esquema/_GridViewPartialEsquema", objEsquema.fnc_obtener_esquemas)
        End Function

        <ValidateInput(False)> _
        Public Function GridViewPartialConfiguracionMontos() As ActionResult

            Return PartialView("Esquema/_GridViewPartialConfiguracionMontos", objEsquema.fnc_obtener_montos)
        End Function

        <HttpPost()> _
        Function fnc_obtener_info_esquema(ByVal intCodEsquema As Integer) As JsonResult
            Return Json(objEsquema.fnc_obtener_un_esquema(intCodEsquema), JsonRequestBehavior.DenyGet)
        End Function

        <HttpPost()> _
        Function fnc_obtener_cantidad_meses(ByVal intCodInterTiemp As Integer) As Integer
            Return objEsquema.fnc_obtener_cant_meses(intCodInterTiemp)
        End Function

        <HttpPost()> _
        Function fnc_borrar_esquema(ByVal intCodEsquema As Integer) As Integer
            If login.Fnc_loggeado() IsNot Nothing Then
                objEsquema.esq_codigo = intCodEsquema
                objEsquema.esq_usuario_accion = HttpContext.Session("usuario")
                Return objEsquema.fnc_operacion_esquema(3)
            Else
                Return 4
            End If
        End Function

        <HttpPost()> _
        Function fnc_actualizar_esquema(ByVal intCodEsquema As Integer, ByVal intCodPago As Integer, ByVal strNombreEsq As String, _
                                        ByVal strCenso As String, ByVal strFechaIni As String, ByVal strFechaFin As String, _
                                        ByVal intMeses As Integer, ByVal intTipInterva As Integer, ByVal strTipoPago As String, _
                                        ByVal strFechaElegibi As String, ByVal intAño As Integer, ByVal intCantBono As Integer, _
                                        ByVal strDetMeses As String, ByVal intTipEsquema As Integer, ByVal strObser As String, _
                                        ByVal intNumEsque As Integer, ByVal intCodCofMonto As Integer) As Integer
            If login.Fnc_loggeado() IsNot Nothing Then
                objEsquema.esq_codigo = intCodEsquema
                objEsquema.pag_codigo = intCodPago
                objEsquema.nom_esquema = strNombreEsq
                objEsquema.esq_censo = strCenso
                objEsquema.esq_fecha_ini = strFechaIni
                objEsquema.esq_fecha_fin = strFechaFin
                objEsquema.esq_meses = intMeses
                objEsquema.esq_tipo_intervalo = intTipInterva
                objEsquema.esq_tipo_pago = strTipoPago
                objEsquema.esq_fecha_elegibilidad = strFechaElegibi
                objEsquema.esq_anyo = intAño
                objEsquema.esq_cant_bonos_act = intCantBono
                objEsquema.esq_detalle_meses = strDetMeses
                objEsquema.esq_tipo_esquema = intTipEsquema
                objEsquema.esq_observaciones = strObser
                objEsquema.esq_numero = intNumEsque
                objEsquema.esq_usuario_accion = HttpContext.Session("usuario")
                objEsquema.esq_cof_monto = intCodCofMonto
                Return objEsquema.fnc_operacion_esquema(2)
            Else
                Return 4
            End If
        End Function

        <HttpPost()> _
        Function fnc_ingresar_esquema(ByVal intCodPago As Integer, ByVal strNombreEsq As String, _
                                    ByVal strCenso As String, ByVal strFechaIni As String, ByVal strFechaFin As String, _
                                    ByVal intMeses As Integer, ByVal intTipInterva As Integer, ByVal strTipoPago As String, _
                                    ByVal strFechaElegibi As String, ByVal intAño As Integer, ByVal intCantBono As Integer, _
                                    ByVal strDetMeses As String, ByVal intTipEsquema As Integer, ByVal strObser As String, _
                                    ByVal intCodCofMonto As Integer) As Integer
            If login.Fnc_loggeado() IsNot Nothing Then
                objEsquema.pag_codigo = intCodPago
                objEsquema.nom_esquema = strNombreEsq
                objEsquema.esq_censo = strCenso
                objEsquema.esq_fecha_ini = strFechaIni
                objEsquema.esq_fecha_fin = strFechaFin
                objEsquema.esq_meses = intMeses
                objEsquema.esq_tipo_intervalo = intTipInterva
                objEsquema.esq_tipo_pago = strTipoPago
                objEsquema.esq_fecha_elegibilidad = strFechaElegibi
                objEsquema.esq_anyo = intAño
                objEsquema.esq_cant_bonos_act = intCantBono
                objEsquema.esq_detalle_meses = strDetMeses
                objEsquema.esq_tipo_esquema = intTipEsquema
                objEsquema.esq_observaciones = strObser
                objEsquema.esq_usuario_accion = HttpContext.Session("usuario")
                objEsquema.esq_cof_monto = intCodCofMonto
                Return objEsquema.fnc_operacion_esquema(1)
            Else
                Return 4
            End If
        End Function

#End Region

#Region "Cobertura de planilla"
        Function ViewCoberturaPlanilla() As ActionResult
            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(3) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View()
                    Else
                        Return Redirect("/Planilla/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Function GridViewPlanillas() As ActionResult

            Return PartialView("Cobertura_planilla/_GridViewPartialPlanillas", objCobertura.fnc_obtener_esquemas)
        End Function

        Function GridViewCargas() As ActionResult
            Return PartialView("Cobertura_planilla/_GridViewPartialCargas", objCobertura.fnc_obtener_cargas)
        End Function

        Function fnc_agregar_modificar_cobertura_esquema(ByVal strCodEsquema As String, _
                                                         ByVal strCodCargas As String, _
                                                         ByVal strHaSalido As String, _
                                                         ByVal strNoHaSalido As String, _
                                                         ByVal strSigno As String, _
                                                         ByVal strDepar As String, _
                                                         ByVal strMun As String, _
                                                         ByVal strAld As String, _
                                                         ByVal strCase As String, _
                                                         ByVal strDescripcion As String, _
                                                         ByVal bolEnlazadoBD As Boolean) As Integer
            Try
                If login.Fnc_loggeado() IsNot Nothing Then
                    Return objCobertura.fnc_guardar_localizacion(Split(strCodEsquema, ","), _
                                                             strCodCargas, _
                                                             strHaSalido, _
                                                             strNoHaSalido, _
                                                             strSigno, _
                                                             Split(strDepar, ","), _
                                                             Split(strMun, ","), _
                                                             Split(strAld, ","), _
                                                             Split(strCase, ","), _
                                                             strDescripcion, _
                                                             HttpContext.Session("usuario"), _
                                                             bolEnlazadoBD)
                Else
                    Return 4 'Usuario no logiado.
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Function
#End Region

#Region "Establecimientos de fondos"
        Function ViewEstablecimientoFondo() As ActionResult
            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(3) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View()
                    Else
                        Return Redirect("/Planilla/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Function FondosComboBox() As ActionResult
            Return PartialView("Establecimiento_fondos/_ComBoxViewPartialFondo", objEstableFondo.fnc_obtener_fondos)
        End Function

        Function fnc_agregar_modificar_estable_fondo(ByVal strCodEsquema As String, _
                                                     ByVal intCodFondo As Integer, _
                                                     ByVal strDescripcion As String, _
                                                    ByVal strSigno As String, _
                                                    ByVal strDepar As String, _
                                                    ByVal strMun As String, _
                                                    ByVal strAld As String, _
                                                    ByVal strCase As String, _
                                                    ByVal bolEnlazadoBD As Boolean) As Integer
            Try
                If login.Fnc_loggeado() IsNot Nothing Then
                    Return objEstableFondo.fnc_guardar_localizacion(Split(strCodEsquema, ","), _
                                                                    intCodFondo, _
                                                                    strSigno, _
                                                                    strDescripcion, _
                                                                    Split(strDepar, ","), _
                                                                    Split(strMun, ","), _
                                                                    Split(strAld, ","), _
                                                                    Split(strCase, ","), _
                                                                    HttpContext.Session("usuario"), _
                                                                    bolEnlazadoBD)
                Else
                    Return 4 'Usuario no logiado.
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Function

#End Region

#Region "Mecanismo de pago"
        Function ViewMecanismoPago() As ActionResult
            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(3) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View()
                    Else
                        Return Redirect("/Planilla/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Function AspxComboBoxPagador() As ActionResult
            Return PartialView("Mecanismo_pago/_ComBoxViewPartialPagador", objMecaniPago.fnc_obtener_pagadores)
        End Function

        Function fnc_agregar_modificar_mecani_pago(ByVal strCodEsquema As String, _
                                                    ByVal intCodPagador As Integer, _
                                                    ByVal strDescripcion As String, _
                                                    ByVal strSigno As String, _
                                                    ByVal strDepar As String, _
                                                    ByVal strMun As String, _
                                                    ByVal strAld As String, _
                                                    ByVal strCase As String, _
                                                    ByVal bolEnlazadoBD As Boolean) As Integer
            Try
                If login.Fnc_loggeado() IsNot Nothing Then
                    Return objMecaniPago.fnc_guardar_localizacion(Split(strCodEsquema, ","), _
                                                                intCodPagador, _
                                                                strSigno, _
                                                                strDescripcion, _
                                                                Split(strDepar, ","), _
                                                                Split(strMun, ","), _
                                                                Split(strAld, ","), _
                                                                Split(strCase, ","), _
                                                                HttpContext.Session("usuario"), _
                                                                bolEnlazadoBD)
                Else
                    Return 4 'Usuario no logiado.
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Function
#End Region

#Region "Transferencias Actuales"
        Function ViewTransferenciaActual() As ActionResult
            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(3) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View()
                    Else
                        Return Redirect("/Planilla/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Function GridViewDetalleTransfeActual(ByVal strCodEsquemas As String) As ActionResult
            Return PartialView("Transferencias_actuales\_GridViewDetalleTransfeActual", objTrasnfeActual.fnc_obtener_transferencias_actuales(strCodEsquemas))
        End Function

        Function fnc_generar_bonos(ByVal strCodEsquemas As String) As Integer
            Try
                If login.Fnc_loggeado() IsNot Nothing Then
                    Return objTrasnfeActual.fnc_generar_bono(Split(strCodEsquemas, ","), HttpContext.Session("usuario"))
                Else
                    Return 4 'Usuario no logiado.
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Function fnc_borrar_bonos(ByVal intCodBono As Integer) As Integer
            Try
                If login.Fnc_loggeado() IsNot Nothing Then
                    Return objTrasnfeActual.fnc_borrar_bonos(intCodBono, HttpContext.Session("usuario"))
                Else
                    Return 4 'Usuario no logiado.
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Function
#End Region

#Region "Corresponsabilidad de transferencia"
        Function ViewCorresponsabilidadTransferencia() As ActionResult
            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(3) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View()
                    Else
                        Return Redirect("/Planilla/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Function fnc_agregar_corresp_transferencia(ByVal strCodTarnsferencias As String, ByVal intCodCorresp As Integer, ByVal intCodComponente As Integer) As Integer
            Try
                If login.Fnc_loggeado() IsNot Nothing Then
                    Return objCorresTrans.fnc_agregar_corresp_transferencia(HttpContext.Session("usuario"), Split(strCodTarnsferencias, ","), intCodCorresp, intCodComponente)
                Else
                    Return 4 'Usuario no logiado.
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Function fnc_reordenar_corresp_transferencia(ByVal strCodBonos As String, ByVal intCodTransferencia As Integer) As Integer
            Try
                If login.Fnc_loggeado() IsNot Nothing Then
                    Return objCorresTrans.fnc_reordenar_corresp_transferencia(Split(strCodBonos, ","), HttpContext.Session("usuario"), intCodTransferencia)
                Else
                    Return 4 'Usuario no logiado.
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Function fnc_borrar_corresp_transferencia(ByVal intCodBono As Integer, ByVal intCodTransferencia As Integer) As Integer
            Try
                If login.Fnc_loggeado() IsNot Nothing Then
                    Return objCorresTrans.fnc_borrar_corresp_transferencia(HttpContext.Session("usuario"), intCodBono, intCodTransferencia)
                Else
                    Return 4 'Usuario no logiado.
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Function
#End Region

#Region "Corresponsabilidad de apercibimiento"
        Function ViewCorresponsabilidadApercibimiento() As ActionResult
            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(3) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View()
                    Else
                        Return Redirect("/Planilla/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Function fnc_agregar_corresp_aperc(ByVal strCodTarnsferencias As String, ByVal intCodCorresp As Integer, ByVal intCodComponente As Integer) As Integer
            Try
                If login.Fnc_loggeado() IsNot Nothing Then
                        Return objCorresAper.fnc_agregar_corresp_aperc(HttpContext.Session("usuario"), Split(strCodTarnsferencias, ","), intCodCorresp, intCodComponente)
                    Else
                        Return 4 'Usuario no logiado.
                    End If
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Function fnc_borrar_corresp_aperc(ByVal intCodBono As Integer, ByVal intCodTransferencia As Integer) As Integer
            Try
                If login.Fnc_loggeado() IsNot Nothing Then
                    Return objCorresAper.fnc_borrar_corresp_aperc(HttpContext.Session("usuario"), intCodBono, intCodTransferencia)
                Else
                    Return 4 'Usuario no logiado.
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Function fnc_reordenar_corresp_aperc(ByVal strCodBonos As String, ByVal intCodTransferencia As Integer) As Integer
            Try
                If login.Fnc_loggeado() IsNot Nothing Then
                    Return objCorresAper.fnc_reordenar_corresp_aperc(Split(strCodBonos, ","), HttpContext.Session("usuario"), intCodTransferencia)
                Else
                    Return 4 'Usuario no logiado.
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Function

#End Region

#Region "Transferencia Acumulada"
        Function ViewTransferenciaAcumulada() As ActionResult
            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(3) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View()
                    Else
                        Return Redirect("/Planilla/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Function GridViewPagoAcumulado(ByVal intCodPago As Integer) As ActionResult
            ViewData("intCodPago") = intCodPago

            Return PartialView("Transferencia_acumulada\_GridViewPagoAcumulado", objTransAcumu.fnc_obtener_pago_para_acumulado(intCodPago))
        End Function

        Function GridViewEsquemaAcumulado(ByVal intCodPago As Integer) As ActionResult
            ViewData("intCodPago") = intCodPago

            Return PartialView("Transferencia_acumulada\_GridViewEsquemaAcumulado", objTransAcumu.fnc_obtener_detalle_esquema_para_acumulado(intCodPago))
        End Function

        Function GridViewDetalleEsquemaAcumulado(ByVal strCodEsquemaAdd As String, intCodEsqBuscar As Integer) As ActionResult
            Dim arrCodigosEsquemas() As String = Split(strCodEsquemaAdd, ",")
            Dim arrDatos As Array
            Dim objDataTable As DataTable = objTransAcumu.fnc_obtener_detalle_esquemas_acumulados(intCodEsqBuscar)

            Dim intCodTemporal As Integer = 0
            ViewData("strCodEsquemaAdd") = strCodEsquemaAdd
            ViewData("intCodEsqBuscar") = intCodEsqBuscar

            For i = 0 To arrCodigosEsquemas.Length - 1
                If arrCodigosEsquemas(i) <> "0" Then
                    Dim objNuevaRow As DataRow = objDataTable.NewRow()
                    intCodTemporal -= -1
                    arrDatos = objTransAcumu.fnc_obtener_detalle_esquema(CInt(arrCodigosEsquemas(i)))
                    objNuevaRow("bonacnc_codigo") = intCodTemporal
                    objNuevaRow("pag_codigo") = arrDatos(0)
                    objNuevaRow("pag_numero") = arrDatos(1)
                    objNuevaRow("pag_anyo") = arrDatos(2)
                    objNuevaRow("pag_nombre") = arrDatos(3)
                    objNuevaRow("pag_descripcion") = arrDatos(4)
                    objNuevaRow("esq_codigo") = arrDatos(5)
                    objNuevaRow("esq_tipo_pago") = arrDatos(6)
                    objNuevaRow("nombre_esquema") = arrDatos(7)
                    objDataTable.Rows.Add(objNuevaRow)
                End If
            Next

            Return PartialView("Transferencia_acumulada\_GridViewDetalleEsquemaAcumulado", objDataTable)
        End Function

        Function fnc_validar_ejecu_esquema(ByVal intCodEsquema As Integer) As Boolean
            Return objTransAcumu.fnc_esquema_fue_ejecutado(intCodEsquema)
        End Function

        Function fnc_validar_exist_esq_acumu(ByVal intCodEsqPadre As Integer, ByVal intCodEsqHaAcumu As Integer) As Boolean
            Return objTransAcumu.fnc_existe_esquema_acumulado(intCodEsqPadre, intCodEsqHaAcumu)
        End Function

        Function fnc_guardar_esquemas_acumulados(ByVal strCodEsquemasAcumu As String, ByVal intCodEsquema As Integer) As Integer
            Try
                If login.Fnc_loggeado() IsNot Nothing Then
                    If objTransAcumu.fnc_esquema_principal_ejecutado(intCodEsquema) = False Then

                        Dim arrCodigosEsquemas() As String = Split(strCodEsquemasAcumu, ",")
                        Dim intRespuesta As Integer = 1

                        For i = 1 To arrCodigosEsquemas.Length - 1
                            If intRespuesta = 1 Then
                                intRespuesta = objTransAcumu.fnc_nuevo_bono_acumu(HttpContext.Session("usuario"), intCodEsquema, arrCodigosEsquemas(i))
                            Else
                                Exit For
                            End If
                        Next

                        Return intRespuesta
                    Else
                        Return 2 'El esquema ya fue ejecutado.
                    End If
                Else
                    Return 4 'Usuario no logiado.
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Function fnc_borrar_esquema_acumulado(ByVal intCodBonoAcumu As Integer) As Integer
            Try
                If login.Fnc_loggeado() IsNot Nothing Then
                    Return objTransAcumu.fnc_borrar_bono_acumu(HttpContext.Session("usuario"), intCodBonoAcumu)
                Else
                    Return 4 'Usuario no logiado.
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Function
#End Region

#Region "Otras verificaciones"
        Function ViewOtraVerificacion() As ActionResult
            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(3) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View()
                    Else
                        Return Redirect("/Planilla/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Function GridViewOtrasVerificaciones(ByVal strCodEsquemas As String) As ActionResult
            ViewData("strCodEsquemas") = strCodEsquemas
            Dim strCodigosEsq() As String = Split(strCodEsquemas, ",")
            Dim objDataTable As DataTable
            Dim arrListCodVerificacion As New ArrayList

            If strCodigosEsq(0) <> 0 Then
                objDataTable = objOtraVerifi.fnc_obtener_verificaciones_esquemas(strCodigosEsq(0))

                If objDataTable.Rows.Count > 0 Then
                    For Each objFila In objDataTable.Rows
                        arrListCodVerificacion.Add(objFila("ver_codigo"))
                    Next
                End If
            End If

            ViewData("arrListCodVerificacion") = arrListCodVerificacion

            Return PartialView("Otras_verificaciones\_GridViewPartialDetalleOtrasVerifica", objOtraVerifi.fnc_obetener_verificaciones())
        End Function

        Function fnc_verificar_enlace(ByVal strCodEsquemas As String) As Integer
            Dim arrStrCodigosEsquemas() As String = Split(strCodEsquemas, ",")
            Dim boolEstanEnlazdos As Boolean = True
            Dim objDataTable As DataTable
            Dim boolPoseenDatos() As Boolean = {Nothing, Nothing}
            Dim arrListVerificaX As New ArrayList
            Dim arrListVerificaY As New ArrayList
            Dim intContador As Integer = 0


            If arrStrCodigosEsquemas.Length > 1 Then 'Cuando los esquemas seleccionados son mas de 1.
                For i = 0 To arrStrCodigosEsquemas.Length - 1
                    If boolEstanEnlazdos = True Then
                        intContador += 1
                        objDataTable = objOtraVerifi.fnc_obtener_verificaciones_esquemas(arrStrCodigosEsquemas(i))

                        If objDataTable.Rows.Count > 0 Then
                            arrListVerificaY = New ArrayList

                            For Each objFila In objDataTable.Rows
                                If intContador = 1 Then
                                    arrListVerificaX.Add(objFila("ver_codigo"))
                                    boolPoseenDatos(0) = True
                                Else
                                    arrListVerificaY.Add(objFila("ver_codigo"))
                                    boolPoseenDatos(1) = True
                                End If
                            Next
                        Else
                            If intContador = 1 Then
                                boolPoseenDatos(0) = False
                            Else
                                boolPoseenDatos(1) = False
                            End If
                        End If

                        If intContador > 1 Then 'Cuando ya se ha evaluado por lo meno 2 esqeumas.
                            If boolPoseenDatos(0) = True And boolPoseenDatos(0) = boolPoseenDatos(1) Then
                                Dim intValorX, intValorY As Integer
                                Dim intContadorIgual As Integer = 0 'Indica la cantidad de coincidencias que tuvo el segundo registro con el primer registro evaluado

                                For Each intValorY In arrListVerificaY
                                    For Each intValorX In arrListVerificaX
                                        If intValorY = intValorX Then
                                            intContadorIgual += 1
                                        End If
                                    Next
                                Next

                                If intContadorIgual <> arrListVerificaY.Count Then
                                    boolEstanEnlazdos = False
                                End If

                            ElseIf boolPoseenDatos(0) = False And boolPoseenDatos(0) <> boolPoseenDatos(1) Then
                                boolEstanEnlazdos = False
                            End If
                        End If
                    Else
                        Exit For
                    End If
                Next
            End If

            If boolEstanEnlazdos = True Then
                Return 1
            Else
                Return 0
            End If
        End Function

        Function fnc_guardar_verificaciones(ByVal strCodEsquemas As String, ByVal strCodVerificacion As String) As Integer
            Try
                If login.Fnc_loggeado() IsNot Nothing Then

                    Dim objDataTable As DataTable
                    Dim strCodigosEsquemas() As String = Split(strCodEsquemas, ",")
                    Dim strCodigosVerificacion() As String = Split(strCodVerificacion, ",")
                    Dim arrListCodVerifi As New ArrayList 'Trae los código ya existentes de la base de datos.
                    Dim intCodNumero As Integer
                    Dim arrListNuevosCodigos As New ArrayList 'Los codigos a ingresar.
                    Dim boolYaExiste As Boolean = False
                    Dim arrListCodBorrar As New ArrayList 'Se almacenan los códigos a borrar.
                    Dim intCodUsuario As Integer = HttpContext.Session("usuario")
                    Dim boolYaEjecutado As Integer = False
                    Dim boolUnoContData As Boolean = False

                    For i = 0 To strCodigosEsquemas.Length - 1
                        If boolYaEjecutado = False Then
                            boolYaEjecutado = objOtraVerifi.fnc_esquema_ya_ejecutado(strCodigosEsquemas(i))
                        Else
                            Exit For
                        End If
                    Next

                    If boolYaEjecutado = False Then 'Validar que todos los esquemas no alla sido ejecutados.
                        If strCodigosVerificacion(0) <> "0" Then
                            objDataTable = objOtraVerifi.fnc_obtener_verificaciones_esquemas(strCodigosEsquemas(0))

                            If objDataTable.Rows.Count > 0 Then
                                For Each objFila In objDataTable.Rows
                                    arrListCodVerifi.Add(objFila("ver_codigo"))
                                Next
                                'Invertigar los nuevos códigos.
                                For i = 0 To strCodigosVerificacion.Length - 1
                                    For Each intCodNumero In arrListCodVerifi
                                        If strCodigosVerificacion(i) = intCodNumero Then
                                            boolYaExiste = True
                                            Exit For
                                        End If
                                    Next

                                    If boolYaExiste = False Then
                                        arrListNuevosCodigos.Add(strCodigosVerificacion(i))
                                    End If

                                    boolYaExiste = False
                                Next

                                boolYaExiste = False

                                'Invertigar los codigos a borrar.
                                For Each intCodNumero In arrListCodVerifi
                                    For i = 0 To strCodigosVerificacion.Length - 1
                                        If strCodigosVerificacion(i) = intCodNumero Then
                                            boolYaExiste = True
                                            Exit For
                                        End If
                                    Next
                                    If boolYaExiste = False Then
                                        arrListCodBorrar.Add(intCodNumero)
                                    Else
                                        boolYaExiste = False
                                    End If
                                Next

                                If arrListNuevosCodigos.Count > 0 Then 'Agregar los nuevos códigos.
                                    For i = 0 To strCodigosEsquemas.Length - 1
                                        For Each intCodNumero In arrListNuevosCodigos
                                            objOtraVerifi.fnc_crear_otra_verificacion(intCodUsuario, strCodigosEsquemas(i), intCodNumero)
                                        Next
                                    Next
                                End If

                                If arrListCodBorrar.Count > 0 Then 'Borrar los código inexistentes
                                    Dim intCodPrimaryKey As Integer
                                    For i = 0 To strCodigosEsquemas.Length - 1
                                        For Each intCodNumero In arrListCodBorrar
                                            intCodPrimaryKey = objOtraVerifi.fnc_obtener_primary_key_verifiacion_esquema(strCodigosEsquemas(i), intCodNumero)
                                            objOtraVerifi.fnc_borrar_otra_verificacion(intCodUsuario, intCodPrimaryKey)
                                        Next
                                    Next
                                End If
                            Else 'Cuando ninguno de los esquemas tenia configurado verificaciones.
                                For i = 0 To strCodigosEsquemas.Length - 1
                                    For j = 0 To strCodigosVerificacion.Length - 1
                                        objOtraVerifi.fnc_crear_otra_verificacion(intCodUsuario, strCodigosEsquemas(i), strCodigosVerificacion(j))
                                    Next
                                Next
                            End If
                            Return 1
                        Else
                            Dim intCodPrimaryKey As Integer
                            For i = 0 To strCodigosEsquemas.Length - 1
                                objDataTable = objOtraVerifi.fnc_obtener_verificaciones_esquemas(strCodigosEsquemas(i))
                                If objDataTable.Rows.Count > 0 Then
                                    boolUnoContData = True
                                    For Each objFila In objDataTable.Rows
                                        intCodPrimaryKey = objOtraVerifi.fnc_obtener_primary_key_verifiacion_esquema(strCodigosEsquemas(i), objFila("ver_codigo"))
                                        objOtraVerifi.fnc_borrar_otra_verificacion(intCodUsuario, intCodPrimaryKey)
                                    Next
                                End If
                            Next
                            If boolUnoContData = True Then
                                Return 6
                            Else
                                Return 5
                            End If
                        End If
                    Else
                        Return 3
                    End If
                Else
                    Return 4 'Usuario no logiado.
                End If
            Catch ex As Exception
                Return 0
            End Try


        End Function

#End Region

        Function ViewTransferenciaAcumuladaRecuperacion() As ActionResult
            Return View()
        End Function

#Region "Aprovación de Esquema"

        Function ViewAprobacionEsquema() As ActionResult
            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(3) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View()
                    Else
                        Return Redirect("/Planilla/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Function pv_detalle_esquema(ByVal pago As String, ByVal esquema As String) As ActionResult

            ViewData("pago") = objAprobacion.fnc_obtener_nombre_pago(pago)
            ViewData("esquema") = objAprobacion.fnc_obtener_nombre_esquema(esquema)
            ViewData("monto") = objAprobacion.fnc_obtener_montos(esquema)
            ViewData("coberturaPlanilla") = objAprobacion.fnc_obtener_cobertura(esquema)
            ViewData("establecimientoFondos") = objAprobacion.fnc_obtener_fondos(esquema)
            ViewData("mecanismoPago") = objAprobacion.fnc_obtener_mecanismos_pago(esquema)
            ViewData("transferenciasActuales") = objAprobacion.fnc_obtener_transferencias_actuales(esquema)
            ViewData("corrTransferencia") = objAprobacion.fnc_obtener_corr_transferencia(esquema)
            ViewData("corrApercibimiento") = objAprobacion.fnc_obtener_corr_apercibimiento(esquema)
            ViewData("verificacion") = objAprobacion.fnc_obtener_verificacion(esquema)

            If objAprobacion.fnc_obtener_estado_aprobacion(esquema) = True Then
                ViewData("estado") = "Aprobado"
            Else
                ViewData("estado") = "Falta Aprobación"
            End If

            Return PartialView("Aprobacion/pv_detalle_esquema")
        End Function

        Function aprobar_esquema(ByVal esquema As String)
            If login.Fnc_loggeado() IsNot Nothing Then
                If objAprobacion.fnc_posee_permiso(HttpContext.Session("usuario")) = True Then
                    If objAprobacion.fnc_obtener_estado_aprobacion(esquema) = True Then
                        Return 5
                    Else
                        Return objAprobacion.fnc_aprobar_esquema(esquema)
                    End If
                Else
                    Return 4
                End If
            Else
                Return 3
            End If
        End Function

#End Region

    End Class
End Namespace