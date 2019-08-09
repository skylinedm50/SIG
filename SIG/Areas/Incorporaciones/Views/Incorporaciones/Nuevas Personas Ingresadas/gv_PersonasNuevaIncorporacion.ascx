<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Dim setting As GridViewSettings = SIG.SIG.Areas.Incorporaciones.Controllers.IncorporacionesController.Cl_Exportar.gv_NuevasPersonasIncorporadas()
    setting.CallbackRouteValues = New With {Key .Controller = "Incorporaciones", Key .Action = "gv_PersonasNuevaIncorporacion"}
    Html.DevExpress().GridView(setting).Bind(Model).GetHtml()

%>