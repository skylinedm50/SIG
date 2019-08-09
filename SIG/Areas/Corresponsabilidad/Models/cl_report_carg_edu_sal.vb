Namespace SIG.Areas.Corresponsabilidad.Models
    Public Class cl_report_carg_edu_sal
        Private objConexionDB As New SIG.Areas.Corresponsabilidad.Models.cl_conexion_db
        Private strQuery As String
        Private objTabla As DataTable
        Private arrObjeto As Array
        Dim objFila As DataRow
        Private arrStrTipoRegistrosEdu() As String = {"Estados de matricula", "Razones de cancelación", "Tipo de administración de centro educativo.", "Tipo de periodo escolar", "Niveles de centro educativo.", "Sub-nivel de centro educativo", "Centro educativo", "Matriculas", "Parciales", "Asistencias ó inasistencias", "Promoción", "Sub-nivel por centro educativo."}
        Private arrStrTipoResgistroSal() As String = {"Centro de salud", "Visitas médicas"}
        Private arrStrTipoProceCentSal() As String = {"Nuevo", "Actualización", "Ya existe", "Sin proceso"}



        Public Function fnc_obtener_nombre_actulizacion(ByRef intCodTipCom As Integer, ByRef intTipForm As Integer) As DataTable
            Dim objFila As DataRow

            If intTipForm = 0 Then
                Select Case intCodTipCom
                    Case 1
                        strQuery = "Select " &
                                    "	num_car_log_car, " &
                                    "	'Actualización ' + CONVERT(NVARCHAR, num_car_log_car) AS actuali " &
                                    "FROM " &
                                    "	SIG_R.dbo.t_corr_log_cargas " &
                                    "GROUP BY " &
                                    "	num_car_log_car " &
                                    "ORDER BY " &
                                    "	num_car_log_car DESC "
                        Exit Select
                    Case 2
                        strQuery = "SELECT " &
                                    "	num_car_log_car_sal AS num_car_log_car, " &
                                    "	'Actualización ' + CONVERT(NVARCHAR, num_car_log_car_sal) AS actuali " &
                                    "FROM " &
                                    "	SIG_R.dbo.t_corr_log_carga_salud " &
                                    "GROUP BY " &
                                    "	num_car_log_car_sal " &
                                    "ORDER BY " &
                                    "	num_car_log_car_sal DESC "
                        Exit Select
                End Select
            Else
                Dim strWhere As String = ""

                Select Case intCodTipCom
                    Case 1
                        strWhere = "cod_des_ser < 15 "
                        Exit Select
                    Case 2
                        strWhere = "cod_des_ser > 14 "
                        Exit Select
                End Select

                strQuery = String.Format("SELECT " &
                                        "  num_car_log_car, " &
                                        "  'Actualización ' + CONVERT(NVARCHAR(30), num_car_log_car) AS actuali " &
                                        "FROM " &
                                        "  SIG_R.dbo.t_corr_error_conexion_carga " &
                                        "WHERE " &
                                        "  {0} " &
                                        "GROUP BY " &
                                        "  num_car_log_car " &
                                        "ORDER BY " &
                                        "  num_car_log_car DESC ", strWhere)
            End If

            objTabla = objConexionDB.fnc_crear_datatable(strQuery)

            If objTabla.Rows.Count > 0 Then
                objFila = objTabla.NewRow()
                objFila.Item("num_car_log_car") = 0
                objFila.Item("actuali") = "(Todo)"
                objTabla.Rows.InsertAt(objFila, 0)
            End If


            Return objTabla
        End Function

        Public Function fnc_obtener_actulizaciones(ByRef intTipCom As Integer, intNumActuli As Integer) As DataTable
            Dim strWhere As String = ""

            Select Case intTipCom
                Case 1
                    strQuery = "SELECT " &
                                    "	cod_log_car, " &
                                    "	fec_ini_log_car, " &
                                    "	fec_fin_log_car, " &
                                    "	can_rec_log_car, " &
                                    "	can_dat_pro_log_car, " &
                                    "	cod_est_log_car, " &
                                    "	cod_tab_car AS cod_des_ser, " &
                                    "	num_car_log_car " &
                                    "FROM " &
                                    "	SIG_R.dbo.t_corr_log_cargas "
                    If intNumActuli > 0 Then
                        strWhere = String.Format("WHERE " &
                                                "	num_car_log_car = {0} ", intNumActuli)
                        strQuery += strWhere
                    End If
                    Exit Select
                Case 2
                    strQuery = "SELECT " &
                                    "	cod_log_car_sal AS cod_log_car, " &
                                    "	fec_ini_log_car_sal AS fec_ini_log_car, " &
                                    "	fec_fin_log_car_sal AS fec_fin_log_car, " &
                                    "	can_dat_det_log_car_sal AS can_rec_log_car, " &
                                    "	can_dat_pro_log_car_sal AS can_dat_pro_log_car, " &
                                    "	CASE WHEN fec_fin_log_car_sal IS NULL THEN 1 ELSE 2 END AS cod_est_log_car, " &
                                    "	cod_des_ser, " &
                                    "	num_car_log_car_sal AS num_car_log_car " &
                                    "FROM " &
                                    "	SIG_R.dbo.t_corr_log_carga_salud "
                    If intNumActuli > 0 Then
                        strWhere = String.Format("WHERE " &
                                                "	num_car_log_car_sal = {0} ", intNumActuli)
                        strQuery += strWhere
                    End If
                    Exit Select
            End Select

            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Public Function fnc_estados_log_carga() As DataTable
            strQuery = "SELECT  " &
                        "	cod_est_log_car, " &
                        "   nom_est_log_car " &
                        " FROM  " &
                        "	[SIG_R].[dbo].[t_corr_estado_log_carga] "

            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Public Function fnc_tipos_registros(ByRef intCodTipCarga As Integer) As DataTable
            objTabla = New DataTable

            objTabla.Columns.Add("cod_des_ser", GetType(Integer))
            objTabla.Columns.Add("nom_des_ser", GetType(String))

            If intCodTipCarga = 1 Then
                For i = 1 To arrStrTipoRegistrosEdu.Length
                    objTabla.Rows.Add(i, arrStrTipoRegistrosEdu(i - 1))
                Next
            Else
                For i = 1 To arrStrTipoResgistroSal.Length
                    objTabla.Rows.Add(i, arrStrTipoResgistroSal(i - 1))
                Next
            End If

            Return objTabla
        End Function

        Public Function fnc_obtener_detalle_log(ByRef intCodComp As Integer, ByRef intCodTipRegis As Integer, ByRef intCodLog As Integer) As DataTable
            Select Case intCodComp
                Case 1
                    Select Case intCodTipRegis
                        Case 1
                            strQuery = String.Format("SELECT " &
                                                    "      CAR.cod_est_mat_car_est_mat AS codigo, " &
                                                    "      CAR.nom_est_mat_car_est_mat AS nombre, " &
                                                    "      CASE WHEN CAM.cod_tip_cam IS NULL THEN 4 ELSE CAM.cod_tip_cam END AS cod_pro_regis   " &
                                                    "FROM " &
                                                    "      SIG_R.dbo.t_corr_carga_estados_matricula AS CAR " &
                                                    "LEFT OUTER JOIN " &
                                                    "      SIG_T.dbo.t_corr_cambio_estados_matricula AS CAM " &
                                                    "      ON CAR.cod_car_est_mat = CAM.cod_car_est_mat " &
                                                    "WHERE " &
                                                    "      CAR.cod_log_car = {0} ", intCodLog)
                        Case 2
                            strQuery = String.Format("SELECT " &
                                                    "      CAR.cod_raz_can_car_raz_can AS codigo, " &
                                                    "      CAR.nom_raz_can_car_raz_can AS nombre,     " &
                                                    "      CASE WHEN CAM.cod_tip_cam IS NULL THEN 4 ELSE CAM.cod_tip_cam END AS cod_pro_regis   " &
                                                    "FROM " &
                                                    "      SIG_R.dbo.t_corr_carga_razones_cancelacion AS CAR " &
                                                    "LEFT OUTER JOIN " &
                                                    "      SIG_T.dbo.t_corr_cambio_razones_cancelacion AS CAM " &
                                                    "      ON CAR.cod_car_raz_can = CAM.cod_car_raz_can " &
                                                    "WHERE " &
                                                    "      CAR.cod_log_car = {0} ", intCodLog)
                        Case 3
                            strQuery = String.Format("SELECT " &
                                                    "      CAR.cod_adm_cen_car_adm_cen AS codigo, " &
                                                    "      CAR.nom_adm_cen_car_adm_cen AS nombre, " &
                                                    "      CASE WHEN CAM.cod_tip_cam IS NULL THEN 4 ELSE CAM.cod_tip_cam END AS cod_pro_regis   " &
                                                    "FROM " &
                                                    "      SIG_R.dbo.t_corr_carga_admin_centro AS CAR " &
                                                    "LEFt OUTER JOIN " &
                                                    "      SIG_T.dbo.t_corr_cambio_admin_centro AS CAM " &
                                                    "      ON CAR.cod_car_adm_cen = CAM.cod_car_adm_cen " &
                                                    "WHERE " &
                                                    "      CAR.cod_log_car = {0} ", intCodLog)
                        Case 4
                            strQuery = String.Format("SELECT " &
                                                    "      CAR.cod_car_per_esc, " &
                                                    "      CAR.cod_per_esc_car_per_esc, " &
                                                    "      CAR.des_per_esc_car_per_esc, " &
                                                    "      CAR.mes_ini_per_esc_car_per_esc, " &
                                                    "      CAR.mes_fin_per_esc_car_per_esc, " &
                                                    "      CASE WHEN CAM.cod_tip_cam IS NULL THEN 4 ELSE CAM.cod_tip_cam END AS cod_pro_regis  " &
                                                    "FROM " &
                                                    "      SIG_R.dbo.t_corr_carga_periodo_escolar AS CAR " &
                                                    "LEFT OUTER JOIN " &
                                                    "      SIG_T.dbo.t_corr_cambio_periodo_escolar AS CAM " &
                                                    "      ON CAR.cod_car_per_esc = CAM.cod_car_per_esc " &
                                                    "WHERE  " &
                                                    "      CAR.cod_log_car = {0} ", intCodLog)
                        Case 5
                            strQuery = String.Format("SELECT " &
                                                    "      CAR.cod_car_niv_edu, " &
                                                    "      CAR.cod_sac_niv_edu_car_niv_edu, " &
                                                    "      CAR.cod_niv_edu_car_niv_edu, " &
                                                    "      CAR.des_niv_edu_car_niv_edu, " &
                                                    "      CASE WHEN CAM.cod_tip_cam IS NULL THEN 4 ELSE CAM.cod_tip_cam END AS cod_pro_regis  " &
                                                    "FROM " &
                                                    "      SIG_R.dbo.t_corr_carga_niveles_educativos AS CAR " &
                                                    "LEFT OUTER JOIN " &
                                                    "      SIG_T.dbo.t_corr_cambio_niveles_educativos AS CAM " &
                                                    "      ON CAR.cod_car_niv_edu = CAM.cod_car_niv_edu " &
                                                    "WHERE " &
                                                    "      CAR.cod_log_car = {0} ", intCodLog)
                        Case 6
                            strQuery = String.Format("SELECT " &
                                                    "      CAR.cod_sac_sub_niv_car_sub_niv, " &
                                                    "      CAR.cod_sac_niv_edu_car_sub_niv, " &
                                                    "      CAR.cod_sub_niv_car_sub_niv, " &
                                                    "      CAR.des_sub_niv_car_sub_niv, " &
                                                    "      CASE WHEN CAM.cod_tip_cam IS NULL THEN 4 ELSE CAM.cod_tip_cam END AS cod_pro_regis " &
                                                    "FROM " &
                                                    "      SIG_R.dbo.t_corr_carga_sub_niveles AS CAR " &
                                                    "LEFT OUTER JOIN " &
                                                    "      SIG_T.dbo.t_corr_cambio_sub_niveles AS CAM " &
                                                    "      ON CAR.cod_car_sub_niv = CAM.cod_car_sub_niv " &
                                                    "WHERE  " &
                                                    "      CAR.cod_log_car = {0} ", intCodLog)
                        Case 7
                            strQuery = String.Format("SELECT   " &
                                                    "      CAR.cod_car_cen_edu, " &
                                                    "      DEP.nom_dep_sac,  " &
                                                    "      MUN.nom_mun_sac,  " &
                                                    "      ALD.nom_ald_sac,  " &
                                                    "      CAS.nom_cas_sac,  " &
                                                    "      BAR.nom_bar_sac,  " &
                                                    "      CAR.cod_sac_cen_edu_car_cen_edu,  " &
                                                    "      CAR.cod_cen_edu_car_cen_edu,  " &
                                                    "      CAR.nom_cen_edu_car_cen_edu,  " &
                                                    "      CAR.tel_cen_edu_car_cen_edu,  " &
                                                    "      CAR.cel_cen_edu_car_cen_edu,  " &
                                                    "      CAR.cor_cen_edu_car_cen_edu,  " &
                                                    "      CAR.dir_cen_edu_car_cen_edu, " &
                                                    "      CASE WHEN CAR.cod_fun_cen_edu_car_cen_edu IS NULL OR CAR.cod_fun_cen_edu_car_cen_edu = '' THEN 0 ELSE CAR.cod_fun_cen_edu_car_cen_edu END AS cod_fun_cen_edu, " &
                                                    "      CASE WHEN CAR.cod_adm_cen_car_cen_edu IS NULL OR CAR.cod_adm_cen_car_cen_edu = '' THEN 0 ELSE CAR.cod_adm_cen_car_cen_edu END AS cod_adm_cen, " &
                                                    "      CASE WHEN CAR.cod_tip_zon_car_cen_edu IS NULL OR  CAR.cod_tip_zon_car_cen_edu = '' THEN 0 ELSE CAR.cod_tip_zon_car_cen_edu END AS cod_tip_zon, " &
                                                    "      CASE WHEN CAR.cod_per_esc_car_cen_edu IS NULL OR CAR.cod_per_esc_car_cen_edu = '' THEN 0 ELSE CAR.cod_per_esc_car_cen_edu END AS cod_per_esc,  " &
                                                    "      CASE WHEN CAR.cod_sac_pai_fro_car_cen_edu IS NULL OR CAR.cod_sac_pai_fro_car_cen_edu = '' THEN 0 ELSE CAR.cod_sac_pai_fro_car_cen_edu END AS cod_sac_pai_fro, " &
                                                    "      CASE WHEN CAR.cod_etn_car_cen_edu IS NULL OR CAR.cod_etn_car_cen_edu = '' THEN 0 ELSE CAR.cod_etn_car_cen_edu END AS cod_etn,  " &
                                                    "      CASE WHEN CAM.cod_tip_cam IS NULL THEN 4 ELSE CAM.cod_tip_cam END AS cod_pro_regis  " &
                                                    "FROM  " &
                                                    "      SIG_R.dbo.t_corr_carga_centro_educativo AS CAR  " &
                                                    "LEFT OUTER JOIN  " &
                                                    "      SIG_T.dbo.t_corr_municipio_sace AS MUN  " &
                                                    "      ON CAR.cod_mun_edu_car_cen_edu = MUN.cod_sac_mun_sac  " &
                                                    "LEFT OUTER JOIN  " &
                                                    "      SIG_T.dbo.t_corr_departamento_sace AS DEP  " &
                                                    "      ON MUN.cod_sac_dep_sac = DEP.cod_sac_dep_sac  " &
                                                    "LEFT OUTER JOIN  " &
                                                    "      SIG_T.dbo.t_corr_aldea_sace AS ALD  " &
                                                    "      ON CAR.cod_ald_cen_edu_car_cen_edu = ALD.cod_sac_ald_sac  " &
                                                    "LEFT OUTER JOIN  " &
                                                    "      SIG_T.dbo.t_corr_caserio_sace AS CAS  " &
                                                    "      ON CAR.cod_cas_cen_edu_car_cen_edu = CAS.cod_sac_cas_sac  " &
                                                    "LEFT OUTER JOIN  " &
                                                    "      SIG_T.dbo.t_corr_barrio_sace AS BAR  " &
                                                    "      ON car.cod_bar_cen_edu_car_cen_edu = BAR.cod_sac_bar_sac " &
                                                    "LEFT OUTER JOIN  " &
                                                    "      SIG_T.dbo.t_corr_cambio_centro_educativo AS CAM  " &
                                                    "      ON CAR.cod_car_cen_edu = CAM.cod_car_cen_edu  " &
                                                    "WHERE   " &
                                                    "      CAR.cod_log_car = {0} ", intCodLog)
                        Case 8
                            strQuery = String.Format("SELECT " &
                                                    "	DEP.cod_departamento, " &
                                                    "	DEP.desc_departamento, " &
                                                    "	MUN.desc_municipio, " &
                                                    "	ALD.desc_aldea, " &
                                                    "	CAS.cod_caserio, " &
                                                    "	CAS.desc_caserio, " &
                                                    "	COUNT(CASE WHEN MAT.cod_det_mat IS NOT NULL THEN BEN.per_rup_persona ELSE NULL END) AS con_matricula,  " &
                                                    "	COUNT(CASE WHEN MAT.cod_det_mat IS NULL THEN BEN.per_rup_persona ELSE NULL END) AS sin_matricula,  " &
                                                    "	SUM(CASE WHEN MAT.cod_est_mat = 1 THEN 1 ELSE 0 END) AS est_activa, " &
                                                    "	SUM(CASE WHEN MAT.cod_est_mat = 2 THEN 1 ELSE 0 END) AS est_cancelada, " &
                                                    "	SUM(CASE WHEN MAT.cod_est_mat = 3 THEN 1 ELSE 0 END) AS est_traslado, " &
                                                    "	SUM(CASE WHEN MAT.cod_est_mat = 4 THEN 1 ELSE 0 END) AS est_trasladada, " &
                                                    "	SUM(CASE WHEN MAT.cod_est_mat = 5 THEN 1 ELSE 0 END) AS est_ingreso_tralado " &
                                                    "FROM " &
                                                    "	( " &
                                                    "		SELECT " &
                                                    "			BEN.per_persona, " &
                                                    "			BEN.per_rup_persona, " &
                                                    "			BEN.per_ciclo, " &
                                                    "			BEN.per_sexo, " &
                                                    "			HOG.hog_hogar, " &
                                                    "			HOG.hog_caserio " &
                                                    "		FROM " &
                                                    "			SIG_T.dbo.t_ben_personas AS BEN " &
                                                    "		INNER JOIN " &
                                                    "			SIG_T.dbo.t_ben_hogares AS HOG " &
                                                    "			ON BEN.per_hogar = HOG.hog_hogar " &
                                                    "		WHERE " &
                                                    "			BEN.per_estado = 1 " &
                                                    "			AND HOG.hog_estado = 1 " &
                                                    "			AND BEN.per_titular = 0 " &
                                                    "			AND BEN.per_ciclo BETWEEN 1 AND 6 " &
                                                    "	) AS BEN " &
                                                    "INNER JOIN " &
                                                    "	SIG_T.dbo.t_glo_caserios AS CAS " &
                                                    "	ON BEN.hog_caserio = CAS.cod_caserio " &
                                                    "INNER JOIN " &
                                                    "	SIG_T.dbo.t_glo_aldeas AS ALD " &
                                                    "	ON CAS.cod_aldea = ALD.cod_aldea " &
                                                    "INNER JOIN " &
                                                    "	SIG_T.dbo.t_glo_municipios AS MUN " &
                                                    "	ON ALD.cod_municipio = MUN.cod_municipio " &
                                                    "INNER JOIN " &
                                                    "	SIG_T.dbo.t_glo_departamentos AS DEP " &
                                                    "	ON MUN.cod_departamento = DEP.cod_departamento " &
                                                    "LEFT OUTER JOIN " &
                                                    "	SIG_T.dbo.t_corr_personas_sace AS PER " &
                                                    "	ON BEN.per_rup_persona = PER.cod_rup_per_sac " &
                                                    "LEFT OUTER JOIN " &
                                                    "	( " &
                                                    "		SELECT " &
                                                    "			MAT.cod_det_mat, " &
                                                    "			MAT.cod_per_sac, " &
                                                    "			MAT.cod_est_mat " &
                                                    "		FROM " &
                                                    "			SIG_T.dbo.t_corr_detalle_matriculas AS MAT " &
                                                    "		INNER JOIN " &
                                                    "			( " &
                                                    "				SELECT " &
                                                    "					MAT.cod_per_sac, " &
                                                    "					MAT.ano_mat_det_mat, " &
                                                    "					MAX(MAT.cod_sac_mat_det_mat) cod_sac_mat_det_mat " &
                                                    "				FROM " &
                                                    "					[SIG_T].[dbo].[t_corr_detalle_matriculas] AS MAT " &
                                                    "				INNER JOIN " &
                                                    "					SIG_R.dbo.t_corr_carga_matricula_educacion AS CAR " &
                                                    "					ON MAT.cod_sac_mat_det_mat = CAR.cod_sac_mat_edu " &
                                                    "				WHERE " &
                                                    "					CAR.cod_log_car = {0} " &
                                                    "					AND CAR.cod_err_car_mat_edu = 0 " &
                                                    "				GROUP BY " &
                                                    "					MAT.cod_per_sac, " &
                                                    "					MAT.ano_mat_det_mat " &
                                                    "			) AS MAT_MAX " &
                                                    "			ON MAT.cod_sac_mat_det_mat = MAT_MAX.cod_sac_mat_det_mat " &
                                                    "	)AS MAT " &
                                                    "	ON PER.cod_per_sac = MAT.cod_per_sac " &
                                                    "GROUP BY " &
                                                    "	DEP.cod_departamento, " &
                                                    "	DEP.desc_departamento, " &
                                                    "	MUN.cod_municipio, " &
                                                    "	MUN.desc_municipio, " &
                                                    "	ALD.cod_aldea, " &
                                                    "	ALD.desc_aldea, " &
                                                    "	CAS.cod_caserio, " &
                                                    "	CAS.desc_caserio ", intCodLog)
                        Case 9
                            strQuery = String.Format("SET DATEFORMAT ymd; " &
                                                    "SELECT  " &
                                                    "      CAR.cod_car_par,  " &
                                                    "      CAR.cod_par_sac_car_par, " &
                                                    "      CASE WHEN ISDATE(CAR.fec_ini_par_car_par) = 1 THEN CONVERT(NVARCHAR(30), YEAR(CONVERT(DATE, CAR.fec_ini_par_car_par))) ELSE 'Sin datos' END año_parcial, " &
                                                    "      CAR.nom_par_car_par,  " &
                                                    "      CAR.fec_ini_par_car_par,  " &
                                                    "      CAR.fec_fin_par_car_par,  " &
                                                    "      CASE WHEN CEN.cod_cen_edu IS NULL THEN 'Sin Datos' ELSE CEN.cod_cen_edu END  AS cod_cen_edu, " &
                                                    "      CASE WHEN CEN.nom_cen_edu IS NULL THEN 'Sin Datos' ELSE CEN.nom_cen_edu END AS nom_cen_edu, " &
                                                    "      CASE WHEN CAM.cod_tip_cam IS NULL THEN 4 ELSE CAM.cod_tip_cam END AS cod_pro_regis " &
                                                    "FROM  " &
                                                    "      SIG_R.dbo.t_corr_carga_parciales AS CAR  " &
                                                    "LEFT OUTER JOIN  " &
                                                    "      SIG_T.dbo.t_corr_centro_educativo AS CEN " &
                                                    "        ON CAR.cod_cen_edu_car_par = CEN.cod_sac_cen_edu " &
                                                    "LEFT OUTER JOIN  " &
                                                    "      SIG_T.dbo.t_corr_cambio_parciales AS CAM  " &
                                                    "      ON CAR.cod_car_par = CAM.cod_car_par  " &
                                                    "WHERE  " &
                                                    "      CAR.cod_log_car = {0} ", intCodLog)
                        Case 10
                            strQuery = String.Format("SELECT " &
                                                    "	DEP.cod_departamento, " &
                                                    "	DEP.desc_departamento, " &
                                                    "	MUN.cod_municipio, " &
                                                    "	MUN.desc_municipio, " &
                                                    "	ALD.cod_aldea, " &
                                                    "	ALD.desc_aldea, " &
                                                    "	CAS.cod_caserio, " &
                                                    "	CAS.desc_caserio, " &
                                                    "	COUNT(DISTINCT CASE WHEN MAT.cod_ina_sac_car_ina IS NOT NULL THEN BEN.per_rup_persona ELSE NULL END) AS con_asistencia, " &
                                                    "	COUNT(DISTINCT CASE WHEN MAT.cod_ina_sac_car_ina IS NULL THEN BEN.per_rup_persona ELSE NULL END) AS sin_asistencia " &
                                                    "FROM " &
                                                    "	( " &
                                                    "		SELECT " &
                                                    "			BEN.per_persona, " &
                                                    "			BEN.per_rup_persona, " &
                                                    "			BEN.per_ciclo, " &
                                                    "			BEN.per_sexo, " &
                                                    "			HOG.hog_hogar, " &
                                                    "			HOG.hog_caserio " &
                                                    "		FROM " &
                                                    "			SIG_T.dbo.t_ben_personas AS BEN " &
                                                    "		INNER JOIN " &
                                                    "			SIG_T.dbo.t_ben_hogares AS HOG " &
                                                    "			ON BEN.per_hogar = HOG.hog_hogar " &
                                                    "		WHERE " &
                                                    "			BEN.per_estado = 1 " &
                                                    "			AND HOG.hog_estado = 1 " &
                                                    "			AND BEN.per_titular = 0 " &
                                                    "			AND BEN.per_ciclo BETWEEN 1 AND 6 " &
                                                    "	) AS BEN " &
                                                    "INNER JOIN " &
                                                    "	SIG_T.dbo.t_glo_caserios AS CAS " &
                                                    "	ON BEN.hog_caserio = CAS.cod_caserio " &
                                                    "INNER JOIN " &
                                                    "	SIG_T.dbo.t_glo_aldeas AS ALD " &
                                                    "	ON CAS.cod_aldea = ALD.cod_aldea " &
                                                    "INNER JOIN " &
                                                    "	SIG_T.dbo.t_glo_municipios AS MUN " &
                                                    "	ON ALD.cod_municipio = MUN.cod_municipio " &
                                                    "INNER JOIN " &
                                                    "	SIG_T.dbo.t_glo_departamentos AS DEP " &
                                                    "	ON MUN.cod_departamento = DEP.cod_departamento " &
                                                    "LEFT OUTER JOIN " &
                                                    "	SIG_T.dbo.t_corr_personas_sace AS PER " &
                                                    "	ON BEN.per_rup_persona = PER.cod_rup_per_sac " &
                                                    "LEFT OUTER JOIN " &
                                                    "	( " &
                                                    "		SELECT " &
                                                    "			MAT.cod_det_mat, " &
                                                    "			MAT.cod_per_sac, " &
                                                    "			ASIS.cod_ina_sac_car_ina " &
                                                    "		FROM " &
                                                    "			SIG_T.dbo.t_corr_detalle_matriculas AS MAT " &
                                                    "		INNER JOIN " &
                                                    "			( " &
                                                    "				SELECT  " &
                                                    "					MAT.cod_per_sac, " &
                                                    "					MAT.ano_mat_det_mat, " &
                                                    "					MAX(MAT.cod_sac_mat_det_mat) cod_sac_mat_det_mat " &
                                                    "				FROM  " &
                                                    "					[SIG_T].[dbo].[t_corr_detalle_matriculas] AS MAT " &
                                                    "				GROUP BY  " &
                                                    "					MAT.cod_per_sac, " &
                                                    "					MAT.ano_mat_det_mat			 " &
                                                    "			) AS MAT_MAX " &
                                                    "			ON MAT.cod_sac_mat_det_mat = MAT_MAX.cod_sac_mat_det_mat " &
                                                    "		INNER JOIN " &
                                                    "			( " &
                                                    "				SELECT  " &
                                                    "					ASIS.cod_det_mat, " &
                                                    "					MAX(CONVERT(INT, CAR.cod_ina_sac_car_ina)) AS cod_ina_sac_car_ina " &
                                                    "				FROM " &
                                                    "					SIG_T.dbo.t_corr_detalle_inasistencias AS ASIS " &
                                                    "				INNER JOIN " &
                                                    "					SIG_R.dbo.t_corr_carga_inasistencias AS CAR " &
                                                    "					ON ASIS.cod_ina_sac_det_ina = CAR.cod_ina_sac_car_ina " &
                                                    "				WHERE " &
                                                    "					CAR.cod_log_car = {0} " &
                                                    "				GROUP BY " &
                                                    "					ASIS.cod_det_mat " &
                                                    "			) AS ASIS " &
                                                    "			ON MAT.cod_det_mat = ASIS.cod_det_mat " &
                                                    "	)AS MAT " &
                                                    "	ON PER.cod_per_sac = MAT.cod_per_sac " &
                                                    "GROUP BY  " &
                                                    "	DEP.cod_departamento, " &
                                                    "	DEP.desc_departamento, " &
                                                    "	MUN.cod_municipio, " &
                                                    "	MUN.desc_municipio, " &
                                                    "	ALD.cod_aldea, " &
                                                    "	ALD.desc_aldea, " &
                                                    "	CAS.cod_caserio, " &
                                                    "	CAS.desc_caserio ", intCodLog)
                        Case 11
                            strQuery = String.Format("SELECT  " &
                                                    "      CAR.cod_car_pro_mat, " &
                                                    "      CAR.cod_mat_sac_pro_mat_car_pro_mat, " &
                                                    "      CAR.cod_mat_sac_pro_mat_car_pro_mat, " &
                                                    "      CASE WHEN MAT.ano_mat_det_mat IS NULL THEN 'Sin Datos' ELSE CONVERT(NVARCHAR(30), MAT.ano_mat_det_mat) END AS ano_mat_det_mat, " &
                                                    "      CASE WHEN CAR.cod_est_pro_car_pro_mat IS NULL OR CAR.cod_est_pro_car_pro_mat = '' THEN -1 ELSE CAR.cod_est_pro_car_pro_mat END AS cod_est_pro, " &
                                                    "      CASE WHEN CAM.cod_tip_cam IS NULL THEN 4 ELSE CAM.cod_tip_cam END AS cod_pro_regis " &
                                                    "FROM " &
                                                    "      SIG_R.dbo.t_corr_carga_promocion_matricula AS CAR " &
                                                    "LEFT OUTER JOIN " &
                                                    "      SIG_T.dbo.t_corr_detalle_matriculas AS MAT " &
                                                    "      ON CAR.cod_mat_sac_pro_mat_car_pro_mat = MAT.cod_sac_mat_det_mat " &
                                                    "LEFT OUTER JOIN " &
                                                    "      SIG_T.dbo.t_corr_cambio_promocion_matricula AS CAM " &
                                                    "      ON CAR.cod_car_pro_mat = CAM.cod_car_pro_mat " &
                                                    "WHERE  " &
                                                    "      CAR.cod_log_car = {0} ", intCodLog)
                        Case 12
                            strQuery = String.Format("SELECT " &
                                                    "      CAR.cod_car_sub_niv_cen_edu, " &
                                                    "      CASE WHEN CEN.cod_cen_edu IS NULL THEN 'Sin Datos' ELSE CEN.cod_cen_edu END AS cod_cen_edu, " &
                                                    "      CASE WHEN CEN.nom_cen_edu IS NULL THEN 'Sin Datos' ELSE CEN.nom_cen_edu END AS nom_cen_edu, " &
                                                    "      CAR.cod_sac_sub_niv_cen_edu_car_sub_niv_cen_edu, " &
                                                    "      CASE WHEN SUB_NIV.cod_sac_niv_edu IS NULL THEN 0 ELSE SUB_NIV.cod_sac_niv_edu END AS cod_sac_niv_edu, " &
                                                    "      CASE WHEN SUB_NIV.des_sub_niv IS NULL THEN 'Sin Datos' ELSE SUB_NIV.des_sub_niv END AS des_sub_niv, " &
                                                    "      CASE WHEN CAM.cod_tip_cam IS NULL THEN 4 ELSE CAM.cod_tip_cam END AS cod_pro_regis " &
                                                    "FROM " &
                                                    "      SIG_R.dbo.t_corr_carga_subnivel_centro_educativo AS CAR " &
                                                    "LEFT OUTER JOIN " &
                                                    "      ( " &
                                                    "            SELECT " &
                                                    "                  NIV.cod_sac_niv_edu, " &
                                                    "                  SUB_NIV.cod_sac_sub_niv, " &
                                                    "                  SUB_NIV.des_sub_niv " &
                                                    "            FROM " &
                                                    "                  SIG_T.dbo.t_corr_niveles_educativos AS NIV " &
                                                    "            INNER JOIN " &
                                                    "                  SIG_T.dbo.t_corr_sub_niveles AS SUB_NIV " &
                                                    "                  ON NIV.cod_sac_niv_edu = SUB_NIV.cod_sac_niv_edu " &
                                                    "      )AS SUB_NIV " &
                                                    "      ON CAR.cod_sac_sub_niv_car_sub_niv_edu = SUB_NIV.cod_sac_sub_niv  " &
                                                    "LEFT OUTER JOIN " &
                                                    "      SIG_T.dbo.t_corr_centro_educativo AS CEN " &
                                                    "      ON CAR.cod_sac_cen_edu_car_sub_niv_edu = CEN.cod_sac_cen_edu " &
                                                    "LEFT OUTER JOIN " &
                                                    "      SIG_T.dbo.t_corr_cambio_subnivel_centro_educativo AS CAM " &
                                                    "      ON CAR.cod_car_sub_niv_cen_edu = CAM.cod_car_sub_niv_cen_edu " &
                                                    "WHERE " &
                                                    "      CAR.cod_log_car = {0} ", intCodLog)
                    End Select
                    Exit Select
                Case 2
                    Select Case intCodTipRegis
                        Case 1
                            strQuery = String.Format("SELECT  " &
                                                    "      CAR.cod_dep AS cod_departamento,  " &
                                                    "      DEP.desc_departamento AS desc_departamento,  " &
                                                    "      MUN.desc_municipio,  " &
                                                    "      ALD.desc_aldea,  " &
                                                    "      CAR.cod_case AS cod_caserio,  " &
                                                    "      CAS.desc_caserio,  " &
                                                    "      CAR.cod_rup_cen_car_cen_sal AS cod_renpi,  " &
                                                    "      CAR.nom_cen_sal_car_cen_sal AS nom_centro, " &
                                                    "        CASE " &
                                                    "            WHEN CAR.cod_pro_car_cen_sal = 1 THEN 'Nuevo Registro' " &
                                                    "            WHEN CAR.cod_pro_car_cen_sal = 2 THEN 'Actualización' " &
                                                    "            WHEN CAR.cod_pro_car_cen_sal = 3 THEN 'Ya existe' " &
                                                    "            WHEN CAR.cod_pro_car_cen_sal = 4 THEN 'Contiene errores' " &
                                                    "            WHEN CAR.cod_pro_car_cen_sal IS NULL THEN 'Sin Proceso' " &
                                                    "        END AS proceso " &
                                                    "FROM  " &
                                                    "      (  " &
                                                    "            SELECT  " &
                                                    "                  cod_dep_car_cen_sal AS cod_dep,  " &
                                                    "                  cod_dep_car_cen_sal + cod_mun_car_cen_sal AS cod_mun,  " &
                                                    "                  cod_dep_car_cen_sal + cod_mun_car_cen_sal + cod_ald_car_cen_sal AS cod_alde,  " &
                                                    "                  cod_dep_car_cen_sal + cod_mun_car_cen_sal + cod_ald_car_cen_sal + cod_cas_car_cen_sal AS cod_case,  " &
                                                    "                  cod_rup_cen_car_cen_sal,  " &
                                                    "                  nom_cen_sal_car_cen_sal,  " &
                                                    "                  cod_pro_car_cen_sal  " &
                                                    "            FROM  " &
                                                    "                  SIG_R.dbo.t_corr_carga_centro_salud  " &
                                                    "            WHERE  " &
                                                    "                  cod_log_car_sal =  {0} " &
                                                    "      ) AS CAR  " &
                                                    "LEFT OUTER JOIN  " &
                                                    "      SIG_T.dbo.t_glo_departamentos AS DEP  " &
                                                    "      ON CAR.cod_dep = DEP.cod_departamento  " &
                                                    "LEFT OUTER JOIN  " &
                                                    "      SIG_T.dbo.t_glo_municipios AS MUN  " &
                                                    "      ON CAR.cod_mun = MUN.cod_municipio  " &
                                                    "LEFT OUTER JOIN  " &
                                                    "      SIG_T.dbo.t_glo_aldeas AS ALD  " &
                                                    "      ON CAR.cod_alde = ALD.cod_aldea  " &
                                                    "LEFT OUTER JOIN  " &
                                                    "      SIG_T.dbo.t_glo_caserios AS CAS  " &
                                                    "      ON CAR.cod_case = CAS.cod_caserio ", intCodLog)
                        Case 2
                            strQuery = String.Format("SELECT " &
                                                    "      DEP.cod_departamento, " &
                                                    "      DEP.desc_departamento, " &
                                                    "      MUN.cod_municipio, " &
                                                    "      MUN.desc_municipio, " &
                                                    "      ALD.cod_aldea, " &
                                                    "      ALD.desc_aldea, " &
                                                    "      CAS.cod_caserio, " &
                                                    "      CAS.desc_caserio, " &
                                                    "      COUNT(CASE WHEN VIS.cod_vis_sal IS NOT NULL THEN VIS.cod_vis_sal ELSE NULL END) AS can_vis, " &
                                                    "      COUNT(CASE WHEN VIS.cod_tip_cam = 3 THEN VIS.cod_vis_sal ELSE NULL END) AS can_vis_anu, " &
                                                    "      COUNT(DISTINCT CASE WHEN VIS.cod_vis_sal IS NOT NULL THEN BEN.per_rup_persona ELSE NULL END) AS con_vis, " &
                                                    "      COUNT(DISTINCT CASE WHEN VIS.cod_vis_sal IS NULL THEN BEN.per_rup_persona ELSE NULL END) AS sin_vis " &
                                                    "FROM " &
                                                    "      ( " &
                                                    "            SELECT " &
                                                    "                  BEN.per_persona, " &
                                                    "                  BEN.per_rup_persona, " &
                                                    "                  BEN.per_ciclo, " &
                                                    "                  BEN.per_sexo, " &
                                                    "                  HOG.hog_hogar, " &
                                                    "                  HOG.hog_caserio " &
                                                    "            FROM " &
                                                    "                  SIG_T.dbo.t_ben_personas AS BEN " &
                                                    "            INNER JOIN " &
                                                    "                  SIG_T.dbo.t_ben_hogares AS HOG " &
                                                    "                  ON BEN.per_hogar = HOG.hog_hogar " &
                                                    "            WHERE " &
                                                    "                  BEN.per_estado = 1 " &
                                                    "                  AND HOG.hog_estado = 1 " &
                                                    "                  AND BEN.per_titular = 0 " &
                                                    "                  AND BEN.per_ciclo BETWEEN 1 AND 6 " &
                                                    "      ) AS BEN " &
                                                    "INNER JOIN " &
                                                    "      SIG_T.dbo.t_glo_caserios AS CAS " &
                                                    "      ON BEN.hog_caserio = CAS.cod_caserio " &
                                                    "INNER JOIN " &
                                                    "      SIG_T.dbo.t_glo_aldeas AS ALD " &
                                                    "      ON CAS.cod_aldea = ALD.cod_aldea " &
                                                    "INNER JOIN " &
                                                    "      SIG_T.dbo.t_glo_municipios AS MUN " &
                                                    "      ON ALD.cod_municipio = MUN.cod_municipio " &
                                                    "INNER JOIN " &
                                                    "      SIG_T.dbo.t_glo_departamentos AS DEP " &
                                                    "      ON MUN.cod_departamento = DEP.cod_departamento " &
                                                    "LEFT OUTER JOIN " &
                                                    "      ( " &
                                                    "            SELECT " &
                                                    "                  CAM.cod_vis_sal, " &
                                                    "                  VIS.cod_rup_per, " &
                                                    "                  CAM.cod_tip_cam " &
                                                    "            FROM " &
                                                    "                  SIG_T.dbo.t_corr_visita_salud AS VIS " &
                                                    "            INNER JOIN " &
                                                    "                  ( " &
                                                    "                        SELECT " &
                                                    "                              cod_vis_sal, " &
                                                    "                              cod_car_vis_sal, " &
                                                    "                              cod_tip_cam " &
                                                    "                        FROM " &
                                                    "                              SIG_T.dbo.t_corr_cambio_visita_salud " &
                                                    "                        WHERE " &
                                                    "                              cod_log_car_sal = {0} " &
                                                    "                  ) AS CAM " &
                                                    "                  ON VIS.cod_vis_sal = CAM.cod_vis_sal " &
                                                    "      ) AS VIS " &
                                                    "      ON BEN.per_rup_persona = VIS.cod_rup_per " &
                                                    "GROUP BY  " &
                                                    "      DEP.cod_departamento, " &
                                                    "      DEP.desc_departamento, " &
                                                    "      MUN.cod_municipio, " &
                                                    "      MUN.desc_municipio, " &
                                                    "      ALD.cod_aldea, " &
                                                    "      ALD.desc_aldea, " &
                                                    "      CAS.cod_caserio, " &
                                                    "      CAS.desc_caserio ", intCodLog)

                    End Select
            End Select

            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function


        Public Function fnc_obtener_tipo_proceso_car_cen_sal() As DataTable
            objTabla = New DataTable

            objTabla.Columns.Add("cod_proceso", GetType(Integer))
            objTabla.Columns.Add("nom_proceso", GetType(String))


            For i = 1 To arrStrTipoProceCentSal.Length
                objTabla.Rows.Add(i, arrStrTipoProceCentSal(i - 1))
            Next

            Return objTabla
        End Function

        Public Function fnc_obtener_proceso_registro() As DataTable
            objTabla = New DataTable

            objTabla.Columns.Add("cod_pro_regis", GetType(Integer))
            objTabla.Columns.Add("nom_pro_regis", GetType(String))

            objTabla.Rows.Add(1, "Nuevo")
            objTabla.Rows.Add(2, "Actualización")
            objTabla.Rows.Add(4, "Sin proceso")

            Return objTabla
        End Function

        Public Function fnc_obtener_funcionamiento() As DataTable
            strQuery = "SELECT " &
                        "      cod_fun_cen_edu, " &
                        "      desc_fun_cen_edu " &
                        "FROM " &
                        "      SIG_T.dbo.t_corr_funcionamiento_centro_edu "

            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objFila = objTabla.NewRow()
            objFila.Item("cod_fun_cen_edu") = 0
            objFila.Item("desc_fun_cen_edu") = "Sin Datos"
            objTabla.Rows.InsertAt(objFila, 0)

            Return objTabla
        End Function

        Public Function fnc_obtener_tipo_administracion() As DataTable
            strQuery = "SELECT " &
                        "      cod_adm_cen, " &
                        "      nom_adm_cen " &
                        "FROM " &
                        "      SIG_T.dbo.t_corr_admin_centro "

            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objFila = objTabla.NewRow()
            objFila.Item("cod_adm_cen") = 0
            objFila.Item("nom_adm_cen") = "Sin Datos"
            objTabla.Rows.InsertAt(objFila, 0)

            Return objTabla
        End Function

        Public Function fnc_obtener_tipo_zona() As DataTable
            strQuery = "SELECT " &
                        "      cod_tip_zon, " &
                        "      nom_tip_zon " &
                        "FROM " &
                        "      SIG_T.dbo.t_corr_tipo_zona "

            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objFila = objTabla.NewRow()
            objFila.Item("cod_tip_zon") = 0
            objFila.Item("nom_tip_zon") = "Sin Datos"
            objTabla.Rows.InsertAt(objFila, 0)

            Return objTabla
        End Function

        Public Function fnc_obtener_periodo_escolar() As DataTable
            strQuery = "SELECT  " &
                        "      cod_per_esc,  " &
                        "      des_per_esc  " &
                        "FROM  " &
                        "      SIG_T.dbo.t_corr_periodo_escolar "

            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objFila = objTabla.NewRow()
            objFila.Item("cod_per_esc") = 0
            objFila.Item("des_per_esc") = "Sin Datos"
            objTabla.Rows.InsertAt(objFila, 0)

            Return objTabla
        End Function

        Public Function fnc_obtener_pais_fronterizo() As DataTable
            strQuery = "SELECT " &
                        "      cod_sac_pai_fro, " &
                        "      des_pai_fro " &
                        "FROM " &
                        "      SIG_T.dbo.t_corr_pais_fronterizo "

            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objFila = objTabla.NewRow()
            objFila.Item("cod_sac_pai_fro") = 0
            objFila.Item("des_pai_fro") = "Sin Datos"
            objTabla.Rows.InsertAt(objFila, 0)

            Return objTabla
        End Function

        Public Function fnc_obtener_etnia() As DataTable
            strQuery = "SELECT " &
                        "      cod_etn, " &
                        "      des_etn " &
                        "FROM " &
                        "      SIG_T.dbo.t_corr_etnias "

            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objFila = objTabla.NewRow()
            objFila.Item("cod_etn") = 0
            objFila.Item("des_etn") = "Sin Datos"
            objTabla.Rows.InsertAt(objFila, 0)

            Return objTabla
        End Function

        Public Function fnc_obtener_estado_promocion() As DataTable
            strQuery = "SELECT " &
                        "      cod_est_pro, " &
                        "      desc_est_pro " &
                        "FROM " &
                        "      SIG_T.dbo.t_corr_estado_promocion "

            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objFila = objTabla.NewRow()
            objFila.Item("cod_est_pro") = -1
            objFila.Item("desc_est_pro") = "Sin Datos"
            objTabla.Rows.InsertAt(objFila, 3)

            Return objTabla
        End Function

        Public Function fnc_obtener_nivel_educativo() As DataTable
            strQuery = "SELECT " &
                        "      cod_sac_niv_edu, " &
                        "      des_niv_edu " &
                        "FROM " &
                        "      SIG_T.dbo.t_corr_niveles_educativos "

            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objFila = objTabla.NewRow()
            objFila.Item("cod_sac_niv_edu") = 0
            objFila.Item("des_niv_edu") = "Sin Datos"
            objTabla.Rows.InsertAt(objFila, 0)

            Return objTabla
        End Function
    End Class
End Namespace
