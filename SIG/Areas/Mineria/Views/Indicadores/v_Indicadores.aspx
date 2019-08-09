<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Indicadores Anuales
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <%--<script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/planillasPago.js")%>'></script>--%>

    <h2>Indicadores</h2>
    <div>
        <p>
            1. Porcentaje de hogares beneficiarios de TMC en zonas rurales que se encuentran en situación de pobreza extrema.
        </p>
        <br />
        <% Html.DevExpress().GridView(
               Sub(grid)
                   grid.Name = "grid1"
                   grid.Caption = "Porcentaje de hogares beneficiarios en situación de pobreza extrema."
                   grid.Enabled = False
                   grid.Width = 550
                   grid.Styles.Header.HorizontalAlign = HorizontalAlign.Center
               End Sub).Bind(ViewData("hog_pob_extrema")).GetHtml()%>
    </div>
    <br />
    <div>
        <p>
            2. Porcentaje de estudiantes del 1ero a 6to grado incluidos en el programa que cumplen la corresponsabilidad del 80% de asistencia a la escuela.
        </p>
        <br />
        <% Html.DevExpress().GridView(
               Sub(grid)
                   grid.Name = "grid2"
                   grid.Caption = "Porcentaje de estudiantes de 1ro a 6to cumpliendo asistencia"
                   grid.Enabled = False
                   grid.Width = 550
                   grid.Styles.Header.HorizontalAlign = HorizontalAlign.Center
               End Sub).Bind(ViewData("niños_1_6_asistiendo")).GetHtml()%>
    </div>
    <br />
    <div>
        <p>
            3. Porcentaje de estudiantes del 7mo a 9no grado incluidos en el programa que cumplen la corresponsabilidad del 80% de asistencia a la escuela.
        </p>
        <br />
        <% Html.DevExpress().GridView(
               Sub(grid)
                   grid.Name = "grid3"
                   grid.Caption = "Porcentaje de estudiantes de 7ro a 9to cumpliendo asistencia"
                   grid.Enabled = False
                   grid.Width = 550
                   grid.Styles.Header.HorizontalAlign = HorizontalAlign.Center
               End Sub).Bind(ViewData("niños_7_9_asistiendo")).GetHtml()%>
    </div>
    <br />
    <div>
        <p>
            4. Porcentaje de niños beneficiarios del programa de 13 a 15 años que completaron primaria, sexto grado.
        </p>
        <br />
        <% Html.DevExpress().GridView(
               Sub(grid)
                   grid.Name = "grid4"
                   grid.Caption = "Porcentaje de beneficiarios de 13 a 15 años que aprovaron 6to grado"
                   grid.Enabled = False
                   grid.Width = 550
                   grid.Styles.Header.HorizontalAlign = HorizontalAlign.Center
               End Sub).Bind(ViewData("niños_13_15_aprovaron_primaria")).GetHtml()%>
    </div>
    <br />
    <div>
        <p>
            5. Porcentaje de niños beneficiarios del programa de 16 a 18 años que aprovaron el noveno grado.
        </p>
        <br />
        <% Html.DevExpress().GridView(
               Sub(grid)
                   grid.Name = "grid5"
                   grid.Caption = "Porcentaje de beneficiarios de 16 a 18 años que aprovaron 9no grado"
                   grid.Enabled = False
                   grid.Width = 550
                   grid.Styles.Header.HorizontalAlign = HorizontalAlign.Center
               End Sub).Bind(ViewData("niños_16_18_aprovaron_noveno")).GetHtml()%>
    </div>
    <br />
    <div>
        <p>
            6. Porcentaje de niños beneficiarios del programa matriculados en sexto grado que aprobaron.
        </p>
        <br />
        <% Html.DevExpress().GridView(
               Sub(grid)
                   grid.Name = "grid6"
                   grid.Caption = "Porcentaje de beneficiarios matriculados que aprovaron 6to"
                   grid.Enabled = False
                   grid.Width = 550
                   grid.Styles.Header.HorizontalAlign = HorizontalAlign.Center
               End Sub).Bind(ViewData("matriculado_6to_aprovaron")).GetHtml()%>
    </div>
    <br />
    <div>
        <p>
            7. Porcentaje de niños beneficiarios del programa matriculados en noveno grado que aprobaron.
        </p>
        <br />
        <% Html.DevExpress().GridView(
               Sub(grid)
                   grid.Name = "grid7"
                   grid.Caption = "Porcentaje de beneficiarios matriculados que aprovaron 9no"
                   grid.Enabled = False
                   grid.Width = 550
                   grid.Styles.Header.HorizontalAlign = HorizontalAlign.Center
               End Sub).Bind(ViewData("matriculado_9no_aprovaron")).GetHtml()%>
    </div>
    <br />
    <div>
        <p>
            8. Porcentaje de hogares registrados en el programa con información de elegibilidad actualizada
        </p>
        <br />
        <% Html.DevExpress().GridView(
               Sub(grid)
                   grid.Name = "grid8"
                   grid.Caption = "Porcentaje de hogares con elegibilidad actualizada"
                   grid.Enabled = False
                   grid.Width = 550
                   grid.Styles.Header.HorizontalAlign = HorizontalAlign.Center
               End Sub).Bind(ViewData("hogares_elegibilidad_actulizada")).GetHtml()%>
    </div>
    <br />
    <div>
        <p>
            9. Número de hogares que reciben todos los pagos del año en virtud del Programa.
        </p>
        <br />
        <% Html.DevExpress().GridView(
               Sub(grid)
                   grid.Name = "grid9"
                   grid.Caption = "Número de hogares que recibe todos los pagos del Programa"
                   grid.Enabled = False
                   grid.Width = 550
                   grid.Styles.Header.HorizontalAlign = HorizontalAlign.Center
               End Sub).Bind(ViewData("hogares_reciben_todos_pagos")).GetHtml()%>
    </div>
    <br />
    <div>
        <p>
            10. Número de hogares que reciben todos los pagos del año en virtud del Proyecto.
        </p>
        <br />
        <% Html.DevExpress().GridView(
               Sub(grid)
                   grid.Name = "grid10"
                   grid.Caption = "Número de hogares que recibe todos los pagos del Proyecto"
                   grid.Enabled = False
                   grid.Width = 550
                   grid.Styles.Header.HorizontalAlign = HorizontalAlign.Center
               End Sub).Bind(ViewData("hogares_reciben_todos_pagos_bm")).GetHtml()%>
    </div>
    <br />
    <div>
        <p>
            11. Porcentaje de unidades de salud que informan del cumplimiento, de conformidad con la verificación del calendario de pagos.
        </p>
        <br />
        <% Html.DevExpress().GridView(
               Sub(grid)
                   grid.Name = "grid11"
                   grid.Caption = "Porcentaje de centros de salud que reporan cumplimiento"
                   grid.Enabled = False
                   grid.Width = 550
                   grid.Styles.Header.HorizontalAlign = HorizontalAlign.Center
               End Sub).Bind(ViewData("centros_salud_reportan_cumplimiento")).GetHtml()%>
    </div>
    <br />
    <div>
        <p>
            12. porcentaje hogaares pagados con mecanismos alternativos de pago del programa.
        </p>
        <br />
        <% Html.DevExpress().GridView(
               Sub(grid)
                   grid.Name = "grid12"
                   grid.Caption = "Porcentaje de hogares pagados con mecanismos alternos al programa"
                   grid.Enabled = False
                   grid.Width = 550
                   grid.Styles.Header.HorizontalAlign = HorizontalAlign.Center
               End Sub).Bind(ViewData("hogares_pagados_mecanismos_alternos")).GetHtml()%>
    </div>
    <br />
    <div>
        <p>
            13. porcentaje hogaares pagados con mecanismos alternativos de pago del proyecto.
        </p>
        <br />
        <% Html.DevExpress().GridView(
               Sub(grid)
                   grid.Name = "grid13"
                   grid.Caption = "Porcentaje de hogares pagados con mecanismos alternos al proyecto"
                   grid.Enabled = False
                   grid.Width = 550
                   grid.Styles.Header.HorizontalAlign = HorizontalAlign.Center
               End Sub).Bind(ViewData("hogares_pagados_mecanismos_alternos_bm")).GetHtml()%>
    </div>

</asp:Content>
