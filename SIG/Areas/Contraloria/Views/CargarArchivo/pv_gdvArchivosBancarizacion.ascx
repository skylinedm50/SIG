<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Dim settings As GridViewSettings = New GridViewSettings

    settings.Name = "gdvArchivosBancarizacion"
    settings.Caption = "Archivos de Bancarización"
    settings.SettingsPager.PageSize = 10
    settings.KeyFieldName = "cod_arch_bancarizacion"

    settings.EnablePagingCallbackAnimation = True
    settings.EnableCallbackAnimation = True

    settings.ClientSideEvents.SelectionChanged = "gdvArchivosBancarizacionSelectionChanged"

    settings.Columns.Add("desc_tipo_arch_banc", "Tipo de Archivo")
    settings.Columns.Add("nombre_arch_bancarizacion", "Nombre Archivo")
    settings.Columns.Add("desc_estado_arch_bancarizacion", "Estado")
    settings.Columns.Add("cod_arch_bancarizacion").Visible = False

    Dim strArchivos As String = ViewData("strArchivos")
    Dim inicio As String = ViewData("inicio")
    Dim fin As String = ViewData("fin")

    settings.CommandColumn.Visible = True
    settings.CommandColumn.ShowSelectCheckbox = True
    settings.CommandColumn.Caption = "Slc"
    settings.SettingsBehavior.AllowSelectSingleRowOnly = True
    settings.CallbackRouteValues = New With {Key .Controller = "CargarArchivo", Key .Action = "pv_gdvArchivosBancarizacion", Key .periodo = ViewData("periodo"), Key .banco = ViewData("banco")}

    Html.DevExpress().GridView(settings).Bind(Model).GetHtml()
    %>