Imports DevExpress.Web.Mvc

Namespace SIG.Areas.Planilla.Controllers
    Public Class GeneracionDocumentosController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Planilla/GeneracionDocumentos
        Dim objLogin As New Cl_Login()
        Dim generacion As SIG.Areas.Planilla.Models.cl_generacion_documentos = New SIG.Areas.Planilla.Models.cl_generacion_documentos

        Function ViewGeneracionReceptor() As ActionResult
            Return View()
        End Function

        Function ViewGeneracion() As ActionResult

            If objLogin.Fnc_loggeado() IsNot Nothing Then
                If objLogin.fnc_validarModulo(3) Then
                    objLogin.Fnc_MenuModulo(3)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If
            Return Nothing

        End Function

        Function generarArchivos(ByVal strEsquemas As String) As ActionResult

            If generacion.fnc_validar_esquemas(strEsquemas) Then
                Dim path As String = generacion.fnc_generar_archivos(strEsquemas)
                Session("path") = path
                Return PartialView("pv_fmArchivos", path)
            Else
                Return Nothing
            End If

        End Function

        Function pv_fmArchivos() As ActionResult
            Return PartialView("pv_fmArchivos", Session("path"))
        End Function

        Function downloadFiles() As ActionResult
            Return FileManagerExtension.DownloadFiles("fmArchivos", Session("path"))
        End Function

    End Class
End Namespace