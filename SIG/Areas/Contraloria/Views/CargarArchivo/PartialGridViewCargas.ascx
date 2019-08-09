<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().GridView(
       Sub(settings)
           settings.Name = "gvCargas"
           settings.Caption = "Historial de Cargas y Pre cargas"
           settings.CallbackRouteValues = New With {Key .Controller = "CargarArchivo", Key .Action = "retornarCargas", Key .tipo = ViewData("tipo"), Key .periodo = ViewData("periodo"), Key .banco = ViewData("banco")}
           settings.SettingsPager.PageSize = 10           
           settings.KeyFieldName = "codigo"           
           settings.CommandColumn.Visible = True
           settings.CommandColumn.ShowSelectCheckbox = True
           settings.CommandColumn.Caption = "Slc"
           settings.SettingsBehavior.AllowSelectSingleRowOnly = True                      
           settings.Columns.AddBand(
               Sub(band)
                   band.Caption = "Filtros"
                   band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   band.Columns.Add("Tipo")
                   band.Columns.Add("Periodo")
                   band.Columns.Add("Banco")
               End Sub)
           settings.Columns.AddBand(
               Sub(band)
                   band.Caption = "Archivo"
                   band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   band.Columns.Add("Nombre Archivo")
               End Sub)
           settings.Columns.AddBand(
               Sub(band)
                   band.Caption = "Carga"
                   band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   band.Columns.Add("Nombre Carga")
                   band.Columns.Add("Usuario")
                   band.Columns.Add("Inicio de Carga")
                   band.Columns.Add("Fin de Carga")
               End Sub)           
           settings.Columns.Add("codigo").Visible = False
       End Sub).Bind(Model).GetHtml()%>