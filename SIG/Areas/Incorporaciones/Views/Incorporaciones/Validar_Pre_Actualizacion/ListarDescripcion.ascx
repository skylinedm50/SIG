<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Drawing" %>

<%   

    Html.DevExpress().GridView(Sub(setting)
                                   setting.Name = "GridLista"
                                   setting.Caption = "Descripciones Previas"
                                   setting.CallbackRouteValues = New With {Key .Controller = "Incorporaciones", Key .Action = "DescripcionesPrevias", Key .hog_hogar = ViewData("hogar")}
                                   setting.Theme = "DevEx"
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
                                   setting.CommandColumn.ShowNewButtonInHeader = True
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "DESCRIPCION_ACTUALIZACION"
                                                           columna.Caption = "Descripción"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "FCH_ACTUALIZACION"
                                                           columna.Caption = "Fecha de Actualizacion"
                                                       End Sub)
                                   setting.Columns.Add(Sub(columna)
                                                           columna.FieldName = "USUARIO"
                                                           columna.Caption = "Usuario"
                                                       End Sub)

                               End Sub).Bind(Model).GetHtml()
%>
