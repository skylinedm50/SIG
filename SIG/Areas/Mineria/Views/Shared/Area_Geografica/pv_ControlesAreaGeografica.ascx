<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().FormLayout(
       Sub(frmLayoutAG)
           frmLayoutAG.Name = "frmLayoutAG"
           frmLayoutAG.Items.AddGroupItem(
               Sub(groAG)
                   groAG.Caption = "Área Geográfica"
                   groAG.Items.Add(
                       Sub(itemAG)
                           itemAG.Caption = "Departamento"
                           itemAG.SetNestedContent(
                               Sub()
                                   Html.RenderAction("pv_cbxDepartamentos", "Shared")
                               End Sub)
                       End Sub)
                   groAG.Items.Add(
                       Sub(itemAG)
                           itemAG.Caption = "Municipio"
                           itemAG.SetNestedContent(
                               Sub()
                                   'Html.RenderPartial("~/Mineria/Views/Shared/Area_Geografica/pv_cbxMunicipios.ascx")
                                   'Html.RenderAction("pv_cbxMunicipios", "Shared")
                                   Html.RenderPartial("../Shared/Area_Geografica/pv_cbxMunicipios")
                               End Sub)
                       End Sub)
                   groAG.Items.Add(
                       Sub(itemAG)
                           itemAG.Caption = "Aldea"
                           itemAG.SetNestedContent(
                               Sub()
                                   'Html.RenderPartial("~/Mineria/Views/Shared/Area_Geografica/pv_cbxAldeas.ascx")
                                   'Html.RenderAction("pv_cbxAldeas", "Shared")
                                   Html.RenderPartial("../Shared/Area_Geografica/pv_cbxAldeas")
                               End Sub)
                       End Sub)
               End Sub)
       End Sub).GetHtml()%>