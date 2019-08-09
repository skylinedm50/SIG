<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().GridView(
                  Sub(settings)
                      settings.Name = "gdvDetalleArchivoBancarizacion"
                      settings.Caption = "Detalle Archivo"
                      settings.CallbackRouteValues = New With {Key .Controller = "CargarArchivo", Key .Action = "pv_gdvDetalleArchivoBancarizacion", Key .archivo = ViewData("archivo")}

                      settings.EnablePagingCallbackAnimation = True
                      settings.EnableCallbackAnimation = True

                      settings.SettingsPager.Position = PagerPosition.Bottom
                      settings.SettingsPager.FirstPageButton.Visible = True
                      settings.SettingsPager.LastPageButton.Visible = True
                      settings.SettingsPager.PageSizeItemSettings.Visible = True
                      settings.SettingsPager.PageSizeItemSettings.Items = New String() {"10", "20", "50"}
                      settings.SettingsBehavior.AllowGroup = True
                      settings.SettingsBehavior.AllowSort = True
                      settings.Settings.ShowGroupPanel = True

                      settings.Settings.ShowHeaderFilterButton = True
                      settings.SettingsPopup.HeaderFilter.Height = 200

                      settings.Width = Unit.Percentage(160)
                      settings.Settings.HorizontalScrollBarMode = ScrollBarMode.Visible

                  End Sub).Bind(Model).GetHtml()%>