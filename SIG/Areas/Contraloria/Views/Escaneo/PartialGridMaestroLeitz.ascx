<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().GridView(
       Sub(settings)
           settings.Name = "gvLeitz"
           settings.Caption = "LEITZ"
           settings.CallbackRouteValues = New With { _
               Key .Controller = "Escaneo", _
               Key .Action = "PartialGridMaestroLeitz", _
               Key .periodo = ViewData("periodo"), _
               Key .fondo = ViewData("fondo"), _
               Key .dpto = ViewData("dpto"), _
               Key .tipo = ViewData("tipo") _
           }
           
           settings.SettingsPager.Position = PagerPosition.Bottom
           settings.SettingsPager.FirstPageButton.Visible = True
           settings.SettingsPager.LastPageButton.Visible = True
           settings.SettingsPager.PageSizeItemSettings.Visible = True
           settings.SettingsPager.PageSizeItemSettings.Items = New String() {"10", "20", "50"}
           settings.SettingsBehavior.AllowGroup = True
           settings.SettingsBehavior.AllowSort = True
           settings.Settings.ShowGroupPanel = True
           
           settings.EnableCallbackAnimation = True
           settings.EnablePagingCallbackAnimation = True
           settings.EnableCallbackCompression = True
           
           'settings.CommandColumn.Visible = True
           'settings.CommandColumn.ShowSelectCheckbox = True
           'settings.CommandColumn.Caption = "Cerrar"
           
           settings.Settings.ShowHeaderFilterButton = True
           settings.SettingsPopup.HeaderFilter.Height = 200
           
           'settings.ClientSideEvents.SelectionChanged = "OnSelectionChanged"
           'settings.ClientSideEvents.BeginCallback = "OnBeginCallback"
           settings.KeyFieldName = "cod_leitz"
           
           'código para maestro detalle
           settings.SettingsDetail.AllowOnlyOneMasterRowExpanded = True
           settings.SettingsDetail.ShowDetailRow = True
           
           settings.SetDetailRowTemplateContent(
               Sub(row)
                   Html.RenderAction("PartialGridDetalleDocumentosLeitz", New With { _
                       Key .periodo = ViewData("periodo"), _
                       Key .fondo = ViewData("fondo"), _
                       Key .dpto = ViewData("dpto"), _
                       Key .tipo = ViewData("tipo"), _
                       Key .leitz = DataBinder.Eval(row.DataItem, "cod_leitz") _
                       })
               End Sub)
           
           settings.Columns.AddBand(
               Sub(band)
                   band.Caption = "Filtros"
                   band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   'band.Columns.Add("nom_periodo", "Período")
                   'band.Columns.Add("nom_fondo", "Fondo")
                   band.Columns.Add("pag_nombre", "Período")
                   'band.Columns.Add("nombre_fondo", "Fondo")
                   band.Columns.Add("fond_nombre", "Fondo")
               End Sub)
           settings.Columns.AddBand(
               Sub(band)
                   band.Caption = "Leitz"
                   band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   band.Columns.Add("numero_leitz", "Nro")
                   band.Columns.Add("descripcion_leitz", "Descripción")
               End Sub)
           
       End Sub).Bind(Model).Render()%>