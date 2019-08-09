<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
   <%
       Dim setting As PivotGridSettings = SIG.SIG.Areas.Incorporaciones.Controllers.GridExport.pv_actualizacion_usuarios()
       setting.CallbackRouteValues = New With {Key .Controller = "Reportes", Key .Action = "fnc_PivotGridReporteActualizacionesUsuario"}
       Html.DevExpress().PivotGrid(setting).Bind(Model).GetHtml()

    %>


