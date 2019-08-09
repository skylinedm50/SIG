Namespace SIG.Areas.Corresponsabilidad.Models
    Public Class cl_report_CAI
        Inherits cl_report_carg_edu_sal

        Private objConexionDB As New SIG.Areas.Corresponsabilidad.Models.cl_conexion_db
        Private strQuery As String
        Private objTabla As DataTable
        Dim objFila As DataRow
        Private arrObjeto As Array
        Private arrStrSexo() As String = {"Sin datos", "M", "F"}
        Private arrStrEstProgra() As String = {"No programado", "Programado"}
        Private arrStrEstCobro() As String = {"No cobró", "Si cobró"}
        Private arrStrCicloEdad() As String = {"Ciclo 0 (mayor de 18 años)", "Ciclo 1 (0, 1, 2, 3 y 4 años)", "Ciclo 2 (5 y 6 años)", "Ciclo 3 (7, 8, 9, 10 y 11 años)", "Ciclo 4 (12 años)", "Ciclo 5 (13, 14 y 15 años)", "Ciclo 6 (16 y 17 años)"}
        Private arrStrCicloPlani() As String = {"Ciclo 1 salud", "Ciclo 1 y 2 educación", "Ciclo 3 educación"}
        Private arrStrTipBusqueda() As String = {"Por planilla.", "Estado actual."}


        Public Function fnc_obetner_CAI(ByRef intCodTipBusq As Integer, ByRef intAño As Integer, ByRef strCodPago As String, ByRef strCodCorres As String,
                                        ByRef strCodDep As String, ByRef strCodMun As String, ByRef strCodAld As String, ByRef strCodCas As String) As DataTable
            Dim strWhere As String
            Dim strWhereUbica As String = ""

            If strCodCas <> "-1" And strCodCas <> "00" Then
                strWhereUbica = String.Format("AND UBI.cod_caserio = '{0}'", strCodCas)
            Else
                If strCodAld <> "-1" And strCodAld <> "00" Then
                    strWhereUbica = String.Format("AND UBI.cod_aldea = '{0}'", strCodAld)
                Else
                    If strCodMun <> "-1" And strCodMun <> "00" Then
                        strWhereUbica = String.Format("AND UBI.cod_municipio = '{0}'", strCodMun)
                    Else
                        If strCodDep <> "-1" And strCodDep <> "00" Then
                            strWhereUbica = String.Format("AND UBI.cod_departamento = '{0}'", strCodDep)
                        End If
                    End If
                End If
            End If




            If intCodTipBusq = 0 Then
                strWhere = String.Format("PAG.pag_codigo IN({0}) ", strCodPago)

                strQuery = String.Format("SELECT " &
                                        "      TIT.tit_codigo, " &
                                        "      UBI.desc_departamento, " &
                                        "      UBI.desc_municipio, " &
                                        "      UBI.desc_aldea, " &
                                        "      UBI.desc_caserio, " &
                                        "      TIT.tit_planilla, " &
                                        "      TIT.tit_hogar, " &
                                        "      TIT.tit_proy_corta, " &
                                        "      TIT.tit_identidad, " &
                                        "      TIT.tit_nombre1, " &
                                        "      TIT.tit_nombre2, " &
                                        "      TIT.tit_apellido1, " &
                                        "      TIT.tit_apellido2, " &
                                        "      HOG.hog_rup_hogar, " &
                                        "      TIT.tit_cobro, " &
                                        "      TIT.tit_monto_total_red, " &
                                        "      HABI.hab_identidad, " &
                                        "      HABI.hab_per_persona, " &
                                        "      HABI.hab_nombre1, " &
                                        "      HABI.hab_nombre2, " &
                                        "      HABI.hab_apellido1, " &
                                        "      HABI.hab_apellido2, " &
                                        "      HABI.hab_ciclo_eleg AS cod_ciclo_edad, " &
                                        "      PAG.pag_nombre, " &
                                        "      ISNULL(HABI.hab_sexo, 0) AS hab_sexo, " &
                                        "      CASE WHEN HABI_CORR.habc_clase_corresp = 1 AND COM.comp_codigo = 2 THEN 'Si' ELSE 'No' END AS cumple_salud, " &
                                        "      CASE WHEN HABI_CORR.habc_clase_corresp = 1 AND COM.comp_codigo = 2 AND HABI.hab_ciclo_eleg = 1 THEN 'Si' ELSE 'No' END AS cumple_salud_ciclo_1, " &
                                        "      CASE WHEN HABI_CORR.habc_clase_corresp = 1 AND COM.comp_codigo = 2 AND HABI.hab_ciclo_eleg = 2 THEN 'Si' ELSE 'No' END AS cumple_salud_ciclo_2, " &
                                        "      CASE WHEN HABI_CORR.habc_clase_corresp = 1 AND COM.comp_codigo = 3 THEN 'Si' ELSE 'No' END AS cumple_educ, " &
                                        "      CASE WHEN HABI_CORR.habc_clase_corresp = 1 AND COM.comp_codigo = 3 AND HABI_CORR.habc_nivel_corresp = 2 THEN 'Si' ELSE 'No' END AS cumple_educ_ciclo_1_2, " &
                                        "      CASE WHEN HABI_CORR.habc_clase_corresp = 1 AND COM.comp_codigo = 3 AND HABI_CORR.habc_nivel_corresp = 3 THEN 'Si' ELSE 'No' END AS cumple_educ_ciclo_3, " &
                                        "      CASE WHEN HABI_CORR.habc_clase_corresp = 2 AND COM.comp_codigo = 2 AND HABI_CORR.habc_cant_incumpl = 1 AND HABI.hab_ciclo_eleg = 1 THEN 'Si' ELSE 'No' END AS aperci_salud_ciclo_1, " &
                                        "      CASE WHEN HABI_CORR.habc_clase_corresp = 2 AND COM.comp_codigo = 2 AND HABI_CORR.habc_cant_incumpl = 1 AND HABI.hab_ciclo_eleg = 2 THEN 'Si' ELSE 'No' END AS aperci_salud_ciclo_2, " &
                                        "      CASE WHEN HABI_CORR.habc_clase_corresp = 2 AND COM.comp_codigo = 3 AND HABI_CORR.habc_cant_incumpl = 1 AND HABI_CORR.habc_nivel_corresp = 2 THEN 'Si' ELSE 'No' END AS aperci_educ_ciclo_1_2, " &
                                        "      CASE WHEN HABI_CORR.habc_clase_corresp = 2 AND COM.comp_codigo = 3 AND HABI_CORR.habc_cant_incumpl = 1 AND HABI_CORR.habc_nivel_corresp = 3 THEN 'Si' ELSE 'No' END AS aperci_educ_ciclo_3, " &
                                        "      CASE WHEN  " &
                                        "            (HABI_CORR.habc_clase_corresp = 3 AND HABI.hab_nivel_eleg = 1)  " &
                                        "            OR (HABI_CORR.habc_clase_corresp = 2 AND COM.comp_codigo = 2 AND HABI_CORR.habc_cant_incumpl > 1 AND HABI.hab_ciclo_eleg = 1)  " &
                                        "      THEN 'Si' ELSE 'No' END AS no_cumple_salud_ciclo_1, " &
                                        "      CASE WHEN  " &
                                        "            (HABI_CORR.habc_clase_corresp = 3 AND HABI.hab_nivel_eleg = 1)  " &
                                        "            OR (HABI_CORR.habc_clase_corresp = 2 AND COM.comp_codigo = 2 AND HABI_CORR.habc_cant_incumpl > 1 AND HABI.hab_ciclo_eleg = 2)  " &
                                        "      THEN 'Si' ELSE 'No' END AS no_cumple_salud_ciclo_2, " &
                                        "      CASE WHEN  " &
                                        "            (HABI_CORR.habc_clase_corresp = 3 AND HABI.hab_nivel_eleg = 2 AND HABI_CORR.habc_nivel_corresp = 2)  " &
                                        "            OR (HABI_CORR.habc_clase_corresp = 2 AND COM.comp_codigo = 3 AND HABI_CORR.habc_nivel_corresp = 2 AND HABI_CORR.habc_cant_incumpl > 1)  " &
                                        "      THEN 'Si' ELSE 'No' END AS no_cumple_educ_ciclo_1_2, " &
                                        "      CASE WHEN  " &
                                        "            (HABI_CORR.habc_clase_corresp = 3 AND HABI.hab_nivel_eleg = 3 AND HABI_CORR.habc_nivel_corresp = 2)  " &
                                        "            OR (HABI_CORR.habc_clase_corresp = 2 AND COM.comp_codigo = 3 AND HABI_CORR.habc_nivel_corresp = 3 AND HABI_CORR.habc_cant_incumpl > 1)  " &
                                        "      THEN 'Si' ELSE 'No' END AS no_cumple_educ_ciclo_3, " &
                                        "      CORR.corr_codigo, " &
                                        "      CASE  " &
                                        "            WHEN COM.comp_codigo = 3 THEN CEN_EDU.cod_cen_edu " &
                                        "            WHEN COM.comp_codigo = 2 THEN CONVERT(NVARCHAR(MAX), CEN_SAL.cod_rup_cen_sal) " &
                                        "            ELSE NULL " &
                                        "      END AS cod_centro, " &
                                        "      CASE  " &
                                        "            WHEN COM.comp_codigo = 3 THEN CEN_EDU.nom_cen_edu " &
                                        "            WHEN COM.comp_codigo = 2 THEN CEN_SAL.nom_cen_sal " &
                                        "            ELSE NULL " &
                                        "      END AS nombre_centro, " &
                                        "      HABI_CORR.habc_num_visitas_cs " &
                                        "FROM " &
                                        "      SIG_T.dbo.f_pla_titulares AS TIT " &
                                        "INNER JOIN " &
                                        "      SIG_T.dbo.V_Glo_caserios AS UBI " &
                                        "      ON TIT.tit_cas_codigo = UBI.cod_caserio " &
                                        "INNER JOIN " &
                                        "      SIG_T.dbo.f_pla_esquema AS ESQ " &
                                        "      ON TIT.tit_esquema = ESQ.esq_codigo " &
                                        "INNER JOIN " &
                                        "      SIG_T.dbo.f_pla_pago AS PAG " &
                                        "      ON ESQ.pag_codigo = PAG.pag_codigo " &
                                        "INNER JOIN " &
                                        "      SIG_T.dbo.f_pla_habilitantes AS HABI " &
                                        "      ON TIT.tit_codigo = HABI.hab_tit_codigo " &
                                        "INNER JOIN " &
                                        "      SIG_T.dbo.f_pla_habilitantes_corr AS HABI_CORR " &
                                        "      ON HABI.hab_codigo = HABI_CORR.hab_codigo " &
                                        "INNER JOIN " &
                                        "      SIG_T.dbo.f_pla_corresponsabilidad AS CORR " &
                                        "      ON HABI_CORR.habc_tipo_corr = CORR.corr_codigo " &
                                        "INNER JOIN " &
                                        "      SIG_T.dbo.f_pla_tipo_componente AS COM " &
                                        "      ON CORR.comp_codigo = COM.comp_codigo " &
                                        "INNER JOIN " &
                                        "      SIG_T.dbo.t_ben_hogares AS HOG " &
                                        "      ON TIT.tit_hogar = HOG.hog_hogar " &
                                        "LEFT OUTER JOIN " &
                                        "      SIG_T.dbo.t_corr_centro_educativo AS CEN_EDU " &
                                        "      ON TIT.tit_cod_esc_cs = CEN_EDU.cod_sac_cen_edu " &
                                        "      AND COM.comp_codigo = 3 " &
                                        "LEFT OUTER JOIN " &
                                        "      SIG_T.dbo.t_corr_centro_salud AS CEN_SAL " &
                                        "      ON TIT.tit_cod_esc_cs = CEN_SAL.cod_cen_sal " &
                                        "      AND COM.comp_codigo = 2 " &
                                        "WHERE " &
                                        "   {0} " &
                                        "   {1} ", strWhere, strWhereUbica)
            Else
                Dim arrStrCodCorr() As String = Split(strCodCorres, ",")
                Dim strWhereVisMed As String
                Dim strWhereAsis As String

                Dim dateFechIniMax As DateTime = "1900-01-01"
                Dim dateFechFinMax As DateTime = "1900-01-01"
                Dim intParIniMax As Integer = 0
                Dim intParFinMax As Integer = 0
                Dim arrStrNomPar() As String = {"PARCIAL ERROR", "PARCIAL I", "PARCIAL II", "PARCIAL III", "PARCIAL IV"}
                Dim strNomPar As String = Nothing
                Dim strNombrePar1 As String = "'" & arrStrNomPar(0) & "'"
                Dim strNombrePar2 As String = "'" & arrStrNomPar(0) & "'"

                For i = 0 To arrStrCodCorr.Length - 1
                    arrObjeto = fnc_obtener_datos_corres(arrStrCodCorr(i))
                    Select Case arrObjeto(0)
                        Case 2
                            If CInt(arrObjeto(4)) < intParIniMax Or intParIniMax = 0 Then
                                intParIniMax = CInt(arrObjeto(4))
                            End If

                            If intParFinMax < CInt(arrObjeto(5)) Then
                                intParFinMax = CInt(arrObjeto(5))
                            End If
                            Exit Select
                        Case 3
                            If Convert.ToDateTime(arrObjeto(2)) < dateFechIniMax Or dateFechIniMax.Year = 1900 Then
                                dateFechIniMax = Convert.ToDateTime(arrObjeto(2))
                            End If

                            If dateFechFinMax < Convert.ToDateTime(arrObjeto(3)) Then
                                dateFechFinMax = Convert.ToDateTime(arrObjeto(3))
                            End If
                            Exit Select
                    End Select
                Next

                If dateFechIniMax.Year = 1900 And dateFechFinMax.Year = 1900 Then
                    strWhereVisMed = String.Format("AND fec_rea_vis_sal = CONVERT(DATE, '{0}') ", dateFechIniMax.ToString("yyyy-MM-dd"))
                Else
                    strWhereVisMed = String.Format("AND CONVERT(DATE, fec_rea_vis_sal) BETWEEN CONVERT(DATE, '{0}') AND CONVERT(DATE, '{1}')", dateFechIniMax.ToString("yyyy-MM-dd"), dateFechFinMax.ToString("yyyy-MM-dd"))
                End If


                If intParIniMax <> 0 Then 'Identificar que existe asistencia.
                    For i = intParIniMax To arrStrNomPar.Length - 1 'Recorres el arreglo de los nombres de los parciales.
                        If i <= intParFinMax Then
                            If i = (arrStrNomPar.Length - 1) Then
                                strNomPar += "'" & arrStrNomPar(i) & "'"
                            Else
                                strNomPar += "'" & arrStrNomPar(i) & "',"
                            End If

                            If i = 2 Then
                                strNombrePar1 = "'" & arrStrNomPar(i - 1) & ", " & arrStrNomPar(i) & "'"
                            ElseIf i = 4 Then
                                strNombrePar2 = "'" & arrStrNomPar(i - 1) & ", " & arrStrNomPar(i) & "'"
                            End If
                        End If
                    Next
                Else
                    strNomPar = "'" & arrStrNomPar(0) & "'"
                End If

                strWhereAsis = String.Format("PAR.nom_par IN ({0}) AND MAT.ano_mat_det_mat = {1}", strNomPar, intAño)

                strQuery = String.Format("SELECT " &
                                        "    UBI.desc_departamento, " &
                                        "    UBI.desc_municipio, " &
                                        "    UBI.desc_aldea, " &
                                        "    UBI.desc_caserio, " &
                                        "    HOG.hog_hogar AS tit_hogar, " &
                                        "    HOG.hog_rup_hogar, " &
                                        "    TIT.per_identidad AS tit_identidad, " &
                                        "    TIT.per_nombre1 AS tit_nombre1, " &
                                        "    TIT.per_nombre2 AS tit_nombre2, " &
                                        "    TIT.per_apellido1 AS tit_apellido1, " &
                                        "    TIT.per_apellido2 AS tit_apellido2, " &
                                        "    BEN.per_identidad AS hab_identidad, " &
                                        "    BEN.per_persona AS hab_per_persona, " &
                                        "    BEN.per_nombre1 AS hab_nombre1, " &
                                        "    BEN.per_nombre2 AS hab_nombre2, " &
                                        "    BEN.per_apellido1 AS hab_apellido1, " &
                                        "    BEN.per_apellido2 AS hab_apellido2, " &
                                        "    BEN.per_ciclo AS cod_ciclo_edad, " &
                                        "    ISNULL(BEN.per_sexo, 0) AS hab_sexo, " &
                                        "    CASE WHEN CORR_SAL.cod_vis_sal IS NOT NULL THEN 'Si' ELSE 'No' END AS cumple_salud, " &
                                        "    CASE WHEN CORR_SAL.cod_vis_sal IS NOT NULL AND BEN.per_ciclo = 1 THEN 'Si' ELSE 'No' END AS cumple_salud_ciclo_1, " &
                                        "    CASE WHEN CORR_SAL.cod_vis_sal IS NOT NULL OR BEN.per_ciclo = 2 THEN 'Si' ELSE 'No' END AS cumple_salud_ciclo_2, " &
                                        "    CASE WHEN (CORR_EDU.cod_det_ina IS NOT NULL OR CORR_EDU.cod_det_mat IS NOT NULL) AND CORR_EDU.cod_gra BETWEEN 4 AND 12 THEN 'Si' ELSE 'No' END AS cumple_educ, " &
                                        "    CASE WHEN CORR_EDU.cod_det_mat IS NOT NULL AND CORR_EDU.cod_gra BETWEEN 4 AND 12 THEN 'Si' ELSE 'No' END AS cumple_educ_mat, " &
                                        "    CASE WHEN CORR_EDU.cod_det_ina IS NOT NULL AND CORR_EDU.dia_ina_det_ina < 21 AND CORR_EDU.cod_gra BETWEEN 4 AND 12 THEN 'Si' ELSE 'No' END AS cumple_educ_ina, " &
                                        "    CASE WHEN CORR_EDU.cod_det_mat IS NOT NULL AND CORR_EDU.cod_gra BETWEEN 4 AND 9 THEN 'Si' ELSE 'No' END AS cumple_educ_mat_ciclo_1_2, " &
                                        "    CASE WHEN CORR_EDU.cod_det_mat IS NOT NULL AND CORR_EDU.cod_gra BETWEEN 10 AND 12 THEN 'Si' ELSE 'No' END AS cumple_educ_mat_ciclo_3, " &
                                        "    CASE WHEN CORR_EDU.cod_det_ina IS NOT NULL AND CORR_EDU.dia_ina_det_ina < 21 AND CORR_EDU.cod_gra BETWEEN 4 AND 9 THEN 'Si' ELSE 'No' END AS cumple_educ_ina_ciclo_1_2, " &
                                        "    CASE WHEN CORR_EDU.cod_det_ina IS NOT NULL AND CORR_EDU.dia_ina_det_ina < 21 AND CORR_EDU.cod_gra BETWEEN 10 AND 12 THEN 'Si' ELSE 'No' END AS cumple_educ_ina_ciclo_3, " &
                                        "    CASE WHEN CORR_SAL.cod_vis_sal IS NULL AND BEN.per_ciclo = 1 THEN 'Si' ELSE 'No' END AS no_cumple_salud_ciclo_1, " &
                                        "    CASE WHEN CORR_SAL.cod_vis_sal IS NULL AND BEN.per_ciclo = 2 THEN 'Si' ELSE 'No' END AS no_cumple_salud_ciclo_2, " &
                                        "    CASE WHEN (CORR_EDU.cod_det_mat IS NULL AND CORR_EDU_ANT.cod_det_mat IS NOT NULL AND (CORR_EDU_ANT.cod_gra + 1) BETWEEN 4 AND 9) OR BEN.per_ciclo = 3 THEN 'Si' ELSE 'No' END AS no_cumple_educ_ciclo_1_2, " &
                                        "    CASE WHEN (CORR_EDU.cod_det_mat IS NULL AND CORR_EDU_ANT.cod_det_mat IS NOT NULL AND (CORR_EDU_ANT.cod_gra + 1) BETWEEN 10 AND 12) OR BEN.per_ciclo BETWEEN 4 AND 5 THEN 'Si' ELSE 'No' END AS no_cumple_educ_ciclo_3, " &
                                        "    CORR_EDU.cod_cen_edu, " &
                                        "    CORR_EDU.nom_cen_edu, " &
                                        "    ISNULL(CORR_EDU.cod_gra, 0) AS cod_gra, " &
                                        "    ISNULL(CORR_EDU.dia_ina_det_ina, 0) AS dia_ina_det_ina, " &
                                        "    CORR_SAL.cod_cen_sal, " &
                                        "    CORR_SAL.cod_rup_cen_sal, " &
                                        "    CORR_SAL.nom_cen_sal, " &
                                        "    ISNULL(CORR_SAL.habc_num_visitas_cs, 0) AS habc_num_visitas_cs, " &
                                        "    CASE  " &
                                        "            WHEN CORR_EDU.cod_det_ina IS NOT NULL AND CORR_EDU.dia_ina_det_ina < 21 AND CORR_EDU.cod_gra BETWEEN 4 AND 12 AND CORR_EDU.parciales2 = 'PARCIAL III, PARCIAL IV' THEN 'ASISTENCIA 2 III-IV PARCIAL ' + CONVERT(NVARCHAR(10), CORR_EDU.ano_mat_det_mat) " &
                                        "            WHEN CORR_EDU.cod_det_ina IS NOT NULL AND CORR_EDU.dia_ina_det_ina < 21 AND CORR_EDU.cod_gra BETWEEN 4 AND 12 AND CORR_EDU.parciales1 = 'PARCIAL I, PARCIAL II' THEN 'ASISTENCIA 1 I-II PARCIAL ' + CONVERT(NVARCHAR(10), CORR_EDU.ano_mat_det_mat) " &
                                        "            WHEN CORR_EDU.cod_det_mat IS NOT NULL AND CORR_EDU.cod_gra BETWEEN 4 AND 12 THEN 'MATRICULA ' + CONVERT(NVARCHAR(10), CORR_EDU.ano_mat_det_mat) " &
                                        "            WHEN CORR_SAL.cod_vis_sal IS NOT NULL THEN 'VISITA MÉDICA' " &
                                        "            ELSE 'SIN CORRESPONSABILIDAD' " &
                                        "    END AS corresp " &
                                        "FROM " &
                                        "    SIG_T.dbo.t_ben_hogares AS HOG " &
                                        "INNER JOIN " &
                                        "    SIG_T.dbo.t_ben_personas AS BEN " &
                                        "    ON HOG.hog_hogar = BEN.per_hogar " &
                                        "    AND BEN.per_titular = 0 " &
                                        "    AND BEN.per_estado = 1       " &
                                        "INNER JOIN " &
                                        "    SIG_T.dbo.t_ben_personas AS TIT " &
                                        "    ON HOG.hog_hogar = TIT.per_hogar " &
                                        "    AND TIT.per_titular = 1 " &
                                        "    AND TIT.per_estado = 1 " &
                                        "INNER JOIN " &
                                        "    SIG_T.dbo.V_Glo_caserios AS UBI " &
                                        "    ON HOG.hog_caserio = UBI.cod_caserio " &
                                        "LEFT OUTER JOIN " &
                                        "    ( " &
                                        "        SELECT " &
                                        "            VIS.cod_vis_sal, " &
                                        "            MAX_VIS.cod_rup_per, " &
                                        "            MAX_VIS.cant_visita AS habc_num_visitas_cs, " &
                                        "            CEN.cod_cen_sal, " &
                                        "            CEN.cod_rup_cen_sal, " &
                                        "            CEN.nom_cen_sal " &
                                        "        FROM " &
                                        "            ( " &
                                        "                SELECT " &
                                        "                    cod_rup_per, " &
                                        "                    MAX(cod_vis_sal) AS cod_vis_sal, " &
                                        "                    COUNT(cod_vis_sal) AS cant_visita " &
                                        "                FROM " &
                                        "                    [SIG_T].[dbo].[t_corr_visita_salud] " &
                                        "                WHERE " &
                                        "                    est_vis_sal = 1 " &
                                        "                    {0}  " &
                                        "                GROUP BY " &
                                        "                    cod_rup_per " &
                                        "            ) AS MAX_VIS " &
                                        "        INNER JOIN " &
                                        "            [SIG_T].[dbo].[t_corr_visita_salud] AS VIS " &
                                        "            ON MAX_VIS.cod_vis_sal = VIS.cod_vis_sal " &
                                        "        INNER JOIN " &
                                        "            [SIG_T].[dbo].[t_corr_centro_salud] AS CEN " &
                                        "            ON VIS.cod_cen_sal = CEN.cod_cen_sal " &
                                        "    ) AS CORR_SAL " &
                                        "    ON BEN.per_rup_persona = CORR_SAL.cod_rup_per " &
                                        "LEFT OUTER JOIN " &
                                        "    ( " &
                                        "        SELECT " &
                                        "            MAT.cod_det_mat, " &
                                        "            CEN.cod_cen_edu, " &
                                        "            CEN.nom_cen_edu, " &
                                        "            PER.cod_rup_per_sac, " &
                                        "            ASIS.cod_det_ina, " &
                                        "            ASIS.dia_ina_det_ina, " &
                                        "                        ASIS.parciales1, " &
                                        "                        ASIS.parciales2, " &
                                        "            MAT.cod_gra, " &
                                        "                        MAT.ano_mat_det_mat " &
                                        "        FROM " &
                                        "            SIG_T.dbo.t_corr_detalle_matriculas AS MAT " &
                                        "        INNER JOIN " &
                                        "            ( " &
                                        "                SELECT " &
                                        "                    MAT.cod_per_sac, " &
                                        "                    MAX(MAT.cod_sac_mat_det_mat) cod_sac_mat_det_mat " &
                                        "                FROM " &
                                        "                    [SIG_T].[dbo].[t_corr_detalle_matriculas] AS MAT " &
                                        "                WHERE " &
                                        "                    MAT.cod_est_mat IN (1, 5) " &
                                        "                    AND MAT.ano_mat_det_mat = {1} " &
                                        "                GROUP BY " &
                                        "                        MAT.cod_per_sac " &
                                        "            ) AS MAT_MAX " &
                                        "            ON MAT.cod_sac_mat_det_mat = MAT_MAX.cod_sac_mat_det_mat " &
                                        "        INNER JOIN " &
                                        "            SIG_T.dbo.t_corr_personas_sace AS PER " &
                                        "            ON MAT.cod_per_sac = PER.cod_per_sac " &
                                        "            AND PER.cod_rup_per_sac IS NOT NULL " &
                                        "        INNER JOIN " &
                                        "            SIG_T.dbo.t_corr_centro_educativo AS CEN " &
                                        "            ON MAT.cod_sac_cen_edu = CEN.cod_sac_cen_edu " &
                                        "        LEFT OUTER JOIN " &
                                        "            ( " &
                                        "                SELECT " &
                                        "                    ASIS.cod_det_mat, " &
                                        "                    SUM(ASIS.dia_ina_det_ina) AS dia_ina_det_ina, " &
                                        "                    MAX(ASIS.cod_det_ina) AS cod_det_ina, " &
                                        "                                        {3} AS parciales1, " &
                                        "                                        {4} AS parciales2 " &
                                        "                FROM " &
                                        "                    SIG_T.dbo.t_corr_detalle_inasistencias AS ASIS " &
                                        "                INNER JOIN " &
                                        "                    SIG_T.dbo.t_corr_parciales AS PAR " &
                                        "                    ON ASIS.cod_par = PAR.cod_par " &
                                        "                INNER JOIN " &
                                        "                    SIG_T.dbo.t_corr_detalle_matriculas AS MAT " &
                                        "                    ON ASIS.cod_det_mat = MAT.cod_det_mat " &
                                        "                WHERE " &
                                        "                    {2} " &
                                        "                GROUP BY " &
                                        "                    ASIS.cod_det_mat " &
                                        "            ) AS ASIS " &
                                        "            ON MAT.cod_det_mat = ASIS.cod_det_mat " &
                                        "    ) AS CORR_EDU " &
                                        "    ON BEN.per_rup_persona = CORR_EDU.cod_rup_per_sac " &
                                        "LEFT OUTER JOIN " &
                                        "    ( " &
                                        "        SELECT " &
                                        "            MAT.COD_DET_MAT, " &
                                        "            PER.COD_RUP_PER_SAC, " &
                                        "            MAT.COD_GRA " &
                                        "        FROM " &
                                        "            SIG_T.DBO.T_CORR_DETALLE_MATRICULAS AS MAT " &
                                        "        INNER JOIN " &
                                        "            ( " &
                                        "                SELECT " &
                                        "                    MAT.COD_PER_SAC, " &
                                        "                    MAX(MAT.COD_SAC_MAT_DET_MAT) COD_SAC_MAT_DET_MAT " &
                                        "                FROM " &
                                        "                    [SIG_T].dbo.[T_CORR_DETALLE_MATRICULAS] AS MAT " &
                                        "                WHERE " &
                                        "                    MAT.COD_EST_MAT IN (1, 5) " &
                                        "                    AND MAT.ANO_MAT_DET_MAT < {1} " &
                                        "                GROUP BY " &
                                        "                    MAT.COD_PER_SAC " &
                                        "            ) AS MAT_MAX " &
                                        "            ON MAT.COD_SAC_MAT_DET_MAT = MAT_MAX.COD_SAC_MAT_DET_MAT " &
                                        "        INNER JOIN " &
                                        "            SIG_T.dbo.T_CORR_PERSONAS_SACE AS PER " &
                                        "            ON MAT.COD_PER_SAC = PER.COD_PER_SAC " &
                                        "            AND PER.COD_RUP_PER_SAC IS NOT NULL " &
                                        "    ) AS CORR_EDU_ANT " &
                                        "    ON BEN.PER_RUP_PERSONA = CORR_EDU_ANT.COD_RUP_PER_SAC " &
                                        "WHERE " &
                                        "   HOG.hog_estado = 1 " &
                                        "   {5} ", strWhereVisMed, intAño, strWhereAsis, strNombrePar1, strNombrePar2, strWhereUbica)
            End If

            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            Return objTabla
        End Function


        Public Function fnc_obtener_corresponsabilidades() As DataTable
            strQuery = "SELECT " &
                        "      corr_codigo, " &
                        "      comp_codigo, " &
                        "      corr_nombre " &
                        "FROM " &
                        "      SIG_T.dbo.f_pla_corresponsabilidad "
            objTabla = objConexionDB.fnc_crear_datatable(strQuery)

            Return objTabla
        End Function

        Public Function fnc_obtener_componente() As DataTable
            strQuery = "SELECT " &
                        "      comp_codigo, " &
                        "      comp_nombre " &
                        "FROM " &
                        "      SIG_T.dbo.f_pla_tipo_componente "
            objTabla = objConexionDB.fnc_crear_datatable(strQuery)

            Return objTabla
        End Function

        Public Function fnc_obtener_genero_sexual() As DataTable
            objTabla = New DataTable

            objTabla.Columns.Add("hab_sexo", GetType(Integer))
            objTabla.Columns.Add("hab_sexo_descripcion", GetType(String))

            For i = 1 To arrStrSexo.Length
                objTabla.Rows.Add(i - 1, arrStrSexo(i - 1))
            Next

            Return objTabla
        End Function

        Public Function fnc_obtener_estado_programado() As DataTable
            objTabla = New DataTable

            objTabla.Columns.Add("tit_planilla", GetType(Integer))
            objTabla.Columns.Add("tit_planilla_descripcion", GetType(String))

            For i = 1 To arrStrEstProgra.Length
                objTabla.Rows.Add(i - 1, arrStrEstProgra(i - 1))
            Next

            Return objTabla
        End Function

        Public Function fnc_obtener_estado_cobro() As DataTable
            objTabla = New DataTable

            objTabla.Columns.Add("tit_cobro", GetType(Integer))
            objTabla.Columns.Add("tit_cobro_descripcion", GetType(String))

            For i = 1 To arrStrEstCobro.Length
                objTabla.Rows.Add(i - 1, arrStrEstCobro(i - 1))
            Next

            Return objTabla
        End Function

        Public Function fnc_obtener_ciclo_edades() As DataTable
            objTabla = New DataTable

            objTabla.Columns.Add("cod_ciclo_edad", GetType(Integer))
            objTabla.Columns.Add("ciclo_edad_descripcion", GetType(String))

            For i = 0 To arrStrCicloEdad.Length - 1
                objTabla.Rows.Add(i, arrStrCicloEdad(i))
            Next

            Return objTabla
        End Function

        'Public Function fnc_obtener_ciclo_planilla() As DataTable
        '    objTabla = New DataTable

        '    'objTabla.Columns.Add("cod_ciclo_edad", GetType(Integer))
        '    'objTabla.Columns.Add("ciclo_edad_descripcion", GetType(String))

        '    For i = 1 To arrStrCicloPlani.Length
        '        objTabla.Rows.Add(i, arrStrCicloPlani(i - 1))
        '    Next

        '    Return objTabla
        'End Function




        Public Function fnc_obtener_tipo_busqueda() As DataTable
            objTabla = New DataTable

            objTabla.Columns.Add("cod_tip_bus", GetType(Integer))
            objTabla.Columns.Add("des_tip_bus", GetType(String))

            For i = 1 To arrStrTipBusqueda.Length
                objTabla.Rows.Add(i - 1, arrStrTipBusqueda(i - 1))
            Next

            Return objTabla
        End Function


        Private Function fnc_obtener_datos_corres(ByRef intCodCorr As Integer) As Array
            strQuery = String.Format("SELECT " &
                                    "	CORR.cod_tip_cor, " &
                                    "	CORR.año_det_cor, " &
                                    "	CONVERT(DATE, CORR.fec_ini_det_cor), " &
                                    "	CONVERT(DATE, CORR.fec_fin_det_cor), " &
                                    "	CORR.par_ini_det_cor, " &
                                    "	CORR.par_fin_det_cor " &
                                    "FROM " &
                                    "	SIG_T.dbo.t_corr_detalle_corresponsabilidades AS CORR " &
                                    "INNER JOIN " &
                                    "	SIG_T.dbo.f_pla_corresponsabilidad AS CORR_PLA " &
                                    "	ON CORR.cod_det_cor = CORR_PLA.cod_det_cor " &
                                    "WHERE " &
                                    "   CORR.est_det_cor = 1 " &
                                    "	AND CORR_PLA.corr_codigo = {0} ", intCodCorr)

            Return objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 6)
        End Function

        Public Function fnc_obtener_grados() As DataTable
            strQuery = "SELECT " &
                        "	cod_gra, " &
                        "	nom_gra " &
                        "FROM " &
                        "	SIG_T.dbo.t_corr_grados "

            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objFila = objTabla.NewRow()
            objFila.Item("cod_gra") = 0
            objFila.Item("nom_gra") = "Sin Datos"
            objTabla.Rows.InsertAt(objFila, 0)

            Return objTabla
        End Function

    End Class
End Namespace
