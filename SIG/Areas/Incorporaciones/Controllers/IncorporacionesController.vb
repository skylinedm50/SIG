Imports System.Web.Mvc.ViewResult
Imports System.Web.Script.Serialization
Imports DevExpress.Web.Mvc

Namespace SIG.Areas.Incorporaciones.Controllers
    Public Class IncorporacionesController
        Inherits System.Web.Mvc.Controller

#Region "Variables"

        Private Obj_infor_tabla As New DataTable()
        Private Obj_Hogar_Beneficiario As New SIG.Areas.Incorporaciones.Models.Cl_Hogar_Beneficiario()
        Dim Usuario As New Global.SIG.Cl_Login
        Dim serializador As New JavaScriptSerializer()
        Dim Dictionary_Datos As Dictionary(Of String, Object)
        Dim lista As New List(Of Dictionary(Of String, Object))
        Dim Obj_info_data As New DataTable()


#End Region


#Region "Controlador de formulario de validación de pre-actualizaciones"

        Function BuscarHogarActualizarExpediente(hog_hogar As Integer)

            Dim table_expediente_comentario As New DataTable()
            table_expediente_comentario = Obj_Hogar_Beneficiario.FNC_BuscarExpedienteDigitalHogarActualizar(hog_hogar)

            If table_expediente_comentario.Rows.Count <> 0 Then

                ViewData("solicitud") = 0
                ViewData("comentario") = table_expediente_comentario.Rows(0).Item(1)

                Dim obj_datatable As DataTable = Obj_Hogar_Beneficiario.Fnc_Validar_PreActualizaciones(hog_hogar, Nothing, 1)
                ViewData("descripcion") = If(HttpContext.Session("Observacion") IsNot Nothing, HttpContext.Session("Observacion").ToString(), "")


                obj_datatable.Columns.Add("per_Actualizado", GetType(String))
                obj_datatable.Columns.Add("pre_persona", GetType(String))
                obj_datatable.Columns.Add("Observacion", GetType(String))

                HttpContext.Session("PersonasHogarPreActualizar") = obj_datatable

                Return PartialView("Validar_Pre_Actualizacion/HogarActualizar", obj_datatable)

            Else
                Return 0
            End If

        End Function

        Function GridSolicitudesActualizarInformacion()
            Return PartialView("Validar_Pre_Actualizacion/GridSolicitudesActualizarInformacion", Obj_Hogar_Beneficiario.fnc_solicitudes_Actualizacion())
        End Function


        Function Grid_Detalle_Historico_Actualizaciones(per_persona As Integer)
            Return PartialView("Validar_Pre_Actualizacion/Grid_Detalle_Historico_Actualizaciones", Obj_Hogar_Beneficiario.fnc_BuscarHistoricosActualizacion(per_persona))
        End Function

        Function Fnc_EliminarRegistroNuevo(PK As String)

            If HttpContext.Session("PersonasHogarPreActualizar") IsNot Nothing Then
                Dim Rows = HttpContext.Session("PersonasHogarPreActualizar").Select(" PK = '" + PK + "'")
                For Each row As DataRow In Rows
                    row.Delete()
                Next
            End If
            Return PartialView("Validar_Pre_Actualizacion/PersonasHogarActualizar", HttpContext.Session("PersonasHogarPreActualizar"))
        End Function

        'Este controlador reponde a las peticiones httpGet relizadas por el cliente a la dirección /Incorporaciones/Incorporaciones/ActualizarInformacion
        Function ActualizarInformacion()
            If Usuario.Fnc_loggeado() IsNot Nothing Then
                If Usuario.fnc_validarModulo(1) Then
                    If Usuario.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View("Validar_Pre_Actualizacion/ActualizarInformacion")
                    Else
                        Return Redirect("/Incorporaciones/Inicio")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function


        Function imageSlider(indice As Integer, HExpediente As Integer)
            ViewData("Hogar") = HExpediente
            Return PartialView(indice)
        End Function

        Function PersonasHogarActualizar(hog_hogar As Integer)
            ViewData("hogar") = hog_hogar
            Return PartialView("Validar_Pre_Actualizacion/PersonasHogarActualizar", HttpContext.Session("PersonasHogarPreActualizar"))
        End Function

        Function ActualizarInformacionHogarBeneficiario(hog_hogar As Integer, accion As Integer, descripcion As String)
            Return Me.Obj_Hogar_Beneficiario.fnc_actualiarInformacionHogarBeneficiario(hog_hogar, accion, descripcion)
        End Function

        Function CallbackPersonasActualizar()
            Return PartialView("Validar_Pre_Actualizacion/PersonasHogarActualizar", HttpContext.Session("PersonasHogarPreActualizar"))
        End Function

        Function editarInformacionPersonasActualizar(<ModelBinder(GetType(DevExpress.Web.Mvc.DevExpressEditorsBinder))> ByVal EditarPersona As SIG.Areas.Incorporaciones.Models.Cl_Persona_Editar,
                                                     hogar As Integer)

            If (HttpContext.Session("usuario") IsNot Nothing) Then
                HttpContext.Session("PersonasHogarPreActualizar") = Obj_Hogar_Beneficiario.fnc_actualizar_persona(EditarPersona, hogar, False)
                Return PartialView("Validar_Pre_Actualizacion/PersonasHogarActualizar", HttpContext.Session("PersonasHogarPreActualizar"))
            Else
                Return 0
            End If
        End Function

        Function DescripcionesPrevias(hog_hogar As Integer)
            ViewData("hogar") = hog_hogar
            Return PartialView("Validar_Pre_Actualizacion/ListarDescripcion", Obj_Hogar_Beneficiario.Fnc_ListarDescripciones(hog_hogar))
        End Function

        Function Agregar_nueva_persona(<ModelBinder(GetType(DevExpress.Web.Mvc.DevExpressEditorsBinder))> ByVal NuevaPesona As SIG.Areas.Incorporaciones.Models.Cl_Persona_Editar,
                                       hogar As Integer)

            If (HttpContext.Session("usuario") IsNot Nothing) Then
                HttpContext.Session("PersonasHogarPreActualizar") = Obj_Hogar_Beneficiario.fnc_agregar_nueva_persona(NuevaPesona, hogar, False)
                Return PartialView("Validar_Pre_Actualizacion/PersonasHogarActualizar", HttpContext.Session("PersonasHogarPreActualizar"))
            Else
                Return 0
            End If
        End Function

#End Region

#Region "Controlador de formularios de registrar pre actualizaciones"

        <HttpGet>
        Function pre_actualizaciones(Loggeado As Integer)
            If Loggeado = 1 Then
                Return View("Registrar_Pre_Actualizaciones/pre_actualizaciones")
            End If
        End Function

        Function Buscar_para_preactualizacion(hogar As Integer)
            Return PartialView("Registrar_Pre_Actualizaciones/FormularioRegistroPreActualizaciones", Obj_Hogar_Beneficiario.fnc_buscar_para_pre_actualizar(hogar))
        End Function

        Function personas_pre_actualizacion(hog_hogar As Integer)
            Dim tabla As DataTable
            HttpContext.Session("PersonasHogarPreActualizar") = ""
            tabla = Obj_Hogar_Beneficiario.fnc_buscar_para_pre_actualizar(hog_hogar)
            tabla.Columns.Add("per_Actualizado", GetType(String))
            HttpContext.Session("PersonasHogarPreActualizar") = tabla
            Return PartialView("Registrar_Pre_Actualizaciones/personas_pre_actualizacion", HttpContext.Session("PersonasHogarPreActualizar"))
        End Function

        Function editarInformacionPersonasPre_Actualizar()
            Return PartialView("Registrar_Pre_Actualizaciones/personas_pre_actualizacion", HttpContext.Session("PersonasHogarPreActualizar"))
        End Function

        Function Registrar_PreActualizaciones(hog_hogar As Integer, Nuevo_Titular As Integer, Titular_a_Registrar As List(Of String), registrar_titular As Integer)
            Return Me.Obj_Hogar_Beneficiario.fnc_RegistrarPerActualizaciones(hog_hogar, Nuevo_Titular, Titular_a_Registrar, registrar_titular, 0)
        End Function

        Function Confirmar_registro_nuevo_titular(hog_hogar As Integer)
            Return Me.Obj_Hogar_Beneficiario.fnc_registrarConfirmacionNuevoTitularEnPreActualizaciones(hog_hogar)
        End Function


#End Region

#Region "Cierre del Periodo de Actualizacion"

        Function Cierre_Periodo_Actualizaciones()
            If Usuario.Fnc_loggeado() IsNot Nothing Then
                If Usuario.fnc_validarModulo(1) Then
                    If Usuario.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View("Corte_Actualizaciones/Cierre_Periodo_Actualizaciones")
                    Else
                        Return Redirect("/Incorporaciones/Inicio")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function


        Function Periodo_Actualizacion_Aperturado()
            Return PartialView("Corte_Actualizaciones/Periodo_Actualizacion_Aperturado", Obj_Hogar_Beneficiario.Buscar_Periodo_Actualizacion())
        End Function

        Function Realizar_Cierre(periodo As Integer)
            Return Me.Obj_Hogar_Beneficiario.RealizarCierre(periodo)
        End Function

#End Region

#Region "Apertura de Actualizacion"

        Function Apertura_Periodo_Actualizacion()

            If Usuario.Fnc_loggeado() IsNot Nothing Then
                If Usuario.fnc_validarModulo(1) Then
                    If Usuario.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View("Apertura_Periodo_Actualizacion/Apertura_Periodo_Actualizacion")
                    Else
                        Return Redirect("/Incorporaciones/Inicio")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function Crear_Apertura(PeriodoNombre As String)

            Return Obj_Hogar_Beneficiario.RealizarApertura(PeriodoNombre)

        End Function

#End Region



#Region "Reporte de nuevas personas incorporadas"


        Function Fnc_NuevasPersonasIngresadas()
            If Usuario.Fnc_loggeado() IsNot Nothing Then
                If Usuario.fnc_validarModulo(1) Then
                    If Usuario.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View("Nuevas Personas Ingresadas/NuevasPersonasIngresadas")
                    Else
                        Return Redirect("/Incorporaciones/Inicio")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function


        Function gv_PersonasNuevaIncorporacion()
            Return PartialView("Nuevas Personas Ingresadas/gv_PersonasNuevaIncorporacion", Obj_Hogar_Beneficiario.Fnc_ReportePersonasNuevasIncorporaciones())
        End Function


        Public Function Exportar_gv_reporteHogares_nuevos_miembros()

            Dim Setting As GridViewSettings = Cl_Exportar.gv_NuevasPersonasIncorporadas()
            Return GridViewExtension.ExportToXlsx(Setting, Obj_Hogar_Beneficiario.Fnc_ReportePersonasNuevasIncorporaciones(), "Nuevas personas incorporadas")

        End Function


#End Region


#Region "Clases para exportar Archivos"
        Partial Public Class Cl_Exportar

            Public Shared Function gv_NuevasPersonasIncorporadas() As GridViewSettings

                Dim pv_grid_setting As New GridViewSettings()
                Dim hyperlinkSetting As New HyperLinkSettings

                pv_grid_setting.Name = "gv_gridsetting"
                pv_grid_setting.SettingsPager.PageSize = 50
                pv_grid_setting.CallbackRouteValues = New With {Key .Controller = "Incorporaciones", Key .Action = "gv_PersonasNuevaIncorporacion"}
                pv_grid_setting.Settings.ShowHeaderFilterButton = True
                pv_grid_setting.Settings.ShowFilterRow = True
                pv_grid_setting.Settings.ShowFilterRowMenu = False
                pv_grid_setting.Styles.Header.Font.Bold = True
                pv_grid_setting.Styles.Header.Font.Italic = True
                pv_grid_setting.Styles.Header.Font.Name = "Arial"
                pv_grid_setting.Styles.Header.Font.Size = 10
                pv_grid_setting.KeyFieldName = "pre_personas_actualizacion"
                pv_grid_setting.Styles.Header.Border.BorderColor = System.Drawing.Color.Black
                pv_grid_setting.Styles.Header.Border.BorderStyle = WebControls.BorderStyle.Outset
                pv_grid_setting.CommandColumn.ShowClearFilterButton = True

                pv_grid_setting.Columns.Add(Sub(field)
                                                field.FieldName = "per_hogar"
                                                field.Caption = "N° Hogar"
                                                field.Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                                            End Sub)
                pv_grid_setting.Columns.Add(Sub(field)
                                                field.FieldName = "per_identidad"
                                                field.Caption = "N° Identidad"
                                                field.Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                                            End Sub)
                pv_grid_setting.Columns.Add(Sub(field)
                                                field.FieldName = "per_nombre1"
                                                field.Caption = "Primer Nombre"
                                                field.Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                                            End Sub)
                pv_grid_setting.Columns.Add(Sub(field)
                                                field.FieldName = "per_nombre2"
                                                field.Caption = "Segundo Nombre"
                                                field.Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                                            End Sub)
                pv_grid_setting.Columns.Add(Sub(field)
                                                field.FieldName = "per_apellido1"
                                                field.Caption = "Primer Apellido"
                                                field.Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                                            End Sub)
                pv_grid_setting.Columns.Add(Sub(field)
                                                field.FieldName = "per_apellido2"
                                                field.Caption = "Segundo Apellido"
                                                field.Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                                            End Sub)
                pv_grid_setting.Columns.Add(Sub(field)
                                                field.FieldName = "Titular"
                                                field.Caption = "Titular"
                                                field.SettingsHeaderFilter.Mode = DevExpress.Web.GridHeaderFilterMode.CheckedList
                                                field.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False
                                            End Sub)
                pv_grid_setting.Columns.Add(Sub(field)
                                                field.FieldName = "Fch_Actualizacion"
                                                field.Caption = "Fecha de Actualización"
                                                field.SettingsHeaderFilter.Mode = DevExpress.Web.GridHeaderFilterMode.CheckedList
                                                field.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False
                                                field.PropertiesEdit.DisplayFormatString = "d"
                                            End Sub)
                pv_grid_setting.Columns.Add(Sub(field)
                                                field.FieldName = "Usuario"
                                                field.Caption = "Usuario que Actualizó"
                                                field.SettingsHeaderFilter.Mode = DevExpress.Web.GridHeaderFilterMode.CheckedList
                                                field.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False
                                            End Sub)
                pv_grid_setting.Columns.Add(Sub(field)
                                                field.FieldName = "Estado"
                                                field.Caption = "Resultado de la verificación"
                                                field.SettingsHeaderFilter.Mode = DevExpress.Web.GridHeaderFilterMode.CheckedList
                                                field.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False
                                            End Sub)

                Return pv_grid_setting

            End Function

        End Class
#End Region



    End Class
End Namespace
