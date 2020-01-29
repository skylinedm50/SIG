<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Seguridad/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
SIG | Módulo de Seguridad 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <% 

          Html.DevExpress().RenderScripts(Page,
                                          New Script With {.ExtensionSuite = ExtensionSuite.GridView},
                                          New Script With {.ExtensionSuite = ExtensionSuite.Editors},
                                          New Script With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout}
    )

          Html.DevExpress().RenderStyleSheets(Page,
                                New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
                                New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
                                New StyleSheet With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout}
  )
    %>


    <div align = "center"><h2>REPORTES DE USUARIOS</h2></div>
    <script type="text/javascript" src='/Areas/Seguridad/Scripts/usuarios.js'></script>
    
    <style type="text/css">
            .fixStyles {
                font: normal normal normal 25px FontAwesome;
                color: white ;
       
            }
            .fixStyles span {
                display: none;
            }
    </style>
    <% Html.BeginForm("Exportar_Grid_Usuarios", "Usuarios")%>
    <div>
        <%Html.DevExpress.FormLayout(Sub(frm)
                                           frm.Name = "frmLayout"
                                           frm.Width = Unit.Percentage(100)
                                           frm.ColCount = 2
                                           frm.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                           frm.SettingsItemHelpTexts.VerticalAlign = HelpTextVerticalAlign.Middle
                                           frm.Items.Add(Sub(item)
                                                             item.Caption = "Exportar a"
                                                             item.Width = Unit.Percentage(20)
                                                             item.SetNestedContent(Sub()
                                                                                       Html.DevExpress().ComboBox(Sub(cb)
                                                                                                                      cb.Name = "cbxExpotar"
                                                                                                                      cb.SelectedIndex = 0
                                                                                                                      cb.Properties.Items.Add("Excel")
                                                                                                                      cb.Properties.Items.Add("Pdf")
                                                                                                                      cb.Properties.ValueType = GetType(String)
                                                                                                                  End Sub).GetHtml()
                                                                                   End Sub)
                                                         End Sub)
                                           frm.Items.Add(Sub(item)
                                                             item.ShowCaption = DefaultBoolean.False
                                                             item.SetNestedContent(Sub()
                                                                                       Html.DevExpress().Button(Sub(btn)
                                                                                                                    btn.Name = "btnExportar"
                                                                                                                    btn.Text = "Exportar"
                                                                                                                    btn.UseSubmitBehavior = True
                                                                                                                End Sub).GetHtml()
                                                                                   End Sub)
                                                         End Sub)
                                       End Sub).GetHtml()
            %>
        <%Html.DevExpress.FormLayout(Sub(frm)
                                           frm.Name = "frmLayout2"
                                           frm.Width = Unit.Percentage(100)
                                           frm.ColCount = 1
                                           frm.Items.Add(Sub(item)
                                                             item.ShowCaption = DefaultBoolean.False
                                                             item.Width = Unit.Percentage(100)
                                                             item.SetNestedContent(Sub()
                                                                                       ViewContext.Writer.Write("<div id='divGdvUsuarios'>")
                                                                                       Html.RenderAction("PartialViewGridUsuarios")
                                                                                       ViewContext.Writer.Write("</div>")
                                                                                   End Sub)
                                                         End Sub)
                                       End Sub).GetHtml() %>
    </div>

   
</asp:Content>
