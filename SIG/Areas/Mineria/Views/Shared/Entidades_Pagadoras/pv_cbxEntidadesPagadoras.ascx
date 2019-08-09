<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
    Sub(settings)
        settings.Name = "cbxPagadores"
        settings.Width = 180
        settings.CallbackRouteValues = New With {.Controller = "Shared", .Action = "pv_cbxEntidadesPagadoras"}
        settings.Properties.DropDownStyle = DropDownStyle.DropDownList
        settings.Properties.TextField = "nombre_pagador"
        settings.Properties.ValueField = "cod_pagador"
        settings.Properties.NullText = "Seleccione una Entidad"
        settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { cbxAgencias.PerformCallback(); }"
    End Sub).BindList(Model).GetHtml()%>