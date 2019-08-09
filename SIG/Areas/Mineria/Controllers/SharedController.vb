Namespace SIG.Areas.Mineria.Controllers

    Public Class SharedController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Mineria/Shared

        Dim share As SIG.Areas.Mineria.Models.Cl_Shared = New SIG.Areas.Mineria.Models.Cl_Shared

        'Function Index() As ActionResult
        '    Return View()
        'End Function

#Region "Funciones de área geográfica"

        Function pv_cbxDepartamentos() As ActionResult
            Return PartialView("Area_Geografica/pv_cbxDepartamentos", share.Fnc_obtener_departamentos)
        End Function

        Function pv_cbxMunicipios() As ActionResult
            Dim dpto As String = Request.Params("dpto")
            Return PartialView("Area_Geografica/pv_cbxMunicipios", share.Fnc_obtener_municipios(dpto))
        End Function

        Function pv_cbxAldeas() As ActionResult
            Dim muni As String = Request.Params("muni")
            Return PartialView("Area_Geografica/pv_cbxAldeas", share.Fnc_obtener_aldeas(muni))
        End Function

        Function pv_ControlesAreaGeografica() As ActionResult
            Return PartialView("Area_Geografica/pv_ControlesAreaGeografica")
        End Function

#End Region

#Region "Funciones para información de los pagos"

        Function pv_cbxAnos() As ActionResult
            Return PartialView("Pagos/pv_cbxAnos", share.Fnc_obtener_anos())
        End Function

        Function pv_cbxPagosAno() As ActionResult
            Dim ano As String = Request.Params("ano")
            Return PartialView("Pagos/pv_cbxPagosAno", share.Fnc_obtener_pagos_ano(ano))
        End Function

        Function pv_ControlesPagos() As ActionResult
            Return PartialView("Pagos/pv_ControlesPagos")
        End Function

        Function pv_cbxGridPagos() As ActionResult
            Return PartialView("Pagos/pv_cbxGridPagos", share.Fnc_obtener_tabla_pagos())
        End Function

#End Region

#Region "Funciones para información de las entidades pagadoras"

        Function pv_cbxEntidadesPagadoras() As ActionResult
            Return PartialView("Entidades_Pagadoras/pv_cbxEntidadesPagadoras", share.Fnc_obtener_pagadores())
        End Function

        Function pv_cbxAgencias() As ActionResult
            Dim entidad As String = Request.Params("entidad")
            Return PartialView("Entidades_Pagadoras/pv_cbxAgencias", share.Fnc_obtener_agencias(entidad))
        End Function

        Function pv_ControlesEntidadesPagadoras() As ActionResult
            Return PartialView("Entidades_Pagadoras/pv_ControlesEntidadesPagadoras")
        End Function

        Function pv_rblEntidadesPorPago(ByVal pago As String)
            Return PartialView("Entidades_Pagadoras/pv_rblEntidadesPorPago", share.Fnc_obtener_entidades_pagadoras_por_pago(pago))
        End Function

#End Region

#Region "Funciones para información de los fondos"

        Function pv_rblFondosPorPago(ByVal pago As String)
            Return PartialView("Fondos/pv_rblFondosPorPago", share.Fnc_obtener_fondos_por_pago(pago))
        End Function
#End Region

#Region "Funciones para periodos de tiempo"

        'Function pv_cbxAnos() As ActionResult

        'End Function

#End Region

#Region "Otras funciones para vistas parciales"

        Function pv_ControlesExportar() As ActionResult
            Return PartialView()
        End Function

        Function pv_ControlesExportChart() As ActionResult
            Return PartialView()
        End Function

        Function pv_ControlesTiposGrafico() As ActionResult
            Return PartialView()
        End Function

        Function pv_Spinner() As ActionResult
            Return PartialView()
        End Function
#End Region

    End Class
End Namespace