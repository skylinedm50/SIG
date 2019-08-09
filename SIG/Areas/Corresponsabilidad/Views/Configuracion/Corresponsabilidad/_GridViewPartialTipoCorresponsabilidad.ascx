<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress().GridView(Sub(configuracionGridView As GridViewSettings)
                                   configuracionGridView.Name = "AspxGridViewTiposCorresponsabilidad"
                                   configuracionGridView.ControlStyle.CssClass = "GridViewCorres"
                                   configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Configuracion", Key .Action = "GridViewPartialTipoCorresponsabilidad"}
                                   configuracionGridView.Caption = "Tipos de corresponsabilidades"

                                   configuracionGridView.KeyFieldName = "cod_tip_cor"

                                   configuracionGridView.SettingsPager.Visible = False
                                   configuracionGridView.Settings.ShowGroupPanel = False
                                   configuracionGridView.Settings.ShowFilterRow = False
                                   configuracionGridView.SettingsBehavior.AllowSelectByRowClick = False
                                   configuracionGridView.SettingsBehavior.AllowDragDrop = False
                                   configuracionGridView.SettingsBehavior.AllowGroup = False
                                   configuracionGridView.SettingsBehavior.AllowSort = False

                                   configuracionGridView.SettingsPager.PageSizeItemSettings.Visible = False
                                   configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                         configuracionColumn.FieldName = "cod_tip_cor"
                                                                         configuracionColumn.Visible = False
                                                                     End Sub)
                                   configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                         configuracionColumn.FieldName = "nom_tip_cor"
                                                                         configuracionColumn.Caption = "Nombre"
                                                                     End Sub)
                                   configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                         configuracionColumn.FieldName = "des_tip_cor"
                                                                         configuracionColumn.Caption = "Descripción"
                                                                     End Sub)
                                   configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                         configuracionColumn.FieldName = "comp_nombre"
                                                                         configuracionColumn.Caption = "Componente"
                                                                     End Sub)
                               End Sub).Bind(Model).GetHtml()
%>
