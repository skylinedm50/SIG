<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().FormLayout(
       Sub(frmLayoutPagos)
           frmLayoutPagos.Name = "frmLayoutPagos"
           frmLayoutPagos.Items.AddGroupItem(
               Sub(groPagos)
                   groPagos.Caption = "Filtro de Pago"
                   groPagos.Items.Add(
                       Sub(itemPagos)
                           itemPagos.Caption = "Año"
                           itemPagos.SetNestedContent(
                               Sub()
                                   Html.RenderAction("pv_cbxAnos", "Shared")
                               End Sub)
                       End Sub)
                   groPagos.Items.Add(
                       Sub(itemPagos)
                           itemPagos.Caption = "Pago"
                           itemPagos.SetNestedContent(
                               Sub()
                                   Html.RenderPartial("../Shared/Pagos/pv_cbxPagosAno")
                                   'Html.RenderAction("pv_cbxPagosAno", "Shared")
                               End Sub)
                       End Sub)
               End Sub)
       End Sub).GetHtml()%>