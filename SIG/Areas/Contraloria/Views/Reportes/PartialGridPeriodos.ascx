<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress().GridView(
        Sub(settings)
            settings.Name = "gvPeriodos"
            settings.CallbackRouteValues = New With {Key .Controller = "Reportes", Key .Action = "PartialGridPeriodos"}
            
            settings.SettingsPager.Position = PagerPosition.Bottom
            settings.SettingsPager.FirstPageButton.Visible = True
            settings.SettingsPager.LastPageButton.Visible = True
            settings.SettingsPager.PageSizeItemSettings.Visible = True
            settings.SettingsPager.PageSizeItemSettings.Items = New String() {"10", "20", "50"}
            'settings.Height = Unit.Percentage(100)
            'settings.Height = Unit.Pixel(300)
            
            settings.CommandColumn.Visible = True
            settings.CommandColumn.ShowSelectCheckbox = True
            settings.CommandColumn.Caption = "Slc"
            settings.SettingsBehavior.AllowSelectSingleRowOnly = True
            
            'settings.ClientSideEvents.SelectionChanged = "OnSelectionChanged"
            'settings.ClientSideEvents.BeginCallback = "OnBeginCallback"
            'settings.KeyFieldName = "cod_periodo"
            settings.KeyFieldName = "pag_codigo"
            
            settings.Caption = "PERIODOS"
            settings.Columns.AddBand(
                Sub(band)
                    band.Caption = "Periodo"
                    band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    band.Columns.Add("pag_codigo", "Nro")
                    band.Columns.Add("nombrePeriodo", "Nombre Periodo")
                End Sub)
            settings.Columns.AddBand(
                Sub(band)
                    band.Caption = "Programado"
                    band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    band.Columns.Add("hogProgr", "Hogares")
                    band.Columns.Add("montoProg", "Monto")
                End Sub)
            settings.Columns.AddBand(
                Sub(band)
                    band.Caption = "Fechas Pago"
                    band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    band.Columns.Add("primeraFecha", "Primer Pago")
                    band.Columns.Add("ultimaFecha", "Ultimo Pago")
                End Sub)
            
        End Sub).Bind(Model).Render()
    %>