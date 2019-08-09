<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress.GridView(Sub(configuracionGridView)
                                 configuracionGridView.Name = "AspxGridViewCargas"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Parametros", Key .Action = "GridViewCargas"}
                                 configuracionGridView.CommandColumn.Visible = True
                                 configuracionGridView.SettingsBehavior.AllowSelectByRowClick = False
                                 configuracionGridView.ClientSideEvents.SelectionChanged = "function(s,e){ s.GetSelectedFieldValues('car_codigo', objManejoEsquema.Cargas.Agregar); }"
                                 
                                 configuracionGridView.KeyFieldName = "car_codigo"
		
                                 configuracionGridView.SettingsPager.Visible = True
                                 configuracionGridView.Settings.ShowGroupPanel = False
                                 configuracionGridView.Settings.ShowFilterRow = False
                                 configuracionGridView.Caption = "Configuración de Cargas"
                                 
                                 configuracionGridView.CommandColumn.ShowSelectCheckbox = True
                                 configuracionGridView.CommandColumn.Caption = "Pertenece"
                                 configuracionGridView.CommandColumn.AllowDragDrop = DefaultBoolean.False
                                 
                                 configuracionGridView.SettingsPager.PageSize = "18"
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "car_codigo"
                                                                       configuracionColumn.Caption = "Código"
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "car_nombre"
                                                                       configuracionColumn.Caption = "Carga"
                                                                   End Sub)
                             End Sub).Bind(Model).GetHtml()
%>