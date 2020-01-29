Namespace SIG.Areas.Seguridad.Controllers
    Public Class ModulosController
        Inherits System.Web.Mvc.Controller
        Dim login As New Global.SIG.Cl_Login
        Dim Modulos As New Global.SIG.SIG.Areas.Seguridad.Models.Modulos

        '
        ' GET: /Seguridad/Modulos

        Function ViewAdministraciondeModulos() As ActionResult
            'Return View()

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(6) Then
                    'login.Fnc_MenuModulo()
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View()
                    Else
                        Return Redirect("/Seguridad/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function PartialViewGridModulos() As ActionResult
            Return PartialView(Modulos.fnc_lista_modulos())
        End Function


        ' Function PartialViewGridOpciones()
        'Return PartialView(Modulos.fnc_lista_opciones(1))
        ' End Function

        Function PartialViewGridOpciones(ByVal id_modulos As String) As ActionResult

            ViewData("id_modulos") = id_modulos
            Return PartialView(Modulos.fnc_lista_opciones(id_modulos))
        End Function

        Function PartialViewGridPantallas(ByVal id_opcion As String) As ActionResult

            If id_opcion IsNot Nothing Then
                ViewData("id_opcion") = id_opcion
                Return PartialView(Modulos.fnc_lista_pantallas(id_opcion))
            End If

            Return PartialView()

        End Function

        <HttpPost>
        Function Obtener_info_modulo(ByVal id_modulo As String)
            Return Modulos.fnc_obtener_info_modulo(id_modulo)
        End Function

        <HttpPost>
        Function Obtener_info_opcion(ByVal id_opcion As String)
            Return Modulos.fnc_obtener_info_opcion(id_opcion)
        End Function

        <HttpPost>
        Function Obtener_info_pantalla(ByVal id_pantalla As String)
            Return Modulos.fnc_obtener_info_pantalla(id_pantalla)
        End Function

        <HttpPost>
        Function Agregar_modulo(ByVal nom_modulo As String, ByVal estado_modulo As String, ByVal icono As String)
            Modulos.fnc_agregar_modulo(nom_modulo, estado_modulo, icono)
            Return 0
        End Function

        <HttpPost>
        Function Agregar_opcion(ByVal desc_opcion As String, ByVal estado_opcion As String, ByVal id_modulo As String)
            Modulos.fnc_agregar_opcion(desc_opcion, estado_opcion, id_modulo)
            Return 0
        End Function

        <HttpPost>
        Function Agregar_pantalla(ByVal desc_actividad As String, ByVal id_opcion As String, ByVal estado_actividad As String, ByVal url As String)
            Modulos.fnc_agregar_pantalla(desc_actividad, id_opcion, estado_actividad, url)
            Return 0
        End Function

        <HttpPost>
        Function Actualizar_modulo(ByVal nom_modulo As String, ByVal estado_modulo As String, ByVal icono As String, ByVal id_modulo As String)
            Modulos.fnc_actualizar_modulo(nom_modulo, estado_modulo, icono, id_modulo)
            Return 0
        End Function

        <HttpPost>
        Function Actualizar_opcion(ByVal desc_opcion As String, ByVal estado_opcion As String, ByVal id_modulo As String, ByVal id_opcion As String)
            Modulos.fnc_actualizar_opcion(desc_opcion, estado_opcion, id_modulo, id_opcion)
            Return 0
        End Function

        <HttpPost>
        Function Actualizar_pantalla(ByVal desc_actividad As String, ByVal id_opcion As String, ByVal estado_actividad As String, ByVal url As String, ByVal id_actividad As String)
            Modulos.fnc_actualizar_pantalla(desc_actividad, id_opcion, estado_actividad, url, id_actividad)
            Return 0
        End Function
    End Class
End Namespace