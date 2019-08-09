<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<%      
    Html.DevExpress().FormLayout(Sub(setting)
                                     setting.Name = "formLayout"
                                     setting.ColCount = 1
                                     setting.Theme = "DevEx"
                                     setting.Items.Add(Sub(grupo)
                                                           grupo.Caption = "Número del Hogar"
                                                           grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                           grupo.CaptionStyle.Font.Bold = True
                                                           grupo.CaptionStyle.Font.Italic = True
                                                           grupo.CssClass = "margin"
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
                                                           grupo.Caption = "Departamento "
                                                           grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                           grupo.CaptionStyle.Font.Bold = True
                                                           grupo.CaptionStyle.Font.Italic = True
                                                           grupo.CssClass = "margin"
                                                           grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                                           grupo.SetNestedContent(Sub()
                                                                                      Html.DevExpress().Label(Sub(LblCodDepto)
                                                                                                                  LblCodDepto.Text = Model.Rows(0).Item(21)
                                                                                                                  LblCodDepto.Theme = "DevEx"
                                                                                                                  LblCodDepto.Name = "LblCodDepto"
                                                                                                              End Sub).GetHtml()
                                                                                  End Sub)
                                                       End Sub)
                                     setting.Items.Add(Sub(grupo)
                                                           grupo.Caption = "Municipio "
                                                           grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                           grupo.CaptionStyle.Font.Bold = True
                                                           grupo.CaptionStyle.Font.Italic = True
                                                           grupo.CssClass = "margin"
                                                           grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                                           grupo.SetNestedContent(Sub()
                                                                                      Html.DevExpress().Label(Sub(LblCodMuni)
                                                                                                                  LblCodMuni.Text = Model.Rows(0).Item(19)
                                                                                                                  LblCodMuni.Theme = "DevEx"
                                                                                                                  LblCodMuni.Name = "LblCodMuni"
                                                                                                              End Sub).GetHtml()
                                                                                  End Sub)
                                                       End Sub)
                                     setting.Items.Add(Sub(grupo)
                                                           grupo.Caption = "Aldea "
                                                           grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                           grupo.CaptionStyle.Font.Bold = True
                                                           grupo.CaptionStyle.Font.Italic = True
                                                           grupo.CssClass = "margin"
                                                           grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                                           grupo.SetNestedContent(Sub()
                                                                                      Html.DevExpress().Label(Sub(LblCodAldea)
                                                                                                                  LblCodAldea.Text = Model.Rows(0).Item(17)
                                                                                                                  LblCodAldea.Theme = "DevEx"
                                                                                                                  LblCodAldea.Name = "LblCodAldea"
                                                                                                              End Sub).GetHtml()
                                                                                  End Sub)
                                                       End Sub)
                                     setting.Items.Add(Sub(grupo)
                                                           grupo.Caption = "Caserío "
                                                           grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                           grupo.CaptionStyle.Font.Bold = True
                                                           grupo.CaptionStyle.Font.Italic = True
                                                           grupo.CssClass = "margin"
                                                           grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                                           grupo.SetNestedContent(Sub()
                                                                                      Html.DevExpress().Label(Sub(LblCodCaserio)
                                                                                                                  LblCodCaserio.Text = Model.Rows(0).Item(15)
                                                                                                                  LblCodCaserio.Theme = "DevEx"
                                                                                                                  LblCodCaserio.Name = "LblCodCaserio"
                                                                                                              End Sub).GetHtml()
                                                                                  End Sub)
                                                       End Sub)
                                     setting.Items.Add(Sub(grupo)
                                                           grupo.Caption = "Dirección "
                                                           grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                           grupo.CaptionStyle.Font.Bold = True
                                                           grupo.CaptionStyle.Font.Italic = True
                                                           grupo.CssClass = "margin"
                                                           grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                                           grupo.SetNestedContent(Sub()
                                                                                      Html.DevExpress().Label(Sub(LblDireccion)
                                                                                                                  LblDireccion.Text = Model.Rows(0).Item(2)
                                                                                                                  LblDireccion.Theme = "DevEx"
                                                                                                                  LblDireccion.Name = "LblDireccion"
                                                                                                              End Sub).GetHtml()
                                                                                  End Sub)
                                                       End Sub)
                                     setting.Items.Add(Sub(grupo)
                                                           grupo.Caption = "Número Telefónico "
                                                           grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                           grupo.CaptionStyle.Font.Bold = True
                                                           grupo.CaptionStyle.Font.Italic = True
                                                           grupo.CssClass = "margin"
                                                           grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                                           grupo.SetNestedContent(Sub()
                                                                                      Html.DevExpress().Label(Sub(LblTelefono)
                                                                                                                  LblTelefono.Text = Model.Rows(0).Item(5)
                                                                                                                  LblTelefono.Theme = "DevEx"
                                                                                                                  LblTelefono.Name = "LblTelefono"
                                                                                                              End Sub).GetHtml()
                                                                                  End Sub)
                                                       End Sub)
                                     setting.Items.Add(Sub(grupo)
                                                           grupo.Caption = "Umbral de Pobreza "
                                                           grupo.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                           grupo.CaptionStyle.Font.Bold = True
                                                           grupo.CaptionStyle.Font.Italic = True
                                                           grupo.CssClass = "margin"
                                                           grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                                           grupo.SetNestedContent(Sub()
                                                                                      Html.DevExpress().Label(Sub(LblUmbralHogar)
                                                                                                                  LblUmbralHogar.Text = Model.Rows(0).Item(4)
                                                                                                                  LblUmbralHogar.Theme = "DevEx"
                                                                                                                  LblUmbralHogar.Name = "LblUmbralHogar"
                                                                                                              End Sub).GetHtml()
                                                                                  End Sub)
                                                       End Sub)
                                     'setting.Items.Add(Sub(grupo)
                                     '                      grupo.ShowCaption = DefaultBoolean.False
                                     '                      grupo.SetNestedContent(Sub()
                                     '                                                 Html.DevExpress().CheckBox(Sub(chksetting)
                                     '                                                                                chksetting.Name = "chkNuevoTitular"
                                     '                                                                                chksetting.Text = "Agregar Nuevo Titular"
                                     '                                                                                chksetting.Properties.ClientSideEvents.CheckedChanged = "function(s,e){fnc_MostrarFormulario(s,GridPersonasHogar)}"
                                     '                                                                            End Sub).GetHtml()
                                     '                                             End Sub)
                                     '                  End Sub)
                                     setting.Items.Add(Sub(grupo)
                                                           grupo.CaptionStyle.Font.Bold = True
                                                           grupo.CaptionStyle.Font.Italic = True
                                                           grupo.CssClass = "margin"
                                                           grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                                           grupo.ShowCaption = DefaultBoolean.False
                                                           grupo.SetNestedContent(Sub()
                                                                                      %>
<br />
<div id="NuevoTitular" style="display:none" >
    <div>
<%
                                                                                      Html.DevExpress().FormLayout(Sub(form)
                                                                                                                       form.Name = "nuevoT"
                                                                                                                       form.ColCount = 1
                                                                                                                       form.Items.Add(Sub(Subgrupo)
                                                                                                                                          Subgrupo.SetNestedContent(Sub()
                                                                                                                                                                        Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                                                                      txt.Name = "identidad"
                                                                                                                                                                                                      txt.Properties.MaxLength = 13
                                                                                                                                                                                                      txt.Properties.ClientSideEvents.KeyUp = "function(s,e){ Fnc_ValidarDatos(2,s) }"
                                                                                                                                                                                                  End Sub).GetHtml()
                                                                                                                                                                    End Sub)
                                                                                                                                          Subgrupo.Caption = "Número de identidad"
                                                                                                                                          Subgrupo.CaptionStyle.Font.Italic = True
                                                                                                                                          Subgrupo.CaptionStyle.Font.Bold = True
                                                                                                                                          Subgrupo.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                                      End Sub)
                                                                                                                       form.Items.Add(Sub(Subgrupo)
                                                                                                                                          Subgrupo.SetNestedContent(Sub()
                                                                                                                                                                        Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                                                                      txt.Name = "Nombre1"
                                                                                                                                                                                                      txt.Properties.ClientSideEvents.KeyUp = "function(s,e){ Fnc_ValidarDatos(1,s) }"
                                                                                                                                                                                                  End Sub).GetHtml()
                                                                                                                                                                    End Sub)
                                                                                                                                          Subgrupo.Caption = "Primer Nombre"
                                                                                                                                          Subgrupo.CaptionStyle.Font.Italic = True
                                                                                                                                          Subgrupo.CaptionStyle.Font.Bold = True
                                                                                                                                          Subgrupo.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                                      End Sub)
                                                                                                                       form.Items.Add(Sub(Subgrupo)
                                                                                                                                          Subgrupo.SetNestedContent(Sub()
                                                                                                                                                                        Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                                                                      txt.Name = "Nombre2"
                                                                                                                                                                                                      txt.Properties.ClientSideEvents.KeyUp = "function(s,e){ Fnc_ValidarDatos(1,s) }"
                                                                                                                                                                                                  End Sub).GetHtml()
                                                                                                                                                                    End Sub)
                                                                                                                                          Subgrupo.Caption = "Segundo Nombre"
                                                                                                                                          Subgrupo.CaptionStyle.Font.Italic = True
                                                                                                                                          Subgrupo.CaptionStyle.Font.Bold = True
                                                                                                                                          Subgrupo.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                                      End Sub)
                                                                                                                       form.Items.Add(Sub(Subgrupo)
                                                                                                                                          Subgrupo.SetNestedContent(Sub()
                                                                                                                                                                        Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                                                                      txt.Name = "Apellido1"
                                                                                                                                                                                                      txt.Properties.ClientSideEvents.KeyUp = "function(s,e){ Fnc_ValidarDatos(1,s) }"
                                                                                                                                                                                                  End Sub).GetHtml()
                                                                                                                                                                    End Sub)
                                                                                                                                          Subgrupo.Caption = "Primer Apellido"
                                                                                                                                          Subgrupo.CaptionStyle.Font.Italic = True
                                                                                                                                          Subgrupo.CaptionStyle.Font.Bold = True
                                                                                                                                          Subgrupo.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                                      End Sub)
                                                                                                                       form.Items.Add(Sub(Subgrupo)
                                                                                                                                          Subgrupo.SetNestedContent(Sub()
                                                                                                                                                                        Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                                                                      txt.Name = "Apellido2"
                                                                                                                                                                                                      txt.Properties.ClientSideEvents.KeyUp = "function(s,e){ Fnc_ValidarDatos(1,s) }"
                                                                                                                                                                                                  End Sub).GetHtml()
                                                                                                                                                                    End Sub)
                                                                                                                                          Subgrupo.Caption = "Segundo Apellido"
                                                                                                                                          Subgrupo.CaptionStyle.Font.Italic = True
                                                                                                                                          Subgrupo.CaptionStyle.Font.Bold = True
                                                                                                                                          Subgrupo.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                                      End Sub)
                                                                                                                       form.Items.Add(Sub(Subgrupo)
                                                                                                                                          Subgrupo.SetNestedContent(Sub()
                                                                                                                                                                        Html.DevExpress().ComboBox(Sub(combo)
                                                                                                                                                                                                       combo.Name = "ComboSexo"
                                                                                                                                                                                                       combo.Properties.ValueType = GetType(Integer)
                                                                                                                                                                                                       combo.Properties.Items.Add("Masculino", 1)
                                                                                                                                                                                                       combo.Properties.Items.Add("Femenino", 2)
                                                                                                                                                                                                   End Sub).GetHtml()
                                                                                                                                                                    End Sub)
                                                                                                                                          Subgrupo.Caption = "Sexo"
                                                                                                                                          Subgrupo.CaptionStyle.Font.Italic = True
                                                                                                                                          Subgrupo.CaptionStyle.Font.Bold = True
                                                                                                                                          Subgrupo.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                                      End Sub)
                                                                                                                       form.Items.Add(Sub(Subgrupo)
                                                                                                                                          Subgrupo.SetNestedContent(Sub()
                                                                                                                                                                        Html.DevExpress().DateEdit(Sub(fchNac)
                                                                                                                                                                                                       fchNac.Name = "FchNacimiento"
                                                                                                                                                                                                       fchNac.Properties.MaxDate = Convert.ToString(Today.Year - 18) + "/01/01"
                                                                                                                                                                                                   End Sub).GetHtml()
                                                                                                                                                                    End Sub)
                                                                                                                                          Subgrupo.Caption = "Fecha de Nacimiento"
                                                                                                                                          Subgrupo.CaptionStyle.Font.Italic = True
                                                                                                                                          Subgrupo.CaptionStyle.Font.Bold = True
                                                                                                                                          Subgrupo.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                                      End Sub)
                                                                                                                   End Sub).GetHtml()
%>
        </div>
</div>
<%
                                                                                  End Sub)
                                                       End Sub)
                                     If ViewData("tipo") = 1 Then
                                         setting.Items.Add(Sub(grupo)
                                                               grupo.SetNestedContent(Sub()
                                                                                          Html.DevExpress().ComboBox(Sub(ComboCausaExclusion)
                                                                                                                         ComboCausaExclusion.Name = "ComboAprobarActualización"
                                                                                                                         ComboCausaExclusion.Theme = "DevEx"
                                                                                                                         ComboCausaExclusion.Properties.DropDownStyle = DropDownStyle.DropDownList
                                                                                                                         ComboCausaExclusion.Properties.ValueType = GetType(String)
                                                                                                                         ComboCausaExclusion.Properties.Items.Add("Aprobar Actualización", "1")
                                                                                                                         ComboCausaExclusion.Properties.Items.Add("Rechazar Actualización", "0")
                                                                                                                         ComboCausaExclusion.Properties.EnableClientSideAPI = True
                                                                                                                         ComboCausaExclusion.Properties.ValidationSettings.RequiredField.IsRequired = True
                                                                                                                     End Sub).GetHtml()
                                                                                      End Sub)
                                                               grupo.Caption = "Criterio de Validación"
                                                               grupo.CaptionStyle.Font.Italic = True
                                                               grupo.CaptionStyle.Font.Bold = True
                                                           End Sub)
                                     End If
                                     setting.Items.Add(Sub(grupo)
                                                           grupo.CaptionStyle.Font.Bold = True
                                                           grupo.CaptionStyle.Font.Italic = True
                                                           grupo.CssClass = "margin"
                                                           grupo.CaptionSettings.Location = LayoutItemCaptionLocation.Left
                                                           grupo.ShowCaption = DefaultBoolean.False
                                                           grupo.SetNestedContent(Sub()
                                                                                          %>
                                                                                            <br />
                                                                                            <div id="grid_personasHogarActualizar">       
                                                                                          <%
                                                                                      Html.RenderAction("personas_pre_actualizacion", New With {Key .hog_hogar = Model.Rows(0).Item(0)})
                                                                                          %>
                                                                                            </div><br /><br />
                                                                                          <% 
                                                                                  End Sub)
                                                               
                                                       End Sub)
                                     setting.Items.Add(Sub(grupo)
                                                           grupo.ShowCaption = DefaultBoolean.False
                                                           grupo.SetNestedContent(Sub()
                                                                                      Html.DevExpress().Button(Sub(btnBuscar)
                                                                                                                   btnBuscar.Text = "Actualización"
                                                                                                                   btnBuscar.Name = "btnActualizar"
                                                                                                                   btnBuscar.EnableTheming = False
                                                                                                                   btnBuscar.ControlStyle.CssClass = "Boton"
                                                                                                                   btnBuscar.ClientSideEvents.Click = "function(e,s){fnc_RegistrarPreActualizaciones()}"
                                                                                                               End Sub).GetHtml()
                                                                                  End Sub)
                                                       End Sub)
                                 End Sub).GetHtml()
    %>

