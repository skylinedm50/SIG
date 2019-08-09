<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ProgressBar(
                       Sub(settings)
                           settings.Name = "pbCarga"
                           settings.Width = System.Web.UI.WebControls.Unit.Pixel(200)
                           settings.ClientVisible = False
                           
                       End Sub).GetHtml()%>