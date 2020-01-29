<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
MenuPrincipal
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../../Css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../Css/modulos.css" rel="stylesheet" />
    <script src="../../Scripts/jquery.min.js"></script>

    <input type="button" value="Cerrar Sesión" id="btnLogout" onclick="location.href='<%=  Url.Action("Logout", "Home")%> '" />


<div id="Container">

<header>
<h2>SISTEMA DE INFORMACION GERENCIAL 
<center >
<i class="fa fa-user"></i>
<label id="Sysname" > <%: Session("username") %></label>
<center> 
 </h2> 
</header>


<div id="MenuContainer">
 <%--la informacion del menu--%>
</div>


        <div id="content" align ="center">
              <div class="ContenedorBanner" ></div>

            </div>
       
</div>

    <script>
        $("#MenuContainer").ready(function () {
            //$("#MenuContainer").append($("#cssmenu"))
            document.getElementById("MenuContainer").appendChild(document.getElementById("Menu"))
        });
        if (!window.location.href.match("ref")) {
       
            window.onload = nobackbutton;

            function nobackbutton() {
                window.location.hash = '/?';
                window.location.hash = '/0/?';
                window.onhashchange = function () {
                    window.location.hash = '/?';
                }
            }
}
    </script>


</asp:Content>
