<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().GridView(
        Sub(settings)
            settings.Name = "gvDocumentos"
            settings.Caption = "DOCUMENTOS"
            settings.CallbackRouteValues = New With { _
                Key .Controller = "Escaneo", _
                Key .Action = "PartialGridMaestroLeitz", _
                Key .periodo = ViewData("periodo"), _
                Key .fondo = ViewData("fondo"), _
                Key .dpto = ViewData("dpto"), _
                Key .tipo = ViewData("tipo"), _
                Key .leitz = ViewData("leitz")
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

            settings.CommandColumn.Visible = True
            settings.CommandColumn.Name = "Ver Documento"
            settings.CommandColumn.CustomButtons.Add(New GridViewCommandColumnCustomButton() With {.ID = "btnVerPDF", .Text = "Ver Documento"})
            settings.ClientSideEvents.CustomButtonClick = "btnVerPDFClick"

            settings.Settings.ShowHeaderFilterButton = True
            settings.SettingsPopup.HeaderFilter.Height = 200

            settings.KeyFieldName = "cod_documento"

            settings.Columns.AddBand(
                Sub(band)
                    band.Caption = "Filtros"
                    band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    band.Columns.Add("desc_departamento", "Departamento")
                    band.Columns.Add("nombre_banco", "Banco")
                    band.Columns.Add("nombre_tipo_documento", "Tipo")
                End Sub)
            settings.Columns.AddBand(
                Sub(band)
                    band.Caption = "Documento"
                    band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    band.Columns.Add("nombre_documento", "Nombre")

                    band.Columns.Add("fecha_inicio_documento", "Fecha Inicio")
                    band.Columns.Add("fecha_fin_documento", "Fecha Fin")
                End Sub)

        End Sub).Bind(Model).Render()%>