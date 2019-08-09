<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Planilla/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Emisión de Planilla</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.Editors},
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Planilla/Scripts/jquery.signalR-2.2.0.js")%>'></script>
    <script src="../../signalr/hubs"></script>
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

    <h2>Emisión de Planilla</h2>
    <% Html.BeginForm("exportarPlanillaGenerada", "Ejecucion")%>
    <div id="divControles">
        <% Html.DevExpress.FormLayout(
               Sub(settings)
                   settings.Name = "floControles"
                   settings.ColCount = 2
                   settings.Width = 650
                   settings.Items.Add(
                       Sub(item)
                           item.ColSpan = 2
                           item.Caption = "Pago"
                           item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                           item.SetNestedContent(
                               Sub()
                                   Html.RenderAction("pv_cbxPagos", "Shared")
                               End Sub)
                       End Sub)
                   settings.Items.Add(
                       Sub(item)
                           item.ColSpan = 2
                           item.ShowCaption = DefaultBoolean.False
                           item.SetNestedContent(
                               Sub()
                                   Html.RenderAction("pv_gdvEsquemas", "Shared")
                               End Sub)
                       End Sub)
                   settings.Items.Add(
                       Sub(item)
                           item.ShowCaption = DefaultBoolean.False
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().Button(
                                       Sub(btn)
                                           btn.Name = "btnEjecutarPlanilla"
                                           btn.Text = "Ejecutar Planilla"
                                           btn.UseSubmitBehavior = False
                                           btn.ClientSideEvents.Click = "btnEjecutarPlanillaClick"
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
                                           btn.Name = "btnVerResumen"
                                           btn.Text = "Ver Resumen"
                                           btn.UseSubmitBehavior = False
                                           btn.ClientSideEvents.Click = "btnVerResumenClick"
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
               End Sub).GetHtml()%>
    </div>
    <div id="divMensaje">
        <div class="info"></div>
        <div class="success"></div>
        <div class="warning"></div>
        <div class="error"></div>
    </div>
    <div id="divExportar">
        <% Html.DevExpress().FormLayout(
       Sub(frmLayoutExport)
           frmLayoutExport.Name = "floExportar"
           frmLayoutExport.ColCount = 2
           frmLayoutExport.Items.Add(
               Sub(item)
                   item.Caption = "Exportar a"
                   item.SetNestedContent(
                       Sub()
                           Html.DevExpress().ComboBox(
                               Sub(cbx)
                                   cbx.Name = "cbxExpotar"
                                   cbx.Width = 100
                                   cbx.SelectedIndex = 0
                                   cbx.Properties.Items.Add("Excel")
                                   cbx.Properties.Items.Add("Csv")
                                   cbx.Properties.Items.Add("Pdf")
                                   cbx.Properties.Items.Add("Rtf")
                                   cbx.Properties.Items.Add("Html")
                                   cbx.Properties.ValueType = GetType(String)
                               End Sub).GetHtml()
                       End Sub)
               End Sub)
           frmLayoutExport.Items.Add(
               Sub(item)
                   item.ShowCaption = DefaultBoolean.False
                   item.SetNestedContent(
                       Sub()
                           Html.DevExpress().Button(
                               Sub(btnConsultar)
                                   btnConsultar.Name = "Exportar"
                                   btnConsultar.UseSubmitBehavior = True
                                   btnConsultar.Text = "Exportar"
                                   'btnConsultar.RouteValues = New With {Key .Controller = "Ejecucion", Key .Action = "exportarPlanillaGenerada", Key .esquemas = ""}
                               End Sub).GetHtml()
                       End Sub)
               End Sub)
        End Sub).GetHtml()%>
    </div>
    <div id="divPivotGrid"></div>
    <% Html.DevExpress().TextBox(
           Sub(txt)
               txt.Name = "txtSelectedIdsHidden"
               txt.Text = "test"
               txt.ClientVisible = False
           End Sub).GetHtml()%>
    <% Html.EndForm()%>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>    
</asp:Content>
