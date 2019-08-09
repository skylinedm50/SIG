<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Planilla/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Aprobación de Esquema
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.Editors},
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Planilla/Scripts/Aprobacion/aprobacion.js")%>'></script>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Planilla/Scripts/Aprobacion/pdfmake.js")%>'></script>
    <%--<script type="text/javascript" src='<%: ResolveUrl("~/Areas/Planilla/Scripts/Aprobacion/crimsontext.js")%>'></script>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Planilla/Scripts/Aprobacion/crimsontext.map.js")%>'></script>--%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Planilla/Scripts/Aprobacion/vfs_fonts.js")%>'></script>
    
    <style>
        #divDetalle {
            height: 600px;
            overflow-y: scroll;
            width: 1100px;
        }

        #divContenedorDetalle {
            display: none;
        }

        fieldset {
            border: 1px solid #C2C4CB;
            border-radius: 4px;
            font: 11px Verdana, Geneva, sans-serif;
            /*width: 441px;*/
            padding: 6px;
        }

        legend {
            color: #A1A3AA;
        }

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



    <h2>Aprobación de Esquema</h2>
    <div id="divControles">
        <% Html.DevExpress.FormLayout(
               Sub(settings)
                   settings.Name = "floControles"
                   settings.ColCount = 1
                   settings.Items.AddGroupItem(
                       Sub(group)
                           'group.GroupBoxDecoration = GroupBoxDecoration.None
                           group.Caption = "Controles de búqueda"
                           group.ColCount = 2
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Pago"
                                   item.SetNestedContent(
                                       Sub()
                                           Html.RenderAction("pv_cbxPagos", "Shared")
                                       End Sub)
                               End Sub)
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Esquema"
                                   item.SetNestedContent(
                                       Sub()
                                           Html.RenderAction("pv_cbxEsquemas", "Shared")
                                       End Sub)
                               End Sub)
                           'group.Items.Add(
                           '    Sub(item)
                           '        item.ShowCaption = DefaultBoolean.False
                           '        item.SetNestedContent(
                           '            Sub()
                           '                Html.DevExpress().Button(
                           '                    Sub(btn)
                           '                        btn.Name = "btnBuscarEsquema"
                           '                        btn.Text = "Buscar Esquema"
                           '                        btn.ClientSideEvents.Click = "btnBuscarEsquemaClick"
                           '                    End Sub).GetHtml()
                           '            End Sub)
                           '    End Sub)
                       End Sub)
               End Sub).GetHtml()%>
    </div>
    <div id="divMensaje">
        <div class="info"></div>
        <div class="success"></div>
        <div class="warning"></div>
        <div class="error"></div>
    </div>
    <div id="divContenedorDetalle">
        <fieldset>
            <legend>Detalle de los parámetros del esquema</legend>
            <div id="divDetalle"></div>
        </fieldset>
        <div>
            <% Html.DevExpress.FormLayout(
                   Sub(flo)
                       flo.Name = "floBotones"
                       flo.ColCount = 2
                       flo.Items.Add(
                           Sub(item)
                               item.ShowCaption = DefaultBoolean.False
                               item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Button(
                                           Sub(btn)
                                               btn.Name = "bntAprobar"
                                               btn.Text = "Aprobar"
                                               btn.ClientSideEvents.Click = "function(){ bntAprobarClick(); }"
                                           End Sub).GetHtml()
                                   End Sub)
                           End Sub)
                       flo.Items.Add(
                           Sub(item)
                               item.ShowCaption = DefaultBoolean.False
                               item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Button(
                                           Sub(btn)
                                               btn.Name = "btnImprimir"
                                               btn.Text = "Descargar Informe"
                                               btn.ClientSideEvents.Click = "btnImprimirClick"
                                           End Sub).GetHtml()
                                   End Sub)
                           End Sub)
                   End Sub).GetHtml()%>
        </div>
    </div>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>
</asp:Content>
