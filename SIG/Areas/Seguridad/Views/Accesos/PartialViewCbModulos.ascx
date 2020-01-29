<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress.ComboBox(Sub(cb)
                                 cb.Name = "cbModulo"
                                 cb.Width = 380
                                 cb.CallbackRouteValues = New With {Key .Controller = "Accesos", Key .Action = "PartialViewCbModulos"}
                                 cb.Properties.IncrementalFilteringMode = IncrementalFilteringMode.None
                                 cb.SelectedIndex = 0
                                 cb.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 cb.Properties.ValueField = "id_modulos"
                                 cb.Properties.TextField = "nom_modulo"
                                 cb.Properties.ClientSideEvents.ValueChanged = "cbModuloChange"
                             End Sub).BindList(Model).GetHtml()
    %>