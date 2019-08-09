<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Planilla/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Anulación de Planilla
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Planilla/Scripts/ejecucion.js")%>'></script>
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
            background-image: url('../../Areas/Planilla/Images/info.png');
        }
        .success {
            color: #4F8A10;
            background-color: #DFF2BF;
            background-image:url('../../Areas/Planilla/Images/success.png');
        }
        .warning {
            color: #9F6000;
            background-color: #FEEFB3;
            background-image: url('../../Areas/Planilla/Images/warning.png');
        }
        .error {
            color: #D8000C;
            background-color: #FFBABA;
            background-image: url('../../Areas/Planilla/Images/error.png');
        }
    </style>
    <h2>Anulación de Planilla</h2>
    <div id="divControles">
        <% Html.DevExpress.FormLayout(
               Sub(settings)
                   settings.Name = "floControles"
                   settings.ColCount = 2
                   settings.Items.Add(
                       Sub(item)
                           item.Caption = "Pago"
                           'item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                           item.SetNestedContent(
                               Sub()
                                   Html.RenderAction("pv_cbxPagos", "Shared")
                               End Sub)
                       End Sub)
                   settings.Items.Add(
                       Sub(item)
                           item.Caption = "Esquema"
                           item.SetNestedContent(
                               Sub()
                                   Html.RenderAction("pv_cbxEsquemas", "Shared")
                               End Sub)
                       End Sub)
                   settings.Items.Add(
                       Sub(item)
                           item.Caption = "Razón"
                           item.CaptionSettings.Location = LayoutItemCaptionLocation.Top
                           item.ColSpan = 2
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress.Memo(
                                       Sub(memo)
                                           memo.Name = "txtRazonAnulacion"
                                           memo.Width = 400
                                           memo.Height = 50
                                           memo.Properties.MaxLength = 200
                                           memo.Properties.NullText = "Describa la razón de la anulación de la planilla"
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
                   settings.Items.Add(
                       Sub(item)
                           item.ShowCaption = DefaultBoolean.False
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().Button(
                                       Sub(btn)
                                           btn.Name = "btnAnularPlanilla"
                                           btn.Text = "Anular Planilla"
                                           'btn.UseSubmitBehavior = False
                                           btn.ClientSideEvents.Click = "btnAnularPlanillaClick"
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
               End Sub).GetHtml()%>
    </div>
    <div id="divMensaje">
        <div class="success"></div>
        <div class="error"></div>
    </div>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>  
</asp:Content>
