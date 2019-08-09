<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    Html.DevExpress().GridView(Sub(setting)
                                   setting.Name = "GridPersonasHogar"
                                   setting.Caption = "Personas en el Hogar"
                                   setting.CallbackRouteValues = New With {Key .Controller = "Incorporaciones", Key .Action = "editarInformacionPersonasPre_Actualizar"}
                                   setting.SettingsEditing.BatchUpdateRouteValues = New With {Key .Controller = "Incorporaciones", Key .Action = "editarInformacionPersonasPre_Actualizar"}
                                   setting.Theme = "DevEx"
                                   setting.KeyFieldName = "pre_personas_actualizacion"
                                   setting.Styles.Header.Font.Bold = True
                                   setting.Styles.Header.Font.Italic = True
                                   setting.Styles.Header.Font.Name = "Arial"
                                   setting.Styles.Header.Font.Size = 10
                                   setting.StylesPager.Pager.Paddings.PaddingBottom = 20
                                   setting.StylesPager.Pager.Paddings.PaddingLeft = 10
                                   setting.StylesPager.Pager.Paddings.PaddingRight = 5
                                   setting.SettingsPager.PageSize = 25
                                   setting.Styles.Header.Border.BorderColor = System.Drawing.Color.Black
                                   setting.Styles.Header.Border.BorderStyle = WebControls.BorderStyle.Outset
                                   setting.Width = 800
                                   setting.CommandColumn.ShowNewButtonInHeader = True
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_persona"
                                                           columna.Caption = "Código"
                                                           columna.EditCellStyle.CssClass = "hidden"
                                                           columna.EditFormCaptionStyle.CssClass = "hidden"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_identidad"
                                                           columna.Caption = "N° de Identidad"
                                                           columna.ColumnType = MVCxGridViewColumnType.TextBox
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.MaxLength = 13
                                                           txtEditPropieties.ClientSideEvents.KeyUp = "function(s,e){ Fnc_ValidarDatos(2,s) }"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_nombre1"
                                                           columna.Caption = "Primer Nombre"
                                                           columna.ColumnType = MVCxGridViewColumnType.TextBox
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.ClientSideEvents.KeyUp = "function(s,e){ Fnc_ValidarDatos(1,s) }"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_nombre2"
                                                           columna.Caption = "Segundo Nombre"
                                                           columna.ColumnType = MVCxGridViewColumnType.TextBox
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.ClientSideEvents.KeyUp = "function(s,e){ Fnc_ValidarDatos(1,s) }"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_apellido1"
                                                           columna.Caption = "Primer Apellido"
                                                           columna.ColumnType = MVCxGridViewColumnType.TextBox
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.ClientSideEvents.KeyUp = "function(s,e){ Fnc_ValidarDatos(1,s) }"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_apellido2"
                                                           columna.Caption = "Segundo Apellido"
                                                           columna.ColumnType = MVCxGridViewColumnType.TextBox
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.ClientSideEvents.KeyUp = "function(s,e){ Fnc_ValidarDatos(1,s) }"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_sexoD"
                                                           columna.Caption = "Sexo"
                                                           columna.ColumnType = MVCxGridViewColumnType.ComboBox
                                                           Dim ComboSexo As ComboBoxProperties = columna.PropertiesEdit
                                                           ComboSexo.Items.Add("Masculino", "Masculino")
                                                           ComboSexo.Items.Add("Femenino", "Femenino")
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.Caption = "Fecha de Nacimiento"
                                                           columna.FieldName = "per_fch_nacimiento"
                                                           columna.EditFormSettings.Visible = True
                                                           columna.ColumnType = MVCxGridViewColumnType.DateEdit
                                                           Dim DateEditPropeties As DateEditProperties = columna.PropertiesEdit
                                                           DateEditPropeties.MaxDate = Today()
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_estadoD"
                                                           columna.Caption = "Estado"
                                                           columna.Width = 100
                                                           columna.ColumnType = MVCxGridViewColumnType.ComboBox
                                                           Dim ComboEstado As ComboBoxProperties = columna.PropertiesEdit
                                                           ComboEstado.Items.Add("Activo", "Activo")
                                                           ComboEstado.Items.Add("Falleció", "Falleció")
                                                           ComboEstado.Items.Add("Inactivo", "Inactivo")
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_estado"
                                                           columna.Caption = "EstadoC"
                                                           columna.CellStyle.CssClass = "hidden"
                                                           columna.HeaderStyle.CssClass = "hidden"
                                                           columna.EditCellStyle.CssClass = "hidden"
                                                           columna.EditFormCaptionStyle.CssClass = "hidden"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_sexo"
                                                           columna.Caption = "SexoC"
                                                           columna.CellStyle.CssClass = "hidden"
                                                           columna.HeaderStyle.CssClass = "hidden"
                                                           columna.EditCellStyle.CssClass = "hidden"
                                                           columna.EditFormCaptionStyle.CssClass = "hidden"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "pre_personas_actualizacion"
                                                           columna.Caption = "pre_personas_actualizacion"
                                                           columna.CellStyle.CssClass = "hidden"
                                                           columna.HeaderStyle.CssClass = "hidden"
                                                           columna.EditCellStyle.CssClass = "hidden"
                                                           columna.EditFormCaptionStyle.CssClass = "hidden"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.Caption = "actualizado"
                                                           columna.FieldName = "per_Actualizado"
                                                           columna.CellStyle.CssClass = "hidden"
                                                           columna.HeaderStyle.CssClass = "hidden"
                                                           columna.EditFormSettings.Visible = DefaultBoolean.False
                                                       End Sub)
                                   setting.CommandColumn.ShowEditButton = True
                                   setting.CommandColumn.Visible = True
                                   setting.SettingsBehavior.ConfirmDelete = True
                                   setting.Columns.Add(Sub(columna)
                                                           columna.Caption = "Titular"
                                                           columna.FieldName = "per_titularD"
                                                           columna.ColumnType = MVCxGridViewColumnType.ComboBox
                                                           Dim ComboTitular As ComboBoxProperties = columna.PropertiesEdit
                                                           ComboTitular.Items.Add("Es Titular", "Es Titular")
                                                           ComboTitular.Items.Add("No es Titular", "No Es Titular")
                                                           columna.Width = 100
                                                           ComboTitular.ClientSideEvents.SelectedIndexChanged = "function(s,e){fnc_validar_pre_cambio_titular(s,e)}"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_edad"
                                                           columna.Caption = "per_edad"
                                                           columna.CellStyle.CssClass = "hidden"
                                                           columna.HeaderStyle.CssClass = "hidden"
                                                           columna.EditCellStyle.CssClass = "hidden"
                                                           columna.EditFormCaptionStyle.CssClass = "hidden"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_ciclo"
                                                           columna.Caption = "per_ciclo"
                                                           columna.CellStyle.CssClass = "hidden"
                                                           columna.HeaderStyle.CssClass = "hidden"
                                                           columna.EditCellStyle.CssClass = "hidden"
                                                           columna.EditFormCaptionStyle.CssClass = "hidden"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_titular"
                                                           columna.Caption = "per_titular"
                                                           columna.CellStyle.CssClass = "hidden"
                                                           columna.HeaderStyle.CssClass = "hidden"
                                                           columna.EditCellStyle.CssClass = "hidden"
                                                           columna.EditFormCaptionStyle.CssClass = "hidden"
                                                       End Sub)
                                   setting.HtmlRowPrepared = New ASPxGridViewTableRowEventHandler(Sub(sender As Object, e As ASPxGridViewTableRowEventArgs)
                                                                                                      If (e.RowType = GridViewRowType.Data) Then
                                                                                                          If (Not IsDBNull(e.GetValue("per_Actualizado"))) Then
                                                                                                              e.Row.BackColor = System.Drawing.Color.Salmon
                                                                                                          End If
                                                                                                      End If
                                                                                                  End Sub)
                                   setting.CellEditorInitialize = New ASPxGridViewEditorEventHandler(Sub(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs)
                                                                                                         Dim gv = DirectCast(sender, MVCxGridView)
                                                                                                         Dim l = gv.IsNewRowEditing
                                                                                                         If gv.IsNewRowEditing Then
                                                                                                             If e.Column.FieldName = "per_estado" Then
                                                                                                                 e.Editor.CssClass = "hidden"
                                                                                                                 e.Column.EditFormCaptionStyle.ForeColor = System.Drawing.Color.AliceBlue
                                                                                                             End If
                                                                                                         End If
                                                                                                     End Sub)
                               End Sub).Bind(Model).GetHtml()
%>