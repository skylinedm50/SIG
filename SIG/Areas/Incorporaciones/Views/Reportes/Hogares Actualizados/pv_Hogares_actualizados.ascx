<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<% 
    Dim setting As PivotGridSettings = SIG.SIG.Areas.Incorporaciones.Controllers.GridExport.pv_hogares_actualizados()
    setting.CallbackRouteValues = New With {Key .Controller = "Reportes", Key .Action = "pv_Hogares_actualizados"}
    Html.DevExpress().PivotGrid(setting).Bind(Model).GetHtml()
%>
