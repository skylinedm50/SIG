﻿<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Dim settings As PivotGridSettings = SIG.SIG.Areas.Mineria.Controllers.exportPvgElegiblesControProgramado.ExportPivotGridSettings
    
    settings.CallbackRouteValues = New With {Key .Controller = "PlanillasPago", Key .Action = "pv_pvgElegiblesVsProgramado", Key .pago = ViewData("pago")}
       
    
    Html.DevExpress().PivotGrid(settings).Bind(Model).GetHtml()
    
    %>