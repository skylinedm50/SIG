Imports DevExpress.Web.Mvc

Namespace SIG.Areas.Planilla.Controllers

    Public Class ProgramacionPagoController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Planilla/ProgramacionPago
        Dim objLogin As New Cl_Login()
        Dim programacion As Models.cl_programacion_pago = New SIG.Areas.Planilla.Models.cl_programacion_pago

        Function ViewBancarizacion() As ActionResult
            Return View()
        End Function

        Function pv_gdvPeriodoApertura(ByVal pago As String) As ActionResult
            ViewData("pago") = pago
            Return PartialView(programacion.fnc_obtener_pagadores_fechas(pago))
        End Function

        Function actualizarPeriodoApertura(ByVal pago As String) As ActionResult

            Dim codigo As String = GridViewExtension.GetEditValue(Of String)("Codigo_Pagador")
            Dim inicio As String = GridViewExtension.GetEditValue(Of String)("fecha_inicio")
            Dim fin As String = GridViewExtension.GetEditValue(Of String)("fecha_fin")

            programacion.fnc_insertar_actualizar_periodo_apertuar(pago, codigo, inicio, fin)

            Return PartialView("pv_gdvPeriodoApertura", programacion.fnc_obtener_pagadores_fechas(pago))

        End Function

    End Class

End Namespace

