<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    'Dim deptos As String()
    'deptos = DirectCast(ViewData("deptos"), String())
    
    
    Html.DevExpress().GridView(
       Sub(settings)
           settings.Name = "GridView"
           settings.Caption = "PLANILLAS DE PAGO ABIERTAS"
           settings.CallbackRouteValues = New With {Key .Controller = "CierrePlanilla", Key .Action = "PartialGridViewPlanillas", Key .strDeptos = ViewData("deptos")}
           'settings.CallbackRouteValues = New With {Key .Controller = "CierrePlanilla", Key .Action = "PartialGridViewPlanillas"}
           settings.ClientSideEvents.BeginCallback = "gdvOnBeginCallBack"
           
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
           
           settings.CommandColumn.Visible = True
           settings.CommandColumn.ShowSelectCheckbox = True
           settings.CommandColumn.Caption = "Cerrar"
           
           settings.Settings.ShowHeaderFilterButton = True
           settings.SettingsPopup.HeaderFilter.Height = 200
           
           settings.ClientSideEvents.SelectionChanged = "OnSelectionChanged"
           settings.ClientSideEvents.BeginCallback = "OnBeginCallback"
           settings.KeyFieldName = "cod_aldea"
           
           'código para maestro detalle
           settings.SettingsDetail.AllowOnlyOneMasterRowExpanded = True
           settings.SettingsDetail.ShowDetailRow = True
           
           settings.SetDetailRowTemplateContent(
               Sub(row)
                   Html.RenderAction("DetalleAldea", New With {Key .codAldea = DataBinder.Eval(row.DataItem, "cod_aldea")})
               End Sub)
                      
           settings.Columns.AddBand(
               Sub(ag)
                   ag.Caption = "Área Geográfica"
                   ag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   ag.Columns.Add("desc_departamento", "Departamento").Settings.HeaderFilterMode = HeaderFilterMode.CheckedList
                   ag.Columns.Add("desc_municipio", "Municipio").Settings.HeaderFilterMode = HeaderFilterMode.CheckedList
                   ag.Columns.Add("desc_aldea", "Aldea").Settings.AllowHeaderFilter = DefaultBoolean.False
               End Sub)
           settings.Columns.AddBand(
               Sub(prog)
                   prog.Caption = "Programado"
                   prog.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   prog.Columns.Add("hogProgramados", "Hogares").Settings.AllowHeaderFilter = DefaultBoolean.False
                   prog.Columns.Add("montoProgramado", "Monto").Settings.AllowHeaderFilter = DefaultBoolean.False
               End Sub)
           settings.Columns.AddBand(
               Sub(pag)
                   pag.Caption = "Pagado"
                   pag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   pag.Columns.Add("hogPagados", "Hogares").Settings.AllowHeaderFilter = DefaultBoolean.False
                   pag.Columns.Add("montoPagado", "Monto").Settings.AllowHeaderFilter = DefaultBoolean.False
               End Sub)
           settings.Columns.AddBand(
               Sub(noPag)
                   noPag.Caption = "No Pagado"
                   noPag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   noPag.Columns.Add("hogNoPagados", "Hogares").Settings.AllowHeaderFilter = DefaultBoolean.False
                   noPag.Columns.Add("montoNoPagado", "Monto").Settings.AllowHeaderFilter = DefaultBoolean.False
               End Sub)
           settings.Columns.Add("cumplimiento", "Cumplimiento").Settings.AllowHeaderFilter = DefaultBoolean.False
           settings.Columns.Add("cod_departamento").Visible = False
           settings.Columns.Add("cod_municipio").Visible = False
           settings.Columns.Add("cod_aldea").Visible = False
           'settings.Columns.Add("cumplimiento", "Cumplimiento").PropertiesEdit.DisplayFormatString = "p0"
           
       End Sub).Bind(Model).Render()%>