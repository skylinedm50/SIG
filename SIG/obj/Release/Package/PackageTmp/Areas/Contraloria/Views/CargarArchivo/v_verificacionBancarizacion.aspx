<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Contraloria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
v_verificacionBancarizacion
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
                    New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
                    New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
              New Script With {.ExtensionSuite = ExtensionSuite.GridView},
              New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <link type="text/css" rel="stylesheet" href="/Areas/Contraloria/Style/styles_contraloria.css" />
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/cargaArchivo.js")%>'></script>

    <h2>Verificación de Archivos de Bancarización</h2>
    <% Html.BeginForm("", "")%>
    <div>
        <% Html.DevExpress().FormLayout(
              Sub(frmLayout)
                  frmLayout.Name = "floControles"
                  frmLayout.ColCount = 3
                  frmLayout.Items.Add(
              Sub(item)
                  item.ShowCaption = DefaultBoolean.False
                  item.ColSpan = 3
                  item.SetNestedContent(
                      Sub()
                          'Html.RenderPartial("pv_fmArchivos")
                          Html.RenderAction("pv_fmArchivos")
                      End Sub)
              End Sub)
                  frmLayout.Items.Add(
                  Sub(item)
                      item.ShowCaption = DefaultBoolean.False
                      item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                      item.SetNestedContent(
                          Sub()
                              Html.DevExpress().Button(
                                  Sub(bnt)
                                      bnt.Name = "btnSincronizar"
                                      bnt.Text = "Sincronizar Carpetas"
                                      bnt.ClientSideEvents.Click = "btnSincronizarClick"
                                  End Sub).GetHtml()
                          End Sub)
                  End Sub)
                  frmLayout.Items.Add(
                  Sub(item)
                      item.ShowCaption = DefaultBoolean.False
                      item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                      item.SetNestedContent(
                          Sub()
                              Html.DevExpress().Button(
                                  Sub(bnt)
                                      bnt.Name = "btnHistorial"
                                      bnt.Text = "Mover al Historial"
                                      bnt.ClientSideEvents.Click = "btnHistorialClick"
                                  End Sub).GetHtml()
                          End Sub)
                  End Sub)
                  frmLayout.Items.Add(
                  Sub(item)
                      item.ShowCaption = DefaultBoolean.False
                      item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                      item.SetNestedContent(
                          Sub()
                              Html.DevExpress().Button(
                                  Sub(bnt)
                                      bnt.Name = "btnProcesar"
                                      bnt.Text = "Procesar Archivos"
                                      bnt.ClientSideEvents.Click = "btnProcesarClick"
                                  End Sub).GetHtml()
                          End Sub)
                  End Sub)
                  frmLayout.Items.Add(
                  Sub(item)
                      item.ShowCaption = DefaultBoolean.False
                      item.HorizontalAlign = FormLayoutHorizontalAlign.Left
                      item.ColSpan = 3
                      item.SetNestedContent(
                          Sub()
                              Html.DevExpress().Label(
                                  Sub(lbl)
                                      lbl.Text = "NOTA: Al procesar archivos nuevos estos reemplazarán los previos que pertenezcan al mismo pago y al mismo banco."
                                  End Sub).GetHtml()
                          End Sub)
                  End Sub)
              End Sub).GetHtml()%>
    </div>
    <div>
        <div id="divGdvArchivos"></div>
    </div>
    <div id="modal">
        <% Html.DevExpress().PopupControl(
                        Sub(pcModal)
                            pcModal.Name = "pcModal"
                            pcModal.Width = 320
                            pcModal.CloseAction = CloseAction.CloseButton
                            pcModal.CloseOnEscape = True
                            pcModal.PopupAnimationType = AnimationType.Fade
                            pcModal.HeaderText = "Datos para guardado de Historial"
                            pcModal.Modal = True
                            pcModal.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                            pcModal.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                            pcModal.SetContent(
                            Sub()
                                Html.DevExpress().FormLayout(
                                Sub(frmModal)
                                    frmModal.Name = "frmModal"
                                    frmModal.ColCount = 2
                                    frmModal.Items.Add(
                                    Sub(item)
                                        item.Caption = "Año"
                                        item.ColSpan = 2
                                        item.SetNestedContent(
                                        Sub()
                                            Html.DevExpress().ComboBox(
                                            Sub(cbx)
                                                cbx.Name = "cbxAños"
                                                cbx.Properties.ClientSideEvents.SelectedIndexChanged = "cbxAñosChanged"
                                                cbx.Properties.NullText = "Seleccione un año"
                                                cbx.Properties.EnableClientSideAPI = True
                                            End Sub).GetHtml()
                                        End Sub)
                                    End Sub)
                                    frmModal.Items.Add(
                                    Sub(item)
                                        item.Caption = "Pago"
                                        item.ColSpan = 2
                                        item.SetNestedContent(
                                        Sub()
                                            Html.DevExpress().ComboBox(
                                            Sub(cbx)
                                                cbx.Name = "cbxPagos"
                                                cbx.Properties.EnableClientSideAPI = True
                                                cbx.Properties.NullText = "Seleccione un Pago"
                                            End Sub).GetHtml()
                                        End Sub)
                                    End Sub)
                                    frmModal.Items.Add(
                                    Sub(item)
                                        item.ShowCaption = DefaultBoolean.False
                                        item.SetNestedContent(
                                        Sub()
                                            Html.DevExpress().Button(
                                           Sub(btn As ButtonSettings)
                                               btn.Name = "btnMover"
                                               btn.Text = "Mover"
                                               btn.ClientSideEvents.Click = "btnMoverClick"
                                           End Sub).Render()
                                        End Sub)
                                    End Sub)
                                    frmModal.Items.Add(
                                    Sub(item)
                                        item.ShowCaption = DefaultBoolean.False
                                        item.SetNestedContent(
                                        Sub()
                                            Html.DevExpress().Button(
                                             Sub(btn As ButtonSettings)
                                                 btn.Name = "btnCancelar"
                                                 btn.Text = "Cancelar"
                                                 btn.ClientSideEvents.Click = "function(s, e){ pcModal.Hide(); }"
                                             End Sub).Render()
                                        End Sub)
                                    End Sub)
                                End Sub).GetHtml()
                            End Sub)
                        End Sub).GetHtml() %>
    </div>
    <% Html.EndForm()%>
    <style type="text/css">
        .modal
        {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            left: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
        .center
        {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            /*background-color: White;*/
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }
        .center img
        {
            height: 128px;
            width: 128px;
        }
    </style>
    <div class="modal" style="display: none">
        <div class="center">
            <img alt="" src="<%: ResolveUrl("~/Areas/Contraloria/Images/loader.gif")%>" />
        </div>
    </div>
    <script>
        var data = JSON.parse('<%= ViewData("data").ToString() %>'),
            años = new Array(),
            pagos = new Array()

        $(() => {

            for (let i = 0, len = data.length; i < len; i++) {
                if (i == 0) {
                    cbxAños.AddItem(data[i].pag_anyo.toString(), data[i].pag_anyo)
                } else if (cbxAños.FindItemByText(data[i].pag_anyo) == null) {
                    cbxAños.AddItem(data[i].pag_anyo.toString(), data[i].pag_anyo)
                }
            }
        })
    </script>
</asp:Content>
