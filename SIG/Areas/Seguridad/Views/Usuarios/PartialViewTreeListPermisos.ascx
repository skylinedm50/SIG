<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().TreeList(
       Sub(settings)
           settings.Name = "treePermisos"
           
           settings.ClientSideEvents.BeginCallback = "treePermisosBeginCallback"
           settings.CallbackRouteValues = New With {.Controller = "Usuarios", .Action = "PartialViewTreeListPermisos"}
           
           settings.KeyFieldName = "cod"
           settings.ParentFieldName = "padre"
           
           settings.SettingsSelection.Enabled = True

           settings.Columns.Add("descripcion")
           
           'For Each row As System.Data.DataRow In Model
           '    If (row.Item(3) = 1) Then
           '        settings.Nodes.Item(row(0)).Selected = True
           '    End If
           'Next
           
           'settings.Nodes.Item(1).Selected = True
           'settings.Nodes.Item(0).Selected = True
           
           
           
           
       End Sub).Bind(Model).GetHtml()%>