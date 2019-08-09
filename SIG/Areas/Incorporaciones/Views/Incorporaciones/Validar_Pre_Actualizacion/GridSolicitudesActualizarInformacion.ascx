<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    
    Html.DevExpress().GridView(Sub(setting)
                                  
                                   setting.Name = "griSolicitudes"
                                   setting.Caption = "Solicitudes de Actualización"
                                   setting.CallbackRouteValues = New With {Key .Controller = "Incorporaciones", Key .Action = "GridSolicitudesActualizarInformacion"}
                                   setting.Theme = "DevEx"
                                   setting.KeyFieldName = "Codigo_Solicitud"
                                   setting.Styles.Header.Font.Bold = True
                                   setting.Styles.Header.Font.Italic = True
                                   setting.Styles.Header.Font.Name = "Arial"
                                   setting.Styles.Header.Font.Size = 10
                                   setting.StylesPager.Pager.Paddings.PaddingBottom = 20
                                   setting.StylesPager.Pager.Paddings.PaddingLeft = 10
                                   setting.StylesPager.Pager.Paddings.PaddingRight = 5
                                   setting.Styles.Header.Border.BorderColor = System.Drawing.Color.Black
                                   setting.Styles.Header.Border.BorderStyle = WebControls.BorderStyle.Outset
                                   setting.SettingsPager.PageSize = "25"
                                   setting.Width = 945
                                   setting.Settings.ShowFilterRow = True
                                   setting.Settings.ShowGroupPanel = True
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "per_hogar"
                                                           columna.Caption = "Código Hogar"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "desc_departamento"
                                                           columna.Caption = "Departamento"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "desc_municipio"
                                                           columna.Caption = "Municipio"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "desc_aldea"
                                                           columna.Caption = "Aldea"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "desc_caserio"
                                                           columna.Caption = "Caserío"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "Participante"
                                                           columna.Caption = "Nombre del Titular"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "PATH"
                                                           columna.Caption = "path"
                                                           columna.HeaderStyle.CssClass = "hidden"
                                                           columna.CellStyle.CssClass = "hidden"
                                                           columna.FilterCellStyle.CssClass = "hidden"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "Comentarios_Expediente"
                                                           columna.Caption = "Comentarios_Expediente"
                                                           columna.HeaderStyle.CssClass = "hidden"
                                                           columna.CellStyle.CssClass = "hidden"
                                                           columna.FilterCellStyle.CssClass = "hidden"
                                                       End Sub)
                                   setting.Columns.Add(Sub(column)
                                                           column.Caption = "Seleccionar"
                                                           'column.FieldName = "Seleccionar"
                                                           column.VisibleIndex = 0
                                                           column.CellStyle.Paddings.PaddingBottom = 20
                                                           column.CellStyle.Paddings.PaddingLeft = 10
                                                           column.CellStyle.Paddings.PaddingRight = 5
                                                           column.SetDataItemTemplateContent(Sub(Content)
                                                                                                 Html.DevExpress().HyperLink(Sub(Hsetting)
                                                                                                                                 Hsetting.Properties.Text = "Seleccionar"
                                                                                                                                 Hsetting.ControlStyle.Font.Italic = True
                                                                                                                             End Sub).GetHtml()
                                                                                             End Sub)
                                                       End Sub)
                                   setting.HtmlDataCellPrepared = (Sub(sender, e)
                                                                       If e.DataColumn.Caption = "Seleccionar" Then
                                                                           
                                                                           Dim var1 As Integer = e.GetValue("per_hogar")
                                                                           Dim var2 As String = e.GetValue("PATH")
                                                                           Dim var3 As String = e.GetValue("Comentarios_Expediente")
                                                                           e.Cell.Attributes.Add("onclick", String.Format("Fnc_BuscarActualizacionSolicitud('{0}','{1}','{2}', 0,'{3}')", e.GetValue("per_hogar").ToString(), e.GetValue("per_hogar").ToString(), e.GetValue("PATH").ToString(), e.GetValue("Comentarios_Expediente").ToString().Replace(Environment.NewLine, "")))
                                                                       End If
                                                                   End Sub)
                               End Sub).Bind(Model).GetHtml() 
%>
