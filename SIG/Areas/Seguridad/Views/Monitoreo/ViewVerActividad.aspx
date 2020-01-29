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


<div align="center">
<h2>ACTIVIDAD DEL SISTEMA</h2>
</div>
    <div>
    <%Html.DevExpress.FormLayout(Sub(frm)
                                       frm.Name = "frmLaout"

                                       frm.Width = Unit.Percentage(100)
                                       frm.ColCount = 1
                                       frm.Items.Add(
                                        Sub(item)
                                            item.ShowCaption = DefaultBoolean.False
                                            item.SetNestedContent(
                                            Sub()
                                                Html.RenderAction("PVGV_Actividad")
                                            End Sub)
                                        End Sub)
                                   End Sub).GetHtml()%>
    </div>
</asp:Content>
