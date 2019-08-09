<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>


<%
    Dim objDetalleCorresponsabilidad As New SIG.SIG.Areas.Corresponsabilidad.Models.cl_detalle_corresponsabilidad
    Dim objGridEditarDetalleCorresponsabilidad = Html.DevExpress.GridView(Sub(configuracionGridView As GridViewSettings)
                                                                              configuracionGridView.Name = "AspxGridViewDetalleCorresponsabilidad"
                                                                              configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Configuracion", Key .Action = "GridViewPartialDetalleCorresponsabilidad"}
                                                                              configuracionGridView.CommandColumn.Visible = True

                                                                              configuracionGridView.CommandColumn.CustomButtons.Add(New GridViewCommandColumnCustomButton() With {.ID = "btnUpdate", .Text = "Editar"})
                                                                              configuracionGridView.CommandColumn.CustomButtons.Add(New GridViewCommandColumnCustomButton() With {.ID = "btnDelete", .Text = "Borrar"})
                                                                              configuracionGridView.CommandColumn.HeaderStyle.CssClass = "HeadGridCorresNue"


                                                                              configuracionGridView.ClientSideEvents.CustomButtonClick = "function(s,e){ objCorrespo.Operaciones.Main(s, e); }"

                                                                              configuracionGridView.Caption = "Detalle de Corresponsabilidades"
                                                                              configuracionGridView.KeyFieldName = "cod_det_cor"

                                                                              configuracionGridView.SettingsPager.Visible = True
                                                                              configuracionGridView.Settings.ShowGroupPanel = False
                                                                              configuracionGridView.Settings.ShowFilterRow = True
                                                                              configuracionGridView.SettingsBehavior.AllowSelectByRowClick = True

                                                                              configuracionGridView.SettingsPager.PageSizeItemSettings.Visible = True
                                                                              configuracionGridView.SettingsPager.PageSizeItemSettings.Items = {"50", "100", "150"}
                                                                              configuracionGridView.SettingsPager.PageSize = 30
                                                                              configuracionGridView.Width = 1200

                                                                              configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                    configuracionColumn.FieldName = "cod_det_cor"
                                                                                                                    configuracionColumn.Caption = "Código"

                                                                                                                    configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit

                                                                                                                    Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit

                                                                                                                    SpinEditPropiedades.MinValue = 1
                                                                                                                    SpinEditPropiedades.MaxValue = 10000
                                                                                                                    SpinEditPropiedades.Width = 70
                                                                                                                End Sub)
                                                                              configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                    configuracionColumn.FieldName = "nom_det_corr"
                                                                                                                    configuracionColumn.Caption = "Nombre"
                                                                                                                    configuracionColumn.Width = 300
                                                                                                                End Sub)
                                                                              configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                    configuracionColumn.FieldName = "cod_tip_cor"
                                                                                                                    configuracionColumn.Caption = "Tipo corresponsabilidad"

                                                                                                                    configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                                                                    Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                                                                    ComboBoxPropiedades.DataSource = objDetalleCorresponsabilidad.fnc_obtener_tipo_corresponsabilidad
                                                                                                                    ComboBoxPropiedades.TextField = "nom_tip_cor"
                                                                                                                    ComboBoxPropiedades.ValueField = "cod_tip_cor"
                                                                                                                    ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                                                                End Sub)
                                                                              configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                    configuracionColumn.FieldName = "num_det_cor"
                                                                                                                    configuracionColumn.Caption = "Número"
                                                                                                                    configuracionColumn.ReadOnly = True

                                                                                                                    configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                                                                    Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit

                                                                                                                    SpinEditPropiedades.MinValue = 1
                                                                                                                    SpinEditPropiedades.MaxValue = 100
                                                                                                                    SpinEditPropiedades.Width = 60
                                                                                                                End Sub)
                                                                              configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                    configuracionColumn.FieldName = "año_det_cor"
                                                                                                                    configuracionColumn.Caption = "Año"

                                                                                                                    configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                                                                    Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit

                                                                                                                    SpinEditPropiedades.MinValue = 2014
                                                                                                                    SpinEditPropiedades.MaxValue = 2050
                                                                                                                    SpinEditPropiedades.Width = 80

                                                                                                                End Sub)
                                                                              configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                    configuracionColumn.FieldName = "par_ini_det_cor"
                                                                                                                    configuracionColumn.Caption = "Parcial prin/ini"

                                                                                                                    configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                                                                    Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                                                                    SpinEditPropiedades.MinValue = 1
                                                                                                                    SpinEditPropiedades.MaxValue = 4

                                                                                                                End Sub)
                                                                              configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                    configuracionColumn.FieldName = "par_fin_det_cor"
                                                                                                                    configuracionColumn.Caption = "Parcial final"

                                                                                                                    configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                                                                    Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                                                                    SpinEditPropiedades.MinValue = 1
                                                                                                                    SpinEditPropiedades.MaxValue = 4

                                                                                                                End Sub)
                                                                              configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                    configuracionColumn.FieldName = "fec_ini_det_cor"
                                                                                                                    configuracionColumn.Caption = "Fecha inicio"
                                                                                                                    configuracionColumn.Width = 120

                                                                                                                    configuracionColumn.ColumnType = MVCxGridViewColumnType.DateEdit
                                                                                                                    Dim DateEditPropiedades As DateEditProperties = configuracionColumn.PropertiesEdit
                                                                                                                    DateEditPropiedades.EditFormat = EditFormat.Custom
                                                                                                                    DateEditPropiedades.EditFormatString = "yyyy-MM-dd"

                                                                                                                    DateEditPropiedades.DisplayFormatInEditMode = True
                                                                                                                End Sub)
                                                                              configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                    configuracionColumn.FieldName = "fec_fin_det_cor"
                                                                                                                    configuracionColumn.Caption = "Fecha fin"
                                                                                                                    configuracionColumn.Width = 120

                                                                                                                    configuracionColumn.ColumnType = MVCxGridViewColumnType.DateEdit
                                                                                                                    Dim DateEditPropiedades As DateEditProperties = configuracionColumn.PropertiesEdit
                                                                                                                    DateEditPropiedades.EditFormat = EditFormat.Custom
                                                                                                                    DateEditPropiedades.EditFormatString = "yyyy-MM-dd"

                                                                                                                    DateEditPropiedades.DisplayFormatInEditMode = True
                                                                                                                End Sub)
                                                                          End Sub)
    If ViewData("EditError") IsNot Nothing Then
        objGridEditarDetalleCorresponsabilidad.SetEditErrorText(CStr(ViewData("EditError")))
    End If
    objGridEditarDetalleCorresponsabilidad.Bind(Model).Render()
%>
