<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Contraloria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Carga de Bancarización
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
              New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
              New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
              New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <% Html.DevExpress().GetScripts(
              New Script With {.ExtensionSuite = ExtensionSuite.GridView},
              New Script With {.ExtensionSuite = ExtensionSuite.Editors},
              New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <link type="text/css" rel="stylesheet" href="/Areas/Contraloria/Style/styles_contraloria.css" />
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/cargaArchivo.js")%>'></script>

    <h2>Carga de Archivos de Bancarización</h2>
    <% Html.BeginForm("exportCargaBancarizacion", "CargarArchivo")%>
    <div>
        <% Html.DevExpress().FormLayout(
              Sub(frmLayout)
                  frmLayout.Name = "floControles"
                  frmLayout.Items.AddGroupItem(
                  Sub(group)
                      group.Caption = "Parámetros"
                      group.ColCount = 2
                      group.Items.Add(
                      Sub(item)
                          item.Caption = "Período"
                          item.SetNestedContent(
                              Sub()
                                  Html.DevExpress().ComboBox(
                                          Sub(settings)
                                              settings.Name = "cbxPeriodo"
                                              settings.Width = 180
                                              settings.Properties.DropDownWidth = 550
                                              settings.Properties.DropDownStyle = DropDownStyle.DropDownList
                                             'settings.CallbackRouteValues = New With {Key .Controller = "Reportes", Key .Action = "PartialPeriodosView"}
                                             settings.Properties.CallbackPageSize = 30
                                              settings.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                              settings.Properties.TextFormatString = "{1}"
                                              settings.Properties.ValueField = "pag_codigo"
                                              settings.Properties.ValueType = GetType(Integer)
                                              settings.Properties.NullText = "Seleccione un Periodo"
                                              settings.Properties.Columns.Add("pag_codigo", "Nro Periodo").Width = Unit.Pixel(30)
                                              settings.Properties.Columns.Add("pag_nombre", "Nombre Periodo")
                                          End Sub).BindList(ViewData("periodos")).GetHtml()
                              End Sub)
                      End Sub)
                      group.Items.Add(
                      Sub(item)
                          item.Caption = "Banco"
                          item.SetNestedContent(
                              Sub()
                                  Html.DevExpress().ComboBox(
                                      Sub(settings)
                                          settings.Name = "cbxBanco"
                                          settings.Width = 180
                                          settings.Properties.DropDownStyle = DropDownStyle.DropDownList
                                          settings.Properties.TextField = "nombre_banco"
                                          settings.Properties.ValueField = "cod_banco"
                                          settings.Properties.NullText = "Seleccione un Banco"
                                          settings.Properties.ValueType = GetType(Integer)
                                      End Sub).BindList(ViewData("bancos")).Render()
                              End Sub)
                      End Sub)
                      group.Items.Add(
                          Sub(item)
                              item.ShowCaption = DefaultBoolean.False
                              item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                              item.SetNestedContent(
                                  Sub()
                                      Html.DevExpress().Button(
                                          Sub(bnt)
                                              bnt.Name = "btnArchivosBancarizacion"
                                              bnt.Text = "Ver Archivos"
                                              bnt.ClientSideEvents.Click = "btnArchivosBancarizacionClick"
                                          End Sub).GetHtml()
                                  End Sub)
                          End Sub)
                      group.Items.Add(
                          Sub(item)
                              item.ShowCaption = DefaultBoolean.False
                              item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                              item.SetNestedContent(
                                Sub()
                                    Html.DevExpress().Button(
                                      Sub(bnt)
                                                bnt.Name = "btnExportarCuadroArchivos"
                                                bnt.Text = "Exportar Cuadro de Archivos"
                                                bnt.ClientSideEvents.Click = "btnExportarCuadroArchivosClick"
                                            End Sub).GetHtml()
                                End Sub)
                          End Sub)
                  End Sub)
              End Sub).GetHtml()%>
    </div>
    <div>
        <% Html.DevExpress.FormLayout(
                  Sub(flo)
                      flo.Width = Unit.Percentage(130)
                      flo.Name = "floControles2"
                      flo.ColCount = 4
                      flo.Items.AddGroupItem(
                      Sub(group As MVCxFormLayoutGroup)
                          group.ColSpan = 3
                          group.ShowCaption = DefaultBoolean.False
                          group.ColCount = 3
                          group.GroupBoxDecoration = GroupBoxDecoration.None
                          group.Items.Add(
                              Sub(item)
                                  item.ShowCaption = DefaultBoolean.False
                                  item.ColSpan = 3
                                  item.SetNestedContent(
                                  Sub()
                                      ViewContext.Writer.Write("<div id='divGdvArchivos'>")
                                      Html.RenderPartial("pv_gdvArchivosBancarizacion")
                                      ViewContext.Writer.Write("</div>")
                                  End Sub)
                              End Sub)
                          group.Items.Add(
                              Sub(item)
                                  item.ShowCaption = DefaultBoolean.False
                                  item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                  item.SetNestedContent(
                                  Sub()
                                      Html.DevExpress().Button(
                                          Sub(btn As ButtonSettings)
                                              btn.Name = "btnResumen"
                                              btn.Text = "Ver Resumen"
                                              btn.ClientSideEvents.Click = "btnResumenClick"
                                          End Sub).Render()
                                  End Sub)
                              End Sub)
                          group.Items.Add(
                              Sub(item)
                                  item.ShowCaption = DefaultBoolean.False
                                  item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                  item.SetNestedContent(
                                  Sub()
                                      Html.DevExpress().Button(
                                          Sub(btn As ButtonSettings)
                                              btn.Name = "btnDetalle"
                                              btn.Text = "Ver Detalle"
                                              btn.ClientSideEvents.Click = "btnDetalleClick"
                                          End Sub).Render()
                                  End Sub)
                              End Sub)
                          group.Items.Add(
                              Sub(item)
                                  item.ShowCaption = DefaultBoolean.False
                                  item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                  item.SetNestedContent(
                                  Sub()
                                      Html.DevExpress().Button(
                                          Sub(btn As ButtonSettings)
                                              btn.Name = "btnConsolidadoCarga"
                                              btn.Text = "Consolidado de Carga"
                                              btn.ClientEnabled = False
                                              btn.ClientSideEvents.Click = "btnConsolidadoCargaClick"
                                          End Sub).Render()
                                  End Sub)
                              End Sub)
                      End Sub)
                      flo.Items.Add(
                          Sub(item)
                              item.ColSpan = 1
                              item.Caption = "Resumen"
                              item.CaptionSettings.Location = LayoutItemCaptionLocation.Top
                              item.VerticalAlign = FormLayoutVerticalAlign.Top
                              item.SetNestedContent(
                              Sub()
                                  ViewContext.Writer.Write("<div id='divResumen'>")
                                  Html.RenderPartial("pv_memoBancarizacion")
                                  ViewContext.Writer.Write("</div>")
                              End Sub)
                          End Sub)
                  End Sub).GetHtml() %>
    </div>
    <br />
    <div id="divContenedorGrid" style="display:none">
        <div>
            <% Html.DevExpress().Button(
                  Sub(settings)
                      settings.Name = "btnExportarGrid"
                      settings.Text = "Exportar a Excel"
                      settings.Width = Unit.Pixel(128)
                      settings.ClientSideEvents.Click = "btnExportarGridClick"
                  End Sub).Render()%>
        </div>
        <br />
        <div id="divGridView"></div>
    </div>
    <br />
    <div id="divContenedorPivot" style="display:none">
        <div>
            <% Html.DevExpress().FormLayout(
           Sub(flo)
               flo.Name = "floControles3"
               flo.ColCount = 3
               flo.Items.Add(
                   Sub(item)
                       item.ShowCaption = DefaultBoolean.False
                       item.SetNestedContent(
                           Sub()
                               Html.DevExpress().Button(
                                    Sub(settings)
                                        settings.Name = "btnCargarPagos"
                                        settings.Text = "Cargar Pagos"
                                        settings.ClientSideEvents.Click = "btnCargarPagosClick"
                                    End Sub).Render()
                           End Sub)
                   End Sub)
               flo.Items.Add(
                   Sub(item)
                       item.ShowCaption = DefaultBoolean.False
                       item.SetNestedContent(
                           Sub()
                               Html.DevExpress().Button(
                                  Sub(settings)
                                      settings.Name = "btnExportarExcel"
                                      settings.Text = "Exportar a Excel"
                                      settings.Width = Unit.Pixel(128)
                                      settings.UseSubmitBehavior = True
                                  End Sub).Render()
                           End Sub)
                   End Sub)
               flo.Items.Add(
                   Sub(item)
                       item.ShowCaption = DefaultBoolean.False
                       item.SetNestedContent(
                           Sub()
                               Html.DevExpress().Button(
                                  Sub(btn As ButtonSettings)
                                      btn.Name = "btnListadoTitulares"
                                      btn.Text = "Exportar Titulares"
                                      btn.ClientSideEvents.Click = "btnListadoTitularesClick"
                                  End Sub).Render()
                           End Sub)
                   End Sub)
           End Sub).GetHtml() %>
        </div>
        <br />
        <div id="divPivotGrid"></div>
    </div>
    <% Html.DevExpress().TextBox(
           Sub(txt)
               txt.Name = "txtPagoSeleccionado"
               txt.Text = "text"
               txt.ClientVisible = False
           End Sub).GetHtml()%>
    <% Html.EndForm()%>
    <style type="text/css">
        /*body
        {
            font-family: Arial;
            font-size: 10pt;
        }*/
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
</asp:Content>
