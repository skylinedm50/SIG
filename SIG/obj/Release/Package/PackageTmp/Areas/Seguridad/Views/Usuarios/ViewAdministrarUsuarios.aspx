<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Seguridad/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Administrar Usuarios
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.TreeList},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.Editors},
        New Script With {.ExtensionSuite = ExtensionSuite.TreeList},
        New Script With {.ExtensionSuite = ExtensionSuite.GridView})%>

    <script type="text/javascript" src='/Areas/Seguridad/Scripts/usuarios.js'></script>

    <h2>Administrar Usuarios</h2>
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
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().TextBox(
                                               Sub(txt)
                                                   txt.Name = "txtIdentidad"
                                                   txt.Properties.NullText = "Ingrese Número de Identidad"
                                                   txt.Properties.ClientSideEvents.TextChanged = "txtIdentidadChange"
                                                   txt.Properties.MaxLength = 13
                                               End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                           group.Items.Add(
                               Sub(item)
                                   'item.ShowCaption = DefaultBoolean.False
                                   item.Caption = "Habilitado"
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().CheckBox(
                                               Sub(chk)
                                                   chk.Name = "chkEstado"
                                               End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Primer Nombre"
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().TextBox(
                                               Sub(txt)
                                                   txt.Name = "txtNombre1"
                                                   txt.Properties.NullText = "Primer Nombre"
                                               End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Segundo Nombre"
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().TextBox(
                                               Sub(txt)
                                                   txt.Name = "txtNombre2"
                                                   txt.Properties.NullText = "Segundo Nombre"
                                               End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Primier Apellido"
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().TextBox(
                                               Sub(txt)
                                                   txt.Name = "txtApellido1"
                                                   txt.Properties.NullText = "Primer Apellido"
                                               End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Segundo Apellido"
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().TextBox(
                                               Sub(txt)
                                                   txt.Name = "txtApellido2"
                                                   txt.Properties.NullText = "Segundo Apellido"
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
                                                   txt.Properties.NullText = "Número de Teléfono"
                                                   txt.Properties.MaxLength = 8
                                                   
                                               End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Correo"
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().TextBox(
                                               Sub(txt)
                                                   txt.Name = "txtCorreo"
                                                   txt.Properties.NullText = "Correo Electrónico"
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
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().TextBox(
                                               Sub(txt)
                                                   txt.Name = "txtUsuario"
                                                   txt.Properties.NullText = "Nombre Usuario"
                                               End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Clave"
                                   item.SetNestedContent(
                                       Sub()
                                       
                                           Html.DevExpress().TextBox(
                                               Sub(txt)
                                                   txt.Name = "txtClave"
                                                   txt.Properties.Password = True
                                                   txt.Properties.NullText = "Clave Usuario"
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
                                                    spe.Number = 0
                                                    spe.Properties.MaxValue = 100
                                                    spe.Properties.MinValue = 0
                                                    spe.Width = 40
                                                End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                           group.Items.Add(
                               Sub(item)
                                   item.Caption = "Clave"
                                   item.HelpText = "Clave definida por el usuario."
                                   item.SetNestedContent(
                                       Sub()
                                           Html.DevExpress().CheckBox(
                                               Sub(chk)
                                                   chk.Name = "chkClave"
                                               End Sub).GetHtml()
                                       End Sub)
                               End Sub)
                       End Sub)
                   frmLayout.Items.AddGroupItem(
                       Sub(group)
                           group.Caption = "Permisos"
                           group.ColCount = 1
                           group.Items.Add(
                               Sub(item)
                                   item.ShowCaption = DefaultBoolean.False
                                   item.SetNestedContent(
                                       Sub()
                                           Html.RenderAction("PartialViewGridPermisos")
                                       End Sub)
                               End Sub)
                       End Sub)
               End Sub).GetHtml()%>
    </div>
    <div align="center">
        <% Html.DevExpress().Button(
               Sub(btn)
                   btn.Name = "btnActualizar"
                   btn.Text = "Actualizar Usuario"
                   btn.ClientEnabled = False
                   btn.ClientSideEvents.Click = "btnActualizarClick"
                  
               End Sub).GetHtml()%>
        &nbsp&nbsp&nbsp
        <% Html.DevExpress().Button(
               Sub(btn)
                   btn.Name = "btnGuardar"
                   btn.Text = "Guardar Nuevo Usuarios"
                   btn.ClientEnabled = False
                   btn.ClientSideEvents.Click = "btnGuardarClick"
               End Sub).GetHtml()%>
    </div>
    <% Html.DevExpress.TextBox(
           Sub(txt)
               txt.Name = "txtCodPersona"
               txt.ClientVisible = False
           End Sub).GetHtml()%>
</asp:Content>
