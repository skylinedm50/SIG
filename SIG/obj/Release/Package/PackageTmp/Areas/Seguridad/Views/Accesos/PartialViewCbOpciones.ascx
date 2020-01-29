<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%Html.DevExpress.ComboBox(Sub(cb)
                                 cb.Name = "cbOpcion"
                                 cb.Properties.IncrementalFilteringMode = IncrementalFilteringMode.None
                                 cb.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 cb.SelectedIndex = 0
                                 cb.Width = 380
                                 cb.Properties.ValueField = "id_opcion"
                                 cb.Properties.TextField = "desc_opcion"
                                 cb.Properties.ClientSideEvents.SelectedIndexChanged = "cbOpcionChange"
                                 cb.CallbackRouteValues = New With {Key .Controller = "Accesos", Key .Action = "PartialViewCbOpciones", Key .id_opcion = ViewData("id_opcion")}
                             End Sub).BindList(Model).Render() %>