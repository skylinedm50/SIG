<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    '' MODIFICACIÓN PARA PASAR AL PROYECTO FINAL
    Dim settings As PivotGridSettings = SIG.SIG.Areas.Mineria.Controllers.exportPvgElegiblesControProgramado.ExportPivotGridSettings(ViewData("variante"))

    settings.CallbackRouteValues = New With {Key .Controller = "PlanillasPago", Key .Action = "pv_pvgElegiblesVsProgramado", Key .pago = ViewData("pago"), Key .variante = ViewData("variante")}


    Html.DevExpress().PivotGrid(settings).Bind(Model).GetHtml()

    %>