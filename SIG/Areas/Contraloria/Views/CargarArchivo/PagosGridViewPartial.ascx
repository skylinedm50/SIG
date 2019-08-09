<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Dim settings As GridViewSettings = SIG.SIG.Areas.Contraloria.Controllers.exportGridViewCarga.ExportGridViewSettings
    settings.CallbackRouteValues = New With {Key .Controller = "CargarArchivo", Key .Action = "PagosGridViewPartial"}
    settings.Caption = "PAGOS EN EL ARCHIVO " + Session("fileName")
       
    Html.DevExpress().GridView(settings).Bind(Model).GetHtml()
%>
