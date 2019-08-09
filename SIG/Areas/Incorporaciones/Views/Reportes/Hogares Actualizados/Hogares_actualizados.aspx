<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Incorporaciones/Views/Shared/site.Master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>        
    .Boton 
    {
        background:#1787C8;
        color:#fff;
        border : none;
        font-family : "Arial" Georgia;
        width:200px;
        height:40px;
        vertical-align:central;
        text-align:center;
    }

    .Boton:hover
    {
        background:#fff;
        border: 1px solid #1787C8;
        cursor:pointer;
        color:#1787C8;
        box-sizing:border-box;
    }
</style>


<h2>Hogares Actualizados</h2>

    <%
        Html.BeginForm("Exportar_pv_reporteHogares_actualizados", "Reportes")
        Html.RenderAction("pv_Hogares_actualizados")
    %>
    <br />
    <br />
    <%
        Html.DevExpress().Button(Sub(btnSetting)
                                     btnSetting.Name = "btnExportar"
                                     btnSetting.Text = "Exportar"
                                     btnSetting.EnableTheming = False
                                     btnSetting.UseSubmitBehavior = True
                                     btnSetting.ControlStyle.Font.Size = 9
                                     btnSetting.ControlStyle.CssClass = "Boton"
                                 End Sub).GetHtml()
        Html.EndForm()
    %>


</asp:Content>
