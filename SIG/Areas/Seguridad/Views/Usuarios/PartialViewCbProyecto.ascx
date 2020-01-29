<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%Html.DevExpress.ComboBox(Sub(cb)
                                 cb.Name = "CbProyecto"
                                 cb.CallbackRouteValues = New With {Key .Controller = "Usuarios", Key .Action = "PartialViewCbProyecto"}
                                 cb.Properties.IncrementalFilteringMode = IncrementalFilteringMode.None
                                 cb.SelectedIndex = 0
                                 cb.ClientVisible = True
                                 cb.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 cb.Properties.ValueField = "id_unidad"
                                 cb.Properties.TextField = "desc_unidad"
                                 ' cb.Properties.ClientSideEvents.ValueChanged = "cbModuloChange"
                             End Sub).BindList(Model).GetHtml()%>