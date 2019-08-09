<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress().Memo(
       Sub(settings)
           settings.Name = "txtMemoBancarizacion"

           If ViewData("num") IsNot Nothing Then
               settings.Text = "Datos Generales del Archivo" + vbCrLf &
               "Nombre: " + ViewData("nombre") + vbCrLf &
               "Total de registro: " + ViewData("total_registros") + vbCrLf &
               "Pago al que pertenece: " + ViewData("pago") + vbCrLf &
               "Banco al que pertenece: " + ViewData("banco") + vbCrLf &
               "Tipo: " + ViewData("tipo") + vbCrLf &
               "Estado: " + ViewData("estado") + vbCrLf &
               "Fecha de ingreso: " + ViewData("fecha") + vbCrLf &
               "" + vbCrLf &
               "Contador de estado de los registros:"

               For i = 1 To ViewData("num")
                   settings.Text += vbCr + "    " + ViewData(i)
               Next
           End If

           settings.Properties.Columns = 60
           settings.ReadOnly = True
           settings.Properties.Rows = 25
       End Sub).Render()

    'If ViewData("tipo") = "CONFIMACIÓN DE DEPOSITOS A TODAS LAS CUENTAS" Or ViewData("tipo") = "CIERRE DE DEPOSITO A CUENTAS" Then
    '    ViewContext.Writer.Write("<script type='text/javascript'>")
    '    ViewContext.Writer.Write("btnConsolidado.SetEnabled(true)")
    '    ViewContext.Writer.Write("</script>")
    'Else
    '    ViewContext.Writer.Write("<script type='text/javascript'>")
    '    ViewContext.Writer.Write("btnConsolidado.SetEnabled(false)")
    '    ViewContext.Writer.Write("</script>")
    'End If
    %>

