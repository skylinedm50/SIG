<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Seguridad/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
ViewCambioContraseña
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <script type="text/javascript" src='/Areas/Seguridad/Scripts/cambioContraseña.js'></script>

    <h2>Cambio de Contraseña</h2>
    <div>
        <% Html.DevExpress().FormLayout(
               Sub(frmLayout)
                   frmLayout.Name = "frmLaout"
                   frmLayout.Items.AddGroupItem(
                       Sub(group)
                           group.ColCount = 2
                           group.Caption = "Datos Personales"
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Nro. Identidad"
                                   'item.ColSpan = 2
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().TextBox(
                                               Sub(txt)
                                                   txt.Name = "txtIdentidad"
                                                   'txt.Properties.NullText = "Ingrese Número de Identidad"
                                                   'txt.Properties.ClientSideEvents.TextChanged = "txtIdentidadChange"
                                                   txt.Properties.MaxLength = 13
                                                   txt.Enabled = False
                                                   txt.Text = ViewData("identidad")
                                               End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Teléfono"
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().TextBox(
                                               Sub(txt)
                                                   txt.Name = "txtTelefono"
                                                   'txt.Properties.NullText = "Número de Teléfono"
                                                   txt.Properties.MaxLength = 8
                                                   txt.Enabled = False
                                                   txt.Text = ViewData("telefono")
                                               End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Primer Nombre"
                                   item.ColSpan = 2
                                   
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().TextBox(
                                               Sub(txt)
                                                   txt.Name = "txtNombre"
                                                   'txt.Properties.NullText = "Primer Nombre"
                                                   txt.Enabled = False
                                                   txt.Text = ViewData("nombre")
                                                   txt.Width = 250
                                               End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                           
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Correo"
                                   item.ColSpan = 2
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().TextBox(
                                               Sub(txt)
                                                   txt.Name = "txtCorreo"
                                                   'txt.Properties.NullText = "Correo Electrónico"
                                                   txt.Enabled = False
                                                   txt.Text = ViewData("correo")
                                                   txt.Width = 250
                                               End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                       End Sub)
                   frmLayout.Items.AddGroupItem(
                       Sub(group)
                           group.Caption = "Datos de Usuario"
                           group.ColCount = 2
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Usuario"
                                   item.VerticalAlign = FormLayoutVerticalAlign.Middle
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().TextBox(
                                               Sub(txt)
                                                   txt.Name = "txtUsuario"
                                                   'txt.Properties.NullText = "Nombre Usuario"
                                                   txt.Enabled = False
                                                   txt.Text = ViewData("usuario")
                                               End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                           
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Nro Critico"
                                   item.HelpText = "Módulo de Levantamiento e Incorporaciones"
                                   'item.HelpTextSettings.Position = HelpTextPosition.Right
                               
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().SpinEdit(
                                                Sub(spe)
                                                    spe.Name = "txtNroCritico"
                                                    'spe.Number = 0
                                                    spe.Properties.MaxValue = 100
                                                    spe.Properties.MinValue = 0
                                                    'spe.Width = 40
                                                    spe.Enabled = False
                                                    spe.Number = ViewData("critico")
                                                End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Contraseña"
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().TextBox(
                                               Sub(txt)
                                                   txt.Name = "txtClave"
                                                   txt.Properties.Password = True
                                                   txt.Properties.ClientSideEvents.TextChanged = "txtClaveChange"
                                                   'txt.Properties.NullText = "Clave Usuario"
                                               End Sub).GetHtml()
                                           Html.DevExpress().Label(
                                               Sub(lbl)
                                                   lbl.Name = "lblMensaje"
                                                   'lbl.Text = "Contraseña equivocada."
                                                   lbl.ClientVisible = False
                                                   'lbl.Attributes.Item.
                                               End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Confirmar Contraseña"
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().TextBox(
                                               Sub(txt)
                                                   txt.Name = "txtConfirmacionClave"
                                                   txt.Properties.ClientSideEvents.TextChanged = "txtConfirmacionClaveChange"
                                                   txt.Properties.Password = True
                                                   'txt.Properties.NullText = "Confirme Clave"
                                               End Sub).GetHtml()
                                           Html.DevExpress().Label(
                                               Sub(lbl)
                                                   lbl.Name = "lblMensaje2"
                                                   lbl.Text = "Contraseña equivocada."
                                                   lbl.ClientVisible = False
                                                   'lbl.Attributes.Item.
                                               End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                       End Sub)
               End Sub).GetHtml()%>
    </div>
    <br />
    <div align="center" style="width: 700px">
        <% Html.DevExpress().Button(
               Sub(btn)
                   btn.Name = "btnActualizar"
                   btn.Text = "Actualizar Contraseña"
                   btn.ClientEnabled = False
                   btn.ClientSideEvents.Click = "btnActualizarClick"
                  
               End Sub).GetHtml()%>
        &nbsp&nbsp&nbsp
        <% Html.DevExpress().Button(
               Sub(btn)
                   btn.Name = "btnCancelar"
                   btn.Text = "Cancelar"
                   
                   btn.ClientSideEvents.Click = "btnCancelarClick"
               End Sub).GetHtml()%>
    </div>

</asp:Content>
