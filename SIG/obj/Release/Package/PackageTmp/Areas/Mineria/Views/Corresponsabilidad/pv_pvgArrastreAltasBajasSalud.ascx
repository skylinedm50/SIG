<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Dim settings As PivotGridSettings = SIG.SIG.Areas.Mineria.Controllers.exportPvgArrastreAltasBajas.ExportPivotGridSettings(ViewData("nombre1"), ViewData("nombre2"))
    settings.CallbackRouteValues = New With {Key .Controller = "Corresponsabilidad", Key .Action = "pv_pvgArrastreAltasBajasSalud", Key .strPagos = ViewData("strPagos")}
    Html.DevExpress().PivotGrid(settings).Bind(Model).GetHtml()
    %>