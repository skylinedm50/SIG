<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().FormLayout(
       Sub(frmLayoutEntidadesP)
           frmLayoutEntidadesP.Name = "frmLayoutEntidadesPagadoras"
           frmLayoutEntidadesP.Items.AddGroupItem(
               Sub(groEntidadesP)
                   groEntidadesP.Caption = "Filtro de Entidades Pagadoras"
                   groEntidadesP.Items.Add(
                       Sub(itemEntidadesP)
                           itemEntidadesP.Caption = "Entidad"
                           itemEntidadesP.SetNestedContent(
                               Sub()
                                   Html.RenderAction("pv_cbxEntidadesPagadoras", "Shared")
                               End Sub)
                       End Sub)
                   groEntidadesP.Items.Add(
                       Sub(itemEntidadesP)
                           itemEntidadesP.Caption = "Agencia"
                           itemEntidadesP.SetNestedContent(
                               Sub()
                                   Html.RenderPartial("../Shared/Entidades_Pagadoras/pv_cbxAgencias")
                                   'Html.RenderAction("pv_cbxAgencias", "Shared")
                               End Sub)
                       End Sub)
               End Sub)
       End Sub).GetHtml()%>