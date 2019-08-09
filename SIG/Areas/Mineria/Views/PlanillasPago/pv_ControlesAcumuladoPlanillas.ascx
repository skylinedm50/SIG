<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().FormLayout(
       Sub(frmLayoutFiltro)
           frmLayoutFiltro.Name = "frmLayoutFiltro"
           frmLayoutFiltro.Items.AddGroupItem(
               Sub(groupFiltro)
                   groupFiltro.Caption = "Filtros"
                   groupFiltro.Items.Add(
                       Sub(itemFiltro)
                           itemFiltro.Caption = "Pagos"
                           'itemFiltro.HelpText = "Los pagos seleccionados se mostraran año-número, año-número"
                           itemFiltro.SetNestedContent(
                               Sub()
                                   Html.RenderAction("pv_cbxGridPagos", "Shared")
                                   Html.DevExpress().Label(
                                       Sub(lbl)
                                           lbl.Width = 260
                                           lbl.Text = "Los pagos seleccionados se mostraran año-número, año-número."
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
               End Sub)
       End Sub).GetHtml()%>