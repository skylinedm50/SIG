<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<%


    Html.DevExpress().GridView(Sub(setting)
                                   setting.Name = "gvpersonas" + Model.Rows(0).item(0).ToString()
                                   setting.CallbackRouteValues = New With {Key .Controller = "Administracion", Key .Action = "CallbackInformacionHogarImprimir", Key .Hogar = Model.Rows(0).item(0)}
                                   setting.KeyFieldName = "PER_PERSONA"
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
                                   setting.Width = 1000
                                   setting.Columns.Add(Sub(columna)
                                                           columna.Caption = "Código"
                                                           columna.FieldName = "PER_PERSONA"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.Caption = "Identidad"
                                                           columna.FieldName = "PER_IDENTIDAD"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.Caption = "Nombre"
                                                           columna.FieldName = "NOMBRE"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.Caption = "Fecha de Nacimiento"
                                                           columna.FieldName = "PER_FCH_NACIMIENTO"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.Caption = "Sexo"
                                                           columna.FieldName = "SEXOD"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.Caption = "Edad en RUP"
                                                           columna.FieldName = "CICLO_EDAD"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.Caption = "Estado"
                                                           columna.FieldName = "PER_ESTADO_DESCRIPCION"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.Caption = "Titular"
                                                           columna.SetDataItemTemplateContent(Sub(c)
                                                                                                  Html.DevExpress().CheckBox(Sub(stn)
                                                                                                                                 stn.Name = "chkName" + DataBinder.Eval(c.DataItem, "PER_PERSONA").ToString()
                                                                                                                                 stn.Enabled = False
                                                                                                                                 If DataBinder.Eval(c.DataItem, "PER_TITULAR") = 1 Then
                                                                                                                                     stn.Checked = True
                                                                                                                                 End If
                                                                                                                             End Sub).GetHtml()
                                                                                              End Sub)
                                                       End Sub)
                                   setting.SettingsDetail.ShowDetailRow = True
                                   setting.SettingsDetail.AllowOnlyOneMasterRowExpanded = True
                                   setting.SetDetailRowTemplateContent(Sub(s)

                                                                           Html.RenderAction("Grid_Detalle_Historico_Actualizaciones", "Incorporaciones", New With {Key .per_persona = DataBinder.Eval(s.DataItem, "per_persona")})
                                                                       End Sub)

                                   setting.HtmlRowPrepared = New ASPxGridViewTableRowEventHandler(Sub(sender As Object, e As ASPxGridViewTableRowEventArgs)
                                                                                                      If (e.RowType = GridViewRowType.Data) Then
                                                                                                          If (e.GetValue("ACTUALIZADO") = 1) Then
                                                                                                              e.Row.BackColor = System.Drawing.Color.MediumAquamarine
                                                                                                          End If
                                                                                                      End If
                                                                                                  End Sub)

                               End Sub).Bind(Model).GetHtml()

%>
