Imports DevExpress.Web.Mvc
Imports DevExpress.Utils
Imports DevExpress.Web
Imports DevExpress.XtraPivotGrid
Imports DevExpress.Web.ASPxPivotGrid
Imports DevExpress.Data.PivotGrid

Namespace SIG.Areas.Contraloria.Controllers

    Public Class ReportesController
        Inherits Controller

        Dim Reportes As Models.Reportes = New Models.Reportes
        Dim login As New Cl_Login

#Region "funciones generales"

        Function PartialDptoView() As ActionResult
            Return PartialView(Reportes.getAllDptos())
        End Function

        Function PartialMuniView() As ActionResult
            Dim dpto As String = Request.Params("dpto")
            Return PartialView(Reportes.getMuniByDpto(dpto))
        End Function

        Function PartialAldeaView() As ActionResult
            Dim muni As String = Request.Params("muni")
            Return PartialView(Reportes.getAldeasByMuni(muni))
        End Function

        Function PartialFondoView() As ActionResult
            Return PartialView(Reportes.getAllFondos())
        End Function

        Function PartialBancoView() As ActionResult
            Return PartialView(Reportes.getAllBancos())
        End Function

        Function PartialSucursalView() As ActionResult
            Dim banco As String = Request.Params("banco")
            Return PartialView(Reportes.getSucursalByBanco(banco))
        End Function

        Function pv_cbxAnyos() As ActionResult
            Return PartialView(Reportes.getAllAnios())
        End Function

        Function pv_cbxPagos() As ActionResult
            Dim anyo As String = Request.Params("anyo")
            Return PartialView(Reportes.getPagosAnios(anyo))
        End Function

#End Region

#Region "funciones para reporte de recibos pagados"

        Function ViewRecibosPagados() As ActionResult

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(4) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Session("ds") = Nothing
                        Return View("ViewRecibosPagados")
                    Else
                        Return Redirect("/Contraloria/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function PartialPeriodosView() As ActionResult
            Return PartialView(Reportes.getAllPeriodos())
        End Function

        Function pv_esquemas() As ActionResult
            Dim pago As String = Request.Params("pago")
            Return PartialView(Reportes.getEsquemas(pago))
        End Function

        <HttpPost> _
        Function returnPartialGridView(ByVal tipo As String)
            ViewData("tipo") = tipo
            Return PartialView("PartialGridViewRecibosPagados")
        End Function

        <HttpPost>
        Function PartialGridViewRecibosPagados(ByVal dpto As String, ByVal muni As String, ByVal aldea As String, ByVal fondo As String, ByVal banco As String, ByVal sucursal As String, ByVal inicio As String, ByVal fin As String, ByVal periodo As String, ByVal esquema As String, ByVal tipo As String, ByVal filtro As String) As ActionResult

            Dim _fondo As Integer
            If Not fondo = "" Then
                _fondo = CInt(fondo)
            End If

            Dim _banco As Integer
            If Not banco = "" Then
                _banco = CInt(banco)
            End If

            Dim _sucursal As Integer
            If Not sucursal = "" Then
                _sucursal = CInt(sucursal)
            End If

            Dim _periodo As Integer
            If Not periodo = "" Then
                _periodo = CInt(periodo)
            End If

            Dim _esquema As Integer
            If Not esquema = "" Then
                _esquema = CInt(esquema)
            End If

            Dim _inicio As Date
            If Not inicio = "" Then
                _inicio = CDate(inicio)
            End If

            Dim _fin As Date
            If Not fin = "" Then
                _fin = CDate(fin)
            End If

            Dim dt As DataTable
            If tipo = "detalle" Then
                dt = Reportes.getDetalleRecibosPagados(dpto, muni, aldea, _fondo, _banco, _sucursal, _inicio, _fin, _periodo, esquema, filtro)
            ElseIf tipo = "cantidad" Then
                dt = Reportes.getCantidadRecibosPagados(dpto, muni, aldea, _fondo, _banco, _sucursal, _inicio, _fin, _periodo, esquema, filtro)
            End If

            ViewData("dpto") = dpto
            ViewData("muni") = muni
            ViewData("aldea") = aldea
            ViewData("fondo") = fondo
            ViewData("banco") = banco
            ViewData("sucursal") = sucursal
            ViewData("inicio") = inicio
            ViewData("fin") = fin
            ViewData("periodo") = periodo
            ViewData("esquema") = esquema
            ViewData("tipo") = tipo
            ViewData("filtro") = filtro

            Return PartialView("PartialGridViewRecibosPagados", dt)
        End Function

        Function exportRecibosPagadosToExcel()

            Dim dpto As String = EditorExtension.GetValue(Of String)("cbxDpto")
            Dim muni As String = EditorExtension.GetValue(Of String)("cbxMuni")
            Dim aldea As String = EditorExtension.GetValue(Of String)("cbxAldea")
            Dim fondo As String = EditorExtension.GetValue(Of String)("cbxFondo")
            Dim banco As String = EditorExtension.GetValue(Of String)("cbxBanco")
            Dim sucursal As String = EditorExtension.GetValue(Of String)("cbxSucursal")
            Dim inicio As String = EditorExtension.GetValue(Of String)("deInicio")
            Dim fin As String = EditorExtension.GetValue(Of String)("deFin")
            Dim periodo As String = EditorExtension.GetValue(Of String)("cbxPeriodo")
            Dim esquema As String = EditorExtension.GetValue(Of String)("cbsEsquemas")
            Dim tipo As String = EditorExtension.GetValue(Of String)("rbtTipo")
            Dim filtro As String = EditorExtension.GetValue(Of String)("rbtFiltro")


            Dim _fondo As Integer
            If Not fondo = "" Then
                _fondo = CInt(fondo)
            End If

            Dim _banco As Integer
            If Not banco = "" Then
                _banco = CInt(banco)
            End If

            Dim _sucursal As Integer
            If Not sucursal = "" Then
                _sucursal = CInt(sucursal)
            End If

            Dim _periodo As Integer
            If Not periodo = "" Then
                _periodo = CInt(periodo)
            End If

            Dim _esquema As Integer
            If Not periodo = "" Then
                _esquema = CInt(esquema)
            End If

            Dim _inicio As Date
            If Not inicio = "" Then
                _inicio = CDate(inicio)
            End If

            Dim _fin As Date
            If Not fin = "" Then
                _fin = CDate(fin)
            End If

            Dim dt As DataTable
            If tipo = "detalle" Then
                dt = Reportes.getDetalleRecibosPagados(dpto, muni, aldea, _fondo, _banco, _sucursal, _inicio, _fin, _periodo, _esquema, filtro)
                Reportes.insertLog(Session("usuario"), "Descargo una copia en excel con el detalle de recibos pagados", "ninguna", 3, 0)
            ElseIf tipo = "cantidad" Then
                dt = Reportes.getCantidadRecibosPagados(dpto, muni, aldea, _fondo, _banco, _sucursal, _inicio, _fin, _periodo, _esquema, filtro)
                Reportes.insertLog(Session("usuario"), "Descargo una copia en excel con la cantidad de recibos pagados", "ninguna", 3, 0)
            End If

            Return GridViewExtension.ExportToXlsx(exportGridViewRecibosPagados.ExportGridViewSettings(tipo), dt)

        End Function

#End Region

#Region "funciones para reporte de consolidado"

        Function ViewConsolidado()

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(4) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then

                        Session("ds") = Nothing
                        Return View("ViewConsolidado")

                    Else
                        Return Redirect("/Contraloria/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Function PartialGridPeriodos()
            Return PartialView(Reportes.getPeriodos())
        End Function

        Function pv_pvgConsolidado(ByVal pago As String) As ActionResult
            ViewData("pago") = pago
            Return PartialView(Reportes.fnc_obtener_consolidado(pago))
        End Function

        Function exportConsolidadoToExcel()

            Dim pago As String = EditorExtension.GetValue(Of String)("txtPago")
            Reportes.insertLog(Session("usuario"), "Descargo una copia en excel con el consolidado de los pagos", "pago exportado: " + pago, 3, 0)
            Return PivotGridExtension.ExportToXlsx(exportPvgConsolidadoPago.ExportPivotGridSettings, Reportes.fnc_obtener_consolidado(pago))

        End Function
#End Region

#Region "funciones para cuentas activadas por pago"

        Function v_cuentasActivadas()

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(4) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then

                        Session("ds") = Nothing
                        Return View()

                    Else
                        Return Redirect("/Contraloria/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Function pv_pvgCuentasActivadas(ByVal pago As String) As ActionResult
            ViewData("pago") = pago
            Return PartialView(Reportes.fnc_resumen_cuentas_activadas(pago))
        End Function

        Function exportResumenCuentasActivadas() As ActionResult

            Dim pago As String = EditorExtension.GetValue(Of String)("cbxPagos")
            Dim exportar As String = EditorExtension.GetValue(Of String)("cbxExpotar")
            Dim settings As PivotGridSettings = exportPvgCuentasActivadas.ExportPivotGridSettings()
            Dim dt As DataTable = Reportes.fnc_resumen_cuentas_activadas(pago)

            If exportar = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(settings, dt)
            ElseIf exportar = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(settings, dt)
            ElseIf exportar = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(settings, dt)
            ElseIf exportar = "Html" Then
                Return PivotGridExtension.ExportToHtml(settings, dt)
            ElseIf exportar = "Csv" Then
                Return PivotGridExtension.ExportToCsv(settings, dt)
            End If

            Return Nothing


        End Function

#End Region

#Region "Detalle de cuentas básicas"

        Function v_cuentasBasicas()

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(4) Then
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then

                        Session("ds") = Nothing
                        Return View()

                    Else
                        Return Redirect("/Contraloria/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Function pv_gdvCuentasBasicas(ByVal pago As String) As ActionResult
            ViewData("pago") = pago
            Return PartialView(Reportes.fnc_detalle_cuentas_basicas(pago))
        End Function

        Function exportDetalleCuentasBasicas() As ActionResult

            Dim pago As String = EditorExtension.GetValue(Of String)("cbxPagos")
            'Dim exportar As String = EditorExtension.GetValue(Of String)("cbxExpotar")
            Dim dt As DataTable = Reportes.fnc_detalle_cuentas_basicas(pago)

            Return GridViewExtension.ExportToXlsx(exportGridCuentasBasicas.ExportGridViewSettings, dt)

        End Function

#End Region

    End Class

    'región para contener las clases para exportar los settings de los gridview mostrados en las ventanas
    'de reportes
#Region "clase para exportar gridview"

    'clase para el settings de recibos pagados, esta contiene una variable privada con su metodo get, para extraer 
    'la configuración del gridview
    Public NotInheritable Class exportGridViewRecibosPagados

        Private Shared exportSettings As GridViewSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportGridViewSettings(ByVal tipo As String) As GridViewSettings

            Get
                exportSettings = CreateExportGridViewReciboPagadosSettings(tipo)
                Return exportSettings
            End Get



        End Property

        'clase en la cual se detalla todos los settings
        Public Shared Function CreateExportGridViewReciboPagadosSettings(ByVal tipo As String)

            Dim setting As New GridViewSettings()

            setting.Name = "gvPagos"

            setting.SettingsPager.Position = PagerPosition.Bottom
            setting.SettingsPager.FirstPageButton.Visible = True
            setting.SettingsPager.LastPageButton.Visible = True
            setting.SettingsPager.PageSizeItemSettings.Visible = True
            setting.SettingsPager.PageSizeItemSettings.Items = New String() {"10", "20", "50"}
            setting.SettingsBehavior.AllowGroup = True
            setting.SettingsBehavior.AllowSort = True
            setting.Settings.ShowGroupPanel = True

            setting.EnableCallbackAnimation = True
            setting.EnablePagingCallbackAnimation = True
            setting.EnableCallbackCompression = True

            setting.Settings.ShowHeaderFilterButton = True
            setting.SettingsPopup.HeaderFilter.Height = 200

            If tipo = "cantidad" Or IsNothing(tipo) Then
                setting.SettingsExport.FileName = "CANTIDAD DE RECIBOS PAGADOS"
                setting.Caption = "CANTIDAD DE RECIBOS PAGADOS"
                setting.SettingsExport.FileName = "Cantidad de Recibos Pagados"

                setting.Columns.AddBand(
                    Sub(ib)
                        ib.Caption = "Información Bancaria"
                        ib.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                        ib.Columns.Add("fond_nombre", "Fondo").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                        ib.Columns.Add("nombre_banco", "Nombre").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                        ib.Columns.Add("desc_sucursal", "Sucursal").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    End Sub)
                setting.Columns.AddBand(
                    Sub(ag)
                        ag.Caption = "Área Geográfica"
                        ag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                        ag.Columns.Add("desc_departamento", "Departamento").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                        ag.Columns.Add("desc_municipio", "Municipio").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                        ag.Columns.Add("desc_aldea", "Aldea").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    End Sub)
                setting.Columns.Add("cantidad", "Cantidad").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
            End If

            If tipo = "detalle" Then
                setting.Width = Unit.Pixel(1500)
                setting.Caption = "DETALLE DE RECIBOS PAGADOS"
                setting.SettingsExport.FileName = "Detalle de Recibos Pagados"

                setting.Columns.AddBand(
                    Sub(ag)
                        ag.Caption = "Área Geográfica"
                        ag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                        ag.Columns.Add("desc_departamento", "Departamento").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                        ag.Columns.Add("desc_municipio", "Municipio").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                        ag.Columns.Add("desc_aldea", "Aldea").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    End Sub)
                setting.Columns.AddBand(
                    Sub(ib)
                        ib.Caption = "Información Bancaria"
                        ib.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                        ib.Columns.Add("fond_nombre", "Fondo").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                        ib.Columns.Add("nombre_banco", "Nombre").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                        ib.Columns.Add("desc_sucursal", "Sucursal").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    End Sub)
                setting.Columns.AddBand(
                    Sub(tit)
                        tit.Caption = "Beneficiario"
                        tit.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                        tit.Columns.Add("tit_identidad", "Identidad").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                        tit.Columns.Add("nombres", "Nombres").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                        tit.Columns.Add("apellidos", "Apellidos").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    End Sub)
                setting.Columns.AddBand(
                    Sub(pag)
                        pag.Caption = "Pago"
                        pag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                        pag.Columns.Add("tit_pagina", "Página").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                        pag.Columns.Add("tit_linea", "Linea").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                        pag.Columns.Add("tit_monto_total", "Monto").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                        pag.Columns.Add("fecha_pago", "Fecha").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    End Sub)
            End If

            Return setting
        End Function



    End Class

    Public NotInheritable Class exportPvgConsolidadoPago

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportPivotGridSettings() As PivotGridSettings

            Get
                exportSettings = CreateExportPvgConsolidadoPago()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgConsolidadoPago() As PivotGridSettings

            Dim settings As New PivotGridSettings()

            settings.Name = "pvgConsolidado"
            settings.Width = Unit.Percentage(120)

            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False

            settings.EnableCallbackAnimation = True
            settings.EnablePagingCallbackAnimation = True

            settings.OptionsPager.RowsPerPage = 20

            settings.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Auto
            settings.OptionsView.DataHeadersDisplayMode = PivotDataHeadersDisplayMode.Popup
            settings.OptionsView.EnableFilterControlPopupMenuScrolling = True
            settings.OptionsView.RowTreeOffset = 3
            settings.OptionsView.RowTotalsLocation = PivotRowTotalsLocation.Far

            settings.OptionsData.DataFieldUnboundExpressionMode = DataFieldUnboundExpressionMode.UseSummaryValues

            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 1
                    field.Caption = "Pagador"
                    field.FieldName = "Nombre_Pagador"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 2
                    field.Caption = "Fondo"
                    field.FieldName = "fond_nombre"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 3
                    field.Caption = "Esquema"
                    field.FieldName = "nombre_esquema"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 5
                    field.Caption = "Monto"
                    field.FieldName = "tit_monto_total"
                    field.SummaryType = PivotSummaryType.Min
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 6
                    field.Caption = "Municipio"
                    field.FieldName = "desc_municipio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 7
                    field.Caption = "Aldea"
                    field.FieldName = "desc_aldea"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.RowArea
                    field.Index = 0
                    field.Caption = "Departamento"
                    field.FieldName = "desc_departamento"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 0
                    field.Caption = "Hogares Programados"
                    field.FieldName = "hogares_programados"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 1
                    field.Caption = "Monto Programado"
                    field.FieldName = "monto_programado"
                    field.CellFormat.FormatString = "c"
                    field.CellFormat.FormatType = FormatType.Custom
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 2
                    field.Caption = "Hogares Pagados"
                    field.FieldName = "hogares_pagados"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 3
                    field.Caption = "Monto Pagado"
                    field.FieldName = "monto_pagado"
                    field.CellFormat.FormatString = "c"
                    field.CellFormat.FormatType = FormatType.Custom
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 4
                    field.Caption = "Hogares No Pagados"
                    field.FieldName = "hogares_no_pagados"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 5
                    field.Caption = "Monto No Pagado"
                    field.FieldName = "monto_no_pagado"
                    field.CellFormat.FormatString = "c"
                    field.CellFormat.FormatType = FormatType.Custom
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 6
                    field.Caption = "% Cumplimiento"
                    field.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                    field.UnboundExpression = String.Format("([{0}] * 100)/[{1}]", settings.Fields("hogares_pagados").ID, settings.Fields("hogares_programados").ID)
                    field.UnboundFieldName = "cumplimiento"
                    field.CellFormat.FormatType = FormatType.Numeric
                    field.CellFormat.FormatString = "n2"
                End Sub)

            Return settings

        End Function

    End Class

    Public NotInheritable Class exportPvgCuentasActivadas

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportPivotGridSettings() As PivotGridSettings

            Get
                exportSettings = CreateExportPvgCuentasActivadas()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgCuentasActivadas() As PivotGridSettings

            Dim settings As New PivotGridSettings()

            settings.Name = "pvgCuentasActivadas"
            settings.Width = Unit.Percentage(120)

            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False

            settings.EnableCallbackAnimation = True
            settings.EnablePagingCallbackAnimation = True

            settings.OptionsPager.RowsPerPage = 20

            settings.OptionsView.HorizontalScrollBarMode = ScrollBarMode.Auto
            settings.OptionsView.DataHeadersDisplayMode = PivotDataHeadersDisplayMode.Popup
            settings.OptionsView.EnableFilterControlPopupMenuScrolling = True
            settings.OptionsView.RowTreeOffset = 3
            settings.OptionsView.RowTotalsLocation = PivotRowTotalsLocation.Far

            settings.OptionsData.DataFieldUnboundExpressionMode = DataFieldUnboundExpressionMode.UseSummaryValues

            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 0
                    field.Caption = "Banco"
                    field.FieldName = "nombre_banco"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 1
                    field.Caption = "Municipio"
                    field.FieldName = "desc_municipio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 2
                    field.Caption = "Aldea"
                    field.FieldName = "desc_aldea"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.FilterArea
                    field.Index = 3
                    field.Caption = "Caserío"
                    field.FieldName = "desc_caserio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.RowArea
                    field.Index = 0
                    field.Caption = "Departamento"
                    field.FieldName = "desc_departamento"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 0
                    field.Caption = "Total Cuentas"
                    field.FieldName = "total_cuentas"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 1
                    field.Caption = "Monto Total"
                    field.FieldName = "monto_total"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 2
                    field.Caption = "Cuentas Activivas"
                    field.FieldName = "cuentas_ya_activas"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 3
                    field.Caption = "Monto Cuentas Activas"
                    field.FieldName = "monto_ya_activas"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 4
                    field.Caption = "Cuentas Nuevas"
                    field.FieldName = "cuentas_nuevas"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 5
                    field.Caption = "Monto Nuevas"
                    field.FieldName = "monto_nuevas"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 6
                    field.Caption = "Cuentas Activadas"
                    field.FieldName = "cuentas_activadas"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 7
                    field.Caption = "Monto Activadas"
                    field.FieldName = "monto_activadas"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 8
                    field.Caption = "Cuentas No Activadas"
                    field.FieldName = "cuentas_no_activadas"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = PivotArea.DataArea
                    field.Index = 9
                    field.Caption = "Monto No Activadas"
                    field.FieldName = "monto_no_activadas"
                End Sub)


            Return settings



        End Function

    End Class

    Public NotInheritable Class exportGridCuentasBasicas

        Private Shared exportSettings As GridViewSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportGridViewSettings() As GridViewSettings

            Get
                exportSettings = CreateExportGridCuentasBasicasSettings()
                Return exportSettings
            End Get

        End Property

        'clase en la cual se detalla todos los settings
        Public Shared Function CreateExportGridCuentasBasicasSettings()

            Dim setting As New GridViewSettings()

            setting.Name = "gdvCuentasBasicas"

            setting.SettingsPager.Position = PagerPosition.Bottom
            setting.SettingsPager.FirstPageButton.Visible = True
            setting.SettingsPager.LastPageButton.Visible = True
            setting.SettingsPager.PageSizeItemSettings.Visible = True
            setting.SettingsPager.PageSizeItemSettings.Items = New String() {"10", "20", "50"}
            setting.SettingsBehavior.AllowGroup = True
            setting.SettingsBehavior.AllowSort = True
            setting.Settings.ShowGroupPanel = True

            setting.EnableCallbackAnimation = True
            setting.EnablePagingCallbackAnimation = True
            setting.EnableCallbackCompression = True

            setting.Settings.ShowHeaderFilterButton = True
            setting.SettingsPopup.HeaderFilter.Height = 200

            setting.Width = Unit.Pixel(1500)
            setting.Caption = "DETALLE DE CUENTAS BÁSICAS  "
            setting.SettingsExport.FileName = "Detalle de Cuentas Básicas"

            setting.Columns.AddBand(
                Sub(ag)
                    ag.Caption = "Área Geográfica"
                    ag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    ag.Columns.Add("desc_departamento", "Departamento").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    ag.Columns.Add("desc_municipio", "Municipio").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    ag.Columns.Add("desc_aldea", "Aldea").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                End Sub)
            setting.Columns.AddBand(
                Sub(tit)
                    tit.Caption = "Beneficiario"
                    tit.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    tit.Columns.Add("identidad_persona", "Identidad").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    tit.Columns.Add("nombres", "Nombres").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    tit.Columns.Add("apellidos", "Apellidos").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                End Sub)
            setting.Columns.AddBand(
                Sub(ib)
                    ib.Caption = "Información Bancaria"
                    ib.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    ib.Columns.Add("nombre_banco", "Nombre").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    ib.Columns.Add("estado", "Estado").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                    ib.Columns.Add("fecha_apertura", "Fecha de apertura").SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList
                End Sub)

            Return setting
        End Function



    End Class

#End Region

End Namespace