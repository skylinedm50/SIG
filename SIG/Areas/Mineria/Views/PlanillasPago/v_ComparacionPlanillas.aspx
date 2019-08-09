<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Arrastre, Altas y Bajas entre Pagos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid}, 
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid},
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/planillasPago.js")%>'></script>
    <style>

        /* The Modal (background) */
        .modal2 {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
            -webkit-animation-name: fadeIn; /* Fade in the background */
            -webkit-animation-duration: 0.4s;
            animation-name: fadeIn;
            animation-duration: 0.4s;
        }

        /* Modal Content */
        .modal-content {
            position: relative;
            /*bottom: 0;*/
            background-color: #fefefe;
            margin: 5% auto; /* 15% from the top and centered */
            width: 70%;
            -webkit-animation-name: slideIn;
            -webkit-animation-duration: 0.4s;
            animation-name: slideIn;
            animation-duration: 0.4s;
        }

        /* The Close Button */
        .close {
            color: #424242;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

        .close:hover,
        .close:focus {
            color: #000;
            text-decoration: none;
            cursor: pointer;
        }

        .modal-header {
            padding: 2px 16px;
            background-color: #f2f3f4;
            color: white;
        }

        .modal-body {padding: 2px 16px;}

        .modal-footer {
            padding: 2px 16px;
            background-color: #f2f3f4;
            color: #424242;
        }

        /* Add Animation */
        @-webkit-keyframes slideIn {
            from {bottom: -300px; opacity: 0}
            to {bottom: 0; opacity: 1}
        }

        @keyframes slideIn {
            from {bottom: -300px; opacity: 0}
            to {bottom: 0; opacity: 1}
        }

        @-webkit-keyframes fadeIn {
            from {opacity: 0}
            to {opacity: 1}
        }

        @keyframes fadeIn {
            from {opacity: 0}
            to {opacity: 1}
        }
    </style>
    <h2>Arrastre, Altas y Bajas entre Pagos</h2>
    <% Html.BeginForm("exportarComparacionPlanillas", "PlanillasPago")%>
    <div>
        <% Html.DevExpress().FormLayout(
               Sub(frmLayoutComparacion)
                   frmLayoutComparacion.Name = "frmLayoutComparacion"
                   frmLayoutComparacion.Items.AddGroupItem(
                       Sub(groupComparacion)
                           groupComparacion.Caption = "Planillas"
                           groupComparacion.Items.Add(
                               Sub(item)
                                   item.Caption = "Pagos"
                                   item.HelpText = "Debe de seleccionar solamente dos pagos."
                                   item.SetNestedContent(
                                       Sub()
                                           Html.RenderAction("pv_cbxGridPagos", "Shared")
                                           Html.DevExpress().Label(
                                               Sub(lbl)
                                                   lbl.Width = 260
                                                   lbl.Text = "Los pagos seleccionados se mostraran año-número, año-número."
                                               End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                       End Sub)
               End Sub).GetHtml()%>
    </div>
    <br />
    <div>
         <% Html.DevExpress().FormLayout(
               Sub(frmLayoutButtons)
                   frmLayoutButtons.Name = "frmLayoutButtons"
                   frmLayoutButtons.ColCount = 3
                   frmLayoutButtons.Width = Unit.Percentage(50)
                   frmLayoutButtons.Items.Add(
                       Sub(item)
                           item.ShowCaption = DefaultBoolean.False
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().Button(
                                       Sub(btnConsultar)
                                           btnConsultar.Name = "btnConsultarHogPagados"
                                           btnConsultar.UseSubmitBehavior = False
                                           btnConsultar.Text = "Consultar"
                                           btnConsultar.ClientSideEvents.Click = "btnConsultarComparacionClick"
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
                   frmLayoutButtons.Items.Add(
                       Sub(item)
                           item.ShowCaption = DefaultBoolean.False
                           item.ClientVisible = False
                           item.Name = "itemBtnRazonCaida"
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().Button(
                                       Sub(btn)
                                           btn.Name = "btnRazonCaidaHogares"
                                           btn.ClientSideEvents.Click = "btnRazonCaidaHogaresClick"
                                           btn.Text = "Razón de Caída"
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
               End Sub).GetHtml()%>
    </div>
    <br />
    <div id="divMapa"></div>
    <br />
    <div>
        <% Html.RenderAction("pv_ControlesExportar", "Shared")%>
    </div>
    <div id="divGridView"></div>
    <br />
    <div id="divChart" style="display: none;">
        <% Html.RenderAction("pv_ControlesTiposGrafico", "Shared") %>   
        <% Html.RenderPartial("pv_ControlesExportChart", "Shared")%>
        <br />
        <div>
            <% Html.RenderPartial("pv_chrComparacionPlanillas")%>
        </div> 
    </div>
    <% Html.DevExpress().TextBox(
           Sub(txt)
               txt.Name = "txtPagosSeleccionados2"
               txt.Text = "text"
               txt.ClientVisible = False
           End Sub).GetHtml()%>
    <% Html.EndForm()%>
    <div id="myModal" class="modal2">
        <!-- Modal content -->
        <div class="modal-content">
            <div class="modal-header">
                <span id="spanClose" class="close">&times</span>
                <h2>Razón de Caída de Hogares</h2>
            </div>
            <div class="modal-body">
                <br />
                <% Html.BeginForm("exportarRazonCaida", "PlanillasPago")%>
                    <% Html.DevExpress().TextBox(
                            Sub(txt)
                                txt.Name = "txtPagosSeleccionados"
                                txt.Text = "text"
                                txt.ClientVisible = False
                            End Sub).GetHtml()%>
                    <div>
                        <% Html.DevExpress().FormLayout(
                               Sub(frmLayoutExport)
                                   frmLayoutExport.Name = "floRazonesExclusion"
                                   frmLayoutExport.ColCount = 2
                                   frmLayoutExport.Items.Add(
                                       Sub(item)
                                           item.Caption = "Exportar a"
                                           item.SetNestedContent(
                                              Sub()
                                                  Html.DevExpress().ComboBox(
                                                      Sub(cbx)
                                                          cbx.Name = "cbxExpotarRazonesExclusion"
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
                                                          btnConsultar.Name = "btnExportarRazonesExclusion"
                                                          btnConsultar.UseSubmitBehavior = True
                                                          btnConsultar.Text = "Exportar"
                                                      End Sub).GetHtml()
                                              End Sub)
                                       End Sub)
                               End Sub).GetHtml()%>
                    </div>
                <br />
                    <div id="divRazonCaida"></div>
                <% Html.EndForm()%>
            </div>
            <br />
            <div class="modal-footer">
                <br />
                <h3>SIG | Módulo de Minería de Datos</h3>
                <br />
            </div>
        </div>
    </div>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>
</asp:Content>
