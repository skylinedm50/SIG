<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html().DevExpress().FormLayout(
       Sub(frmLayoutListaPagadores)
           frmLayoutListaPagadores.Name = "frmLayoutListaPagadores"
           frmLayoutListaPagadores.Items.AddGroupItem(
               Sub(groupListaP)
                   groupListaP.Caption = "Entidades Pagadoras"
                   groupListaP.Items.Add(
                       Sub(itemListaP)
                           itemListaP.ShowCaption = DefaultBoolean.False
                           itemListaP.SetNestedContent(
                               Sub()
                                   Html.DevExpress().RadioButtonList(
                                       Sub(rblPagador)
                                           rblPagador.Name = "rblPagadores"
                                           rblPagador.Properties.RepeatLayout = RepeatLayout.Table
                                           rblPagador.Properties.RepeatDirection = RepeatDirection.Vertical
                                           rblPagador.ControlStyle.Border.BorderWidth = 0
                                           rblPagador.Properties.ValueField = "cod_pagador"
                                           rblPagador.Properties.TextField = "nombre_pagador"
                                           rblPagador.SelectedIndex = 0
                                       End Sub).BindList(Model).GetHtml()
                               End Sub)
                       End Sub)
               End Sub)
       End Sub).Bind(Model).GetHtml()
    %>