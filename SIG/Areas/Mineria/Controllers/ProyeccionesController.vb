Imports DevExpress.Web.Mvc
Imports DevExpress.Web
Imports DevExpress.Utils
Imports DevExpress.XtraPivotGrid

Namespace SIG.Areas.Mineria.Controllers
    Public Class ProyeccionesController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Mineria/Proyecciones
        Dim login As New Global.SIG.Cl_Login
        Dim proyecciones As SIG.Areas.Mineria.Models.Cl_Proyecciones = New SIG.Areas.Mineria.Models.Cl_Proyecciones


        Function Home() As ActionResult
            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

#Region "Funciones para los elegibles contro programados"

        Function v_ElegiblesContraProgramados() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function pv_pvgElegiblesVsProgramado(ByVal pago As String) As ActionResult
            ViewData("pago") = pago
            Return PartialView(proyecciones.Fnc_elegibles_contra_programados(pago))
        End Function

        Function exportarElegiblesContraProgramado() As ActionResult

            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")
            Dim settings As PivotGridSettings = SIG.Areas.Mineria.Controllers.exportPvgProyeccionElegiblesControProgramado.ExportPivotGridSettings
            Dim pago As String = ComboBoxExtension.GetValue(Of String)("cbxPagosAno")

            If export = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(settings, proyecciones.Fnc_elegibles_contra_programados(pago), "Elegibles Contra Programados de Proyeccion")
            ElseIf export = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(settings, proyecciones.Fnc_elegibles_contra_programados(pago), "Elegibles Contra Programados de Proyeccion")
            ElseIf export = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(settings, proyecciones.Fnc_elegibles_contra_programados(pago), "Elegibles Contra Programados de Proyeccion")
            ElseIf export = "Html" Then
                Return PivotGridExtension.ExportToHtml(settings, proyecciones.Fnc_elegibles_contra_programados(pago), "Elegibles Contra Programados de Proyeccion")
            ElseIf export = "Csv" Then
                Return PivotGridExtension.ExportToCsv(settings, proyecciones.Fnc_elegibles_contra_programados(pago), "Elegibles Contra Programados de Proyeccion")
            End If

            Return Nothing

        End Function

#End Region


#Region "Funciones para las razones de exclusión de hogares"

        Function v_RazonesExclusionHogares() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function pv_pvgRazonesExclusionHogares(ByVal pago As String) As ActionResult
            ViewData("pago") = pago
            Return PartialView(proyecciones.Fnc_razon_exclusion_hogares(pago))
        End Function

        Function exportarRazonesExclusionHogares() As ActionResult

            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")
            Dim settings As PivotGridSettings = SIG.Areas.Mineria.Controllers.exportPvgProyeccionRazonExclusionHogares.ExportPivotGridSettings
            Dim pago As String = ComboBoxExtension.GetValue(Of String)("cbxPagosAno")

            If export = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(settings, proyecciones.Fnc_razon_exclusion_hogares(pago), "Razón de Exclusion de Hogares en Proyeccion")
            ElseIf export = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(settings, proyecciones.Fnc_razon_exclusion_hogares(pago), "Razón de Exclusion de Hogares en Proyeccion")
            ElseIf export = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(settings, proyecciones.Fnc_razon_exclusion_hogares(pago), "Razón de Exclusion de Hogares en Proyeccion")
            ElseIf export = "Html" Then
                Return PivotGridExtension.ExportToHtml(settings, proyecciones.Fnc_razon_exclusion_hogares(pago), "Razón de Exclusion de Hogares en Proyeccion")
            ElseIf export = "Csv" Then
                Return PivotGridExtension.ExportToCsv(settings, proyecciones.Fnc_razon_exclusion_hogares(pago), "Razón de Exclusion de Hogares en Proyeccion")
            End If

            Return Nothing

        End Function

#End Region

    End Class

#Region "Clase para los settings de los gridview y pivotgrid"


    Public NotInheritable Class exportPvgProyeccionElegiblesControProgramado

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportPivotGridSettings() As PivotGridSettings

            Get
                exportSettings = CreateExportPvgElegibleContraProgramado()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgElegibleContraProgramado() As PivotGridSettings

            Dim settings As New PivotGridSettings()

            settings.Name = "pvgComparacionPlanillas"
            settings.Width = Unit.Percentage(100)
            settings.OptionsView.ShowHorizontalScrollBar = True

            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False

            settings.EnableCallbackAnimation = True
            settings.EnablePagingCallbackAnimation = True

            settings.OptionsView.DataHeadersDisplayMode = DevExpress.Web.ASPxPivotGrid.PivotDataHeadersDisplayMode.Popup
            settings.OptionsView.DataHeadersPopupMinCount = 6
            settings.OptionsView.DataHeadersPopupMaxColumnCount = 6
            settings.OptionsView.RowTreeOffset = 2
            settings.OptionsView.RowTotalsLocation = PivotRowTotalsLocation.Far

            settings.OptionsData.DataFieldUnboundExpressionMode = DataFieldUnboundExpressionMode.UseSummaryValues

            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
                    field.Index = 0
                    field.Caption = "Departamento"
                    field.FieldName = "desc_departamento"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    field.Caption = "Municipio"
                    field.FieldName = "desc_municipio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    field.Caption = "Aldea"
                    field.FieldName = "desc_aldea"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    field.Caption = "Caserío"
                    field.FieldName = "desc_caserio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 0
                    field.Caption = "Total Hogares"
                    field.FieldName = "total_hogares"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 1
                    field.Caption = "Hogares Evaluados"
                    field.FieldName = "total_hogares_evaluados"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 2
                    field.Caption = "Hogares Elegibles"
                    field.FieldName = "total_hogares_elegibles"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 3
                    field.Caption = "Hogares Programados"
                    field.FieldName = "total_hogares_programados"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 4
                    field.Caption = "Programados / Elegibles"
                    field.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                    field.UnboundExpression = String.Format("([{0}] * 100)/[{1}]", settings.Fields("total_hogares_programados").ID, settings.Fields("total_hogares_elegibles").ID)
                    field.UnboundFieldName = "programados_/_elegibles_"
                    field.CellFormat.FormatType = FormatType.Numeric
                    field.CellFormat.FormatString = "n2"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 5
                    field.Caption = "Elegibles Educación"
                    field.FieldName = "hogares_elegibles_educacion"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 6
                    field.Caption = "Programados Educación"
                    field.FieldName = "hogares_programados_educacion"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 7
                    field.Caption = "progra_edu / elegi_edu"
                    field.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                    field.UnboundExpression = String.Format("([{0}] * 100)/[{1}]", settings.Fields("hogares_programados_educacion").ID, settings.Fields("hogares_elegibles_educacion").ID)
                    field.UnboundFieldName = "programados_/_elegibles_educacion"
                    field.CellFormat.FormatType = FormatType.Numeric
                    field.CellFormat.FormatString = "n2"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 8
                    field.Caption = "Elegibles Educación y Salud"
                    field.FieldName = "hogares_elegibles_educacion_salud"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 9
                    field.Caption = "Programados Educación y Salud"
                    field.FieldName = "hogares_programados_educacion_salud"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 10
                    field.Caption = "progra_edu_sal / elegi_edu_sal"
                    field.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                    field.UnboundExpression = String.Format("([{0}] * 100)/[{1}]", settings.Fields("hogares_programados_educacion_salud").ID, settings.Fields("hogares_elegibles_educacion_salud").ID)
                    field.UnboundFieldName = "programados_/_elegibles_educacion_salud"
                    field.CellFormat.FormatType = FormatType.Numeric
                    field.CellFormat.FormatString = "n2"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 11
                    field.Caption = "Elegible Salud"
                    field.FieldName = "hogares_elegibles_salud"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 12
                    field.Caption = "Programados Salud"
                    field.FieldName = "hogares_programados_salud"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 13
                    field.Caption = "progra_sal / elegi_sal"
                    field.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                    field.UnboundExpression = String.Format("([{0}] * 100)/[{1}]", settings.Fields("hogares_programados_salud").ID, settings.Fields("hogares_elegibles_salud").ID)
                    field.UnboundFieldName = "programados_/_elegibles_salud"
                    field.CellFormat.FormatType = FormatType.Numeric
                    field.CellFormat.FormatString = "n2"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 14
                    field.Caption = "Elegibles Primer Pago"
                    field.FieldName = "hogares_elegibles_primer_pago"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 15
                    field.Caption = "Programados Primer Pago"
                    field.FieldName = "hogares_programados_primer_pago"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 16
                    field.Caption = "progra_prime / elegi_prime"
                    field.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                    field.UnboundExpression = String.Format("([{0}] * 100)/[{1}]", settings.Fields("hogares_programados_primer_pago").ID, settings.Fields("hogares_elegibles_primer_pago").ID)
                    field.UnboundFieldName = "programados_/_elegibles_primer_pago"
                    field.CellFormat.FormatType = FormatType.Numeric
                    field.CellFormat.FormatString = "n2"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 17
                    field.Caption = "Elegibles Repago"
                    field.FieldName = "hogares_elegibles_repago"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 18
                    field.Caption = "Programados Repago"
                    field.FieldName = "hogares_programados_repago"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 19
                    field.Caption = "progra_repa / elegi_repa"
                    field.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                    field.UnboundExpression = String.Format("([{0}] * 100)/[{1}]", settings.Fields("hogares_programados_repago").ID, settings.Fields("hogares_elegibles_repago").ID)
                    field.UnboundFieldName = "programados_/_elegibles_repago"
                    field.CellFormat.FormatType = FormatType.Numeric
                    field.CellFormat.FormatString = "n2"
                End Sub)

            Return settings

        End Function

    End Class

    Public NotInheritable Class exportPvgProyeccionRazonExclusionHogares

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportPivotGridSettings() As PivotGridSettings

            Get
                exportSettings = CreateExportPvgRazonExclusionHogares()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgRazonExclusionHogares() As PivotGridSettings

            Dim settings As New PivotGridSettings()

            settings.Name = "pvgComparacionPlanillas"
            settings.Width = Unit.Percentage(100)
            settings.OptionsView.ShowHorizontalScrollBar = True

            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False

            settings.EnableCallbackAnimation = True
            settings.EnablePagingCallbackAnimation = True

            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
                    field.Index = 0
                    field.Caption = "Departamento"
                    field.FieldName = "desc_departamento"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    field.Index = 1
                    field.Caption = "Municipio"
                    field.FieldName = "desc_municipio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    field.Index = 2
                    field.Caption = "Aldea"
                    field.FieldName = "desc_aldea"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    field.Index = 3
                    field.Caption = "Caserío"
                    field.FieldName = "desc_caserio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
                    field.Index = 0
                    field.Caption = "Proyección Corta"
                    field.FieldName = "proyeccion_corta"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    field.Index = 0
                    field.Caption = "Proyección"
                    field.FieldName = "proyeccion"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 0
                    field.Caption = "Hogares"
                    field.FieldName = "cant_hogares"
                End Sub)


            Return settings

        End Function

    End Class

#End Region

End Namespace