<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Security.AccessControl" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Xml.Serialization" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>
<%@ Import Namespace="System.Drawing.Imaging" %>    
<%@ Import Namespace="System.Security.Permissions" %>
<%@ Import Namespace="System.Diagnostics" %>


<style>
    .panelNum {
        padding-top: 320px;
        padding-left: 50px;
        font-weight: bold;
        font-size: 12px;
    }    
    #dotNav {
	    z-index: 5;
	    padding: 7px;
	    padding-top: 12px;
    }
    #dotNav ul {
	    list-style: none;
	    margin:0 auto;
        width:100%;
        margin-left:auto;
        margin-right:auto;
    }
    #dotNav li {
	    position: relative;
	    background: none repeat scroll 0 0 #cccccc;
	    border:1px solid #aaaaaa;
	    border-radius: 15px 15px 15px 15px;
	    cursor: pointer;
	    height: 12px;
	    margin: 10px 10px 0px 0px;
	    width: 12px;
        display:inline-block;
	    vertical-align:bottom;
    }
    #dotNav li.active {
	    background-color: #ffffff;
	    background-image: -moz-linear-gradient(center top , #FFFFFF, #EEEEEE);
	    background-repeat: repeat-x;
    }
    #dotNav li:hover {
	    background: none repeat scroll 0 0 #EEEEEE;
    }
    #dotNav a {
	    outline: 0;
	    vertical-align:top;
	    margin: 0px 0px 0px 25px;
	    position: relative;
	    top:-5px;
    }
</style>       
    <%
        Dim validacion As Integer = 0

        Html.DevExpress().FormLayout(Sub(setting)
            setting.Name = "formLayout"
            setting.ColCount = 3
            setting.Theme = "DevEx"
            setting.Items.Add(Sub(grupo)
                                  grupo.RowSpan = 17
                                  grupo.ColSpan = 1
                                  grupo.ShowCaption = DefaultBoolean.False
                                  grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Top
                                  grupo.VerticalAlign = FormLayoutVerticalAlign.Top
                                  grupo.SetNestedContent(Sub()
                                                             Html.RenderAction("imageSlider", New With {Key .indice = 0, Key .HExpediente = Model.Rows(0).Item(0)})
                                                         End Sub)
                              End Sub)
            setting.Items.Add(Sub(grupo)
                                  grupo.ColSpan = 2
                                  grupo.SetNestedContent(Sub()
                                                             Html.DevExpress().Label(Sub(LblSolicitud)
                                                                                         LblSolicitud.Text = ViewData("descripcion")
                                                                                         LblSolicitud.Theme = "DevEx"
                                                                                         LblSolicitud.ControlStyle.ForeColor = Color.Red
                                                                                         LblSolicitud.Name = "LbObservacion"
                                                                                     End Sub).GetHtml()
                                                         End Sub)
                                  grupo.Caption = "Observación de la Actualización "
                                  grupo.CaptionStyle.Font.Italic = True
                                  grupo.CaptionStyle.Font.Bold = True
                                  grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                  grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                              End Sub)
            setting.Items.Add(Sub(grupo)
                                  grupo.ColSpan = 2
                                  grupo.SetNestedContent(Sub()
                                                             Html.DevExpress().Label(Sub(LblSolicitud)
                                                                                         LblSolicitud.Text = ViewData("comentario")
                                                                                         LblSolicitud.Theme = "DevEx"
                                                                                         LblSolicitud.ControlStyle.ForeColor = Color.Red
                                                                                         LblSolicitud.Name = "Lblcomentario"
                                                                                     End Sub).GetHtml()
                                                         End Sub)
                                  grupo.Caption = "Comentario Expediente "
                                  grupo.CaptionStyle.Font.Italic = True
                                  grupo.CaptionStyle.Font.Bold = True
                                  grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                  grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                              End Sub)
            setting.Items.Add(Sub(grupo)
                                  grupo.ColSpan = 2
                                  grupo.Caption = "Número del Hogar"
                                  grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                  grupo.CaptionStyle.Font.Bold = True
                                  grupo.CaptionStyle.Font.Italic = True
                                  grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                  grupo.SetNestedContent(Sub()
                                                             Html.DevExpress().Label(Sub(LblHogar)
                                                                                         LblHogar.Text = Model.Rows(0).Item(0)
                                                                                         LblHogar.Theme = "DevEx"
                                                                                         LblHogar.Name = "LblHogar"
                                                                                     End Sub).GetHtml()
                                                         End Sub)
                              End Sub)
            setting.Items.Add(Sub(grupo)
                                  grupo.ColSpan = 2
                                  grupo.Caption = "Departamento "
                                  grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                  grupo.CaptionStyle.Font.Bold = True
                                  grupo.CaptionStyle.Font.Italic = True
                                  grupo.CssClass = "margin"
                                  grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                  grupo.SetNestedContent(Sub()
                                                             Html.DevExpress().Label(Sub(LblCodDepto)
                                                                                         LblCodDepto.Text = Model.Rows(0).Item(1)
                                                                                         LblCodDepto.Theme = "DevEx"
                                                                                         LblCodDepto.Name = "LblCodDepto"
                                                                                     End Sub).GetHtml()
                                                         End Sub)
                              End Sub)
            setting.Items.Add(Sub(grupo)
                                  grupo.ColSpan = 2
                                  grupo.Caption = "Municipio "
                                  grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                  grupo.CaptionStyle.Font.Bold = True
                                  grupo.CaptionStyle.Font.Italic = True
                                  grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                  grupo.SetNestedContent(Sub()
                                                             Html.DevExpress().Label(Sub(LblCodMuni)
                                                                                         LblCodMuni.Text = Model.Rows(0).Item(2)
                                                                                         LblCodMuni.Theme = "DevEx"
                                                                                         LblCodMuni.Name = "LblCodMuni"
                                                                                     End Sub).GetHtml()
                                                         End Sub)
                              End Sub)
            setting.Items.Add(Sub(grupo)
                                  grupo.ColSpan = 2
                                  grupo.Caption = "Aldea "
                                  grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                  grupo.CaptionStyle.Font.Bold = True
                                  grupo.CaptionStyle.Font.Italic = True
                                  grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                  grupo.SetNestedContent(Sub()
                                                             Html.DevExpress().Label(Sub(LblCodAldea)
                                                                                         LblCodAldea.Text = Model.Rows(0).Item(3)
                                                                                         LblCodAldea.Theme = "DevEx"
                                                                                         LblCodAldea.Name = "LblCodAldea"
                                                                                     End Sub).GetHtml()
                                                         End Sub)
                              End Sub)
            setting.Items.Add(Sub(grupo)
                                  grupo.ColSpan = 2
                                  grupo.Caption = "Caserío "
                                  grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                  grupo.CaptionStyle.Font.Bold = True
                                  grupo.CaptionStyle.Font.Italic = True
                                  grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                  grupo.SetNestedContent(Sub()
                                                             Html.DevExpress().Label(Sub(LblCodCaserio)
                                                                                         LblCodCaserio.Text = Model.Rows(0).Item(4)
                                                                                         LblCodCaserio.Theme = "DevEx"
                                                                                         LblCodCaserio.Name = "LblCodCaserio"
                                                                                     End Sub).GetHtml()
                                                         End Sub)
                              End Sub)
            setting.Items.Add(Sub(grupo)
                                  grupo.ColSpan = 2
                                  grupo.Caption = "Dirección "
                                  grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                  grupo.CaptionStyle.Font.Bold = True
                                  grupo.CaptionStyle.Font.Italic = True
                                  grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                  grupo.SetNestedContent(Sub()
                                                             Html.DevExpress().Label(Sub(LblDireccion)
                                                                                         LblDireccion.Text = Model.Rows(0).Item(5)
                                                                                         LblDireccion.Theme = "DevEx"
                                                                                         LblDireccion.Name = "LblDireccion"
                                                                                     End Sub).GetHtml()
                                                         End Sub)
                              End Sub)
            setting.Items.Add(Sub(grupo)
                                  grupo.ColSpan = 2
                                  grupo.Caption = "Número Telefónico "
                                  grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                  grupo.CaptionStyle.Font.Bold = True
                                  grupo.CaptionStyle.Font.Italic = True
                                  grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                  grupo.SetNestedContent(Sub()
                                                             Html.DevExpress().Label(Sub(LblTelefono)
                                                                                         LblTelefono.Text = Model.Rows(0).Item(6)
                                                                                         LblTelefono.Theme = "DevEx"
                                                                                         LblTelefono.Name = "LblTelefono"
                                                                                     End Sub).GetHtml()
                                                         End Sub)
                              End Sub)
            setting.Items.Add(Sub(grupo)
                                  grupo.ColSpan = 2
                                  grupo.Caption = "Umbral de Pobreza "
                                  grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                  grupo.CaptionStyle.Font.Bold = True
                                  grupo.CaptionStyle.Font.Italic = True
                                  grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                  grupo.SetNestedContent(Sub()
                                                             Html.DevExpress().Label(Sub(LblUmbralHogar)
                                                                                         LblUmbralHogar.Text = Model.Rows(0).Item(7)
                                                                                         LblUmbralHogar.Theme = "DevEx"
                                                                                         LblUmbralHogar.Name = "LblUmbralHogar"
                                                                                     End Sub).GetHtml()
                                                         End Sub)
                              End Sub)
            setting.Items.Add(Sub(grupo)
                                  grupo.ColSpan = 2
                                  grupo.Caption = " "
                                  grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                  grupo.SetNestedContent(Sub()
                                                             Html.DevExpress().HyperLink(Sub(Hlink)
                                                                                             Hlink.Name = "Hlink"
                                                                                             Hlink.Properties.Text = "Descripciones Previas"
                                                                                             Hlink.ControlStyle.Font.Bold = True
                                                                                             Hlink.ControlStyle.Font.Size = 10
                                                                                             Hlink.Properties.ClientSideEvents.Click = "function(s,e){debugger; namePopup.Show()}"
                                                                                         End Sub).GetHtml()
                                                         End Sub)
                              End Sub)
            setting.Items.Add(Sub(grupo)
                                  grupo.ColSpan = 2
                                  grupo.Caption = "Descripción "
                                  grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                  grupo.CaptionStyle.Font.Bold = True
                                  grupo.CaptionStyle.Font.Italic = True
                                  grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                  grupo.SetNestedContent(Sub()
                                                             Html.DevExpress().Memo(Sub(memodescipcion)
                                                                                        memodescipcion.Width = 500
                                                                                        memodescipcion.Height = 50
                                                                                        memodescipcion.Theme = "DevEx"
                                                                                        memodescipcion.Name = "memodescipcion"
                                                                                    End Sub).GetHtml()
                                                         End Sub)
                              End Sub)
            setting.Items.Add(Sub(grupo)
                grupo.ColSpan = 2
                grupo.ShowCaption = DefaultBoolean.False
                grupo.SetNestedContent(Sub()
    %>
    <div id="divMensaje">
        <div class="warning" id="Warning_text" ></div>
        <div class="error" id="Error_text"></div>
    </div> 
    <%
            End Sub)
        End Sub)

        setting.Items.Add(Sub(grupo)
            grupo.ColSpan = 2
            grupo.CaptionStyle.Font.Bold = True
            grupo.CaptionStyle.Font.Italic = True
            grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
            grupo.ShowCaption = DefaultBoolean.False
            grupo.SetNestedContent(Sub()
    %>
    <div id="grid_personasHogarActualizar">       
    <%
        Html.RenderAction("PersonasHogarActualizar", New With {Key .hog_hogar = Model.Rows(0).Item(0)})
    %>
    </div>
    <% 
                End Sub)

            End Sub)
            setting.Items.Add(Sub(grupo)
                                  grupo.ShowCaption = DefaultBoolean.False
                                  grupo.ColSpan = 1
                                  grupo.SetNestedContent(Sub()
                                                             Html.DevExpress().Button(Sub(btnBuscar)
                                                                                          btnBuscar.Text = "Aprobar Actualización"
                                                                                          btnBuscar.Name = "btnActualizar"
                                                                                          btnBuscar.EnableTheming = False
                                                                                          btnBuscar.ControlStyle.CssClass = "Boton"
                                                                                          btnBuscar.ClientSideEvents.Click = "function(e,s){fnc_ActualizarInformacionHogarBeneficiario(" + validacion.ToString() + ")}"
                                                                                      End Sub).GetHtml()
                                                         End Sub)
                              End Sub)
            setting.Items.Add(Sub(grupo)
                                  grupo.ShowCaption = DefaultBoolean.False
                                  grupo.ColSpan = 1
                                  grupo.SetNestedContent(Sub()
                                                             Html.DevExpress().Button(Sub(btnBuscar)
                                                                                          btnBuscar.Text = "Atras"
                                                                                          btnBuscar.Name = "btnVolver"
                                                                                          btnBuscar.EnableTheming = False
                                                                                          btnBuscar.ControlStyle.CssClass = "Boton"
                                                                                          btnBuscar.ClientSideEvents.Click = "function(e,s){ Fnc_VolverBuscarActualizarHogare(); }"
                                                                                      End Sub).GetHtml()
                                                         End Sub)
                              End Sub)
        End Sub).GetHtml()

        Html.DevExpress().PopupControl(Sub(popup)
                                           popup.Name = "namePopup"
                                           popup.Width = 350
                                           popup.AllowDragging = True
                                           popup.HeaderText = "Descripciones Previas"
                                           popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                           popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                           popup.Modal = True
                                           popup.ShowCloseButton = True
                                           popup.SetContent(Sub()
                                                                Html.RenderAction("DescripcionesPrevias", New With {Key .hog_hogar = Model.Rows(0).Item(0)})
                                                            End Sub)
                                       End Sub).GetHtml()

    %>


