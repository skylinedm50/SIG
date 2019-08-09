<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<% 

    Html.DevExpress.DocumentViewer(Sub(configuracionDocumentViewer As DocumentViewerSettings)
                                       configuracionDocumentViewer.Name = "AspxDocumentViewerBusqCorresBenef"
                                       configuracionDocumentViewer.Report = Model
                                       configuracionDocumentViewer.Width = 840
                                       configuracionDocumentViewer.Height = 900
                                       configuracionDocumentViewer.CallbackRouteValues = New With {Key .Controller = "Reportes",
                                                                                                   Key .Action = "AspxDocumentViewerBusqCorresBenef",
                                                                                                   Key .intCodBenef = ViewData("intCodBenef"),
                                                                                                   Key .intTipoOperacion = ViewData("intTipoOperacion")}
                                       configuracionDocumentViewer.ExportRouteValues = New With {Key .Controller = "Reportes",
                                                                                                Key .Action = "AspxDocumentViewerBusqCorresBenef",
                                                                                                Key .intCodBenef = ViewData("intCodBenef"),
                                                                                                Key .intTipoOperacion = 1}
                                       configuracionDocumentViewer.ClientSideEvents.BeforeExportRequest = "function(s, e){  e.customArgs['intCodBenef'] = objBusqCorresBenef.Session.intCodBenefBusCorres; }"
                                       configuracionDocumentViewer.ClientSideEvents.BeginCallback = "function(s, e){  e.customArgs['intCodBenef'] = objBusqCorresBenef.Session.intCodBenefBusCorres; }"
                                       configuracionDocumentViewer.ClientSideEvents.CallbackError = "function(s, e){ AspxPopupControlReportCorresBenef.Hidden(); AspxLabelError.SetText('*ERROR, imposible conectarse con el servidor favor comunicarse con el administrador del sistema.'); }"
                                   End Sub).Render()
%>