<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress.Memo(Sub(configuracionMemo)
                             configuracionMemo.Name = "AspxMemoDescripcion"
                             configuracionMemo.Width = 300
                             configuracionMemo.Height = 100
                             configuracionMemo.Properties.NullText = "Ingrese una descripción en los casos que se requiera."
                         End Sub).Render()
%>