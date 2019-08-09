<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().GridView(
       Sub(settings)
           settings.Name = "gdvPersonas"
           settings.Caption = "Listado de Personas encontradas"
           settings.CallbackRouteValues = New With {Key .Controller = "Hogares", Key .Action = "pv_gdvPersonas", Key .identidad = ViewData("identidad"), Key .nombre = ViewData("nombre")}
           settings.KeyFieldName = "cod_hogar"
           
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
           
           settings.Settings.ShowHeaderFilterButton = True
           settings.Settings.ShowFilterRow = True
           settings.SettingsPopup.HeaderFilter.Height = 200
           
           settings.CommandColumn.Visible = True
           settings.CommandColumn.ShowSelectCheckbox = True
           settings.CommandColumn.Caption = "Slc"
           settings.SettingsBehavior.AllowSelectSingleRowOnly = True
           
           settings.Columns.AddBand(
                Sub(band)
                    band.Caption = "Área Geografica"
                    band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    band.Columns.Add("desc_departamento", "Departamento").Settings.HeaderFilterMode = HeaderFilterMode.CheckedList
                    band.Columns.Add("desc_municipio", "Municipio").Settings.HeaderFilterMode = HeaderFilterMode.CheckedList
                    band.Columns.Add("desc_aldea", "Aldea").Settings.HeaderFilterMode = HeaderFilterMode.CheckedList
                    band.Columns.Add("desc_caserio", "Caserío").Settings.HeaderFilterMode = HeaderFilterMode.CheckedList
                End Sub)
           settings.Columns.AddBand(
               Sub(band)
                   band.Caption = "Hogar"
                   band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   band.Columns.Add("cod_hogar", "Código Hogar").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                   band.Columns.Add("titular", "Titular").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
               End Sub)
           settings.Columns.AddBand(
               Sub(band)
                   band.Caption = "Persona"
                   band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   band.Columns.Add("identidad", "Identidad").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
                   band.Columns.Add("nombre", "Nombre").Settings.AllowHeaderFilter = DevExpress.Utils.DefaultBoolean.False
               End Sub)
           
       End Sub).Bind(Model).GetHtml()%>