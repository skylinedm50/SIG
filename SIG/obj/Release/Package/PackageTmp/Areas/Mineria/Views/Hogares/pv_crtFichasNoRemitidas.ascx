<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().Chart(
       Sub(settings)
           settings.Name = "chart"
           'settings.BorderOptions.Visibility = DefaultBoolean.False
           settings.Width = 1000
           settings.Height = 500
           settings.EnableClientSideAPI = True
           settings.CallbackRouteValues =
               New With {
                    Key .Controller = "Hogares",
                    Key .Action = "pv_crtFichasNoRemitidas",
                    Key .departamento = ViewData("departamento"),
                    Key .municipio = ViewData("municipio"),
                    Key .aldea = ViewData("aldea"),
                    Key .fechas = ViewData("fecha")}
           
           'titulo
           settings.Titles.Add(New ChartTitle() With {.Text = "Fichas No Procesadas Por CENISS"})
           
           
           Dim serie As New Series
           serie = New Series("Fichas", DevExpress.XtraCharts.ViewType.Bar)
           serie.ArgumentDataMember = "area"
           serie.ValueDataMembers(0) = "fichas"
           serie.LabelsVisibility = DefaultBoolean.True
           serie.Label.ResolveOverlappingMode = ResolveOverlappingMode.Default
           
           settings.Series.Add(serie)
           
           Dim diagra As XYDiagram = CType(settings.Diagram, XYDiagram)
           'Dim diagra As New XYDiagram
           diagra.AxisY.Title.Text = "Número de Fichas"
           diagra.AxisY.Title.Visible = True
           diagra.AxisY.Interlaced = True
           diagra.AxisY.VisualRange.Auto = True
           settings.Diagram.Assign(diagra)

       End Sub).Bind(Model).GetHtml()%>