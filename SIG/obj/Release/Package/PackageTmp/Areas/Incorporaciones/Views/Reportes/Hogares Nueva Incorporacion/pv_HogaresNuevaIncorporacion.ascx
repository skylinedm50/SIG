<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Dim setting As PivotGridSettings = SIG.SIG.Areas.Incorporaciones.Controllers.GridExport.pv_hogares_nuevos_miembros()
    setting.CallbackRouteValues = New With {Key .Controller = "Reportes", Key .Action = "pv_HogaresNuevaIncorporacion"}
    Html.DevExpress().PivotGrid(setting).Bind(Model).GetHtml()
%>
