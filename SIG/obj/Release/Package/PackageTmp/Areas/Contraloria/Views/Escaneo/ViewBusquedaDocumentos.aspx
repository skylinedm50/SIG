<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Contraloria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Busqueda de Documentos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <link type="text/css" rel="stylesheet" href="/Areas/Contraloria/Style/styles_contraloria.css" />
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/escaneo.js")%>'></script>

<h2>Busqueda de Documentos</h2>
    <div id="divControles">
        <% Html.DevExpress().FormLayout(
               Sub(settings2)
                   settings2.Name = "frmLayoutControles"
                   settings2.ColCount = 2
                   settings2.SettingsItems.VerticalAlign = FormLayoutVerticalAlign.Top
                   settings2.Items.Add(
                       Sub(item2)
                           item2.ShowCaption = DefaultBoolean.False
                           item2.SetNestedContent(
                               Sub()
                                   Html.DevExpress().FormLayout(
                                       Sub(settings)
                                           settings.Name = "frmLayoutLeitz"
                                           settings.ColCount = 1
                                           settings.Items.AddGroupItem(
                                               Sub(grupo)
                                                   grupo.Caption = "Filtro"
                                                   grupo.Items.Add(
                                                       Sub(item)
                                                           item.Caption = "Período"
                                                           item.SetNestedContent(
                                                               Sub()
                                                                   Html.RenderAction("PartialCbxPeriodo")
                                                               End Sub)
                                                       End Sub)
                                                   grupo.Items.Add(
                                                       Sub(item)
                                                           item.Caption = "Fondo"
                                                           item.SetNestedContent(
                                                               Sub()
                                                                   Html.RenderPartial("PartialCbxFondo")
                                                               End Sub)
                                                       End Sub)
                                               End Sub)
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
                   settings2.Items.Add(
                       Sub(item2)
                           item2.ShowCaption = DefaultBoolean.False
                           item2.SetNestedContent(
                               Sub()
                                   Html.DevExpress().FormLayout(
                                       Sub(settings)
                                           settings.Name = "frmLayoutDocumento"
                                           settings.Items.AddGroupItem(
                                               Sub(grupo)
                                                   grupo.Caption = "Otros Parámetros"
                                                   grupo.Items.Add(
                                                   Sub(item)
                                                       item.Caption = "Departamento"
                                                       item.SetNestedContent(
                                                           Sub()
                                                               Html.DevExpress().ComboBox(
                                                                   Sub(cbxDpto)
                                                                       cbxDpto.Name = "cbxDepto"
                                                                       cbxDpto.Properties.DropDownStyle = DropDownStyle.DropDownList
                                                                       'cbxDpto.Properties.TextField = "nom_depto"
                                                                       'cbxDpto.Properties.ValueField = "cod_depto"
                                                                       cbxDpto.Properties.TextField = "desc_departamento"
                                                                       cbxDpto.Properties.ValueField = "cod_departamento"
                                                                       cbxDpto.Properties.NullText = "Seleccione un Departamento"
                                                                   End Sub).BindList(ViewData("dpto")).GetHtml()
                                                           End Sub)
                                                   End Sub)
                                                   grupo.Items.Add(
                                                       Sub(item)
                                                           item.Caption = "Banco"
                                                           item.SetNestedContent(
                                                               Sub()
                                                                   Html.DevExpress().ComboBox(
                                                                   Sub(cbxDpto)
                                                                       cbxDpto.Name = "cbxBanco"
                                                                       cbxDpto.Properties.DropDownStyle = DropDownStyle.DropDownList
                                                                       cbxDpto.Properties.TextField = "nombre_banco"
                                                                       cbxDpto.Properties.ValueField = "cod_banco"
                                                                       cbxDpto.Properties.NullText = "Seleccione un Banco"
                                                                   End Sub).BindList(ViewData("banco")).GetHtml()
                                                               End Sub)
                                                       End Sub)
                                                   grupo.Items.Add(
                                                       Sub(item)
                                                           item.Caption = "Tipo"
                                                           item.SetNestedContent(
                                                               Sub()
                                                                   Html.DevExpress().RadioButtonList(
                                                                       Sub(rbTipo)
                                                                           rbTipo.Name = "rbTipo"
                                                                           rbTipo.Properties.RepeatLayout = RepeatLayout.Table
                                                                           rbTipo.Properties.RepeatDirection = RepeatDirection.Horizontal
                                                                           rbTipo.Properties.RepeatColumns = 2
                                                                           rbTipo.ControlStyle.Border.BorderWidth = Unit.Pixel(0)
                                                                           rbTipo.Properties.ValueField = "cod_tipo_documento"
                                                                           rbTipo.Properties.TextField = "nombre_tipo_documento"
                                                                           rbTipo.Properties.ClientSideEvents.ValueChanged = "rbTipoValueChange"
                                                                       End Sub).BindList(ViewData("documentos")).GetHtml()
                                                               End Sub)
                                                       End Sub)
                                                   'grupo.Items.AddGroupItem(
                                                   '     Sub(grupo2)
                                                   '         grupo2.Caption = "Fechas de Pago de Reporte"
                                                   '         grupo2.Name = "grupoFecha"
                                                   '         'grupo2.Visible = False
                                                   '         grupo2.ClientVisible = False
                                                   '         grupo2.Items.Add(
                                                   '             Sub(fchIncio)
                                                   '                 fchIncio.Caption = "Inicio"
                                                   '                 fchIncio.ShowCaption = DefaultBoolean.False
                                                   '                 'fchIncio.Visible = False
                                                   '                 fchIncio.SetNestedContent(
                                                   '                     Sub()
                                                   '                         ViewContext.Writer.Write("<table><tr><td>Desde:</td><td style='padding-right:5px;'>")
                                                   '                         Html.DevExpress().DateEdit(
                                                   '                             Sub(dtInicio)
                                                   '                                 dtInicio.Name = "dtInicio"
                                                   '                                 dtInicio.Properties.UseMaskBehavior = True
                                                   '                                 dtInicio.Properties.EditFormat = EditFormat.Custom
                                                   '                                 dtInicio.Properties.NullText = "dd/MM/yyyy"
                                                   '                                 dtInicio.Properties.EditFormatString = "dd/MM/yyyy"
                                                   '                                 dtInicio.Width = 100
                                                   '                                 dtInicio.Properties.ClientSideEvents.ValueChanged = "function(s, e) { validarFecha('inicio'); }"
                                                   '                             End Sub).GetHtml()
                                                   '                         ViewContext.Writer.Write("</td><td>Hasta:</td><td>")
                                                   '                         Html.DevExpress().DateEdit(
                                                   '                             Sub(dtFin)
                                                   '                                 dtFin.Name = "dtFin"
                                                   '                                 dtFin.Properties.UseMaskBehavior = True
                                                   '                                 dtFin.Properties.EditFormat = EditFormat.Custom
                                                   '                                 dtFin.Properties.NullText = "dd/MM/yyyy"
                                                   '                                 dtFin.Properties.EditFormatString = "dd/MM/yyyy"
                                                   '                                 dtFin.Width = 100
                                                   '                                 dtFin.Properties.ClientSideEvents.ValueChanged = "function(s, e) { validarFecha('fin'); }"
                                                   '                             End Sub).GetHtml()
                                                   '                         ViewContext.Writer.Write("</td></tr></table>")
                                                   '                     End Sub)
                                                   '             End Sub)
                                                   '     End Sub)
                                               End Sub)
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
               End Sub).GetHtml()%>
        <div>
            <% Html.DevExpress().Button(
                   Sub(settings)
                       settings.Name = "btnConsultar"
                       settings.Text = "Consultar"
                       settings.ClientSideEvents.Click = "btnConsultarClick"
                       settings.ControlStyle.CssClass = "buttonBusqueda"
                   End Sub).GetHtml()%>
        </div>
    </div>
    <div id="divGridView">
        <% Html.RenderPartial("PartialGridMaestroLeitz")%>
    </div>

</asp:Content>
