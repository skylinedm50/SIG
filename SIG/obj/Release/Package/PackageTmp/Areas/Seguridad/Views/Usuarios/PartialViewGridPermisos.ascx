<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().GridView(
       Sub(settings)
           settings.Name = "gdvPermisos"
           settings.CallbackRouteValues = New With {.Controller = "Usuarios", .Action = "PartialViewGridPermisos"}
           
           settings.CommandColumn.Visible = True
           settings.CommandColumn.ShowSelectCheckbox = True
           'settings.CommandColumn.Caption = ""
           
           settings.KeyFieldName = "id_rol"
           
           settings.Columns.Add("modulo", "Módulo").GroupIndex = 0
           settings.Columns.Add("desc_rol", "Rol")
           
           
           
       End Sub).Bind(Model).GetHtml()%>