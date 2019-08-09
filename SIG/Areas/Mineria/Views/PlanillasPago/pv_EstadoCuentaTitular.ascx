<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().FormLayout(
       Sub(Settings)
           Settings.Name = "frmEstadoCuenta"
           Settings.ColCount = 1
           Settings.Style.Add("margin", "0 auto")
           Settings.Items.AddGroupItem(
               Sub(group)
                   group.Caption = "Información de Planilla"
                   group.ColCount = 2
                   group.Items.Add(
                       Sub(item)
                           item.Caption = "Nro. Identidad"
                           item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("identidad")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   group.Items.Add(
                       Sub(item)
                           item.Caption = "Nombre"
                           item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("nombre")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   group.Items.Add(
                       Sub(item)
                           item.Caption = "Proyeccion Corta"
                           item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("proyeccion_corta")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   group.Items.Add(
                       Sub(item)
                           item.Caption = "Estado"
                           item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("estado")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   group.Items.Add(
                       Sub(item)
                           item.Caption = "Monto"
                           item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("monto")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   group.Items.Add(
                       Sub(item)
                           item.Caption = "Deducción"
                           item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("deduccion")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   If ViewData("estado") = "Programado" And ViewData("cobro") = "Cobró" Then
                       'mostrar fecha de cobro
                       group.Items.Add(
                           Sub(item)
                               item.Caption = "Estado Pago"
                               item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().Label(
                                               Sub(lbl)
                                                   lbl.Text = ViewData("cobro")
                                               End Sub).GetHtml()
                                       End Sub)
                           End Sub)
                       group.Items.Add(
                           Sub(item)
                               item.Caption = "Fecha Cobro"
                               item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().Label(
                                               Sub(lbl)
                                                   lbl.Text = ViewData("fecha_cobro")
                                               End Sub).GetHtml()
                                       End Sub)
                           End Sub)
                   ElseIf ViewData("estado") = "Programado" And ViewData("cobro") = "No Cobró" Then
                       group.Items.Add(
                           Sub(item)
                               item.Caption = "Estado Pago"
                               item.ColSpan = 2
                               item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().Label(
                                               Sub(lbl)
                                                   lbl.Text = ViewData("cobro")
                                               End Sub).GetHtml()
                                       End Sub)
                           End Sub)
                   End If
                   group.Items.Add(
                       Sub(item)
                           item.Caption = "Elegibilidad"
                           item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("elegibilidad")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   group.Items.Add(
                       Sub(item)
                           item.Caption = "Descripción"
                           item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("desc_elegibilidad")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   group.Items.Add(
                       Sub(item)
                           item.Caption = "Componente"
                           item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("componente")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   group.Items.Add(
                       Sub(item)
                           item.Caption = "Descripción"
                           item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("desc_componente")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   group.Items.Add(
                       Sub(item)
                           item.Caption = "Corresponsabilidad"
                           item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("corresponsabilidad")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   group.Items.Add(
                       Sub(item)
                           item.Caption = "Descripción"
                           item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("desc_corresponsabilidad")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   group.Items.Add(
                       Sub(item)
                           item.Caption = "Proyección"
                           item.ColSpan = 2
                           item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("proyeccion")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
                   group.Items.Add(
                       Sub(item)
                           item.Caption = "Estado del Hogar"
                           item.ColSpan = 2
                           item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().Label(
                                           Sub(lbl)
                                               lbl.Text = ViewData("estado_hogar")
                                           End Sub).GetHtml()
                                   End Sub)
                       End Sub)
               End Sub)
           Settings.Items.AddGroupItem(
               Sub(group)
                   group.Caption = "Estado de Cuenta"
                   group.ColCount = 1
                   group.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                   group.HorizontalAlign = FormLayoutHorizontalAlign.Center
                   group.Items.Add(
                       Sub(item)
                           item.ShowCaption = DefaultBoolean.False
                           item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress.GridView(
                                       Sub(gdv)
                                           gdv.Name = "gdvEstadoCuenta"
                                           gdv.Caption = "Estado de Cuenta Participante"
                                           gdv.Enabled = False
                                           
                                           gdv.Columns.AddBand(
                                               Sub(band)
                                                   band.Caption = "NIVEL"
                                                   band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                   band.Columns.Add("nivel", "Nivel")
                                                   band.Columns.Add("nivel_ciclo", "Ciclo")
                                               End Sub)
                                           gdv.Columns.AddBand(
                                               Sub(band)
                                                   band.Caption = "NIÑOS"
                                                   band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                   band.Columns.Add("niños_total", "Total")
                                                   band.Columns.Add("niños_cumpliendo", "Cumpliendo")
                                                   band.Columns.Add("niños_apercibidos", "Apercibidos")
                                                   band.Columns.Add("niños_no_cumpliendo", "No Cumpliendo")
                                               End Sub)
                                           gdv.Columns.Add("monto_nivel_neto", "Monto Nivel")
                                       End Sub).Bind(ViewData("estado_cuenta")).GetHtml()
                               End Sub)
                       End Sub)
               End Sub)
           Settings.Items.AddGroupItem(
               Sub(group)
                   group.Caption = "Información de los Niños"
                   group.ColCount = 1
                   group.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                   group.HorizontalAlign = FormLayoutHorizontalAlign.Center
                   group.Items.Add(
                       Sub(item)
                           item.ShowCaption = DefaultBoolean.False
                           item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                           item.SetNestedContent(
                               Sub()
                                   Html.RenderAction("pv_gdvListadoNinos", "PlanillasPago", New With {Key .pago = ViewData("pago"), Key .identidad = ViewData("identidad")})
                               End Sub)
                       End Sub)
               End Sub)
       End Sub).GetHtml()%>