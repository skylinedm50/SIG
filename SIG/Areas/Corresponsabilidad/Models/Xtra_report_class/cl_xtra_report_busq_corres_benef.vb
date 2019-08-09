Public Class cl_xtra_report_busq_corres_benef
    Inherits DevExpress.XtraReports.UI.XtraReport

#Region " Designer generated code "

    Public Sub New(ByRef intCodBenef As Integer, ByRef strUserName As String)
        MyBase.New()

        'This call is required by the Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Dim objReportBusqCorresBenef As New SIG.Areas.Corresponsabilidad.Models.cl_report_busq_corres_benef
        Dim objArrDatosHogar, objArrDatosTitu, objArrDatosBenef As Array
        Dim objDataTable As DataTable
        Dim objDataFila As DataRow
        Dim arrListDatos As New ArrayList
        Dim strFecha As String = Format(Convert.ToDateTime(DateTime.Now), "dd/MM/yyyy HH:mm")

        Me.XrLabelValFecha.Text = strFecha
        Me.XrLabelValUsu.Text = strUserName

        objArrDatosBenef = objReportBusqCorresBenef.fnc_obtener_info_beneficiario(intCodBenef)

        Me.XrLabelValBenef.Text = objArrDatosBenef(11)


        If objArrDatosBenef.Length > 0 Then
            objArrDatosHogar = objReportBusqCorresBenef.fnc_obtener_info_hogar(CInt(objArrDatosBenef(10)))
        Else
            objArrDatosHogar = Nothing
        End If

        Me.XrTableCellHogDepVal.Text = objArrDatosHogar(1)
        Me.XrTableCellHogMunVal.Text = objArrDatosHogar(2)
        Me.XrTableCellHogAldVal.Text = objArrDatosHogar(3)
        Me.XrTableCellHogCasVal.Text = objArrDatosHogar(4)
        Me.XrTableCellHogDirVal.Text = objArrDatosHogar(5)
        Me.XrTableCellHogUmbVal.Text = objArrDatosHogar(6)
        Me.XrTableCellHogEstVal.Text = objArrDatosHogar(7)

        objArrDatosTitu = objReportBusqCorresBenef.fnc_obtener_info_titular(CInt(objArrDatosBenef(10)))

        Me.XrTableCellTituCodVal.Text = objArrDatosTitu(0)
        Me.XrTableCellTituIdenVal.Text = objArrDatosTitu(1)
        Me.XrTableCellTituNo1Val.Text = objArrDatosTitu(2)
        Me.XrTableCellTituNo2Val.Text = objArrDatosTitu(3)
        Me.XrTableCellTituApe1Val.Text = objArrDatosTitu(4)
        Me.XrTableCellTituApe2Val.Text = objArrDatosTitu(5)
        Me.XrTableCellTituEdadVal.Text = objArrDatosTitu(6)
        Me.XrTableCellTituFecNaVal.Text = objArrDatosTitu(7)
        Me.XrTableCellTituSexVal.Text = objArrDatosTitu(8)
        Me.XrTableCellTituEstVal.Text = objArrDatosTitu(9)

        Me.XrTableCellBenefCodVal.Text = intCodBenef
        Me.XrTableCellBenefIdenVal.Text = objArrDatosBenef(0)
        Me.XrTableCellBenefNo1Val.Text = objArrDatosBenef(1)
        Me.XrTableCellBenefNo2Val.Text = objArrDatosBenef(2)
        Me.XrTableCellBenefApe1Val.Text = objArrDatosBenef(3)
        Me.XrTableCellBenefApe2Val.Text = objArrDatosBenef(4)
        Me.XrTableCellBenefEdadVal.Text = objArrDatosBenef(5)
        Me.XrTableCellBenefCicloVal.Text = objArrDatosBenef(6)
        Me.XrTableCellBenefFecNaVal.Text = objArrDatosBenef(7)
        Me.XrTableCellBenefSexVal.Text = objArrDatosBenef(8)
        Me.XrTableCellBenefEstVal.Text = objArrDatosBenef(9)
        Me.XrTableCellBenefHogVal.Text = objArrDatosBenef(10)


        'Informacicón de la corresponsabilidad de educación
        objDataTable = objReportBusqCorresBenef.fnc_obtener_corres_educacion(intCodBenef)

        Dim strAño As String = "0"
        Dim strCodMat As String = "0"
        Dim intContador As Integer = 0
        Dim boolCambioRegistro As Boolean = False

        Dim singleX As Single = 53.08332!
        Dim singleY As Single = 10.00001!
        Dim singleYAsis As Single = 10.00001!
        Dim sigleYEspacio As Single = 39.2!

        For Each objDataFila In objDataTable.Rows
            Dim objTableMat As New DevExpress.XtraReports.UI.XRTable
            Dim objTableCen As New DevExpress.XtraReports.UI.XRTable
            Dim objTableAsis As New DevExpress.XtraReports.UI.XRTable

            objTableAsis = objReportBusqCorresBenef.fnc_dibujar_table(singleX, singleYAsis, 3)
            objTableAsis.Rows.Add(objReportBusqCorresBenef.fnc_crear_encabe_asis_corr_edu())
            objTableAsis.Rows.Add(objReportBusqCorresBenef.fnc_crear_fila_asis_corr_edu(objDataFila("cod_det_mat"),
                                                                                        objDataFila("ano_mat_det_mat"),
                                                                                        objDataFila("dia_ina_det_ina_1"),
                                                                                        objDataFila("dia_ina_det_ina_2")))
            singleYAsis += sigleYEspacio
            objTableAsis.Borders = DevExpress.XtraPrinting.BorderSide.All
            Me.SubBand4.Controls.Add(objTableAsis)

            intContador += 1
            If intContador = 1 Then
                strAño = objDataFila("ano_mat_det_mat")
                strCodMat = objDataFila("cod_det_mat")

                objTableMat = objReportBusqCorresBenef.fnc_dibujar_table(singleX, singleY, 1)
                objTableMat.Rows.Add(objReportBusqCorresBenef.fnc_crear_encabe_matri_corr_edu())
                objTableMat.Rows.Add(objReportBusqCorresBenef.fnc_crear_fila_data_matri_corr_edu(strCodMat, strAño,
                                                                                                    objDataFila("nom_est_mat"),
                                                                                                     objDataFila("nom_raz_can"),
                                                                                                     objDataFila("nom_gra"),
                                                                                                     objDataFila("desc_est_pro")))
                objTableMat.Borders = DevExpress.XtraPrinting.BorderSide.All
                Me.SubBand3.Controls.Add(objTableMat)

                singleY += sigleYEspacio
                objTableCen = objReportBusqCorresBenef.fnc_dibujar_table(singleX, singleY, 2)
                objTableCen.Rows.Add(objReportBusqCorresBenef.fnc_crear_encabe_centro_corr_edu())
                objTableCen.Rows.Add(objReportBusqCorresBenef.fnc_crear_fila_data_centro_corr_edu(strCodMat,
                                                                                                        objDataFila("cod_cen_edu"),
                                                                                                        objDataFila("nom_cen_edu"),
                                                                                                        objDataFila("nom_dep_sac"),
                                                                                                        objDataFila("nom_mun_sac"),
                                                                                                        objDataFila("nom_ald_sac"),
                                                                                                        objDataFila("nom_cas_sac"),
                                                                                                        objDataFila("nom_bar_sac")))
                objTableCen.Borders = DevExpress.XtraPrinting.BorderSide.All
                Me.SubBand3.Controls.Add(objTableCen)
            Else
                If strAño <> objDataFila("ano_mat_det_mat") And strCodMat <> objDataFila("cod_det_mat") Then
                    strAño = objDataFila("ano_mat_det_mat")
                    strCodMat = objDataFila("cod_det_mat")
                    boolCambioRegistro = True
                Else
                    boolCambioRegistro = False
                End If
            End If

            If boolCambioRegistro = True Then

                singleY += sigleYEspacio

                objTableMat = objReportBusqCorresBenef.fnc_dibujar_table(singleX, singleY, 1)
                objTableMat.Rows.Add(objReportBusqCorresBenef.fnc_crear_encabe_matri_corr_edu())
                objTableMat.Rows.Add(objReportBusqCorresBenef.fnc_crear_fila_data_matri_corr_edu(strCodMat, strAño,
                                                                                                    objDataFila("nom_est_mat"),
                                                                                                     objDataFila("nom_raz_can"),
                                                                                                     objDataFila("nom_gra"),
                                                                                                     objDataFila("desc_est_pro")))
                objTableMat.Borders = DevExpress.XtraPrinting.BorderSide.All
                Me.SubBand3.Controls.Add(objTableMat)

                singleY += sigleYEspacio
                objTableCen = objReportBusqCorresBenef.fnc_dibujar_table(singleX, singleY, 2)
                objTableCen.Rows.Add(objReportBusqCorresBenef.fnc_crear_encabe_centro_corr_edu())
                objTableCen.Rows.Add(objReportBusqCorresBenef.fnc_crear_fila_data_centro_corr_edu(strCodMat,
                                                                                                        objDataFila("cod_cen_edu"),
                                                                                                        objDataFila("nom_cen_edu"),
                                                                                                        objDataFila("nom_dep_sac"),
                                                                                                        objDataFila("nom_mun_sac"),
                                                                                                        objDataFila("nom_ald_sac"),
                                                                                                        objDataFila("nom_cas_sac"),
                                                                                                        objDataFila("nom_bar_sac")))
                objTableCen.Borders = DevExpress.XtraPrinting.BorderSide.All
                Me.SubBand3.Controls.Add(objTableCen)
            End If
        Next



        'Información de la corresponsabilidad de salud
        Dim strCodCentro As String = "0"
        objDataTable = objReportBusqCorresBenef.fnc_obtener_corres_salud(intCodBenef)
        intContador = 0
        boolCambioRegistro = False

        For Each objDataFila In objDataTable.Rows
            intContador += 1
            If intContador = 1 Then
                strCodCentro = objDataFila("cod_cen_sal")

                Me.XrTableCorrSal.Rows.Add(objReportBusqCorresBenef.fnc_crea_encabe_centro_corr_sal())
                Me.XrTableCorrSal.Rows.Add(objReportBusqCorresBenef.fnc_crea_data_centro_corr_sal(objDataFila("cod_cen_sal"), objDataFila("cod_rup_cen_sal"), objDataFila("nom_cen_sal"), objDataFila("desc_departamento"), objDataFila("desc_municipio"), objDataFila("desc_aldea"), objDataFila("desc_caserio")))
                For i = 1 To 2
                    Me.XrTableCorrSal.Rows.Add(objReportBusqCorresBenef.fnc_crea_encabe_visi_corr_sal(i))
                Next
                Me.XrTableCorrSal.Rows.Add(objReportBusqCorresBenef.fnc_crear_fila_data_centro_corr_sal(objDataFila("cod_vis_sal"), objDataFila("fec_rea_vis_sal"), objDataFila("fec_reg_vis_sal")))
            Else
                If strCodCentro <> objDataFila("cod_cen_sal") Then
                    strCodCentro = objDataFila("cod_cen_sal")
                    boolCambioRegistro = True
                Else
                    boolCambioRegistro = False
                End If
            End If

            If boolCambioRegistro = True Then
                Me.XrTableCorrSal.Rows.Add(objReportBusqCorresBenef.fnc_crea_encabe_centro_corr_sal())
                Me.XrTableCorrSal.Rows.Add(objReportBusqCorresBenef.fnc_crea_data_centro_corr_sal(objDataFila("cod_cen_sal"), objDataFila("cod_rup_cen_sal"), objDataFila("nom_cen_sal"), objDataFila("desc_departamento"), objDataFila("desc_municipio"), objDataFila("desc_aldea"), objDataFila("desc_caserio")))
                For i = 1 To 2
                    Me.XrTableCorrSal.Rows.Add(objReportBusqCorresBenef.fnc_crea_encabe_visi_corr_sal(i))
                Next
                Me.XrTableCorrSal.Rows.Add(objReportBusqCorresBenef.fnc_crear_fila_data_centro_corr_sal(objDataFila("cod_vis_sal"), objDataFila("fec_rea_vis_sal"), objDataFila("fec_reg_vis_sal")))
            ElseIf intContador > 1 Then
                Me.XrTableCorrSal.Rows.Add(objReportBusqCorresBenef.fnc_crear_fila_data_centro_corr_sal(objDataFila("cod_vis_sal"), objDataFila("fec_rea_vis_sal"), objDataFila("fec_reg_vis_sal")))
            End If
        Next
        Me.XrTableCorrSal.Borders = DevExpress.XtraPrinting.BorderSide.All

    End Sub

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Friend WithEvents XrLabelTextoPrincipal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPictureBoxDistintivoSSIS As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents XrLabelNomReport As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents SubBand1 As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents XrTableInfHog As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellHogTitulo As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellHogDep As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellHogDepVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellHogMun As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellHogMunVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow3 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellHogAld As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellHogAldVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellHogCas As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellHogCasVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow4 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellHogDir As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellHogDirVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow5 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellHogUmb As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellHogUmbVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellHogEst As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellHogEstVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableInfTitu As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow6 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellTituTitulo As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow7 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellTituCod As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellTituCodVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellTituIdenVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow8 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellTituNo1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellTituNo1Val As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellTituNo2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow9 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellTituApe2Val As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow10 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellTituFecNa As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellTituEdadVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellTituIden As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellTituNo2Val As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellTituApe1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellTituApe1Val As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellTituApe2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents SubBand2 As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents SubBand3 As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents SubBand4 As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents XrTableInfBenef As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow11 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellBenefTitulo As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow12 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellBenefCod As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefCodVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefIden As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefIdenVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow13 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellBenefNo1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefNo1Val As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefNo2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefNo2Val As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow14 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellBenefApe1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefApe1Val As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefApe2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefApe2Val As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow15 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellBenefFecNa As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefEdad As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefFecNaVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefEdadVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellTituFecNaVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellTituEdad As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow16 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellTituSex As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellTituSexVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellTituEst As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellTituEstVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow19 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellBenefSex As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefSexVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefEst As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefEstVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow18 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellBenefCiclo As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefCicloVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefHog As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCellBenefHogVal As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCorrSal As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow17 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCellCorrSalTitu As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrLabelTituFecha As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabelValFecha As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents SubBand5 As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents XrLabelValBenef As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabelTituBenef As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabelValUsu As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabelTituUsu As DevExpress.XtraReports.UI.XRLabel

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(cl_xtra_report_busq_corres_benef))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrTableInfHog = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellHogTitulo = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellHogDep = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellHogDepVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellHogMun = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellHogMunVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow3 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellHogAld = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellHogAldVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellHogCas = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellHogCasVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow4 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellHogDir = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellHogDirVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow5 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellHogUmb = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellHogUmbVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellHogEst = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellHogEstVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.SubBand1 = New DevExpress.XtraReports.UI.SubBand()
        Me.XrTableInfTitu = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow6 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellTituTitulo = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow7 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellTituCod = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellTituCodVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellTituIden = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellTituIdenVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow8 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellTituNo1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellTituNo1Val = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellTituNo2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellTituNo2Val = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow9 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellTituApe1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellTituApe1Val = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellTituApe2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellTituApe2Val = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow10 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellTituFecNa = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellTituFecNaVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellTituEdad = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellTituEdadVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow16 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellTituSex = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellTituSexVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellTituEst = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellTituEstVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.SubBand2 = New DevExpress.XtraReports.UI.SubBand()
        Me.XrTableInfBenef = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow11 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellBenefTitulo = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow12 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellBenefCod = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefCodVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefIden = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefIdenVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow13 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellBenefNo1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefNo1Val = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefNo2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefNo2Val = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow14 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellBenefApe1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefApe1Val = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefApe2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefApe2Val = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow15 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellBenefFecNa = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefFecNaVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefEdad = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefEdadVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow19 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellBenefSex = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefSexVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefEst = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefEstVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow18 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellBenefCiclo = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefCicloVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefHog = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCellBenefHogVal = New DevExpress.XtraReports.UI.XRTableCell()
        Me.SubBand3 = New DevExpress.XtraReports.UI.SubBand()
        Me.SubBand4 = New DevExpress.XtraReports.UI.SubBand()
        Me.SubBand5 = New DevExpress.XtraReports.UI.SubBand()
        Me.XrTableCorrSal = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow17 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCellCorrSalTitu = New DevExpress.XtraReports.UI.XRTableCell()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.XrLabelValUsu = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabelTituUsu = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabelValBenef = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabelTituBenef = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabelValFecha = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabelTituFecha = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPictureBoxDistintivoSSIS = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrLabelTextoPrincipal = New DevExpress.XtraReports.UI.XRLabel()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrLabelNomReport = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        CType(Me.XrTableInfHog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTableInfTitu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTableInfBenef, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTableCorrSal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTableInfHog})
        Me.Detail.Dpi = 100.0!
        Me.Detail.HeightF = 117.2562!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.StylePriority.UseBorderDashStyle = False
        Me.Detail.SubBands.AddRange(New DevExpress.XtraReports.UI.SubBand() {Me.SubBand1, Me.SubBand2, Me.SubBand3, Me.SubBand4, Me.SubBand5})
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTableInfHog
        '
        Me.XrTableInfHog.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableInfHog.Dpi = 100.0!
        Me.XrTableInfHog.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableInfHog.LocationFloat = New DevExpress.Utils.PointFloat(76.33171!, 0!)
        Me.XrTableInfHog.Name = "XrTableInfHog"
        Me.XrTableInfHog.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1, Me.XrTableRow2, Me.XrTableRow3, Me.XrTableRow4, Me.XrTableRow5})
        Me.XrTableInfHog.SizeF = New System.Drawing.SizeF(597.4493!, 107.2562!)
        Me.XrTableInfHog.StylePriority.UseBorders = False
        Me.XrTableInfHog.StylePriority.UseFont = False
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellHogTitulo})
        Me.XrTableRow1.Dpi = 100.0!
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1.0R
        '
        'XrTableCellHogTitulo
        '
        Me.XrTableCellHogTitulo.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellHogTitulo.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCellHogTitulo.Dpi = 100.0!
        Me.XrTableCellHogTitulo.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellHogTitulo.Name = "XrTableCellHogTitulo"
        Me.XrTableCellHogTitulo.StylePriority.UseBackColor = False
        Me.XrTableCellHogTitulo.StylePriority.UseBorders = False
        Me.XrTableCellHogTitulo.StylePriority.UseFont = False
        Me.XrTableCellHogTitulo.StylePriority.UseTextAlignment = False
        Me.XrTableCellHogTitulo.Text = "Datos del Hogar"
        Me.XrTableCellHogTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellHogTitulo.Weight = 3.0R
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellHogDep, Me.XrTableCellHogDepVal, Me.XrTableCellHogMun, Me.XrTableCellHogMunVal})
        Me.XrTableRow2.Dpi = 100.0!
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1.0R
        '
        'XrTableCellHogDep
        '
        Me.XrTableCellHogDep.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellHogDep.Dpi = 100.0!
        Me.XrTableCellHogDep.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellHogDep.Name = "XrTableCellHogDep"
        Me.XrTableCellHogDep.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellHogDep.StylePriority.UseBackColor = False
        Me.XrTableCellHogDep.StylePriority.UseFont = False
        Me.XrTableCellHogDep.StylePriority.UsePadding = False
        Me.XrTableCellHogDep.StylePriority.UseTextAlignment = False
        Me.XrTableCellHogDep.Text = "Departamento"
        Me.XrTableCellHogDep.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellHogDep.Weight = 0.59304945725661062R
        '
        'XrTableCellHogDepVal
        '
        Me.XrTableCellHogDepVal.Dpi = 100.0!
        Me.XrTableCellHogDepVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellHogDepVal.Name = "XrTableCellHogDepVal"
        Me.XrTableCellHogDepVal.StylePriority.UseFont = False
        Me.XrTableCellHogDepVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellHogDepVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellHogDepVal.Weight = 1.4069505427433895R
        '
        'XrTableCellHogMun
        '
        Me.XrTableCellHogMun.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellHogMun.Dpi = 100.0!
        Me.XrTableCellHogMun.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellHogMun.Name = "XrTableCellHogMun"
        Me.XrTableCellHogMun.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellHogMun.StylePriority.UseBackColor = False
        Me.XrTableCellHogMun.StylePriority.UseFont = False
        Me.XrTableCellHogMun.StylePriority.UsePadding = False
        Me.XrTableCellHogMun.StylePriority.UseTextAlignment = False
        Me.XrTableCellHogMun.Text = "Municipio"
        Me.XrTableCellHogMun.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellHogMun.Weight = 0.44871807391826923R
        '
        'XrTableCellHogMunVal
        '
        Me.XrTableCellHogMunVal.Dpi = 100.0!
        Me.XrTableCellHogMunVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellHogMunVal.Name = "XrTableCellHogMunVal"
        Me.XrTableCellHogMunVal.StylePriority.UseFont = False
        Me.XrTableCellHogMunVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellHogMunVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellHogMunVal.Weight = 1.5512819260817308R
        '
        'XrTableRow3
        '
        Me.XrTableRow3.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellHogAld, Me.XrTableCellHogAldVal, Me.XrTableCellHogCas, Me.XrTableCellHogCasVal})
        Me.XrTableRow3.Dpi = 100.0!
        Me.XrTableRow3.Name = "XrTableRow3"
        Me.XrTableRow3.Weight = 1.0R
        '
        'XrTableCellHogAld
        '
        Me.XrTableCellHogAld.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellHogAld.Dpi = 100.0!
        Me.XrTableCellHogAld.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellHogAld.Name = "XrTableCellHogAld"
        Me.XrTableCellHogAld.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellHogAld.StylePriority.UseBackColor = False
        Me.XrTableCellHogAld.StylePriority.UseFont = False
        Me.XrTableCellHogAld.StylePriority.UsePadding = False
        Me.XrTableCellHogAld.StylePriority.UseTextAlignment = False
        Me.XrTableCellHogAld.Text = "Aldea"
        Me.XrTableCellHogAld.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellHogAld.Weight = 0.59304945725661051R
        '
        'XrTableCellHogAldVal
        '
        Me.XrTableCellHogAldVal.Dpi = 100.0!
        Me.XrTableCellHogAldVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellHogAldVal.Name = "XrTableCellHogAldVal"
        Me.XrTableCellHogAldVal.StylePriority.UseFont = False
        Me.XrTableCellHogAldVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellHogAldVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellHogAldVal.Weight = 1.4069505427433895R
        '
        'XrTableCellHogCas
        '
        Me.XrTableCellHogCas.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellHogCas.Dpi = 100.0!
        Me.XrTableCellHogCas.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellHogCas.Name = "XrTableCellHogCas"
        Me.XrTableCellHogCas.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellHogCas.StylePriority.UseBackColor = False
        Me.XrTableCellHogCas.StylePriority.UseFont = False
        Me.XrTableCellHogCas.StylePriority.UsePadding = False
        Me.XrTableCellHogCas.StylePriority.UseTextAlignment = False
        Me.XrTableCellHogCas.Text = "Caserío"
        Me.XrTableCellHogCas.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellHogCas.Weight = 0.44871807391826918R
        '
        'XrTableCellHogCasVal
        '
        Me.XrTableCellHogCasVal.Dpi = 100.0!
        Me.XrTableCellHogCasVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellHogCasVal.Name = "XrTableCellHogCasVal"
        Me.XrTableCellHogCasVal.StylePriority.UseFont = False
        Me.XrTableCellHogCasVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellHogCasVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellHogCasVal.Weight = 1.5512819260817308R
        '
        'XrTableRow4
        '
        Me.XrTableRow4.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellHogDir, Me.XrTableCellHogDirVal})
        Me.XrTableRow4.Dpi = 100.0!
        Me.XrTableRow4.Name = "XrTableRow4"
        Me.XrTableRow4.Weight = 1.0R
        '
        'XrTableCellHogDir
        '
        Me.XrTableCellHogDir.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellHogDir.Dpi = 100.0!
        Me.XrTableCellHogDir.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellHogDir.Name = "XrTableCellHogDir"
        Me.XrTableCellHogDir.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellHogDir.StylePriority.UseBackColor = False
        Me.XrTableCellHogDir.StylePriority.UseFont = False
        Me.XrTableCellHogDir.StylePriority.UsePadding = False
        Me.XrTableCellHogDir.StylePriority.UseTextAlignment = False
        Me.XrTableCellHogDir.Text = "Dirección"
        Me.XrTableCellHogDir.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellHogDir.Weight = 0.59304945725661051R
        '
        'XrTableCellHogDirVal
        '
        Me.XrTableCellHogDirVal.Dpi = 100.0!
        Me.XrTableCellHogDirVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellHogDirVal.Name = "XrTableCellHogDirVal"
        Me.XrTableCellHogDirVal.StylePriority.UseFont = False
        Me.XrTableCellHogDirVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellHogDirVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellHogDirVal.Weight = 3.4069505427433895R
        '
        'XrTableRow5
        '
        Me.XrTableRow5.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellHogUmb, Me.XrTableCellHogUmbVal, Me.XrTableCellHogEst, Me.XrTableCellHogEstVal})
        Me.XrTableRow5.Dpi = 100.0!
        Me.XrTableRow5.Name = "XrTableRow5"
        Me.XrTableRow5.Weight = 1.0R
        '
        'XrTableCellHogUmb
        '
        Me.XrTableCellHogUmb.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellHogUmb.Dpi = 100.0!
        Me.XrTableCellHogUmb.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellHogUmb.Name = "XrTableCellHogUmb"
        Me.XrTableCellHogUmb.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellHogUmb.StylePriority.UseBackColor = False
        Me.XrTableCellHogUmb.StylePriority.UseFont = False
        Me.XrTableCellHogUmb.StylePriority.UsePadding = False
        Me.XrTableCellHogUmb.StylePriority.UseTextAlignment = False
        Me.XrTableCellHogUmb.Text = "Umbral"
        Me.XrTableCellHogUmb.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellHogUmb.Weight = 0.59304945725661051R
        '
        'XrTableCellHogUmbVal
        '
        Me.XrTableCellHogUmbVal.Dpi = 100.0!
        Me.XrTableCellHogUmbVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellHogUmbVal.Name = "XrTableCellHogUmbVal"
        Me.XrTableCellHogUmbVal.StylePriority.UseFont = False
        Me.XrTableCellHogUmbVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellHogUmbVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellHogUmbVal.Weight = 1.4069505427433895R
        '
        'XrTableCellHogEst
        '
        Me.XrTableCellHogEst.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellHogEst.Dpi = 100.0!
        Me.XrTableCellHogEst.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellHogEst.Name = "XrTableCellHogEst"
        Me.XrTableCellHogEst.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellHogEst.StylePriority.UseBackColor = False
        Me.XrTableCellHogEst.StylePriority.UseFont = False
        Me.XrTableCellHogEst.StylePriority.UsePadding = False
        Me.XrTableCellHogEst.StylePriority.UseTextAlignment = False
        Me.XrTableCellHogEst.Text = "Estado"
        Me.XrTableCellHogEst.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellHogEst.Weight = 0.44871807391826918R
        '
        'XrTableCellHogEstVal
        '
        Me.XrTableCellHogEstVal.Dpi = 100.0!
        Me.XrTableCellHogEstVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellHogEstVal.Name = "XrTableCellHogEstVal"
        Me.XrTableCellHogEstVal.StylePriority.UseFont = False
        Me.XrTableCellHogEstVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellHogEstVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellHogEstVal.Weight = 1.5512819260817308R
        '
        'SubBand1
        '
        Me.SubBand1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTableInfTitu})
        Me.SubBand1.Dpi = 100.0!
        Me.SubBand1.HeightF = 151.9036!
        Me.SubBand1.Name = "SubBand1"
        '
        'XrTableInfTitu
        '
        Me.XrTableInfTitu.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableInfTitu.Dpi = 100.0!
        Me.XrTableInfTitu.LocationFloat = New DevExpress.Utils.PointFloat(76.33172!, 10.0!)
        Me.XrTableInfTitu.Name = "XrTableInfTitu"
        Me.XrTableInfTitu.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow6, Me.XrTableRow7, Me.XrTableRow8, Me.XrTableRow9, Me.XrTableRow10, Me.XrTableRow16})
        Me.XrTableInfTitu.SizeF = New System.Drawing.SizeF(597.809!, 129.2588!)
        Me.XrTableInfTitu.StylePriority.UseBorders = False
        '
        'XrTableRow6
        '
        Me.XrTableRow6.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellTituTitulo})
        Me.XrTableRow6.Dpi = 100.0!
        Me.XrTableRow6.Name = "XrTableRow6"
        Me.XrTableRow6.Weight = 1.0R
        '
        'XrTableCellTituTitulo
        '
        Me.XrTableCellTituTitulo.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellTituTitulo.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCellTituTitulo.Dpi = 100.0!
        Me.XrTableCellTituTitulo.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituTitulo.Name = "XrTableCellTituTitulo"
        Me.XrTableCellTituTitulo.StylePriority.UseBackColor = False
        Me.XrTableCellTituTitulo.StylePriority.UseBorders = False
        Me.XrTableCellTituTitulo.StylePriority.UseFont = False
        Me.XrTableCellTituTitulo.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituTitulo.Text = "Datos del Titular"
        Me.XrTableCellTituTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellTituTitulo.Weight = 3.0R
        '
        'XrTableRow7
        '
        Me.XrTableRow7.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellTituCod, Me.XrTableCellTituCodVal, Me.XrTableCellTituIden, Me.XrTableCellTituIdenVal})
        Me.XrTableRow7.Dpi = 100.0!
        Me.XrTableRow7.Name = "XrTableRow7"
        Me.XrTableRow7.Weight = 1.0R
        '
        'XrTableCellTituCod
        '
        Me.XrTableCellTituCod.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellTituCod.Dpi = 100.0!
        Me.XrTableCellTituCod.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituCod.Name = "XrTableCellTituCod"
        Me.XrTableCellTituCod.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellTituCod.StylePriority.UseBackColor = False
        Me.XrTableCellTituCod.StylePriority.UseFont = False
        Me.XrTableCellTituCod.StylePriority.UsePadding = False
        Me.XrTableCellTituCod.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituCod.Text = "Código"
        Me.XrTableCellTituCod.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellTituCod.Weight = 1.7758957279110983R
        '
        'XrTableCellTituCodVal
        '
        Me.XrTableCellTituCodVal.Dpi = 100.0!
        Me.XrTableCellTituCodVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituCodVal.Name = "XrTableCellTituCodVal"
        Me.XrTableCellTituCodVal.StylePriority.UseFont = False
        Me.XrTableCellTituCodVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituCodVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellTituCodVal.Weight = 3.0163141793078529R
        '
        'XrTableCellTituIden
        '
        Me.XrTableCellTituIden.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellTituIden.Dpi = 100.0!
        Me.XrTableCellTituIden.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituIden.Name = "XrTableCellTituIden"
        Me.XrTableCellTituIden.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellTituIden.StylePriority.UseBackColor = False
        Me.XrTableCellTituIden.StylePriority.UseFont = False
        Me.XrTableCellTituIden.StylePriority.UsePadding = False
        Me.XrTableCellTituIden.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituIden.Text = "Identidad"
        Me.XrTableCellTituIden.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellTituIden.Weight = 1.7495621655617937R
        '
        'XrTableCellTituIdenVal
        '
        Me.XrTableCellTituIdenVal.Dpi = 100.0!
        Me.XrTableCellTituIdenVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituIdenVal.Name = "XrTableCellTituIdenVal"
        Me.XrTableCellTituIdenVal.StylePriority.UseFont = False
        Me.XrTableCellTituIdenVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituIdenVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellTituIdenVal.Weight = 3.1815610048718068R
        '
        'XrTableRow8
        '
        Me.XrTableRow8.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellTituNo1, Me.XrTableCellTituNo1Val, Me.XrTableCellTituNo2, Me.XrTableCellTituNo2Val})
        Me.XrTableRow8.Dpi = 100.0!
        Me.XrTableRow8.Name = "XrTableRow8"
        Me.XrTableRow8.Weight = 1.0R
        '
        'XrTableCellTituNo1
        '
        Me.XrTableCellTituNo1.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellTituNo1.Dpi = 100.0!
        Me.XrTableCellTituNo1.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituNo1.Name = "XrTableCellTituNo1"
        Me.XrTableCellTituNo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellTituNo1.StylePriority.UseBackColor = False
        Me.XrTableCellTituNo1.StylePriority.UseFont = False
        Me.XrTableCellTituNo1.StylePriority.UsePadding = False
        Me.XrTableCellTituNo1.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituNo1.Text = "Primer Nombre"
        Me.XrTableCellTituNo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellTituNo1.Weight = 1.7117740700592039R
        '
        'XrTableCellTituNo1Val
        '
        Me.XrTableCellTituNo1Val.Dpi = 100.0!
        Me.XrTableCellTituNo1Val.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituNo1Val.Name = "XrTableCellTituNo1Val"
        Me.XrTableCellTituNo1Val.StylePriority.UseFont = False
        Me.XrTableCellTituNo1Val.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituNo1Val.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellTituNo1Val.Weight = 2.9074050894846906R
        '
        'XrTableCellTituNo2
        '
        Me.XrTableCellTituNo2.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellTituNo2.Dpi = 100.0!
        Me.XrTableCellTituNo2.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituNo2.Name = "XrTableCellTituNo2"
        Me.XrTableCellTituNo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellTituNo2.StylePriority.UseBackColor = False
        Me.XrTableCellTituNo2.StylePriority.UseFont = False
        Me.XrTableCellTituNo2.StylePriority.UsePadding = False
        Me.XrTableCellTituNo2.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituNo2.Text = "Segundo Nombre"
        Me.XrTableCellTituNo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellTituNo2.Weight = 1.6863913426905262R
        '
        'XrTableCellTituNo2Val
        '
        Me.XrTableCellTituNo2Val.Dpi = 100.0!
        Me.XrTableCellTituNo2Val.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituNo2Val.Name = "XrTableCellTituNo2Val"
        Me.XrTableCellTituNo2Val.StylePriority.UseFont = False
        Me.XrTableCellTituNo2Val.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituNo2Val.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellTituNo2Val.Weight = 3.0666851959217061R
        '
        'XrTableRow9
        '
        Me.XrTableRow9.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellTituApe1, Me.XrTableCellTituApe1Val, Me.XrTableCellTituApe2, Me.XrTableCellTituApe2Val})
        Me.XrTableRow9.Dpi = 100.0!
        Me.XrTableRow9.Name = "XrTableRow9"
        Me.XrTableRow9.Weight = 1.0R
        '
        'XrTableCellTituApe1
        '
        Me.XrTableCellTituApe1.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellTituApe1.Dpi = 100.0!
        Me.XrTableCellTituApe1.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituApe1.Name = "XrTableCellTituApe1"
        Me.XrTableCellTituApe1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellTituApe1.StylePriority.UseBackColor = False
        Me.XrTableCellTituApe1.StylePriority.UseFont = False
        Me.XrTableCellTituApe1.StylePriority.UsePadding = False
        Me.XrTableCellTituApe1.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituApe1.Text = "Primer Apellido"
        Me.XrTableCellTituApe1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellTituApe1.Weight = 3.9978569181723107R
        '
        'XrTableCellTituApe1Val
        '
        Me.XrTableCellTituApe1Val.Dpi = 100.0!
        Me.XrTableCellTituApe1Val.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituApe1Val.Name = "XrTableCellTituApe1Val"
        Me.XrTableCellTituApe1Val.StylePriority.UseFont = False
        Me.XrTableCellTituApe1Val.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituApe1Val.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellTituApe1Val.Weight = 6.7737524517162564R
        '
        'XrTableCellTituApe2
        '
        Me.XrTableCellTituApe2.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellTituApe2.Dpi = 100.0!
        Me.XrTableCellTituApe2.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituApe2.Name = "XrTableCellTituApe2"
        Me.XrTableCellTituApe2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellTituApe2.StylePriority.UseBackColor = False
        Me.XrTableCellTituApe2.StylePriority.UseFont = False
        Me.XrTableCellTituApe2.StylePriority.UsePadding = False
        Me.XrTableCellTituApe2.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituApe2.Text = "Segundo Apellido"
        Me.XrTableCellTituApe2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellTituApe2.Weight = 3.95508180851051R
        '
        'XrTableCellTituApe2Val
        '
        Me.XrTableCellTituApe2Val.Dpi = 100.0!
        Me.XrTableCellTituApe2Val.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituApe2Val.Name = "XrTableCellTituApe2Val"
        Me.XrTableCellTituApe2Val.StylePriority.UseFont = False
        Me.XrTableCellTituApe2Val.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituApe2Val.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellTituApe2Val.Weight = 7.1622581230553672R
        '
        'XrTableRow10
        '
        Me.XrTableRow10.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellTituFecNa, Me.XrTableCellTituFecNaVal, Me.XrTableCellTituEdad, Me.XrTableCellTituEdadVal})
        Me.XrTableRow10.Dpi = 100.0!
        Me.XrTableRow10.Name = "XrTableRow10"
        Me.XrTableRow10.Weight = 1.0R
        '
        'XrTableCellTituFecNa
        '
        Me.XrTableCellTituFecNa.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellTituFecNa.Dpi = 100.0!
        Me.XrTableCellTituFecNa.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituFecNa.Name = "XrTableCellTituFecNa"
        Me.XrTableCellTituFecNa.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellTituFecNa.StylePriority.UseBackColor = False
        Me.XrTableCellTituFecNa.StylePriority.UseFont = False
        Me.XrTableCellTituFecNa.StylePriority.UsePadding = False
        Me.XrTableCellTituFecNa.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituFecNa.Text = "FechaNacimiento"
        Me.XrTableCellTituFecNa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellTituFecNa.Weight = 4.251984172964292R
        '
        'XrTableCellTituFecNaVal
        '
        Me.XrTableCellTituFecNaVal.Dpi = 100.0!
        Me.XrTableCellTituFecNaVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituFecNaVal.Name = "XrTableCellTituFecNaVal"
        Me.XrTableCellTituFecNaVal.StylePriority.UseFont = False
        Me.XrTableCellTituFecNaVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituFecNaVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellTituFecNaVal.Weight = 7.20433277556212R
        '
        'XrTableCellTituEdad
        '
        Me.XrTableCellTituEdad.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellTituEdad.Dpi = 100.0!
        Me.XrTableCellTituEdad.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituEdad.Name = "XrTableCellTituEdad"
        Me.XrTableCellTituEdad.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellTituEdad.StylePriority.UseBackColor = False
        Me.XrTableCellTituEdad.StylePriority.UseFont = False
        Me.XrTableCellTituEdad.StylePriority.UsePadding = False
        Me.XrTableCellTituEdad.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituEdad.Text = "Edad"
        Me.XrTableCellTituEdad.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellTituEdad.Weight = 4.20649011323936R
        '
        'XrTableCellTituEdadVal
        '
        Me.XrTableCellTituEdadVal.Dpi = 100.0!
        Me.XrTableCellTituEdadVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituEdadVal.Name = "XrTableCellTituEdadVal"
        Me.XrTableCellTituEdadVal.StylePriority.UseFont = False
        Me.XrTableCellTituEdadVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituEdadVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellTituEdadVal.Weight = 7.6175324715782882R
        '
        'XrTableRow16
        '
        Me.XrTableRow16.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellTituSex, Me.XrTableCellTituSexVal, Me.XrTableCellTituEst, Me.XrTableCellTituEstVal})
        Me.XrTableRow16.Dpi = 100.0!
        Me.XrTableRow16.Name = "XrTableRow16"
        Me.XrTableRow16.Weight = 1.0R
        '
        'XrTableCellTituSex
        '
        Me.XrTableCellTituSex.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellTituSex.Dpi = 100.0!
        Me.XrTableCellTituSex.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituSex.Name = "XrTableCellTituSex"
        Me.XrTableCellTituSex.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellTituSex.StylePriority.UseBackColor = False
        Me.XrTableCellTituSex.StylePriority.UseFont = False
        Me.XrTableCellTituSex.StylePriority.UsePadding = False
        Me.XrTableCellTituSex.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituSex.Text = "Sexo"
        Me.XrTableCellTituSex.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellTituSex.Weight = 4.2519841729642911R
        '
        'XrTableCellTituSexVal
        '
        Me.XrTableCellTituSexVal.Dpi = 100.0!
        Me.XrTableCellTituSexVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituSexVal.Name = "XrTableCellTituSexVal"
        Me.XrTableCellTituSexVal.StylePriority.UseFont = False
        Me.XrTableCellTituSexVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituSexVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellTituSexVal.Weight = 7.2043314708057737R
        '
        'XrTableCellTituEst
        '
        Me.XrTableCellTituEst.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellTituEst.Dpi = 100.0!
        Me.XrTableCellTituEst.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituEst.Name = "XrTableCellTituEst"
        Me.XrTableCellTituEst.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellTituEst.StylePriority.UseBackColor = False
        Me.XrTableCellTituEst.StylePriority.UseFont = False
        Me.XrTableCellTituEst.StylePriority.UsePadding = False
        Me.XrTableCellTituEst.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituEst.Text = "Estado"
        Me.XrTableCellTituEst.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellTituEst.Weight = 4.2064901638164391R
        '
        'XrTableCellTituEstVal
        '
        Me.XrTableCellTituEstVal.Dpi = 100.0!
        Me.XrTableCellTituEstVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellTituEstVal.Name = "XrTableCellTituEstVal"
        Me.XrTableCellTituEstVal.StylePriority.UseFont = False
        Me.XrTableCellTituEstVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellTituEstVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellTituEstVal.Weight = 7.6175337257575553R
        '
        'SubBand2
        '
        Me.SubBand2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTableInfBenef})
        Me.SubBand2.Dpi = 100.0!
        Me.SubBand2.HeightF = 186.4829!
        Me.SubBand2.Name = "SubBand2"
        '
        'XrTableInfBenef
        '
        Me.XrTableInfBenef.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableInfBenef.Dpi = 100.0!
        Me.XrTableInfBenef.LocationFloat = New DevExpress.Utils.PointFloat(77.02475!, 9.999974!)
        Me.XrTableInfBenef.Name = "XrTableInfBenef"
        Me.XrTableInfBenef.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow11, Me.XrTableRow12, Me.XrTableRow13, Me.XrTableRow14, Me.XrTableRow15, Me.XrTableRow19, Me.XrTableRow18})
        Me.XrTableInfBenef.SizeF = New System.Drawing.SizeF(597.116!, 163.6527!)
        Me.XrTableInfBenef.StylePriority.UseBorders = False
        '
        'XrTableRow11
        '
        Me.XrTableRow11.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellBenefTitulo})
        Me.XrTableRow11.Dpi = 100.0!
        Me.XrTableRow11.Name = "XrTableRow11"
        Me.XrTableRow11.Weight = 1.0R
        '
        'XrTableCellBenefTitulo
        '
        Me.XrTableCellBenefTitulo.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellBenefTitulo.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCellBenefTitulo.Dpi = 100.0!
        Me.XrTableCellBenefTitulo.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefTitulo.Name = "XrTableCellBenefTitulo"
        Me.XrTableCellBenefTitulo.StylePriority.UseBackColor = False
        Me.XrTableCellBenefTitulo.StylePriority.UseBorders = False
        Me.XrTableCellBenefTitulo.StylePriority.UseFont = False
        Me.XrTableCellBenefTitulo.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefTitulo.Text = "Datos del Beneficiario"
        Me.XrTableCellBenefTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellBenefTitulo.Weight = 6.0R
        '
        'XrTableRow12
        '
        Me.XrTableRow12.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellBenefCod, Me.XrTableCellBenefCodVal, Me.XrTableCellBenefIden, Me.XrTableCellBenefIdenVal})
        Me.XrTableRow12.Dpi = 100.0!
        Me.XrTableRow12.Name = "XrTableRow12"
        Me.XrTableRow12.Weight = 1.0R
        '
        'XrTableCellBenefCod
        '
        Me.XrTableCellBenefCod.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellBenefCod.Dpi = 100.0!
        Me.XrTableCellBenefCod.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefCod.Name = "XrTableCellBenefCod"
        Me.XrTableCellBenefCod.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellBenefCod.StylePriority.UseBackColor = False
        Me.XrTableCellBenefCod.StylePriority.UseFont = False
        Me.XrTableCellBenefCod.StylePriority.UsePadding = False
        Me.XrTableCellBenefCod.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefCod.Text = "Código"
        Me.XrTableCellBenefCod.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellBenefCod.Weight = 1.7833453862743431R
        '
        'XrTableCellBenefCodVal
        '
        Me.XrTableCellBenefCodVal.Dpi = 100.0!
        Me.XrTableCellBenefCodVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefCodVal.Name = "XrTableCellBenefCodVal"
        Me.XrTableCellBenefCodVal.StylePriority.UseFont = False
        Me.XrTableCellBenefCodVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefCodVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellBenefCodVal.Weight = 3.0216039397499115R
        '
        'XrTableCellBenefIden
        '
        Me.XrTableCellBenefIden.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellBenefIden.Dpi = 100.0!
        Me.XrTableCellBenefIden.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefIden.Name = "XrTableCellBenefIden"
        Me.XrTableCellBenefIden.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellBenefIden.StylePriority.UseBackColor = False
        Me.XrTableCellBenefIden.StylePriority.UseFont = False
        Me.XrTableCellBenefIden.StylePriority.UsePadding = False
        Me.XrTableCellBenefIden.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefIden.Text = "Identidad"
        Me.XrTableCellBenefIden.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellBenefIden.Weight = 1.7642649752304598R
        '
        'XrTableCellBenefIdenVal
        '
        Me.XrTableCellBenefIdenVal.Dpi = 100.0!
        Me.XrTableCellBenefIdenVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefIdenVal.Name = "XrTableCellBenefIdenVal"
        Me.XrTableCellBenefIdenVal.StylePriority.UseFont = False
        Me.XrTableCellBenefIdenVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefIdenVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellBenefIdenVal.Weight = 3.1949067725565525R
        '
        'XrTableRow13
        '
        Me.XrTableRow13.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellBenefNo1, Me.XrTableCellBenefNo1Val, Me.XrTableCellBenefNo2, Me.XrTableCellBenefNo2Val})
        Me.XrTableRow13.Dpi = 100.0!
        Me.XrTableRow13.Name = "XrTableRow13"
        Me.XrTableRow13.Weight = 1.0R
        '
        'XrTableCellBenefNo1
        '
        Me.XrTableCellBenefNo1.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellBenefNo1.Dpi = 100.0!
        Me.XrTableCellBenefNo1.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefNo1.Name = "XrTableCellBenefNo1"
        Me.XrTableCellBenefNo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellBenefNo1.StylePriority.UseBackColor = False
        Me.XrTableCellBenefNo1.StylePriority.UseFont = False
        Me.XrTableCellBenefNo1.StylePriority.UsePadding = False
        Me.XrTableCellBenefNo1.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefNo1.Text = "Primer Nombre"
        Me.XrTableCellBenefNo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellBenefNo1.Weight = 1.6161955693316272R
        '
        'XrTableCellBenefNo1Val
        '
        Me.XrTableCellBenefNo1Val.Dpi = 100.0!
        Me.XrTableCellBenefNo1Val.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefNo1Val.Name = "XrTableCellBenefNo1Val"
        Me.XrTableCellBenefNo1Val.StylePriority.UseFont = False
        Me.XrTableCellBenefNo1Val.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefNo1Val.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellBenefNo1Val.Weight = 2.7383957110141575R
        '
        'XrTableCellBenefNo2
        '
        Me.XrTableCellBenefNo2.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellBenefNo2.Dpi = 100.0!
        Me.XrTableCellBenefNo2.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefNo2.Name = "XrTableCellBenefNo2"
        Me.XrTableCellBenefNo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellBenefNo2.StylePriority.UseBackColor = False
        Me.XrTableCellBenefNo2.StylePriority.UseFont = False
        Me.XrTableCellBenefNo2.StylePriority.UsePadding = False
        Me.XrTableCellBenefNo2.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefNo2.Text = "Segundo Nombre"
        Me.XrTableCellBenefNo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellBenefNo2.Weight = 1.5989024072525209R
        '
        'XrTableCellBenefNo2Val
        '
        Me.XrTableCellBenefNo2Val.Dpi = 100.0!
        Me.XrTableCellBenefNo2Val.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefNo2Val.Name = "XrTableCellBenefNo2Val"
        Me.XrTableCellBenefNo2Val.StylePriority.UseFont = False
        Me.XrTableCellBenefNo2Val.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefNo2Val.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellBenefNo2Val.Weight = 2.8954538000566208R
        '
        'XrTableRow14
        '
        Me.XrTableRow14.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellBenefApe1, Me.XrTableCellBenefApe1Val, Me.XrTableCellBenefApe2, Me.XrTableCellBenefApe2Val})
        Me.XrTableRow14.Dpi = 100.0!
        Me.XrTableRow14.Name = "XrTableRow14"
        Me.XrTableRow14.Weight = 1.0R
        '
        'XrTableCellBenefApe1
        '
        Me.XrTableCellBenefApe1.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellBenefApe1.Dpi = 100.0!
        Me.XrTableCellBenefApe1.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefApe1.Name = "XrTableCellBenefApe1"
        Me.XrTableCellBenefApe1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellBenefApe1.StylePriority.UseBackColor = False
        Me.XrTableCellBenefApe1.StylePriority.UseFont = False
        Me.XrTableCellBenefApe1.StylePriority.UsePadding = False
        Me.XrTableCellBenefApe1.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefApe1.Text = "Primer Apellido"
        Me.XrTableCellBenefApe1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellBenefApe1.Weight = 4.0146268836880346R
        '
        'XrTableCellBenefApe1Val
        '
        Me.XrTableCellBenefApe1Val.Dpi = 100.0!
        Me.XrTableCellBenefApe1Val.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefApe1Val.Name = "XrTableCellBenefApe1Val"
        Me.XrTableCellBenefApe1Val.StylePriority.UseFont = False
        Me.XrTableCellBenefApe1Val.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefApe1Val.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellBenefApe1Val.Weight = 6.8021657926378509R
        '
        'XrTableCellBenefApe2
        '
        Me.XrTableCellBenefApe2.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellBenefApe2.Dpi = 100.0!
        Me.XrTableCellBenefApe2.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefApe2.Name = "XrTableCellBenefApe2"
        Me.XrTableCellBenefApe2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellBenefApe2.StylePriority.UseBackColor = False
        Me.XrTableCellBenefApe2.StylePriority.UseFont = False
        Me.XrTableCellBenefApe2.StylePriority.UsePadding = False
        Me.XrTableCellBenefApe2.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefApe2.Text = "Segundo Apellido"
        Me.XrTableCellBenefApe2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellBenefApe2.Weight = 3.9716723382359245R
        '
        'XrTableCellBenefApe2Val
        '
        Me.XrTableCellBenefApe2Val.Dpi = 100.0!
        Me.XrTableCellBenefApe2Val.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefApe2Val.Name = "XrTableCellBenefApe2Val"
        Me.XrTableCellBenefApe2Val.StylePriority.UseFont = False
        Me.XrTableCellBenefApe2Val.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefApe2Val.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellBenefApe2Val.Weight = 7.1923018425176926R
        '
        'XrTableRow15
        '
        Me.XrTableRow15.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellBenefFecNa, Me.XrTableCellBenefFecNaVal, Me.XrTableCellBenefEdad, Me.XrTableCellBenefEdadVal})
        Me.XrTableRow15.Dpi = 100.0!
        Me.XrTableRow15.Name = "XrTableRow15"
        Me.XrTableRow15.Weight = 1.0R
        '
        'XrTableCellBenefFecNa
        '
        Me.XrTableCellBenefFecNa.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellBenefFecNa.Dpi = 100.0!
        Me.XrTableCellBenefFecNa.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefFecNa.Name = "XrTableCellBenefFecNa"
        Me.XrTableCellBenefFecNa.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellBenefFecNa.StylePriority.UseBackColor = False
        Me.XrTableCellBenefFecNa.StylePriority.UseFont = False
        Me.XrTableCellBenefFecNa.StylePriority.UsePadding = False
        Me.XrTableCellBenefFecNa.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefFecNa.Text = "Fecha Nacimiento"
        Me.XrTableCellBenefFecNa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellBenefFecNa.Weight = 2.0699508088951997R
        '
        'XrTableCellBenefFecNaVal
        '
        Me.XrTableCellBenefFecNaVal.Dpi = 100.0!
        Me.XrTableCellBenefFecNaVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefFecNaVal.Name = "XrTableCellBenefFecNaVal"
        Me.XrTableCellBenefFecNaVal.StylePriority.UseFont = False
        Me.XrTableCellBenefFecNaVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefFecNaVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellBenefFecNaVal.Weight = 3.5072134629760936R
        '
        'XrTableCellBenefEdad
        '
        Me.XrTableCellBenefEdad.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellBenefEdad.Dpi = 100.0!
        Me.XrTableCellBenefEdad.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefEdad.Name = "XrTableCellBenefEdad"
        Me.XrTableCellBenefEdad.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellBenefEdad.StylePriority.UseBackColor = False
        Me.XrTableCellBenefEdad.StylePriority.UseFont = False
        Me.XrTableCellBenefEdad.StylePriority.UsePadding = False
        Me.XrTableCellBenefEdad.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefEdad.Text = "Edad"
        Me.XrTableCellBenefEdad.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellBenefEdad.Weight = 2.0478021586776034R
        '
        'XrTableCellBenefEdadVal
        '
        Me.XrTableCellBenefEdadVal.Dpi = 100.0!
        Me.XrTableCellBenefEdadVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefEdadVal.Name = "XrTableCellBenefEdadVal"
        Me.XrTableCellBenefEdadVal.StylePriority.UseFont = False
        Me.XrTableCellBenefEdadVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefEdadVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellBenefEdadVal.Weight = 3.7083669340845176R
        '
        'XrTableRow19
        '
        Me.XrTableRow19.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellBenefSex, Me.XrTableCellBenefSexVal, Me.XrTableCellBenefEst, Me.XrTableCellBenefEstVal})
        Me.XrTableRow19.Dpi = 100.0!
        Me.XrTableRow19.Name = "XrTableRow19"
        Me.XrTableRow19.Weight = 1.0R
        '
        'XrTableCellBenefSex
        '
        Me.XrTableCellBenefSex.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellBenefSex.Dpi = 100.0!
        Me.XrTableCellBenefSex.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefSex.Name = "XrTableCellBenefSex"
        Me.XrTableCellBenefSex.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellBenefSex.StylePriority.UseBackColor = False
        Me.XrTableCellBenefSex.StylePriority.UseFont = False
        Me.XrTableCellBenefSex.StylePriority.UsePadding = False
        Me.XrTableCellBenefSex.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefSex.Text = "Sexo"
        Me.XrTableCellBenefSex.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellBenefSex.Weight = 2.0699505428445177R
        '
        'XrTableCellBenefSexVal
        '
        Me.XrTableCellBenefSexVal.Dpi = 100.0!
        Me.XrTableCellBenefSexVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefSexVal.Name = "XrTableCellBenefSexVal"
        Me.XrTableCellBenefSexVal.StylePriority.UseFont = False
        Me.XrTableCellBenefSexVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefSexVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellBenefSexVal.Weight = 3.5072121327226844R
        '
        'XrTableCellBenefEst
        '
        Me.XrTableCellBenefEst.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellBenefEst.Dpi = 100.0!
        Me.XrTableCellBenefEst.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefEst.Name = "XrTableCellBenefEst"
        Me.XrTableCellBenefEst.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellBenefEst.StylePriority.UseBackColor = False
        Me.XrTableCellBenefEst.StylePriority.UseFont = False
        Me.XrTableCellBenefEst.StylePriority.UsePadding = False
        Me.XrTableCellBenefEst.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefEst.Text = "Estado"
        Me.XrTableCellBenefEst.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellBenefEst.Weight = 2.0478032228803307R
        '
        'XrTableCellBenefEstVal
        '
        Me.XrTableCellBenefEstVal.Dpi = 100.0!
        Me.XrTableCellBenefEstVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefEstVal.Name = "XrTableCellBenefEstVal"
        Me.XrTableCellBenefEstVal.StylePriority.UseFont = False
        Me.XrTableCellBenefEstVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefEstVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellBenefEstVal.Weight = 3.7083674661858814R
        '
        'XrTableRow18
        '
        Me.XrTableRow18.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellBenefCiclo, Me.XrTableCellBenefCicloVal, Me.XrTableCellBenefHog, Me.XrTableCellBenefHogVal})
        Me.XrTableRow18.Dpi = 100.0!
        Me.XrTableRow18.Name = "XrTableRow18"
        Me.XrTableRow18.Weight = 1.0R
        '
        'XrTableCellBenefCiclo
        '
        Me.XrTableCellBenefCiclo.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellBenefCiclo.Dpi = 100.0!
        Me.XrTableCellBenefCiclo.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefCiclo.Name = "XrTableCellBenefCiclo"
        Me.XrTableCellBenefCiclo.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellBenefCiclo.StylePriority.UseBackColor = False
        Me.XrTableCellBenefCiclo.StylePriority.UseFont = False
        Me.XrTableCellBenefCiclo.StylePriority.UsePadding = False
        Me.XrTableCellBenefCiclo.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefCiclo.Text = "Ciclo"
        Me.XrTableCellBenefCiclo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellBenefCiclo.Weight = 2.0699505428445177R
        '
        'XrTableCellBenefCicloVal
        '
        Me.XrTableCellBenefCicloVal.Dpi = 100.0!
        Me.XrTableCellBenefCicloVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefCicloVal.Name = "XrTableCellBenefCicloVal"
        Me.XrTableCellBenefCicloVal.StylePriority.UseFont = False
        Me.XrTableCellBenefCicloVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefCicloVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellBenefCicloVal.Weight = 3.5072116006213205R
        '
        'XrTableCellBenefHog
        '
        Me.XrTableCellBenefHog.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellBenefHog.Dpi = 100.0!
        Me.XrTableCellBenefHog.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefHog.Name = "XrTableCellBenefHog"
        Me.XrTableCellBenefHog.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCellBenefHog.StylePriority.UseBackColor = False
        Me.XrTableCellBenefHog.StylePriority.UseFont = False
        Me.XrTableCellBenefHog.StylePriority.UsePadding = False
        Me.XrTableCellBenefHog.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefHog.Text = "Hogar"
        Me.XrTableCellBenefHog.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCellBenefHog.Weight = 2.0478042870830584R
        '
        'XrTableCellBenefHogVal
        '
        Me.XrTableCellBenefHogVal.Dpi = 100.0!
        Me.XrTableCellBenefHogVal.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellBenefHogVal.Name = "XrTableCellBenefHogVal"
        Me.XrTableCellBenefHogVal.StylePriority.UseFont = False
        Me.XrTableCellBenefHogVal.StylePriority.UseTextAlignment = False
        Me.XrTableCellBenefHogVal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellBenefHogVal.Weight = 3.7083669340845176R
        '
        'SubBand3
        '
        Me.SubBand3.Dpi = 100.0!
        Me.SubBand3.HeightF = 85.03793!
        Me.SubBand3.Name = "SubBand3"
        Me.SubBand3.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand
        '
        'SubBand4
        '
        Me.SubBand4.Dpi = 100.0!
        Me.SubBand4.HeightF = 77.08334!
        Me.SubBand4.Name = "SubBand4"
        Me.SubBand4.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand
        '
        'SubBand5
        '
        Me.SubBand5.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTableCorrSal})
        Me.SubBand5.Dpi = 100.0!
        Me.SubBand5.HeightF = 100.0!
        Me.SubBand5.Name = "SubBand5"
        Me.SubBand5.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand
        '
        'XrTableCorrSal
        '
        Me.XrTableCorrSal.Dpi = 100.0!
        Me.XrTableCorrSal.LocationFloat = New DevExpress.Utils.PointFloat(53.41647!, 0!)
        Me.XrTableCorrSal.Name = "XrTableCorrSal"
        Me.XrTableCorrSal.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow17})
        Me.XrTableCorrSal.SizeF = New System.Drawing.SizeF(660.9742!, 25.0!)
        '
        'XrTableRow17
        '
        Me.XrTableRow17.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCellCorrSalTitu})
        Me.XrTableRow17.Dpi = 100.0!
        Me.XrTableRow17.Name = "XrTableRow17"
        Me.XrTableRow17.Weight = 1.0R
        '
        'XrTableCellCorrSalTitu
        '
        Me.XrTableCellCorrSalTitu.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrTableCellCorrSalTitu.Dpi = 100.0!
        Me.XrTableCellCorrSalTitu.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCellCorrSalTitu.Name = "XrTableCellCorrSalTitu"
        Me.XrTableCellCorrSalTitu.StylePriority.UseBackColor = False
        Me.XrTableCellCorrSalTitu.StylePriority.UseFont = False
        Me.XrTableCellCorrSalTitu.StylePriority.UseTextAlignment = False
        Me.XrTableCellCorrSalTitu.Text = "Corresponsabilidad de Salud (Visitas Médicas)"
        Me.XrTableCellCorrSalTitu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCellCorrSalTitu.Weight = 3.0R
        '
        'TopMargin
        '
        Me.TopMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabelValUsu, Me.XrLabelTituUsu, Me.XrLabelValBenef, Me.XrLabelTituBenef, Me.XrLabelValFecha, Me.XrLabelTituFecha, Me.XrPictureBoxDistintivoSSIS, Me.XrLabelTextoPrincipal})
        Me.TopMargin.Dpi = 100.0!
        Me.TopMargin.HeightF = 162.5!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 20, 0, 100.0!)
        Me.TopMargin.StylePriority.UsePadding = False
        Me.TopMargin.StylePriority.UseTextAlignment = False
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify
        '
        'XrLabelValUsu
        '
        Me.XrLabelValUsu.Dpi = 100.0!
        Me.XrLabelValUsu.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabelValUsu.LocationFloat = New DevExpress.Utils.PointFloat(618.1886!, 60.04333!)
        Me.XrLabelValUsu.Name = "XrLabelValUsu"
        Me.XrLabelValUsu.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabelValUsu.SizeF = New System.Drawing.SizeF(161.4703!, 23.0!)
        Me.XrLabelValUsu.StylePriority.UseFont = False
        '
        'XrLabelTituUsu
        '
        Me.XrLabelTituUsu.Dpi = 100.0!
        Me.XrLabelTituUsu.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabelTituUsu.LocationFloat = New DevExpress.Utils.PointFloat(545.4828!, 60.04333!)
        Me.XrLabelTituUsu.Name = "XrLabelTituUsu"
        Me.XrLabelTituUsu.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabelTituUsu.SizeF = New System.Drawing.SizeF(56.25!, 23.0!)
        Me.XrLabelTituUsu.StylePriority.UseFont = False
        Me.XrLabelTituUsu.Text = "Usuario:"
        '
        'XrLabelValBenef
        '
        Me.XrLabelValBenef.Dpi = 100.0!
        Me.XrLabelValBenef.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabelValBenef.LocationFloat = New DevExpress.Utils.PointFloat(324.3908!, 130.875!)
        Me.XrLabelValBenef.Name = "XrLabelValBenef"
        Me.XrLabelValBenef.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabelValBenef.SizeF = New System.Drawing.SizeF(216.1414!, 23.00001!)
        Me.XrLabelValBenef.StylePriority.UseFont = False
        '
        'XrLabelTituBenef
        '
        Me.XrLabelTituBenef.Dpi = 100.0!
        Me.XrLabelTituBenef.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabelTituBenef.LocationFloat = New DevExpress.Utils.PointFloat(208.7821!, 130.875!)
        Me.XrLabelTituBenef.Name = "XrLabelTituBenef"
        Me.XrLabelTituBenef.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabelTituBenef.SizeF = New System.Drawing.SizeF(113.5417!, 22.99998!)
        Me.XrLabelTituBenef.StylePriority.UseFont = False
        Me.XrLabelTituBenef.Text = "Beneficiario (niño) :"
        '
        'XrLabelValFecha
        '
        Me.XrLabelValFecha.Dpi = 100.0!
        Me.XrLabelValFecha.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabelValFecha.LocationFloat = New DevExpress.Utils.PointFloat(618.1886!, 35.58502!)
        Me.XrLabelValFecha.Name = "XrLabelValFecha"
        Me.XrLabelValFecha.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabelValFecha.SizeF = New System.Drawing.SizeF(91.66675!, 23.0!)
        Me.XrLabelValFecha.StylePriority.UseFont = False
        '
        'XrLabelTituFecha
        '
        Me.XrLabelTituFecha.Dpi = 100.0!
        Me.XrLabelTituFecha.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabelTituFecha.LocationFloat = New DevExpress.Utils.PointFloat(545.4828!, 35.58502!)
        Me.XrLabelTituFecha.Name = "XrLabelTituFecha"
        Me.XrLabelTituFecha.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabelTituFecha.SizeF = New System.Drawing.SizeF(45.35126!, 23.0!)
        Me.XrLabelTituFecha.StylePriority.UseFont = False
        Me.XrLabelTituFecha.StylePriority.UseTextAlignment = False
        Me.XrLabelTituFecha.Text = "Fecha:"
        Me.XrLabelTituFecha.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrPictureBoxDistintivoSSIS
        '
        Me.XrPictureBoxDistintivoSSIS.Dpi = 100.0!
        Me.XrPictureBoxDistintivoSSIS.Image = CType(resources.GetObject("XrPictureBoxDistintivoSSIS.Image"), System.Drawing.Image)
        Me.XrPictureBoxDistintivoSSIS.LocationFloat = New DevExpress.Utils.PointFloat(302.7959!, 10.00001!)
        Me.XrPictureBoxDistintivoSSIS.Name = "XrPictureBoxDistintivoSSIS"
        Me.XrPictureBoxDistintivoSSIS.SizeF = New System.Drawing.SizeF(175.9633!, 73.04333!)
        Me.XrPictureBoxDistintivoSSIS.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        '
        'XrLabelTextoPrincipal
        '
        Me.XrLabelTextoPrincipal.AutoWidth = True
        Me.XrLabelTextoPrincipal.Dpi = 100.0!
        Me.XrLabelTextoPrincipal.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabelTextoPrincipal.LocationFloat = New DevExpress.Utils.PointFloat(184.5171!, 84.77647!)
        Me.XrLabelTextoPrincipal.Multiline = True
        Me.XrLabelTextoPrincipal.Name = "XrLabelTextoPrincipal"
        Me.XrLabelTextoPrincipal.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabelTextoPrincipal.SizeF = New System.Drawing.SizeF(405.9837!, 34.30688!)
        Me.XrLabelTextoPrincipal.StylePriority.UseFont = False
        Me.XrLabelTextoPrincipal.StylePriority.UseTextAlignment = False
        Me.XrLabelTextoPrincipal.Text = "PRESIDENCIA  DE LA REPUBLICA DE HONDURAS PROGRAMA TRANSFERENCIAS MONETARIAS CONDI" &
    "CIONADAS ""BONO VIDA MEJOR"""
        Me.XrLabelTextoPrincipal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter
        '
        'BottomMargin
        '
        Me.BottomMargin.Dpi = 100.0!
        Me.BottomMargin.HeightF = 100.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabelNomReport})
        Me.ReportHeader.Dpi = 100.0!
        Me.ReportHeader.HeightF = 40.58083!
        Me.ReportHeader.KeepTogether = True
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrLabelNomReport
        '
        Me.XrLabelNomReport.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.XrLabelNomReport.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabelNomReport.Dpi = 100.0!
        Me.XrLabelNomReport.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabelNomReport.LocationFloat = New DevExpress.Utils.PointFloat(77.24008!, 3.743553!)
        Me.XrLabelNomReport.Name = "XrLabelNomReport"
        Me.XrLabelNomReport.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabelNomReport.SizeF = New System.Drawing.SizeF(596.9006!, 26.83729!)
        Me.XrLabelNomReport.StylePriority.UseBackColor = False
        Me.XrLabelNomReport.StylePriority.UseBorders = False
        Me.XrLabelNomReport.StylePriority.UseFont = False
        Me.XrLabelNomReport.StylePriority.UseTextAlignment = False
        Me.XrLabelNomReport.Text = "Historial de Corresponsabilidades"
        Me.XrLabelNomReport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'PageFooter
        '
        Me.PageFooter.Dpi = 100.0!
        Me.PageFooter.HeightF = 24.76855!
        Me.PageFooter.LockedInUserDesigner = True
        Me.PageFooter.Name = "PageFooter"
        '
        'cl_xtra_report_busq_corres_benef
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.PageFooter})
        Me.Margins = New System.Drawing.Printing.Margins(33, 0, 162, 100)
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "16.1"
        Me.VerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Smart
        CType(Me.XrTableInfHog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTableInfTitu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTableInfBenef, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTableCorrSal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand

#End Region

End Class