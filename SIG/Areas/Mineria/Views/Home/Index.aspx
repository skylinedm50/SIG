<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Mineria de Datos
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ButtonNavBar" runat="server">
    <a class="navbar-brand" href="/Home/MenuPrincipal"><i class="fa fa-home fa-2x" aria-hidden="true"></i></a>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container text-center">
        <div class="row">
            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                    <div class="pt-4 pb-3">
                        <i class="fa fa-file-text-o fa-3x" aria-hidden="true"></i>
                    </div>
                    <div class="pt-4 pb-3" style="letter-spacing: 0px">
                        <h4>Corresponsabilidad</h4>
                    </div>
              
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href = '/Mineria/Corresponsabilidad/Home'">Acceder</button>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                    <div class="pt-4 pb-3">
                        <i class="fa fa-file-text-o fa-3x" aria-hidden="true"></i>
                    </div>
                    <div class="pt-4 pb-3" style="letter-spacing: 0px">
                        <h4>Hogares y Ficha</h4>
                    </div>
              
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href = '/Mineria/Hogares/Home'">Acceder</button>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                    <div class="pt-4 pb-3">
                        <i class="fa fa-file-text-o fa-3x" aria-hidden="true"></i>
                    </div>
                    <div class="pt-4 pb-3" style="letter-spacing: 0px">
                        <h4>Planillas de Pago</h4>
                    </div>
              
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href = '/Mineria/PlanillasPago/Home'">Acceder</button>
                    </div>
                </div>
            </div>

            <div class="col-lg-4 col-md-6 col-sm-10 pb-4 d-block m-auto">
                <div style="box-shadow: 0px 0px 30px -7px rgba(0,0,0,0.29);">
                    <div class="pt-4 pb-3">
                        <i class="fa fa-file-text-o fa-3x" aria-hidden="true"></i>
                    </div>
                    <div class="pt-4 pb-3" style="letter-spacing: 0px">
<<<<<<< HEAD
                        <h4>Listados</h4>
=======
                        <h4>Proyecciones</h4>
>>>>>>> 951c825fa1e75313dcfacc2e2df6d3d158a54c40
                    </div>
              
                    <div class="pb-4">
                        <button type="button" class="btn btn-outline-primary bt-sm" onclick="location.href = '/Mineria/Proyecciones/Home'">Acceder</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
