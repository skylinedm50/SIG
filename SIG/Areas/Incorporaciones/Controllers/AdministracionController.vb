Imports System.Web.Script.Serialization
Imports DevExpress.XtraCharts.Web
Imports DevExpress.XtraCharts
Imports System.Web.SessionState
Imports System.IO
Imports System.Web.HttpContext

Namespace SIG.Areas.Incorporaciones.Controllers

    Public Class AdministracionController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Administracion

#Region "Variables"

        Dim Obj_info_data As New DataTable()
        Dim obj_hogares_beneficiarioas As New SIG.Areas.Incorporaciones.Models.Cl_Hogar_Beneficiario()
        Dim serializador As New JavaScriptSerializer()
        Dim Dictionary_Datos As Dictionary(Of String, Object)
        Dim lista As New List(Of Dictionary(Of String, Object))
        Dim obj_usuarios As New SIG.Areas.Incorporaciones.Models.Cl_Usuarios()
        Dim Usuario As New Global.SIG.Cl_Login
#End Region



#Region "Impresion nucleos del hogar"

        <HttpGet>
        Function ImpresionNucleo()
            If Usuario.Fnc_loggeado() IsNot Nothing Then
                If Usuario.fnc_validarModulo(1) Then
                    If Usuario.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View("Impresion_Nucleo/Imprimir_Nucleos_Hogar")
                    Else
                        Return Redirect("/Incorporaciones/Inicio")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Function FormLayout_HogaresNucleos(hog_hogar As Integer, titular As String)

            Dim obj_model = obj_hogares_beneficiarioas.fnc_buscar_nucleo(hog_hogar, titular, 1)

            Try
                If (obj_model.Rows(0).item(16) = 1 Or obj_model.Rows(0).item(16) = 0) Then

                    ViewData("Hogar1") = obj_model
                    ViewData("Hogar2") = Nothing
                ElseIf (obj_model.Rows(0).item(16) > 1) Then

                    ViewData("Hogar1") = obj_model
                    obj_model = obj_hogares_beneficiarioas.fnc_buscar_nucleo(hog_hogar, titular, 2)
                    ViewData("Hogar2") = obj_model

                End If
            Catch ex As Exception
                Return Nothing
            End Try

            Return PartialView("Impresion_Nucleo/FormLayout_HogaresNucleos")
        End Function

        Function CallbackInformacionHogarImprimir(Hogar As Integer)
            Return PartialView("Impresion_Nucleo/informacionPersonasHogar", obj_hogares_beneficiarioas.fnc_buscar_nucleo(Hogar, "", 1))
        End Function

        Function informacion_hogar(Hogar As DataTable, Nombre As String)
            ViewData("nombre") = Hogar

            Return PartialView("Impresion_Nucleo/informacion_hogar", Hogar)
        End Function

        Function informacionPersonasHogar(Hogar As DataTable)
            ViewData("personas_hogar_v") = Hogar

            Return PartialView("Impresion_Nucleo/informacionPersonasHogar", Hogar)
        End Function

        Function NucleoHogarJSON(hogar As Integer)

            Dim Tabla As New DataTable()
            Tabla = obj_hogares_beneficiarioas.fnc_buscar_nucleo(hogar, "", 1)
            Try
                For Each Fila As DataRow In Tabla.Rows
                    Dictionary_Datos = New Dictionary(Of String, Object)
                    For Each Columna As DataColumn In Tabla.Columns
                        Dictionary_Datos.Add(Columna.ColumnName, Fila(Columna))
                    Next
                    lista.Add(Dictionary_Datos)
                Next
            Catch ex As Exception
                Return ex.HResult
            End Try

            Return serializador.Serialize(lista)
        End Function

#End Region


#Region "SupenderHogar"

        <HttpGet>
        Function suspender_hogar()
            If Usuario.Fnc_loggeado() IsNot Nothing Then
                If Usuario.fnc_validarModulo(1) Then
                    If Usuario.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View("Supension Hogares/suspender_hogar")
                    Else
                        Return Redirect("/Incorporaciones/Inicio")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function


        Function buscarInformacionHogar(identidad_titular As String, int_hogar As Integer)
            Try
                System.Web.HttpContext.Current.Session.Item("error") = ""

                Me.obj_hogares_beneficiarioas.Fnc_InformacionHogar(identidad_titular, int_hogar)
                If (System.Web.HttpContext.Current.Session("error") <> "") Then
                    Return System.Web.HttpContext.Current.Session("error")
                End If
                Me.Obj_info_data = Me.obj_hogares_beneficiarioas.Fnc_InformacionHogar(identidad_titular, int_hogar)

                For Each Fila As DataRow In Obj_info_data.Rows
                    Dictionary_Datos = New Dictionary(Of String, Object)
                    For Each Columna As DataColumn In Obj_info_data.Columns
                        Dictionary_Datos.Add(Columna.ColumnName, Fila(Columna))
                    Next
                    lista.Add(Dictionary_Datos)
                Next
            Catch ex As Exception
                Return ex.HResult
            End Try

            Return serializador.Serialize(lista)
        End Function


        <HttpPost>
        Function Realizar_Suspension(int_hogar As Integer, int_estado As Integer, str_descripcion As String)
            Return Me.obj_hogares_beneficiarioas.Fnc_SuspensionActivacionHogar(int_hogar, int_estado, str_descripcion)
        End Function


#End Region





    End Class
End Namespace
