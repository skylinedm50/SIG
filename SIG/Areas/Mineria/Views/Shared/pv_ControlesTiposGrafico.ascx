<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().FormLayout(
       Sub(frmLayoutExport)
           frmLayoutExport.Name = "floTipoChart"
           frmLayoutExport.ColCount = 2
           frmLayoutExport.Items.Add(
               Sub(item)
                   item.Caption = "Tipo de Grafico"
                   item.SetNestedContent(
                       Sub()
                           Html.DevExpress().ComboBox(
                               Sub(cbx)
                                   cbx.Name = "cbxTipoGrafico"
                                   cbx.Width = 150
                                   cbx.Properties.NullText = "Seleccione un tipo de gráfico"
                                   cbx.Properties.Items.Add("Bar")
                                   cbx.Properties.Items.Add("StackedBar")
                                   cbx.Properties.Items.Add("FullStackedBar")
                                   cbx.Properties.Items.Add("SideBySideStackedBar")
                                   cbx.Properties.Items.Add("SideBySideFullStackedBar")
                                   cbx.Properties.Items.Add("Pie")
                                   cbx.Properties.Items.Add("Doughnut")
                                   'cbx.Properties.Items.Add("NestedDoughnut")
                                   cbx.Properties.Items.Add("Funnel")
                                   cbx.Properties.Items.Add("Point")
                                   cbx.Properties.Items.Add("Bubble")
                                   cbx.Properties.Items.Add("Line")
                                   cbx.Properties.Items.Add("StackedLine")
                                   cbx.Properties.Items.Add("FullStackedLine")
                                   cbx.Properties.Items.Add("StepLine")
                                   cbx.Properties.Items.Add("Spline")
                                   cbx.Properties.Items.Add("ScatterLine")
                                   'cbx.Properties.Items.Add("SwiftPlot")
                                   cbx.Properties.Items.Add("Area")
                                   cbx.Properties.Items.Add("StepArea")
                                   cbx.Properties.Items.Add("SplineArea")
                                   cbx.Properties.Items.Add("StackedArea")
                                   'cbx.Properties.Items.Add("StackedStepArea")
                                   cbx.Properties.Items.Add("StackedSplineArea")
                                   cbx.Properties.Items.Add("FullStackedArea")
                                   cbx.Properties.Items.Add("FullStackedSplineArea")
                                   'cbx.Properties.Items.Add("FullStackedStepArea")
                                   cbx.Properties.Items.Add("RangeArea")
                                   cbx.Properties.Items.Add("Stock")
                                   cbx.Properties.Items.Add("CandleStick")
                                   cbx.Properties.Items.Add("SideBySideRangeBar")
                                   cbx.Properties.Items.Add("SideBySideGantt")
                                   cbx.Properties.Items.Add("Gantt")
                                   'cbx.Properties.Items.Add("PolarPoint")
                                   'cbx.Properties.Items.Add("PolarLine")
                                   'cbx.Properties.Items.Add("ScatterPolarLine")
                                   'cbx.Properties.Items.Add("PolarArea")
                                   'cbx.Properties.Items.Add("PolarRangeArea")
                                   cbx.Properties.Items.Add("RadarPoint")
                                   cbx.Properties.Items.Add("RadarLine")
                                   'cbx.Properties.Items.Add("ScatterRadarLine")
                                   cbx.Properties.Items.Add("RadarArea")
                                   'cbx.Properties.Items.Add("RadarRangeArea")
                                   cbx.Properties.Items.Add("Bar3D")
                                   cbx.Properties.Items.Add("StackedBar3D")
                                   cbx.Properties.Items.Add("FullStackedBar3D")
                                   cbx.Properties.Items.Add("ManhattanBar")
                                   cbx.Properties.Items.Add("SideBySideStackedBar3D")
                                   cbx.Properties.Items.Add("SideBySideFullStackedBar3D")
                                   cbx.Properties.Items.Add("Pie3D")
                                   cbx.Properties.Items.Add("Doughnut3D")
                                   cbx.Properties.Items.Add("Funnel3D")
                                   cbx.Properties.Items.Add("Line3D")
                                   cbx.Properties.Items.Add("StackedLine3D")
                                   cbx.Properties.Items.Add("FullStackedLine3D")
                                   cbx.Properties.Items.Add("StepLine3D")
                                   cbx.Properties.Items.Add("Area3D")
                                   cbx.Properties.Items.Add("StackedArea3D")
                                   cbx.Properties.Items.Add("FullStackedArea3D")
                                   cbx.Properties.Items.Add("StepArea3D")
                                   cbx.Properties.Items.Add("Spline3D")
                                   cbx.Properties.Items.Add("SplineArea3D")
                                   cbx.Properties.Items.Add("StackedSplineArea3D")
                                   cbx.Properties.Items.Add("FullStackedSplineArea3D")
                                   cbx.Properties.Items.Add("RangeArea3D")
                               
                               End Sub).GetHtml()
                       End Sub)
               End Sub)
           frmLayoutExport.Items.Add(
               Sub(item)
                   item.ShowCaption = DefaultBoolean.False
                   item.SetNestedContent(
                       Sub()
                           Html.DevExpress().Button(
                               Sub(btnConsultar)
                                   btnConsultar.Name = "btnAplicar"
                                   btnConsultar.Text = "Aplicar"
                                   btnConsultar.ClientSideEvents.Click = "function(s, e) {chart.PerformCallback();}"
                               End Sub).GetHtml()
                       End Sub)
               End Sub)
       End Sub).GetHtml()%>