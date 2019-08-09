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

    <h2>Actualizaciones Realizadas por Usuario</h2>
    <br />
    <br />
    <%
        Html.BeginForm("Exportar_pv_reporteActualizacionUsuario", "Reportes")
        Html.RenderAction("fnc_PivotGridReporteActualizacionesUsuario")
    %>
    <br />
    <br />
    <%
        Html.DevExpress().Button(Sub(btnSetting)
                                     btnSetting.Name = "BtnExport"
                                     btnSetting.Text = "Exportar"
                                     btnSetting.EnableTheming = False
                                     btnSetting.UseSubmitBehavior = True
                                     btnSetting.ControlStyle.Font.Size = 9
                                     btnSetting.ControlStyle.CssClass = "Boton"
                                 End Sub).GetHtml()

        Html.EndForm()
    %>
</asp:Content>
