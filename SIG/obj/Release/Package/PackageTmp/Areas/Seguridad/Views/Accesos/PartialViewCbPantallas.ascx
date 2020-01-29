<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%Html.DevExpress.ComboBox(Sub(cb)
                                 cb.Name = "cbPantalla"
                                 cb.Width = 380
                                 cb.Properties.IncrementalFilteringMode = IncrementalFilteringMode.None
                                 cb.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 cb.SelectedIndex = 0
                                 cb.Properties.ValueField = "id_actividad"
                                 cb.Properties.TextField = "desc_actividad"
                             End Sub).BindList(Model).GetHtml() %>