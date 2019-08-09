Namespace SIG.Areas.Corresponsabilidad.Models
    Public Class cl_report_busq_corres_benef
        Inherits cl_report_CAI

        Private objConexionDB As New SIG.Areas.Corresponsabilidad.Models.cl_conexion_db
        Private strQuery As String
        Private objTabla As DataTable
        Private arrObjeto As Array

        Public Function fnc_obtener_result_benef(ByRef intCodBenef As Integer,
                                                 ByRef intCodRUPBenef As Integer,
                                                 ByRef strIdentiBenef As String,
                                                 ByRef strNom1Benef As String,
                                                 ByRef strNom2Benef As String,
                                                 ByRef strApe1Benef As String,
                                                 ByRef strApe2Benef As String,
                                                 ByRef strIdentiTit As String,
                                                 ByVal intCodHogRUP As Integer,
                                                 ByVal intCodHogSIG As Integer) As DataTable

            Dim strWhere As String
            If intCodBenef = 0 And intCodRUPBenef = 0 And strIdentiBenef = "null" And strNom1Benef = "null" And strNom2Benef = "null" _
                And strApe1Benef = "null" And strApe2Benef = "null" And strIdentiTit = "null" And intCodHogRUP = 0 And intCodHogSIG = 0 Then
                strWhere = "WHERE " &
                           "	BEN.per_persona = 0 "
            Else
                strWhere = String.Format("WHERE " &
                                        "	BEN.per_persona = {0} " &
                                        "	OR BEN.per_rup_persona = {1} " &
                                        "	OR BEN.per_identidad LIKE '%{2}%' " &
                                        "	OR BEN.per_nombre1 LIKE '%{3}%' " &
                                        "	OR BEN.per_nombre2 LIKE '%{4}%' " &
                                        "	OR BEN.per_apellido1 LIKE '%{5}%' " &
                                        "	OR BEN.per_apellido2 LIKE '%{6}%' " &
                                        "	OR TIT.per_identidad LIKE '%{7}%' " &
                                        "   OR HOG.hog_rup_hogar = {8} " &
                                        "   OR HOG.hog_hogar = {9} ", intCodBenef, intCodRUPBenef, strIdentiBenef, strNom1Benef, strNom2Benef, strApe1Benef, strApe2Benef, strIdentiTit, intCodHogRUP, intCodHogSIG)
            End If

            strQuery = String.Format("SELECT " &
                                    "	UBI.desc_departamento, " &
                                    "	UBI.desc_municipio, " &
                                    "	UBI.desc_aldea, " &
                                    "	UBI.desc_caserio, " &
                                    "	TIT.per_identidad AS tit_identidad, " &
                                    "	TIT.per_nombre1 AS tit_nombre1, " &
                                    "	TIT.per_nombre2 AS tit_nombre2, " &
                                    "	TIT.per_apellido1 AS tit_apellido1, " &
                                    "	TIT.per_apellido2 AS tit_apellido2, " &
                                    "	BEN.per_persona, " &
                                    "	BEN.per_rup_persona, " &
                                    "	BEN.per_identidad, " &
                                    "	BEN.per_nombre1, " &
                                    "	BEN.per_nombre2, " &
                                    "	BEN.per_apellido1, " &
                                    "	BEN.per_apellido2, " &
                                    "	BEN.per_sexo AS hab_sexo, " &
                                    "	BEN.per_ciclo AS cod_ciclo_edad, " &
                                    "	BEN.per_fch_nacimiento " &
                                    "FROM " &
                                    "	SIG_T.dbo.t_ben_hogares AS HOG " &
                                    "INNER JOIN " &
                                    "	SIG_T.dbo.t_ben_personas AS TIT " &
                                    "	ON HOG.hog_hogar = TIT.per_hogar " &
                                    "	AND TIT.per_titular = 1 " &
                                    "INNER JOIN " &
                                    "	SIG_T.dbo.t_ben_personas AS BEN " &
                                    "	ON HOG.hog_hogar = BEN.per_hogar " &
                                    "	AND BEN.per_titular = 0 " &
                                    "INNER JOIN " &
                                    "	SIG_T.dbo.V_Glo_caserios AS UBI " &
                                    "	ON HOG.hog_caserio = UBI.cod_caserio " &
                                    "{0} ", strWhere)

            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_info_beneficiario(ByRef intCodBenef As Integer) As Array
            strQuery = String.Format("SELECT " &
                                    "	CASE WHEN BEN.per_identidad IS NULL OR BEN.per_identidad = '' THEN 'Sin Datos' ELSE BEN.per_identidad END AS per_identidad, " &
                                    "	CASE WHEN BEN.per_nombre1 IS NULL OR BEN.per_nombre1 = '' THEN 'Sin Datos' ELSE BEN.per_nombre1 END AS per_nombre1, " &
                                    "	CASE WHEN BEN.per_nombre2 IS NULL OR BEN.per_nombre2 = '' THEN 'Sin Datos' ELSE BEN.per_nombre2 END AS per_nombre2,  " &
                                    "	CASE WHEN BEN.per_apellido1 IS NULL OR BEN.per_apellido1 = '' THEN 'Sin Datos' ELSE BEN.per_apellido1 END AS per_apellido1, " &
                                    "	CASE WHEN BEN.per_apellido2 IS NULL OR BEN.per_apellido2 = '' THEN 'Sin Datos' ELSE BEN.per_apellido2 END AS per_apellido2, " &
                                    "	CASE WHEN BEN.per_edad IS NULL THEN 'Sin Datos' ELSE CONVERT(NVARCHAR(200), BEN.per_edad) END AS per_edad, " &
                                    "	CASE  " &
                                    "		WHEN BEN.per_ciclo = 0 THEN 'Ciclo 0 (mayor de 18 años)' " &
                                    "		WHEN BEN.per_ciclo = 1 THEN 'Ciclo 1 (0, 1, 2, 3 y 4 años)'  " &
                                    "		WHEN BEN.per_ciclo = 2 THEN 'Ciclo 2 (5 y 6 años)'  " &
                                    "		WHEN BEN.per_ciclo = 3 THEN 'Ciclo 3 (7, 8, 9, 10 y 11 años)'  " &
                                    "		WHEN BEN.per_ciclo = 4 THEN 'Ciclo 4 (12 años)'  " &
                                    "		WHEN BEN.per_ciclo = 5 THEN 'Ciclo 5 (13, 14 y 15 años)' " &
                                    "		ELSE " &
                                    "			'Ciclo 6 (16 y 17 años)' " &
                                    "	END AS per_ciclo, " &
                                    "	CASE WHEN BEN.per_fch_nacimiento IS NULL THEN 'Sin Datos' ELSE CONVERT(NVARCHAR(200), BEN.per_fch_nacimiento) END AS per_fch_nacimiento, " &
                                    "	CASE  " &
                                    "		WHEN BEN.per_sexo = 1 THEN 'M'  " &
                                    "		WHEN BEN.per_sexo IS NULL THEN 'Sin Datos'  " &
                                    "		ELSE 'F'  " &
                                    "	END AS per_sexo, " &
                                    "	EST.per_estado_descripcion, " &
                                    "	BEN.per_hogar, " &
                                    "   ISNULL(BEN.per_nombre1, '') + ' ' + ISNULL(BEN.per_nombre2, '') + ' ' + ISNULL(BEN.per_apellido1, '') + ' ' + ISNULL(BEN.per_apellido2, '') AS nom_completo " &
                                    "FROM " &
                                    "	SIG_T.dbo.t_ben_personas AS BEN " &
                                    "INNER JOIN " &
                                    "	SIG_T.dbo.t_per_estados AS EST " &
                                    "	ON BEN.per_estado = EST.per_estado " &
                                    "WHERE " &
                                    "	BEN.per_persona = {0} ", intCodBenef)

            Return objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 12)
        End Function

        Function fnc_obtener_info_titular(ByRef intCodHogar As Integer) As Array
            strQuery = String.Format("SELECT " &
                                    "   TIT.per_persona, " &
                                    "	CASE WHEN TIT.per_identidad IS NULL OR TIT.per_identidad = '' THEN 'Sin Datos' ELSE TIT.per_identidad END AS per_identidad, " &
                                    "	CASE WHEN TIT.per_nombre1 IS NULL OR TIT.per_nombre1 = '' THEN 'Sin Datos' ELSE TIT.per_nombre1 END AS per_nombre1, " &
                                    "	CASE WHEN TIT.per_nombre2 IS NULL OR TIT.per_nombre2 = '' THEN 'Sin Datos' ELSE TIT.per_nombre2 END AS per_nombre2,  " &
                                    "	CASE WHEN TIT.per_apellido1 IS NULL OR TIT.per_apellido1 = '' THEN 'Sin Datos' ELSE TIT.per_apellido1 END AS per_apellido1, " &
                                    "	CASE WHEN TIT.per_apellido2 IS NULL OR TIT.per_apellido2 = '' THEN 'Sin Datos' ELSE TIT.per_apellido2 END AS per_apellido2, " &
                                    "	CASE WHEN TIT.per_edad IS NULL THEN 'Sin Datos' ELSE CONVERT(NVARCHAR(200), TIT.per_edad) END AS per_edad, " &
                                    "	CASE WHEN TIT.per_fch_nacimiento IS NULL THEN 'Sin Datos' ELSE CONVERT(NVARCHAR(200), TIT.per_fch_nacimiento) END AS per_fch_nacimiento, " &
                                    "	CASE  " &
                                    "		WHEN TIT.per_sexo = 1 THEN 'M'  " &
                                    "		WHEN TIT.per_sexo IS NULL THEN 'Sin Datos'  " &
                                    "		ELSE 'F'  " &
                                    "	END AS per_sexo, " &
                                    "	EST.per_estado_descripcion " &
                                    "FROM " &
                                    "	SIG_T.dbo.t_ben_personas AS TIT " &
                                    "INNER JOIN " &
                                    "	SIG_T.dbo.t_per_estados AS EST " &
                                    "	ON TIT.per_estado = EST.per_estado " &
                                    "WHERE " &
                                    "	TIT.per_titular = 1 " &
                                    "	AND TIT.per_hogar = {0} ", intCodHogar)

            Return objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 10)
        End Function

        Function fnc_obtener_info_hogar(ByRef intCodHogar As Integer) As Array
            strQuery = String.Format("SELECT " &
                                    "	HOG.hog_hogar, " &
                                    "	UBI.desc_departamento, " &
                                    "	UBI.desc_municipio, " &
                                    "	UBI.desc_aldea, " &
                                    "	UBI.desc_caserio, " &
                                    "	CASE  WHEN HOG.hogar_direccion IS NULL OR HOG.hogar_direccion = '' THEN 'Sin Datos' ELSE HOG.hogar_direccion END AS hogar_direccion, " &
                                    "	HOG.hog_umbral, " &
                                    "	EST.hog_estado_descripcion " &
                                    "FROM " &
                                    "	SIG_T.dbo.t_ben_hogares AS HOG " &
                                    "INNER JOIN " &
                                    "	SIG_T.dbo.t_hog_estados AS EST " &
                                    "	ON HOG.hog_estado = EST.hog_estado " &
                                    "INNER JOIN " &
                                    "	SIG_T.dbo.V_Glo_caserios AS UBI " &
                                    "	ON HOG.hog_caserio = UBI.cod_caserio " &
                                    "WHERE " &
                                    "	HOG.hog_hogar = {0} ", intCodHogar)

            Return objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 8)
        End Function

        Function fnc_obtener_corres_educacion(ByRef intCodBenef As Integer) As DataTable
            strQuery = String.Format("SELECT   " &
                                    "   CASE WHEN CORR_EDU.cod_det_mat IS NULL THEN 'Sin Datos' ELSE CONVERT(NVARCHAR(200), CORR_EDU.cod_det_mat) END AS cod_det_mat,   " &
                                    "   CASE WHEN CORR_EDU.ano_mat_det_mat IS NULL THEN 'Sin Datos' ELSE CONVERT(NVARCHAR(200), CORR_EDU.ano_mat_det_mat) END AS ano_mat_det_mat,   " &
                                    "   CASE WHEN CORR_EDU.nom_gra IS NULL THEN 'Sin Datos' ELSE CORR_EDU.nom_gra END AS nom_gra ,   " &
                                    "   CASE WHEN CORR_EDU.nom_est_mat IS NULL THEN 'Sin Datos' ELSE CORR_EDU.nom_est_mat END AS nom_est_mat,   " &
                                    "   CASE WHEN CORR_EDU.nom_raz_can IS NULL THEN 'Sin Datos' ELSE CORR_EDU.nom_raz_can END AS nom_raz_can,   " &
                                    "   CASE WHEN CORR_EDU.cod_cen_edu IS NULL THEN 'Sin Datos' ELSE CORR_EDU.cod_cen_edu END AS cod_cen_edu,    " &
                                    "   CASE WHEN CORR_EDU.nom_cen_edu IS NULL THEN 'Sin Datos' ELSE CORR_EDU.nom_cen_edu END AS nom_cen_edu,   " &
                                    "   CASE WHEN CORR_EDU.nom_dep_sac IS NULL THEN 'Sin Datos' ELSE CORR_EDU.nom_dep_sac END AS nom_dep_sac,   " &
                                    "   CASE WHEN CORR_EDU.nom_mun_sac IS NULL THEN 'Sin Datos' ELSE CORR_EDU.nom_mun_sac END AS nom_mun_sac,   " &
                                    "   CASE WHEN CORR_EDU.nom_ald_sac IS NULL THEN 'Sin Datos' ELSE CORR_EDU.nom_ald_sac END AS nom_ald_sac,   " &
                                    "   CASE WHEN CORR_EDU.nom_cas_sac IS NULL THEN 'Sin Datos' ELSE CORR_EDU.nom_cas_sac END AS nom_cas_sac,   " &
                                    "   CASE WHEN CORR_EDU.nom_bar_sac IS NULL THEN 'Sin Datos' ELSE CORR_EDU.nom_bar_sac END AS nom_bar_sac,   " &
                                    "   CASE WHEN CORR_EDU.dia_ina_det_ina IS NULL THEN 'Sin Datos' ELSE CONVERT(NVARCHAR(200), CORR_EDU.dia_ina_det_ina) END AS dia_ina_det_ina_1,                " &
                                    "   CASE WHEN CORR_EDU.dia_ina_det_ina_2 IS NULL THEN 'Sin Datos' ELSE CONVERT(NVARCHAR(200), CORR_EDU.dia_ina_det_ina_2) END AS dia_ina_det_ina_2, " &
                                    "   CASE WHEN CORR_EDU.desc_est_pro IS NULL THEN 'Sin Datos' ELSE CORR_EDU.desc_est_pro END AS desc_est_pro " &
                                    "FROM   " &
                                    "   SIG_T.dbo.t_ben_personas AS BEN   " &
                                    "LEFT OUTER JOIN    " &
                                    "    (    " &
                                    "        SELECT   " &
                                    "           PER.cod_rup_per_sac,   " &
                                    "           MAT.cod_det_mat,   " &
                                    "           MAT.ano_mat_det_mat,   " &
                                    "           GRA.nom_gra,   " &
                                    "           EST.nom_est_mat,   " &
                                    "           RAZ.nom_raz_can,   " &
                                    "           CEN.cod_cen_edu,    " &
                                    "           CEN.nom_cen_edu,   " &
                                    "           CEN.nom_dep_sac,   " &
                                    "           CEN.nom_mun_sac,   " &
                                    "           CEN.nom_ald_sac,   " &
                                    "           CEN.nom_cas_sac,   " &
                                    "           CEN.nom_bar_sac,               " &
                                    "           ASIS_1.dia_ina_det_ina,  " &
                                    "           ASIS_2.dia_ina_det_ina AS dia_ina_det_ina_2, " &
                                    "               PRO.desc_est_pro         " &
                                    "        FROM    " &
                                    "            SIG_T.dbo.t_corr_detalle_matriculas AS MAT   " &
                                    "       INNER JOIN   " &
                                    "           SIG_T.dbo.t_corr_grados AS GRA   " &
                                    "           ON MAT.cod_gra = GRA.cod_gra   " &
                                    "       INNER JOIN   " &
                                    "           SIG_T.dbo.t_corr_estados_matricula As EST   " &
                                    "           ON MAT.cod_est_mat = EST.cod_est_mat   " &
                                    "       LEFT OUTER JOIN   " &
                                    "           SIG_T.dbo.t_corr_razones_cancelacion AS RAZ   " &
                                    "           ON MAT.cod_raz_can = RAZ.cod_raz_can   " &
                                    "        INNER JOIN    " &
                                    "            SIG_T.dbo.t_corr_personas_sace AS PER    " &
                                    "            ON MAT.cod_per_sac = PER.cod_per_sac    " &
                                    "            AND PER.cod_rup_per_sac IS NOT NULL    " &
                                    "        INNER JOIN   " &
                                    "            (   " &
                                    "                        SELECT   " &
                                    "                              DEP.nom_dep_sac,   " &
                                    "                              MUN.nom_mun_sac,   " &
                                    "                              ALD.nom_ald_sac,   " &
                                    "                              CAS.nom_cas_sac,   " &
                                    "                              BAR.nom_bar_sac,   " &
                                    "                              CEN.cod_sac_cen_edu,   " &
                                    "                              CEN.cod_cen_edu,   " &
                                    "                              CEN.nom_cen_edu   " &
                                    "                        FROM   " &
                                    "                              SIG_T.dbo.t_corr_centro_educativo AS CEN   " &
                                    "                        LEFT OUTER JOIN   " &
                                    "                              SIG_T.dbo.t_corr_municipio_sace AS MUN   " &
                                    "                              ON CEN.cod_sac_mun_sac = MUN.cod_sac_mun_sac   " &
                                    "                        INNER JOIN   " &
                                    "                              SIG_T.dbo.t_corr_departamento_sace AS DEP   " &
                                    "                              ON MUN.cod_sac_dep_sac = DEP.cod_sac_dep_sac   " &
                                    "                        LEFT OUTER JOIN   " &
                                    "                              SIG_T.dbo.t_corr_aldea_sace AS ALD   " &
                                    "                              ON CEN.cod_sac_ald_sac = ALD.cod_sac_ald_sac   " &
                                    "                        LEFT OUTER JOIN   " &
                                    "                              SIG_T.dbo.t_corr_caserio_sace AS CAS   " &
                                    "                              ON CEN.cod_sac_cas_sac = CAS.cod_sac_cas_sac   " &
                                    "                        LEFT OUTER JOIN   " &
                                    "                              SIG_T.dbo.t_corr_barrio_sace AS BAR   " &
                                    "                              ON CEN.cod_sac_bar_sac = BAR.cod_sac_bar_sac   " &
                                    "            )AS CEN   " &
                                    "                  ON MAT.cod_sac_cen_edu = CEN.cod_sac_cen_edu   " &
                                    "        LEFT OUTER JOIN   " &
                                    "            (    " &
                                    "                SELECT     " &
                                    "                    ASIS.cod_det_mat,  " &
                                    "                    SUM(ASIS.dia_ina_det_ina) AS dia_ina_det_ina  " &
                                    "                FROM    " &
                                    "                    SIG_T.dbo.t_corr_detalle_inasistencias AS ASIS  " &
                                    "                INNER JOIN  " &
                                    "                    SIG_T.dbo.t_corr_parciales AS PAR  " &
                                    "                    ON ASIS.cod_par = PAR.cod_par  " &
                                    "                        WHERE  " &
                                    "                              PAR.nom_par IN ('PARCIAL I', 'PARCIAL II')  " &
                                    "                        GROUP BY  " &
                                    "                              ASIS.cod_det_mat  " &
                                    "                      " &
                                    "            ) AS ASIS_1  " &
                                    "            ON MAT.cod_det_mat = ASIS_1.cod_det_mat  " &
                                    "        LEFT OUTER JOIN   " &
                                    "            (    " &
                                    "                SELECT     " &
                                    "                    ASIS.cod_det_mat,  " &
                                    "                    SUM(ASIS.dia_ina_det_ina) AS dia_ina_det_ina  " &
                                    "                FROM    " &
                                    "                    SIG_T.dbo.t_corr_detalle_inasistencias AS ASIS  " &
                                    "                INNER JOIN  " &
                                    "                    SIG_T.dbo.t_corr_parciales AS PAR  " &
                                    "                    ON ASIS.cod_par = PAR.cod_par  " &
                                    "                WHERE  " &
                                    "                    PAR.nom_par IN ('PARCIAL III', 'PARCIAL IV')  " &
                                    "                GROUP BY  " &
                                    "                    ASIS.cod_det_mat                      " &
                                    "            ) AS ASIS_2  " &
                                    "                  ON MAT.cod_det_mat = ASIS_2.cod_det_mat " &
                                    "            LEFT OUTER JOIN " &
                                    "                  ( " &
                                    "                        SELECT " &
                                    "                              PRO.cod_det_mat, " &
                                    "                              EST.desc_est_pro " &
                                    "                        FROM " &
                                    "                              SIG_T.dbo.t_corr_promocion_matricula AS PRO " &
                                    "                        INNER JOIN " &
                                    "                              SIG_T.dbo.t_corr_estado_promocion AS EST " &
                                    "                              ON PRO.cod_est_pro = EST.cod_est_pro " &
                                    "                  ) AS PRO " &
                                    "                  ON MAT.cod_det_mat = PRO.cod_det_mat " &
                                    "    ) AS CORR_EDU   " &
                                    "   ON BEN.per_rup_persona = CORR_EDU.cod_rup_per_sac   " &
                                    "WHERE   " &
                                    "   BEN.per_persona = {0} ", intCodBenef)

            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_corres_salud(ByRef intCodBenef As Integer) As DataTable
            strQuery = String.Format("SELECT " &
                                    "      CASE WHEN CORR_SAL.cod_vis_sal IS NULL THEN 'Sin Datos' ELSE CORR_SAL.cod_vis_sal END AS cod_vis_sal, " &
                                    "      CASE WHEN CORR_SAL.fec_rea_vis_sal IS NULL THEN 'Sin Datos' ELSE CONVERT(NVARCHAR(30), CORR_SAL.fec_rea_vis_sal, 103) END AS fec_rea_vis_sal, " &
                                    "      CASE WHEN CORR_SAL.fec_reg_vis_sal IS NULL THEN 'Sin Datos'  ELSE CONVERT(NVARCHAR(30),CORR_SAL.fec_reg_vis_sal, 103) END AS fec_reg_vis_sal, " &
                                    "      CASE WHEN CORR_SAL.cod_cen_sal IS NULL THEN 'Sin Datos' ELSE CORR_SAL.cod_cen_sal END AS cod_cen_sal, " &
                                    "      CASE WHEN CORR_SAL.cod_rup_cen_sal IS NULL THEN 'Sin Datos' ELSE CORR_SAL.cod_rup_cen_sal END AS cod_rup_cen_sal, " &
                                    "      CASE WHEN CORR_SAL.nom_cen_sal IS NULL THEN 'SIn Datos' ELSE CORR_SAL.nom_cen_sal END AS nom_cen_sal, " &
                                    "      CASE WHEN CORR_SAL.desc_departamento  IS NULL THEN 'Sin Datos' ELSE CORR_SAL.desc_departamento END AS desc_departamento, " &
                                    "      CASE WHEN CORR_SAL.desc_municipio IS NULL THEN 'Sin Datos' ELSE CORR_SAL.desc_municipio END AS desc_municipio, " &
                                    "      CASE WHEN CORR_SAL.desc_aldea IS NULL THEN 'Sin Datos' ELSE CORR_SAL.desc_aldea END AS desc_aldea, " &
                                    "      CASE WHEN CORR_SAL.desc_caserio IS NULL THEN 'Sin Datos' ELSE CORR_SAL.desc_caserio END AS desc_caserio " &
                                    "FROM " &
                                    "      SIG_T.dbo.t_ben_personas AS BEN " &
                                    "LEFT OUTER JOIN " &
                                    "      ( " &
                                    "            SELECT " &
                                    "                  VIS.cod_vis_sal, " &
                                    "                  VIS.cod_rup_per, " &
                                    "                  VIS.fec_rea_vis_sal, " &
                                    "                  VIS.fec_reg_vis_sal, " &
                                    "                  CEN.cod_cen_sal, " &
                                    "                  CEN.cod_rup_cen_sal, " &
                                    "                  CEN.nom_cen_sal, " &
                                    "                  DEP.desc_departamento, " &
                                    "                  MUN.desc_municipio, " &
                                    "                  ALD.desc_aldea, " &
                                    "                  CAS.desc_caserio " &
                                    "            FROM " &
                                    "                  [SIG_T].[dbo].[t_corr_visita_salud] AS VIS " &
                                    "            INNER JOIN " &
                                    "                  [SIG_T].[dbo].[t_corr_centro_salud] AS CEN " &
                                    "                  ON VIS.cod_cen_sal = CEN.cod_cen_sal " &
                                    "            LEFT OUTER JOIN " &
                                    "                  SIG_T.dbo.t_glo_departamentos AS DEP " &
                                    "                  ON CEN.cod_dep_cen_sal = DEP.cod_departamento " &
                                    "            LEFT OUTER JOIN " &
                                    "                  SIG_T.dbo.t_glo_municipios AS MUN " &
                                    "                  ON CEN.cod_mun_cen_sal = MUN.cod_municipio " &
                                    "            LEFT OUTER JOIN " &
                                    "                  SIG_T.dbo.t_glo_aldeas AS ALD " &
                                    "                  ON CEN.cod_al_cen_sal = ALD.cod_aldea " &
                                    "            LEFT OUTER JOIN " &
                                    "                  SIG_T.dbo.t_glo_caserios AS CAS " &
                                    "                  ON CEN.cod_cas_cen_sal = CAS.cod_caserio " &
                                    "      ) AS CORR_SAL " &
                                    "    ON BEN.per_rup_persona = CORR_SAL.cod_rup_per " &
                                    "WHERE " &
                                    "      BEN.per_persona = {0} ", intCodBenef)
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function




        Function fnc_dibujar_table(ByRef singleX As Single, ByRef singleY As Single, ByRef intTipoTable As Integer) As DevExpress.XtraReports.UI.XRTable
            Dim objXRTable As New DevExpress.XtraReports.UI.XRTable
            Dim objXRFila As New DevExpress.XtraReports.UI.XRTableRow()
            Dim objXRCeldaEncabezado As New DevExpress.XtraReports.UI.XRTableCell()
            Dim strTituTabla() As String = {"Corresponsabilidad de Educación (Matricula)", "Corresponsabilidad de Educación (Centro Educativo)", "Corresponsabilidad de Educación (Asistencia)"}

            objXRTable.LocationFloat = New DevExpress.Utils.PointFloat(singleX, singleY)
            objXRTable.SizeF = New System.Drawing.SizeF(661.3074!, 25.0!)

            objXRCeldaEncabezado.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold)
            objXRCeldaEncabezado.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            objXRCeldaEncabezado.BackColor = System.Drawing.SystemColors.ScrollBar

            Select Case intTipoTable
                Case 1
                    objXRCeldaEncabezado.Text = strTituTabla(0)
                Case 2
                    objXRCeldaEncabezado.Text = strTituTabla(1)
                Case Else
                    objXRCeldaEncabezado.Text = strTituTabla(2)
            End Select

            objXRFila.Cells.Add(objXRCeldaEncabezado)
            objXRTable.Rows.Add(objXRFila)

            Return objXRTable
        End Function


        Function fnc_crear_encabe_matri_corr_edu() As DevExpress.XtraReports.UI.XRTableRow
            Dim objXRFila As New DevExpress.XtraReports.UI.XRTableRow()

            For i = 1 To 6
                Dim objXRCeldaEncabezado As New DevExpress.XtraReports.UI.XRTableCell()

                objXRCeldaEncabezado.BackColor = System.Drawing.SystemColors.ScrollBar
                objXRCeldaEncabezado.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold)
                objXRCeldaEncabezado.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
                objXRCeldaEncabezado.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)

                Select Case i
                    Case 1
                        objXRCeldaEncabezado.Text = "Matricula"
                    Case 2
                        objXRCeldaEncabezado.Text = "Año"
                    Case 3
                        objXRCeldaEncabezado.Text = "Estado"
                    Case 4
                        objXRCeldaEncabezado.Text = "Grado"
                    Case 5
                        objXRCeldaEncabezado.Text = "Razón de Cancelación"
                    Case 6
                        objXRCeldaEncabezado.Text = "Promoción"
                End Select

                objXRFila.Cells.Add(objXRCeldaEncabezado)
            Next

            Return objXRFila
        End Function

        Function fnc_crear_fila_data_matri_corr_edu(ByRef strcodMat As String,
                                                    ByRef strAño As String,
                                                    ByRef strEst As String,
                                                    ByRef strRazon As String,
                                                    ByRef strGrado As String,
                                                    ByRef strPro As String) As DevExpress.XtraReports.UI.XRTableRow
            Dim objXRFila As New DevExpress.XtraReports.UI.XRTableRow()

            For i = 1 To 6
                Dim objXRCeldaValor As New DevExpress.XtraReports.UI.XRTableCell()
                objXRCeldaValor.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular)
                objXRCeldaValor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter

                Select Case i
                    Case 1
                        objXRCeldaValor.Text = strcodMat
                    Case 2
                        objXRCeldaValor.Text = strAño
                    Case 3
                        objXRCeldaValor.Text = strEst
                    Case 4
                        objXRCeldaValor.Text = strGrado
                    Case 5
                        objXRCeldaValor.Text = strRazon
                    Case 6
                        objXRCeldaValor.Text = strPro
                End Select
                objXRFila.Cells.Add(objXRCeldaValor)
            Next
            Return objXRFila
        End Function


        Function fnc_crear_encabe_centro_corr_edu() As DevExpress.XtraReports.UI.XRTableRow
            Dim objXRFila As New DevExpress.XtraReports.UI.XRTableRow()

            For i = 1 To 8
                Dim objXRCeldaEncabezado As New DevExpress.XtraReports.UI.XRTableCell()

                objXRCeldaEncabezado.BackColor = System.Drawing.SystemColors.ScrollBar
                objXRCeldaEncabezado.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold)
                objXRCeldaEncabezado.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
                objXRCeldaEncabezado.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)

                Select Case i
                    Case 1
                        objXRCeldaEncabezado.Text = "Matricula"
                    Case 2
                        objXRCeldaEncabezado.Text = "Código Centro"
                    Case 3
                        objXRCeldaEncabezado.Text = "Centro"
                    Case 4
                        objXRCeldaEncabezado.Text = "Departamento"
                    Case 5
                        objXRCeldaEncabezado.Text = "Municipio"
                    Case 6
                        objXRCeldaEncabezado.Text = "Aldea"
                    Case 7
                        objXRCeldaEncabezado.Text = "Caserio"
                    Case 8
                        objXRCeldaEncabezado.Text = "Barrio"
                End Select

                objXRFila.Cells.Add(objXRCeldaEncabezado)
            Next

            Return objXRFila
        End Function

        Function fnc_crear_fila_data_centro_corr_edu(ByRef strcodMat As String,
                                                    ByRef strCodCentro As String,
                                                    ByRef strCentro As String,
                                                    ByRef strDep As String,
                                                    ByRef strMuni As String,
                                                    ByRef strAld As String,
                                                    ByRef strCas As String,
                                                    ByRef strBar As String) As DevExpress.XtraReports.UI.XRTableRow
            Dim objXRFila As New DevExpress.XtraReports.UI.XRTableRow()

            For i = 1 To 8
                Dim objXRCeldaValor As New DevExpress.XtraReports.UI.XRTableCell()
                objXRCeldaValor.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular)
                objXRCeldaValor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter

                Select Case i
                    Case 1
                        objXRCeldaValor.Text = strcodMat
                    Case 2
                        objXRCeldaValor.Text = strCodCentro
                    Case 3
                        objXRCeldaValor.Text = strCentro
                    Case 4
                        objXRCeldaValor.Text = strDep
                    Case 5
                        objXRCeldaValor.Text = strMuni
                    Case 6
                        objXRCeldaValor.Text = strAld
                    Case 7
                        objXRCeldaValor.Text = strCas
                    Case 8
                        objXRCeldaValor.Text = strBar
                End Select
                objXRFila.Cells.Add(objXRCeldaValor)
            Next
            Return objXRFila
        End Function




        'Fila con los registros de la asistencia.
        Function fnc_crear_encabe_asis_corr_edu() As DevExpress.XtraReports.UI.XRTableRow
            Dim objXRFila As New DevExpress.XtraReports.UI.XRTableRow()

            For i = 1 To 4
                Dim objXRCeldaEncabezado As New DevExpress.XtraReports.UI.XRTableCell()

                objXRCeldaEncabezado.BackColor = System.Drawing.SystemColors.ScrollBar
                objXRCeldaEncabezado.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold)
                objXRCeldaEncabezado.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
                objXRCeldaEncabezado.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)

                Select Case i
                    Case 1
                        objXRCeldaEncabezado.Text = "Matricula"
                    Case 2
                        objXRCeldaEncabezado.Text = "Año"
                    Case 3
                        objXRCeldaEncabezado.Text = "Asistencia I (días inasistidos)"
                    Case 4
                        objXRCeldaEncabezado.Text = "Asistencia II (días inasistidos)"
                End Select

                objXRFila.Cells.Add(objXRCeldaEncabezado)
            Next

            Return objXRFila
        End Function

        Function fnc_crear_fila_asis_corr_edu(ByRef strcodMat As String, ByRef strAño As String,
                                              ByRef strPer1NumDia As String, ByRef strPer2NumDia As String) As DevExpress.XtraReports.UI.XRTableRow
            Dim objXRFila As New DevExpress.XtraReports.UI.XRTableRow()

            For i = 1 To 4
                Dim objXRCeldaValor As New DevExpress.XtraReports.UI.XRTableCell()
                objXRCeldaValor.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular)
                objXRCeldaValor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter

                Select Case i
                    Case 1
                        objXRCeldaValor.Text = strcodMat
                    Case 2
                        objXRCeldaValor.Text = strAño
                    Case 3
                        objXRCeldaValor.Text = strPer1NumDia
                    Case 4
                        objXRCeldaValor.Text = strPer2NumDia
                End Select
                objXRFila.Cells.Add(objXRCeldaValor)
            Next
            Return objXRFila
        End Function


        Function fnc_crea_encabe_centro_corr_sal() As DevExpress.XtraReports.UI.XRTableRow
            Dim objXRFila As New DevExpress.XtraReports.UI.XRTableRow()

            For i = 1 To 7
                Dim objXRCeldaEncabezado As New DevExpress.XtraReports.UI.XRTableCell()

                objXRCeldaEncabezado.BackColor = System.Drawing.SystemColors.ScrollBar
                objXRCeldaEncabezado.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold)
                objXRCeldaEncabezado.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
                objXRCeldaEncabezado.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)

                Select Case i
                    Case 1
                        objXRCeldaEncabezado.Text = "Código SIG"
                    Case 2
                        objXRCeldaEncabezado.Text = "Código RENPI"
                    Case 3
                        objXRCeldaEncabezado.Text = "Centro"
                    Case 4
                        objXRCeldaEncabezado.Text = "Departamento"
                    Case 5
                        objXRCeldaEncabezado.Text = "Municipio"
                    Case 6
                        objXRCeldaEncabezado.Text = "Aldea"
                    Case 7
                        objXRCeldaEncabezado.Text = "Caserio"
                End Select

                objXRFila.Cells.Add(objXRCeldaEncabezado)
            Next

            Return objXRFila
        End Function

        Function fnc_crea_data_centro_corr_sal(ByRef strCodCent As String,
                                                ByRef strCodRUPCent As String,
                                                ByRef strNomCent As String,
                                                ByRef strDep As String,
                                                ByRef strMun As String,
                                                ByRef strAld As String,
                                                ByRef strCas As String) As DevExpress.XtraReports.UI.XRTableRow
            Dim objXRFila As New DevExpress.XtraReports.UI.XRTableRow()

            For i = 1 To 7
                Dim objXRCeldaValor As New DevExpress.XtraReports.UI.XRTableCell()
                objXRCeldaValor.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular)
                objXRCeldaValor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter

                Select Case i
                    Case 1
                        objXRCeldaValor.Text = strCodCent
                    Case 2
                        objXRCeldaValor.Text = strCodRUPCent
                    Case 3
                        objXRCeldaValor.Text = strNomCent
                    Case 4
                        objXRCeldaValor.Text = strDep
                    Case 5
                        objXRCeldaValor.Text = strMun
                    Case 6
                        objXRCeldaValor.Text = strAld
                    Case 7
                        objXRCeldaValor.Text = strCas
                End Select

                objXRFila.Cells.Add(objXRCeldaValor)
            Next
            Return objXRFila
        End Function


        Function fnc_crea_encabe_visi_corr_sal(ByRef intCodTipCell As Integer) As DevExpress.XtraReports.UI.XRTableRow
            Dim objXRFila As New DevExpress.XtraReports.UI.XRTableRow()

            Select Case intCodTipCell
                Case 1
                    Dim objXRCeldaEncabezado As New DevExpress.XtraReports.UI.XRTableCell()

                    objXRCeldaEncabezado.BackColor = System.Drawing.SystemColors.ScrollBar
                    objXRCeldaEncabezado.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold)
                    objXRCeldaEncabezado.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
                    objXRCeldaEncabezado.Text = "Visitas Médicas"
                    objXRFila.Cells.Add(objXRCeldaEncabezado)
                Case 2
                    For i = 1 To 3
                        Dim objXRCeldaEncabezado As New DevExpress.XtraReports.UI.XRTableCell()

                        objXRCeldaEncabezado.BackColor = System.Drawing.SystemColors.ScrollBar
                        objXRCeldaEncabezado.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold)
                        objXRCeldaEncabezado.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter

                        Select Case i
                            Case 1
                                objXRCeldaEncabezado.Text = "Código Visita"
                            Case 2
                                objXRCeldaEncabezado.Text = "Fecha Visita"
                            Case 3
                                objXRCeldaEncabezado.Text = "Fecha Registro RENPI"
                        End Select
                        objXRFila.Cells.Add(objXRCeldaEncabezado)
                    Next
            End Select

            Return objXRFila
        End Function

        Function fnc_crear_fila_data_centro_corr_sal(
                                                    ByRef strcodVis As String,
                                                    ByRef strFechVisi As String,
                                                    ByRef strFechRegis As String) As DevExpress.XtraReports.UI.XRTableRow
            Dim objXRFila As New DevExpress.XtraReports.UI.XRTableRow()
            For i = 1 To 3
                Dim objXRCeldaValor As New DevExpress.XtraReports.UI.XRTableCell()
                objXRCeldaValor.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular)
                objXRCeldaValor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter

                Select Case i
                    Case 1
                        objXRCeldaValor.Text = strcodVis
                    Case 2
                        objXRCeldaValor.Text = strFechVisi
                    Case 3
                        objXRCeldaValor.Text = strFechRegis
                End Select
                objXRFila.Cells.Add(objXRCeldaValor)
            Next
            Return objXRFila
        End Function
    End Class
End Namespace
