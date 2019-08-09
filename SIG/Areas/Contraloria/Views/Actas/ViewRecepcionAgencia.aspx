<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Contraloria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Acta de Recepción de Documentos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <%--<script src="../../Scripts/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/jspdf.plugin.split_text_to_size.js"></script>
    <script type="text/javascript" src="../../Scripts/jspdf.plugin.standard_fonts_metrics.js"></script>
    <script src="../../Scripts/actaRecepcionAgencia.js"></script>--%>

    <%--<script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/jspdf.js")%>'></script>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/jspdf.plugin.split_text_to_size.js")%>'></script>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/jspdf.plugin.standard_fonts_metrics.js")%>'></script>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/actaRecepcionAgencia.js")%>'></script>--%>

    <link type="text/css" rel="stylesheet" href="/Areas/Contraloria/Style/styles_contraloria.css" />

    <script type="text/javascript" src='/Areas/Contraloria/Scripts/jspdf.js'></script>
    <script type="text/javascript" src='/Areas/Contraloria/Scripts/jspdf.plugin.split_text_to_size.js'></script>
    <script type="text/javascript" src='/Areas/Contraloria/Scripts/jspdf.plugin.standard_fonts_metrics.js'></script>
    <script type="text/javascript" src='/Areas/Contraloria/Scripts/jspdf.plugin.total_pages.js'></script>
    <script type="text/javascript" src='/Areas/Contraloria/Scripts/actaRecepcionAgencia.js'></script>

    <script type="text/javascript" src="/Areas/Contraloria/Scripts/pdfmake.min.js"></script>
	<script type="text/javascript" src="/Areas/Contraloria/Scripts/vfs_fonts.js"></script>
    <script type="text/javascript">
        var usuario = '<%=ViewData("userName") %>';
    </script>

    <%--<style>
        .col_50 {
              width:48%;
              /*margin:0 2% 0 0;*/
              /*margin: 0 auto;*/
              float:left;
        }
    </style>--%>
<h2>Acta de Recepción de Documentos de Pago de Agencia</h2>
    <div id="contenidoActa" class="col_50" align="center">
        <% Html.DevExpress().FormLayout(
            Sub(settings)
                settings.Name = "FormLayout1"
                settings.ColCount = 1
                settings.Items.AddGroupItem(
                    Sub(group)
                        group.Caption = "Contenido del Acta"
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Dirigida a"
                                item.SetNestedContent(
                                    Sub()
                                        Html.DevExpress().TextBox(
                                            Sub(tbx)
                                                tbx.Name = "txtNombre"
                                                tbx.Properties.NullText = "Ingrese el nombre"
                                            End Sub).GetHtml()
                                    End Sub)
                            End Sub)
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Titulo"
                                item.SetNestedContent(
                                    Sub()
                                        Html.DevExpress().ComboBox(
                                            Sub(cbx)
                                                cbx.Name = "cbxGrado"
                                                cbx.Properties.Items.Add("Ingeniero")
                                                cbx.Properties.Items.Add("Licenciado")
                                                cbx.Properties.Items.Add("Doctor")
                                                cbx.Properties.Items.Add("Abogado")
                                                cbx.Properties.NullText = "Seleccione un titulo"
                                            End Sub).GetHtml()
                                    End Sub)
                            End Sub)
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Fondo"
                                item.SetNestedContent(
                                    Sub()
                                        Html.DevExpress().ComboBox(
                                            Sub(cbx)
                                                cbx.Name = "cbxFondo"
                                                'cbx.Properties.TextField = "nombre_fondo"
                                                'cbx.Properties.ValueField = "cod_fondo"
                                                cbx.Properties.TextField = "fond_nombre"
                                                cbx.Properties.ValueField = "fond_codigo"
                                                cbx.Properties.NullText = "Seleccione un fondo"
                                            End Sub).BindList(ViewData("fondos")).GetHtml()
                                    End Sub)
                            End Sub)
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Proyecto"
                                item.SetNestedContent(
                                    Sub()
                                        Html.DevExpress().ComboBox(
                                            Sub(cbx)
                                                cbx.Name = "cbxProyecto"
                                                cbx.Properties.Items.Add("PRAF/BANADESA")
                                                cbx.Properties.Items.Add("PRAF/BANRURAL")
                                                cbx.Properties.Items.Add("PRAF/OCCIDENTE")
                                                cbx.Properties.NullText = "Seleccione el proyecto"
                                            End Sub).GetHtml()
                                    End Sub)
                            End Sub)
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Institución Bancaria"
                                item.SetNestedContent(
                                    Sub()
                                        Html.DevExpress().ComboBox(
                                            Sub(cbx)
                                                cbx.Name = "cbxBanco"
                                                cbx.Properties.TextField = "nombre_banco"
                                                cbx.Properties.ValueField = "cod_banco"
                                                cbx.Properties.NullText = "Seleccione un banco"
                                                cbx.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { cbxSucursal.PerformCallback(); }"
                                            End Sub).BindList(ViewData("bancos")).GetHtml()
                                    End Sub)
                            End Sub)
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Agencia"
                                item.SetNestedContent(
                                    Sub()
                                        Html.RenderPartial("PartialSucursalView")
                                    End Sub)
                            End Sub)
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Departamento"
                                item.SetNestedContent(
                                    Sub()
                                        Html.DevExpress().ComboBox(
                                            Sub(cbx)
                                                cbx.Name = "cbxDpto"
                                                cbx.Properties.TextField = "desc_departamento"
                                                cbx.Properties.ValueField = "cod_departamento"
                                                cbx.Properties.NullText = "Seleccione un departamento"
                                            End Sub).BindList(ViewData("departamentos")).GetHtml()
                                    End Sub)
                            End Sub)
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Período"
                                item.SetNestedContent(
                                    Sub()
                                        Html.DevExpress().ComboBox(
                                            Sub(cbx)
                                                cbx.Name = "cbxPeriodo"
                                                'cbx.Properties.TextField = "nom_periodo"
                                                'cbx.Properties.ValueField = "cod_periodo"
                                                cbx.Properties.TextField = "pag_nombre"
                                                cbx.Properties.ValueField = "pag_codigo"
                                                cbx.Properties.NullText = "Seleccione un período"
                                            End Sub).BindList(ViewData("periodos")).GetHtml()
                                    End Sub)
                            End Sub)
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Inversión"
                                item.SetNestedContent(
                                    Sub()
                                        Html.DevExpress().TextBox(
                                            Sub(tbx)
                                                tbx.Name = "txtInversion"
                                                tbx.Properties.NullText = "Ingrese el monto"
                                            End Sub).GetHtml()
                                    End Sub)
                            End Sub)
                    End Sub)
                settings.Items.AddGroupItem(
                    Sub(group)
                        group.Caption = "Documentos"
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Nro. Actas de Cierre"
                                item.SetNestedContent(
                                    Sub()
                                        Html.DevExpress().SpinEdit(
                                            Sub(sne)
                                                sne.Name = "cbxActas"
                                                sne.Number = 0
                                                sne.Properties.MaxValue = 100
                                                sne.Properties.MinValue = 0
                                                sne.Width = 40
                                            End Sub).GetHtml()
                                    End Sub)
                            End Sub)
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Nro. Notas de Cargo"
                                item.SetNestedContent(
                                    Sub()
                                        Html.DevExpress().SpinEdit(
                                            Sub(sne)
                                                sne.Name = "cbxNotasCargo"
                                                sne.Number = 0
                                                sne.Properties.MaxValue = 100
                                                sne.Properties.MinValue = 0
                                                sne.Width = 40
                                            End Sub).GetHtml()
                                    End Sub)
                            End Sub)
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Nro. de Reportes"
                                item.SetNestedContent(
                                    Sub()
                                        Html.DevExpress().SpinEdit(
                                            Sub(sne)
                                                sne.Name = "cbxReportes"
                                                sne.Number = 0
                                                sne.Properties.MaxValue = 100
                                                sne.Properties.MinValue = 0
                                                sne.Width = 40
                                            End Sub).GetHtml()
                                    End Sub)
                            End Sub)
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Nro. de Recibos"
                                item.SetNestedContent(
                                    Sub()
                                        Html.DevExpress().SpinEdit(
                                            Sub(sne)
                                                sne.Name = "cbxRecibos"
                                                sne.Number = 0
                                                sne.Properties.MaxValue = 100
                                                sne.Properties.MinValue = 0
                                                sne.Width = 50
                                            End Sub).GetHtml()
                                    End Sub)
                            End Sub)
                        group.Items.AddGroupItem(
                            Sub(group2)
                                group2.Caption = "Pagos Correspondientes a las fechas"
                                group2.ColCount = 2
                                group2.Items.Add(
                                    Sub(item2)
                                        item2.Caption = "Desde"
                                        item2.SetNestedContent(
                                            Sub()
                                                Html.DevExpress().DateEdit(
                                                    Sub(dte)
                                                        dte.Name = "dteFechaInicio"
                                                        dte.Properties.UseMaskBehavior = True
                                                        dte.Properties.EditFormat = EditFormat.Custom
                                                        dte.Properties.NullText = "dd/MM/yyyy"
                                                        dte.Properties.EditFormatString = "dd/MM/yyyy"
                                                        dte.Width = 100
                                                        'dte.Properties.ClientSideEvents.ValueChanged = "function(s, e) { validarFecha('inicio'); }"
                                                    End Sub).GetHtml()
                                            End Sub)
                                    End Sub)
                                group2.Items.Add(
                                    Sub(item2)
                                        item2.Caption = "Hasta"
                                        item2.SetNestedContent(
                                            Sub()
                                                Html.DevExpress().DateEdit(
                                                    Sub(dte)
                                                        dte.Name = "dteFechaFin"
                                                        dte.Properties.UseMaskBehavior = True
                                                        dte.Properties.EditFormat = EditFormat.Custom
                                                        dte.Properties.NullText = "dd/MM/yyyy"
                                                        dte.Properties.EditFormatString = "dd/MM/yyyy"
                                                        dte.Width = 100
                                                        'dte.Properties.ClientSideEvents.ValueChanged = "function(s, e) { validarFecha('inicio'); }"
                                                    End Sub).GetHtml()
                                            End Sub)
                                    End Sub)
                            End Sub)
                        
                        'group.Items.Add(
                        '    Sub(item)
                        '        item.Caption = ""
                        '        item.ShowCaption = DefaultBoolean.False
                        '        item.SetNestedContent(
                        '            Sub()
                        '                Html.DevExpress().FormLayout(
                        '                    Sub(settings2)
                        '                        settings2.Name = "FormLayout2"
                        '                        settings2.ColCount = 2
                        '                        settings2.Items.AddGroupItem(
                        '                            Sub(group2)
                        '                                group2.Caption = "Pagos Correspondientes a las fechas"
                        '                                group2.Items.Add(
                        '                                    Sub(item2)
                        '                                        item2.Caption = "Desde"
                        '                                        item2.SetNestedContent(
                        '                                            Sub()
                        '                                                Html.DevExpress().DateEdit(
                        '                                                   Sub(dte)
                        '                                                       dte.Name = "dteFechaInicio"
                        '                                                       dte.Properties.UseMaskBehavior = True
                        '                                                       dte.Properties.EditFormat = EditFormat.Custom
                        '                                                       dte.Properties.NullText = "dd/MM/yyyy"
                        '                                                       dte.Properties.EditFormatString = "dd/MM/yyyy"
                        '                                                       dte.Width = 100
                        '                                                       'dte.Properties.ClientSideEvents.ValueChanged = "function(s, e) { validarFecha('inicio'); }"
                        '                                                   End Sub).GetHtml()
                        '                                            End Sub)
                        '                                    End Sub)
                        '                                group2.Items.Add(
                        '                                    Sub(item2)
                        '                                        item2.Caption = "Hasta"
                        '                                        item2.SetNestedContent(
                        '                                            Sub()
                        '                                                Html.DevExpress().DateEdit(
                        '                                                   Sub(dte)
                        '                                                       dte.Name = "dteFechaFin"
                        '                                                       dte.Properties.UseMaskBehavior = True
                        '                                                       dte.Properties.EditFormat = EditFormat.Custom
                        '                                                       dte.Properties.NullText = "dd/MM/yyyy"
                        '                                                       dte.Properties.EditFormatString = "dd/MM/yyyy"
                        '                                                       dte.Width = 100
                        '                                                       'dte.Properties.ClientSideEvents.ValueChanged = "function(s, e) { validarFecha('inicio'); }"
                        '                                                   End Sub).GetHtml()
                        '                                            End Sub)
                        '                                    End Sub)
                        '                            End Sub)
                        '                    End Sub).GetHtml()
                        '            End Sub)
                        '    End Sub)
                    End Sub)
            End Sub).GetHtml()%>
        <div align="center">
            <% Html.DevExpress().Button(
                   Sub(btn)
                       btn.Name = "btnLimpiar"
                       btn.Text = "Limpiar"
                       btn.ClientSideEvents.Click = ""
                   End Sub).GetHtml()
                Html.DevExpress().Button(
                    Sub(btn)
                        btn.Name = "btnVerActa"
                        btn.Text = "Ver Acta"
                        btn.ClientSideEvents.Click = "btnVerActaClick"
                    End Sub).GetHtml()%>
        </div>
    </div>
    <div id="PDF" class="col_50">
        <iframe height="792px" width="612px"></iframe>
    </div>
</asp:Content>
