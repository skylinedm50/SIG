<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%Html.DevExpress.ComboBox(Sub(cb)
                                 cb.Name = "cbNuevoModulo"
                                 cb.Width = 380
                                 cb.CallbackRouteValues = New With {Key .Controller = "Roles", Key .Action = "PartialViewCbNuevoModulo"}
                                 cb.Properties.IncrementalFilteringMode = IncrementalFilteringMode.None
                                 cb.SelectedIndex = 0
                                 cb.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 cb.Properties.ValueField = "id_modulos"
                                 cb.Properties.TextField = "nom_modulo"
                             End Sub).BindList(Model).GetHtml()%>