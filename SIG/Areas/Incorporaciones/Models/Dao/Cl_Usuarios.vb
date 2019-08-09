Imports System.Web.HttpContext
Imports System.Web.SessionState.HttpSessionState

Namespace SIG.Areas.Incorporaciones.Models

    Public Class Cl_Usuarios
        Inherits SIG.Areas.Incorporaciones.Models.Cl_Conexion

#Region "variables"
        Dim str_user As String
        Dim str_rol As String
#End Region
        Function Fnc_ListarUsuarios()

            Me.Str_Query = "select cod_user, pri_nom_user +' '+seg_nom_user+' '+pri_apell_user+' '+seg_apell_user 'Nombre'"
            Me.Str_Query += " from t_incor_usuarios"
            Me.Str_Query += " where estado_user = 1"
            Return FncGetTableQuery()

        End Function

        Function Fnc_LevantamientosPorUsuarios(usuarios As Integer)

            Me.Str_Query = " select cod_user_detalle, tipo.desc_tipo_lev,count(*) as levantamiento"
            Me.Str_Query += " from t_lev_detalle_users as usuarios_levantamientos"
            Me.Str_Query += " join t_incor_levantamientos as levantamientos"
            Me.Str_Query += " on levantamientos.cod_levantamiento = usuarios_levantamientos.cod_lev_detalle"
            Me.Str_Query += " join t_incor_tipo_levatamientos as tipo"
            Me.Str_Query += " on tipo.cod_tipo_lev = levantamientos.cod_tipo_lev"
            Me.Str_Query += " where usuarios_levantamientos.cod_user_detalle = {0}"
            Me.Str_Query += " group by cod_user_detalle , tipo.desc_tipo_lev"

            Me.Str_Query = String.Format(Me.Str_Query, usuarios)

            Return FncGetTableQuery()
        End Function

        Function Fnc_LoggOutSession()
            HttpContext.Current.Session.Clear()
            HttpContext.Current.Session.Abandon()
            HttpContext.Current.User = Nothing
        End Function

        Function Fnc_Autenticar(username As String, password As String)
            Me.Str_Query = "select cod_user , cod_rol_user , pass_user"
            Me.Str_Query += " from t_incor_usuarios"
            Me.Str_Query += " where userName ='{0}'"

            Me.Str_Query = String.Format(Str_Query, username)
            Dim table As DataTable
            table = FncGetTableQuery()
            If table.Rows.Count > 0 Then
                If (table.Rows(0).Item(2) = password) Then
                    Me.str_rol = table.Rows(0).Item(1)
                    Me.str_user = table.Rows(0).Item(0)
                    fnc_MantenerLogeado()
                    Return 1
                Else
                    Return 0
                End If
            Else
                Return 0
            End If

        End Function

        Function fnc_crear_Personal(str_ident As String, str_n1 As String, str_n2 As String, str_ap1 As String, _
                                     str_ap2 As String, cod_critico As String)

            If cod_critico = "" Then
                cod_critico = Nothing
            End If

            Me.Str_Query = " insert into t_incor_usuarios"
            Me.Str_Query += " (iden_user,pri_nom_user,seg_nom_user,pri_apell_user,seg_apell_user,cod_crit_user,estado_user)"
            Me.Str_Query += " values('{0}',UPPER('{1}'),UPPER('{2}'),UPPER('{3}'),UPPER('{4}'),{5},1)"
            Me.Str_Query = String.Format(Me.Str_Query, str_ident, str_n1, str_n2, str_ap1, str_ap2, If(cod_critico Is Nothing, "null", cod_critico))

            Return Fnc_ExecQuery()
        End Function

        Function fnc_Personalhabilitado()
            Me.Str_Query = "select * "
            Me.Str_Query += " from t_incor_usuarios where estado_user = 1"
            Return FncGetTableQuery()
        End Function

        Function fnc_ActualizarInformaciónPersonal(int_user As Integer, str_ident As String, str_n1 As String, str_n2 As String, str_a1 As String, str_a2 As String, _
                                              int_cod_critico As String)

            Me.Str_Query = "update t_incor_usuarios"
            Me.Str_Query += "  set iden_user= '{0}',"
            Me.Str_Query += "      pri_nom_user='{1}',"
            Me.Str_Query += "      seg_nom_user='{2}',"
            Me.Str_Query += "      pri_apell_user='{3}',"
            Me.Str_Query += "      seg_apell_user='{4}',"
            Me.Str_Query += "      cod_crit_user={5}"
            Me.Str_Query += " where cod_user={6}"

            Me.Str_Query = String.Format(Me.Str_Query, str_ident, str_n1, str_n2, str_a1, str_a2, int_cod_critico, int_user)
            Return Fnc_ExecQuery()
        End Function

        Function fnc_EliminarInformaciónPersonal(int_user As Integer)

            Me.Str_Query = "update t_incor_usuarios"
            Me.Str_Query += "  set estado_user=0"
            Me.Str_Query += " where cod_user={0}"

            Me.Str_Query = String.Format(Me.Str_Query, int_user)
            Return Fnc_ExecQuery()
        End Function




        Private Sub fnc_MantenerLogeado()
            HttpContext.Current.Session("UsrVl") = str_user
            HttpContext.Current.Session("UsrRol") = str_rol
        End Sub

        Shared Function EstaLoggeado()
            Return HttpContext.Current.Session("UsrVl")
        End Function

    End Class

End Namespace
