<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress.FormLayout(
       Sub(flo)
           flo.Name = "floDetalle"
           flo.Items.Add(
               Sub(item)
                   item.Caption = "Pago"
                   item.SetNestedContent(
                       Sub()
                           Html.DevExpress.TextBox(
                                        Sub(txt)
                                            txt.Name = "txtPago"
                                            txt.ClientEnabled = False
                                            txt.Width = 300
                                            txt.Text = ViewData("pago")
                                        End Sub).GetHtml()
                       End Sub)
               End Sub)
           flo.Items.Add(
               Sub(item)
                   item.Caption = "Esquema"
                   item.SetNestedContent(
                       Sub()
                           Html.DevExpress.TextBox(
                                        Sub(txt)
                                            txt.Name = "txtEsquema"
                                            txt.ClientEnabled = False
                                            txt.Width = 500
                                            txt.Text = ViewData("esquema")
                                        End Sub).GetHtml()
                       End Sub)
               End Sub)
           flo.Items.Add(
               Sub(item)
                   item.Caption = "Estado"
                   item.SetNestedContent(
                       Sub()
                           Html.DevExpress.TextBox(
                                        Sub(txt)
                                            txt.Name = "txtEstado"
                                            txt.ClientEnabled = False
                                            txt.Width = 300
                                            txt.Text = ViewData("estado")
                                        End Sub).GetHtml()
                       End Sub)
               End Sub)
       End Sub).GetHtml()%>
<br />
<br />
<% Html.DevExpress.GridView(
        Sub(gdv)
            gdv.Name = "gdvMontos"
            gdv.Caption = "Montos"
            gdv.Width = Unit.Percentage(100)
            gdv.SettingsPager.PageSize = 100
            gdv.SettingsBehavior.AllowDragDrop = False
            gdv.SettingsBehavior.AllowGroup = False
            gdv.SettingsBehavior.AllowSort = False
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "confm_basico"
                    col.Caption = "Básico"
                    col.PropertiesEdit.DisplayFormatString = "c"
                End Sub)
            gdv.Columns.AddBand(
                Sub(band)
                    band.Caption = "Salud"
                    band.Columns.Add(
                        Sub(col)
                            col.FieldName = "confm_nivel1_1"
                            col.Caption = "1 Niño(a)"
                            col.PropertiesEdit.DisplayFormatString = "c"
                        End Sub)
                    band.Columns.Add(
                        Sub(col)
                            col.FieldName = "confm_nivel1_2"
                            col.Caption = "2 Niños(a) o más"
                            col.PropertiesEdit.DisplayFormatString = "c"
                        End Sub)
                End Sub)
            gdv.Columns.AddBand(
                Sub(band)
                    band.Caption = "1er y 2do Ciclo Educación"
                    band.Columns.Add(
                        Sub(col)
                            col.FieldName = "confm_nivel2_1"
                            col.Caption = "1 Niño(a)"
                            col.PropertiesEdit.DisplayFormatString = "c"
                        End Sub)
                    band.Columns.Add(
                        Sub(col)
                            col.FieldName = "confm_nivel2_2"
                            col.Caption = "2 Niños(a) o más"
                            col.PropertiesEdit.DisplayFormatString = "c"
                        End Sub)
                End Sub)
            gdv.Columns.AddBand(
                Sub(band)
                    band.Caption = "3er Ciclo Educación"
                    band.Columns.Add(
                        Sub(col)
                            col.FieldName = "confm_nivel3_1"
                            col.Caption = "1 Niño(a)"
                            col.PropertiesEdit.DisplayFormatString = "c"
                        End Sub)
                    band.Columns.Add(
                        Sub(col)
                            col.FieldName = "confm_nivel3_2"
                            col.Caption = "2 Niños(a) o más"
                            col.PropertiesEdit.DisplayFormatString = "c"
                        End Sub)
                End Sub)
        End Sub).Bind(ViewData("monto")).GetHtml()%>
<br />
<br />
<% Html.DevExpress.GridView(
        Sub(gdv)
            gdv.Name = "gdvCoberturaPlanilla"
            gdv.Caption = "Cobertura Planilla"
            gdv.Width = Unit.Percentage(100)
            gdv.SettingsPager.PageSize = 100
            gdv.SettingsBehavior.AllowDragDrop = False
            gdv.SettingsBehavior.AllowGroup = False
            gdv.SettingsBehavior.AllowSort = False
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "departamento"
                    col.Caption = "Departamento"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "municipio"
                    col.Caption = "Municipio"
                    col.PropertiesEdit.NullDisplayText = "Todo"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "aldea"
                    col.Caption = "Aldea"
                    col.PropertiesEdit.NullDisplayText = "Todo"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "caserio"
                    col.Caption = "Caserío"
                    col.PropertiesEdit.NullDisplayText = "Todo"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "escuela"
                    col.Caption = "Escuela"
                    col.PropertiesEdit.NullDisplayText = "Todo"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "cs_salud"
                    col.Caption = "Centro de salud"
                    col.PropertiesEdit.NullDisplayText = "Todo"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "esqc_planillas_no"
                    col.Caption = "Planilla No"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "esqc_planillas_si"
                    col.Caption = "Planilla Si"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "esqc_cargas_si"
                    col.Caption = "Pertenece Carga"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "esqc_cargas_no"
                    col.Caption = "No Pertenece Carga"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "esqc_signo"
                    col.Caption = "Acción"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "descripcion_origen"
                    col.Caption = "Origen"
                End Sub)
        End Sub).Bind(ViewData("coberturaPlanilla")).GetHtml()%>
<br />
<br />
<% Html.DevExpress.GridView(
        Sub(gdv)
            gdv.Name = "gdvEstablecimientoFondos"
            gdv.Caption = "Establecimiento de Fondos"
            gdv.Width = Unit.Percentage(100)
            gdv.SettingsPager.PageSize = 100
            gdv.SettingsBehavior.AllowDragDrop = False
            gdv.SettingsBehavior.AllowGroup = False
            gdv.SettingsBehavior.AllowSort = False
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "departamento"
                    col.Caption = "Departamento"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "municipio"
                    col.Caption = "Municipio"
                    col.PropertiesEdit.NullDisplayText = "Todo"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "aldea"
                    col.Caption = "Aldea"
                    col.PropertiesEdit.NullDisplayText = "Todo"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "caserio"
                    col.Caption = "Caserío"
                    col.PropertiesEdit.NullDisplayText = "Todo"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "fond_nombre"
                    col.Caption = "fondo"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "fesq_signo"
                    col.Caption = "Acción"
                End Sub)
        End Sub).Bind(ViewData("establecimientoFondos")).GetHtml()%>
<br />
<br />
<% Html.DevExpress.GridView(
        Sub(gdv)
            gdv.Name = "gdvMecanismoPago"
            gdv.Caption = "Mecanismo de Pago"
            gdv.Width = Unit.Percentage(100)
            gdv.SettingsPager.PageSize = 100
            gdv.SettingsBehavior.AllowDragDrop = False
            gdv.SettingsBehavior.AllowGroup = False
            gdv.SettingsBehavior.AllowSort = False
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "departamento"
                    col.Caption = "Departamento"
                    col.PropertiesEdit.NullDisplayText = "Todo"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "municipio"
                    col.Caption = "Municipio"
                    col.PropertiesEdit.NullDisplayText = "Todo"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "aldea"
                    col.Caption = "Aldea"
                    col.PropertiesEdit.NullDisplayText = "Todo"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "caserio"
                    col.Caption = "Caserío"
                    col.PropertiesEdit.NullDisplayText = "Todo"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "Nombre_Pagador"
                    col.Caption = "Pagador"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "pesq_signo"
                    col.Caption = "Acción"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "descripcion_origen"
                    col.Caption = "Origen"
                End Sub)
        End Sub).Bind(ViewData("mecanismoPago")).GetHtml()%>
<br />
<br />
<% Html.DevExpress.GridView(
        Sub(gdv)
            gdv.Name = "gdvTransferenciasActuales"
            gdv.Caption = "Transferencias Actuales"
            gdv.Width = Unit.Percentage(100)
            gdv.SettingsPager.PageSize = 100
            gdv.SettingsBehavior.AllowDragDrop = False
            gdv.SettingsBehavior.AllowGroup = False
            gdv.SettingsBehavior.AllowSort = False
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "int_nombre"
                    col.Caption = "Intervalo"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "bon_anyo"
                    col.Caption = "Año"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "bon_fecha_ini"
                    col.Caption = "Fecha incio"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "bon_fecha_fin"
                    col.Caption = "Fecha fin"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "bon_fecha_elegibilidad"
                    col.Caption = "Fecha de elegibilidad"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "bon_detalle_meses"
                    col.Caption = "Detalle meses"
                End Sub)
        End Sub).Bind(ViewData("transferenciasActuales")).GetHtml()%>
<br />
<br />
<% Html.DevExpress.GridView(
        Sub(gdv)
            gdv.Name = "gdvCorrTransferencia"
            gdv.Caption = "Corresponsabilidad de Transferencia"
            gdv.Width = Unit.Percentage(100)
            gdv.SettingsPager.PageSize = 100
            gdv.SettingsBehavior.AllowDragDrop = False
            gdv.SettingsBehavior.AllowGroup = False
            gdv.SettingsBehavior.AllowSort = False
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "comp_descripcion"
                    col.Caption = "Componente"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "corr_descripcion"
                    col.Caption = "Corresponsabilidad"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "bonc_orden"
                    col.Caption = "Orden"
                End Sub)
        End Sub).Bind(ViewData("corrTransferencia")).GetHtml()%>
<br />
<br />
<% Html.DevExpress.GridView(
        Sub(gdv)
            gdv.Name = "gdvCorrApercibimiento"
            gdv.Caption = "Corresponsabilidad de Apercibimiento"
            gdv.Width = Unit.Percentage(100)
            gdv.SettingsPager.PageSize = 100
            gdv.SettingsBehavior.AllowDragDrop = False
            gdv.SettingsBehavior.AllowGroup = False
            gdv.SettingsBehavior.AllowSort = False
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "comp_descripcion"
                    col.Caption = "Componente"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "corr_nombre"
                    col.Caption = "Corresponsabilidad"
                End Sub)
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "bonca_orden"
                    col.Caption = "Orden"
                End Sub)
        End Sub).Bind(ViewData("corrApercibimiento")).GetHtml()%>
<br />
<br />
<% Html.DevExpress.GridView(
        Sub(gdv)
            gdv.Name = "gdvVerificaciones"
            gdv.Caption = "Otras Verificaciones"
            gdv.Width = Unit.Percentage(100)
            gdv.SettingsPager.PageSize = 100
            gdv.SettingsBehavior.AllowDragDrop = False
            gdv.SettingsBehavior.AllowGroup = False
            gdv.SettingsBehavior.AllowSort = False
            gdv.Columns.Add(
                Sub(col)
                    col.FieldName = "ver_nombre"
                    col.Caption = "Verificación"
                End Sub)
        End Sub).Bind(ViewData("verificacion")).GetHtml()%>
