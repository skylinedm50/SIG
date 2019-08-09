Imports System.ComponentModel.DataAnnotations

Namespace SIG.Areas.Corresponsabilidad.Models

    Public Class cl_detalle_corresponsabilidad
        Private objConexionDB As New cl_conexion_db()
        Private objManeDato As New cl_manejo_datos()
        Private strQuery As String
        Private objTabla As DataTable
        Private arrObjeto As Array
        Private blExiste, blExiste2 As Boolean
        Private arrMeses() As String = {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"}
        Private arrTipoCorres() As String = {"MATRICULA", "ASISTENCIA", "VISITAS"}

        Public cod_det_cor As Integer
        Public cod_tip_cor As Integer
        Public num_det_cor As Integer
        Public año_det_cor As Integer
        Public par_ini_det_cor As Integer
        Public par_fin_det_cor As Integer
        Public fec_ini_det_cor As Date
        Public fec_fin_det_cor As Date

        Public intTipoOpera As Integer
        Public intCodUser As Integer


        Function fnc_obtener_detalle_corrresponsabilidad() As DataTable 'Funcion que crea un DataTble apartir de los detalle de corresponsabilidad.

            strQuery = "SELECT  " &
                        "	cod_det_cor, " &
                        "	nom_det_corr, " &
                        "	cod_tip_cor,  " &
                        "	num_det_cor,  " &
                        "	año_det_cor,  " &
                        "	CASE par_ini_det_cor  " &
                        "		WHEN 0 THEN NULL  " &
                        "		ELSE par_ini_det_cor  " &
                        "	END AS par_ini_det_cor,  " &
                        "	CASE par_fin_det_cor  " &
                        "		WHEN 0 THEN NULL  " &
                        "		ELSE par_fin_det_cor  " &
                        "	END AS par_fin_det_cor,  " &
                        "	CASE fec_ini_det_cor  " &
                        "		WHEN '1900-01-01' THEN NULL  " &
                        "		ELSE fec_ini_det_cor  " &
                        "	END AS fec_ini_det_cor,  " &
                        "	CASE fec_fin_det_cor  " &
                        "		WHEN '1900-01-01' THEN NULL  " &
                        "		ELSE fec_fin_det_cor  " &
                        "    End AS fec_fin_det_cor  " &
                        "FROM  " &
                        "	SIG_T.dbo.t_corr_detalle_corresponsabilidades  " &
                        "WHERE  " &
                        "	est_det_cor = 1 " &
                        "ORDER BY " &
                        "		cod_det_cor DESC "
            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            Return objTabla
        End Function

        Function fnc_obtener_tipo_corresponsabilidad() As DataTable
            strQuery = "SELECT " & _
                        "	cod_tip_cor, " & _
                        "	nom_tip_cor " & _
                        "FROM " & _
                        "	SIG_T.dbo.t_corr_tipo_corresponsabilidad"
            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            Return objTabla
        End Function

        Function fnc_obtener_nombre_corres() As String
            Dim strNombreCorres As String = ""
            Dim strWordParci As String = "PARCIAL"

            Select Case cod_tip_cor
                Case 1
                    strNombreCorres = arrTipoCorres(0) & " " & año_det_cor
                    Exit Select
                Case 2
                    strNombreCorres = arrTipoCorres(1) & " " & num_det_cor & " "
                    If par_fin_det_cor <> 0 Then
                        strNombreCorres &= fnc_conver_decimal_roman(par_ini_det_cor) & "-" & fnc_conver_decimal_roman(par_fin_det_cor) & " " & strWordParci & " " & año_det_cor
                    Else
                        strNombreCorres &= fnc_conver_decimal_roman(par_ini_det_cor) & " " & strWordParci & " " & año_det_cor
                    End If
                    Exit Select
                Case 3
                    strNombreCorres = arrTipoCorres(1) & " " & num_det_cor & " "

                    If fec_ini_det_cor.Year = fec_fin_det_cor.Year Then 'Mismo año.
                        If fec_ini_det_cor.Month = fec_fin_det_cor.Month Then 'Mismo mes
                            strNombreCorres &= " " & arrMeses(fec_ini_det_cor.Month - 1) & " " & año_det_cor
                        Else
                            strNombreCorres &= " " & arrMeses(fec_ini_det_cor.Month - 1) & " A " & arrMeses(fec_fin_det_cor.Month - 1) & " " & año_det_cor
                        End If
                    Else 'Años diferentes
                        strNombreCorres &= " " & arrMeses(fec_ini_det_cor.Month - 1) & " " & fec_ini_det_cor.Year & " A " & arrMeses(fec_fin_det_cor.Month - 1) & " " & fec_fin_det_cor.Year
                    End If
            End Select

            Return strNombreCorres
        End Function

        Function fnc_conver_decimal_roman(ByRef intNumParcial As Integer) As String
            Select Case intNumParcial
                Case 1
                    Return "I"
                Case 2
                    Return "II"
                Case 3
                    Return "III"
                Case Else
                    Return "IV"
            End Select
        End Function

        Function fnc_obtener_numero_corr() As Integer
            strQuery = String.Format("SELECT " &
                                    "	(ISNULL(MAX(num_det_cor), 0) + 1) " &
                                    "FROM " &
                                    "	SIG_T.dbo.t_corr_detalle_corresponsabilidades " &
                                    "WHERE " &
                                    "	est_det_cor = 1 " &
                                    "	AND cod_tip_cor = {0} " &
                                    "	AND cod_det_cor <> {1} ", cod_tip_cor, cod_det_cor)

            Select Case cod_tip_cor
                Case 1 To 2
                    strQuery &= String.Format(" AND año_det_cor = {0} ", año_det_cor)
                    Exit Select
                Case 3
                    If fec_ini_det_cor.Year = fec_fin_det_cor.Year Then 'Mismo año.
                        strQuery &= String.Format(" AND año_det_cor = {0}", fec_ini_det_cor.Year)
                    Else 'Años diferentes
                        strQuery &= String.Format(" AND YEAR(fec_ini_det_cor) = {0} " &
                                                 " AND YEAR(fec_fin_det_cor) = {1} ", fec_ini_det_cor.Year, fec_fin_det_cor.Year)
                    End If
            End Select

            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)
            num_det_cor = CInt(arrObjeto(0))

            Return CInt(arrObjeto(0))
        End Function

        Public Function fnc_nuevo_actualizar_detalle_corrresponsabilidad() As Integer
            Return objConexionDB.fnc_p_corr_operaciones_corresponsabilidades(intTipoOpera,
                                                                             intCodUser,
                                                                             cod_det_cor,
                                                                             cod_tip_cor,
                                                                             fnc_obtener_numero_corr(),
                                                                             año_det_cor,
                                                                             par_ini_det_cor,
                                                                             par_fin_det_cor,
                                                                             fec_ini_det_cor,
                                                                             fec_fin_det_cor,
                                                                             fnc_obtener_nombre_corres())
        End Function

        Public Function fnc_obtener_info_corres(ByRef intCodDetCorr As Integer) As List(Of Dictionary(Of String, Object))
            strQuery = String.Format("SELECT " &
                                    "	cod_tip_cor, " &
                                    "	num_det_cor, " &
                                    "	año_det_cor, " &
                                    "	par_ini_det_cor, " &
                                    "	par_fin_det_cor, " &
                                    "	CONVERT(DATE, fec_ini_det_cor, 103) AS fec_ini_det_cor, " &
                                    "	CONVERT(DATE, fec_fin_det_cor,103) AS fec_fin_det_cor " &
                                    "FROM " &
                                    "	SIG_T.dbo.t_corr_detalle_corresponsabilidades " &
                                    "WHERE " &
                                    "	cod_det_cor = {0} ", intCodDetCorr)
            objTabla = objConexionDB.fnc_crear_datatable(strQuery)

            Return objManeDato.fnc_crear_datatable_diccionario(objTabla)
        End Function

        Public Function fnc_borrar_detalle_corrresponsabilidad() As Integer
            Return objConexionDB.fnc_p_corr_operaciones_corresponsabilidades(3,
                                                                            intCodUser,
                                                                            cod_det_cor,
                                                                            0,
                                                                            0,
                                                                            0,
                                                                            0,
                                                                            0,
                                                                            fec_ini_det_cor,
                                                                            fec_fin_det_cor,
                                                                            "NULL")

        End Function

        Function fnc_validar_fechas() As Boolean 'Validar que los registro de fecha no coincidan con otras corresponsabilidades del mismo año.
            strQuery = String.Format("SELECT  " &
                                    "	cod_det_cor " &
                                    "FROM " &
                                    "	SIG_T.dbo.t_corr_detalle_corresponsabilidades " &
                                    "WHERE " &
                                    "	est_det_cor = 1 " &
                                    "	AND " &
                                    "		( " &
                                    "			fec_ini_det_cor >= '{0}' " &
                                    "			AND fec_fin_det_cor <= '{1}' " &
                                    "			OR fec_fin_det_cor >= '{2}' " &
                                    "			AND fec_ini_det_cor <= '{3}' " &
                                    "		) " &
                                    "	AND cod_tip_cor = {4} " &
                                    "	AND cod_det_cor <> {5} ",
                                    Format(Convert.ToDateTime(fec_ini_det_cor), "yyyy-MM-dd"),
                                    Format(Convert.ToDateTime(fec_fin_det_cor), "yyyy-MM-dd"),
                                    Format(Convert.ToDateTime(fec_ini_det_cor), "yyyy-MM-dd"),
                                    Format(Convert.ToDateTime(fec_fin_det_cor), "yyyy-MM-dd"),
                                    cod_tip_cor,
                                    cod_det_cor)
            blExiste = objConexionDB.fnc_verificar_existe_registros(strQuery)

            Return blExiste
        End Function

        Function fnc_validar_no_exite_parciales() As Boolean 'Validar que los registro de parciales no coincidan con otras corresponsabilidades del mismo año.
            strQuery = String.Format("SELECT " &
                                    "	cod_det_cor " &
                                    "FROM " &
                                    "	SIG_T.dbo.t_corr_detalle_corresponsabilidades " &
                                    "WHERE " &
                                    "	est_det_cor = 1  " &
                                    "	AND (par_ini_det_cor BETWEEN {0} AND {1} OR par_fin_det_cor BETWEEN {2} AND {3}) " &
                                    "	AND año_det_cor = {4}  " &
                                    "	AND cod_tip_cor = {5}  " &
                                    "	AND cod_det_cor <> {6} ",
                                     par_ini_det_cor,
                                     par_fin_det_cor,
                                     par_ini_det_cor,
                                     par_fin_det_cor,
                                     año_det_cor,
                                     cod_tip_cor,
                                     cod_det_cor)
            blExiste = objConexionDB.fnc_verificar_existe_registros(strQuery)

            Return blExiste
        End Function

        Function fnc_validar_no_existe_un_parcial() As Boolean 'Validar que el registro del parcial no coincida con otras corresponsabilidades del mismo año.
            strQuery = String.Format("SELECT  " &
                                    "	CORR2.cod_det_cor " &
                                    "FROM  " &
                                    "	SIG_T.dbo.t_corr_detalle_corresponsabilidades AS CORR " &
                                    "INNER JOIN " &
                                    "	( " &
                                    "        SELECT " &
                                    "        	cod_det_cor, " &
                                    "        	cod_tip_cor, " &
                                    "        	año_det_cor, " &
                                    "        	(par_ini_det_cor + 1) AS par_ini_det_cor, " &
                                    "        	(par_fin_det_cor - 1) AS par_fin_det_cor " &
                                    "        FROM  " &
                                    "        	SIG_T.dbo.t_corr_detalle_corresponsabilidades " &
                                    "        WHERE " &
                                    "		    año_det_cor = {0} " &
                                    "		    AND est_det_cor = 1 " &
                                    "		    AND cod_tip_cor = {1} " &
                                    "		    AND cod_det_cor <> {2} " &
                                    "    ) AS CORR2 " &
                                    "    ON CORR.cod_det_cor = CORR2.cod_det_cor " &
                                    "WHERE " &
                                    "	CORR2.par_ini_det_cor = {3} " &
                                    "	OR CORR2.par_fin_det_cor = {4} ",
                                    año_det_cor, cod_tip_cor,
                                    cod_det_cor,
                                    par_ini_det_cor,
                                    par_ini_det_cor)
            blExiste = objConexionDB.fnc_verificar_existe_registros(strQuery)


            strQuery = String.Format("SELECT " &
                                    "   cod_det_cor " &
                                    "FROM " &
                                    "    SIG_T.dbo.t_corr_detalle_corresponsabilidades " &
                                    "WHERE " &
                                    "   est_det_cor = 1 " &
                                    "   AND año_det_cor = {0} " &
                                    "   AND cod_tip_cor = {1} " &
                                    "   AND cod_det_cor <> {2} " &
                                    "   AND (par_ini_det_cor = {3} OR par_fin_det_cor = {4}) ",
                                         año_det_cor,
                                         cod_tip_cor,
                                         cod_det_cor,
                                         par_ini_det_cor,
                                         par_ini_det_cor)
            blExiste2 = objConexionDB.fnc_verificar_existe_registros(strQuery)

            If blExiste Or blExiste2 Then
                Return False
            Else
                Return True
            End If
        End Function

        Function fnc_validar_año_no_existe() As Boolean
            strQuery = String.Format("SELECT " &
                                    "	cod_det_cor " &
                                    "FROM " &
                                    "   SIG_T.dbo.t_corr_detalle_corresponsabilidades " &
                                    "WHERE " &
                                    "	est_det_cor = 1 " &
                                    "	AND año_det_cor = {0} " &
                                    "	AND cod_tip_cor = {1} " &
                                    "	AND cod_det_cor <> {2} ",
                                     año_det_cor,
                                     cod_tip_cor,
                                     cod_det_cor)
            blExiste = objConexionDB.fnc_verificar_existe_registros(strQuery)

            Return blExiste
        End Function

        Function fnc_correspo_en_uso() As Boolean
            strQuery = String.Format("SELECT " &
                                    "	TOP 1 1 " &
                                    "FROM " &
                                    "	SIG_T.dbo.t_corr_detalle_corresponsabilidades AS CORR " &
                                    "INNER JOIN " &
                                    "	SIG_T.dbo.f_pla_corresponsabilidad AS CORR_PLA " &
                                    "	ON CORR.cod_det_cor = CORR_PLA.cod_det_cor " &
                                    "INNER JOIN " &
                                    "	SIG_T.dbo.f_pla_bonos_corresp AS BON_CORR " &
                                    "	ON CORR_PLA.corr_codigo = BON_CORR.corresp_codigo " &
                                    "WHERE " &
                                    "	CORR.cod_det_cor = {0} ", cod_det_cor)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            If arrObjeto(0) Is Nothing Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

End Namespace
