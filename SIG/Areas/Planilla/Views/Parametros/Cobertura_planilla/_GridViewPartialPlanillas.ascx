<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Dim strCodEsquema As String = "0"
    Dim objEsquema As New SIG.SIG.Areas.Planilla.Models.cl_esquema
    Html.DevExpress.GridView(Sub(configuracionGridView)
                                 configuracionGridView.Name = "AspxGridViewPlanilla"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Parametros", _
                                                                                       Key .Action = "GridViewPlanillas"}
                                 configuracionGridView.CommandColumn.Visible = True
                                 configuracionGridView.CommandColumn.ShowClearFilterButton = True
                                 
                                 configuracionGridView.KeyFieldName = "esq_codigo"
		
                                 configuracionGridView.SettingsPager.Visible = True
                                 configuracionGridView.Settings.ShowGroupPanel = False
                                 configuracionGridView.Settings.ShowFilterRow = True
                                 configuracionGridView.CommandColumn.ShowClearFilterButton = True
                                 configuracionGridView.Caption = "Configuración de Planillas"
                                 
                                 configuracionGridView.ClientSideEvents.EndCallback = "function(){objManejoEsquema.Planillas.Checked();}"
                                 
                                 configuracionGridView.SettingsPager.PageSize = "1000"
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "pag_codigo"
                                                                       configuracionColumn.Caption = "Pago"
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                       ComboBoxPropiedades.DataSource = objEsquema.fnc_obtener_pago
                                                                       ComboBoxPropiedades.TextField = "pag_nombre"
                                                                       ComboBoxPropiedades.ValueField = "pag_codigo"
                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                       ComboBoxPropiedades.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                                                                       configuracionColumn.Width = 200
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "esq_codigo"
                                                                       configuracionColumn.Caption = "Código"
                                                                       configuracionColumn.Width = 50
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "nombre_esquema"
                                                                       configuracionColumn.Caption = "Esquema"
                                                                       configuracionColumn.Width = 580
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.Caption = "Han salido"
                                                                       configuracionColumn.SetDataItemTemplateContent(Sub(configuracionItemContent)
                                                                                                                          configuracionItemContent.ViewStateMode = System.Web.UI.ViewStateMode.Enabled
                                                                                                                          Dim intCodEsquema As Integer = DataBinder.Eval(configuracionItemContent.DataItem, "esq_codigo")
                                                                                                                          Dim strRadio As String
                                                                                                                          strRadio = String.Format("<input type='radio' name='radioHasalido_{0}' id={0} onchange='objManejoEsquema.Planillas.ManejoRadioButton(this)' value='1'>", intCodEsquema)
                                                                                                                          
                                                                                                                          ViewContext.Writer.Write(strRadio)
                                                                                                                      End Sub)
                                                                       configuracionColumn.Width = 70
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.Caption = "No ha salido"
                                                                       configuracionColumn.SetDataItemTemplateContent(Sub(configuracionItemContent)
                                                                                                                          configuracionItemContent.ViewStateMode = System.Web.UI.ViewStateMode.Enabled
                                                                                                                          Dim intCodEsquema As Integer = DataBinder.Eval(configuracionItemContent.DataItem, "esq_codigo")
                                                                                                                          Dim strRadio As String
                                                                                                                          
                                                                                                                          strRadio = String.Format("<input type='radio' name='radioHasalido_{0}' id={0} onchange='objManejoEsquema.Planillas.ManejoRadioButton(this)' value='0'>", intCodEsquema)
                                                                                                                          
                                                                                                                          ViewContext.Writer.Write(strRadio)
                                                                                                                      End Sub)
                                                                       configuracionColumn.Width = 85
                                                                   End Sub)
                                 configuracionGridView.Settings.VerticalScrollBarMode = ScrollBarMode.Auto
                                 configuracionGridView.Settings.VerticalScrollableHeight = 246
                             End Sub).Bind(Model).GetHtml()
%>