Imports DevExpress.Web.Mvc
Imports DevExpress.Web
Imports DevExpress.Utils
Imports Newtonsoft.Json

Namespace SIG.Areas.Mineria.Controllers
    Public Class CorresponsabilidadController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Mineria/Corresponsabilidad

        Dim login As New Global.SIG.Cl_Login
        Dim corr As SIG.Areas.Mineria.Models.Cl_Corresponsabilidad = New SIG.Areas.Mineria.Models.Cl_Corresponsabilidad
        Dim share As SIG.Areas.Mineria.Models.Cl_Shared = New SIG.Areas.Mineria.Models.Cl_Shared

#Region "Funciones para porcentaje de niños que incumplen corresponsabilidad"

        Function v_PorcentajeNinosIncumpliendoCorr() As ActionResult

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

        Function pv_gdvNinoIncumplenComp(ByVal pago As String, ByVal hogares As String, ByVal departamento As String, ByVal municipio As String, ByVal aldea As String) As ActionResult

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
            ViewData("hogares") = hogares
            ViewData("departamento") = departamento
            ViewData("municipio") = municipio
            ViewData("aldea") = aldea
            Return PartialView(corr.Fnc_obtener_porcentaje_ninos_incumplieron_comp(pago, hogares, departamento, municipio, aldea))

        End Function

        Function exportarPorcentajeNinosIncumpliendoCorr() As ActionResult

            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")
            Dim pago As String = ComboBoxExtension.GetValue(Of String)("cbxPagosAno")
            Dim hogares As String = RadioButtonListExtension.GetValue(Of String)("rbTipo")
            Dim departamento As String = ComboBoxExtension.GetValue(Of String)("cbxDpto")
            Dim municipio As String = ComboBoxExtension.GetValue(Of String)("cbxMuni")
            Dim aldea As String = ComboBoxExtension.GetValue(Of String)("cbxAldea")

            Dim dt As DataTable = corr.Fnc_obtener_porcentaje_ninos_incumplieron_comp(pago, hogares, departamento, municipio, aldea)

            If export = "Excel" Then
                Return GridViewExtension.ExportToXlsx(SIG.Areas.Mineria.Controllers.exportGdvNinosIncumpliendo.ExportGridViewSettings, dt, "Niños incumpliendo Corresponsabilidad")
            ElseIf export = "Pdf" Then
                Return GridViewExtension.ExportToPdf(SIG.Areas.Mineria.Controllers.exportGdvNinosIncumpliendo.ExportGridViewSettings, dt, "Niños incumpliendo Corresponsabilidad")
            ElseIf export = "Rtf" Then
                Return GridViewExtension.ExportToRtf(SIG.Areas.Mineria.Controllers.exportGdvNinosIncumpliendo.ExportGridViewSettings, dt, "Niños incumpliendo Corresponsabilidad")
                'ElseIf export = "Html" Then
                'Return GridViewExtension.ExportToHtml(SIG.Areas.Mineria.Controllers.exportGdvNinosIncumpliendo.ExportGridViewSettings, dt, "Resultado Fichas")
            ElseIf export = "Csv" Then
                Return GridViewExtension.ExportToCsv(SIG.Areas.Mineria.Controllers.exportGdvNinosIncumpliendo.ExportGridViewSettings, dt, "Niños incumpliendo Corresponsabilidad")
            End If

            Return Nothing

        End Function

#End Region

#Region "funciones arrastre, altas y bajas de educación"

        Function v_ArrastreAltasBajasEducacion() As ActionResult

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

        Function pv_pvgArrastreAltasBajasEduacion(ByVal strPagos As String) As ActionResult

            Dim pagos() As Integer = Array.ConvertAll(Split(strPagos, ","), Function(s) Int32.Parse(s))
            Dim pago1 As Integer = Math.Min(pagos(0), pagos(1))
            Dim pago2 As Integer = Math.Max(pagos(0), pagos(1))

            ViewData("strPagos") = strPagos
            ViewData("nombre1") = share.Fnc_obtener_nombre_pago(pago1)
            ViewData("nombre2") = share.Fnc_obtener_nombre_pago(pago2)

            Return PartialView(corr.Fnc_obtener_arrastre_altas_bajas_educacion(pago1, pago2))

        End Function

        Function exportarArrastreAltasBajasEducacion() As ActionResult

            Dim strPagos As String = TextBoxExtension.GetValue(Of String)("txtPagosSeleccionados")
            Dim pagos() As Integer = Array.ConvertAll(Split(strPagos, ","), Function(s) Int32.Parse(s))
            Dim pago1 As Integer = Math.Min(pagos(0), pagos(1))
            Dim pago2 As Integer = Math.Max(pagos(0), pagos(1))
            Dim nombre1 As String = share.Fnc_obtener_nombre_pago(pago1)
            Dim nombre2 As String = share.Fnc_obtener_nombre_pago(pago2)

            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")
            Dim dt As DataTable = corr.Fnc_obtener_arrastre_altas_bajas_educacion(pago1, pago2)
            Dim settings As PivotGridSettings = SIG.Areas.Mineria.Controllers.exportPvgArrastreAltasBajas.ExportPivotGridSettings(nombre1, nombre2)

            If export = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(settings, dt, "Arrastre, Altas y Bajas de Niños en Educación entre " + nombre1 + " y " + nombre2)
            ElseIf export = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(settings, dt, "Arrastre, Altas y Bajas de Niños en Educación entre " + nombre1 + " y " + nombre2)
            ElseIf export = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(settings, dt, "Arrastre, Altas y Bajas de Niños en Educación entre " + nombre1 + " y " + nombre2)
            ElseIf export = "Html" Then
                Return PivotGridExtension.ExportToHtml(settings, dt, "Arrastre, Altas y Bajas de Niños en Educación entre " + nombre1 + " y " + nombre2)
            ElseIf export = "Csv" Then
                Return PivotGridExtension.ExportToCsv(settings, dt, "Arrastre, Altas y Bajas de Niños en Educación entre " + nombre1 + " y " + nombre2)
            End If

            Return Nothing

        End Function

        Function pv_mapaDiferenciaAltasBajasEducacion(ByVal strPagos As String) As ActionResult

            Dim pagos() As Integer = Array.ConvertAll(Split(strPagos, ","), Function(s) Int32.Parse(s))
            Dim jsonString As String = String.Empty
            jsonString = JsonConvert.SerializeObject(corr.Fnc_obtener_diferencia_entre_altas_bajas_educacion(Math.Min(pagos(0), pagos(1)), Math.Max(pagos(0), pagos(1))))

            ViewData("jsonString") = jsonString
            Return PartialView()

        End Function

        Function pv_mapaDiferenciaAltasBajasEducacionDepartamento(ByVal strPagos As String, ByVal departamento As String) As JsonResult
            Dim pagos() As Integer = Array.ConvertAll(Split(strPagos, ","), Function(s) Int32.Parse(s))
            Return Json(JsonConvert.SerializeObject(corr.Fnc_obtener_diferencia_entre_altas_bajas_educacion_departamento(Math.Min(pagos(0), pagos(1)), Math.Max(pagos(0), pagos(1)), departamento)))
        End Function

#End Region

#Region "funciones arrastre, altas y bajas de salud"

        Function v_ArrastreAltasBajasSalud() As ActionResult

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

        Function pv_pvgArrastreAltasBajasSalud(ByVal strPagos As String) As ActionResult

            Dim pagos() As Integer = Array.ConvertAll(Split(strPagos, ","), Function(s) Int32.Parse(s))
            Dim pago1 As Integer = Math.Min(pagos(0), pagos(1))
            Dim pago2 As Integer = Math.Max(pagos(0), pagos(1))

            ViewData("strPagos") = strPagos
            ViewData("nombre1") = share.Fnc_obtener_nombre_pago(pago1)
            ViewData("nombre2") = share.Fnc_obtener_nombre_pago(pago2)

            Return PartialView(corr.Fnc_obtener_arrastre_altas_bajas_salud(pago1, pago2))

        End Function

        Function exportarArrastreAltasBajasSalud() As ActionResult

            Dim strPagos As String = TextBoxExtension.GetValue(Of String)("txtPagosSeleccionados")
            Dim pagos() As Integer = Array.ConvertAll(Split(strPagos, ","), Function(s) Int32.Parse(s))
            Dim pago1 As Integer = Math.Min(pagos(0), pagos(1))
            Dim pago2 As Integer = Math.Max(pagos(0), pagos(1))
            Dim nombre1 As String = share.Fnc_obtener_nombre_pago(pago1)
            Dim nombre2 As String = share.Fnc_obtener_nombre_pago(pago2)

            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")
            Dim dt As DataTable = corr.Fnc_obtener_arrastre_altas_bajas_salud(pago1, pago2)
            Dim settings As PivotGridSettings = SIG.Areas.Mineria.Controllers.exportPvgArrastreAltasBajas.ExportPivotGridSettings(nombre1, nombre2)

            If export = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(settings, dt, "Arrastre, Altas y Bajas de Niños en Salud entre " + nombre1 + " y " + nombre2)
            ElseIf export = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(settings, dt, "Arrastre, Altas y Bajas de Niños en Salud entre " + nombre1 + " y " + nombre2)
            ElseIf export = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(settings, dt, "Arrastre, Altas y Bajas de Niños en Salud entre " + nombre1 + " y " + nombre2)
            ElseIf export = "Html" Then
                Return PivotGridExtension.ExportToHtml(settings, dt, "Arrastre, Altas y Bajas de Niños en Salud entre " + nombre1 + " y " + nombre2)
            ElseIf export = "Csv" Then
                Return PivotGridExtension.ExportToCsv(settings, dt, "Arrastre, Altas y Bajas de Niños en Salud entre " + nombre1 + " y " + nombre2)
            End If

            Return Nothing

        End Function

        Function pv_mapaDiferenciaAltasBajasSalud(ByVal strPagos As String) As ActionResult

            Dim pagos() As Integer = Array.ConvertAll(Split(strPagos, ","), Function(s) Int32.Parse(s))
            Dim jsonString As String = String.Empty
            jsonString = JsonConvert.SerializeObject(corr.Fnc_obtener_diferencia_entre_altas_bajas_salud(Math.Min(pagos(0), pagos(1)), Math.Max(pagos(0), pagos(1))))

            ViewData("jsonString") = jsonString
            Return PartialView()

        End Function

        Function pv_mapaDiferenciaAltasBajasSaludDepartamento(ByVal strPagos As String, ByVal departamento As String) As JsonResult
            Dim pagos() As Integer = Array.ConvertAll(Split(strPagos, ","), Function(s) Int32.Parse(s))
            Return Json(JsonConvert.SerializeObject(corr.Fnc_obtener_diferencia_entre_altas_bajas_salud_departamento(Math.Min(pagos(0), pagos(1)), Math.Max(pagos(0), pagos(1)), departamento)))
        End Function

#End Region

#Region "funciones para el total de niños cumpliendo, apercibidos, no cumpliendo por componente"

        Function v_TotalesNinosComponente() As ActionResult

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

        Function pv_pvgTotalesNinosComponente(ByVal pago As String) As ActionResult
            ViewData("pago") = pago
            Return PartialView(corr.Fnc_obtener_totales_niños_componente(pago))

        End Function

        Function exportarTotalesNinosComponente() As ActionResult

            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")
            Dim pago As String = ComboBoxExtension.GetValue(Of String)("cbxPagosAno")
            Dim settings As PivotGridSettings = SIG.Areas.Mineria.Controllers.exportPvgTotalesNinosComponente.ExportPivotGridSettings

            If export = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(settings, corr.Fnc_obtener_totales_niños_componente(pago), "Totales Niños por Componente")
            ElseIf export = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(settings, corr.Fnc_obtener_totales_niños_componente(pago), "Totales Niños por Componente")
            ElseIf export = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(settings, corr.Fnc_obtener_totales_niños_componente(pago), "Totales Niños por Componente")
            ElseIf export = "Html" Then
                Return PivotGridExtension.ExportToHtml(settings, corr.Fnc_obtener_totales_niños_componente(pago), "Totales Niños por Componente")
            ElseIf export = "Csv" Then
                Return PivotGridExtension.ExportToCsv(settings, corr.Fnc_obtener_totales_niños_componente(pago), "Totales Niños por Componente")
            End If

            Return Nothing

        End Function


#End Region


    End Class

#Region "Clase para los settings de los gridview y pivotgrid"

    Public NotInheritable Class exportGdvNinosIncumpliendo

        Private Shared exportSettings As GridViewSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportGridViewSettings() As GridViewSettings

            Get
                exportSettings = CreateExportGdvNinosIncumpliendo()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportGdvNinosIncumpliendo() As GridViewSettings

            Dim settings As New GridViewSettings()

            settings.Name = "gdvNinosIncumpliendoComp"

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

            settings.SettingsExport.FileName = "NIÑOS INCUMPLIENDO COMPONENTE"
            settings.Caption = "NIÑOS INCUMPLIENDO COMPONENTE"

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
                    tit.Caption = "Titular"
                    tit.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    tit.Columns.Add("cod_hogar", "Código Hogar").Settings.HeaderFilterMode = HeaderFilterMode.CheckedList
                    tit.Columns.Add("identidad_titular", "Nro. Identidad").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                    tit.Columns.Add("nombre_titular", "Nombre Completo").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                End Sub)
            settings.Columns.Add("total_niños", "Total Niños").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
            settings.Columns.AddBand(
                Sub(ficha)
                    ficha.Caption = "Educación"
                    ficha.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    ficha.Columns.Add("total_niños_educacion", "Total Educación").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                    ficha.Columns.Add("cumplen_educacion", "Cumplen").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                    ficha.Columns.Add("apercibidos_educacion", "Apercibidos").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                    ficha.Columns.Add("no_cumple_educacion", "No Cumplen").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                    ficha.Columns.Add("porcentaje_educacion", "% incumplimiento").Settings.HeaderFilterMode = HeaderFilterMode.CheckedList
                End Sub)
            settings.Columns.AddBand(
                Sub(ficha)
                    ficha.Caption = "Salud"
                    ficha.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    ficha.Columns.Add("total_niños_salud", "Total Educación").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                    ficha.Columns.Add("cumplen_salud", "Cumplen").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                    ficha.Columns.Add("apercibidos_salud", "Apercibidos").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                    ficha.Columns.Add("no_cumple_salud", "No Cumplen").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                    ficha.Columns.Add("porcentaje_salud", "% incumplimiento").Settings.HeaderFilterMode = HeaderFilterMode.CheckedList
                End Sub)

            Return settings

        End Function

    End Class

    'esta clase es llamada el arrastre, altas y bajas tanto de educación y salud
    Public NotInheritable Class exportPvgArrastreAltasBajas

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportPivotGridSettings(ByVal pago1 As String, ByVal pago2 As String) As PivotGridSettings

            Get
                exportSettings = CreateExportPvgArrastreAltasBajas(pago1, pago2)
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgArrastreAltasBajas(ByVal pago1 As String, ByVal pago2 As String) As PivotGridSettings

            Dim settings As New PivotGridSettings()

            settings.Name = "pvgInfo"
            settings.Width = Unit.Percentage(100)
            settings.OptionsView.ShowHorizontalScrollBar = True

            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False

            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
                    field.Index = 2
                    field.Caption = "Departamento"
                    field.FieldName = "desc_departamento"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    field.Index = 3
                    field.Caption = "Municipio"
                    field.FieldName = "desc_municipio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    field.Index = 4
                    field.Caption = "Aldea"
                    field.FieldName = "desc_aldea"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    field.Index = 5
                    field.Caption = "Caserío"
                    field.FieldName = "desc_caserio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 0
                    field.Caption = pago1
                    field.FieldName = "total_planilla_1"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 1
                    field.Caption = pago2
                    field.FieldName = "total_planilla_2"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 2
                    field.Caption = "Arrastre"
                    field.FieldName = "arrastre"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 3
                    field.Caption = "Altas"
                    field.FieldName = "altas"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 4
                    field.Caption = "Bajas"
                    field.FieldName = "bajas"
                End Sub)


            Return settings

        End Function

    End Class

    Public NotInheritable Class exportPvgTotalesNinosComponente

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportPivotGridSettings() As PivotGridSettings

            Get
                exportSettings = CreateExportPvgTotalesNinosComponentes()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgTotalesNinosComponentes() As PivotGridSettings

            Dim settings As New PivotGridSettings()

            settings.Name = "pvgInfo"
            settings.Width = Unit.Percentage(100)
            settings.OptionsView.ShowHorizontalScrollBar = True

            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False

            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
                    field.Index = 0
                    field.Caption = "Departamento"
                    field.FieldName = "desc_departamento"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    field.Caption = "Municipio"
                    field.FieldName = "desc_municipio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    field.Caption = "Aldea"
                    field.FieldName = "desc_aldea"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    field.Caption = "Caserío"
                    field.FieldName = "desc_caserio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
                    field.Index = 0
                    field.Caption = "Componente"
                    field.FieldName = "componente"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 0
                    field.Caption = "Total Niños"
                    field.FieldName = "total_niños"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 1
                    field.Caption = "Cumpliendo"
                    field.FieldName = "cumplen"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 2
                    field.Caption = "Apercibidos"
                    field.FieldName = "apercibidos"
                End Sub)
            'settings.Fields.Add(
            '    Sub(field)
            '        field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
            '        field.Index = 3
            '        field.Caption = "No Cumplen"
            '        field.FieldName = "no_cumple"
            '    End Sub)
            Return settings

        End Function

    End Class

#End Region

End Namespace