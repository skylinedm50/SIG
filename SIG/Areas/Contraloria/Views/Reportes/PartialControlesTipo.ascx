<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<fieldset>
    <legend> Tipo de Consolidado </legend>
    <% Html.DevExpress().RadioButtonList(
        Sub(rb)
            rb.Name = "rbTipo"
            rb.Properties.RepeatLayout = RepeatLayout.Table
            rb.Properties.RepeatColumns = 1
            rb.Properties.RepeatDirection = RepeatDirection.Horizontal
            rb.ControlStyle.Border.BorderWidth = 0
            rb.Properties.Items.Add("Fondo", "fondo").Selected = True
            rb.Properties.Items.Add("Área Geográfica", "areaGeografica")
            rb.Properties.Items.Add("Banco", "banco")
            rb.Properties.ValueType = GetType(String)
            rb.Properties.ClientSideEvents.ValueChanged = "rbTipovalueChanged"
        End Sub).GetHtml()%>
</fieldset>