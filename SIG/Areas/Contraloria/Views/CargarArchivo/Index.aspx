<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Contraloria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="System.Data" %>

<%@ Import Namespace="Contraloría" %>
<%@ Import Namespace="DevExpress.XtraReports.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Cargar Archivo de Pago
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>

    <link type="text/css" rel="stylesheet" href="/Areas/Contraloria/Style/styles_contraloria.css" />

    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/cronometro.js")%>'></script>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/jquery.signalR-2.2.0.js")%>'></script>
    <script src="../../signalr/hubs"></script>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/cargaArchivo.js")%>'></script>
   <style>
       #content {
           width: 1100px;
       }
   </style>
    <h2>Cargar Archivos de Pago</h2>
    <% Html.BeginForm("exportToExcel", "CargarArchivo")%>
    <div>
        <div id="divControlesCarga" class="col_50">      
            <div>
                <p>Selecciones un archivo</p>
                <% Html.DevExpress().UploadControl(
                       Sub(upc)
                           upc.Name = "uc"
                           upc.CallbackRouteValues = New With {.Controller = "CargarArchivo", .Action = "UploadControlCallback"}
                           upc.FileInputCount = 1                           
                           upc.BrowseButton.Text = "Buscar"
                           upc.ValidationSettings.AllowedFileExtensions = New String() {".txt"}                           
                       End Sub).GetHtml()%>
            </div>
            <br />
            <div>
                <% Html.DevExpress().FormLayout(
                                Sub(frmLayout)
                                    frmLayout.Name = "frmLayout"
                                   'frmLayout.Attributes.
                                   'frmLayout.ColCount = 1
                                   frmLayout.Items.AddGroupItem(
                                        Sub(group)
                                            group.Caption = "Parámetros"
                                            group.ColCount = 1
                                           'group.ShowCaption = DefaultBoolean.False
                                           'group.GroupBoxDecoration = GroupBoxDecoration.None
                                           group.Items.Add(
                                                Sub(item)
                                                    item.Caption = "Nombre"
                                                    item.SetNestedContent(
                                                        Sub()
                                                            Html.DevExpress().TextBox(
                                                                Sub(settings)
                                                                    settings.Name = "txtNombre"
                                                                    settings.Properties.NullText = "Nombre de Carga"
                                                                End Sub).GetHtml()
                                                        End Sub)
                                                End Sub)
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
                                                                    'settings.Properties.ValueField = "cod_periodo"
                                                                    settings.Properties.ValueField = "pag_codigo"
                                                                     settings.Properties.ValueType = GetType(Integer)
                                                                     settings.Properties.NullText = "Seleccione un Periodo"

                                                                    'settings.Properties.Columns.Add("cod_periodo", "Nro Periodo").Width = Unit.Pixel(30)
                                                                    'settings.Properties.Columns.Add("nom_periodo", "Nombre Periodo")
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
                                                                   'settings.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                                                   'settings.CallbackRouteValues = New With {.Controller = "RecibosPagados", .Action = "PartialBancoView"}
                                                                   settings.Properties.DropDownStyle = DropDownStyle.DropDownList
                                                                    settings.Properties.TextField = "nombre_banco"
                                                                    settings.Properties.ValueField = "cod_banco"
                                                                    settings.Properties.NullText = "Seleccione un Banco"
                                                                    settings.Properties.ValueType = GetType(Integer)
                                                                   'settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { cbxSucursal.PerformCallback(); }"
                                                               End Sub).BindList(ViewData("bancos")).Render()
                                                        End Sub)
                                                End Sub)
                                            group.Items.Add(
                                                Sub(item)
                                                    item.Caption = "Formato de Fecha"
                                                    item.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                    item.SetNestedContent(
                                                        Sub()
                                                           'Html.DevExpress().ComboBox(
                                                           '    Sub(settings)
                                                           '        settings.Name = "cbxTipoFecha"
                                                           '        settings.Width = 180
                                                           '        settings.Properties.DropDownStyle = DropDownStyle.DropDownList
                                                           '        settings.Properties.Items.Add("dd/mm/yyyy")
                                                           '        settings.Properties.Items.Add("mm/dd/yyyy")
                                                           '    End Sub).Render()
                                                           Html.DevExpress().RadioButtonList(
                                                                Sub(settings)
                                                                    settings.Name = "rbtTipoFecha"
                                                                    settings.Properties.RepeatColumns = 2
                                                                    settings.Properties.RepeatDirection = RepeatDirection.Horizontal
                                                                    settings.Properties.RepeatLayout = RepeatLayout.Table

                                                                   'settings.Width = 180
                                                                   'settings.Properties.DropDownStyle = DropDownStyle.DropDownList
                                                                   settings.Properties.Items.Add("dd/mm/yyyy")
                                                                    settings.Properties.Items.Add("mm/dd/yyyy")
                                                                    settings.ControlStyle.Border.BorderStyle = BorderStyle.None

                                                                End Sub).Render()
                                                        End Sub)
                                                End Sub)
                                            group.Items.Add(
                                                Sub(item)
                                                    item.Caption = "Registros fuera de la fecha programada"
                                                    item.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                    item.SetNestedContent(
                                                    Sub()
                                                        Html.DevExpress().RadioButtonList(
                                                           Sub(setting)
                                                               setting.Name = "rbtFchFueraProgramacion"
                                                               setting.Properties.RepeatColumns = 2
                                                               setting.Properties.RepeatDirection = RepeatDirection.Horizontal
                                                               setting.Properties.RepeatLayout = RepeatLayout.Table
                                                               setting.Properties.Items.Add("Permitir")
                                                               setting.Properties.Items.Add("Denegar")
                                                               setting.ControlStyle.Border.BorderStyle = BorderStyle.None
                                                           End Sub).Render()
                                                    End Sub)
                                                End Sub)
                                            group.Items.Add(
                                                Sub(item)
                                                    item.Caption = "Tipo de Carga"
                                                    item.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle

                                                    item.SetNestedContent(
                                                    Sub()
                                                        Html.DevExpress().RadioButtonList(
                                                           Sub(setting)
                                                               setting.Name = "rbtTipoCarga"

                                                               setting.Properties.RepeatColumns = 2
                                                               setting.Properties.RepeatDirection = RepeatDirection.Horizontal
                                                               setting.Properties.RepeatLayout = RepeatLayout.Table
                                                               setting.Properties.Items.Add("Rapida")
                                                               setting.Properties.Items.Add("Normal")
                                                               setting.ControlStyle.Border.BorderStyle = BorderStyle.None
                                                           End Sub).Render()
                                                    End Sub)
                                                End Sub)
                                        End Sub)
                                End Sub).GetHtml()%>
            </div>
            <br />
            <div id="divProgress">
                <% Html.DevExpress().ProgressBar(
                       Sub(settings)
                           settings.Name = "pbCarga"
                           settings.Width = System.Web.UI.WebControls.Unit.Pixel(200)
                           settings.ClientVisible = False
                       End Sub).GetHtml()%>
                <% Html.DevExpress().Label(
                       Sub(settings)
                           settings.Name = "lblCronometro"
                           settings.Text = "00:00:00"
                           settings.ClientVisible = False
                       End Sub).GetHtml()%>
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <% Html.DevExpress().Button(
                               Sub(settings)
                                   settings.Name = "btnCargaPreliminar"
                                   settings.Text = "Carga Preliminar"
                                   settings.ClientSideEvents.Click = "function(s, e) { verificar(); }"
                                   settings.ControlStyle.CssClass = "buttonCarga"
                                   settings.Width = System.Web.UI.WebControls.Unit.Pixel(128)
                                   'settings.ControlStyle.CssClass = "button"
                               End Sub).Render()%>
                        </td>
                        <td>
                            <% Html.DevExpress().Button(
                               Sub(settings)
                                   settings.Name = "btnCargar"
                                   settings.Text = "Carga"
                                   'settings.ClientSideEvents.Click = "function(s, e) { $('#divGridView').load('/CargarArchivo/PartialGridView'); }"
                                   settings.ClientSideEvents.Click = "function(s, e) { cargar(); }"
                                   'settings.ControlStyle.CssClass = "button"
                                   settings.Width = System.Web.UI.WebControls.Unit.Pixel(128)
                                   'settings.RouteValues = New With {Key .Controller = "CargarArchivo", Key .Action = "guardarCarga"}
                                   settings.ControlStyle.CssClass = "buttonCarga"
                               End Sub).Render()%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <% Html.DevExpress().Button(
                               Sub(settings)
                                   settings.Name = "btnExportarExcel"
                                   settings.Text = "Exportar a Excel"
                                   'settings.ClientSideEvents.Click = "function(s, e) {  }"
                                   settings.ControlStyle.CssClass = "buttonCarga"
                                   settings.Width = System.Web.UI.WebControls.Unit.Pixel(128)
                                   'settings.RouteValues = New With {Key .Controller = "CargarArchivo", Key .Action = "ExportToExcel"}
                                   'settings.ControlStyle.CssClass = "button"
                                   settings.UseSubmitBehavior = True
                               End Sub).Render()%>
                        </td>
                        <td>
                            <% Html.DevExpress().Button(
                               Sub(settings)
                                   settings.Name = "btnDetalleCarga"
                                   settings.Text = "Ver Detalle"
                                   settings.ClientSideEvents.Click = "function(s, e) { recargarGridView() }"
                                   settings.ControlStyle.CssClass = "buttonCarga"
                                   settings.Width = System.Web.UI.WebControls.Unit.Pixel(128)
                               End Sub).Render()%>
                        </td>
                    </tr>
                </table>
            </div>        
            <br />
        </div>
        <br />
        <br />
        <div id="divResumen" class="col_50">
            <% Html.RenderAction("PartialMemo")%>
        </div>
    </div>    
    <br />  
    <br />
    <br />
    <div id="divGridView" style="display:inline-block; margin-top:20px;">
        <% Html.RenderPartial("PagosGridViewPartial")%>
    </div>
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
