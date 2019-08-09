<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Contraloria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Recibos Pagados
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<% Html.DevExpress().GetStyleSheets(
                          New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
                          New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
	<% Html.DevExpress().GetScripts(
                          New Script With {.ExtensionSuite = ExtensionSuite.GridView},
                          New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>
	<%--<script type="text/javascript" src="../../Scripts/reportes.js" async"></script>--%>

	<link type="text/css" rel="stylesheet" href="/Areas/Contraloria/Style/styles_contraloria.css" />
	<script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/reportes.js")%>'></script>
	
	<h2>Recibos Pagados</h2>
	<% Html.BeginForm("exportRecibosPagadosToExcel", "Reportes")%>
	<div id="divFiltros">
		<div id="divAreaGeografica" class="col33" style="height:170px;">
			<fieldset class="FildRecibosPagador">
				<legend>Área Geográfica</legend>
				<table>
					<tr>
						<td>
							Departamento:
						</td>
						<td>
							<% Html.RenderAction("PartialDptoView") %>
						</td>
					</tr>
					<tr>
						<td>
							Municipio:
						</td>
						<td>
							<% Html.RenderPartial("PartialMuniView")%>
						</td>
					</tr>
					<tr>
						<td>
							Aldea:
						</td>
						<td>
							<% Html.RenderPartial("PartialAldeaView")%>
						</td>
					</tr>
				</table>
			</fieldset>        
		</div>
		<div id="divInfoBancaria" class="col33">
			<fieldset class="FildRecibosPagador">
				<legend>Información Bancaria</legend>
				<table>
					<tr>
						<td>
							Fondos:
						</td>
						<td>
							<% Html.RenderAction("PartialFondoView")%>
						</td>
					</tr>
					<tr>
						<td>
							Banco:
						</td>
						<td>
							<% Html.RenderAction("PartialBancoView")%>
						</td>
					</tr>
					<tr>
						<td>
							Sucursal:
						</td>
						<td>
							<% Html.RenderPartial("PartialSucursalView")%>
						</td>
					</tr>
				</table>
			</fieldset>
		</div>
		<div id="divFiltro" class="col33">
			<fieldset class="FildRecibosPagador">
				<legend>Filtrar Por</legend>
				<% Html.DevExpress().RadioButtonList(
 Sub(settings)
     settings.Name = "rbtFiltro"
     settings.Properties.RepeatLayout = RepeatLayout.Table
     settings.Properties.RepeatDirection = RepeatDirection.Horizontal
     settings.Properties.RepeatColumns = 2
     settings.ControlStyle.Border.BorderWidth = Unit.Pixel(0)
     settings.Properties.Items.Add("Fecha", "fecha").Selected = True
     settings.Properties.Items.Add("Periodo", "periodo")
     settings.Properties.ClientSideEvents.ValueChanged = "FiltroValueChange"
 End Sub).GetHtml()%>
			</fieldset>
			<div id="divFecha">
				<fieldset class="FildRecibosPagador">
					<legend>Fecha</legend>
					<table>
						<tr>
							<td>
								Fecha Inicio:
							</td>
							<td>
								<% Html.DevExpress().DateEdit(
 Sub(settings)
     settings.Name = "deInicio"
     settings.Properties.UseMaskBehavior = True
     settings.Properties.EditFormat = EditFormat.Custom
     settings.Properties.NullText = "dd/MM/yyyy"
     settings.Properties.EditFormatString = "dd/MM/yyyy"
     settings.Properties.ClientSideEvents.ValueChanged = "function(s, e) { validarFecha('inicio'); }"
 End Sub).GetHtml()%>
							</td>
						</tr>
						<tr>
							<td>
								Fecha Fin:
							</td>
							<td>
								<% Html.DevExpress().DateEdit(
 Sub(settings)
     settings.Name = "deFin"
     settings.Properties.UseMaskBehavior = True
     settings.Properties.EditFormat = EditFormat.Custom
     settings.Properties.NullText = "dd/MM/yyyy"
     settings.Properties.EditFormatString = "dd/MM/yyyy"
     settings.Properties.ClientSideEvents.ValueChanged = "function(s, e) { validarFecha('fin'); }"
 End Sub).GetHtml()%>
							</td>
						</tr>
					</table>            
				<br />        
				</fieldset>
			</div>
			<div id="divPeriodo" hidden>
				<fieldset class="FildRecibosPagador">
					<legend>Periodo</legend>
                    <table>
					    <tr>
						    <td>
                                <label>
						            Periodo:
						            <% Html.RenderAction("PartialPeriodosView")%>
					            </label>
                            </td>
					    </tr>
                        <tr>
						    <td>
                                <label>
						            Esquema:
						            <% Html.RenderPartial("pv_esquemas")%>
					            </label>
                            </td>
					    </tr>
				    </table>
				</fieldset>                
			</div>    
		</div>
	</div>
	<div>
		<div id="divRbtTipo" class="col33">
			<fieldset class="FildRecibosPagador">
				<legend>Tipo de Reporte</legend>
			
			<% Html.DevExpress().RadioButtonList(
             Sub(settings)
                 settings.Name = "rbtTipo"
                 settings.Properties.RepeatLayout = RepeatLayout.Table
                 settings.Properties.RepeatDirection = RepeatDirection.Horizontal
                 settings.Properties.RepeatColumns = 2
                 settings.ControlStyle.Border.BorderWidth = Unit.Pixel(0)
                 settings.Properties.Items.Add("Cantidad", "cantidad").Selected = True
                 settings.Properties.Items.Add("Detalle", "detalle")
                'settings.Properties.ClientSideEvents.SelectedIndexChanged = "rbtTipoChange"
                settings.Properties.ClientSideEvents.SelectedIndexChanged = "function (s, e) { cambiarGrid(); }"
             End Sub).GetHtml()%>
			</fieldset>
		</div>
		<div id="divBotones" class="col66">
			<% Html.DevExpress().Button(
         Sub(settings)
             settings.Name = "btnConsultar"
             settings.Text = "Consultar"
             settings.ClientSideEvents.Click = "consultarClick"
             settings.ControlStyle.CssClass = "buttonRepRecibosPagados"
         End Sub).GetHtml()%>
			&nbsp&nbsp&nbsp&nbsp&nbsp
			<% Html.DevExpress().Button(
         Sub(settings)
             settings.Name = "btnExportar"
             settings.Text = "Exportar"

             settings.ControlStyle.CssClass = "buttonRepRecibosPagados"
            'settings.RouteValues = New With {Key .Controller = "RecibosPagados", Key .Action = "ExportToExcel"}
            'settings.ClientSideEvents.Click = "function(s, e) {  }"
            'settings.ClientSideEvents.Click = "exportarExcelClick"
            settings.UseSubmitBehavior = True
         End Sub).GetHtml()%>
			&nbsp&nbsp&nbsp&nbsp&nbsp
			<% Html.DevExpress().Button(
         Sub(settings)
             settings.Name = "btnReporte"
             settings.Text = "Reporte"
             settings.ClientSideEvents.Click = ""
             settings.ControlStyle.CssClass = "buttonRepRecibosPagados"
             settings.ClientVisible = False
         End Sub).GetHtml()%>
		</div>
		
	</div>
	<br />
	<br />
	<div id="divGridView">
		<% Html.RenderPartial("PartialGridViewRecibosPagados")%>
	</div>
	<% Html.EndForm()%>
</asp:Content>
