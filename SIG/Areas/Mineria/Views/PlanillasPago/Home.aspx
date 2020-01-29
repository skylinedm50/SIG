<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Planillas de Pago
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ButtonNavBar" runat="server">
    <a class="navbar-brand" href="/Mineria/Home"><i class="fa fa-arrow-left fa-2x" aria-hidden="true"></i></a>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container text-center">
        <div class="row">
            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto"    ">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29); height:100%;">
                    <div class="pt-4 pb-3" style="letter-spacing: 2px">
                        <h4>Arrastre, Altas y Bajas Entre Dos Pagos</h4>
                    </div>
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/PlanillasPago/v_ComparacionPlanillas'">Acceder</button>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto" >
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29); height:100%;">
                    <div class="pt-4 pb-3" style="letter-spacing: 2px">
                        <h4>Base Pagado</h4>
                    </div>
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/PlanillasPago/v_BasePagado'">Acceder</button>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                    <div class="pt-4 pb-3" style="letter-spacing: 2px">
                        <h4>Cantidad de Fichas Por Censo y Año</h4>
                    </div>
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/PlanillasPago/v_CantidadFichasPorCensoAno'">Acceder</button>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                    <div class="pt-4 pb-3" style="letter-spacing: 2px">
                        <h4>Cantidad de Hogares Pagados</h4>
                    </div>
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/PlanillasPago/v_HogaresPagadosPeriodoTiempo'">Acceder</button>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                    <div class="pt-4 pb-3" style="letter-spacing: 2px">
                        <h4>Global Niños pagados</h4>
                    </div>
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/PlanillasPago/v_NinosPagadosPorCiclo?variante=1'">Acceder</button>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                    <div class="pt-4 pb-3" style="letter-spacing: 2px">
                        <h4>Niños pagados por ciclo</h4>
                    </div>
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/PlanillasPago/v_NinosPagadosPorCiclo?variante=2'">Acceder</button>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                    <div class="pt-4 pb-3" style="letter-spacing: 2px">
                        <h4>Cumplimiento de niños</h4>
                    </div>
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/PlanillasPago/v_NinosPagadosPorCiclo?variante=3'">Acceder</button>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                    <div class="pt-4 pb-3" style="letter-spacing: 2px">
                        <h4>Niños pagados desagregados</h4>
                    </div>
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/PlanillasPago/v_NinosPagadosPorCiclo?variante=4'">Acceder</button>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                    <div class="pt-4 pb-3" style="letter-spacing: 2px">
                        <h4>Consolidado de Pago</h4>
                    </div>
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/PlanillasPago/v_ConsolidadoPago'">Acceder</button>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                    <div class="pt-4 pb-3" style="letter-spacing: 2px">
                        <h4>Elegibles Contra Programados</h4>
                    </div>
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/PlanillasPago/v_ElegiblesContraProgramados'">Acceder</button>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                    <div class="pt-4 pb-3" style="letter-spacing: 2px">
                        <h4>Estado Cuenta de Cumplimiento, Apercibimiento e Incumplimiento</h4>
                    </div>
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/PlanillasPago/v_ConsultaECCAI'">Acceder</button>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                    <div class="pt-4 pb-3" style="letter-spacing: 2px">
                        <h4>Listado de Niños en Pago</h4>
                    </div>
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/PlanillasPago/v_ListadoNinosPago'">Acceder</button>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                    <div class="pt-4 pb-3" style="letter-spacing: 2px">
                        <h4>Listado de Titulares</h4>
                    </div>
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/PlanillasPago/v_TitularesPago'">Acceder</button>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                    <div class="pt-4 pb-3" style="letter-spacing: 2px">
                        <h4>Nucleo Familiar</h4>
                    </div>
                    <div class="pb-4">
                         <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/PlanillasPago/v_NucleoFamiliarMuestra'">Acceder</button>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                    <div class="pt-4 pb-3" style="letter-spacing: 2px">
                        <h4>Razones de Exclusión de Hogares</h4>
                    </div>
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/PlanillasPago/v_RazonesExclusionHogares'">Acceder</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
