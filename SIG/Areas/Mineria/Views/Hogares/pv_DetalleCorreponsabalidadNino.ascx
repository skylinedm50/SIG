<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().FormLayout(
       Sub(frmDetalleCorr)
           frmDetalleCorr.Name = "frmDetalleNiño"
           frmDetalleCorr.ColCount = 1
           frmDetalleCorr.SettingsItems.HorizontalAlign = FormLayoutHorizontalAlign.Left
           frmDetalleCorr.SettingsItemCaptions.HorizontalAlign = FormLayoutHorizontalAlign.Right
           frmDetalleCorr.Style.Add("margin", "0 auto")
           frmDetalleCorr.Items.AddGroupItem(
               Sub(groupDetalleCorr)
                   groupDetalleCorr.Caption = "Información del Niño"
                   groupDetalleCorr.ColCount = 2
                   groupDetalleCorr.Items.Add(
                       Sub(itemDetalleCorr)
                           itemDetalleCorr.Caption = "Identidad"
                           itemDetalleCorr.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("identidad")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   groupDetalleCorr.Items.Add(
                       Sub(itemDetalleCorr)
                           itemDetalleCorr.Caption = "Nombre"
                           itemDetalleCorr.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("nombre")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   groupDetalleCorr.Items.Add(
                       Sub(itemDetalleCorr)
                           itemDetalleCorr.Caption = "Género"
                           itemDetalleCorr.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("genero")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   groupDetalleCorr.Items.Add(
                       Sub(itemDetalleCorr)
                           itemDetalleCorr.Caption = "Fecha de Nacimiento"
                           itemDetalleCorr.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("fecha_nacimiento")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
               End Sub)
           frmDetalleCorr.Items.AddGroupItem(
               Sub(groupDetalleCorr)
                   groupDetalleCorr.Caption = "Información de Correponsabilidad"
                   groupDetalleCorr.ColCount = 2
                   groupDetalleCorr.Items.Add(
                       Sub(itemDetalleCorr)
                           itemDetalleCorr.Caption = "Elegibilidad"
                           itemDetalleCorr.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("elegibilidad")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   groupDetalleCorr.Items.Add(
                       Sub(itemDetalleCorr)
                           itemDetalleCorr.Caption = "Ciclo"
                           itemDetalleCorr.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("ciclo")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   groupDetalleCorr.Items.Add(
                       Sub(itemDetalleCorr)
                           itemDetalleCorr.Caption = "Correponsabilidad"
                           itemDetalleCorr.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("correponsabilidad")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   groupDetalleCorr.Items.Add(
                       Sub(itemDetalleCorr)
                           itemDetalleCorr.Caption = "Cumpliendo"
                           itemDetalleCorr.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("cumplimiento")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
               End Sub)
           
           If ViewData("elegibilidad") = "EDUCACION" Then
               
               frmDetalleCorr.Items.AddGroupItem(
                   Sub(groupDetalleCorr)
                       groupDetalleCorr.Caption = "Detalle de Correponsabilidad"
                       groupDetalleCorr.ColCount = 2
                       groupDetalleCorr.Items.Add(
                           Sub(itemDetalleCorr)
                               itemDetalleCorr.Caption = "Centro Educativo"
                               itemDetalleCorr.ColSpan = 2
                               itemDetalleCorr.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().Label(
                                               Sub(lbl)
                                                   lbl.Text = ViewData("centro")
                                               End Sub).GetHtml()
                                       End Sub)
                           End Sub)
                       groupDetalleCorr.Items.Add(
                           Sub(itemDetalleCorr)
                               itemDetalleCorr.Caption = "Grado"
                               itemDetalleCorr.ColSpan = 2
                               itemDetalleCorr.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().Label(
                                               Sub(lbl)
                                                   lbl.Text = ViewData("grado")
                                               End Sub).GetHtml()
                                       End Sub)
                           End Sub)
                   End Sub)
               
           Else
               
               frmDetalleCorr.Items.AddGroupItem(
                   Sub(groupDetalleCorr)
                       groupDetalleCorr.Caption = "Detalle de Correponsabilidad"
                       groupDetalleCorr.ColCount = 2
                       groupDetalleCorr.Items.Add(
                           Sub(itemDetalleCorr)
                               itemDetalleCorr.Caption = "Centro de Salud"
                               itemDetalleCorr.ColSpan = 2
                               itemDetalleCorr.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().Label(
                                               Sub(lbl)
                                                   lbl.Text = ViewData("centro")
                                               End Sub).GetHtml()
                                       End Sub)
                           End Sub)
                       groupDetalleCorr.Items.Add(
                           Sub(itemDetalleCorr)
                               itemDetalleCorr.Caption = "Visitas"
                               itemDetalleCorr.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().Label(
                                               Sub(lbl)
                                                   lbl.Text = ViewData("visitas")
                                               End Sub).GetHtml()
                                       End Sub)
                           End Sub)
                       groupDetalleCorr.Items.Add(
                           Sub(itemDetalleCorr)
                               itemDetalleCorr.Caption = "Ultima Visita"
                               itemDetalleCorr.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().Label(
                                               Sub(lbl)
                                                   lbl.Text = ViewData("ultima_visita")
                                               End Sub).GetHtml()
                                       End Sub)
                           End Sub)
                   End Sub)
           End If
           
       End Sub).Render()%>