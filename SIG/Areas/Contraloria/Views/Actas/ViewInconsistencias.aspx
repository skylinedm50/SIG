<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Contraloria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Acta de Inconsistencias
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>

    <%--<script src="../../Scripts/popup.js" type="text/javascript"></script>
    <link href="../../Style/popup.css" rel="stylesheet" type="text/css" />--%>
    
    <%--<script type="text/javascript" src="../../Scripts/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/jspdf.plugin.split_text_to_size.js"></script>
    <script type="text/javascript" src="../../Scripts/jspdf.plugin.standard_fonts_metrics.js"></script>  
    <script type="text/javascript" src="../../Scripts/jspdf.plugin.total_pages.js"></script>  
    <script type="text/javascript" src="../../Scripts/actaInconsistencias.js"></script>--%>

    <%--<script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/popup.js")%>'></script>
    <link type="text/css" rel="stylesheet" href="<%: ResolveUrl("~/Areas/Contraloria/Style/popup.css")%>" />

    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/jspdf.js")%>'></script>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/jspdf.plugin.split_text_to_size.js")%>'></script>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/jspdf.plugin.standard_fonts_metrics.js")%>'></script>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/jspdf.plugin.total_pages.js")%>'></script>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/actaInconsistencias.js")%>'></script>--%>

    <link type="text/css" rel="stylesheet" href="/Areas/Contraloria/Style/styles_contraloria.css" />
    <script type="text/javascript" src="/Areas/Contraloria/Scripts/popup.js"></script>
    <link type="text/css" rel="stylesheet" href="/Areas/Contraloria/Style/popup.css"/>

    <script type="text/javascript" src='/Areas/Contraloria/Scripts/jspdf.js'></script>
    <script type="text/javascript" src='/Areas/Contraloria/Scripts/jspdf.plugin.split_text_to_size.js'></script>
    <script type="text/javascript" src='/Areas/Contraloria/Scripts/jspdf.plugin.standard_fonts_metrics.js'></script>
    <script type="text/javascript" src='/Areas/Contraloria/Scripts/jspdf.plugin.total_pages.js'></script>
    <script type="text/javascript" src='/Areas/Contraloria/Scripts/actaInconsistencias.js'></script>

    <script type="text/javascript" src="/Areas/Contraloria/Scripts/pdfmake.min.js"></script>
	<script type="text/javascript" src="/Areas/Contraloria/Scripts/vfs_fonts.js"></script>
    <script type="text/javascript">
        var usuario = '<%=ViewData("userName") %>';
    </script>

    <%--<style>
    fieldset {
        border: 1px solid #C2C4CB;
        border-radius: 4px;
        font: 11px Verdana, Geneva, sans-serif;
        width: 441px;
        padding: 6px;
    }

    legend {
        color: #A1A3AA;
        text-align: left;
    }

    textarea{
		height: 100px;
		resize: none;
		width: 97%;
	}

    .field-det {
        width: 560px;
    }

    .field-rec {
        width: 548px;
    }
    
    .col_40 {
        width: 38%;
        float: left;
        margin:0 2% 0 0;
    }

    .col_50 {
          width:48%;
          margin:0 2% 0 0;
          /*margin: 0 auto;*/
          float:left;
          /*display: inline-block;*/
    }

    .col_60 {
        width: 58%;
        float: left;
        margin:0 2% 0 0;
    }
</style>--%>
<h2>Acta de Inconsistencias</h2>
    <div id="contenidoActa" class="col_50" align="center">
        <% Html.DevExpress().FormLayout(
               Sub(settings)
                   settings.Name = "frmLoutEncabezado"
                   settings.ColCount = 1
                   settings.Items.AddGroupItem(
                       Sub(group)
                           group.Caption = "Contenido"
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Contralor"
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().TextBox(
                                                      Sub(txt)
                                                          txt.Name = "txtNombre"
                                                          txt.Properties.NullText = "Ingrese el nombre"
                                                      End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                           group.Items.Add(
                                Sub(item)
                                    item.Caption = "Departamento"
                                    item.SetNestedContent(
                                        Sub()
                                            Html.DevExpress().ComboBox(
                                                   Sub(cbx)
                                                       cbx.Name = "cbxDepartamento"
                                                       cbx.Properties.TextField = "desc_departamento"
                                                       cbx.Properties.ValueField = "cod_departamento"
                                                   End Sub).BindList(ViewData("departamentos")).GetHtml()
                                        
                                        End Sub)
                                End Sub)
                           group.Items.Add(
                                Sub(item)
                                    item.Caption = "Fecha de Recepción de Documentos"
                                    item.SetNestedContent(
                                        Sub()
                                            Html.DevExpress().DateEdit(
                                               Sub(dte)
                                                   dte.Name = "dteRecepcion"
                                                   dte.Properties.UseMaskBehavior = True
                                                   dte.Properties.EditFormat = EditFormat.Custom
                                                   dte.Properties.NullText = "dd/MM/yyyy"
                                                   dte.Properties.EditFormatString = "dd/MM/yyyy"
                                                   dte.Width = 155
                                               End Sub).GetHtml()
                                        End Sub)
                                End Sub)
                           group.Items.AddGroupItem(
                               Sub(group2)
                                   group2.Caption = "Fechas de Pago"
                                   group2.ColCount = 2
                                   group2.Items.Add(
                                       Sub(item)
                                           item.Caption = "Desde"
                                           item.SetNestedContent(
                                               Sub()
                                                   Html.DevExpress().DateEdit(
                                                       Sub(dte)
                                                           dte.Name = "dteInicio"
                                                           dte.Properties.UseMaskBehavior = True
                                                           dte.Properties.EditFormat = EditFormat.Custom
                                                           dte.Properties.NullText = "dd/MM/yyyy"
                                                           dte.Properties.EditFormatString = "dd/MM/yyyy"
                                                           dte.Width = 120
                                                       End Sub).GetHtml()
                                               End Sub)
                                       End Sub)
                                   group2.Items.Add(
                                       Sub(item)
                                           item.Caption = "Hasta"
                                           item.SetNestedContent(
                                               Sub()
                                                   Html.DevExpress().DateEdit(
                                                       Sub(dte)
                                                           dte.Name = "dteFin"
                                                           dte.Properties.UseMaskBehavior = True
                                                           dte.Properties.EditFormat = EditFormat.Custom
                                                           dte.Properties.NullText = "dd/MM/yyyy"
                                                           dte.Properties.EditFormatString = "dd/MM/yyyy"
                                                           dte.Width = 120
                                                       End Sub).GetHtml()
                                               End Sub)
                                       End Sub)
                               End Sub)
                           'group.Items.Add(
                           '     Sub(item)
                           '         item.Caption = ""
                           '         item.SetNestedContent(
                           '             Sub()
                                        
                           '             End Sub)
                           '     End Sub)
                       End Sub)
               End Sub).GetHtml()%>
        <fieldset>
            <legend>Inconsistencias</legend>
            <div>
                <% Html.DevExpress().FormLayout(
                   Sub(settings)
                       settings.Name = "frmLoutRecibos"
                       'settings.ColCount = 2
                       settings.Items.AddGroupItem(
                           Sub(group)
                               group.Caption = "Recibos"
                               group.ColCount = 2
                               group.Items.Add(
                                   Sub(item)
                                       item.Caption = "Sin Huella"
                                       item.SetNestedContent(
                                           Sub()
                                               Html.DevExpress().SpinEdit(
                                                    Sub(spe)
                                                        spe.Name = "txtRSH"
                                                        spe.Number = 0
                                                        spe.Properties.MaxValue = 100
                                                        spe.Properties.MinValue = 0
                                                        spe.Width = 40
                                                    End Sub).GetHtml()
                                           End Sub)
                                   End Sub)
                               group.Items.Add(
                                   Sub(item)
                                       item.Caption = "Referencia Ilegible"
                                       item.SetNestedContent(
                                           Sub()
                                               Html.DevExpress().SpinEdit(
                                                    Sub(spe)
                                                        spe.Name = "txtRRI"
                                                        spe.Number = 0
                                                        spe.Properties.MaxValue = 100
                                                        spe.Properties.MinValue = 0
                                                        spe.Width = 40
                                                    End Sub).GetHtml()
                                           End Sub)
                                   End Sub)
                               group.Items.Add(
                                   Sub(item)
                                       item.Caption = "Huella Ilegible"
                                       item.SetNestedContent(
                                           Sub()
                                               Html.DevExpress().SpinEdit(
                                                    Sub(spe)
                                                        spe.Name = "txtRHI"
                                                        spe.Number = 0
                                                        spe.Properties.MaxValue = 100
                                                        spe.Properties.MinValue = 0
                                                        spe.Width = 40
                                                    End Sub).GetHtml()
                                           End Sub)
                                   End Sub)
                               group.Items.Add(
                                   Sub(item)
                                       item.Caption = "Sin Corresponsabilidad"
                                       item.SetNestedContent(
                                           Sub()
                                               Html.DevExpress().SpinEdit(
                                                    Sub(spe)
                                                        spe.Name = "txtRSC"
                                                        spe.Number = 0
                                                        spe.Properties.MaxValue = 100
                                                        spe.Properties.MinValue = 0
                                                        spe.Width = 40
                                                    End Sub).GetHtml()
                                           End Sub)
                                   End Sub)
                               group.Items.Add(
                                   Sub(item)
                                       item.Caption = "Sin Sello"
                                       item.SetNestedContent(
                                           Sub()
                                               Html.DevExpress().SpinEdit(
                                                    Sub(spe)
                                                        spe.Name = "txtRSS"
                                                        spe.Number = 0
                                                        spe.Properties.MaxValue = 100
                                                        spe.Properties.MinValue = 0
                                                        spe.Width = 40
                                                    End Sub).GetHtml()
                                           End Sub)
                                   End Sub)
                               group.Items.Add(
                                   Sub(item)
                                       item.Caption = "Con Papel Reciclado"
                                       item.SetNestedContent(
                                           Sub()
                                               Html.DevExpress().SpinEdit(
                                                    Sub(spe)
                                                        spe.Name = "txtRPR"
                                                        spe.Number = 0
                                                        spe.Properties.MaxValue = 100
                                                        spe.Properties.MinValue = 0
                                                        spe.Width = 40
                                                    End Sub).GetHtml()
                                           End Sub)
                                   End Sub)
                               group.Items.Add(
                                   Sub(item)
                                       item.Caption = "Mal Impreso"
                                       item.SetNestedContent(
                                           Sub()
                                               Html.DevExpress().SpinEdit(
                                                    Sub(spe)
                                                        spe.Name = "txtRMI"
                                                        spe.Number = 0
                                                        spe.Properties.MaxValue = 100
                                                        spe.Properties.MinValue = 0
                                                        spe.Width = 40
                                                    End Sub).GetHtml()
                                           End Sub)
                                   End Sub)
                               group.Items.Add(
                                   Sub(item)
                                       item.Caption = "Fecha no coincide con Fecha de Pago"
                                       item.SetNestedContent(
                                           Sub()
                                               Html.DevExpress().SpinEdit(
                                                    Sub(spe)
                                                        spe.Name = "txtRFC"
                                                        spe.Number = 0
                                                        spe.Properties.MaxValue = 100
                                                        spe.Properties.MinValue = 0
                                                        spe.Width = 40
                                                    End Sub).GetHtml()
                                           End Sub)
                                   End Sub)
                               group.Items.Add(
                                   Sub(item)
                                       item.Caption = "Duplicado"
                                       item.SetNestedContent(
                                           Sub()
                                               Html.DevExpress().SpinEdit(
                                                    Sub(spe)
                                                        spe.Name = "txtRD"
                                                        spe.Number = 0
                                                        spe.Properties.MaxValue = 100
                                                        spe.Properties.MinValue = 0
                                                        spe.Width = 40
                                                    End Sub).GetHtml()
                                           End Sub)
                                   End Sub)
                               group.Items.Add(
                                   Sub(item)
                                       item.Caption = "Municipio no Coincide"
                                       item.SetNestedContent(
                                           Sub()
                                               Html.DevExpress().SpinEdit(
                                                    Sub(spe)
                                                        spe.Name = "txtRMC"
                                                        spe.Number = 0
                                                        spe.Properties.MaxValue = 100
                                                        spe.Properties.MinValue = 0
                                                        spe.Width = 40
                                                    End Sub).GetHtml()
                                           End Sub)
                                   End Sub)
                               group.Items.Add(
                                   Sub(item)
                                       item.Caption = "Roto"
                                       item.SetNestedContent(
                                           Sub()
                                               Html.DevExpress().SpinEdit(
                                                    Sub(spe)
                                                        spe.Name = "txtRR"
                                                        spe.Number = 0
                                                        spe.Properties.MaxValue = 100
                                                        spe.Properties.MinValue = 0
                                                        spe.Width = 40
                                                    End Sub).GetHtml()
                                           End Sub)
                                   End Sub)
                               group.Items.Add(
                                   Sub(item)
                                       item.Caption = "Faltantes"
                                       item.SetNestedContent(
                                           Sub()
                                               Html.DevExpress().SpinEdit(
                                                    Sub(spe)
                                                        spe.Name = "txtRF"
                                                        spe.Number = 0
                                                        spe.Properties.MaxValue = 100
                                                        spe.Properties.MinValue = 0
                                                        spe.Width = 40
                                                    End Sub).GetHtml()
                                           End Sub)
                                   End Sub)
                           End Sub)
                   End Sub).GetHtml()%>
            </div>
            <div align="center">
                <% Html.DevExpress().FormLayout(
                       Sub(settings)
                           settings.Name = "frmLoutActasCierre"
                           settings.Items.AddGroupItem(
                               Sub(group)
                                   group.Caption = "Actas de Cierre"
                                   group.ColCount = 2
                                   group.Items.Add(
                                       Sub(item)
                                           item.Caption = "Formato Incorrecto"
                                           item.SetNestedContent(
                                               Sub()
                                                   Html.DevExpress().SpinEdit(
                                                    Sub(spe)
                                                        spe.Name = "txtACFormatoIncorrecto"
                                                        spe.Number = 0
                                                        spe.Properties.MaxValue = 100
                                                        spe.Properties.MinValue = 0
                                                        spe.Width = 40
                                                    End Sub).GetHtml()
                                               End Sub)
                                       End Sub)
                                   group.Items.Add(
                                       Sub(item)
                                           item.Caption = "Fecha Incorrecta"
                                           item.SetNestedContent(
                                               Sub()
                                                   Html.DevExpress().SpinEdit(
                                                    Sub(spe)
                                                        spe.Name = "txtACFechaIncorrecta"
                                                        spe.Number = 0
                                                        spe.Properties.MaxValue = 100
                                                        spe.Properties.MinValue = 0
                                                        spe.Width = 40
                                                    End Sub).GetHtml()
                                               End Sub)
                                       End Sub)
                                   group.Items.Add(
                                       Sub(item)
                                           item.Caption = "Fondo Incorrecto"
                                           item.SetNestedContent(
                                               Sub()
                                                   Html.DevExpress().SpinEdit(
                                                    Sub(spe)
                                                        spe.Name = "txtACFondoIncorrecto"
                                                        spe.Number = 0
                                                        spe.Properties.MaxValue = 100
                                                        spe.Properties.MinValue = 0
                                                        spe.Width = 40
                                                    End Sub).GetHtml()
                                               End Sub)
                                       End Sub)
                                   group.Items.Add(
                                       Sub(item)
                                           item.Caption = "Otros"
                                           item.SetNestedContent(
                                               Sub()
                                                   Html.DevExpress().SpinEdit(
                                                    Sub(spe)
                                                        spe.Name = "txtACOtros"
                                                        spe.Number = 0
                                                        spe.Properties.MaxValue = 100
                                                        spe.Properties.MinValue = 0
                                                        spe.Width = 40
                                                    End Sub).GetHtml()
                                               End Sub)
                                       End Sub)
                               End Sub)
                       End Sub).GetHtml()%>
            </div>
            <div align="center">
                <div class="col_50">
                    <% Html.DevExpress().FormLayout(
                           Sub(settings)
                               settings.Name = "frmLoutReportes"
                               settings.ColCount = 1
                               settings.Items.AddGroupItem(
                                   Sub(group)
                                       group.Caption = "Reportes"
                                       group.Items.Add(
                                           Sub(item)
                                               item.Caption = "Ilegible"
                                               item.SetNestedContent(
                                                   Sub()
                                                       Html.DevExpress().SpinEdit(
                                                           Sub(spe)
                                                               spe.Name = "txtReportesIlegibles"
                                                               spe.Number = 0
                                                               spe.Properties.MaxValue = 100
                                                               spe.Properties.MinValue = 0
                                                               spe.Width = 40
                                                           End Sub).GetHtml()
                                                   End Sub)
                                           End Sub)
                                       group.Items.Add(
                                           Sub(item)
                                               item.Caption = "Sin Sello"
                                               item.SetNestedContent(
                                                   Sub()
                                                       Html.DevExpress().SpinEdit(
                                                           Sub(spe)
                                                               spe.Name = "txtReportesSinSello"
                                                               spe.Number = 0
                                                               spe.Properties.MaxValue = 100
                                                               spe.Properties.MinValue = 0
                                                               spe.Width = 40
                                                           End Sub).GetHtml()
                                                   End Sub)
                                           End Sub)
                                       group.Items.Add(
                                           Sub(item)
                                               item.Caption = "Sin Firma"
                                               item.SetNestedContent(
                                                   Sub()
                                                       Html.DevExpress().SpinEdit(
                                                           Sub(spe)
                                                               spe.Name = "txtReportesSinFirma"
                                                               spe.Number = 0
                                                               spe.Properties.MaxValue = 100
                                                               spe.Properties.MinValue = 0
                                                               spe.Width = 40
                                                           End Sub).GetHtml()
                                                   End Sub)
                                           End Sub)
                                   End Sub)
                           End Sub).GetHtml()%>
                </div>
                <div class="col_50">
                    <% Html.DevExpress().FormLayout(
                           Sub(settings)
                               settings.Name = "frmLoutNotasCargo"
                               settings.ColCount = 1
                               settings.Items.AddGroupItem(
                                   Sub(group)
                                       group.Caption = "Notas de Cargo"
                                       group.Items.Add(
                                           Sub(item)
                                               item.Caption = "Sin Sello"
                                               item.SetNestedContent(
                                                   Sub()
                                                       Html.DevExpress().SpinEdit(
                                                           Sub(spe)
                                                               spe.Name = "txtNotasSinSello"
                                                               spe.Number = 0
                                                               spe.Properties.MaxValue = 100
                                                               spe.Properties.MinValue = 0
                                                               spe.Width = 40
                                                           End Sub).GetHtml()
                                                   End Sub)
                                           End Sub)
                                       group.Items.Add(
                                           Sub(item)
                                               item.Caption = "Sin Firma"
                                               item.SetNestedContent(
                                                   Sub()
                                                       Html.DevExpress().SpinEdit(
                                                           Sub(spe)
                                                               spe.Name = "txtNotasSinFirma"
                                                               spe.Number = 0
                                                               spe.Properties.MaxValue = 100
                                                               spe.Properties.MinValue = 0
                                                               spe.Width = 40
                                                           End Sub).GetHtml()
                                                   End Sub)
                                           End Sub)
                                       group.Items.Add(
                                           Sub(item)
                                               item.Caption = "Notas Faltantes"
                                               item.SetNestedContent(
                                                   Sub()
                                                       Html.DevExpress().SpinEdit(
                                                           Sub(spe)
                                                               spe.Name = "txtNotasFaltantes"
                                                               spe.Number = 0
                                                               spe.Properties.MaxValue = 100
                                                               spe.Properties.MinValue = 0
                                                               spe.Width = 40
                                                           End Sub).GetHtml()
                                                   End Sub)
                                           End Sub)
                                   End Sub)
                           End Sub).GetHtml()%>
                </div>
            </div>
        </fieldset>
        <br />
        <div align="center">
            <% Html.DevExpress().Button(
                   Sub(btn)
                       btn.Name = "btnDetalles"
                       btn.Text = "Introducir Detalles"
                       btn.ControlStyle.CssClass = "topopup"
                       btn.ClientSideEvents.Click = "btnDetallesClick"
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

    <!--popup....-->
    <div id="toPopup">     	
        <div class="close"></div>
        <span class="ecs_tooltip">Press Esc to close <span class="arrow"></span></span>
	    <div id="popup_content"  align="center"> <!--your content start-->
            <fieldset class="field-det">
                <legend>Detalles</legend>
                <div id="divRecibos" hidden>
                    <fieldset class="field-rec">
                        <legend>Recibos</legend>
                        <div id="div_RSH"></div>
                        <div id="div_RRI"></div>
                        <div id="div_RHI"></div>
                        <div id="div_RSC"></div>
                        <div id="div_RSS"></div>
                        <div id="div_RPR"></div>
                        <div id="div_RMI"></div>
                        <div id="div_RFC"></div>
                        <div id="div_RD"></div>
                        <div id="div_RMC"></div>
                        <div id="div_RR"></div>
                        <div id="div_RF"></div>
                    </fieldset>
                </div>
                <br />
                <div id="divActasCierre" hidden>
                    <fieldset class="field-rec">
                        <legend>Actas de Cierre</legend>
                        <div id="div_ACFORI"></div>
                        <div id="div_ACFECI"></div>
                        <div id="div_ACFONI"></div>
                        <div id="div_ACO"></div>
                    </fieldset>
                </div>
                <br />
                <div id="divReportes" hidden>
                    <fieldset class="field-rec">
                        <legend>Reportes</legend>
                        <div id="div_REI"></div>
                        <div id="div_RESS"></div>
                        <div id="div_RESF"></div>
                    </fieldset>
                </div>
                <br />
                <div id="divNotasCargo" hidden>
                    <fieldset class="field-rec">
                        <legend>Notas de Cargo</legend>
                        <div id="div_NCSS"></div>
                        <div id="div_NCSF"></div>
                        <div id="div_NCF"></div>
                    </fieldset>
                </div>
            </fieldset>
            <br />
            <div align="center">
                <% Html.DevExpress().Button(
                       Sub(btn)
                           btn.Name = "btnVerActa2"
                           btn.Text = "Ver Acta"
                           btn.ClientSideEvents.Click = "btnVerActaClick"
                       End Sub).GetHtml()%>
            </div>
        </div> <!--your content end-->
    
    </div> <!--toPopup end-->
    <div class="loader"></div>
    <div id="backgroundPopup"></div>
</asp:Content>
