<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
MenuPrincipal
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../../Css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../Css/modulos.css" rel="stylesheet" />
    <script src="../../Scripts/jquery.min.js"></script>

    <%--<a href='<%= Url.Action("ContraloriaIndex", "Home") %>' id="1">Contraloria<a>--%>
    <%--<br />
    <br />
    <a href='<%= Url.Action("IncorporacionesIndex", "Home") %>' id="2">Incorporaciones<a>--%>

    <input type="button" value="Cerrar Sesión" id="btnLogout" onclick="location.href='<%=  Url.Action("Logout", "Home")%> '" />

<%--    
    iconos menu
    map  &#xf0c0;
    contra &#xf0f6;
    
    
    --%>


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


        <div id="content">
              <div class="ContenedorBanner" ></div>

            </div>
       
</div>

    <script>
        $("#MenuContainer").ready(function () {
            //$("#MenuContainer").append($("#cssmenu"))
            document.getElementById("MenuContainer").appendChild(document.getElementById("Menu"))
        });
    </script>


</asp:Content>
