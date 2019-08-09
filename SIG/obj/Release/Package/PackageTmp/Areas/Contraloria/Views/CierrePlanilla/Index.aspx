<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Contraloria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Cierre de Planilla
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>

    <link type="text/css" rel="stylesheet" href="/Areas/Contraloria/Style/styles_contraloria.css" />

    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/cierrePlanilla.js")%>'></script>
    <h2>Cierre de Planilla de Pago</h2>
    <%--<style>
        .dropdown {
            display: inline-block;
            vertical-align: middle;
            margin-right: 10px;
        }

        .buttonCierreParcial {
            margin: auto 15px;
        }

        .detalleAldea {
            margin: 0 auto;
        }
    </style>--%>
    <br />
    <div>
        <% Html.DevExpress().DropDownEdit(
           Sub(settings)
               settings.Name = "checkComboBox"
               settings.Width = 210
               settings.ControlStyle.CssClass = "dropdown"
               settings.Properties.DropDownWindowStyle.BackColor = System.Drawing.Color.FromArgb(241, 241, 241)
               settings.Properties.DropDownWindowStyle.HorizontalAlign = HorizontalAlign.Center
               settings.Properties.DropDownWindowStyle.Border.BorderStyle = BorderStyle.Solid
               settings.Properties.DropDownWindowStyle.Border.BorderColor = System.Drawing.Color.Black
               
               settings.SetDropDownWindowTemplateContent(
                   Sub(ddwt)
                       Html.DevExpress().ListBox(
                           Sub(lb)
                               lb.Name = "checkListBox"
                               lb.ControlStyle.Border.BorderWidth = 0
                               lb.ControlStyle.BorderBottom.BorderWidth = 1
                               lb.Width = Unit.Percentage(100)
                               
                               lb.Properties.SelectionMode = ListEditSelectionMode.CheckColumn
                               lb.Properties.ValueField = "cod_departamento"
                               lb.Properties.TextField = "desc_departamento"
                               
                               lb.Properties.ClientSideEvents.SelectedIndexChanged = "OnListBoxSelectionChanged"
                           End Sub).BindList(ViewData("dptos")).GetHtml()
                       ViewContext.Writer.Write("<div style=" + Chr(34) + "padding: 6px; height: 24px;" + Chr(34) + ">")
                       Html.DevExpress().Button(
                           Sub(btn)
                               btn.Name = "btnClose"
                               btn.Text = "Cerrar"
                               btn.Style.Add("floar", "right")
                               btn.Style.Add("padding", "0px 2px")
                               btn.ClientSideEvents.Click = "function(s, e){ checkComboBox.HideDropDown(); }"
                               btn.Height = 26
                           End Sub).Render()
                       ViewContext.Writer.Write("</div>")
                   End Sub)
               settings.Properties.AnimationType = AnimationType.Slide
               settings.Properties.ClientSideEvents.TextChanged = "SynchronizeListBoxValues"
               settings.Properties.ClientSideEvents.DropDown = "SynchronizeListBoxValues"
           End Sub).GetHtml()%>
        <% Html.DevExpress().Button(
           Sub(settings)
               settings.Name = "btnBuscar"
               settings.Text = "Buscar"
               settings.ClientSideEvents.Click = "function(s, e){ GetPlanillasAbiertas(); }"
           End Sub).GetHtml()%>
    </div>    
    <br />
    <div id="divGridView">
        <% Html.RenderPartial("PartialGridViewPlanillas")%>
    </div>
    <% Html.Hidden("selectedIDsHF")%>
    <br />
    <div>
        <% Html.DevExpress().Button(
        Sub(settings)
            settings.Name = "btnSeleccionar"
            settings.Text = "Seleccionar Todo"
            settings.ClientSideEvents.Click = "function(s, e){ GridView.SelectRows(); }"
            'settings.Attributes.Item
            'settings.ControlStyle.CssClass = "buttonCierreParcial"
            settings.ControlStyle.CssClass = "buttonCarga"
        End Sub).GetHtml()%>
        <% Html.DevExpress().Button(
        Sub(settings)
            settings.Name = "btnDesSeleccionar"
            settings.Text = "Deseleccionar Todo"
            settings.ClientSideEvents.Click = "function(s, e){ GridView.UnselectRows(); }"
            'settings.ControlStyle.CssClass = "buttonCierreParcial"
            settings.ControlStyle.CssClass = "buttonCarga"
        End Sub).GetHtml()%>
        <% Html.DevExpress().Button(
           Sub(settings)
               settings.Name = "btnCerrar"
               settings.Text = "Cerrar Planilla"
               settings.ClientSideEvents.Click = "OnClick"
               'settings.ControlStyle.CssClass = "buttonCierreParcial"
               settings.ControlStyle.CssClass = "buttonCarga"
           End Sub).GetHtml()%>
    </div>   
    
</asp:Content>
