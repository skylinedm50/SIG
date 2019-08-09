<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().FormLayout(
       Sub(frmlayInfoHogar)
           frmlayInfoHogar.Name = "frmlayInfoHogar"
           frmlayInfoHogar.Items.AddGroupItem(
               Sub(groupInfoHogar)
                   groupInfoHogar.Caption = "Titular Actual"
                   groupInfoHogar.ColCount = 2
                   groupInfoHogar.Items.Add(
                       Sub(item)
                           item.Caption = "Identidad"
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().Label(
                                       Sub(lbl)
                                           lbl.Name = "lblIdentidad"
                                           lbl.Text = ViewData("identidad")
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
                   groupInfoHogar.Items.Add(
                       Sub(item)
                           item.Caption = "Nombre"
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().Label(
                                       Sub(lbl)
                                           lbl.Name = "lblNombre"
                                           lbl.Text = ViewData("nombre")
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
               End Sub)
           frmlayInfoHogar.Items.AddGroupItem(
               Sub(groupInfoHogar)
                   groupInfoHogar.Caption = "Información Geográfica"
                   groupInfoHogar.ColCount = 2
                   groupInfoHogar.Items.Add(
                       Sub(item)
                           item.Caption = "Departamento"
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().Label(
                                       Sub(lbl)
                                           lbl.Name = "lblDepartamento"
                                           lbl.Text = ViewData("departamento")
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
                   groupInfoHogar.Items.Add(
                       Sub(item)
                           item.Caption = "Municipio"
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().Label(
                                       Sub(lbl)
                                           lbl.Name = "lblMunicipio"
                                           lbl.Text = ViewData("municipio")
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
                   groupInfoHogar.Items.Add(
                       Sub(item)
                           item.Caption = "Aldea"
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().Label(
                                       Sub(lbl)
                                           lbl.Name = "lblAldea"
                                           lbl.Text = ViewData("aldea")
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
                   groupInfoHogar.Items.Add(
                       Sub(item)
                           item.Caption = "Caserio"
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().Label(
                                       Sub(lbl)
                                           lbl.Name = "lblCaserio"
                                           lbl.Text = ViewData("caserio")
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
               End Sub)
       End Sub).GetHtml()%>