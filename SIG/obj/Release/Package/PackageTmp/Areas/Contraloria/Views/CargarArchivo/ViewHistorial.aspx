<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Contraloria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
ViewHistorial
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="loadingIco"  style="top:0; left:0; width:150%; height:100%; position:absolute; display:none;" >
            <img style="top:50%; position:relative; left:50%; height: 50px; width:50px;" src='<%: ResolveUrl("~/Areas/Incorporaciones/Content/Images/loading.gif")%>'/>       
    </div>
   
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>

    <link type="text/css" rel="stylesheet" href="/Areas/Contraloria/Style/styles_contraloria.css" />

    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/cargaArchivo.js")%>'></script>
    
    <h2>Historial de Cargas de Archivo</h2>
    <% Html.BeginForm("exportToExcel", "CargarArchivo")%>
    <div>
        <div id="divControles" class="col_33">
            <% Html.DevExpress().FormLayout(
                Sub(settings)
                    settings.Name = "frmLayoutTipo"
                    settings.ColCount = 1
                    settings.Items.AddGroupItem(
                        Sub(group)
                            group.Caption = "Parámetros de Filtración"
                            group.Items.Add(
                                    Sub(item)
                                        item.Caption = "Tipo"
                                        item.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                        item.SetNestedContent(
                                            Sub()
                                                Html.DevExpress().RadioButtonList(
                                                    Sub(rb)
                                                        rb.Name = "rbTipo"
                                                        rb.Properties.RepeatLayout = RepeatLayout.Table
                                                        rb.Properties.RepeatDirection = RepeatDirection.Horizontal
                                                        rb.Properties.RepeatColumns = 3
                                                        rb.ControlStyle.Border.BorderWidth = Unit.Pixel(0)
                                                        'rb.Properties.Items.Add("Todo", "todo").Selected = True
                                                        rb.Properties.Items.Add("Pre Carga", "preCarga")
                                                        rb.Properties.Items.Add("Carga", "carga")
                                                        'rb.Properties.ClientSideEvents.ValueChanged = "FiltroValueChange"
                                                    End Sub).GetHtml()
                                            End Sub)
                                    End Sub)
                            group.Items.Add(
                                    Sub(item)
                                        item.Caption = "Periodo"
                                        item.SetNestedContent(
                                            Sub()
                                                Html.DevExpress().ComboBox(
                                                    Sub(cbx)
                                                        cbx.Name = "cbxPeriodo"
                                                        cbx.Width = 180
                                                        cbx.Properties.DropDownWidth = 550
                                                        cbx.Properties.DropDownStyle = DropDownStyle.DropDownList
                                                        'cbx.CallbackRouteValues = New With {Key .Controller = "Reportes", Key .Action = "PartialPeriodosView"}
                                                        cbx.Properties.CallbackPageSize = 30
                                                        cbx.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                                        cbx.Properties.TextFormatString = "{1}"
                                                        'cbx.Properties.ValueField = "cod_periodo"
                                                        cbx.Properties.ValueField = "pag_codigo"
                                                        cbx.Properties.ValueType = GetType(Integer)
                                                        cbx.Properties.NullText = "Seleccione un Periodo"
        
                                                        'cbx.Properties.Columns.Add("cod_periodo", "Nro Periodo").Width = Unit.Pixel(30)
                                                        'cbx.Properties.Columns.Add("nom_periodo", "Nombre Periodo")
                                                        cbx.Properties.Columns.Add("pag_codigo", "Nro Periodo").Width = Unit.Pixel(30)
                                                        cbx.Properties.Columns.Add("pag_nombre", "Nombre Periodo")
                                                    End Sub).BindList(ViewData("periodos")).GetHtml()
                                            End Sub)
                                    End Sub)
                            group.Items.Add(
                                    Sub(item)
                                        item.Caption = "Banco"
                                        item.SetNestedContent(
                                            Sub()
                                                Html.DevExpress().ComboBox(
                                                    Sub(cbx)
                                                        cbx.Name = "cbxBanco"
                                                        cbx.Width = 180
                                                        'cbx.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                                        'cbx.CallbackRouteValues = New With {.Controller = "RecibosPagados", .Action = "PartialBancoView"}
                                                        cbx.Properties.DropDownStyle = DropDownStyle.DropDownList
                                                        cbx.Properties.TextField = "nombre_banco"
                                                        cbx.Properties.ValueField = "cod_banco"
                                                        cbx.Properties.NullText = "Seleccione un Banco"
                                                        cbx.Properties.ValueType = GetType(Integer)
                                                        'cbx.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { cbxSucursal.PerformCallback(); }"
                                                    End Sub).BindList(ViewData("bancos")).Render()
                                            End Sub)
                                    End Sub)
                            group.Items.Add(
                                    Sub(item)
                                        item.Caption = "Banco"
                                        item.ShowCaption = DefaultBoolean.False
                                        item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                        item.SetNestedContent(
                                            Sub()
                                                Html.DevExpress().Button(
                                                   Sub(bnt)
                                                       bnt.Name = "btnConsultar"
                                                       bnt.Text = "Consultar"
                                                       bnt.ClientSideEvents.Click = "btnConsultarClick"                                                       
                                                   End Sub).GetHtml()
                                            End Sub)
                                    End Sub)
                        End Sub)
                End Sub).GetHtml()%>
        </div>
        <div class="col_66">
            <div id="divGridCargas">
                <% Html.RenderPartial("PartialGridViewCargas") %>
            </div>
            <br />
            <div style="width:110%;">
                <% Html.DevExpress().Button(
                       Sub(settings)
                           settings.Name = "btnRegistros"
                           settings.Text = "Ver Registros"
                           settings.ClientSideEvents.Click = "btnRegistrosClick"
                           settings.ControlStyle.CssClass = "buttonCarga"
                       End Sub).GetHtml()%>
                <% Html.DevExpress().Button(
                       Sub(settings)
                           settings.Name = "btnExportar"
                           settings.Text = "Exportar Pagos Excel"                           
                           settings.UseSubmitBehavior = True
                           settings.ControlStyle.CssClass = "buttonCarga"
                       End Sub).GetHtml()%>
                <%
                    Html.DevExpress().Button(
                        Sub(setting)
                            setting.Name = "btnExportarHistorial"
                            setting.Text = "Exportar Historial de Carga"
                            setting.UseSubmitBehavior = False
                            setting.ClientSideEvents.Click = "Fnc_ExportarHistorialCarga"
                            setting.ControlStyle.CssClass = "buttonCarga"
                        End Sub).GetHtml()
                %>
                <% Html.DevExpress().Button(
                       Sub(settings)
                           settings.Name = "btnDescargar"
                           settings.Text = "Descargar Archivo"
                           settings.ClientSideEvents.Click = "btnDescargarClick"
                           settings.ControlStyle.CssClass = "buttonCarga"                           
                       End Sub).GetHtml()%>
            </div>
        </div>        
    </div>
    <br />
    <br />
    <div id="divGridView" class="col_100">
        <% Html.RenderPartial("PagosGridViewPartial")%>
    </div>
    <% Html.EndForm()%>
</asp:Content>
