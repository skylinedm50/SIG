Imports DevExpress.Web.Mvc
Imports DevExpress.Utils
Imports Microsoft.AspNet.SignalR

Namespace SIG.Areas.Planilla.Controllers
    Public Class EjecucionController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Planilla/Ejecucion
        Dim objLogin As New Cl_Login()
        Dim ejecucion As SIG.Areas.Planilla.Models.cl_ejecucion = New SIG.Areas.Planilla.Models.cl_ejecucion

        Function ViewEmisionProyeccion() As ActionResult
            Return View()
        End Function

        <ValidateInput(False)>
        Function ViewEmisionPlanilla() As ActionResult
            If objLogin.Fnc_loggeado() IsNot Nothing Then
                If objLogin.fnc_validarModulo(3) Then
                    objLogin.Fnc_MenuModulo(3)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Sub recibirSignalRId(ByVal id As String)
            Session("signalrId") = id
        End Sub

        Function ViewAnulacionPlanilla() As ActionResult
            If objLogin.Fnc_loggeado() IsNot Nothing Then
                If objLogin.fnc_validarModulo(3) Then
                    objLogin.Fnc_MenuModulo(3)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Sub ejecutarPlanilla(ByVal esquemas As String) 'As JavaScriptResult

            Dim esquema As String() = Split(esquemas, ",")
            Dim json As String = ""

            For i = 0 To esquema.Length - 1

                json += "{""codigo"": " + esquema(i) + ", ""nombre"": """ + ejecucion.fnc_obtener_nombre_esquema(esquema(i)) + """, ""estado"": " + ejecucion.fnc_generar_planilla(esquema(i)).ToString() + "}"

                If Not i = esquema.Length - 1 Then
                    json += ","
                End If
            Next
            Session("strEsquemas") = esquemas
            'Return JavaScript(json)

            Dim pbconexion As New signalREjecucion
            pbconexion.finEjecucion(json)

        End Sub

        Function pv_gdvPlanillaGenerada(ByVal strEsquemas As String) As ActionResult

            Dim planillas As String = Session("strEsquemas")

            ViewData("strEsquemas") = strEsquemas

            If planillas.Length > 0 Then
                Return PartialView(ejecucion.fnc_obtener_planilla_generada(Session("strEsquemas")))
            Else
                Return PartialView(ejecucion.fnc_obtener_planilla_generada(strEsquemas))
            End If

            'Return PartialView(ejecucion.fnc_obtener_planilla_generada(strEsquemas))

        End Function

        Function exportarPlanillaGenerada() As ActionResult

            Dim esquemas As String = TextBoxExtension.GetValue(Of String)("txtSelectedIdsHidden")
            Dim exportar As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")
            Dim dt As DataTable = ejecucion.fnc_obtener_planilla_generada(esquemas)
            Dim settings As PivotGridSettings = SIG.Areas.Planilla.Controllers.exportPvgPlanillaGenerada.ExportPivotGridSettings

            If exportar = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(settings, dt, "Planilla Generada")
            ElseIf exportar = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(settings, dt, "Planilla Generada")
            ElseIf exportar = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(settings, dt, "Planilla Generada")
            ElseIf exportar = "Html" Then
                Return PivotGridExtension.ExportToHtml(settings, dt, "Planilla Generada")
            ElseIf exportar = "Csv" Then
                Return PivotGridExtension.ExportToCsv(settings, dt, "Planilla Generada")
            End If

        End Function

        Function anularEsquema(ByVal esquema As String, ByVal observacion As String)
            Return ejecucion.fnc_anular_planilla(esquema, observacion)
        End Function

    End Class

#Region "clase de signalR para el progress bar"

    Public Class signalREjecucion
        Inherits Hub

        'Public Sub enviarIncremento()

        '    Dim total As Integer = System.Web.HttpContext.Current.Session("totalReg")
        '    Dim actual As Integer = System.Web.HttpContext.Current.Session("actual")
        '    Dim connectionID As String = HttpContext.Current.Session("signalrId")

        '    Dim conexion As IHubContext = GlobalHost.ConnectionManager.GetHubContext(Of progressClass)()
        '    Dim progreso As Integer = (actual / total) * 100

        '    If progreso = 100 And actual < total Then
        '        progreso = 99
        '    End If

        '    conexion.Clients.Client(connectionID).incrementarProgressBar(progreso)
        'End Sub

        'Public Sub finPrecarga()
        '    Dim connectionID As String = HttpContext.Current.Session("signalrId")
        '    Dim conexion As IHubContext = GlobalHost.ConnectionManager.GetHubContext(Of progressClass)()
        '    conexion.Clients.Client(connectionID).finPrecarga()
        'End Sub

        Public Sub finEjecucion(ByVal json As String)

            Dim connectionID As String = HttpContext.Current.Session("signalrId")
            Dim conexion As IHubContext = GlobalHost.ConnectionManager.GetHubContext(Of signalREjecucion)()
            conexion.Clients.Client(connectionID).finEjecucion(json)

        End Sub


    End Class

#End Region

#Region "Clase para los settings de los gridview y pivotgrid"

    Public NotInheritable Class exportPvgPlanillaGenerada

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportPivotGridSettings() As PivotGridSettings

            Get
                exportSettings = CreateExportPvgPlanillaGenerada()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgPlanillaGenerada() As PivotGridSettings

            Dim settings As New PivotGridSettings()

            settings.Name = "pvgPlanillaGenerada"
            settings.Width = Unit.Percentage(100)
            settings.OptionsView.ShowHorizontalScrollBar = True

            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False

            settings.OptionsView.DataHeadersDisplayMode = DevExpress.Web.ASPxPivotGrid.PivotDataHeadersDisplayMode.Popup
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
                    field.Caption = "Fondo"
                    field.FieldName = "fond_nombre"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    field.Index = 3
                    field.Caption = "Pagador"
                    field.FieldName = "Nombre_Pagador"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    field.Index = 4
                    field.Caption = "Detalle Bono"
                    field.FieldName = "eleg_nombre"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    field.Index = 6
                    field.Caption = "Período"
                    field.FieldName = "periodo"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 0
                    field.Caption = "Hogares Programados"
                    field.FieldName = "total_hogares"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 1
                    field.Caption = "Monto Programado"
                    field.FieldName = "monto_total"
                    field.CellFormat.FormatString = "c"
                    field.CellFormat.FormatType = FormatType.Custom
                End Sub)

            Return settings

        End Function

    End Class

#End Region
End Namespace