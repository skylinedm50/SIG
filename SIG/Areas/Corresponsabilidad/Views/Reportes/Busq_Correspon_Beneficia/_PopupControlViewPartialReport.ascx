<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress.PopupControl(Sub(configuracionAspxPopupControl As PopupControlSettings)
                                     configuracionAspxPopupControl.Name = "AspxPopupControlReportCorresBenef"
                                     configuracionAspxPopupControl.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                     configuracionAspxPopupControl.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                     configuracionAspxPopupControl.HeaderText = "Resultado"
                                     configuracionAspxPopupControl.ShowHeader = True
                                     configuracionAspxPopupControl.Modal = True
                                     configuracionAspxPopupControl.CloseAction = CloseAction.CloseButton
                                     configuracionAspxPopupControl.AllowDragging = True
                                     configuracionAspxPopupControl.SetContent(Sub()
                                                                                  Html.RenderAction("AspxDocumentViewerBusqCorresBenef", New With {Key .intCodBenef = 0, Key .intTipoOperacion = 0})
                                                                              End Sub)
                                 End Sub).GetHtml()
%>