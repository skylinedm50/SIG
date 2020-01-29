<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Html.DevExpress().Chart(
        Sub(settings)
            settings.Name = "chart"
            settings.CallbackRouteValues = New With {Key .Controller = "PlanillasPago", Key .Action = "pv_chrComparacionPlanillas"}
            settings.EnableClientSideAPI = True
            settings.Legend.MaxHorizontalPercentage = 10
            settings.BorderOptions.Visible = True
            settings.Width = 1200
            settings.Height = 700
            settings.ClientSideEvents.BeginCallback = "OnBeginChartComparacionCallback"

            settings.SeriesDataMember = "Series"
            settings.SeriesTemplate.ChangeView(ViewData("tipo"))
            settings.SeriesTemplate.ChangeView(DevExpress.XtraCharts.ViewType.Pie)
            settings.SeriesTemplate.ArgumentDataMember = "Arguments"
            settings.SeriesTemplate.ValueDataMembers(0) = "Values"
            settings.SeriesTemplate.Label.ResolveOverlappingMode = ResolveOverlappingMode.Default
            settings.SeriesTemplate.ToolTipEnabled = DefaultBoolean.True

            settings.SeriesTemplate.Label.LineVisible = True
            settings.SeriesTemplate.LabelsVisibility = DefaultBoolean.True

        End Sub).Bind(Model).GetHtml()
%>