Imports Newtonsoft.Json

Namespace SIG.Areas.Corresponsabilidad.Controllers
    Public Class ReportesController
        Inherits System.Web.Mvc.Controller
        Private objConexionDB As New SIG.Areas.Corresponsabilidad.Models.cl_conexion_db()
        Private strQuery As String
        Private objTabla As DataTable
        Private objConfiguracionDatos As New SIG.Areas.Corresponsabilidad.Models.cl_manejo_datos
        Private arrListDataTable As New List(Of Dictionary(Of String, Object))
        Private objFila As DataRow
        Private objDatosReporte As New SIG.Areas.Corresponsabilidad.Models.cl_data_reporte


        Private login As New Global.SIG.Cl_Login

#Region "Reporte de actualizaciones de educación y salud"
        Function ViewReportCargasEducaSalud() As ActionResult

            Return View()
        End Function

        Function AspxComboBoxActulizacion(ByVal intCodTipCom As Integer, ByVal intTipoForm As Integer) As ActionResult
            Dim objReporCarEduSal As New SIG.Areas.Corresponsabilidad.Models.cl_report_carg_edu_sal
            ViewData("intCodTipCom") = intCodTipCom
            ViewData("intTipoForm") = intTipoForm

            Return View("Combox_Actualizacion/ViewPartialComboxActuali", objReporCarEduSal.fnc_obtener_nombre_actulizacion(intCodTipCom, intTipoForm))
        End Function

        Function AspxGridViewCargasEduSal(ByVal intTipCom As Integer, ByVal intNumActu As Integer) As ActionResult
            Dim objReporCarEduSal As New Models.cl_report_carg_edu_sal
            ViewData("intNumActu") = intNumActu
            ViewData("intTipCom") = intTipCom

            Return View("Carga_Educa_Sal/_GridCargaEduSal", objReporCarEduSal.fnc_obtener_actulizaciones(intTipCom, intNumActu))
        End Function

        Function AspxGridViewDetallePorLog(ByVal intCodLog As Integer, ByVal intCodTipRegis As Integer, ByVal intComp As Integer) As ActionResult
            Dim objReporCarEduSal As New Models.cl_report_carg_edu_sal
            ViewData("intCodLog") = intCodLog
            ViewData("intCodTipRegis") = intCodTipRegis
            ViewData("intComp") = intComp

            If Session("intCodLog") <> intCodLog Or Session("intCodTipRegis") <> intCodTipRegis Or Session("intComp") <> intComp Then
                Session("intCodLog") = intCodLog
                Session("intCodTipRegis") = intCodTipRegis
                Session("intComp") = intComp

                Session("objConlDetLog") = objReporCarEduSal.fnc_obtener_detalle_log(intComp, intCodTipRegis, intCodLog)
            End If

            Return View("Carga_Educa_Sal/_GridDetallePorLog", Session("objConlDetLog"))
        End Function

#End Region

#Region "Reporte CAI"
        Function viewReporteCAI() As ActionResult

            Return View()
        End Function

        Function AspxGridViewCAI(ByVal intCodTipBusq As Integer, ByVal intAño As Integer, ByVal strCodPago As String, ByVal strCodCorres As String,
                                 ByVal strCodDep As String, ByVal strCodMun As String, ByVal strCodAld As String, ByVal strCodCas As String) As ActionResult
            Dim objReporCAI As New Models.cl_report_CAI
            ViewData("intCodTipBusq") = intCodTipBusq
            ViewData("intAño") = intAño
            ViewData("strCodPago") = strCodPago
            ViewData("strCodCorres") = strCodCorres
            ViewData("strCodDep") = strCodDep
            ViewData("strCodMun") = strCodMun
            ViewData("strCodAld") = strCodAld
            ViewData("strCodCas") = strCodCas

            If Session("intCodTipBusqCorres") <> intCodTipBusq Or Session("intAñoCorres") <> intAño Or Session("strCodPagoCorres") <> strCodPago Or Session("strCodCorresCorres") <> strCodCorres Or
                Session("strCodDepCAICorres") <> strCodDep Or Session("strCodMunCAICorres") <> strCodMun Or Session("strCodAldCAICorres") <> strCodAld Or Session("strCodCasCAICorres") <> strCodCas Then
                Session("intCodTipBusqCorres") = intCodTipBusq
                Session("intAñoCorres") = intAño
                Session("strCodPagoCorres") = strCodPago
                Session("strCodCorresCorres") = strCodCorres
                Session("strCodDepCAICorres") = strCodDep
                Session("strCodMunCAICorres") = strCodMun
                Session("strCodAldCAICorres") = strCodAld
                Session("strCodCasCAICorres") = strCodCas

                Session("infoCAICorres") = objReporCAI.fnc_obetner_CAI(intCodTipBusq, intAño, strCodPago, strCodCorres, strCodDep, strCodMun, strCodAld, strCodCas)
            End If

            Return View("CAI/_GridViewCAI", Session("infoCAICorres"))
        End Function

        Function AspxGridViewPago(ByVal intAño As Integer, ByVal intCodTipBusq As Integer) As ActionResult
            If intCodTipBusq = 1 Then
                intAño = 0
            End If
            ViewData("intAño") = intAño
            ViewData("intCodTipBusq") = intCodTipBusq

            strQuery = String.Format("SELECT " &
                                    "        pag_codigo, " &
                                    "        pag_nombre, " &
                                    "        pag_anyo " &
                                    "FROM " &
                                    "        SIG_T.dbo.f_pla_pago " &
                                    "WHERE " &
                                    "        pag_anyo = {0} ", intAño)
            Return View("Control_pago/_ViewPartialGridViewPago", objConexionDB.fnc_crear_datatable(strQuery))
        End Function

        Function AspxGridViewCorres(ByVal intAño As Integer, ByVal intCodTipBusq As Integer) As ActionResult
            Dim strAndWhere As String = " AND CORR.cod_det_cor > 0 "
            Dim objDataTable As DataTable
            Dim strCodEsquemas As String = ""
            Dim intContador As Integer
            ViewData("intAño") = intAño
            ViewData("intCodTipBusq") = intCodTipBusq


            If intCodTipBusq = 0 Then
                strQuery = String.Format("SELECT " &
                                        "  ESQ.esq_codigo " &
                                        "FROM " &
                                        "  SIG_T.dbo.f_pla_pago AS PAG " &
                                        "INNER JOIN " &
                                        "  SIG_T.dbo.f_pla_esquema AS ESQ " &
                                        "  ON PAG.pag_codigo = ESQ.pag_codigo " &
                                        "WHERE " &
                                        "  PAG.pag_anyo = {0} " &
                                        "  AND PAG.Pag_Estado <> 9 ", intAño)
                objDataTable = objConexionDB.fnc_crear_datatable(strQuery)

                If objDataTable.Rows.Count > 0 Then
                    For Each objFila As DataRow In objDataTable.Rows
                        If objDataTable.Rows.Count = (intContador + 1) Then
                            strCodEsquemas &= objFila("esq_codigo")
                        Else
                            strCodEsquemas &= objFila("esq_codigo") & ", "
                        End If
                        intContador += 1
                    Next
                Else
                    strCodEsquemas = 0
                End If

                strQuery = String.Format("SELECT " &
                                        "  DISTINCT CORR.corr_codigo, " &
                                        "  COM.comp_nombre, " &
                                        "  PAG.pag_nombre, " &
                                        "  CORR.corr_nombre " &
                                        "FROM " &
                                        "  SIG_T.dbo.f_pla_esquema AS ESQ " &
                                        "INNER JOIN " &
                                        "   SIG_T.dbo.f_pla_pago AS PAG " &
                                        "   ON ESQ.pag_codigo = PAG.pag_codigo " &
                                        "INNER JOIN " &
                                        "  SIG_T.dbo.f_pla_bonos_act AS BON " &
                                        "  ON ESQ.esq_codigo = BON.esq_codigo " &
                                        "INNER JOIN " &
                                        "  SIG_T.dbo.f_pla_bonos_corresp AS BON_CORR " &
                                        "  ON BON.bon_codigo = BON_CORR.bon_codigo " &
                                        "INNER JOIN " &
                                        "  SIG_T.dbo.f_pla_corresponsabilidad AS CORR " &
                                        "  ON BON_CORR.corresp_codigo = CORR.corr_codigo " &
                                        "INNER JOIN " &
                                        "  SIG_T.dbo.f_pla_tipo_componente AS COM " &
                                        "  ON CORR.comp_codigo = COM.comp_codigo " &
                                        "WHERE " &
                                        "  ESQ.esq_codigo IN({0}) " &
                                        "ORDER BY " &
                                        "  PAG.pag_nombre ", strCodEsquemas)
            Else
                strQuery = String.Format("SELECT " &
                                        "        CORR.corr_codigo, " &
                                        "        COM.comp_nombre, " &
                                        "        CORR.corr_nombre " &
                                        "FROM " &
                                        "        SIG_T.dbo.f_pla_corresponsabilidad AS CORR " &
                                        "INNER JOIN " &
                                        "        SIG_T.dbo.f_pla_tipo_componente AS COM " &
                                        "        ON CORR.comp_codigo = COM.comp_codigo " &
                                        "WHERE " &
                                        "    YEAR(CORR.corr_fecha_inicio) = {0} " &
                                        "    AND CORR.cod_det_cor > 0 ", intAño)
            End If

            Return View("Control_corres/_ViewPartialGridViewCorres", objConexionDB.fnc_crear_datatable(strQuery))
        End Function

#End Region

#Region "Reporte altas y bajas por centros."
        Function viewReporteAltasBajasCentros() As ActionResult

            Return View()
        End Function
#End Region

#Region "Reporte búqueda de corresponsabilidad de beneficiario."
        Function viewReporteCorresBeneficia() As ActionResult

            Return View()
        End Function

        Function AspxGridViewResultBusqCorrBenef(ByVal intCodBenef As Integer,
                                                ByVal intCodRUPBenef As Integer,
                                                ByVal strIdentiBenef As String,
                                                ByVal strNom1Benef As String,
                                                ByVal strNom2Benef As String,
                                                ByVal strApe1Benef As String,
                                                ByVal strApe2Benef As String,
                                                ByVal strIdentiTit As String,
                                                ByVal intCodHogRUP As Integer,
                                                ByVal intCodHogSIG As Integer) As ActionResult
            Dim objReporBusqCorresBenef As New Models.cl_report_busq_corres_benef
            ViewData("intCodBene") = intCodBenef
            ViewData("intCodRUPBenef") = intCodRUPBenef
            ViewData("strIdentiBenef") = strIdentiBenef
            ViewData("strNom1Benef") = strNom1Benef
            ViewData("strNom2Benef") = strNom2Benef
            ViewData("strApe1Benef") = strApe1Benef
            ViewData("strApe2Benef") = strApe2Benef
            ViewData("strIdentiTit") = strIdentiTit
            ViewData("intCodHogRUP") = intCodHogRUP
            ViewData("intCodHogSIG") = intCodHogSIG


            If Session("intCodBenefBusCorr") <> intCodBenef Or Session("intCodRUPBenefBusCorr") <> intCodRUPBenef Or Session("strIdentiBenefBusCorr") <> strIdentiBenef Or
                Session("strNom1BenefBusCorr") <> strNom1Benef Or Session("strNom2BenefBusCorr") <> strNom2Benef Or Session("strApe1BenefBusCorr") <> strApe1Benef Or
                Session("strApe2BenefBusCorr") <> strApe2Benef Or Session("strIdentiTitBusCorr") <> strIdentiTit Or Session("intCodHogRUPBusCorr") <> intCodHogRUP Or
                Session("intCodHogSIGBusCorr") <> intCodHogSIG Then

                Session("intCodBenefBusCorr") = intCodBenef
                Session("intCodRUPBenefBusCorr") = intCodRUPBenef
                Session("strIdentiBenefBusCorr") = strIdentiBenef
                Session("strNom1BenefBusCorr") = strNom1Benef
                Session("strNom2BenefBusCorr") = strNom2Benef
                Session("strApe1BenefBusCorr") = strApe1Benef
                Session("strApe2BenefBusCorr") = strApe2Benef
                Session("strIdentiTitBusCorr") = strIdentiTit
                Session("intCodHogRUPBusCorr") = intCodHogRUP
                Session("intCodHogSIGBusCorr") = intCodHogSIG

                Session("InfoReportBusCorrBenf") = objReporBusqCorresBenef.fnc_obtener_result_benef(intCodBenef,
                                                                                                    intCodRUPBenef,
                                                                                                    strIdentiBenef,
                                                                                                    strNom1Benef,
                                                                                                    strNom2Benef,
                                                                                                    strApe1Benef,
                                                                                                    strApe2Benef,
                                                                                                    strIdentiTit,
                                                                                                    intCodHogRUP,
                                                                                                    intCodHogSIG)
            End If

            Return View("Busq_Correspon_Beneficia/_GridResulBenef", Session("InfoReportBusCorrBenf"))
        End Function

        Function AspxDocumentViewerBusqCorresBenef(ByVal intCodBenef As Integer, ByVal intTipoOperacion As Integer) As ActionResult
            ViewData("intCodBenef") = intCodBenef
            ViewData("intTipoOperacion") = intTipoOperacion

            If Session("intCodBenefBusqCorres") <> intCodBenef Then
                Dim objXtraReport As New cl_xtra_report_busq_corres_benef(intCodBenef, Session("username"))
                objXtraReport.PaperKind = System.Drawing.Printing.PaperKind.A4
                objXtraReport.Name = "report_hist_corre_" & intCodBenef

                Session("intCodBenefBusqCorres") = intCodBenef
                Session("objReportBuqCorresBenef") = objXtraReport
            End If

            If intTipoOperacion = 0 Then
                Return View("Busq_Correspon_Beneficia/_DocumentViewerBusqCorresBenef", Session("objReportBuqCorresBenef"))
            Else
                Return DevExpress.Web.Mvc.DocumentViewerExtension.ExportTo(Session("objReportBuqCorresBenef"))
            End If
        End Function

#End Region

#Region "Reporte de errores detectados en peticiones de actualización."
        Function viewReporteDetecErrPetActua() As ActionResult

            Return View()
        End Function

        Function AspxComboBoxTipComponente() As ActionResult
            ViewData("intTipoForm") = 1

            Return View("Combox_Tip_Componente/ViewPartialComboxTipCompo")
        End Function

        Function AspxGridViewErrorPetiActua(ByVal intCodComp As Integer, ByVal intNumActua As Integer) As ActionResult
            Dim objReporErrPetActu As New SIG.Areas.Corresponsabilidad.Models.cl_report_error_petici_actuali

            ViewData("intCodComp") = intCodComp
            ViewData("intNumActua") = intNumActua

            Return View("Err_petic_actuali/_GridErroPeticActu", objReporErrPetActu.fnc_obtener_info_errores(intNumActua, intCodComp))
        End Function
#End Region





#Region "Ubicación SIG AJAX"
        <HttpPost()>
        Function fnc_car_combo_muni(ByVal strCodDepartamento As String) As JsonResult
            strQuery = String.Format("SELECT  " +
                                    "      [cod_municipio],  " +
                                    "      [desc_municipio] " +
                                    "FROM  " +
                                    "      [dbo].[t_glo_municipios] " +
                                    "WHERE  " +
                                    "      [cod_departamento] = '{0}'", strCodDepartamento)
            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objFila = objTabla.NewRow()
            objFila.Item("cod_municipio") = "00"
            objFila.Item("desc_municipio") = "(Todo)"
            objTabla.Rows.InsertAt(objFila, 0)

            arrListDataTable = objConfiguracionDatos.fnc_crear_datatable_diccionario(objTabla)

            Return Json(arrListDataTable, JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost()>
        Function fnc_car_combo_alde(ByVal strCodMunicipio As String) As JsonResult
            strQuery = String.Format("SELECT  " +
                                    "      cod_aldea,  " +
                                    "      desc_aldea " +
                                    "FROM  " +
                                    "      [dbo].[t_glo_aldeas] " +
                                    "WHERE  " +
                                    "      cod_municipio = '{0}' ", strCodMunicipio)
            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objFila = objTabla.NewRow()
            objFila.Item("cod_aldea") = "00"
            objFila.Item("desc_aldea") = "(Todo)"
            objTabla.Rows.InsertAt(objFila, 0)

            arrListDataTable = objConfiguracionDatos.fnc_crear_datatable_diccionario(objTabla)

            Return Json(arrListDataTable, JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost()>
        Function fnc_car_combo_case(ByVal strCodAldea As String) As JsonResult
            strQuery = String.Format("SELECT  " +
                                    "      [cod_caserio],  " +
                                    "      [desc_caserio] " +
                                    "FROM  " +
                                    "      [dbo].[t_glo_caserios] " +
                                    "WHERE  " +
                                    "      [cod_aldea] = '{0}' ", strCodAldea)
            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objFila = objTabla.NewRow()
            objFila.Item("cod_caserio") = "00"
            objFila.Item("desc_caserio") = "(Todo)"
            objTabla.Rows.InsertAt(objFila, 0)

            arrListDataTable = objConfiguracionDatos.fnc_crear_datatable_diccionario(objTabla)

            Return Json(arrListDataTable, JsonRequestBehavior.AllowGet)
        End Function
#End Region

#Region "Ubicación SACE AJAX"
        <HttpPost()>
        Function fnc_car_combo_muni_sace(ByVal intCodDepar As Integer) As JsonResult

            If intCodDepar <> 0 Then
                strQuery = String.Format("SELECT cod_sac_mun_sac, nom_mun_sac " +
                                        "FROM t_corr_municipio_sace " +
                                        "WHERE cod_sac_dep_sac = {0}", intCodDepar)
                objTabla = objConexionDB.fnc_crear_datatable(strQuery)
                objFila = objTabla.NewRow()
                objFila.Item("cod_sac_mun_sac") = "0"
                objFila.Item("nom_mun_sac") = "(Todos)"
                objTabla.Rows.InsertAt(objFila, 0)
                arrListDataTable = objConfiguracionDatos.fnc_crear_datatable_diccionario(objTabla)
            End If
            Return Json(arrListDataTable, JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost()>
        Function fnc_car_combo_aldea_sace(ByVal intCodMuni As Integer) As JsonResult

            If intCodMuni <> 0 Then
                strQuery = String.Format("SELECT cod_sac_ald_sac, nom_ald_sac " +
                                        "FROM dbo.t_corr_aldea_sace " +
                                        "WHERE cod_sac_mun_sac = {0}", intCodMuni)
                objTabla = objConexionDB.fnc_crear_datatable(strQuery)
                objFila = objTabla.NewRow()
                objFila.Item("cod_sac_ald_sac") = "0"
                objFila.Item("nom_ald_sac") = "(Todos)"
                objTabla.Rows.InsertAt(objFila, 0)
                arrListDataTable = objConfiguracionDatos.fnc_crear_datatable_diccionario(objTabla)
            End If

            Return Json(arrListDataTable, JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost()>
        Function fnc_car_combo_caserio_sace(ByVal intCodAld As Integer) As JsonResult

            If intCodAld <> 0 Then
                strQuery = String.Format("SELECT cod_sac_cas_sac, nom_cas_sac " +
                                        "FROM dbo.t_corr_caserio_sace " +
                                        "WHERE cod_sac_ald_sac = {0}", intCodAld)
                objTabla = objConexionDB.fnc_crear_datatable(strQuery)
                objFila = objTabla.NewRow()
                objFila.Item("cod_sac_cas_sac") = "0"
                objFila.Item("nom_cas_sac") = "(Todos)"
                objTabla.Rows.InsertAt(objFila, 0)
                arrListDataTable = objConfiguracionDatos.fnc_crear_datatable_diccionario(objTabla)
            End If

            Return Json(arrListDataTable, JsonRequestBehavior.AllowGet)
        End Function

#End Region

#Region "Centro Educativo"
        <HttpPost()>
        Function fnc_car_combo_centro_educativo(ByVal intCodMuni As Integer, ByVal intCodAld As Integer, ByVal intCodCase As Integer) As JsonResult

            If intCodAld < 1 And intCodCase < 1 Then 'Filtrar por municipio.
                strQuery = String.Format("SELECT cod_sac_cen_edu, CONVERT(NVARCHAR, cod_sac_cen_edu) + ' - ' + nom_cen_edu AS nom_cen_edu " +
                                        "FROM t_corr_centro_educativo " +
                                        "WHERE cod_sac_mun_sac = {0}", intCodMuni)
            ElseIf intCodCase < 1 Then 'Filtrar por aldea.
                strQuery = String.Format("SELECT cod_sac_cen_edu, CONVERT(NVARCHAR, cod_sac_cen_edu) + ' - ' + nom_cen_edu AS nom_cen_edu " +
                                        "FROM t_corr_centro_educativo " +
                                        "WHERE cod_sac_ald_sac = {0}", intCodAld)
            Else 'Filtrar por caserio.
                strQuery = String.Format("SELECT cod_sac_cen_edu, CONVERT(NVARCHAR, cod_sac_cen_edu) + ' - ' + nom_cen_edu AS nom_cen_edu " +
                                        "FROM t_corr_centro_educativo " +
                                        "WHERE cod_sac_cas_sac = {0}", intCodCase)
            End If

            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objFila = objTabla.NewRow()
            objFila.Item("cod_sac_cen_edu") = 0
            objFila.Item("nom_cen_edu") = "(Todos)"
            objTabla.Rows.InsertAt(objFila, 0)
            arrListDataTable = objConfiguracionDatos.fnc_crear_datatable_diccionario(objTabla)

            Return Json(arrListDataTable, JsonRequestBehavior.AllowGet)
        End Function
#End Region

#Region "Centro de salud"
        <HttpPost()>
        Function fnc_car_combo_centro_salud(ByVal strCodMunicipio As String, ByVal strCodAldea As String, ByVal strCodCaserio As String) As JsonResult
            Dim strWhere As String

            If strCodAldea = "-1" And strCodCaserio = "-1" Then 'Filtrar por municipio.
                strWhere = String.Format("WHERE [cod_mun_cen_sal] = '{0}'", strCodMunicipio)
            ElseIf strCodCaserio = "-1" Then 'Filtrar por aldea.
                strWhere = String.Format("WHERE [cod_al_cen_sal] = '{0}'", strCodAldea)
            Else 'Filtrar por caserio.
                strWhere = String.Format("WHERE [cod_cas_cen_sal] = '{0}'", strCodCaserio)
            End If

            strQuery = String.Format("SELECT  " +
                                    "      [cod_cen_sal],  " +
                                    "      [nom_cen_sal] " +
                                    "FROM  " +
                                    "      [dbo].[t_corr_centro_salud] {0}", strWhere)

            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objFila = objTabla.NewRow()
            objFila.Item("cod_cen_sal") = 0
            objFila.Item("nom_cen_sal") = "(Todos)"
            objTabla.Rows.InsertAt(objFila, 0)
            arrListDataTable = objConfiguracionDatos.fnc_crear_datatable_diccionario(objTabla)

            Return Json(arrListDataTable, JsonRequestBehavior.AllowGet)
        End Function
#End Region

    End Class
End Namespace