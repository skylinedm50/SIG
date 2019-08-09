Imports DevExpress.Web

'Namespace SIG.Corresponsabilidad



Public Class Principal
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim arrNomItemConfi() As String = {"WebServices", "Agregar reportes", "Levantamientos de citas medicas"}

        'ASPxMenuPrinNavega.Items.Add("Inicio")
        'ASPxMenuPrinNavega.Items.Add("Reportes")
        'ASPxMenuPrinNavega.Items.Add("Configuración")
        'ASPxMenuPrinNavega.Items.Add("Manual de uso")

        'For Each strNomItem As String In arrNomItemConfi
        '    ASPxMenuPrinNavega.Items(2).Items.Add(strNomItem)
        'Next
        'Dim objNavBarItem As NavBarItem
        'Dim objNavBarGrupo As NavBarGroup

        'objNavBarGrupo = New NavBarGroup("Menu Principal")
        '' objNavBarItem = New NavBarItem("Página de inicio", "itemPagIni", "", "/Home/Index")
        ''  objNavBarGrupo.Items.Add(objNavBarItem)
        'ASPxNavBarPrincipal.Groups.Add(objNavBarGrupo)

        'objNavBarGrupo = New NavBarGroup("Reportes")
        'objNavBarItem = New NavBarItem("Sección de reportes", "itemRepor", "", "/Reportes/Index")
        'objNavBarGrupo.Items.Add(objNavBarItem)
        'ASPxNavBarPrincipal.Groups.Add(objNavBarGrupo)

        'objNavBarGrupo = New NavBarGroup("Configuración")
        'objNavBarItem = New NavBarItem("WebService", "itemWebServ", "", "")
        'objNavBarGrupo.Items.Add(objNavBarItem)
        'objNavBarItem = New NavBarItem("Corresponsabilidad", "itemCorresponsabilidad", "", "/Configuracion/Corresponsabilidad")
        'objNavBarGrupo.Items.Add(objNavBarItem)
        'objNavBarItem = New NavBarItem("Ciclos", "itemCiclos", "", "/Configuracion/Ciclos")
        'objNavBarGrupo.Items.Add(objNavBarItem)
        'objNavBarItem = New NavBarItem("Agregar reporte", "itemAgreReport", "", "/Configuracion/AgregarReporte")
        'objNavBarGrupo.Items.Add(objNavBarItem)
        'objNavBarItem = New NavBarItem("Levantamineto de citas medicas", "itemLevaCitMed", "", "")
        'objNavBarGrupo.Items.Add(objNavBarItem)
        'ASPxNavBarPrincipal.Groups.Add(objNavBarGrupo)

        'objNavBarGrupo = New NavBarGroup("Manual de uso")
        'objNavBarItem = New NavBarItem("Uso del módulo", "itemUsoMod", "", "")
        'objNavBarGrupo.Items.Add(objNavBarItem)
        'ASPxNavBarPrincipal.Groups.Add(objNavBarGrupo)


    End Sub

End Class

'End Namespace