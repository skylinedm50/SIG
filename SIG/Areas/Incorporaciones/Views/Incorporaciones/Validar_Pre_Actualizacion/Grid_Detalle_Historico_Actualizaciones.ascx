<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Html.DevExpress().GridView(Sub(grid)
                                   grid.Name = "detalle_actualizacion"
                                   grid.Columns.AddBand(Sub(band)
                                                            band.Caption = "Identidad"
                                                            band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                            band.Columns.Add("IDENTIDAD_ANTES", "Antes")
                                                            band.Columns.Add("IDENTIDAD_DESPUES", "Despues")
                                                        End Sub)
                                   grid.Columns.AddBand(Sub(band)
                                                            band.Caption = "Nombre1"
                                                            band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                            band.Columns.Add("NOMBRE1_ANTES", "Antes")
                                                            band.Columns.Add("NOMBRE1_DESPUES", "Despues")
                                                        End Sub)
                                   grid.Columns.AddBand(Sub(band)
                                                            band.Caption = "Nombre2"
                                                            band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                            band.Columns.Add("NOMBRE2_ANTES", "Antes")
                                                            band.Columns.Add("NOMBRE2_DESPUES", "Despues")
                                                        End Sub)
                                   grid.Columns.AddBand(Sub(band)
                                                            band.Caption = "Apellido1"
                                                            band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                            band.Columns.Add("APELLIDO1_ANTES", "Antes")
                                                            band.Columns.Add("APELLIDO1_DESPUES", "Despues")
                                                        End Sub)
                                   grid.Columns.AddBand(Sub(band)
                                                            band.Caption = "Apellido2"
                                                            band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                            band.Columns.Add("APELLIDO2_ANTES", "Apellido2 antes")
                                                            band.Columns.Add("APELLIDO2_DESPUES", "Apellido2 despues")
                                                        End Sub)
                                   grid.Columns.AddBand(Sub(band)
                                                            band.Caption = "Género"
                                                            band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                            band.Columns.Add("SEXO_ANTES", "Antes")
                                                            band.Columns.Add("SEXO_DESPUES", "Despues")
                                                        End Sub)
                                   grid.Columns.AddBand(Sub(band)
                                                            band.Caption = "Fecha de Nacimiento"
                                                            band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                            band.Columns.Add("FCH_NACIMIENTO_ANTES", " Antes").PropertiesEdit.DisplayFormatString = "d"
                                                            band.Columns.Add("FCH_NACIMIENTO_DESPUES", "Despues").PropertiesEdit.DisplayFormatString = "d"
                                                        End Sub)
                                   grid.Columns.AddBand(Sub(band)
                                                            band.Caption = "Estado"
                                                            band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                            band.Columns.Add("ESTADO_ANTES", "Antes")
                                                            band.Columns.Add("ESTADO_DESPUES", "Despues")
                                                        End Sub)
                                   grid.Columns.AddBand(Sub(band)
                                                            band.Caption = "Titular"
                                                            band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                            band.Columns.Add("TITULAR_ANTES", "Antes")
                                                            band.Columns.Add("TITULAR_DESPUES", "Despues")
                                                        End Sub)
                                   grid.Columns.AddBand(Sub(band)
                                                            band.Caption = "Actualización"
                                                            band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                            band.Columns.Add("PER_FCH_REGISTRO_PRE_ACTUALIZACION", "Fecha que se actualizó")
                                                            band.Columns.Add("NOMBRE_USUARIO", "Usuario que realizo la actualización")
                                                        End Sub)
                                   
                                   grid.Enabled = False
                                   grid.Styles.Header.Font.Bold = True
                                   grid.Styles.Header.Font.Italic = True
                                   grid.Styles.Header.Font.Name = "Arial"
                                   grid.Styles.Header.Font.Size = 8
                                   grid.SettingsPager.PageSize = 25
                                   grid.Styles.Header.Border.BorderColor = System.Drawing.Color.Black
                                   grid.Styles.Header.Border.BorderStyle = WebControls.BorderStyle.Outset
                                   grid.Styles.Cell.Font.Size = 8
                                                                     
                                   grid.HtmlRowPrepared = New ASPxGridViewTableRowEventHandler(Sub(sender As Object, e As ASPxGridViewTableRowEventArgs)
                                                                                                   If (e.RowType = GridViewRowType.Data) Then
                                                                                                       If (Not IsDBNull(e.GetValue("ORDEN"))) Then
                                                                                                           If (e.GetValue("ORDEN") = 1) Then
                                                                                                               e.Row.BackColor = System.Drawing.Color.SeaShell
                                                                                                           End If
                                                                                                       End If
                                                                                                       
                                                                                                   End If
                                                                                                      
                                                                                               End Sub)
                                   grid.HtmlDataCellPrepared = New ASPxGridViewTableDataCellEventHandler(Sub(sender As Object, e As ASPxGridViewTableDataCellEventArgs)
                                                                                                             Dim Columnas As New List(Of Integer)(New Integer() {1})
                                                                                                             If (e.CellValue.ToString() <> "SIN CAMBIO" And (Columnas.Contains(e.DataColumn.VisibleIndex))) Then
                                                                                                                 e.Cell.BackColor = System.Drawing.Color.LightCyan
                                                                                                             End If
                                                                                                         End Sub)
                                   
                               End Sub).Bind(Model).GetHtml()
    
    
 %>