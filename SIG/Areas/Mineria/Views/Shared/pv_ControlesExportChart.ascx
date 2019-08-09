<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().FormLayout(
       Sub(frmLayoutExport)
           frmLayoutExport.Name = "frmLayoutExportChart"
           frmLayoutExport.ColCount = 2
           frmLayoutExport.Items.Add(
               Sub(item)
                   item.Caption = "Exportar a"
                   item.SetNestedContent(
                       Sub()
                           Html.DevExpress().ComboBox(
                               Sub(cbx)
                                   cbx.Name = "cbxExportarChart"
                                   cbx.Width = 100
                                   cbx.SelectedIndex = 0
                                   cbx.Properties.Items.Add("xls")
                                   cbx.Properties.Items.Add("xlsx")
                                   cbx.Properties.Items.Add("pdf")
                                   cbx.Properties.Items.Add("rtf")
                                   'cbx.Properties.Items.Add("mht")
                                   cbx.Properties.Items.Add("png")
                                   cbx.Properties.Items.Add("jpeg")
                                   'cbx.Properties.ValueType = GetType(String)
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
                                   btnConsultar.Name = "btnExportarChart"
                                   btnConsultar.Text = "Exportar"
                                   btnConsultar.ClientSideEvents.Click = "function(s, e) {chart.SaveToDisk(cbxExportarChart.GetText())}"
                               End Sub).GetHtml()
                       End Sub)
               End Sub)
       End Sub).GetHtml()%>