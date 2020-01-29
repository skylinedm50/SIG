<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().TreeList(Sub(settings)
                                   settings.Name = "treePermisos"
                                   settings.CallbackRouteValues = New With {.Controller = "Usuarios", .Action = "PartialViewTreeListPermisos"}
                                   settings.Columns.Add("descripcion")
                                   settings.KeyFieldName = "cod"
                                   settings.ParentFieldName = "padre"
                               End Sub).Bind(Model).GetHtml()%>