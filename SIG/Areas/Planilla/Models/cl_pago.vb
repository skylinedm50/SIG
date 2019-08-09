Imports System.ComponentModel.DataAnnotations

Namespace SIG.Areas.Planilla.Models
    Public Class cl_pago
        Private objConexionDB As New SIG.Areas.Planilla.Models.cl_conexion_db()
        Private objManejoDatos As New SIG.Areas.Planilla.Models.cl_manejo_datos()
        Private strQuery As String
        Private objTabla As DataTable
        Private arrObjeto As Array

        <Key>
        Public Property pag_codigo As Integer

        <Required(ErrorMessage:="El campo es obligatorio, no puede ser nulo.")>
        <StringLength(60, ErrorMessage:="El nombre debe poseer más de 2 caracteres.", MinimumLength:=3)>
        Public Property pag_nombre As String

        <Required(ErrorMessage:="El campo es obligatorio, no puede ser nulo.")>
        Public Property pag_anyo As Integer

        Public Property pag_numero As Integer

        <Required(ErrorMessage:="El campo es obligatorio, no puede ser nulo.")>
        <StringLength(60, ErrorMessage:="La descripción debe poseer más de 2 caracteres.", MinimumLength:=3)>
        Public Property pag_descripcion As String

        Public Property pag_accion As Integer
        Public Property pag_usuario_accion As Integer


        Function fnc_obtener_pagos() As DataTable
            strQuery = "SELECT " & _
                        "	pag_codigo, " & _
                        "	pag_anyo, " & _
                        "	pag_numero, " & _
                        "	pag_nombre, " & _
                        "	pag_descripcion " & _
                        "FROM " & _
                        "	SIG_T.dbo.f_pla_pago " & _
                        "ORDER BY " & _
                        "	pag_codigo DESC "

            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_ingresar_pago() As Integer
            Try
                strQuery = String.Format("DECLARE @intCodPago INT, " & _
                                        "       @intNumePago INT;" & _
                                        "SELECT " & _
                                        "	@intNumePago = ISNULL(MAX(pag_numero) + 1, 1) " & _
                                        "FROM " & _
                                        "	SIG_T.dbo.f_pla_pago " & _
                                        "WHERE " & _
                                        "	pag_anyo = {0}; " & _
                                        "INSERT INTO SIG_T.dbo.f_pla_pago " & _
                                        "			( " & _
                                        "				pag_nombre, " & _
                                        "				pag_descripcion, " & _
                                        "				pag_anyo, " & _
                                        "				pag_numero, " & _
                                        "				Pag_Estado " & _
                                        "			) " & _
                                        "VALUES(RTRIM(LTRIM('{1}')), RTRIM(LTRIM('{2}')), {3}, @intNumePago, 0); " & _
                                        "SET @intCodPago = SCOPE_IDENTITY(); " & _
                                        "INSERT INTO SIG_T.dbo.f_pla_pagoH " & _
                                        "			( " & _
                                        "				pag_codigo, " & _
                                        "				pag_nombre, " & _
                                        "				pag_descripcion, " & _
                                        "				pag_anyo, " & _
                                        "				pag_numero, " & _
                                        "				Pag_Estado, " & _
                                        "				pag_accion, " & _
                                        "				pag_fech_accion, " & _
                                        "				pag_usuario_accion " & _
                                        "			) " & _
                                        "SELECT " & _
                                        "	pag_codigo, " & _
                                        "	pag_nombre, " & _
                                        "	pag_descripcion, " & _
                                        "	pag_anyo, " & _
                                        "	pag_numero, " & _
                                        "	Pag_Estado, " & _
                                        "	1, " & _
                                        "	GETDATE(), " & _
                                        "   {4} " & _
                                        "FROM " & _
                                        "	SIG_T.dbo.f_pla_pago " & _
                                        "WHERE " & _
                                        "	pag_codigo = @intCodPago; ", pag_anyo, objManejoDatos.fnc_formatear_texto_query(pag_nombre), objManejoDatos.fnc_formatear_texto_query(pag_descripcion), pag_anyo, pag_usuario_accion)
                objConexionDB.fnc_ejecutar_simple_comando(strQuery)
                Return 1
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Function fnc_actualizar_pago() As Integer
            Try
                strQuery = String.Format("UPDATE  " & _
                                        "	SIG_T.dbo.f_pla_pago " & _
                                        "SET " & _
                                        "	pag_nombre = LTRIM(RTRIM('{0}')), " & _
                                        "	pag_descripcion = LTRIM(RTRIM('{1}')) " & _
                                        "WHERE " & _
                                        "	pag_codigo = {2}; " & _
                                        "INSERT INTO SIG_T.dbo.f_pla_pagoH  " & _
                                        "			(  " & _
                                        "				pag_codigo,  " & _
                                        "				pag_nombre,  " & _
                                        "				pag_descripcion,  " & _
                                        "				pag_anyo,  " & _
                                        "				pag_numero,  " & _
                                        "				Pag_Estado,  " & _
                                        "				pag_accion,  " & _
                                        "				pag_fech_accion,  " & _
                                        "				pag_usuario_accion  " & _
                                        "			)  " & _
                                        "SELECT  " & _
                                        "	pag_codigo,  " & _
                                        "	pag_nombre,  " & _
                                        "	pag_descripcion,  " & _
                                        "	pag_anyo,  " & _
                                        "	pag_numero,  " & _
                                        "	Pag_Estado,  " & _
                                        "	1,  " & _
                                        "	GETDATE(),  " & _
                                        "   {3}  " & _
                                        "FROM  " & _
                                        "	SIG_T.dbo.f_pla_pago  " & _
                                        "WHERE  " & _
                                        "	pag_codigo = {2}; ", objManejoDatos.fnc_formatear_texto_query(pag_nombre), objManejoDatos.fnc_formatear_texto_query(pag_descripcion), pag_codigo, pag_usuario_accion)
                objConexionDB.fnc_ejecutar_simple_comando(strQuery)
                Return 1
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Function fnc_borrar_pago() As Integer
            Try
                strQuery = String.Format("INSERT INTO SIG_T.dbo.f_pla_pagoH " & _
                            "			( " & _
                            "				pag_codigo, " & _
                            "				pag_nombre, " & _
                            "				pag_descripcion, " & _
                            "				pag_anyo, " & _
                            "				pag_numero, " & _
                            "				Pag_Estado, " & _
                            "							" & _
                            "				pag_accion, " & _
                            "				pag_fech_accion, " & _
                            "				pag_usuario_accion " & _
                            "			) " & _
                            "SELECT " & _
                            "	pag_codigo, " & _
                            "	pag_nombre, " & _
                            "	pag_descripcion, " & _
                            "	pag_anyo, " & _
                            "	pag_numero, " & _
                            "	Pag_Estado, " & _
                            "				" & _
                            "	3, " & _
                            "	GETDATE(), " & _
                            "   {0} " & _
                            "FROM " & _
                            "	SIG_T.dbo.f_pla_pago " & _
                            "WHERE " & _
                            "	pag_codigo = {1} ", pag_usuario_accion, pag_codigo)
                objConexionDB.fnc_ejecutar_simple_comando(strQuery)

                strQuery = String.Format("DELETE  " & _
                                        "FROM  " & _
                                        "	SIG_T.dbo.f_pla_pago  " & _
                                        "WHERE  " & _
                                        "	pag_codigo = {0} ", pag_codigo)
                objConexionDB.fnc_ejecutar_simple_comando(strQuery)
                Return 1
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Function fnc_verificar_nombre_repetido() As Integer
            Dim strCondicionByCod As String

            If pag_codigo = 0 Then
                strCondicionByCod = ""
            Else
                strCondicionByCod = String.Format("AND pag_codigo <> {0}", pag_codigo)
            End If

            strQuery = String.Format("SELECT " & _
                                    "	COUNT(pag_codigo) " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.f_pla_pago " & _
                                    "WHERE " & _
                                    "	pag_nombre = RTRIM(LTRIM('{0}')) " & _
                                    "   {1} ", objManejoDatos.fnc_formatear_texto_query(pag_nombre), strCondicionByCod)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            If arrObjeto(0) = 0 Then
                Return 0
            Else
                Return 1
            End If
        End Function

        Function fnc_pago_pertence_esquemas() As Integer
            strQuery = String.Format("SELECT " & _
                                    "	COUNT(esq_codigo) " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.f_pla_esquema " & _
                                    "WHERE " & _
                                    "	pag_codigo = {0} ", pag_codigo)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            If arrObjeto(0) = 0 Then
                Return 0
            Else
                Return 1
            End If
        End Function

        Function fnc_pagos_menores_mismo_año()
            strQuery = String.Format("DECLARE @intAño INT, " & _
                                    "		@intNumero INT; " & _
                                    "SELECT " & _
                                    "	@intAño = pag_anyo,  " & _
                                    "	@intNumero = pag_numero " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.f_pla_pago " & _
                                    "WHERE " & _
                                    "	pag_codigo = {0}; " & _
                                    "SELECT " & _
                                    "	COUNT(pag_codigo) " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.f_pla_pago " & _
                                    "WHERE " & _
                                    "	pag_anyo = @intAño " & _
                                    "	AND pag_numero > @intNumero; ", pag_codigo)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            If arrObjeto(0) = 0 Then
                Return 0
            Else
                Return 1
            End If
        End Function

    End Class
End Namespace
