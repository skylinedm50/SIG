Imports DevExpress.Web.Mvc

Namespace SIG.Areas.Corresponsabilidad.Controllers
    Public Class ConfiguracionController
        Inherits System.Web.Mvc.Controller
        Private objConexionDB As New SIG.Areas.Corresponsabilidad.Models.cl_conexion_db()
        Private objConfiguracionDatos As New SIG.Areas.Corresponsabilidad.Models.cl_manejo_datos
        Private strQuery As String
        Private objRegisDataSet As DataSet
        Private objTabla As DataTable
        Private arrErroresCorresponsabilidad() As String = {"Por favor, corrija todos los errores."}
        Private objDetalleCorresponsabilidad As New SIG.Areas.Corresponsabilidad.Models.cl_detalle_corresponsabilidad
        Private arrListDataTable As New List(Of Dictionary(Of String, Object))
        Private login As New Global.SIG.Cl_Login

#Region "Corresponsabilidad"
        Function Corresponsabilidad() As ActionResult
            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(2) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        strQuery = "SELECT " &
                                    "	cod_tip_cor, " &
                                    "	nom_tip_cor " &
                                    "FROM " &
                                    "	SIG_T.dbo.t_corr_tipo_corresponsabilidad"
                        objTabla = objConexionDB.fnc_crear_datatable(strQuery)
                        ViewBag.datosTipoCorrespon = objTabla

                        Return View()
                    Else
                        Return Redirect("/Corresponsabilidad/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Public Function GridViewPartialTipoCorresponsabilidad() As ActionResult
            Dim objTipoCorresponsabilidad As New SIG.Areas.Corresponsabilidad.Models.cl_tipo_corresponsabilidad

            Return PartialView("Corresponsabilidad/_GridViewPartialTipoCorresponsabilidad", objTipoCorresponsabilidad.fnc_obtener_tipo_corrresponsabilidad)
        End Function

        Public Function GridViewPartialDetalleCorresponsabilidad() As ActionResult
            Dim objDetalleCorresponsabilidad As New SIG.Areas.Corresponsabilidad.Models.cl_detalle_corresponsabilidad

            Return PartialView("Corresponsabilidad/_GridViewPartialDetalleCorresponsabilidad", objDetalleCorresponsabilidad.fnc_obtener_detalle_corrresponsabilidad)
        End Function

        <HttpPost()>
        Public Function fnc_operacion_corres(ByVal cod_det_cor As Integer,
                                            ByVal cod_tip_cor As Integer,
                                            ByVal num_det_cor As Integer,
                                            ByVal año_det_cor As Integer,
                                            ByVal par_ini_det_cor As Integer,
                                            ByVal par_fin_det_cor As Integer,
                                            ByVal fec_ini_det_cor As String,
                                            ByVal fec_fin_det_cor As String,
                                            ByVal intCodOper As Integer) As Integer
            Dim boolEnUso As Boolean = False

            If login.Fnc_loggeado() IsNot Nothing Then
                objDetalleCorresponsabilidad.cod_det_cor = cod_det_cor
                objDetalleCorresponsabilidad.cod_tip_cor = cod_tip_cor
                objDetalleCorresponsabilidad.num_det_cor = num_det_cor
                objDetalleCorresponsabilidad.año_det_cor = año_det_cor
                objDetalleCorresponsabilidad.par_ini_det_cor = par_ini_det_cor
                objDetalleCorresponsabilidad.par_fin_det_cor = par_fin_det_cor
                objDetalleCorresponsabilidad.fec_ini_det_cor = fec_ini_det_cor
                objDetalleCorresponsabilidad.fec_fin_det_cor = fec_fin_det_cor
                objDetalleCorresponsabilidad.intTipoOpera = intCodOper
                objDetalleCorresponsabilidad.intCodUser = HttpContext.Session("usuario")

                If intCodOper = 1 Or intCodOper = 2 Then
                    If intCodOper = 2 Then
                        boolEnUso = objDetalleCorresponsabilidad.fnc_correspo_en_uso()
                    End If

                    If boolEnUso = False Then
                        Select Case cod_tip_cor
                            Case 1 'Matricula
                                If objDetalleCorresponsabilidad.fnc_validar_año_no_existe() = False Then
                                    Return objDetalleCorresponsabilidad.fnc_nuevo_actualizar_detalle_corrresponsabilidad()
                                Else
                                    Return 2 'Ya existe una corresponsabilidad con esa información.
                                End If
                            Case 2 'Asistencia
                                If par_fin_det_cor <> 0 Then
                                    If objDetalleCorresponsabilidad.fnc_validar_no_exite_parciales() = False Then
                                        Return objDetalleCorresponsabilidad.fnc_nuevo_actualizar_detalle_corrresponsabilidad()
                                    Else
                                        Return 2
                                    End If
                                Else
                                    If objDetalleCorresponsabilidad.fnc_validar_no_existe_un_parcial() = False Then
                                        Return objDetalleCorresponsabilidad.fnc_nuevo_actualizar_detalle_corrresponsabilidad()
                                    Else
                                        Return 2
                                    End If
                                End If
                            Case 3 'Visitas médicas.
                                If objDetalleCorresponsabilidad.fnc_validar_fechas() = False Then
                                    Return objDetalleCorresponsabilidad.fnc_nuevo_actualizar_detalle_corrresponsabilidad()
                                Else
                                    Return 2
                                End If
                            Case Else
                                Return 0
                        End Select
                    Else
                        Return 4 'Imposible realizar la acción la corresponsabilidad tiene datos relacionados con el módulo de planilla.
                    End If
                Else
                    Return 0
                End If
            Else
                Return 3 'Sessión expirada.
            End If
        End Function

        <HttpPost()>
        Function fnc_obtener_infor_det_corr(ByVal intCodDetCorr As Integer) As JsonResult
            arrListDataTable = objDetalleCorresponsabilidad.fnc_obtener_info_corres(intCodDetCorr)

            Return Json(arrListDataTable, JsonRequestBehavior.DenyGet)
        End Function

        <HttpPost()>
        Function fnc_borrar_corres(ByVal cod_det_cor As Integer) As Integer
            If login.Fnc_loggeado() IsNot Nothing Then
                objDetalleCorresponsabilidad.cod_det_cor = cod_det_cor
                objDetalleCorresponsabilidad.fec_ini_det_cor = "01/01/1900"
                objDetalleCorresponsabilidad.fec_fin_det_cor = "01/01/1900"
                objDetalleCorresponsabilidad.intCodUser = HttpContext.Session("usuario")

                If objDetalleCorresponsabilidad.fnc_correspo_en_uso() = False Then
                    Return objDetalleCorresponsabilidad.fnc_borrar_detalle_corrresponsabilidad()
                Else
                    Return 3
                End If
            Else
                Return 2
            End If
        End Function


#End Region


    End Class
End Namespace