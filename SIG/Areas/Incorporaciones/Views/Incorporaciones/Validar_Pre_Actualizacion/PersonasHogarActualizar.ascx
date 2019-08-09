<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Drawing" %>

<%   

    Html.DevExpress().GridView(Sub(setting)
                                   setting.Name = "GridPersonasHogar"
                                   setting.Caption = "Personas en el Hogar"
                                   setting.SettingsEditing.AddNewRowRouteValues = New With {Key .Controller = "Incorporaciones", Key .Action = "Agregar_nueva_persona", Key .hogar = ViewData("hogar")}
                                   setting.CallbackRouteValues = New With {Key .Controller = "Incorporaciones", Key .Action = "CallbackPersonasActualizar"}
                                   setting.SettingsEditing.UpdateRowRouteValues = New With {Key .Controller = "Incorporaciones", Key .Action = "editarInformacionPersonasActualizar", Key .hogar = ViewData("hogar")}
                                   setting.SettingsEditing.DeleteRowRouteValues = New With {Key .Controller = "Incorporaciones", Key .Action = "Fnc_EliminarRegistroNuevo"}
                                   setting.Theme = "DevEx"
                                   setting.KeyFieldName = "PK"
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
                                                           columna.EditFormSettings.VisibleIndex = 1
                                                           columna.EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.None
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.ClientInstanceName = "EditPer_Persona"
                                                           txtEditPropieties.Style.CssClass = "hidden"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_identidad"
                                                           columna.Caption = "N° de Identidad"
                                                           columna.EditFormSettings.VisibleIndex = 0
                                                           columna.ColumnType = MVCxGridViewColumnType.TextBox
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.MaxLength = 13
                                                           txtEditPropieties.ClientSideEvents.KeyUp = "function(s,e){ Fnc_ValidarDatos(2,s) }"
                                                           txtEditPropieties.ClientInstanceName = "EditIdentidad"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_nombre1"
                                                           columna.Caption = "Primer Nombre"
                                                           columna.EditFormSettings.VisibleIndex = 2
                                                           columna.ColumnType = MVCxGridViewColumnType.TextBox
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.ClientInstanceName = "EditNombre1"
                                                           txtEditPropieties.ClientSideEvents.KeyUp = "function(s,e){ Fnc_ValidarDatos(1,s) }"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_nombre2"
                                                           columna.Caption = "Segundo Nombre"
                                                           columna.EditFormSettings.VisibleIndex = 3
                                                           columna.ColumnType = MVCxGridViewColumnType.TextBox
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.ClientInstanceName = "EditNombre2"
                                                           txtEditPropieties.ClientSideEvents.KeyUp = "function(s,e){ Fnc_ValidarDatos(1,s) }"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_apellido1"
                                                           columna.Caption = "Primer Apellido"
                                                           columna.EditFormSettings.VisibleIndex = 4
                                                           columna.ColumnType = MVCxGridViewColumnType.TextBox
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.ClientInstanceName = "EditApellido1"
                                                           txtEditPropieties.ClientSideEvents.KeyUp = "function(s,e){ Fnc_ValidarDatos(1,s) }"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_apellido2"
                                                           columna.Caption = "Segundo Apellido"
                                                           columna.EditFormSettings.VisibleIndex = 5
                                                           columna.ColumnType = MVCxGridViewColumnType.TextBox
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.ClientInstanceName = "EditApellido2"
                                                           txtEditPropieties.ClientSideEvents.KeyUp = "function(s,e){ Fnc_ValidarDatos(1,s) }"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_sexoD"
                                                           columna.Caption = "Sexo"
                                                           columna.EditFormSettings.VisibleIndex = 6
                                                           columna.ColumnType = MVCxGridViewColumnType.ComboBox
                                                           Dim ComboSexo As ComboBoxProperties = columna.PropertiesEdit
                                                           ComboSexo.ClientInstanceName = "CbEditSexoD"
                                                           ComboSexo.Items.Add("Masculino", "Masculino")
                                                           ComboSexo.Items.Add("Femenino", "Femenino")
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.Caption = "Fecha de Nacimiento"
                                                           columna.FieldName = "per_fch_nacimiento"
                                                           columna.EditFormSettings.Visible = True
                                                           columna.EditFormSettings.VisibleIndex = 7
                                                           columna.ColumnType = MVCxGridViewColumnType.DateEdit
                                                           Dim DateEditPropeties As DateEditProperties = columna.PropertiesEdit
                                                           DateEditPropeties.EditFormatString = "dd/MM/yyyy"
                                                           DateEditPropeties.DisplayFormatString = "dd/MM/yyyy"
                                                           DateEditPropeties.ClientInstanceName = "EditfchNacimiento"
                                                           DateEditPropeties.MaxDate = Today()
                                                           DateEditPropeties.MinDate = "1/1/1800"
                                                           DateEditPropeties.ClientSideEvents.ValueChanged = "function(s,e){fnc_validarCambioFechaNacimiento(s,e)}"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.Caption = "Edad en RUP"
                                                           columna.FieldName = "Ciclo_Edad"
                                                           columna.EditFormSettings.Visible = True
                                                           columna.EditFormSettings.VisibleIndex = 10
                                                           columna.ColumnType = MVCxGridViewColumnType.TextBox
                                                           columna.EditFormSettings.Visible = DefaultBoolean.False
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_estadoD"
                                                           columna.Caption = "Estado"
                                                           columna.ColumnType = MVCxGridViewColumnType.ComboBox
                                                           Dim ComboEstado As ComboBoxProperties = columna.PropertiesEdit
                                                           columna.EditFormSettings.VisibleIndex = 9
                                                           ComboEstado.ClientInstanceName = "EditComboEstado"
                                                           ComboEstado.Items.Add("Activo", "Activo")
                                                           ComboEstado.Items.Add("Falleció", "Falleció")
                                                           ComboEstado.Items.Add("Desagregado", "Desagregado")
                                                           ComboEstado.Items.Add("No remitido por RUP", "No remitido por RUP")
                                                           ComboEstado.Items.Add("Desagregación por Ficha RUP", "Desagregación por Ficha RUP")
                                                           ComboEstado.Items.Add("Suspensión temporal por revisión", "Suspensión temporal por revisión")
                                                           ComboEstado.ClientSideEvents.DropDown = "function(s,e){ fnc_SelecionarEstadosAnteriores(s,e) }"
                                                           ComboEstado.ClientSideEvents.ValueChanged = "function(s,e){ fnc_CambioEstadoPersonaActualizar(s,e) }"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_estado"
                                                           columna.Caption = "EstadoC"
                                                           columna.EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.None
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.ClientInstanceName = "EditTxtEstadoC"
                                                           columna.CellStyle.CssClass = "hidden"
                                                           columna.HeaderStyle.CssClass = "hidden"
                                                           txtEditPropieties.Style.CssClass = "hidden"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_sexo"
                                                           columna.Caption = "SexoC"
                                                           columna.EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.None
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.ClientInstanceName = "EditSexoC"
                                                           txtEditPropieties.Style.CssClass = "hidden"
                                                           columna.CellStyle.CssClass = "hidden"
                                                           columna.HeaderStyle.CssClass = "hidden"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "pre_personas_actualizacion"
                                                           columna.Caption = "pre_personas_actualizacion"
                                                           columna.EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.None
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.ClientInstanceName = "EditPre_PersonaActualizacion"
                                                           columna.CellStyle.CssClass = "hidden"
                                                           columna.HeaderStyle.CssClass = "hidden"
                                                           txtEditPropieties.Style.CssClass = "hidden"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.Caption = "per_Actualizado"
                                                           columna.FieldName = "per_Actualizado"
                                                           columna.EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.None
                                                           columna.CellStyle.CssClass = "hidden"
                                                           columna.HeaderStyle.CssClass = "hidden"
                                                           columna.EditFormSettings.Visible = DefaultBoolean.False
                                                       End Sub)
                                   setting.CommandColumn.ShowEditButton = True
                                   setting.CommandColumn.ShowDeleteButton = True
                                   setting.CommandColumn.Visible = True
                                   setting.SettingsBehavior.ConfirmDelete = True
                                   setting.Columns.Add(Sub(columna)
                                                           columna.Caption = "Titular"
                                                           columna.FieldName = "per_titularD"
                                                           columna.EditFormSettings.VisibleIndex = 9
                                                           columna.ColumnType = MVCxGridViewColumnType.ComboBox
                                                           Dim ComboTitular As ComboBoxProperties = columna.PropertiesEdit
                                                           ComboTitular.ClientInstanceName = "EditComboTitular"
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
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.ClientInstanceName = "EditTxtEdad"
                                                           txtEditPropieties.Style.CssClass = "hidden"
                                                           columna.EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.None
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_ciclo"
                                                           columna.Caption = "per_ciclo"
                                                           columna.CellStyle.CssClass = "hidden"
                                                           columna.HeaderStyle.CssClass = "hidden"
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.ClientInstanceName = "EditTxtCiclo"
                                                           txtEditPropieties.Style.CssClass = "hidden"
                                                           columna.EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.None
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_titular"
                                                           columna.Caption = "per_titular"
                                                           columna.EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.None
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.ClientInstanceName = "EditTxtTitular"
                                                           txtEditPropieties.Style.CssClass = "hidden"
                                                           columna.CellStyle.CssClass = "hidden"
                                                           columna.HeaderStyle.CssClass = "hidden"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "pre_persona"
                                                           columna.Caption = "pre_persona"
                                                           columna.EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.None
                                                           Dim txtEditPropieties As TextBoxProperties = columna.PropertiesEdit
                                                           txtEditPropieties.ClientInstanceName = "EditTxtPre_Persona"
                                                           txtEditPropieties.Style.CssClass = "hidden"
                                                           columna.CellStyle.CssClass = "hidden"
                                                           columna.HeaderStyle.CssClass = "hidden"
                                                       End Sub)

                                   setting.HtmlRowPrepared = New ASPxGridViewTableRowEventHandler(Sub(sender As Object, e As ASPxGridViewTableRowEventArgs)
                                                                                                      If (e.RowType = GridViewRowType.Data) Then
                                                                                                          If (Not IsDBNull(e.GetValue("per_Actualizado"))) Then
                                                                                                              e.Row.BackColor = System.Drawing.Color.Salmon
                                                                                                          ElseIf (Not IsDBNull(e.GetValue("Desagregar"))) Then
                                                                                                              e.Row.BackColor = System.Drawing.Color.MediumAquamarine
                                                                                                          End If
                                                                                                      End If

                                                                                                  End Sub)

                                   setting.CommandButtonInitialize = New ASPxGridViewCommandButtonEventHandler(Sub(sender As Object, e As ASPxGridViewCommandButtonEventArgs)

                                                                                                                   If Not e.IsEditingRow Then
                                                                                                                       If (e.ButtonType = ColumnCommandButtonType.Delete) Then
                                                                                                                           If Not IsDBNull(Model.Rows(e.VisibleIndex).item("per_Actualizado")) Then
                                                                                                                               If (Model.Rows(e.VisibleIndex).item("per_Actualizado") <> 2) Then
                                                                                                                                   e.Visible = False
                                                                                                                               End If
                                                                                                                           Else
                                                                                                                               e.Visible = False
                                                                                                                           End If

                                                                                                                       End If
                                                                                                                   End If

                                                                                                               End Sub)

                               End Sub).Bind(Model).GetHtml()
%>
