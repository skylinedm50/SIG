Namespace SIG.Areas.Mineria.Controllers

    Public Class IndicadoresController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Mineria/Indicadores
        Dim login As New Global.SIG.Cl_Login
        Dim indicadores As SIG.Areas.Mineria.Models.Cl_Indicadores = New SIG.Areas.Mineria.Models.Cl_Indicadores


#Region "Funciones para los indicadores anuales"

        Function v_Indicadores() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)

                    ViewData("hog_pob_extrema") = indicadores.Fnc_obtener_hogares_pobreza_extrema()
                    ViewData("niños_1_6_asistiendo") = indicadores.Fnc_obtener_asistencia_1ro_6to_asistiendo()
                    ViewData("niños_7_9_asistiendo") = indicadores.Fnc_obtener_asistencia_7mo_9no_asistiendo()
                    ViewData("niños_13_15_aprovaron_primaria") = indicadores.Fnc_obtener_13_15_aprovaron_primaria()
                    ViewData("niños_16_18_aprovaron_noveno") = indicadores.Fnc_obtener_16_18_aprovaron_noveno()
                    ViewData("matriculado_6to_aprovaron") = indicadores.Fnc_obtener_matriculados_6to_aprovaron()
                    ViewData("matriculado_9no_aprovaron") = indicadores.Fnc_obtener_matriculados_9no_aprovaron()
                    ViewData("hogares_elegibilidad_actulizada") = indicadores.Fnc_obtener_hogares_informacion_actualizada()
                    ViewData("hogares_reciben_todos_pagos") = indicadores.Fnc_obtener_hogares_reciben_todos_pagos()
                    ViewData("hogares_reciben_todos_pagos_bm") = indicadores.Fnc_obtener_hogares_reciben_todos_pagos_bm()
                    ViewData("centros_salud_reportan_cumplimiento") = indicadores.Fnc_obtener_centros_salud_reportan_cumplimiento()
                    ViewData("hogares_pagados_mecanismos_alternos") = indicadores.Fnc_obtener_hogares_pagados_mecanismos_alternos()
                    ViewData("hogares_pagados_mecanismos_alternos_bm") = indicadores.Fnc_obtener_hogares_pagados_mecanismos_alternos_bm()

                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

#End Region

        

    End Class
End Namespace