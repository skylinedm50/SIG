Imports DevExpress.Web.Mvc
Imports DevExpress.Utils
Imports DevExpress.Web.ASPxButton

Namespace SIG.Areas.Seguridad.Controllers

    Public Class UsuariosController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Seguridad/Usuarios
        Dim login As New Global.SIG.Cl_Login
        Dim Usuarios As New Global.SIG.SIG.Areas.Seguridad.Models.Usuarios

        Function ViewNuevoUsuario() As ActionResult
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

        Function ViewReportesUsuarios() As ActionResult
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
        Function ViewAdmonUsuarios() As ActionResult
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
        Function ViewDepartamentos() As ActionResult
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

        Function PartialViewGridUsuarios() As ActionResult
            Return PartialView(Usuarios.Fnc_obtener_Usuarios)
        End Function

        Function Pgv_Admon_Usuarios() As ActionResult
            Return PartialView(Usuarios.Fnc_obtener_Usuarios)
        End Function

        <HttpPost>
        Function obtener_info_usuario(ByVal cod_usuario As String)
            Return Usuarios.fnc_obtener_info_usuario(cod_usuario)
        End Function

        <HttpPost>
        Function guardar_departamento(ByVal desc_unidad As String, ByVal estado As String)
            Usuarios.Fnc_guardar_departamento(desc_unidad, estado)
            Return 0
        End Function

        <HttpPost>
        Function obtenerInfoPersona(ByVal identidad As String)
            Return Usuarios.Fnc_obtener_info_persona(identidad)
        End Function

        <HttpPost>
        Function actualizar_usuario(ByVal identidad As String, ByVal estado As String, ByVal nombre1 As String, ByVal nombre2 As String, ByVal apellido1 As String,
                                ByVal apellido2 As String, ByVal telefono As String, ByVal correo As String, ByVal codUsuario As String, ByVal jefe As String, ByVal id_unidad As String)
            Return Usuarios.Fnc_actualizar_usuario(identidad, estado, nombre1, nombre2, apellido1, apellido2, telefono, correo, codUsuario, jefe, id_unidad)
        End Function

        <HttpPost>
        Function obtenerRolesPersona(ByVal identidad As String)
            Return Usuarios.Fnc_obtener_roles_persona(identidad)
        End Function

        <HttpPost>
        Function RegistrarUsuario(ByVal identidad As String, ByVal nombre1 As String, ByVal nombre2 As String, ByVal apellido1 As String,
                                ByVal apellido2 As String, ByVal telefono As String, ByVal correo As String, ByVal nroCritico As String, ByVal roles() As String,
                                  ByVal jefe As String, ByVal id_unidad As String)

            Return Usuarios.Fnc_RegistrarUsuario(identidad, nombre1, nombre2, apellido1, apellido2, telefono, correo, nroCritico, roles, jefe, id_unidad)

        End Function

        Function PVGPermisosNuevoUsuario()
            Return PartialView(Usuarios.Fnc_lista_roles())
        End Function

        Function PVGDepartamentos() As ActionResult
            Return PartialView(Usuarios.Fnc_lista_departamentos())
        End Function

        Function PartialViewCbProyecto()
            Return PartialView(Usuarios.Fnc_lista_departamentos)
        End Function

        <HttpPost>
        Function actualizar_departamento(ByVal id_unidad As String, ByVal desc_unidad As String, ByVal estado As String)
            Return Usuarios.fnc_actualizar_depto(id_unidad, desc_unidad, estado)
        End Function

        <HttpPost>
        Function Obtener_depto(ByVal id_unidad As String)
            Return Usuarios.fnc_obtener_depto(id_unidad)
        End Function

        Function EliminarRolporUsuario(ByVal id_rol As String, ByVal cod_usuario As String)
            Usuarios.Fnc_EliminarRolporUsuario(id_rol, cod_usuario)
            Return 0
        End Function

        Function AgregarRolporUsuario(ByVal id_rol As String, ByVal cod_usuario As String)
            Usuarios.Fnc_AgregarRolporUsuario(id_rol, cod_usuario)
            Return 0
        End Function

        Function PVGPermisosEditarUsuario(ByVal cod_usuario As String) As ActionResult
            ViewData("cod_usuario") = cod_usuario
            Return PartialView(Usuarios.Fnc_lista_roles_por_usuario(cod_usuario))
        End Function

        <HttpPost>
        Function Obtener_lista_rol_por_Usuario(ByVal cod_usuario As String)
            Return Usuarios.Fnc_lista_roles_por_usuario(cod_usuario)
        End Function

        Function CambiarContrasena(ByVal cod_usuario As String)
            Usuarios.Fnc_Cambiar_contrasena(cod_usuario)
            Return 0
        End Function

        Function RecordarContrasena(ByVal cod_usuario As String)
            Return Usuarios.Fnc_Recordar_Contrasena(cod_usuario)
        End Function

        Function ViewCambioContraseña() As ActionResult

            If login.Fnc_loggeado() IsNot Nothing Then

                Dim table As DataTable = Usuarios.fnc_obtener_info_usuario2(Session("usuario"))
                ViewData("identidad") = table.Rows(0).Item("ide_usr_persona")
                ViewData("nombre") = table.Rows(0).Item("nombre")
                ViewData("telefono") = table.Rows(0).Item("num_tel_usr_persona")
                ViewData("correo") = table.Rows(0).Item("email_usr_persona")
                ViewData("usuario") = table.Rows(0).Item("nom_usuario")
                ViewData("critico") = table.Rows(0).Item("cod_critico_usuario")

                Return View()
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function PGVUsuarioGenerado() As ActionResult
            Return PartialView(Usuarios.Fnc_obtener_Usuarios())
        End Function

        Function Exportar_Grid_Usuarios()
            Dim exportar As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")
            Dim dt As DataTable = Usuarios.Fnc_obtener_Usuarios()
            Dim settings As GridViewSettings = exportPvgUsuarios.ExportPivotGridSettings
            If exportar = "Excel" Then
                Return GridViewExtension.ExportToXls(settings, dt, "Repote_UsuariosSIG")
            ElseIf exportar = "Pdf" Then
                Return GridViewExtension.ExportToPdf(settings, dt, "Repote_UsuariosSIG")
            End If

            Return 0
        End Function

        Public NotInheritable Class exportPvgUsuarios
            Private Shared exportSettings As GridViewSettings
            Private Sub New()
            End Sub

            Public Shared ReadOnly Property ExportPivotGridSettings() As GridViewSettings

                Get
                    exportSettings = CreateExportPvgUsuario()
                    Return exportSettings
                End Get

            End Property

            Private Shared Function CreateExportPvgUsuario() As GridViewSettings
                Dim settings As New GridViewSettings()
                settings.Name = "GdvUsuarios"
                settings.Width = Unit.Percentage(100)

                settings.KeyFieldName = "cod_usuario"
                settings.SettingsBehavior.AllowFocusedRow = True
                settings.CallbackRouteValues = New With {Key .Controller = "Usuarios", Key .Action = "PartialViewGridUsuarios"}
                settings.SettingsBehavior.EnableRowHotTrack = True

                settings.Settings.ShowFilterRow = True
                settings.Settings.ShowFilterRowMenu = True
                settings.SettingsPager.Position = PagerPosition.Bottom
                settings.SettingsPager.FirstPageButton.Visible = True
                settings.SettingsPager.LastPageButton.Visible = True
                settings.SettingsPager.PageSizeItemSettings.Visible = True
                settings.SettingsPager.PageSizeItemSettings.Items = New String() {"10", "20", "50"}
                settings.SettingsExport.LeftMargin = 0
                settings.SettingsExport.RightMargin = 0

                settings.Columns.Add("nom_usuario", " Usuario").ExportWidth = 120
                settings.Columns.Add("nombre_completo", "Nombre Completo").ExportWidth = 140
                settings.Columns.Add("ide_usr_persona", "Identidad")
                settings.Columns.Add("num_tel_usr_persona", "Teléfono")
                settings.Columns.Add("email_usr_persona", "E-mail").ExportWidth = 150
                settings.Columns.Add(Sub(col)
                                         col.Caption = "Habilitado"
                                         col.FieldName = "Estado"
                                         col.EditorProperties().ComboBox(Sub(cb)
                                                                             cb.Items.Add("Si")
                                                                             cb.Items.Add("No")
                                                                         End Sub)
                                     End Sub)
                settings.Columns.Add(Sub(col)
                                         col.Caption = "Fecha de Registro"
                                         col.FieldName = "fecha_registro"
                                         col.ExportWidth = 70
                                         col.EditorProperties().DateEdit(Sub(dt)
                                                                         End Sub)
                                     End Sub)
                settings.Columns.Add(Sub(col)
                                         col.Caption = "Fecha de Baja"
                                         col.FieldName = "fecha_baja"
                                         col.ExportWidth = 70
                                         col.EditorProperties().DateEdit(Sub(dt)
                                                                         End Sub)
                                     End Sub)
                Return settings
            End Function
        End Class

        <HttpPost>
        Function actualizarContrasena(ByVal pass As String)

            Return Usuarios.Fnc_actualizar_contrasena(Session("usuario"), pass)

        End Function

    End Class

End Namespace