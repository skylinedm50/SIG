<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Incorporaciones/Views/Shared/site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <style>
              .info, .success, .warning, .error, .validation {
                    border: 1px solid;
                    margin: 10px 0px;
                    padding:15px 10px 15px 50px;
                    background-repeat: no-repeat;
                    background-position: 10px center;
                    display: none;
                }
                .info {
                    color: #00529B;
                    background-color: #BDE5F8;
                    background-image: url("/Areas/Incorporaciones/Content/Images/info.png");
                }
                .success {
                    color: #4F8A10;
                    background-color: #DFF2BF;
                    background-image:url("/Areas/Incorporaciones/Content/Images/success.png");
                }
                .warning 
                {
                    color: #9F6000;
                    background-color: #FEEFB3;
                    background-image: url( "/Areas/Incorporaciones/Content/Images/warning.png");
                }
                .error 
                {
                    color: #D8000C;
                    background-color: #FFBABA;
                    background-image: url("/Areas/Incorporaciones/Content/Images/error.png");
                }
                .hidden 
                {
                    display:none;
                }
                .Boton 
                {
                    background:#1787C8;
                    color:#fff;
                    border : none;
                    font-family : "Arial" Georgia;
                    width:200px;
                    height:40px;
                    vertical-align:central;
                    text-align:center;
                }
                .Boton:hover
                {
                    background:#fff;
                    border: 1px solid #1787C8;
                    cursor:pointer;
                    color:#1787C8;
                    box-sizing:border-box;
                }

	            .spinner {
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
	            .center	{
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
	            .center img	{
		            height: 128px;
		            width: 128px;
	            }
        </style>

      <div class="div-main-content">
      <form id="form1" runat="server">
          <div class="Center-layout">
              <div style="margin-left:25%">
                    <br />                        
                        <%
                            Html.DevExpress().Label(Sub(lbl)
                                                        lbl.Name = "ASPxLabel1"
                                                        lbl.Text = "Cambio de estado a hogares participantes"
                                                        lbl.ControlStyle.Font.Bold = True
                                                        lbl.ControlStyle.Font.Italic = True
                                                        lbl.ControlStyle.Font.Size = 11
                                                        lbl.ControlStyle.Font.Underline = True
                                                    End Sub).GetHtml()
                         %>
                    <br />
                </div> 
              <br />
                   <%
                       Html.DevExpress().FormLayout(Sub(formulario)
                                                        formulario.Name = "formulario"
                                                        formulario.Width = Unit.Percentage(80)
                                                        formulario.Items.AddGroupItem(Sub(group)
                                                                                          group.Caption = "información del titular"
                                                                                          group.Items.Add(Sub(item)
                                                                                                              item.Caption = "Número de identidad del titular"
                                                                                                              item.SetNestedContent(Sub()
                                                                                                                                        Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                                      txt.Name = "txtIdentidad"
                                                                                                                                                                      txt.Properties.ClientSideEvents.KeyPress = "function(s,e){Fnc_ValidarDatos(2, s)}"
                                                                                                                                                                  End Sub).GetHtml()
                                                                                                                                    End Sub)
                                                                                                          End Sub)
                                                                                          group.Items.Add(Sub(item)
                                                                                                              item.Caption = "Número de hogar del titular"
                                                                                                              item.SetNestedContent(Sub()
                                                                                                                                        Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                                      txt.Name = "txtHogar"
                                                                                                                                                                      txt.Properties.ClientSideEvents.KeyPress = "function(s,e){ Fnc_ValidarDatos(2, s)}"
                                                                                                                                                                  End Sub).GetHtml()
                                                                                                                                    End Sub)
                                                                                                          End Sub)
                                                                                          group.Items.Add(Sub(item)
                                                                                                              item.ShowCaption = DefaultBoolean.False
                                                                                                              item.SetNestedContent(Sub()
                                                                                                                                        %><br /><br /><%
                                                                                                                                        Html.DevExpress().Button(Sub(setting)
                                                                                                                                                                     setting.Name = "BtnBuscarSuspender"
                                                                                                                                                                     setting.Text = "Buscar Información hogar"
                                                                                                                                                                     setting.EnableTheming = False
                                                                                                                                                                     setting.Width = 200
                                                                                                                                                                     setting.Height = 40
                                                                                                                                                                     setting.UseSubmitBehavior = False
                                                                                                                                                                     setting.AllowFocus = False
                                                                                                                                                                     setting.ControlStyle.Font.Size = 9
                                                                                                                                                                     setting.ClientSideEvents.Click = " function(s,e) {  Fnc_BuscarInformacionHogar(null,4)}"
                                                                                                                                                                     setting.ControlStyle.CssClass = "Boton"
                                                                                                                                                                 End Sub).GetHtml()
                                                                                                                                                                      End Sub)
                                                                                                                                                                  End Sub)
                                                                                                                                                              End Sub)
                                                                                                                                                          End Sub).GetHtml()

                   %>

<div id="divMensaje">
        <div class="warning" id="Warning_text" ></div>
        <div class="error" id="Error_text"></div>
</div> 

<%
    Html.DevExpress().FormLayout(
    Sub(formulario)
        formulario.Name = "formulario2"
        formulario.Width = Unit.Percentage(80)
        formulario.SettingsItems.Height = 35
        formulario.ControlStyle.Font.Bold = True
        formulario.ControlStyle.Font.Size = 8
        formulario.Items.AddGroupItem(
            Sub(item)
            item.Caption = "información del hogar"
            item.Items.Add(
                    Sub(Control)
                        Control.Caption = "Número del Hogar"
                        Control.SetNestedContent(
                                Sub()
                                    Html.DevExpress().Label(Sub(lbl)
                                                                lbl.Name = "lblNumeroHogar"
                                                            End Sub).GetHtml()
                                End Sub)
                    End Sub)

            item.Items.Add(
                    Sub(Control)
                        Control.Caption = "Departamento"
                        Control.SetNestedContent(
                                Sub()
                                    Html.DevExpress().Label(Sub(lbl)
                                                                lbl.Name = "lblDepartamento"
                                                            End Sub).GetHtml()
                                End Sub)
                    End Sub)
            item.Items.Add(
                    Sub(Control)
                        Control.Caption = "Municipio"
                        Control.SetNestedContent(Sub()
                                                     Html.DevExpress().Label(Sub(lbl)
                                                                                 lbl.Name = "lblMunicipio"
                                                                             End Sub).GetHtml()
                                                 End Sub)
                    End Sub)
            item.Items.Add(
                    Sub(Control)
                        Control.Caption = "Caserio"
                        Control.SetNestedContent(Sub()
                                                     Html.DevExpress().Label(Sub(lbl)
                                                                                 lbl.Name = "lblCaserio"
                                                                             End Sub).GetHtml()
                                                 End Sub)
                    End Sub)
            item.Items.Add(
                    Sub(Control)
                        Control.Caption = "Dirección"
                        Control.SetNestedContent(Sub()
                                                     Html.DevExpress().Label(Sub(lbl)
                                                                                 lbl.Name = "lblDireccio"
                                                                             End Sub).GetHtml()
                                                 End Sub)
                    End Sub)
            item.Items.Add(
                    Sub(Control)
                        Control.Caption = "Nombre del titular"
                        Control.SetNestedContent(Sub()
                                                     Html.DevExpress().Label(Sub(lbl)
                                                                                 lbl.Name = "lbltitular"
                                                                             End Sub).GetHtml()
                                                 End Sub)
                    End Sub)
            item.Items.Add(
                    Sub(Control)
                        Control.Caption = "Umbral de pobreza"
                        Control.SetNestedContent(Sub()
                                                     Html.DevExpress().Label(Sub(lbl)
                                                                                 lbl.Name = "lblUmbral"
                                                                             End Sub).GetHtml()
                                                 End Sub)
                    End Sub)
            item.Items.Add(
                    Sub(Control)
                        Control.Caption = "Acción"
                        Control.SetNestedContent(Sub()
                                                     Html.DevExpress.ComboBox(Sub(cmb)
                                                                                  cmb.Name = "cmbHogares"
                                                                                  cmb.Properties.Items.Add("Suspensión de Hogares", "1")
                                                                                  cmb.Properties.Items.Add("Activación de Hogares", "2")
                                                                                  cmb.Properties.ClientSideEvents.SelectedIndexChanged = "function(s,e){fnc_MostrarRazonCambioEstado(s,e)}"
                                                                              End Sub).GetHtml()
                                                 End Sub)
                    End Sub)
            item.Items.Add(
                    Sub(Control)
                        Control.Caption = "Estado"
                        Control.SetNestedContent(Sub()
                                                     Html.DevExpress().Label(Sub(lbl)
                                                                                 lbl.Name = "lblEstado"
                                                                             End Sub).GetHtml()
                                                 End Sub)
                    End Sub)
            item.Items.Add(
                    Sub(Control)

                Control.Caption = "Razón de excluisión"
                Control.SetNestedContent(Sub()
                                                         %><div id="suspension_id" style="display:none"><%
                                                                                                            Html.DevExpress().RadioButtonList(
                                                                                                                Sub(RadioList)
                                                                                                                    RadioList.Properties.ItemSpacing = 15
                                                                                                                    RadioList.Name = "RadioButton1"
                                                                                                                    RadioList.ControlStyle.Font.Size = 9
                                                                                                                    RadioList.ControlStyle.Border.BorderStyle = BorderStyle.None
                                                                                                                    RadioList.Properties.Items.Add("El titular no reclamo el bono por dos periodo consecutivos. ", "12")
                                                                                                                    RadioList.Properties.Items.Add("EL hogar migró a una ubicación territorial  no beneficiaria del programa y que no haya resultado elegible/focalizado.", "14")
                                                                                                                    RadioList.Properties.Items.Add("La suspensión fue solicitada por escrito por la/el titular, por ausencia permanente o justificada.", "15")
                                                                                                                    RadioList.Properties.Items.Add("Suspensión por investigación de elegibilidad.", "17")
                                                                                                                End Sub).GetHtml()

                                                         %></div><div id="activacion_id" style="display:none"><%

                                                                                                                  Html.DevExpress.TextBox(Sub(TxtSetting)
                                                                                                                                              TxtSetting.Name = "memoid"
                                                                                                                                              TxtSetting.Width = 500
                                                                                                                                              TxtSetting.Height = 50
                                                                                                                                          End Sub).GetHtml()
                                                                                          %></div><%
                                                             End Sub)
                                                         End Sub)
                                                     End Sub)
                                                 End Sub).GetHtml()
%>
         <br />     <div id="SuspensionBtn">
                <%
        Html.DevExpress.Button(Sub(botonSalvar)
                                   botonSalvar.Name = "salvar"
                                   botonSalvar.Text = "Actualizar"
                                   botonSalvar.Width = 250
                                   botonSalvar.EnableTheming = False
                                   botonSalvar.ControlStyle.CssClass = "Boton"
                                   botonSalvar.Style.Value = "margin-left:80px"
                                   botonSalvar.ClientSideEvents.Click = "function(s,e){Fnc_Suspension_Activacion()}"
                               End Sub).GetHtml()
                  %>
            </div>
          </div>
      </form>      
           <link href="<%: ResolveUrl("~/Areas/Incorporaciones/Content/Style/Modules/suspensiones.css")%>" rel="stylesheet" />
   </div>
   <div class="spinner" style="display: none;">
	  <div class="center">
		<img src="/Areas/Incorporaciones/Content/Images/loader.gif">
	  </div>
   </div>
</asp:Content>
