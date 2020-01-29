<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<%Html.DevExpress().ComboBox(Sub(cb)
                                   cb.Name = "cbJefeInmediato"
                                   cb.ClientVisible = True
                                   cb.Properties.Items.Add("asd", "No")
                                   cb.CallbackRouteValues = New With {Key .Controller = "Usuarios", Key .Action = "PartialViewCbJefeInmediato"}
                                   cb.Properties.IncrementalFilteringMode = IncrementalFilteringMode.None
                                   cb.SelectedIndex = 0
                                   cb.Properties.DropDownStyle = DropDownStyle.DropDownList
                                   cb.Properties.ValueField = "cod_usuario"
                                   cb.Properties.TextField = "nombre"
                               End Sub).BindList(Model).GetHtml()%>