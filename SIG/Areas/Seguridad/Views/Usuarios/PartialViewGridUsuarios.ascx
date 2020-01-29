<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Dim settings As GridViewSettings = SIG.SIG.Areas.Seguridad.Controllers.UsuariosController.exportPvgUsuarios.ExportPivotGridSettings

    Html.DevExpress.GridView(settings).Bind(Model).GetHtml()
    %>

