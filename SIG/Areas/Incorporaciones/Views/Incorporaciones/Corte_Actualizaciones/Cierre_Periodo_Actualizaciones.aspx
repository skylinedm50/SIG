<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Incorporaciones/Views/Shared/site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<style>
   .info, .success, .warning, .error, .validation {
            border: 1px solid;
            margin: 10px 0px;
            padding:15px 10px 15px 50px;
            background-repeat: no-repeat;
            background-position: 10px center;
            display: none;
        }
        .info {
            color: #00529B;
            background-color: #BDE5F8;
            background-image: url("/Areas/Incorporaciones/Content/Images/info.png");
        }
        .success {
            color: #4F8A10;
            background-color: #DFF2BF;
            background-image:url("/Areas/Incorporaciones/Content/Images/success.png");
        }
        .warning 
        {
            color: #9F6000;
            background-color: #FEEFB3;
            background-image: url( "/Areas/Incorporaciones/Content/Images/warning.png");
        }
        .error 
        {
            color: #D8000C;
            background-color: #FFBABA;
            background-image: url("/Areas/Incorporaciones/Content/Images/error.png");
        }

        .hidden 
        {
            display:none;
        }

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




       <%
           Html.RenderAction("Periodo_Actualizacion_Aperturado", "Incorporaciones")
       %>
    <br /><br />
        <%
            Html.DevExpress().Button(Sub(Btn)
                                         Btn.Name = "bntcerrar"
                                         Btn.EnableTheming = False
                                         Btn.Width = 200
                                         Btn.Height = 40
                                         Btn.Text = "Corte de Actualizaciones"
                                         Btn.ControlStyle.Font.Size = 10
                                         Btn.ControlStyle.CssClass = "Boton"
                                         Btn.ClientSideEvents.Click = "function(s,e){ RealizarCierre () }"
                                     End Sub).GetHtml()
        %>
    <br /><br />
    <div id="divMensaje">
        <div class="error" id="Error_text"></div>
    </div>


</asp:Content>
