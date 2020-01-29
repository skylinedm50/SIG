Imports DevExpress.Web.Mvc
Imports DevExpress.Web
Imports DevExpress.Utils
Imports Newtonsoft.Json

Imports DevExpress.Web.ASPxPivotGrid
Imports DevExpress.XtraPivotGrid
Imports DevExpress.Data.PivotGrid
Imports DevExpress.Export

Namespace SIG.Areas.Mineria.Controllers
    Public Class PlanillasPagoController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Mineria/PlanillasPago
        Dim login As New Global.SIG.Cl_Login
        Dim planillas As SIG.Areas.Mineria.Models.Cl_PlanillasPago = New SIG.Areas.Mineria.Models.Cl_PlanillasPago
        Dim share As SIG.Areas.Mineria.Models.Cl_Shared = New SIG.Areas.Mineria.Models.Cl_Shared

        Function Home() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function


#Region "funciones para la página del listado de titulares"

        Function v_TitularesPago() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function pv_gdvMaestroListadoTitulares(ByVal pago As String, ByVal tipo As String, ByVal departamento As String, ByVal municipio As String, ByVal aldea As String) As ActionResult

            If departamento = "" Then
                departamento = Nothing
            End If

            If municipio = "" Then
                municipio = Nothing
            End If

            If aldea = "" Then
                aldea = Nothing
            End If

            ViewData("pago") = pago
            ViewData("tipo") = tipo
            ViewData("departamento") = departamento
            ViewData("municpio") = municipio
            ViewData("aldea") = aldea
            Return PartialView(planillas.Fnc_listado_particitantes(pago, tipo, departamento, municipio, aldea))
        End Function

        Function pv_EstadoCuentaTitular(ByVal pago As String, ByVal identidad As String) As ActionResult

            Dim ds As DataSet = planillas.Fnc_estado_cuenta_participante(pago, identidad)

            ViewData("identidad") = ds.Tables("info_planilla").Rows(0).Item("identidad_titular")
            ViewData("nombre") = ds.Tables("info_planilla").Rows(0).Item("nombre_titular")
            ViewData("estado") = ds.Tables("info_planilla").Rows(0).Item("estado")
            ViewData("monto") = ds.Tables("info_planilla").Rows(0).Item("monto_total")
            ViewData("deduccion") = ds.Tables("info_planilla").Rows(0).Item("deducciones")
            ViewData("elegibilidad") = ds.Tables("info_planilla").Rows(0).Item("nombre_elegibilidad")
            ViewData("desc_nivel_elegibilidad") = ds.Tables("info_planilla").Rows(0).Item("desc_elegibilidad")
            ViewData("componente") = ds.Tables("info_planilla").Rows(0).Item("nombre_componente")
            ViewData("desc_componente") = ds.Tables("info_planilla").Rows(0).Item("desc_componente")
            ViewData("corresponsabilidad") = ds.Tables("info_planilla").Rows(0).Item("nombre_corresponsabilidad")
            ViewData("desc_corresponsabilidad") = ds.Tables("info_planilla").Rows(0).Item("desc_correponsabilidad")
            ViewData("proyeccion") = ds.Tables("info_planilla").Rows(0).Item("proyeccion")
            ViewData("proyeccion_corta") = ds.Tables("info_planilla").Rows(0).Item("proyeccion_corta")
            ViewData("cobro") = ds.Tables("info_planilla").Rows(0).Item("cobro")
            ViewData("fecha_cobro") = ds.Tables("info_planilla").Rows(0).Item("fecha_cobro")
            ViewData("estado_hogar") = ds.Tables("info_planilla").Rows(0).Item("estado_hogar")

            ViewData("estado_cuenta") = ds.Tables("estado_cuenta")

            ViewData("pago") = pago
            Return PartialView()
        End Function

        Function pv_gdvListadoNinos(ByVal pago As String, ByVal identidad As String) As ActionResult
            ViewData("pago") = pago
            ViewData("identidad") = identidad
            Return PartialView(planillas.Fnc_listado_ninos(pago, identidad))
        End Function

        Function pv_DetalleCorreponsabalidadNino(ByVal pago As String, ByVal cod_persona As String) As ActionResult

            Dim dt As DataTable = planillas.Fnc_detalle_correponsabilidad_nino(pago, cod_persona)

            'información del niño
            ViewData("identidad") = dt.Rows(0).Item("identidad").ToString()
            ViewData("fecha_nacimiento") = dt.Rows(0).Item("fecha_nacimiento").ToString()

            If dt.Rows(0).Item("sexo_persona").ToString() = "1" Then
                ViewData("genero") = "Masculino"
            ElseIf dt.Rows(0).Item("sexo_persona").ToString() = "2" Then
                ViewData("genero") = "Femenino"
            End If
            'ViewData("") = dt.Rows(0).Item("sexo_persona")
            ViewData("nombre") = dt.Rows(0).Item("nombre_persona")

            'información correponsabilidad
            ViewData("elegibilidad") = dt.Rows(0).Item("elegibilidad")
            ViewData("ciclo") = dt.Rows(0).Item("desc_nivel_elegibilidad")
            ViewData("correponsabilidad") = dt.Rows(0).Item("desc_corresponsabilidad")
            ViewData("cumplimiento") = dt.Rows(0).Item("estado_corresponsabilidad")

            'detalle correponsabilidad
            ViewData("centro") = dt.Rows(0).Item("nombre_centro_educativo_salud")
            ViewData("año") = dt.Rows(0).Item("año")
            ViewData("grado") = dt.Rows(0).Item("nombre_grado").ToString()
            ViewData("nro_visitas") = dt.Rows(0).Item("numero_visitas_centro_salud").ToString()
            ViewData("ultima_visita") = dt.Rows(0).Item("fecha_ultima_visita").ToString()

            'Return PartialView(planillas.Fnc_detalle_correponsabilidad_nino(pago, cod_persona))
            Return PartialView()
        End Function

        Function exportarListadoTitulares() As ActionResult

            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")
            Dim pago As String = ComboBoxExtension.GetValue(Of String)("cbxPagosAno")
            Dim tipo As String = RadioButtonListExtension.GetValue(Of String)("rbTipo")
            Dim departamento As String = ComboBoxExtension.GetValue(Of String)("cbxDpto")
            Dim municipio As String = ComboBoxExtension.GetValue(Of String)("cbxMuni")
            Dim aldea As String = ComboBoxExtension.GetValue(Of String)("cbxAldea")

            Dim dt As DataTable = planillas.Fnc_listado_particitantes(pago, tipo, departamento, municipio, aldea)

            If export = "Excel" Then
                Return GridViewExtension.ExportToXlsx(SIG.Areas.Mineria.Controllers.exportGdvListadoParticipantes.ExportGridViewSettings, dt, "Listado de Titulares en Pago")
            ElseIf export = "Pdf" Then
                Return GridViewExtension.ExportToPdf(SIG.Areas.Mineria.Controllers.exportGdvListadoParticipantes.ExportGridViewSettings, dt, "Listado de Titulares en Pago")
            ElseIf export = "Rtf" Then
                Return GridViewExtension.ExportToRtf(SIG.Areas.Mineria.Controllers.exportGdvListadoParticipantes.ExportGridViewSettings, dt, "Listado de Titulares en Pago")
                'ElseIf export = "Html" Then
                'Return GridViewExtension.ExportToHtml(SIG.Areas.Mineria.Controllers.exportGdvListadoParticipantes.ExportGridViewSettings, dt, "Listado de Titulares en Pago")
            ElseIf export = "Csv" Then
                Return GridViewExtension.ExportToCsv(SIG.Areas.Mineria.Controllers.exportGdvListadoParticipantes.ExportGridViewSettings, dt, "Listado de Titulares en Pago")
            End If

            Return Nothing

        End Function

#End Region

#Region "funciones para el consolidado de pago"

        Function v_ConsolidadoPago() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

            Return Nothing
        End Function

        Function pv_pvgConsolidadoPago(ByVal pago As String) As ActionResult

            Dim dt As DataTable

            If Session("dtConsolidado") Is Nothing Then
                dt = planillas.Fnc_consolidado_pago(pago)
                Session("dtConsolidado") = dt
                Session("codConsolidado") = pago
            Else
                If Session("codConsolidado") = pago Then
                    dt = Session("dtConsolidado")
                Else
                    dt = planillas.Fnc_consolidado_pago(pago)
                    Session("dtConsolidado") = dt
                    Session("codConsolidado") = pago
                End If
            End If

            ViewData("pago") = pago
            Return PartialView(dt)
        End Function

        Function pv_chrConsolidadoPago() As ActionResult

            ViewData("tipo") = share.fnc_obtener_tipo_grafico(Request.Params("tipoGrafico"))
            Dim chartModel = PivotGridExtension.GetDataObject(SIG.Areas.Mineria.Controllers.exportPvgConsolidadoPago.ExportPivotGridSettings, Session("dtConsolidado"))
            Return PartialView(chartModel)

        End Function

        Function exportarConsolidadoPago() As ActionResult

            Dim pago As String = ComboBoxExtension.GetValue(Of String)("cbxPagosAno")
            Dim exportar As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")
            Dim nombre As String = "Consolidado de " + share.Fnc_obtener_nombre_pago(pago)
            Dim settings As PivotGridSettings = SIG.Areas.Mineria.Controllers.exportPvgConsolidadoPago.ExportPivotGridSettings()
            Dim dt As DataTable = planillas.Fnc_consolidado_pago(pago)

            If exportar = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(settings, dt, nombre)
            ElseIf exportar = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(settings, dt, nombre)
            ElseIf exportar = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(settings, dt, nombre)
            ElseIf exportar = "Html" Then
                Return PivotGridExtension.ExportToHtml(settings, dt, nombre)
            ElseIf exportar = "Csv" Then
                Return PivotGridExtension.ExportToCsv(settings, dt, nombre)
            End If

            Return Nothing
        End Function

#End Region

#Region "Funciones para la comparación entre dos planillas"

        Function v_ComparacionPlanillas() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

            Return Nothing
        End Function

        Function pv_pvgComparacionPlanillas(ByVal strPagos As String) As ActionResult

            Dim pagos() As Integer = Array.ConvertAll(Split(strPagos, ","), Function(s) Int32.Parse(s))
            Dim pago1 As Integer = Math.Min(pagos(0), pagos(1))
            Dim pago2 As Integer = Math.Max(pagos(0), pagos(1))

            ViewData("strPagos") = strPagos
            ViewData("pago1") = share.Fnc_obtener_nombre_pago(pago1)
            ViewData("pago2") = share.Fnc_obtener_nombre_pago(pago2)

            Return PartialView(planillas.Fnc_comparacion_planillas(pago1, pago2))

        End Function

        Function pv_chrComparacionPlanillas() As ActionResult

            ViewData("tipo") = share.fnc_obtener_tipo_grafico(Request.Params("tipoGrafico"))
            'If ViewData("tipo") Then
            '    ViewData("tipo") = tipoGrafico
            'End If

            Dim pagos() As Integer = Array.ConvertAll(Split(Request.Params("strPagos"), ","), Function(s) Int32.Parse(s))
            Dim pago1 As Integer = Math.Min(pagos(0), pagos(1))
            Dim pago2 As Integer = Math.Max(pagos(0), pagos(1))
            Dim chartModel = PivotGridExtension.GetDataObject(SIG.Areas.Mineria.Controllers.exportPvgComparacionPlanillas.ExportPivotGridSettings(share.Fnc_obtener_nombre_pago(pago1), share.Fnc_obtener_nombre_pago(pago2)), planillas.Fnc_comparacion_planillas(pago1, pago2))
            Return PartialView(chartModel)

        End Function

        Function exportarComparacionPlanillas() As ActionResult

            Dim strPagos As String = TextBoxExtension.GetValue(Of String)("txtPagosSeleccionados2")
            Dim pagos() As Integer = Array.ConvertAll(Split(strPagos, ","), Function(s) Int32.Parse(s))
            Dim pago1 As Integer = Math.Min(pagos(0), pagos(1))
            Dim pago2 As Integer = Math.Max(pagos(0), pagos(1))
            Dim nombre1 As String = share.Fnc_obtener_nombre_pago(pago1)
            Dim nombre2 As String = share.Fnc_obtener_nombre_pago(pago2)

            Dim settings As PivotGridSettings = SIG.Areas.Mineria.Controllers.exportPvgComparacionPlanillas.ExportPivotGridSettings(nombre1, nombre2)
            Dim dt As DataTable = planillas.Fnc_comparacion_planillas(pago1, pago2)
            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")

            If export = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(settings, dt, "Arrastre, Altas y Bajas entre " + nombre1 + " y " + nombre2)
            ElseIf export = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(settings, dt, "Arrastre, Altas y Bajas entre " + nombre1 + " y " + nombre2)
            ElseIf export = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(settings, dt, "Arrastre, Altas y Bajas entre " + nombre1 + " y " + nombre2)
            ElseIf export = "Html" Then
                Return PivotGridExtension.ExportToHtml(settings, dt, "Arrastre, Altas y Bajas entre " + nombre1 + " y " + nombre2)
            ElseIf export = "Csv" Then
                Return PivotGridExtension.ExportToCsv(settings, dt, "Arrastre, Altas y Bajas entre " + nombre1 + " y " + nombre2)
            End If

            Return Nothing

        End Function

        Function pv_mapaDiferenciaAltasBajasPlanillas(ByVal strPagos As String) As ActionResult

            Dim pagos() As Integer = Array.ConvertAll(Split(strPagos, ","), Function(s) Int32.Parse(s))
            Dim jsonString As String = String.Empty
            jsonString = JsonConvert.SerializeObject(planillas.Fnc_diferencia_entre_altas_bajas_planillas(Math.Min(pagos(0), pagos(1)), Math.Max(pagos(0), pagos(1))))

            ViewData("jsonString") = jsonString
            Return PartialView()

        End Function

        Function pv_mapaDiferenciaAltasBajasPlanillasDepartamento(ByVal strPagos As String, ByVal departamento As String) As JsonResult

            Dim pagos() As Integer = Array.ConvertAll(Split(strPagos, ","), Function(s) Int32.Parse(s))
            Return Json(JsonConvert.SerializeObject(planillas.Fnc_diferencia_entre_altas_bajas_planillas_departamento(Math.Min(pagos(0), pagos(1)), Math.Max(pagos(0), pagos(1)), departamento)))

        End Function

        Function pv_pvgRazonCaidaHogares(ByVal strPagos As String) As ActionResult

            Dim pagos() As Integer = Array.ConvertAll(Split(strPagos, ","), Function(s) Int32.Parse(s))
            ViewData("strPagos") = strPagos

            Return PartialView(planillas.fnc_razon_caida_hogares(Math.Min(pagos(0), pagos(1)), Math.Max(pagos(0), pagos(1))))

        End Function

        Function exportarRazonCaida() As ActionResult

            Dim strPagos As String = TextBoxExtension.GetValue(Of String)("txtPagosSeleccionados")
            Dim pagos() As Integer = Array.ConvertAll(Split(strPagos, ","), Function(s) Int32.Parse(s))
            Dim pago1 As Integer = Math.Min(pagos(0), pagos(1))
            Dim pago2 As Integer = Math.Max(pagos(0), pagos(1))
            Dim nombre1 As String = share.Fnc_obtener_nombre_pago(pago1)
            Dim nombre2 As String = share.Fnc_obtener_nombre_pago(pago2)

            Dim dt As DataTable = planillas.fnc_razon_caida_hogares(pago1, pago2)
            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotarRazonesExclusion")
            Dim settings As PivotGridSettings = SIG.Areas.Mineria.Controllers.exportPvgRazonCaidaHogares.ExportPivotGridSettings

            If export = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(settings, dt, "Razón de Caída de Hogares entre " + nombre1 + " y " + nombre2)
            ElseIf export = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(settings, dt, "Razón de Caída de Hogares entre " + nombre1 + " y " + nombre2)
            ElseIf export = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(settings, dt, "Razón de Caída de Hogares entre " + nombre1 + " y " + nombre2)
            ElseIf export = "Html" Then
                Return PivotGridExtension.ExportToHtml(settings, dt, "Razón de Caída de Hogares entre " + nombre1 + " y " + nombre2)
            ElseIf export = "Csv" Then
                Return PivotGridExtension.ExportToCsv(settings, dt, "Razón de Caída de Hogares entre " + nombre1 + " y " + nombre2)
            End If

            Return Nothing
        End Function

#End Region

#Region "Funciones para los elegibles contro programados"

        Function v_ElegiblesContraProgramados() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

            Return Nothing

        End Function

        Function pv_pvgElegiblesVsProgramado(ByVal pago As String) As ActionResult
            ViewData("pago") = pago
            Return PartialView(planillas.Fnc_elegibles_contra_programados(pago))
        End Function

        Function exportarElegiblesContraProgramado() As ActionResult

            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")
            Dim settings As PivotGridSettings = SIG.Areas.Mineria.Controllers.exportPvgElegiblesControProgramado.ExportPivotGridSettings
            Dim pago As String = ComboBoxExtension.GetValue(Of String)("cbxPagosAno")
            Dim nombre As String = "Elegibles contra programados del pago " + share.Fnc_obtener_nombre_pago(pago)
            Dim dt As DataTable = planillas.Fnc_elegibles_contra_programados(pago)

            If export = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(settings, dt, nombre)
            ElseIf export = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(settings, dt, nombre)
            ElseIf export = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(settings, dt, nombre)
            ElseIf export = "Html" Then
                Return PivotGridExtension.ExportToHtml(settings, dt, nombre)
            ElseIf export = "Csv" Then
                Return PivotGridExtension.ExportToCsv(settings, dt, nombre)
            End If

            Return Nothing

        End Function

#End Region

#Region "Funciones para las razones de exclusión de hogares"

        Function v_RazonesExclusionHogares() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function pv_pvgRazonesExclusionHogares(ByVal pago As String) As ActionResult
            ViewData("pago") = pago
            Return PartialView(planillas.Fnc_razon_exclusion_hogares(pago))
        End Function

        Function exportarRazonesExclusionHogares() As ActionResult

            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")
            Dim settings As PivotGridSettings = SIG.Areas.Mineria.Controllers.exportPvgRazonExclusionHogares.ExportPivotGridSettings
            Dim pago As String = ComboBoxExtension.GetValue(Of String)("cbxPagosAno")

            If export = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(settings, planillas.Fnc_razon_exclusion_hogares(pago), "Razón de Exclusion de Hogares")
            ElseIf export = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(settings, planillas.Fnc_razon_exclusion_hogares(pago), "Razón de Exclusion de Hogares")
            ElseIf export = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(settings, planillas.Fnc_razon_exclusion_hogares(pago), "Razón de Exclusion de Hogares")
            ElseIf export = "Html" Then
                Return PivotGridExtension.ExportToHtml(settings, planillas.Fnc_razon_exclusion_hogares(pago), "Razón de Exclusion de Hogares")
            ElseIf export = "Csv" Then
                Return PivotGridExtension.ExportToCsv(settings, planillas.Fnc_razon_exclusion_hogares(pago), "Razón de Exclusion de Hogares")
            End If

            Return Nothing

        End Function

#End Region

#Region "Funciones para censos y fichas utilizadas en un pago"

        Function v_CantidadFichasPorCensoAno() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function pv_pgvFichasCensoAno(ByVal pago As String) As ActionResult
            ViewData("pago") = pago
            Return PartialView(planillas.Fnc_censo_ano_fichas_pago(pago))
        End Function

        Function exportarFichasPorCensoAno() As ActionResult

            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")
            Dim settings As PivotGridSettings = SIG.Areas.Mineria.Controllers.exportPvgRazonExclusionHogares.ExportPivotGridSettings
            Dim pago As String = ComboBoxExtension.GetValue(Of String)("cbxPagosAno")

            If export = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(SIG.Areas.Mineria.Controllers.exportPvgFichasPorCensoAnoEnPago.exportPvgSettings, planillas.Fnc_censo_ano_fichas_pago(pago), "Cantidad de Fichas Por Censo y Año En Pago")
            ElseIf export = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(SIG.Areas.Mineria.Controllers.exportPvgFichasPorCensoAnoEnPago.exportPvgSettings, planillas.Fnc_censo_ano_fichas_pago(pago), "Cantidad de Fichas Por Censo y Año En Pago")
            ElseIf export = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(SIG.Areas.Mineria.Controllers.exportPvgFichasPorCensoAnoEnPago.exportPvgSettings, planillas.Fnc_censo_ano_fichas_pago(pago), "Cantidad de Fichas Por Censo y Año En Pago")
            ElseIf export = "Html" Then
                Return PivotGridExtension.ExportToHtml(SIG.Areas.Mineria.Controllers.exportPvgFichasPorCensoAnoEnPago.exportPvgSettings, planillas.Fnc_censo_ano_fichas_pago(pago), "Cantidad de Fichas Por Censo y Año En Pago")
            ElseIf export = "Csv" Then
                Return PivotGridExtension.ExportToCsv(SIG.Areas.Mineria.Controllers.exportPvgFichasPorCensoAnoEnPago.exportPvgSettings, planillas.Fnc_censo_ano_fichas_pago(pago), "Cantidad de Fichas Por Censo y Año En Pago")
            End If

            Return Nothing

        End Function

#End Region

#Region "Funciones niños pagados por ciclo"

        Function v_NinosPagadosPorCiclo(variante As Int16) As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    ViewData("variante") = variante
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function



        Function pv_pgvNinosPagadosPorCiclos(ByVal pago As String, ByVal variante As Integer) As ActionResult
            ViewData("pago") = pago
            ViewData("variante") = variante
            Return PartialView(planillas.Fnc_ninos_pagados_ciclo(pago))
        End Function

        Function exportarNinosPagadosPorCiclo(ByVal variante As Integer) As ActionResult

            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")
            Dim settings As PivotGridSettings = SIG.Areas.Mineria.Controllers.exportPvgRazonExclusionHogares.ExportPivotGridSettings
            Dim pago As String = ComboBoxExtension.GetValue(Of String)("cbxPagosAno")

            If export = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(SIG.Areas.Mineria.Controllers.exportPvgNinosPagadosPorCiclo.exportPvgSettings(variante), planillas.Fnc_ninos_pagados_ciclo(pago), "Cantidad de Niños Pagados Por Ciclo")
            ElseIf export = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(SIG.Areas.Mineria.Controllers.exportPvgNinosPagadosPorCiclo.exportPvgSettings(variante), planillas.Fnc_ninos_pagados_ciclo(pago), "Cantidad de Niños Pagados Por Ciclo")
            ElseIf export = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(SIG.Areas.Mineria.Controllers.exportPvgNinosPagadosPorCiclo.exportPvgSettings(variante), planillas.Fnc_ninos_pagados_ciclo(pago), "Cantidad de Niños Pagados Por Ciclo")
            ElseIf export = "Html" Then
                Return PivotGridExtension.ExportToHtml(SIG.Areas.Mineria.Controllers.exportPvgNinosPagadosPorCiclo.exportPvgSettings(variante), planillas.Fnc_ninos_pagados_ciclo(pago), "Cantidad de Niños Pagados Por Ciclo")
            ElseIf export = "Csv" Then
                Return PivotGridExtension.ExportToCsv(SIG.Areas.Mineria.Controllers.exportPvgNinosPagadosPorCiclo.exportPvgSettings(variante), planillas.Fnc_ninos_pagados_ciclo(pago), "Cantidad de Niños Pagados Por Ciclo")
            End If

            Return Nothing

        End Function

        Function pv_mapaCantidadNinosPorCiclo(ByVal pago As String) As ActionResult

            Dim jsonString As String = String.Empty
            jsonString = JsonConvert.SerializeObject(planillas.Fnc_cantidad_ninos_ciclo(pago))

            ViewData("jsonString") = jsonString
            Return PartialView()

        End Function

        Function pv_mapaCantidadNinosPorCicloDepartamento(ByVal pago As String, ByVal dpto As String) As JsonResult
            Return Json(JsonConvert.SerializeObject(planillas.Fnc_cantidad_ninos_ciclo_departamento(pago, dpto)))
        End Function

#End Region

#Region "Funciones listado de niños con corresponsabilidada en planilla"

        Function v_ListadoNinosPago() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function pv_gdvListadoNinosPago(ByVal pago As String, ByVal departamento As String, ByVal municipio As String, ByVal aldea As String) As ActionResult

            If departamento = "" Then
                departamento = Nothing
            End If

            If municipio = "" Then
                municipio = Nothing
            End If

            If aldea = "" Then
                aldea = Nothing
            End If

            ViewData("pago") = pago
            ViewData("departamento") = departamento
            ViewData("municpio") = municipio
            ViewData("aldea") = aldea

            Session("dt") = planillas.Fnc_listado_ninos_con_correponsabilidad_pago(pago, departamento, municipio, aldea)

            Return PartialView(Session("dt"))

        End Function

        Function exportarListadoNinoPago() As ActionResult

            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")
            Dim pago As String = ComboBoxExtension.GetValue(Of String)("cbxPagosAno")
            Dim departamento As String = ComboBoxExtension.GetValue(Of String)("cbxDpto")
            Dim municipio As String = ComboBoxExtension.GetValue(Of String)("cbxMuni")
            Dim aldea As String = ComboBoxExtension.GetValue(Of String)("cbxAldea")

            Dim dt As DataTable = planillas.Fnc_listado_ninos_con_correponsabilidad_pago(pago, departamento, municipio, aldea)

            If export = "Excel" Then
                Return GridViewExtension.ExportToXlsx(SIG.Areas.Mineria.Controllers.exportGdvListadoNinosPago.ExportGridViewSettings, dt, "Listado Niños con Corresponsabilidad en Pago")
            ElseIf export = "Pdf" Then
                Return GridViewExtension.ExportToPdf(SIG.Areas.Mineria.Controllers.exportGdvListadoNinosPago.ExportGridViewSettings, dt, "Listado Niños con Corresponsabilidad en Pago")
            ElseIf export = "Rtf" Then
                Return GridViewExtension.ExportToRtf(SIG.Areas.Mineria.Controllers.exportGdvListadoNinosPago.ExportGridViewSettings, dt, "Listado Niños con Corresponsabilidad en Pago")
                'ElseIf export = "Html" Then
                'Return GridViewExtension.ExportToHtml(SIG.Areas.Mineria.Controllers.exportGdvListadoNinosPago.ExportGridViewSettings, dt, "Listado Niños con Corresponsabilidad en Pago")
            ElseIf export = "Csv" Then
                Return GridViewExtension.ExportToCsv(SIG.Areas.Mineria.Controllers.exportGdvListadoNinosPago.ExportGridViewSettings, dt, "Listado Niños con Corresponsabilidad en Pago")
            End If

            Return Nothing

        End Function

#End Region

#Region "Funciones para los nucleos famiales de la muestra"

        Function v_NucleoFamiliarMuestra() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function pv_pvgNucleoFamiliarMuestra(ByVal pago As String, ByVal hogares As String) As ActionResult

            ViewData("pago") = pago
            ViewData("hogares") = hogares
            Return PartialView(planillas.Fnc_nucleo_familiar_muestra(pago, hogares))

        End Function

        Function exportarNucleoFamiliarMuestra() As ActionResult

            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")
            Dim pago As String = ComboBoxExtension.GetValue(Of String)("cbxPagosAno")
            Dim hogares As String = TextBoxExtension.GetValue(Of String)("txtHogaresHiden")


            Dim dt As DataTable = planillas.Fnc_nucleo_familiar_muestra(pago, hogares)

            If export = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(SIG.Areas.Mineria.Controllers.exportPvgNucleoFamiliarMuestra.exportPvgSettings, dt, "Nucleo Familiar de Hogares")
            ElseIf export = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(SIG.Areas.Mineria.Controllers.exportPvgNucleoFamiliarMuestra.exportPvgSettings, dt, "Nucleo Familiar de Hogares")
            ElseIf export = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(SIG.Areas.Mineria.Controllers.exportPvgNucleoFamiliarMuestra.exportPvgSettings, dt, "Nucleo Familiar de Hogares")
            ElseIf export = "Html" Then
                Return PivotGridExtension.ExportToHtml(SIG.Areas.Mineria.Controllers.exportPvgNucleoFamiliarMuestra.exportPvgSettings, dt, "Nucleo Familiar de Hogares")
            ElseIf export = "Csv" Then
                Return PivotGridExtension.ExportToCsv(SIG.Areas.Mineria.Controllers.exportPvgNucleoFamiliarMuestra.exportPvgSettings, dt, "Nucleo Familiar de Hogares")
            End If

            Return Nothing

        End Function

#End Region

#Region "Funciones para la base de lo pagado"

        Function v_BasePagado() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function pv_pvgBasePagado(ByVal pago As String) As ActionResult

            ViewData("pago") = pago
            Return PartialView(planillas.Fnc_base_pagado(pago))

        End Function

        Function exportarBasePagado() As ActionResult

            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")
            Dim pago As String = ComboBoxExtension.GetValue(Of String)("cbxPagosAno")
            Dim settings As PivotGridSettings = SIG.Areas.Mineria.Controllers.exportPvgBasePagado.exportPvgSettings

            Dim dt As DataTable = planillas.Fnc_base_pagado(pago)

            If export = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(settings, dt, "Base pagado")
            ElseIf export = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(settings, dt, "Base pagado")
            ElseIf export = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(settings, dt, "Base pagado")
            ElseIf export = "Html" Then
                Return PivotGridExtension.ExportToHtml(settings, dt, "Base pagado")
            ElseIf export = "Csv" Then
                Return PivotGridExtension.ExportToCsv(settings, dt, "Base pagado")
            End If

            Return Nothing

        End Function

#End Region

#Region "Funciones para la pantalla de cantidad de hogares pagados"

        Function v_HogaresPagadosPeriodoTiempo() As ActionResult

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

            Return Nothing

        End Function

        Function pv_ControlesAcumuladoPlanillas() As ActionResult
            Return PartialView()
        End Function

        Function pv_pvgHogaresPagados(ByVal tipo As String, ByVal campos As String, ByVal strPlanillas As String)

            If strPlanillas Is Nothing Then
                strPlanillas = ""
            End If

            ViewData("tipo") = tipo
            ViewData("campos") = campos
            ViewData("planillas") = strPlanillas

            Return PartialView(planillas.fnc_obtener_cantidad_hogares_pagados(tipo, campos, strPlanillas))

        End Function

        Function exportarHogaresPagados()

            Dim tipo As String = EditorExtension.GetValue(Of String)("rblPeriodoTiempo")
            Dim export As String = EditorExtension.GetValue(Of String)("cbxExpotar")
            Dim campos As String = EditorExtension.GetValue(Of String)("cblCampos")
            Dim strPlanilla As String = EditorExtension.GetValue(Of String)("gdlPagos")

            If strPlanilla Is Nothing Then
                strPlanilla = ""
            End If

            Dim settings As PivotGridSettings = exportPvgHogaresPagados.exportPvgSettings(tipo, campos)
            Dim dt As DataTable = planillas.fnc_obtener_cantidad_hogares_pagados(tipo, campos, strPlanilla)
            Dim nombre As String = "Cantidad de Hogares Pagados"

            If export = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(settings, dt, nombre)
            ElseIf export = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(settings, dt, nombre)
            ElseIf export = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(settings, dt, nombre)
            ElseIf export = "Html" Then
                Return PivotGridExtension.ExportToHtml(settings, dt, nombre)
            ElseIf export = "Csv" Then
                Return PivotGridExtension.ExportToCsv(settings, dt, nombre)
            End If

            Return Nothing

        End Function

#End Region

#Region "Funciones para la consulta ECCAI"

        Function v_ConsultaECCAI() As ActionResult

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

            Return Nothing

        End Function

        Function exportarECCAI() As ActionResult

            Dim export As String = EditorExtension.GetValue(Of String)("cbxExpotar")
            Dim pago As String = EditorExtension.GetValue(Of String)("cbxPagosAno")
            Dim hogares As String = EditorExtension.GetValue(Of String)("txtHogaresHiden")
            Dim nombre As String = "Estado Cuenta, Cumplimiento, Apercibimeinto e Incumplimiento"

            Dim dt As DataTable

            If Not pago Is Nothing Then
                dt = planillas.fnc_obtener_eccai(pago, hogares)
            Else
                dt = planillas.fnc_obtener_eccai(hogares)
            End If

            If export = "Excel" Then
                Return GridViewExtension.ExportToXlsx(exportGdvECCAI.ExportGridViewSettings, dt, nombre, ExportType.WYSIWYG)
            ElseIf export = "Pdf" Then
                Return GridViewExtension.ExportToPdf(exportGdvECCAI.ExportGridViewSettings, dt, nombre, ExportType.WYSIWYG)
            ElseIf export = "Rtf" Then
                Return GridViewExtension.ExportToRtf(exportGdvECCAI.ExportGridViewSettings, dt, nombre, ExportType.WYSIWYG)
            ElseIf export = "Csv" Then
                Return GridViewExtension.ExportToCsv(exportGdvECCAI.ExportGridViewSettings, dt, nombre, ExportType.WYSIWYG)

            End If

            Return Nothing

        End Function

        Function pv_gdvECCAI(ByVal pago As String, ByVal hogares As String) As ActionResult

            ViewData("pago") = pago
            ViewData("hogares") = hogares

            If Not pago = "" Then
                Return PartialView(planillas.fnc_obtener_eccai(pago, hogares))
            Else
                Return PartialView(planillas.fnc_obtener_eccai(hogares))
            End If

        End Function

#End Region

    End Class

#Region "Clase para los settings de los gridview y pivotgrid"

    Public NotInheritable Class exportGdvListadoParticipantes

        Private Shared exportSettings As GridViewSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportGridViewSettings() As GridViewSettings

            Get
                exportSettings = CreateExportGdvMaestroListadoParticipantes()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportGdvMaestroListadoParticipantes() As GridViewSettings

            Dim settings As New GridViewSettings()

            settings.Name = "gvMaestroListadoParticipantes"

            settings.SettingsPager.Position = PagerPosition.Bottom
            settings.SettingsPager.FirstPageButton.Visible = True
            settings.SettingsPager.LastPageButton.Visible = True
            settings.SettingsPager.PageSizeItemSettings.Visible = True
            settings.SettingsPager.PageSizeItemSettings.Items = New String() {"10", "20", "50"}
            settings.SettingsBehavior.AllowGroup = True
            settings.SettingsBehavior.AllowSort = True
            settings.Settings.ShowGroupPanel = True

            settings.EnableCallbackAnimation = True
            settings.EnablePagingCallbackAnimation = True
            settings.EnableCallbackCompression = True

            settings.Settings.ShowHeaderFilterButton = True
            settings.Settings.ShowFilterRow = True
            settings.SettingsPopup.HeaderFilter.Height = 200

            settings.SettingsExport.FileName = "LISTADO DE TITULARES"
            settings.Caption = "LISTADO DE TITULARES"

            'necesarios para el detalle
            settings.KeyFieldName = "identidad_titular"
            settings.SettingsDetail.AllowOnlyOneMasterRowExpanded = True
            settings.SettingsDetail.ShowDetailRow = True
            settings.SettingsDetail.ExportMode = GridViewDetailExportMode.Expanded

            settings.Columns.AddBand(
                Sub(ag)
                    ag.Caption = "Área Geográfica"
                    ag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    ag.Columns.Add("desc_departamento", "Departamento").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    ag.Columns.Add("desc_municipio", "Municipio").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    ag.Columns.Add("desc_aldea", "Aldea").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList

                    settings.Columns.Add("cod_departamento").Visible = False
                    settings.Columns.Add("cod_municipio").Visible = False
                    settings.Columns.Add("cod_aldea").Visible = False
                End Sub)
            settings.Columns.AddBand(
                Sub(tit)
                    tit.Caption = "Titular"
                    tit.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    tit.Columns.Add("cod_hogar", "Hogar").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                    tit.Columns.Add("identidad_titular", "Nro. Identidad").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                    tit.Columns.Add("nombre_titular", "Nombre Completo").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                End Sub)
            settings.Columns.Add("Estado", "Estado").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
            settings.Columns.AddBand(
                Sub(ficha)
                    ficha.Caption = "Ficha"
                    ficha.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    ficha.Columns.Add("numero_ficha", "Número").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                    ficha.Columns.Add("año_ficha", "Año")
                    ficha.Columns.Add("censo", "Censo")
                End Sub)

            Return settings

        End Function

    End Class

    Public NotInheritable Class exportPvgConsolidadoPago

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
            settings.OptionsPager.RowsPerPage = 20
            settings.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Auto
 

            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False

            settings.EnableCallbackAnimation = True
            settings.EnablePagingCallbackAnimation = True

            settings.OptionsView.DataHeadersDisplayMode = PivotDataHeadersDisplayMode.Popup
            settings.OptionsView.EnableFilterControlPopupMenuScrolling = True
            settings.OptionsView.RowTreeOffset = 3
            settings.OptionsView.RowTotalsLocation = PivotRowTotalsLocation.Far

            settings.OptionsData.DataFieldUnboundExpressionMode = DataFieldUnboundExpressionMode.UseSummaryValues
            settings.ClientSideEvents.EndCallback = "UpdateChart"

#Region "campos del consolidado"
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 0
                    field.Caption = "Hogares No Pagados"
                    field.FieldName = "hogares_no_pagados"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 1
                    field.Caption = "Monto No Pagado"
                    field.FieldName = "monto_no_pagado"
                    field.CellFormat.FormatString = "c"
                    field.CellFormat.FormatType = FormatType.Custom
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 2
                    field.Caption = "Elegibilidad"
                    field.FieldName = "nombre_elegibilidad"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 3
                    field.Caption = "Componente"
                    field.FieldName = "nombre_componente"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 4
                    field.Caption = "Pagador"
                    field.FieldName = "nombre_pagador"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 5
                    field.Caption = "Fondo"
                    field.FieldName = "nombre_fondo"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 6
                    field.Caption = "Mecanismo"
                    field.FieldName = "tipo_pago"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 7
                    field.Caption = "Esquema"
                    field.FieldName = "nombre_esquema"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 8
                    field.Caption = "Monto"
                    field.FieldName = "monto_total"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 9
                    field.Caption = "Municipio"
                    field.FieldName = "desc_municipio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 10
                    field.Caption = "Aldea"
                    field.FieldName = "desc_aldea"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 11
                    field.Caption = "Caserío"
                    field.FieldName = "desc_caserio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.RowArea
                    field.Index = 0
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
                    field.Caption = "% Ejecución"
                    field.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                    field.UnboundExpression = String.Format("([{0}] * 100)/[{1}]", settings.Fields("hogares_pagados").ID, settings.Fields("hogares_programados").ID)
                    field.UnboundFieldName = "cumplimiento"
                    field.CellFormat.FormatType = FormatType.Numeric
                    field.CellFormat.FormatString = "n2"
                End Sub)

#End Region

            Return settings

        End Function

    End Class

    Public NotInheritable Class exportPvgComparacionPlanillas

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportPivotGridSettings(ByVal pago1 As String, ByVal pago2 As String) As PivotGridSettings

            Get
                exportSettings = CreateExportPvgComparacionPlanillas(pago1, pago2)
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgComparacionPlanillas(ByVal pago1 As String, ByVal pago2 As String) As PivotGridSettings

            Dim settings As New PivotGridSettings()

            settings.Name = "pvgComparacionPlanillas"
            settings.Width = 1000 'Unit.Percentage(100)
            settings.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Auto
            settings.OptionsPager.RowsPerPage = 20

            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False
            settings.OptionsView.DataHeadersDisplayMode = PivotDataHeadersDisplayMode.Popup

            settings.ClientSideEvents.EndCallback = "UpdateChart"

#Region "campos"
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.RowArea
                    field.Index = 2
                    field.Caption = "Departamento"
                    field.FieldName = "desc_departamento"
                    field.Width = 50
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 3
                    field.Caption = "Municipio"
                    field.FieldName = "desc_municipio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 4
                    field.Caption = "Aldea"
                    field.FieldName = "desc_aldea"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 5
                    field.Caption = "Caserío"
                    field.FieldName = "desc_caserio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 0
                    field.Caption = pago1
                    field.FieldName = "total_planilla_1"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 1
                    field.Caption = pago2
                    field.FieldName = "total_planilla_2"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 2
                    field.Caption = "Arrastre"
                    field.FieldName = "arrastre"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 3
                    field.Caption = "Altas"
                    field.FieldName = "altas"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 4
                    field.Caption = "Bajas"
                    field.FieldName = "bajas"
                End Sub)
#End Region

            Return settings

        End Function

    End Class

    Public NotInheritable Class exportPvgRazonCaidaHogares

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportPivotGridSettings() As PivotGridSettings

            Get
                exportSettings = CreateExportPvgRazonCaidaHogares()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgRazonCaidaHogares() As PivotGridSettings

            Dim settings As New PivotGridSettings()

            settings.Name = "pvgRazonCaida"
            settings.Width = Unit.Percentage(100)
            settings.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Auto
            settings.OptionsPager.RowsPerPage = 20

            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False

            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    'field.Index = 0
                    field.Caption = "Departamento"
                    field.FieldName = "desc_departamento"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    'field.Index = 0
                    field.Caption = "Municipio"
                    field.FieldName = "desc_municipio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    'field.Index = 1
                    field.Caption = "Aldea"
                    field.FieldName = "desc_aldea"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    'field.Index = 2
                    field.Caption = "Caserío"
                    field.FieldName = "desc_caserio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.RowArea
                    'field.Index = 1
                    field.Caption = "Razón Caida"
                    field.FieldName = "proyeccion_corta"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    'field.Index = 0
                    field.Caption = "Total Hogares"
                    field.FieldName = "total"
                End Sub)

            Return settings

        End Function

    End Class

    Public NotInheritable Class exportPvgElegiblesControProgramado

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportPivotGridSettings() As PivotGridSettings

            Get
                exportSettings = CreateExportPvgElegibleContraProgramado()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgElegibleContraProgramado() As PivotGridSettings

            Dim settings As New PivotGridSettings()

            settings.Name = "pvgComparacionPlanillas"
            settings.Width = Unit.Percentage(100)


            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False

            settings.EnableCallbackAnimation = True
            settings.EnablePagingCallbackAnimation = True

            settings.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Auto
            settings.OptionsView.DataHeadersDisplayMode = PivotDataHeadersDisplayMode.Popup
            settings.OptionsView.DataHeadersPopupMinCount = 6
            settings.OptionsView.DataHeadersPopupMaxColumnCount = 6
            settings.OptionsView.RowTreeOffset = 2
            settings.OptionsView.RowTotalsLocation = PivotRowTotalsLocation.Far

            settings.OptionsData.DataFieldUnboundExpressionMode = DataFieldUnboundExpressionMode.UseSummaryValues

            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.RowArea
                    field.Index = 0
                    field.Caption = "Departamento"
                    field.FieldName = "desc_departamento"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Caption = "Municipio"
                    field.FieldName = "desc_municipio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Caption = "Aldea"
                    field.FieldName = "desc_aldea"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Caption = "Caserío"
                    field.FieldName = "desc_caserio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 0
                    field.Caption = "Total Hogares"
                    field.FieldName = "total_hogares"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 1
                    field.Caption = "Hogares Evaluados"
                    field.FieldName = "total_hogares_evaluados"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 2
                    field.Caption = "Hogares Elegibles"
                    field.FieldName = "total_hogares_elegibles"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 3
                    field.Caption = "Hogares Programados"
                    field.FieldName = "total_hogares_programados"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 4
                    field.Caption = "Programados / Elegibles"
                    field.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                    field.UnboundExpression = String.Format("([{0}] * 100)/[{1}]", settings.Fields("total_hogares_programados").ID, settings.Fields("total_hogares_elegibles").ID)
                    field.UnboundFieldName = "programados_/_elegibles_"
                    field.CellFormat.FormatType = FormatType.Numeric
                    field.CellFormat.FormatString = "n2"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 5
                    field.Caption = "Elegibles Educación"
                    field.FieldName = "hogares_elegibles_educacion"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 6
                    field.Caption = "Programados Educación"
                    field.FieldName = "hogares_programados_educacion"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 7
                    field.Caption = "progra_edu / elegi_edu"
                    field.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                    field.UnboundExpression = String.Format("([{0}] * 100)/[{1}]", settings.Fields("hogares_programados_educacion").ID, settings.Fields("hogares_elegibles_educacion").ID)
                    field.UnboundFieldName = "programados_/_elegibles_educacion"
                    field.CellFormat.FormatType = FormatType.Numeric
                    field.CellFormat.FormatString = "n2"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 8
                    field.Caption = "Elegibles Educación y Salud"
                    field.FieldName = "hogares_elegibles_educacion_salud"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 9
                    field.Caption = "Programados Educación y Salud"
                    field.FieldName = "hogares_programados_educacion_salud"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 10
                    field.Caption = "progra_edu_sal / elegi_edu_sal"
                    field.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                    field.UnboundExpression = String.Format("([{0}] * 100)/[{1}]", settings.Fields("hogares_programados_educacion_salud").ID, settings.Fields("hogares_elegibles_educacion_salud").ID)
                    field.UnboundFieldName = "programados_/_elegibles_educacion_salud"
                    field.CellFormat.FormatType = FormatType.Numeric
                    field.CellFormat.FormatString = "n2"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 11
                    field.Caption = "Elegible Salud"
                    field.FieldName = "hogares_elegibles_salud"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 12
                    field.Caption = "Programados Salud"
                    field.FieldName = "hogares_programados_salud"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 13
                    field.Caption = "progra_sal / elegi_sal"
                    field.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                    field.UnboundExpression = String.Format("([{0}] * 100)/[{1}]", settings.Fields("hogares_programados_salud").ID, settings.Fields("hogares_elegibles_salud").ID)
                    field.UnboundFieldName = "programados_/_elegibles_salud"
                    field.CellFormat.FormatType = FormatType.Numeric
                    field.CellFormat.FormatString = "n2"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 14
                    field.Caption = "Elegibles Primer Pago"
                    field.FieldName = "hogares_elegibles_primer_pago"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 15
                    field.Caption = "Programados Primer Pago"
                    field.FieldName = "hogares_programados_primer_pago"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 16
                    field.Caption = "progra_prime / elegi_prime"
                    field.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                    field.UnboundExpression = String.Format("([{0}] * 100)/[{1}]", settings.Fields("hogares_programados_primer_pago").ID, settings.Fields("hogares_elegibles_primer_pago").ID)
                    field.UnboundFieldName = "programados_/_elegibles_primer_pago"
                    field.CellFormat.FormatType = FormatType.Numeric
                    field.CellFormat.FormatString = "n2"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 17
                    field.Caption = "Elegibles Repago"
                    field.FieldName = "hogares_elegibles_repago"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 18
                    field.Caption = "Programados Repago"
                    field.FieldName = "hogares_programados_repago"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 19
                    field.Caption = "progra_repa / elegi_repa"
                    field.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                    field.UnboundExpression = String.Format("([{0}] * 100)/[{1}]", settings.Fields("hogares_programados_repago").ID, settings.Fields("hogares_elegibles_repago").ID)
                    field.UnboundFieldName = "programados_/_elegibles_repago"
                    field.CellFormat.FormatType = FormatType.Numeric
                    field.CellFormat.FormatString = "n2"
                End Sub)
            Return settings

        End Function

    End Class

    Public NotInheritable Class exportPvgRazonExclusionHogares

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportPivotGridSettings() As PivotGridSettings

            Get
                exportSettings = CreateExportPvgRazonExclusionHogares()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgRazonExclusionHogares() As PivotGridSettings

            Dim settings As New PivotGridSettings()

            settings.Name = "pvgComparacionPlanillas"
            settings.Width = Unit.Percentage(100)
            settings.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Auto

            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False

            settings.EnableCallbackAnimation = True
            settings.EnablePagingCallbackAnimation = True

            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.RowArea
                    field.Index = 0
                    field.Caption = "Departamento"
                    field.FieldName = "desc_departamento"
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
                    field.Area = PivotArea.FilterArea
                    field.Index = 2
                    field.Caption = "Aldea"
                    field.FieldName = "desc_aldea"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 3
                    field.Caption = "Caserío"
                    field.FieldName = "desc_caserio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.ColumnArea
                    field.Index = 0
                    field.Caption = "Proyección Corta"
                    field.FieldName = "proyeccion_corta"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 0
                    field.Caption = "Proyección"
                    field.FieldName = "proyeccion"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 0
                    field.Caption = "Hogares"
                    field.FieldName = "cant_hogares"
                End Sub)


            Return settings

        End Function

    End Class

    Public NotInheritable Class exportPvgFichasPorCensoAnoEnPago

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property exportPvgSettings() As PivotGridSettings

            Get
                exportSettings = CreateExportPvgFichasPorCensoAnoEnPago()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgFichasPorCensoAnoEnPago() As PivotGridSettings

            Dim settings As New PivotGridSettings()

            settings.Name = "pvgFichasPorCensoAno"
            settings.Width = Unit.Percentage(100)
            settings.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Auto

            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False

            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.ColumnArea
                    field.Index = 0
                    field.Caption = "Año"
                    field.FieldName = "año_ficha"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.RowArea
                    field.Index = 1
                    field.Caption = "Censo"
                    field.FieldName = "censo_ficha"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 0
                    field.Caption = "Fichas"
                    field.FieldName = "fichas"
                End Sub)

            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.RowArea
                    field.Index = 0
                    field.Caption = "Departamento"
                    field.FieldName = "desc_departamento"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    'field.Index = 3
                    field.Caption = "Municipio"
                    field.FieldName = "desc_municipio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    'field.Index = 4
                    field.Caption = "Aldea"
                    field.FieldName = "desc_aldea"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    'field.Index = 5
                    field.Caption = "Caserio"
                    field.FieldName = "desc_caserio"
                End Sub)

            Return settings

        End Function

    End Class

    Public NotInheritable Class exportPvgNinosPagadosPorCiclo

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property exportPvgSettings(variante As Integer) As PivotGridSettings

            Get
                exportSettings = CreateExportPvgNinosPagadosPorCiclo(variante)
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgNinosPagadosPorCiclo(variante As Integer) As PivotGridSettings

            Dim settings As New PivotGridSettings()

            settings.Name = "pvgNinosPagadosPorCiclo"
            settings.Width = 1000
            settings.OptionsPager.RowsPerPage = 20
            settings.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Auto

            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False


            'settings.OptionsView.ShowRowGrandTotals = False
            'settings.OptionsView.ShowColumnGrandTotalHeader = False
            'settings.OptionsView.ShowRowGrandTotalHeader = False
            'settings.OptionsView.ShowRowTotals = False

            settings.EnableCallbackAnimation = True
            settings.EnablePagingCallbackAnimation = True
            settings.OptionsView.DataHeadersDisplayMode = PivotDataHeadersDisplayMode.Popup
            If (variante = 1) Then
                settings.Fields.Add(
                     Sub(field)
                         field.Area = PivotArea.RowArea
                         field.Index = 0
                         field.Caption = "Ciclo"
                         field.FieldName = "descripcion_ciclo"
                     End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 0
                        field.Caption = "Total"
                        field.FieldName = "total_niños"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 1
                        field.Caption = "Cumpliendo"
                        field.FieldName = "total_niños_cumpliendo"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 2
                        field.Caption = "Apercibidos"
                        field.FieldName = "total_niños_apercibidos"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 3
                        field.Caption = "No Cumpliendo"
                        field.FieldName = "total_niños_no_cumpliendo"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 4
                        field.Caption = "Programados"
                        field.FieldName = "total_niños_programados"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 5
                        field.Caption = "Pagados"
                        field.FieldName = "total_niños_pagados"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 6
                        field.Caption = "No Pagados"
                        field.FieldName = "total_niños_no_pagados"
                    End Sub)

                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.RowArea
                        field.Index = 0
                        field.Caption = "Departamento"
                        field.FieldName = "desc_departamento"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.FilterArea
                        field.Caption = "Municipio"
                        field.FieldName = "desc_municipio"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.FilterArea
                        field.Caption = "Aldea"
                        field.FieldName = "desc_aldea"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.FilterArea
                        field.Caption = "Caserio"
                        field.FieldName = "desc_caserio"
                    End Sub)


            ElseIf variante = 2 Then


                settings.Fields.Add(
                     Sub(field)
                         field.Area = PivotArea.ColumnArea
                         field.Index = 1
                         field.Caption = "Ciclo"
                         field.FieldName = "descripcion_ciclo"
                     End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 0
                        field.Caption = "Total"
                        field.FieldName = "total_niños"
                    End Sub)

                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.RowArea
                        field.Index = 0
                        field.Caption = "Departamento"
                        field.FieldName = "desc_departamento"
                    End Sub)


            ElseIf variante = 3 Then


                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 0
                        field.Caption = "Total"
                        field.FieldName = "total_niños"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 1
                        field.Caption = "Cumpliendo"
                        field.FieldName = "total_niños_cumpliendo"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 2
                        field.Caption = "Apercibidos"
                        field.FieldName = "total_niños_apercibidos"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 3
                        field.Caption = "No Cumpliendo"
                        field.FieldName = "total_niños_no_cumpliendo"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 4
                        field.Caption = "Programados"
                        field.FieldName = "total_niños_programados"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 5
                        field.Caption = "Pagados"
                        field.FieldName = "total_niños_pagados"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 6
                        field.Caption = "No Pagados"
                        field.FieldName = "total_niños_no_pagados"
                    End Sub)

                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.RowArea
                        field.Index = 0
                        field.Caption = "Departamento"
                        field.FieldName = "desc_departamento"
                    End Sub)


            ElseIf variante = 4 Then


                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.ColumnArea
                        field.Index = 0
                        field.Caption = "Ciclo"
                        field.FieldName = "descripcion_ciclo"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 0
                        field.Caption = "Total"
                        field.FieldName = "total_niños"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 1
                        field.Caption = "Cumpliendo"
                        field.FieldName = "total_niños_cumpliendo"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 2
                        field.Caption = "Apercibidos"
                        field.FieldName = "total_niños_apercibidos"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 3
                        field.Caption = "No Cumpliendo"
                        field.FieldName = "total_niños_no_cumpliendo"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 4
                        field.Caption = "Programados"
                        field.FieldName = "total_niños_programados"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 5
                        field.Caption = "Pagados"
                        field.FieldName = "total_niños_pagados"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.DataArea
                        field.Index = 6
                        field.Caption = "No Pagados"
                        field.FieldName = "total_niños_no_pagados"
                    End Sub)

                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.RowArea
                        field.Index = 0
                        field.Caption = "Departamento"
                        field.FieldName = "desc_departamento"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.FilterArea
                        field.Caption = "Municipio"
                        field.FieldName = "desc_municipio"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.FilterArea
                        field.Caption = "Aldea"
                        field.FieldName = "desc_aldea"
                    End Sub)
                settings.Fields.Add(
                    Sub(field)
                        field.Area = PivotArea.FilterArea
                        field.Caption = "Caserio"
                        field.FieldName = "desc_caserio"
                    End Sub)
            End If


            Return settings

        End Function

    End Class

    Public NotInheritable Class exportGdvListadoNinosPago

        Private Shared exportSettings As GridViewSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportGridViewSettings() As GridViewSettings

            Get
                exportSettings = CreateExportGdvListadoNinosPago()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportGdvListadoNinosPago() As GridViewSettings

            Dim settings As New GridViewSettings()

            settings.Name = "gvMaestroListadoParticipantes"

            settings.SettingsPager.Position = PagerPosition.Bottom
            settings.SettingsPager.FirstPageButton.Visible = True
            settings.SettingsPager.LastPageButton.Visible = True
            settings.SettingsPager.PageSizeItemSettings.Visible = True
            settings.SettingsPager.PageSizeItemSettings.Items = New String() {"10", "20", "50"}
            settings.SettingsBehavior.AllowGroup = True
            settings.SettingsBehavior.AllowSort = True
            settings.Settings.ShowGroupPanel = True

            settings.EnableCallbackAnimation = True
            settings.EnablePagingCallbackAnimation = True
            settings.EnableCallbackCompression = True

            settings.Settings.ShowHeaderFilterButton = True
            settings.Settings.ShowFilterRow = True
            settings.SettingsPopup.HeaderFilter.Height = 200

            settings.SettingsExport.FileName = "LISTADO NIÑOS CON CORRESPONSABILIDAD EN PLANILLA"
            settings.Caption = "LISTADO NIÑOS CON CORRESPONSABILIDAD EN PLANILLA"

            'necesarios para el detalle
            'settings.KeyFieldName = "identidad_titular"
            'settings.SettingsDetail.AllowOnlyOneMasterRowExpanded = True
            'settings.SettingsDetail.ShowDetailRow = True
            'settings.SettingsDetail.ExportMode = GridViewDetailExportMode.Expanded

            settings.Columns.AddBand(
                Sub(ag)
                    ag.Caption = "Área Geográfica"
                    ag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    ag.Columns.Add("desc_departamento", "Departamento").Settings.HeaderFilterMode = HeaderFilterMode.CheckedList
                    ag.Columns.Add("desc_municipio", "Municipio").Settings.HeaderFilterMode = HeaderFilterMode.CheckedList
                    ag.Columns.Add("desc_aldea", "Aldea").Settings.HeaderFilterMode = HeaderFilterMode.CheckedList

                    settings.Columns.Add("cod_departamento").Visible = False
                    settings.Columns.Add("cod_municipio").Visible = False
                    settings.Columns.Add("cod_aldea").Visible = False
                End Sub)
            'settings.Columns.Add("cod_hogar", "Código Hogar").Settings.HeaderFilterMode = HeaderFilterMode.CheckedList
            settings.Columns.AddBand(
                Sub(tit)
                    tit.Caption = "Niño"
                    tit.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    tit.Columns.Add("cod_hogar", "Código Hogar").Settings.AllowHeaderFilter = DefaultBoolean.False
                    tit.Columns.Add("identidad", "Nro. Identidad").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                    tit.Columns.Add("nombre_persona", "Nombre Completo").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                End Sub)
            settings.Columns.AddBand(
                Sub(eleg)
                    eleg.Caption = "Elegibilidad"
                    eleg.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    eleg.Columns.Add("elegibilidad", "Elegibilidad")
                    eleg.Columns.Add("desc_nivel_elegibilidad", "Nivel Elegibilidad")
                End Sub)
            settings.Columns.AddBand(
                Sub(corr)
                    corr.Caption = "Corresponsabilidad"
                    corr.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    corr.Columns.Add("desc_corresponsabilidad", "Descripción") '.Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                    corr.Columns.Add("estado_corresponsabilidad", "Estado") '.Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                End Sub)
            settings.Columns.Add("pagado", "Pagado")

            Return settings

        End Function

    End Class

    Public NotInheritable Class exportPvgNucleoFamiliarMuestra

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property exportPvgSettings() As PivotGridSettings

            Get
                exportSettings = CreateExportPvgNucleoFamiliarMuestra()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgNucleoFamiliarMuestra() As PivotGridSettings

            Dim settings As New PivotGridSettings()

            settings.Name = "gdvNucleoFamiliarMuestra"

            settings.OptionsView.ShowRowGrandTotals = False
            settings.OptionsView.ShowColumnGrandTotalHeader = False
            settings.OptionsView.ShowRowGrandTotalHeader = False

            settings.Width = Unit.Percentage(170)
            settings.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Auto

            settings.EnableCallbackAnimation = True
            settings.EnablePagingCallbackAnimation = True

            settings.OptionsView.ShowRowGrandTotals = False
            settings.OptionsView.ShowColumnGrandTotalHeader = False
            settings.OptionsView.ShowRowGrandTotalHeader = False
            settings.OptionsView.ShowRowTotals = False

            settings.OptionsPager.RowsPerPage = 15
            settings.OptionsView.DataHeadersDisplayMode = PivotDataHeadersDisplayMode.Popup
            settings.OptionsView.DataHeadersPopupMinCount = 6
            settings.OptionsView.DataHeadersPopupMaxColumnCount = 6
            settings.OptionsView.RowTreeOffset = 2
            settings.OptionsView.RowTotalsLocation = PivotRowTotalsLocation.Far

            'Columnas para rowarea
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "cod_hogar"
                    field.Caption = "Código Hogar"
                    field.Area = PivotArea.RowArea
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "cod_persona"
                    field.Caption = "Código Persona"
                    field.Area = PivotArea.RowArea
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "identidad"
                    field.Caption = "Identidad"
                    field.Area = PivotArea.RowArea
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "nombre"
                    field.Caption = "Nombre"
                    field.Area = PivotArea.RowArea
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "titular"
                    field.Caption = "Titular"
                    field.Area = PivotArea.RowArea
                End Sub)

            'Columnas en dataarea
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "cod_departamento"
                    field.Caption = "Código Departamento"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "desc_departamento"
                    field.Caption = "Nombre Departamento"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "cod_municipio"
                    field.Caption = "Código Municipio"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "desc_municipio"
                    field.Caption = "Nombre Municipio"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "cod_aldea"
                    field.Caption = "Código Aldea"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "desc_aldea"
                    field.Caption = "Nombre Aldea"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "cod_caserio"
                    field.Caption = "Código Caserío"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "desc_caserio"
                    field.Caption = "Nombre Caserío"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "año_ficha"
                    field.Caption = "Año Ficha"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "censo_ficha"
                    field.Caption = "Censo Ficha"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "numero_ficha"
                    field.Caption = "Número Ficha"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "fecha_emision_planilla"
                    field.Caption = "Fecha Emisión Planilla"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "umbral"
                    field.Caption = "Umbral"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "nombre_fondo"
                    field.Caption = "Nombre Fondo"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "nombre_pagador"
                    field.Caption = "Nombre Pagador"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "Corresponsabilidad_Det"
                    field.Caption = "Detalle Corresponsabilidad"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "Nombre_Bono_Acum"
                    field.Caption = "Nombre Bono Acum"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "proyeccion"
                    field.Caption = ""
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "nombre_corresponsabilidad"
                    field.Caption = "Nombre Corresponsabilidad"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "cod_titular"
                    field.Caption = "Código Titular"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "referencia"
                    field.Caption = "Referencia"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "Periodo_Esquema"
                    field.Caption = "Período Esquema"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "Monto"
                    field.Caption = "Monto"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "Cod_CentroSace"
                    field.Caption = "Código Centro Sace"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "Centro_Educativo"
                    field.Caption = "Centro Educativo"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "Grado"
                    field.Caption = "Grado"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "Año"
                    field.Caption = "Año"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "CodigoCentroDeSaludRENPI"
                    field.Caption = "Código Centro Salud RENPI"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    'field.AreaIndex = 1
                    field.FieldName = "DescripcionCentroDeSaludRENPI"
                    field.Caption = "Desc Centro Salud RENPI"
                    field.Area = PivotArea.DataArea
                    field.SummaryType = PivotSummaryType.Min
                End Sub)

            Return settings

        End Function

    End Class

    Public NotInheritable Class exportPvgBasePagado

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property exportPvgSettings() As PivotGridSettings

            Get
                exportSettings = CreateExportPvgBasePagado()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgBasePagado() As PivotGridSettings

            Dim setting As New PivotGridSettings()

            setting.Name = "pivot"
            'setting.CallbackRouteValues = New With {Key .Controller = "PlanillasPago", Key .Action = "partialReporteProgramado"}

            setting.OptionsView.ShowRowGrandTotals = False
            setting.OptionsView.ShowColumnGrandTotalHeader = False
            setting.OptionsView.ShowRowGrandTotalHeader = False

            setting.Width = Unit.Percentage(170)
            setting.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Auto

            setting.EnableCallbackAnimation = True
            setting.EnablePagingCallbackAnimation = True

            setting.OptionsPager.RowsPerPage = 15
            setting.OptionsView.DataHeadersDisplayMode = PivotDataHeadersDisplayMode.Popup
            setting.OptionsView.DataHeadersPopupMinCount = 6
            setting.OptionsView.DataHeadersPopupMaxColumnCount = 6
            setting.OptionsView.RowTreeOffset = 2
            setting.OptionsView.RowTotalsLocation = PivotRowTotalsLocation.Far
            setting.Fields.Add(
                Sub(field)
                    field.AreaIndex = 1
                    field.FieldName = "bono"
                    field.Caption = "PTI_BONO"
                    field.Area = PivotArea.DataArea
                End Sub)
            setting.Fields.Add(
                Sub(field)
                    field.AreaIndex = 2
                    field.FieldName = "cod_hogar"
                    field.Caption = "HOGAR"
                    field.Area = PivotArea.DataArea
                End Sub)
            setting.Fields.Add(
                Sub(field)
                    field.AreaIndex = 3
                    field.FieldName = "año"
                    field.Caption = "AÑO"
                    field.Area = PivotArea.DataArea
                End Sub)
            setting.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.AreaIndex = 4
                    field.FieldName = "nombre_fondo"
                    field.Caption = "PROYECTO"
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            setting.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.AreaIndex = 5
                    field.FieldName = "nombre_pagador"
                    field.Caption = "PAGADOR"
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            setting.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.AreaIndex = 7
                    field.FieldName = "esquema"
                    field.Caption = "PERIODO"
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            setting.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.AreaIndex = 8
                    field.FieldName = "fecha_pago"
                    field.Caption = "FECHA_PAGO"
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            setting.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.RowArea
                    field.AreaIndex = 0
                    field.FieldName = "depto"
                    field.Caption = "DEPARTAMENTOS"
                    field.TotalsVisibility = PivotTotalsVisibility.None
                End Sub)
            setting.Fields.Add(
                Sub(field)
                    field.AreaIndex = 1
                    field.FieldName = "muni"
                    field.Caption = "MUNICIPIO"
                    field.TotalsVisibility = PivotTotalsVisibility.None
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            setting.Fields.Add(
                Sub(field)
                    field.AreaIndex = 2
                    field.FieldName = "aldea"
                    field.Caption = "MUNICIPIOS"
                    field.TotalsVisibility = PivotTotalsVisibility.None
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            setting.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.RowArea
                    field.AreaIndex = 3
                    field.FieldName = "nombre_titular"
                    field.Caption = "NOMBRE DEL TITULAR  "
                End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.AreaIndex = 4
            '        field.FieldName = "identidad_titular"
            '        field.Caption = "NÚMERO DE IDENTIDAD"
            '        field.SummaryType = PivotSummaryType.Min
            '    End Sub)
            setting.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.AreaIndex = 11
                    field.FieldName = "pagina"
                    field.Caption = "PAGINA"
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            setting.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.AreaIndex = 12
                    field.FieldName = "linea"
                    field.Caption = "LINEA"
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 13
            '        field.FieldName = "referencia"
            '        field.Caption = "REFERENCIA"
            '    End Sub)
            setting.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.AreaIndex = 14
                    field.FieldName = "HOG_PROG_ESC_corresp"
                    field.Caption = "HOG_PROG_ESC_CORRESP"
                End Sub)
            setting.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.AreaIndex = 15
                    field.FieldName = "HOG_PROG_ESC_acum"
                    field.Caption = "HOG_PROG_ESC_ACUM"
                End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 16
            '        field.FieldName = "HOG_PROG_ESC"
            '        field.Caption = "HOG_PROG_ESC"
            '    End Sub)
            setting.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.AreaIndex = 17
                    field.FieldName = "HOG_PROG_SAL_corresp"
                    field.Caption = "HOG_PROG_SAL_CORRESP"
                End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 18
            '        field.FieldName = "HOG_PROG_SAL_acum"
            '        field.Caption = "HOG_PROG_SAL_ACUM"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 19
            '        field.FieldName = "HOG_PROG_SAL"
            '        field.Caption = "HOG_PROG_SAL"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 20
            '        field.FieldName = "HOG_PROG_TOT"
            '        field.Caption = "HOG_PROG_TOT"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 21
            '        field.FieldName = "MONTO_PROG_ESC_corresp"
            '        field.Caption = "MONTO_PROG_ESC_CORRESP"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 22
            '        field.FieldName = "MONTO_PROG_ESC_acum"
            '        field.Caption = "MONTO_PROG_ESC_ACUM"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 23
            '        field.FieldName = "MONTO_PROG_ESC"
            '        field.Caption = "MONTO_PROG_ESC"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 24
            '        field.FieldName = "MONTO_PROG_SAL_corresp"
            '        field.Caption = "MONTO_PROG_SAL_CORRESP"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 25
            '        field.FieldName = "MONTO_PROG_SAL_acum"
            '        field.Caption = "MONTO_PROG_SAL_ACUM"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 26
            '        field.FieldName = "MONTO_PROG_SAL"
            '        field.Caption = "MONTO_PROG_SAL"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 27
            '        field.FieldName = "MONTO_PROG_TOT"
            '        field.Caption = "MONTO_PROG_TOT"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 28
            '        field.FieldName = "HOG_PAG_ESC_corresp"
            '        field.Caption = "HOG_PAG_ESC_corresp"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 29
            '        field.FieldName = "HOG_PAG_ESC_acum"
            '        field.Caption = "HOG_PAG_ESC_ACUM"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 30
            '        field.FieldName = "HOG_PAG_ESC"
            '        field.Caption = "HOG_PAG_ESC"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 31
            '        field.FieldName = "HOG_PAG_SAL_corresp"
            '        field.Caption = "HOG_PAG_SAL_CORRESP"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 32
            '        field.FieldName = "HOG_PAG_SAL_acum"
            '        field.Caption = "HOG_PAG_SAL_ACUM"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 33
            '        field.FieldName = "HOG_PAG_SAL"
            '        field.Caption = "HOG_PAG_SAL"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 34
            '        field.FieldName = "HOG_PAG_TOT"
            '        field.Caption = "HOG_PAG_TOT"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 35
            '        field.FieldName = "MONTO_PAG_ESC_corresp"
            '        field.Caption = "MONTO_PAG_ESC_CORRESP"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 36
            '        field.FieldName = "MONTO_PAG_ESC_acum"
            '        field.Caption = "MONTO_PAG_ESC_ACUM"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 37
            '        field.FieldName = "MONTO_PAG_ESC"
            '        field.Caption = "MONTO_PAG_ESC"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 38
            '        field.FieldName = "MONTO_PAG_SAL_corresp"
            '        field.Caption = "MONTO_PAG_SAL_CORRESP"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 39
            '        field.FieldName = "MONTO_PAG_SAL_acum"
            '        field.Caption = "MONTO_PROG_SAL_ACUM"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 40
            '        field.FieldName = "MONTO_PAG_SAL"
            '        field.Caption = "MONTO_PAG_SAL"
            '    End Sub)
            'setting.Fields.Add(
            '    Sub(field)
            '        field.Area = PivotArea.DataArea
            '        field.AreaIndex = 41
            '        field.FieldName = "MONTO_PAG_TOT"
            '        field.Caption = "MONTO_PAG_TOT"
            '    End Sub)

            Return setting

        End Function

    End Class

    Public NotInheritable Class exportPvgHogaresPagados

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property exportPvgSettings(ByVal tipo As String, ByVal strCampos As String) As PivotGridSettings

            Get
                exportSettings = CreateExportPvgHogaresPagados(tipo, strCampos)
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgHogaresPagados(ByVal tipo As String, ByVal strCampos As String) As PivotGridSettings

            Dim settings As New PivotGridSettings()
            Dim campos() As String = strCampos.Split(",")

            settings.Name = "pvgHogaresPagados"
            settings.Width = Unit.Percentage(100)
            settings.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Auto
            settings.OptionsPager.RowsPerPage = 20

            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False

            Select Case tipo
                Case "Anual"

                    settings.Fields.Add(
                        Sub(field)
                            field.Area = PivotArea.ColumnArea
                            field.Index = 0
                            field.Caption = "Año"
                            field.FieldName = "año_pago"
                        End Sub)
                Case "Semestral"

                    settings.Fields.Add(
                        Sub(field)
                            field.Area = PivotArea.ColumnArea
                            field.Index = 0
                            field.Caption = "Año"
                            field.FieldName = "año_pago"
                        End Sub)
                    settings.Fields.Add(
                        Sub(field)
                            field.Area = PivotArea.ColumnArea
                            field.Index = 1
                            field.Caption = "Semestre"
                            field.FieldName = "semestre"
                        End Sub)

            End Select

            For Each campo In campos

                Select Case campo
                    Case "Área Geografica"

                        settings.Fields.Add(
                        Sub(field)
                            field.Area = PivotArea.RowArea
                            field.Index = 2
                            field.Caption = "Departamento"
                            field.FieldName = "desc_departamento"
                        End Sub)
                        settings.Fields.Add(
                        Sub(field)
                            field.Area = PivotArea.FilterArea
                            field.Index = 3
                            field.Caption = "Municipio"
                            field.FieldName = "desc_municipio"
                        End Sub)
                        settings.Fields.Add(
                        Sub(field)
                            field.Area = PivotArea.FilterArea
                            field.Index = 4
                            field.Caption = "Aldea"
                            field.FieldName = "desc_aldea"
                        End Sub)
                        settings.Fields.Add(
                        Sub(field)
                            field.Area = PivotArea.FilterArea
                            field.Index = 5
                            field.Caption = "Caserio"
                            field.FieldName = "desc_caserio"
                        End Sub)

                    Case "Fondo"

                        settings.Fields.Add(
                        Sub(field)
                            field.Area = PivotArea.RowArea
                            field.Index = 0
                            field.Caption = "Fondo"
                            field.FieldName = "nombre_fondo"
                        End Sub)

                    Case "Pagador"
                        settings.Fields.Add(
                        Sub(field)
                            field.Area = PivotArea.RowArea
                            field.Index = 1
                            field.Caption = "Pagador"
                            field.FieldName = "nombre_pagador"
                        End Sub)
                End Select

            Next

            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 0
                    field.Caption = "Hogares Pagados"
                    field.FieldName = "hogares_pagados"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 1
                    field.Caption = "Monto Pagado"
                    field.FieldName = "monto_pagado"
                    field.CellFormat.FormatString = "c"
                    field.CellFormat.FormatType = FormatType.Custom
                End Sub)

            Return settings

        End Function

    End Class

    Public NotInheritable Class exportGdvECCAI

        Private Shared exportSettings As GridViewSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportGridViewSettings() As GridViewSettings

            Get
                exportSettings = CreateExportGdvECCAI()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportGdvECCAI() As GridViewSettings

            Dim settings As New GridViewSettings()

            settings.Name = "gdvECCAI"

            settings.SettingsPager.Position = PagerPosition.Bottom
            settings.SettingsPager.FirstPageButton.Visible = True
            settings.SettingsPager.LastPageButton.Visible = True
            settings.SettingsPager.PageSizeItemSettings.Visible = True
            settings.SettingsPager.PageSizeItemSettings.Items = New String() {"10", "20", "50"}
            settings.SettingsBehavior.AllowGroup = True
            settings.SettingsBehavior.AllowSort = True
            settings.Settings.ShowGroupPanel = True

            settings.EnableCallbackAnimation = True
            settings.EnablePagingCallbackAnimation = True
            settings.EnableCallbackCompression = True

            settings.Settings.ShowHeaderFilterButton = True
            settings.Settings.ShowFilterRow = True
            settings.SettingsPopup.HeaderFilter.Height = 200

            settings.SettingsExport.FileName = "Estado Cuenta cumplimiento apercibimiento e incumplimiento"


            settings.Caption = "Estado Cuenta cumplimiento apercibimiento e incumplimiento"

            'settings.Styles.FixedColumn.BackColor = System.Drawing.Color.LightYellow
            settings.Settings.HorizontalScrollBarMode = ScrollBarMode.Visible
            settings.Width = Unit.Pixel(1200)

            settings.Columns.AddBand(
            Sub(band)
                band.Caption = "Área Geográfica"
                band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                'band.FixedStyle = GridViewColumnFixedStyle.Left
                band.Columns.Add("desc_departamento", "Departamento").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("desc_municipio", "Municipio").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("desc_aldea", "Aldea").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
            End Sub)
            settings.Columns.AddBand(
            Sub(band)
                band.Caption = "Titular"
                band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                band.Columns.Add("cod_titular", "Código").Settings.AllowHeaderFilter = DefaultBoolean.False
                band.Columns.Add("identidad_titular", "Nro. Identidad").Settings.AllowHeaderFilter = DefaultBoolean.False
                band.Columns.Add("nombre_titular", "Nombre Completo").Settings.AllowHeaderFilter = DefaultBoolean.False
            End Sub)
            settings.Columns.AddBand(
            Sub(band)
                band.Caption = "Información de Planilla"
                band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                band.Columns.Add("cod_hogar", "Código Hogar").Settings.AllowHeaderFilter = DefaultBoolean.False
                band.Columns.Add("programado", "Programado").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("detalle_bono", "Detalle Bono").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("proyeccion_corta", "Proyacción Corta").Settings.AllowHeaderFilter = DefaultBoolean.False
                band.Columns.Add("cobro", "Pagado").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("monto_total", "Monto Total").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("num_meses", "Nro. Meses").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
            End Sub)
            settings.Columns.AddBand(
            Sub(band)
                band.Caption = "Información de los Montos"
                band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                band.Columns.Add("BÁSICO", "Base").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("SALUD", "Salud").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("1 Y 2 CICLO EDUCACIÓN", "Educación 1 y 2").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("3 CICLO EDUCACIÓN", "Educación 3").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("monto_total_neto", "Total Neto").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("deducciones", "Debucciones").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("monto_acumulado", "Acumulado").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("monto_total_final", "Total Final").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
            End Sub)
            settings.Columns.AddBand(
            Sub(band)
                band.Caption = "Información del Niño"
                band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                band.Columns.Add("cod_persona", "Código").Settings.AllowHeaderFilter = DefaultBoolean.False
                band.Columns.Add("identidad_niño", "Identidad").Settings.AllowHeaderFilter = DefaultBoolean.False
                band.Columns.Add("nombre_persona", "Nombre").Settings.AllowHeaderFilter = DefaultBoolean.False
                band.Columns.Add("sexo_niño", "Sexo").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("ciclo_eleg_niño", "Ciclo Elegibilidad").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
            End Sub)
            settings.Columns.AddBand(
            Sub(band)
                band.Caption = "Cumplimiento Salud"
                band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                band.Columns.Add("niños_cump_salud", "Cumple Salud").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("niños_cump_salud_c1", "Salud C1").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("niños_cump_salud_c2", "Salud C2").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
            End Sub)
            settings.Columns.AddBand(
            Sub(band)
                band.Caption = "Cumplimiento Educación"
                band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                band.Columns.Add("niños_cump_educ_1", "Educación 1").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("niños_cump_educ_2_3", "Educación 2 3").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("niños_cump_educ_tot", "Educación Total").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
            End Sub)
            settings.Columns.AddBand(
            Sub(band)
                band.Caption = "Apercibimiento Salud"
                band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                band.Columns.Add("niños_aper_salud", "Apercibido Salud").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("niños_aper_salud_c1", "Salud C1").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("niños_aper_salud_c2", "Salud C2").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
            End Sub)
            settings.Columns.AddBand(
            Sub(band)
                band.Caption = "Apercibimiento Educación"
                band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                band.Columns.Add("niños_aper_educ_1", "Educación 1").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("niños_aper_educ_2_3", "Educación 2 3").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("niños_aper_educ_tot", "Educación Total").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
            End Sub)
            settings.Columns.AddBand(
            Sub(band)
                band.Caption = "No Cumple Salud"
                band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                band.Columns.Add("niños_no_cumple_salud", "No Cumple Salud").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("niños_no_cumple_salud_c1", "Salud C1").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("niños_no_cumple_salud_c2", "Salud C2").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
            End Sub)
            settings.Columns.AddBand(
            Sub(band)
                band.Caption = "No Cumple Educación"
                band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                band.Columns.Add("niños_no_cumple_educ_1", "Educación 1").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("niños_no_cumple_educ_2_3", "Educación 2 3").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
            End Sub)
            settings.Columns.AddBand(
            Sub(band)
                band.Caption = "Información de Corresponsabilidad"
                band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                band.Columns.Add("nombre_corresponsabilidad", "Nombre").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("cod_centro_educativo_salud", "Código Centro Salud/Educativo").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("nombre_centro_educativo_salud", "Nombre Centro Salud/Educativo").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("nombre_grado", "Nombre Grado").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                band.Columns.Add("numero_visitas_centro_salud", "Número de Visitas").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
            End Sub)

            Return settings

        End Function

    End Class

#End Region

End Namespace