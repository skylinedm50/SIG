Namespace SIG.Areas.Planilla.Controllers
    Public Class SharedController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Planilla/Shared
        Dim share As SIG.Areas.Planilla.Models.cl_shared = New SIG.Areas.Planilla.Models.cl_shared
        Dim objConfiguracionDatos As New SIG.Areas.Planilla.Models.cl_manejo_datos
        Dim arrListDataTable As New List(Of Dictionary(Of String, Object))
        Dim objCobertura As New SIG.Areas.Planilla.Models.cl_cobertura_planilla
        Dim objEstableFondo As New SIG.Areas.Planilla.Models.cl_establecimiento_fondo
        Dim objMecaniPago As New SIG.Areas.Planilla.Models.cl_mecanismo_pago
        Dim objCorresTrans As New SIG.Areas.Planilla.Models.cl_corresponsabilidad_transferencia
        Dim objCorresAper As New SIG.Areas.Planilla.Models.cl_corresponsabilidad_apercibimiento
        Private login As New Global.SIG.Cl_Login

        Function pv_cbxPagos() As ActionResult
            Return PartialView(share.fnc_obtener_pagos())
        End Function

        Function pv_cbxEsquemas() As ActionResult
            Dim pago As String = Request.Params("pago")
            Return PartialView(share.fnc_obtener_esquemas(pago))
        End Function

        Function pv_gdvEsquemas() As ActionResult
            Dim pago As String = Request.Params("pago")
            Return PartialView(share.fnc_obtener_esquemas_con_detalle(pago))
        End Function

        Function pv_Spinner() As ActionResult
            Return PartialView()
        End Function

        Function pv_mensajes() As ActionResult
            Return PartialView()
        End Function


        Function GridViewPartialDepartamentoSIG(ByVal strCodDepaSelect As String, _
                                                ByVal strCodDepaConHijos As String, _
                                                ByVal strCodMuniSelectByDepa As String, _
                                                ByVal strCodMuniConHijos As String, _
                                                ByVal intSelectAllOrNot As Integer) As ActionResult
            ViewData("strCodDepaSelect") = Split(strCodDepaSelect, ",")
            ViewData("strCodDepaConHijos") = Split(strCodDepaConHijos, ",")
            ViewData("strCodMuniSelectByDepa") = strCodMuniSelectByDepa
            ViewData("strCodMuniConHijos") = strCodMuniConHijos
            ViewData("intSelectAllOrNot") = intSelectAllOrNot

            Return PartialView("UbicacionSIG/_GridViewPartialDepartamento", share.fnc_obtener_departamento_sig())
        End Function

        Function GridViewPartialMunicipioSIG(ByVal strCodDepa As String, _
                                             ByVal bolCheckedDepa As Boolean, _
                                             ByVal strCodMuniSelect As String, _
                                             ByVal strCodMuniConHijos As String, _
                                             ByVal strCodAldSelect As String, _
                                             ByVal strCodAldConHijos As String, _
                                             ByVal strCodCaseSelectByAld As String
                                             ) As ActionResult
            ViewData("strCodDepa") = strCodDepa
            ViewData("bolCheckedDepa") = bolCheckedDepa
            ViewData("strCodMuniSelect") = Split(strCodMuniSelect, ",")
            ViewData("strCodMuniConHijos") = Split(strCodMuniConHijos, ",")

            ViewData("strCodAldSelect") = strCodAldSelect
            ViewData("strCodAldConHijos") = strCodAldConHijos
            ViewData("strCodCaseSelectByAld") = strCodCaseSelectByAld

            Return PartialView("UbicacionSIG/_GridViewPartialMunicipio", share.fnc_obtener_municipio_sig(strCodDepa))
        End Function

        Function GridViewPartialAldeaSIG(ByVal strCodMuni As String, _
                                         ByVal bolCheckedMuni As Boolean, _
                                         ByVal strCodAldSelect As String, _
                                         ByVal strCodAldeaConHijos As String, _
                                         ByVal strCodCaseSelectByAld As String
                                         ) As ActionResult
            ViewData("strCodMuni") = strCodMuni
            ViewData("bolCheckedMuni") = bolCheckedMuni
            ViewData("strCodAldSelect") = Split(strCodAldSelect, ",")
            ViewData("strCodAldeaConHijos") = Split(strCodAldeaConHijos, ",")
            ViewData("strCodCaseSelectByAld") = strCodCaseSelectByAld

            Return PartialView("UbicacionSIG/_GridViewPartialAldea", share.fnc_obtener_aldea_sig(strCodMuni))
        End Function

        Function GridViewPartialCaserioSIG(ByVal strCodAld As String, ByVal bolCheckedAld As Boolean, ByVal strCodCaseSelect As String) As ActionResult
            ViewData("strCodAld") = strCodAld
            ViewData("bolCheckedAld") = bolCheckedAld
            ViewData("strCodCaseSelect") = Split(strCodCaseSelect, ",")

            Return PartialView("UbicacionSIG/_GridViewPartialCaserio", share.fnc_obtener_caserio_sig(strCodAld))
        End Function


        Function GridViewEsquemasPorPago() As ActionResult
            Dim pago As String = Request.Params("pago")
            Return PartialView("Esquemas_enlazados\_GridViewEsquemasPorPago", share.fnc_obtener_esquemas_con_detalle(pago))
        End Function

        Function fnc_obtener_esquemas_enlazados(ByVal intCodEsquema As Integer, ByVal intCodTipoEnlace As Integer) As String
            Dim objArrRespuesta As Array = {}

            Select Case intCodTipoEnlace
                Case 0
                    objArrRespuesta = objCobertura.fnc_obtener_esquemas_enlazados(intCodEsquema)
                Case 1
                    objArrRespuesta = objEstableFondo.fnc_obtener_esquemas_enlazados(intCodEsquema)
                Case 2
                    objArrRespuesta = objMecaniPago.fnc_obtener_esquemas_enlazados(intCodEsquema)
            End Select

            If objArrRespuesta(0) IsNot Nothing Then
                Return objArrRespuesta(0)
            Else
                Return "0"
            End If
        End Function

        Function fnc_desenlzar_esquema(ByVal intCodEsquema As Integer, ByVal intCodTipoEnlace As Integer) As Integer
            Try
                If login.Fnc_loggeado() IsNot Nothing Then
                    Select Case intCodTipoEnlace
                        Case 0
                            Return objCobertura.fnc_desenlazar_cobertura_esquema(intCodEsquema, HttpContext.Session("usuario"))
                        Case 1
                            Return objEstableFondo.fnc_desenlazar_estable_fondo_esquema(intCodEsquema, HttpContext.Session("usuario"))
                        Case 2
                            Return objMecaniPago.fnc_desenlazar_mecani_pago(intCodEsquema, HttpContext.Session("usuario"))
                    End Select
                Else
                    Return 4 'Usuario no logiado.
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Function GridViewDetalleEsquemaEnlazadoLocalizacion(ByVal intCodEsquema As Integer, ByVal intTipoConfig As Integer) As ActionResult
            ViewData("intCodEsquema ") = intCodEsquema
            ViewData("intTipoConfig") = intTipoConfig

            Select Case intTipoConfig
                Case 0
                    Return PartialView("Esquemas_enlazados/_GridViewDetalleEsquemaEnlaza", objCobertura.fnc_obtener_cobertura_planilla(intCodEsquema))
                    Exit Select
                Case 1
                    Return PartialView("Esquemas_enlazados/_GridViewDetalleEsquemaEnlaza", objEstableFondo.fnc_obtener_establecimiento_fondo(intCodEsquema))
                    Exit Select
                Case 2
                    Return PartialView("Esquemas_enlazados/_GridViewDetalleEsquemaEnlaza", objMecaniPago.fnc_obtener_mecanismo_pago_esquema(intCodEsquema))
                    Exit Select
                Case Else
                    Return PartialView("Esquemas_enlazados/_GridViewDetalleEsquemaEnlaza")
                    Exit Select
            End Select
        End Function

        Function fnc_borrar_localizacion_esquema_enlazado(ByVal intCodPrincipal As Integer, ByVal intTipoEnlace As Integer, ByVal strDescripcion As String) As Integer
            Try
                If login.Fnc_loggeado() IsNot Nothing Then
                    Select Case intTipoEnlace
                        Case 0
                            Return objCobertura.fnc_borrar_cobertura(intCodPrincipal, HttpContext.Session("usuario"))
                        Case 1
                            Return objEstableFondo.fnc_borrar_estable_fondo(intCodPrincipal, HttpContext.Session("usuario"), strDescripcion)
                        Case Else
                            Return objMecaniPago.fnc_borrar_mecani_pago(intCodPrincipal, HttpContext.Session("usuario"), strDescripcion)
                    End Select
                Else
                    Return 4 'Usuario no logiado.
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Function fnc_existe_registro_esquema(ByVal intCodEsquema As Integer, ByVal intCodTipoEnlace As Integer) As Integer
            Select Case intCodTipoEnlace
                Case 0
                    Return objCobertura.fnc_existe_cobertura_esquema(intCodEsquema)
                    Exit Select
                Case 1
                    Return objEstableFondo.fnc_existe_establecimiento_fondo_esquema(intCodEsquema)
                    Exit Select
                Case Else
                    Return objMecaniPago.fnc_existe_mecanismo_pago_esquema(intCodEsquema)
                    Exit Select
            End Select
        End Function

        Function FormLayoutDetalleEsquemaEnlazado(ByVal intTipoConfig As Integer, ByVal intCodEsquema As Integer) As ActionResult
            ViewData("intTipoConfig") = intTipoConfig
            ViewData("intCodEsquema") = intCodEsquema

            Return PartialView("Esquemas_enlazados/_FormLayoutDetalleEsquemaEnlazado")
        End Function

        Function FormLayoutControlEsquemasEnlazados(ByVal intTipoConfig As Integer) As ActionResult
            ViewData("intTipoConfig") = intTipoConfig

            Return PartialView("Esquemas_enlazados/_ControlesPartialViewEsquemasEnlazados")
        End Function


        Function GridViewTransferencias(ByVal intCodEsquema As Integer) As ActionResult
            ViewData("intCodEsquema") = intCodEsquema

            Return PartialView("Corresponsabilidad_de\_GridViewTransferencias", share.fnc_obtener_trasferencias(intCodEsquema))
        End Function

        Function AspxComboBoxCorresponsabilidad(ByVal intCodComponente As Integer) As ActionResult
            ViewData("intCodComponente") = intCodComponente

            Return PartialView("Corresponsabilidad_de\_ComBoxViewPartialCorresponsabilidad", share.fnc_obetener_corresponsabilidad(intCodComponente))
        End Function

        Function GridViewOrdenCorresTransfer(ByVal intTipoCorres As Integer, _
                                             ByVal intCodTransfer As Integer, _
                                             ByVal intFocusIndice As Integer, _
                                             ByVal intCodKeyGrid As Integer) As ActionResult
            Dim objDataTable As DataTable
            Dim objColumna As New DataColumn
            Dim intNumFila As Integer = 0
            objColumna.DataType = GetType(Integer)
            objColumna.ColumnName = "order"
            objColumna.ReadOnly = False

            ViewData("intTipoCorres") = intTipoCorres
            ViewData("intCodTransfer") = intCodTransfer
            ViewData("intFocusIndice") = intFocusIndice
            ViewData("intCodKeyGrid") = intCodKeyGrid


            If intTipoCorres = 0 Then
                objDataTable = objCorresTrans.fnc_obtener_corresp_transferencia(intCodTransfer)
            Else
                objDataTable = objCorresAper.fnc_obtener_corresp_apercibimiento(intCodTransfer)
            End If

            objDataTable.Columns.Add(objColumna)
            For Each objDataRow As DataRow In objDataTable.Rows
                intNumFila += 1

                If intNumFila = intFocusIndice Then
                    If CInt(objDataRow("bon_codigo_key")) = intCodKeyGrid Then
                        objDataRow("order") = intFocusIndice
                    Else
                        intNumFila += 1
                        objDataRow("order") = intNumFila
                    End If
                Else
                    If CInt(objDataRow("bon_codigo_key")) = intCodKeyGrid Then
                        intNumFila -= 1
                        objDataRow("order") = intFocusIndice
                    Else
                        objDataRow("order") = intNumFila
                    End If
                End If
            Next
            Return PartialView("Corresponsabilidad_de\_GridViewOrdenCorresTransfer", objDataTable)
        End Function
    End Class
End Namespace