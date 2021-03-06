﻿<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Seguridad/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>



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
<h2 align="center">INICIOS DE SESIÓN</h2>
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
                                                    Html.RenderAction("PVGV_InicioSesion")
                                                End Sub)
                                            End Sub)
                                       End Sub).GetHtml()%>
    </div>
</asp:Content>
