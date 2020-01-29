<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

 <% Html.DevExpress.GridView(Sub(gdv)
                                  gdv.Name = "GdvModulos"
                                  gdv.Width = Unit.Percentage(100)

                                  gdv.CallbackRouteValues = New With {Key .Controller = "Monitoreo", Key .Action = "PVGV_Actividad"}

                                  gdv.Settings.ShowFilterRow = True
                                  gdv.Settings.ShowFilterRowMenu = True
                                  gdv.SettingsBehavior.EnableRowHotTrack = True
                                  gdv.SettingsPager.PageSizeItemSettings.Visible = True
                                  gdv.SettingsPager.PageSizeItemSettings.Items = New String() {"10", "20", "50"}
                                  gdv.SettingsPager.PageSize = 20
                                  gdv.Columns.Add("nom_usuario", "Usuario")
                                  gdv.Columns.Add("operac_reali_log", "Operación")
                                  gdv.Columns.Add(Sub(col)
                                                      col.FieldName = "modulo"
                                                      col.Caption = "Módulo"
                                                      col.EditorProperties().ComboBox(Sub(cb)
                                                                                          cb.Items.Add("Corresponsabilidad")
                                                                                          cb.Items.Add("Incorporación y Actualización")
                                                                                      End Sub)
                                                  End Sub) '"modulo", "Módulo"
                                  gdv.Columns.Add(Sub(col)
                                                      col.Caption = "Fecha"
                                                      col.FieldName = "fch_ejecucion_log"
                                                      col.EditorProperties().DateEdit(Sub(dt)
                                                                                      End Sub)
                                                  End Sub)
                                  gdv.Columns.Add(Sub(col)
                                                      col.Caption = "Hora"
                                                      col.FieldName = "fch_ejecucion_log"
                                                      col.EditorProperties().TimeEdit(Sub(tm)
                                                                                      End Sub)
                                                  End Sub)
                              End Sub).Bind(Model).GetHtml()%>