<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    
    Html.DevExpress().GridView(Sub(settings)
                                   
                                   settings.Name = "Periodo_Actualizacion"
                                   settings.Caption = "Periodo de Actualización"
                                   settings.CallbackRouteValues = New With {Key .Controller = "Incorporaciones", Key .Action = "Periodo_Actualizacion_Aperturado"}
                                   settings.SettingsPager.PageSize = 10
                                   settings.KeyFieldName = "cod_actualizacion_periodo"
                                   settings.CommandColumn.Visible = True
                                   settings.CommandColumn.ShowSelectCheckbox = True
                                   settings.CommandColumn.Caption = "Seleccionar"
                                   'settings.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['pago'] = cbxPagos.GetValue(); }"
                                   settings.ClientSideEvents.SelectionChanged = "OnSelectionChanged"
                                   settings.Width = 650
           
                                   settings.EnableCallbackAnimation = True
                                   settings.EnablePagingCallbackAnimation = True
                                   settings.EnableCallbackCompression = True
           
                                   settings.Columns.Add("periodo_actualizacion_nombre", "Periodo de Actualizacion")
                                   settings.Columns.Add("periodo_fch_apertura", "Fecha de la Apertura")
                                   settings.Columns.Add("EstadoD", "Estado del Periodo")
                               End Sub).Bind(Model).GetHtml()
   
%>