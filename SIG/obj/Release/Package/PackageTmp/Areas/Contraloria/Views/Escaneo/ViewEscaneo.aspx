<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Contraloria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
ViewEscaneo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <%--<script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/popup.js")%>'></script>--%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/escaneo.js")%>'></script>
    <link type="text/css" rel="stylesheet" href="<%: ResolveUrl("~/Areas/Contraloria/Style/popup.css")%>" />
    <style>
        .col_50 {
          width:48%;
          /*margin:0 2% 0 0;*/
          margin: 0 auto;
          float:left;
        }

        /*#toPopup {
            width: auto;
            margin-left: -150px;
            top: auto;
        }

        div#popup_content {
            overflow-y: hidden;
            height: auto;
        }*/
    </style>

<h2>Digitalización de Documentos de Pago</h2>
    <% Html.BeginForm()%>
    <div class="col_50" align="center">
        <div id="divControles">
            <% Html.DevExpress().FormLayout(
                  Sub(settings)
                      settings.Name = "frmLayoutLeitz"
                      settings.ColCount = 1
                      settings.Items.AddGroupItem(
                          Sub(grupo)
                              grupo.Caption = "Leitz"
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
                              grupo.Items.Add(
                                  Sub(item)
                                      item.Caption = "No. Leitz"
                                      item.Name = "itemLeitz"
                                      item.SetNestedContent(
                                          Sub()
                                              Html.RenderPartial("PartialCbxLeitz")
                                              Html.DevExpress().Label(
                                                  Sub(lbl)
                                                      lbl.Name = "lblVacio"
                                                      lbl.Text = "No existen Leitz en este período y fondo"
                                                      lbl.Properties.EnableClientSideAPI = True
                                                      lbl.ClientVisible = False
                                                  End Sub).GetHtml()
                                          End Sub)
                                  End Sub)
                             'grupo.Items.Add(
                             '    Sub(item)
                             '        item.Caption = "Descripción"
                             '        item.ShowCaption = DefaultBoolean.False
                             '        item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                             '        item.SetNestedContent(
                             '            Sub()
                             '                Html.DevExpress().Button(
                             '                    Sub(btn)
                             '                        btn.Name = "btnAgregarDesc"
                             '                        btn.Text = "Agregar Nuevo Leitz"
                             '                        btn.ClientSideEvents.Click = "btnAgregarDescClick"
                             '                        btn.ControlStyle.CssClass = "topopup"
                             '                    End Sub).GetHtml()
                             '            End Sub)
                             '    End Sub)
                         End Sub)
                  End Sub).GetHtml()%>
            <% Html.DevExpress().FormLayout(
                   Sub(settings)
                       settings.Name = "frmLayoutDocumento"
                       settings.Items.AddGroupItem(
                           Sub(grupo)
                               grupo.Caption = "Documento"
                               grupo.Items.Add(
                               Sub(item)
                                   item.Caption = "Dapartamento"
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().ComboBox(
                                               Sub(cbxDpto)
                                                   cbxDpto.Name = "cbxDepto"
                                                   cbxDpto.Properties.DropDownStyle = DropDownStyle.DropDownList
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
                               grupo.Items.AddGroupItem(
                                    Sub(grupo2)
                                        grupo2.Caption = "Fechas de Pago de Reporte"
                                        grupo2.Name = "grupoFecha"
                                        'grupo2.Visible = False
                                        grupo2.ClientVisible = False
                                        grupo2.Items.Add(
                                            Sub(fchIncio)
                                                fchIncio.Caption = "Inicio"
                                                fchIncio.ShowCaption = DefaultBoolean.False
                                                'fchIncio.Visible = False
                                                fchIncio.SetNestedContent(
                                                    Sub()
                                                        ViewContext.Writer.Write("<table><tr><td>Desde:</td><td style='padding-right:5px;'>")
                                                        Html.DevExpress().DateEdit(
                                                            Sub(dtInicio)
                                                                dtInicio.Name = "dtInicio"
                                                                dtInicio.Properties.UseMaskBehavior = True
                                                                dtInicio.Properties.EditFormat = EditFormat.Custom
                                                                dtInicio.Properties.NullText = "dd/MM/yyyy"
                                                                dtInicio.Properties.EditFormatString = "dd/MM/yyyy"
                                                                dtInicio.Width = 100
                                                                dtInicio.Properties.ClientSideEvents.ValueChanged = "function(s, e) { validarFecha('inicio'); }"
                                                            End Sub).GetHtml()
                                                        ViewContext.Writer.Write("</td><td>Hasta:</td><td>")
                                                        Html.DevExpress().DateEdit(
                                                            Sub(dtFin)
                                                                dtFin.Name = "dtFin"
                                                                dtFin.Properties.UseMaskBehavior = True
                                                                dtFin.Properties.EditFormat = EditFormat.Custom
                                                                dtFin.Properties.NullText = "dd/MM/yyyy"
                                                                dtFin.Properties.EditFormatString = "dd/MM/yyyy"
                                                                dtFin.Width = 100
                                                                dtFin.Properties.ClientSideEvents.ValueChanged = "function(s, e) { validarFecha('fin'); }"
                                                            End Sub).GetHtml()
                                                        ViewContext.Writer.Write("</td></tr></table>")
                                                    End Sub)
                                            End Sub)
                                    End Sub)
                           End Sub)
                   End Sub).GetHtml()%>
        </div>
        <div id="divBotones">
            <% Html.DevExpress().UploadControl(
                   Sub(upc)
                       upc.Name = "uc"
                       upc.CallbackRouteValues = New With {.Controller = "Escaneo", .Action = "UploadControlCallback"}
                       upc.FileInputCount = 1
                       'upc.ClientSideEvents.FileUploadComplete = "function(s, e) { recargar(); }"
                       upc.BrowseButton.Text = "Buscar"
                       upc.ValidationSettings.AllowedFileExtensions = New String() {".pdf"}
                       'upc.ClientSideEvents.TextChanged = "function(s, e) { uc.Upload(); }"
                       upc.ClientSideEvents.TextChanged = "ucTextChanged"
                       upc.ShowProgressPanel = True
                       'upc.ProgressBarSettings.ShowPosition = True
                   End Sub).GetHtml()%>
            <br />
            <% Html.DevExpress().Button(
                   Sub(settings)
                       settings.Name = "btnGuardarDocumento"
                       settings.Text = "Guardar"
                       settings.ClientSideEvents.Click = "btnGuardarDocumentoClick"
                   End Sub).GetHtml()%>
        </div>
    </div>

    <div id="divDocumento" class="col_50">
        <% Html.RenderPartial("PartialVisorDocumento") %>
    </div>
    
    <%--<div id="toPopup">
        <div class="close"></div>
        <span class="ecs_tooltip">Press Esc to close <span class="arrow"></span></span>
        <div id="popup_content"> <!--your content start-->
            <% Html.DevExpress().FormLayout(
                   Sub(settings)
                       settings.Name = "frmLayoutDesc"
                       'settings.ColCount = 1
                       settings.Items.Add(
                           Sub(item)
                               item.Caption = "Descripción"
                               item.CaptionSettings.Location = LayoutItemCaptionLocation.Top
                               item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Memo(
                                           Sub(memo)
                                               memo.Name = "txtDescripcion"
                                               memo.Width = 300
                                               memo.Height = 100
                                           End Sub).GetHtml()
                                   End Sub)
                           End Sub)
                       settings.Items.Add(
                           Sub(item)
                               item.ShowCaption = DefaultBoolean.False
                               item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                               item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Button(
                                               Sub(btn)
                                                   btn.Name = "btnAgregarLeitz"
                                                   btn.Text = "Agregar"
                                                   btn.ClientSideEvents.Click = "btnAgregarLeitzClick"
                                               End Sub).GetHtml()
                                   End Sub)
                           End Sub)
                   End Sub).GetHtml()%>
        </div> <!--your content end-->
    </div>
    <div class="loader"></div>
    <div id="backgroundPopup"></div>--%>
    <% Html.EndForm()%>
</asp:Content>
