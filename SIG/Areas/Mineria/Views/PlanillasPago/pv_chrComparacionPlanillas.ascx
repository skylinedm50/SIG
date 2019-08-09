<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Html.DevExpress().Chart(
        Sub(settings)
            settings.Name = "chart"
            settings.CallbackRouteValues = New With {Key .Controller = "PlanillasPago", Key .Action = "pv_chrComparacionPlanillas"}
            settings.EnableClientSideAPI = True
            settings.Legend.MaxHorizontalPercentage = 30
            settings.BorderOptions.Visible = True
            settings.Width = Unit.Pixel(960)
            settings.Height = Unit.Pixel(500)
            settings.ClientSideEvents.BeginCallback = "OnBeginChartComparacionCallback"
            
            settings.SeriesDataMember = "Series"
            settings.SeriesTemplate.ChangeView(ViewData("tipo"))
            settings.SeriesTemplate.ArgumentDataMember = "Arguments"
            settings.SeriesTemplate.ValueDataMembers(0) = "Values"
            settings.SeriesTemplate.Label.ResolveOverlappingMode = ResolveOverlappingMode.Default
            settings.SeriesTemplate.ToolTipEnabled = DefaultBoolean.True
            
            settings.SeriesTemplate.Label.LineVisible = True
            settings.SeriesTemplate.LabelsVisibility = DefaultBoolean.True
            
        End Sub).Bind(Model).GetHtml()
%>