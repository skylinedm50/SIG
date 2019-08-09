<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    If Not ViewData("nombre") Is Nothing Then
        ViewContext.Writer.Write("<iframe id='pdfViewer' runat='server' src='" + ResolveUrl("~/Areas/Contraloria/Content/Uploaded/Archivos/") + ViewData("nombre") + "' height='792px' width='612px'></iframe>")
    Else
        ViewContext.Writer.Write("<iframe id='pdfViewer' runat='server' src='' height='792px' width='612px'></iframe>")
    End If

    %>
