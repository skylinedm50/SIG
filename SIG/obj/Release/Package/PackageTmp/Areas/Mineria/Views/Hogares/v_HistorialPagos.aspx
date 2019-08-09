<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Historial de Pagos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/hogares.js")%>'></script>
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
            background-image: url('../../Areas/Mineria/images/info.png');
        }
        .success {
            color: #4F8A10;
            background-color: #DFF2BF;
            background-image:url('../../Areas/Mineria/images/success.png');
        }
        .warning {
            color: #9F6000;
            background-color: #FEEFB3;
            background-image: url('../../Areas/Mineria/images/warning.png');
        }
        .error {
            color: #D8000C;
            background-color: #FFBABA;
            background-image: url('../../Areas/Mineria/images/error.png');
        }
    </style>
    <h2>Historial de Pagos del Hogar</h2>
    <div id="divControles">
        <% Html.DevExpress().FormLayout(
               Sub(flo)
                   flo.Name = "floBuscarPersona"
                   flo.ColCount = 2
                   flo.Items.AddGroupItem(
                       Sub(group)
                           group.Caption = "Buscar persona(s)"
                           group.ColCount = 2
                           group.ColSpan = 2
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Nombre"
                                   'item.HelpText = "Escriba parte del nombre de la persona que busca"
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().TextBox(
                                               Sub(txt)
                                                   txt.Name = "txtNombre"
                                                   txt.Properties.NullText = "Ingrese el nombre o parte del nombre"
                                               End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Identidad"
                                   item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().TextBox(
                                           Sub(txt)
                                               txt.Name = "txtIdentidad"
                                               txt.Properties.MaxLength = 13
                                               'txt.Width = 120
                                               txt.Properties.MaskSettings.Mask = "0000-0000-00000"
                                               txt.Properties.MaskSettings.IncludeLiterals = MaskIncludeLiteralsMode.None
                                           End Sub).GetHtml()
                                   End Sub)
                               End Sub)
                       End Sub)
                   flo.Items.Add(
                       Sub(item)
                           item.ShowCaption = DefaultBoolean.False
                           'item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().Button(
                                       Sub(btn)
                                           btn.Name = "btnBuscarPersona"
                                           btn.Text = "Buscar persona"
                                           btn.ClientSideEvents.Click = "btnBuscarPersonaClick"
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
                   flo.Items.Add(
                       Sub(item)
                           item.Name = "itemBtnConsultar"
                           item.ShowCaption = DefaultBoolean.False
                           item.ClientVisible = False
                           'item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().Button(
                                       Sub(btn)
                                           btn.Name = "btnConsularHistorial"
                                           btn.Text = "Obtener Historial"
                                           btn.ClientSideEvents.Click = "btnConsularHistorialClick"
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
               End Sub).GetHtml()%>
    </div>
    <br />
    <div id="divMensaje">
        <div class="info"></div>
        <div class="success"></div>
        <div class="warning"></div>
        <div class="error"></div>
    </div>
    <br />
    <div id="divGridPersonas"></div>
    <br />
    <div id="divGridView"></div>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>
</asp:Content>
