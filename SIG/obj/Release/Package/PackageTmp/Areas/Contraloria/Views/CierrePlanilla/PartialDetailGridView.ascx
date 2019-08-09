<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().GridView(
       Sub(settings)
           settings.Name = "GridViewDetailAldea"
           settings.Caption = "PLANILLAS DE PAGO POR ALDEA"
           settings.CallbackRouteValues = New With {Key .Controller = "CierrePlanilla", Key .Action = "detalleAldea"}
           
           settings.SettingsPager.Position = PagerPosition.Bottom
           settings.SettingsPager.FirstPageButton.Visible = True
           settings.SettingsPager.LastPageButton.Visible = True
           settings.SettingsPager.PageSizeItemSettings.Visible = True
           settings.SettingsPager.PageSizeItemSettings.Items = New String() {"10", "20", "50"}
           settings.ControlStyle.CssClass = "detalleAldea"
           'settings.SettingsBehavior.AllowGroup = True
           'settings.SettingsBehavior.AllowSort = True
           'settings.Settings.ShowGroupPanel = True
           
           'settings.CommandColumn.Visible = True
           'settings.CommandColumn.ShowSelectCheckbox = True
           'settings.CommandColumn.Caption = "Cerrar"
           'settings.CommandColumn.VisibleIndex = 10
           
           settings.Columns.Add("desc_caserio", "Caserio")
           settings.Columns.AddBand(
               Sub(prog)
                   prog.Caption = "Programado"
                   prog.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   prog.Columns.Add("hogProgramados", "Hogares")
                   prog.Columns.Add("montoProgramado", "Monto")
               End Sub)
           settings.Columns.AddBand(
               Sub(pag)
                   pag.Caption = "Pagado"
                   pag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   pag.Columns.Add("hogPagados", "Hogares")
                   pag.Columns.Add("montoPagado", "Monto")
               End Sub)
           settings.Columns.AddBand(
               Sub(noPag)
                   noPag.Caption = "No Pagado"
                   noPag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   noPag.Columns.Add("hogNoPagados", "Hogares")
                   noPag.Columns.Add("montoNoPagado", "Monto")
               End Sub)
           settings.Columns.Add("cumplimiento", "Cumplimiento")
           'settings.Columns.Add("cumplimiento", "Cumplimiento").PropertiesEdit.DisplayFormatString = "p0"
           
           settings.Settings.ShowFooter = True
           settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "hogProgramados")
           settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "montoProgramado")
           settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "hogPagados")
           settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "montoPagado")
           settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "hogNoPagados")
           settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "montoNoPagado")
           
       End Sub).Bind(Model).Render()%>