<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Incorporaciones/Views/Shared/site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="/Areas/Incorporaciones/Content/Style/boostrap/bootstrap.min.js"></script>
    <link href="/Areas/Incorporaciones/Content/Style/boostrap/bootstrap.min.css" rel="stylesheet" />
 
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
        Html.DevExpress().FormLayout(Sub(setting)
                                         setting.Name = "Form_Layout"
                                         setting.ColCount = 1
                                         setting.Width = 880
                                         setting.Theme = "DevEx"
                                         setting.Items.Add(Sub(grupo)
                                                               grupo.Caption = "Nombre del Periodo de Actualización"
                                                               grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Top
                                                               grupo.SetNestedContent(Sub()
                                                                                          Html.DevExpress().TextBox(Sub(txt)
                                                                                                                        txt.Name = "txtApertura"
                                                                                                                        txt.Properties.Native = True
                                                                                                                        txt.ControlStyle.CssClass = "form-control input-lg"
                                                                                                                    End Sub).GetHtml()
                                                                                      End Sub)
                                                               
                                                           End Sub)
                                         setting.Items.Add(Sub(grupo)
                                                               grupo.ShowCaption = DefaultBoolean.False
                                                               grupo.SetNestedContent(Sub()
                                                                                          %><br /><br /><%
                                                                                          Html.DevExpress().Button(Sub(Btn)
                                                                                                                       Btn.Name = "bntApertura"
                                                                                                                       Btn.EnableTheming = False
                                                                                                                       Btn.Width = 200
                                                                                                                       Btn.Height = 40
                                                                                                                       Btn.Text = "Crear registro"
                                                                                                                       Btn.ControlStyle.Font.Size = 10
                                                                                                                       Btn.ControlStyle.CssClass = "Boton"
                                                                                                                       Btn.ClientSideEvents.Click = "function(s,e){ RealizarApertura () }"
                                                                                                                   End Sub).GetHtml()
                                                                                      End Sub)
                                                               
                                                           End Sub)
                                     End Sub).GetHtml()
    %>

    <br /><br />
     <div id="divMensaje">
        <div class="error" id="Error_text"></div>
    </div>

</asp:Content>