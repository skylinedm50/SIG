<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().FileManager(
       Sub(settings)
           settings.Name = "fmArchivos"
           settings.CallbackRouteValues = New With {Key .Controller = "GeneracionDocumentos", Key .Action = "pv_fmArchivos"}
           settings.DownloadRouteValues = New With {Key .Controller = "GeneracionDocumentos", Key .Action = "downloadFiles"}
           settings.SettingsToolbar.ShowDownloadButton = True
           
           'settings.Settings.ThumbnailFolder = Url.Content("~/Content/FileManager/Thumbnails")
           settings.EnableCallbackAnimation = True
           
           settings.SettingsEditing.AllowDownload = True
           settings.SettingsEditing.AllowCopy = False
           settings.SettingsEditing.AllowCreate = False
           settings.SettingsEditing.AllowDelete = False
           settings.SettingsEditing.AllowMove = False
           settings.SettingsEditing.AllowRename = False
           settings.SettingsUpload.AdvancedModeSettings.EnableMultiSelect = False
           settings.SettingsUpload.UseAdvancedUploadMode = False
           settings.SettingsUpload.Enabled = False
           
       End Sub).BindToFolder(Model).GetHtml()
    %>