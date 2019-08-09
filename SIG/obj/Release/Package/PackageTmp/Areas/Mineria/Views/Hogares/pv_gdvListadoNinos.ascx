<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress.GridView(
        Sub(gdv)
            gdv.Name = "gdvNiños"
            gdv.Caption = "Información de los Niños"
                                           
            gdv.KeyFieldName = "cod_persona"
            gdv.SettingsDetail.AllowOnlyOneMasterRowExpanded = True
            gdv.SettingsDetail.ShowDetailRow = True
            gdv.SettingsDetail.ExportMode = GridViewDetailExportMode.Expanded
                                           
            gdv.Columns.AddBand(
                Sub(band)
                    band.Caption = "BENEFICIARIO"
                    band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    band.Columns.Add("identidad", "Identidad")
                    band.Columns.Add("nombre_persona", "Nombre")
                End Sub)
            gdv.Columns.AddBand(
                Sub(band)
                    band.Caption = "CORREPONSABILIDAD"
                    band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    band.Columns.Add("desc_nivel_elegibilidad", "Ciclo")
                    band.Columns.Add("desc_corresponsabilidad", "Correponsabilidad")
                    band.Columns.Add("estado_corresponsabilidad", "Estado")
                End Sub)
                                           
            gdv.CallbackRouteValues = New With {Key .Controller = "Hogares", Key .Action = "pv_gdvListadoNinos", Key .pago = ViewData("pago"), Key .hogar = ViewData("hogar")}
            gdv.SetDetailRowTemplateContent(
                Sub(row)
                    Html.RenderAction("pv_DetalleCorreponsabalidadNino", New With {Key .pago = ViewData("pago"), Key .cod_persona = DataBinder.Eval(row.DataItem, "cod_persona")})
                End Sub)
        End Sub).Bind(Model).GetHtml()%>