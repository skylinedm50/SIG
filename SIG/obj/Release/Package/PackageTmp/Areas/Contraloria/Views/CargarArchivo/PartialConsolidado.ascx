<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="System.DevExpress" %>

<% Html.DevExpress().DocumentViewer(
       Sub(Settings)
           Settings.Name = "dvConsolidado"
           'Settings.Report = CType(Model, XtraReport)
           'Settings.Report = CType(ViewData("report"), Contraloría.Contraloría.Reports.consolidadoArchivo)
           Settings.Report = CType(ViewData("report"), SIG.SIG.Contraloria.consolidadoArchivo)
           Settings.CallbackRouteValues = New With {.Controller = "CargarArchivo", .Action = "PartialConsolidado"}
       End Sub).GetHtml()%>