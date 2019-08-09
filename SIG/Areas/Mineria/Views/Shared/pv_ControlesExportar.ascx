<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().FormLayout(
       Sub(frmLayoutExport)
           frmLayoutExport.Name = "frmLayoutExport"
           frmLayoutExport.ColCount = 2
           frmLayoutExport.Items.Add(
               Sub(item)
                   item.Caption = "Exportar a"
                   item.SetNestedContent(
                       Sub()
                           Html.DevExpress().ComboBox(
                               Sub(cbx)
                                   cbx.Name = "cbxExpotar"
                                   cbx.Width = 100
                                   cbx.SelectedIndex = 0
                                   cbx.Properties.Items.Add("Excel")
                                   cbx.Properties.Items.Add("Csv")
                                   cbx.Properties.Items.Add("Pdf")
                                   cbx.Properties.Items.Add("Rtf")
                                   cbx.Properties.Items.Add("Html")
                                   cbx.Properties.ValueType = GetType(String)
                               End Sub).GetHtml()
                       End Sub)
               End Sub)
           frmLayoutExport.Items.Add(
               Sub(item)
                   item.ShowCaption = DefaultBoolean.False
                   item.SetNestedContent(
                       Sub()
                           Html.DevExpress().Button(
                               Sub(btnConsultar)
                                   btnConsultar.Name = "Exportar"
                                   btnConsultar.UseSubmitBehavior = True
                                   btnConsultar.Text = "Exportar"
                               End Sub).GetHtml()
                       End Sub)
               End Sub)
       End Sub).GetHtml()%>