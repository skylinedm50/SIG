<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Hogares Ficha
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ButtonNavBar" runat="server">
    <a class="navbar-brand" href="/Mineria/Home"><i class="fa fa-arrow-left fa-2x" aria-hidden="true"></i></a>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="container text-center">
    <div class="row">
        <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
            <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                <div class="pt-4 pb-3" style="letter-spacing: 2px">
                    <h4>Cantidad de Fichas no Procesadas</h4>
                </div>
                <div class="pb-4">
                    <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/Hogares/v_FichasNoRemitidas'">Acceder</button>
                </div>
            </div>
        </div>

        <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
            <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                <div class="pt-4 pb-3" style="letter-spacing: 2px">
                    <h4>Cantidad de Fichas por Censo y Año</h4>
                </div>
                <div class="pb-4">
                    <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/Hogares/v_FichasPorCensoAno'">Acceder</button>
                </div>
            </div>
        </div>

        <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
            <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                <div class="pt-4 pb-3" style="letter-spacing: 2px">
                    <h4>Historial de Pagos a Hogar</h4>
                </div>
                <div class="pb-4">
                    <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/Hogares/v_HistorialPagos'">Acceder</button>
                </div>
            </div>
        </div>

        <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
            <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                <div class="pt-4 pb-3" style="letter-spacing: 2px">
                    <h4>Resultado de Fichas</h4>
                </div>
                <div class="pb-4">
                    <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href='/Mineria/Hogares/v_ResultadoFichas'">Acceder</button>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
