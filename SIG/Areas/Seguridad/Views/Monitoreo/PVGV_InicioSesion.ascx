<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress.GridView(Sub(gdv)
                                 gdv.Name = "GdvInicioSesion"
                                 gdv.Width = Unit.Percentage(100)
                                 gdv.CallbackRouteValues = New With {Key .Controller = "Monitoreo", Key .Action = "PVGV_InicioSesion"}

                                 gdv.Settings.ShowFilterRow = True
                                 gdv.Settings.ShowFilterRowMenu = True
                                 gdv.SettingsBehavior.EnableRowHotTrack = True
                                 gdv.SettingsPager.PageSizeItemSettings.Visible = True
                                 gdv.SettingsPager.PageSizeItemSettings.Items = New String() {"10", "20", "50"}
                                 gdv.SettingsPager.PageSize = 20
                                 gdv.Columns.Add("nombre_usuario_intento_logeo", "Usuario")
                                 gdv.Columns.Add("mac_intento_logeo", "Mac")
                                 gdv.Columns.Add("ip_intento_logeo", "IP")
                                 gdv.Columns.Add("ip_proxy_intento_logeo", "IP Proxy")
                                 gdv.Columns.Add("hostname_intento_logeo", "Hostname")
                                 gdv.Columns.Add(Sub(col)
                                                     col.Caption = "Fecha"
                                                     col.FieldName = "fecha_intento_logeo"
                                                     col.EditorProperties().DateEdit(Sub(dt)
                                                                                     End Sub)
                                                 End Sub)
                                 gdv.Columns.Add(Sub(col)
                                                     col.Caption = "Hora"
                                                     col.FieldName = "fecha_intento_logeo"
                                                     col.EditorProperties().TimeEdit(Sub(tm)
                                                                                     End Sub)
                                                 End Sub)
                             End Sub).Bind(Model).GetHtml()%>

