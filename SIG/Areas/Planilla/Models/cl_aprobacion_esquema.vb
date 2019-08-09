Namespace SIG.Areas.Planilla.Models
    Public Class cl_aprobacion_esquema

        Private objConexionDB As New SIG.Areas.Planilla.Models.cl_conexion_db()
        Private strQuery As String
        Private objTabla As DataTable
        Private strResultado As String
        Private intResultado As Boolean
        Private arrObjeto As Array

        Function fnc_obtener_nombre_pago(ByVal pago As String) As String

            strQuery = "SELECT pag_nombre FROM f_pla_pago WHERE pag_codigo = " + pago

            Try
                Return objConexionDB.fnc_obtener_scalar(strQuery)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Function fnc_obtener_nombre_esquema(ByVal esquema As String) As String

            strQuery = "SELECT nombre_esquema FROM f_pla_esquema WHERE esq_codigo = " + esquema

            Try
                Return objConexionDB.fnc_obtener_scalar(strQuery)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Function fnc_obtener_montos(ByVal esquema As String) As DataTable

            strQuery = "" + vbCr + _
                "SELECT	monto.confm_basico, monto.confm_nivel1_1, monto.confm_nivel1_2, monto.confm_nivel2_1," + vbCr + _
                "       monto.confm_nivel2_2, monto.confm_nivel3_1, monto.confm_nivel3_2" + vbCr + _
                "   FROM F_Pla_Conf_Montos AS monto" + vbCr + _
                "      INNER JOIN F_Pla_Conf_Montos_Esq monto_esq ON monto.confm_codigo = monto_esq.confm_codigo" + vbCr + _
                "   WHERE esq_codigo = " + esquema
            Try
                objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            Catch ex As Exception
            End Try

            Return objTabla

        End Function

        Function fnc_obtener_cobertura(ByVal esquema As String) As DataTable

            strQuery = "" + vbCr + _
                "SELECT	esq.departamento, esq.municipio, esq.aldea, esq.caserio, esq.escuela, esq.cs_salud," + vbCr + _
                "       esq.esqc_planillas_no, esq.esqc_planillas_si, esq.esqc_cargas_si, esq.esqc_cargas_no," + vbCr + _
                "       esq.esqc_signo, ori.descripcion_origen" + vbCr + _
                "	FROM f_plan_esquema_cobertura_localizacion AS esq" + vbCr + _
                "		INNER JOIN f_glo_origen AS ori ON esq.esqc_origen = ori.cod_origen" + vbCr + _
                "   WHERE esq.esq_codigo = " + esquema
            Try
                objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            Catch ex As Exception
            End Try

            Return objTabla

        End Function

        Function fnc_obtener_fondos(ByVal esquema As String) As DataTable

            strQuery = "" + vbCr + _
                "SELECT	fon_esq.departamento, fon_esq.municipio, fon_esq.aldea, fon_esq.caserio, " + vbCr + _
                "		fon_esq.escuela, fon_esq.cs_salud, fon.fond_nombre, fon_esq.fesq_signo" + vbCr + _
                "	FROM f_pla_fondos_esquema_localizacion AS fon_esq" + vbCr + _
                "		INNER JOIN [dbo].[f_pla_fondos] AS fon ON fon_esq.fond_codigo = fon.fond_codigo" + vbCr + _
                "   WHERE fon_esq.esq_codigo = " + esquema
            Try
                objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            Catch ex As Exception
            End Try

            Return objTabla

        End Function

        Function fnc_obtener_mecanismos_pago(ByVal esquema As String) As DataTable

            strQuery = "" + vbCr + _
                "SELECT	pag_esq.departamento, pag_esq.municipio, pag_esq.aldea, pag_esq.caserio, pag_esq.escuela, " + vbCr + _
                "		pag_esq.cs_salud, pag.Nombre_Pagador, pag_esq.pesq_signo, ori.descripcion_origen" + vbCr + _
                "	FROM f_pla_pagadores_esquema_localizacion AS pag_esq" + vbCr + _
                "		INNER JOIN [dbo].[f_pla_pagadores] AS pag ON pag_esq.pag_codigo = pag.Codigo_Pagador" + vbCr + _
                "		INNER JOIN f_glo_origen AS ori ON pag_esq.pesq_cod_origen = ori.cod_origen" + vbCr + _
                "   WHERE pag_esq.esq_codigo = " + esquema
            Try
                objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            Catch ex As Exception
            End Try

            Return objTabla

        End Function

        Function fnc_obtener_transferencias_actuales(ByVal esquema As String) As DataTable

            strQuery = "" + vbCr + _
                "SELECT	INTER.int_nombre, tra.bon_anyo, CONVERT(NVARCHAR, tra.bon_fecha_ini, 103) AS 'bon_fecha_ini', " + vbCr + _
                "		CONVERT(NVARCHAR, tra.bon_fecha_fin, 103) AS 'bon_fecha_fin', " + vbCr + _
                "		CONVERT(NVARCHAR, tra.bon_fecha_elegibilidad, 103) AS 'bon_fecha_elegibilidad', tra.bon_detalle_meses" + vbCr + _
                "	FROM [dbo].[f_pla_bonos_act] AS tra" + vbCr + _
                "		INNER JOIN [dbo].[f_pla_intervalo] AS inter ON tra.bon_tipo_intervalo = inter.int_codigo" + vbCr + _
                "	WHERE tra.esq_codigo = " + esquema

            Try
                objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            Catch ex As Exception
            End Try

            Return objTabla

        End Function

        Function fnc_obtener_corr_transferencia(ByVal esquema As String) As DataTable

            strQuery = "" + vbCr + _
                "SELECT com.comp_descripcion, corre.corr_descripcion, corr.bonc_orden" + vbCr + _
                "	FROM [dbo].[f_pla_bonos_corresp] AS corr" + vbCr + _
                "		INNER JOIN [dbo].[f_pla_bonos_act]  AS tra ON corr.bon_codigo = tra.bon_codigo" + vbCr + _
                "		INNER JOIN [dbo].[f_pla_corresponsabilidad] AS corre ON corr.corresp_codigo = corre.corr_codigo" + vbCr + _
                "		INNER JOIN [dbo].[f_pla_tipo_componente] AS com ON corre.comp_codigo = com.comp_codigo" + vbCr + _
                "	WHERE tra.esq_codigo = " + esquema + vbCr + _
                "   ORDER BY corr.bonc_orden ASC"

            Try
                objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            Catch ex As Exception
            End Try

            Return objTabla

        End Function

        Function fnc_obtener_corr_apercibimiento(ByVal esquema As String) As DataTable

            strQuery = "" + vbCr + _
                "SELECT	COM.comp_descripcion, CORR.corr_nombre, APER.bonca_orden" + vbCr + _
                "	FROM [dbo].[f_pla_bonos_corresp_aperc] AS APER" + vbCr + _
                "		INNER JOIN [dbo].[f_pla_corresponsabilidad] AS CORR ON APER.corra_codigo = CORR.corr_codigo" + vbCr + _
                "		INNER JOIN [dbo].[f_pla_tipo_componente] AS COM ON CORR.comp_codigo = COM.comp_codigo" + vbCr + _
                "		INNER JOIN [dbo].[f_pla_bonos_act]  AS TRA ON APER.bon_codigo = TRA.bon_codigo" + vbCr + _
                "	WHERE TRA.esq_codigo = " + esquema + vbCr + _
                "	ORDER BY APER.bonca_orden ASC"

            Try
                objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            Catch ex As Exception
            End Try

            Return objTabla

        End Function

        Function fnc_obtener_verificacion(ByVal esquema As String) As DataTable

            strQuery = "" + vbCr + _
                "SELECT VERI.ver_nombre" + vbCr + _
                "	FROM f_pla_verif_esq AS VER" + vbCr + _
                "		INNER JOIN f_pla_verificaciones AS VERI ON VER.ver_codigo = VERI.ver_codigo" + vbCr + _
                "	WHERE VER.esq_codigo = " + esquema

            Try
                objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            Catch ex As Exception
            End Try

            Return objTabla

        End Function

        Function fnc_obtener_estado_aprobacion(ByVal esquema As String) As Integer

            strQuery = "" + vbCr + _
                "SELECT esq_aprobado" + vbCr + _
                "	FROM f_pla_esquema" + vbCr + _
                "	WHERE esq_codigo = " + esquema

            Try
                intResultado = objConexionDB.fnc_obtener_scalar(strQuery)
            Catch ex As Exception
            End Try

            Return intResultado

        End Function

        Function fnc_aprobar_esquema(ByVal esquema As String) As Integer

            strQuery = "" + vbCr + _
                "UPDATE f_pla_esquema " + vbCr + _
                "   SET esq_aprobado = 1" + vbCr + _
                "   WHERE esq_codigo = " + esquema + vbCr + _
                "" + vbCr + _
                "INSERT INTO [dbo].[f_pla_esquemaH](" + vbCr + _
                "       [esq_codigo], [pag_codigo]," + vbCr + _
                "   	[nom_esquema], [esq_numero], " + vbCr + _
                "		[esq_censo], [esq_fecha_ini], " + vbCr + _
                "		[esq_fecha_fin], [esq_meses]," + vbCr + _
                "		[esq_tipo_intervalo], [esq_tipo_pago]," + vbCr + _
                "		[esq_fecha_elegibilidad], [esq_anyo]," + vbCr + _
                "   	[esq_cant_bonos_act], [esq_detalle_meses], " + vbCr + _
                "		[periodo], [esq_estado]," + vbCr + _
                "		[esq_tipo_esquema], [esq_accion], [esq_fech_accion], " + vbCr + _
                "       [esq_usuario_accion], [esq_desc_cambio] )" + vbCr + _
                "	SELECT" + vbCr + _
                "           [esq_codigo], [pag_codigo], " + vbCr + _
                "			[nombre_esquema], [esq_numero], " + vbCr + _
                "			[esq_censo], [esq_fecha_ini], " + vbCr + _
                "			[esq_fecha_fin], [esq_meses]," + vbCr + _
                "			[esq_tipo_intervalo], [esq_tipo_pago]," + vbCr + _
                "			[esq_fecha_elegibilidad], [esq_anyo]," + vbCr + _
                "			[esq_cant_bonos_act], [esq_detalle_meses], " + vbCr + _
                "			[periodo], [esq_estado]," + vbCr + _
                "			[esq_tipo_esquema], 2, GETDATE(), " + vbCr + _
                "           " + CStr(HttpContext.Current.Session("usuario")) + ", 'Aprobado'" + vbCr + _
                "		FROM " + vbCr + _
                "			[dbo].[f_pla_esquema]" + vbCr + _
                "		WHERE [esq_codigo] = " + esquema

            Try
                Return objConexionDB.fnc_ejecutar_simple_comando(strQuery)
            Catch ex As Exception
                Return 0
            End Try
        End Function


        Function fnc_posee_permiso(ByRef intCodUsuario As Integer) As Boolean
            strQuery = String.Format("SELECT " & _
                                    "	COUNT(USU.cod_usuario) " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.T_usuarios AS USU " & _
                                    "INNER JOIN " & _
                                    "	SIG_T.dbo.t_usuarios_roles AS USU_ROL " & _
                                    "	ON USU.cod_usuario = USU_ROL.cod_usuario " & _
                                    "INNER JOIN " & _
                                    "	SIG_T.dbo.T_Roles AS ROL " & _
                                    "	ON USU_ROL.id_rol = ROL.id_rol " & _
                                    "WHERE " & _
                                    "	USU.cod_usuario = {0} " & _
                                    "	AND ROL.id_rol = 24 ", intCodUsuario)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            If arrObjeto(0) = 0 Then
                Return False
            Else
                Return True
            End If
        End Function


    End Class
End Namespace

