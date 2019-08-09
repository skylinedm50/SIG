<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html().DevExpress().FormLayout(
       Sub(frmLayoutListaFondos)
           frmLayoutListaFondos.Name = "frmLayoutListaFondos"
           frmLayoutListaFondos.Items.AddGroupItem(
               Sub(groupListaF)
                   groupListaF.Caption = "Entidades Pagadoras"
                   groupListaF.Items.Add(
                       Sub(itemListaF)
                           itemListaF.ShowCaption = DefaultBoolean.False
                           itemListaF.SetNestedContent(
                               Sub()
                                   Html.DevExpress().RadioButtonList(
                                       Sub(rblFondos)
                                           rblFondos.Name = "rblFondos"
                                           rblFondos.Properties.RepeatLayout = RepeatLayout.Table
                                           rblFondos.Properties.RepeatDirection = RepeatDirection.Vertical
                                           rblFondos.ControlStyle.Border.BorderWidth = 0
                                           rblFondos.Properties.ValueField = "cod_fondo"
                                           rblFondos.Properties.TextField = "nombre_fondo"
                                           rblFondos.SelectedIndex = 0
                                       End Sub).BindList(Model).GetHtml()
                               End Sub)
                       End Sub)
               End Sub)
       End Sub).Bind(Model).GetHtml()
    %>