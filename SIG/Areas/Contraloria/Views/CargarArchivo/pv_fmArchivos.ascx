<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().FileManager(
                            Sub(settings)
                                settings.Name = "fmArchivos"
                                settings.CallbackRouteValues = New With {Key .Controller = "CargarArchivo", Key .Action = "pv_fmArchivos"}
                                settings.DownloadRouteValues = New With {Key .Controller = "CargarArchivo", Key .Action = "downloadFiles"}
                               'settings.SettingsToolbar.ShowDownloadButton = True
                               settings.SettingsEditing.AllowDownload = True

                               'settings.Settings.ThumbnailFolder = Url.Content("~/Content/FileManager/Thumbnails")
                               settings.Settings.InitialFolder = "Recepcion/"
                                settings.EnableCallbackAnimation = True
                                settings.SettingsFileList.View = FileListView.Details
                                settings.SettingsFileList.DetailsViewSettings.AllowColumnResize = True

                                settings.SettingsEditing.AllowDownload = True
                                settings.SettingsEditing.AllowCopy = False
                                settings.SettingsEditing.AllowCreate = False
                                settings.SettingsEditing.AllowDelete = False
                                settings.SettingsEditing.AllowMove = False
                                settings.SettingsEditing.AllowRename = False
                                settings.SettingsUpload.AdvancedModeSettings.EnableMultiSelect = True
                                settings.SettingsUpload.UseAdvancedUploadMode = False
                                settings.SettingsUpload.Enabled = False
                                settings.Width = 1000

                            End Sub).BindToFolder(Model).GetHtml()
    %>