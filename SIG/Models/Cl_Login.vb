﻿Imports System.Web.Script.Serialization
Imports System.Net

Public Class Cl_Login
    Inherits Cl_Conexion

    Dim Dictionary_Datos As Dictionary(Of String, Object)
    Dim lista As New List(Of Dictionary(Of String, Object))
    Dim serializador As New JavaScriptSerializer()

    Function Fnc_BuscarUsuario(ByVal usuario As String, ByVal password As String)

        Dim tabla As New DataTable()

        Me.Str_Query = "exec P_Autenticar '{0}'"
        Me.Str_Query = String.Format(Str_Query, usuario)

        Try
            tabla = Me.FncGetTableQuery()
        Catch ex As Exception
            tabla = Nothing
        End Try

        'tabla = Me.FncGetTableQuery()

        Return tabla
    End Function


    'Private Sub fnc_MantenerLogeado()
    '    HttpContext.Current.Session("UsrVl") = str_user
    '    HttpContext.Current.Session("UsrRol") = str_rol
    'End Sub

    Function Fnc_loggeado()
        Return HttpContext.Current.Session("Asignacion")
    End Function


    Function fnc_validarModulo(modulo)

        For Each row As DataRow In HttpContext.Current.Session("Asignacion").rows
            If row(3) = modulo Then
                Return True
            End If
        Next

        Return False

    End Function

    Sub Fnc_MenuPrincipal()


        HttpContext.Current.Response.Write("<nav id='Menu'>")
        HttpContext.Current.Response.Write("<ul>")
        HttpContext.Current.Response.Write("<li id='tituloMenu' style='text-align:center;   box-shadow: 0 0 5px rgba(0,0,0,0.1); background-color: #3B59DB; color:white; width:300px; height:40px; line-height:40px; margin: 0 auto; '><b>MENU PRINCIPAL</b></li>")

        For Each Fila As DataRow In HttpContext.Current.Session("modulo").Rows
            HttpContext.Current.Response.Write("<li><b><a class='list' href=" + Fila(6) + "><i class = 'fa fa-" + Fila(9) + "'></i>&nbsp;" + Fila(5) + "</a></b><li>")
        Next

        HttpContext.Current.Response.Write("</ul>")
        HttpContext.Current.Response.Write("</nav>")

    End Sub


    Sub Fnc_ListarActividadesUsuario()
        Dim tablaActividades As New DataTable()

        If HttpContext.Current.Session("MenuModulo") Is Nothing Then
            If HttpContext.Current.Session("usuario") Is Nothing Then
                HttpContext.Current.Response.Redirect("/Home/Login")
            End If
            Me.Str_Query = "exec P_Actividades '{0}'"
            Me.Str_Query = String.Format(Str_Query, HttpContext.Current.Session("usuario"))
            tablaActividades = Me.FncGetTableQuery()
            HttpContext.Current.Session.Add("MenuModulo", tablaActividades)
        End If

    End Sub


    Function Fnc_ValidarPermisoUrl(GetUrl As String)
        For Each fila As DataRow In HttpContext.Current.Session("MenuModulo").Rows
            If fila(1) = GetUrl Then
                Return True
            End If
        Next
        Return False
    End Function

    Function Fnc_MenuModulo(modulo)


        Dim StringMenu As String = ""

        Fnc_ListarActividadesUsuario()
        Dim opcion As String = ""

        StringMenu += "<ul id='accordion' class='accordion'>"
        StringMenu += " <li id='tituloMenu' style='text-align:center;  box-shadow: 0 0 5px rgba(0,0,0,0.1); background-color: #3B59DB; color:white; width:300px; height:40px; line-height:40px; margin: 0 auto;'><b>MENU MODULO</b></li>"
        StringMenu += " <li>"
        For Each Fila As DataRow In HttpContext.Current.Session("MenuModulo").Rows
            If modulo = Fila(3) Then
                If opcion <> Fila(2) Then
                    If opcion <> "" Then
                        StringMenu += " </ul>"
                    End If
                    StringMenu += " <div class='link'><b><i class='fa fa-list-ul'></i>" + Fila(2) + "<i class='fa fa-chevron-down'></i></b></div>"
                    StringMenu += " <ul class='submenu'>"
                    StringMenu += " <b><li><a href=" + Fila(1) + ">" + Fila(0) + "</a></li></b>"
                Else
                    StringMenu += " <b><li><a href=" + Fila(1) + ">" + Fila(0) + "</a></li></b>"
                End If
                opcion = Fila(2)

            End If
        Next

        StringMenu += " </ul>"
        StringMenu += " </li>"
        StringMenu += " <b><li class='link'><i class='fa fa-reply-all'></i><a style='color:#077CC0;' href='/Home/MenuPrincipal/?ref=428" + HttpContext.Current.Session("usuario").ToString() + "'>Regresar al Menu Principal</a></li></b>"

        StringMenu += " </ul>"

        HttpContext.Current.Session("menu") = StringMenu

        'HttpContext.Current.Response.Write("<div>HOLA</div>")
        Return StringMenu

    End Function

    Function Fnc_Bloquer(ByVal userName As String)
        Dim hostname = Dns.GetHostName()
        Dim ipHostEntry As IPHostEntry = Dns.GetHostEntry(hostname)
        Dim ipClientHost = Convert.ToString(ipHostEntry.AddressList(1))

        Me.Str_Query = "exec P_Bloquear '{0}' , '{1}'"
        Me.Str_Query = String.Format(Me.Str_Query, ipClientHost, userName)

        Return Me.Fnc_ExecQuery()

    End Function

    Function Fnc_Ultimo_Intento(ByVal usr As String)
        Try
            Dim fecha As Date
            Dim a As Boolean
            Me.Str_Query = "SELECT TOP 1 [fecha_intento_logeo] FROM [SIG_T].[dbo].[t_log_intento_logeo]" + vbCr +
                            "WHERE nombre_usuario_intento_logeo = '" + usr + "' ORDER BY fecha_intento_logeo desc "
            Dim table As DataTable = Me.FncGetTableQuery()
            fecha = table.Rows(0).Item("fecha_intento_logeo")

            Return DateDiff(DateInterval.Minute, fecha, Date.Now)
        Catch ex As Exception
            Return 0
        End Try
    End Function


    Function Fnc_user_opciones_planilla(ByVal codUser As String) As String

        Dim table As New DataTable
        Dim strJson As String

        Me.Str_Query = "" + vbCr + _
            "SELECT DISTINCT opc.desc_opcion, opc.id_opcion" + vbCr + _
            "FROM T_usuarios AS usu" + vbCr + _
            "	INNER JOIN t_usuarios_roles AS usu_rol ON usu_rol.cod_usuario = usu.cod_usuario" + vbCr + _
            "	INNER JOIN T_rol_actividad AS rol_act ON rol_act.id_rol = usu_rol.id_rol" + vbCr + _
            "	INNER JOIN T_actividad AS act ON act.id_actividad = rol_act.id_actividad" + vbCr + _
            "	INNER JOIN T_opciones As opc ON opc.id_opcion = act.id_opcion" + vbCr + _
            "WHERE usu.cod_usuario = " + codUser + " AND opc.id_modulo = 3"

        table = Me.FncGetTableQuery()

        Dim arrListDataTable As New List(Of Dictionary(Of String, Object))
        Dim obj As SIG.Areas.Corresponsabilidad.Models.cl_manejo_datos = New SIG.Areas.Corresponsabilidad.Models.cl_manejo_datos()

        strJson = obj.fnc_crear_datatable_json(table)

        Return strJson
    End Function



    Function Fnc_user_actividades_planilla(ByVal codUser As String) As String

        Dim table As New DataTable
        Dim strJson As String

        Me.Str_Query = "" + vbCr + _
            "SELECT act.url, opc.id_opcion " + vbCr + _
            "FROM T_usuarios AS usu " + vbCr + _
            "   INNER JOIN t_usuarios_roles AS usu_rol " + vbCr + _
            "       ON usu_rol.cod_usuario = usu.cod_usuario " + vbCr + _
            "   INNER JOIN T_rol_actividad AS rol_act " + vbCr + _
            "       ON usu_rol.id_rol = rol_act.id_rol " + vbCr + _
            "   INNER JOIN T_actividad AS act " + vbCr + _
            "       ON act.id_actividad = rol_act.id_actividad " + vbCr + _
            "   INNER JOIN T_opciones AS opc " + vbCr + _
            "       ON act.id_opcion = opc.id_opcion " + vbCr + _
            "WHERE usu.cod_usuario = " + codUser + " AND id_modulo = 0"

        table = Me.FncGetTableQuery()

        Dim arrListDataTable As New List(Of Dictionary(Of String, Object))
        Dim obj As SIG.Areas.Corresponsabilidad.Models.cl_manejo_datos = New SIG.Areas.Corresponsabilidad.Models.cl_manejo_datos()

        strJson = obj.fnc_crear_datatable_json(table)

        Return strJson
    End Function

    Function Fnc_verificar_estado_contraseña(ByVal usuario As String) As Boolean

        Me.Str_Query = "" + vbCr + _
            "SELECT clave_def_usuario" + vbCr + _
            "FROM T_usuarios" + vbCr + _
            "WHERE cod_usuario = " + usuario

        If Me.Fnc_GetSingledataConexion() = 1 Then
            Return True
        Else
            Return False
        End If

        'Return Me.Fnc_GetSingledataConexion()

    End Function

    Function fnc_guardar_intento_logeo(ByVal usuario As String, ByVal hostname As String, ByVal mac As String, ByVal ip As String, ByVal ipProxy As String) As Integer

        Me.Str_Query = "" + vbCr +
            "INSERT INTO [SIG_T].[dbo].[t_log_intento_logeo]" + vbCr +
            "           ([nombre_usuario_intento_logeo]" + vbCr +
            "           ,[mac_intento_logeo]" + vbCr +
            "           ,[ip_intento_logeo]" + vbCr +
            "           ,[ip_proxy_intento_logeo]" + vbCr +
            "           ,[hostname_intento_logeo]" + vbCr +
            "           ,[fecha_intento_logeo])" + vbCr +
            "        VALUES" + vbCr +
            "           ('" + usuario + "'" + vbCr +
            "           ,'" + mac + "'" + vbCr +
            "           ,'" + ip + "'" + vbCr +
            "           ,'" + ipProxy + "'" + vbCr +
            "           ,'" + hostname + "'" + vbCr +
            "           ,GETDATE())" + vbCr +
            "" + vbCr +
            "SELECT @@IDENTITY"

        Return Me.Fnc_GetSingledataConexion()

    End Function

    Function fnc_guardar_intento_logeo(ByVal usuario As String, ByVal ip As String) As Integer

        Me.Str_Query = "" + vbCr +
            "INSERT INTO [SIG_T].[dbo].[t_log_intento_logeo]" + vbCr +
            "           ([nombre_usuario_intento_logeo]" + vbCr +
            "           ,[ip_intento_logeo]" + vbCr +
            "           ,[fecha_intento_logeo])" + vbCr +
            "        VALUES" + vbCr +
            "           ('" + usuario + "'" + vbCr +
            "           ,'" + ip + "'" + vbCr +
            "           ,GETDATE())" + vbCr +
            "" + vbCr +
            "SELECT @@IDENTITY"

        Return Me.Fnc_GetSingledataConexion()

    End Function

    Sub fnc_guardar_inicio_sesion(ByVal cod_intento As String, ByVal nombre As String)

        Me.Str_Query = "" + vbCr +
            "INSERT INTO [SIG_T].[dbo].[t_log_inicios_sesion]" + vbCr +
            "           ([cod_intento_logeo]" + vbCr +
            "           ,[nombre_persona_inicio_sesion])" + vbCr +
            "        VALUES" + vbCr +
            "           (" + cod_intento + vbCr +
            "           ,'" + nombre + "')"

        Me.Fnc_GetSingledataConexion()

    End Sub

End Class
