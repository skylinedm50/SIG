<%@ Page Title="" Language="VB" MasterPageFile="~/Views/shared/site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
SIG | Recuperación de Contraseña
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <script type="text/javascript" src="../../Scripts/jquery.min.js"></script>
  <script type="text/javascript" src="../../Scripts/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../Scripts/forgot.js"></script>
  <link href="../../Css/icon.css" rel="stylesheet">
  <script type="text/javascript" src="../../Scripts/materialize.min.js"></script>
  <link href="../../Css/materialize.min.css" rel="stylesheet" />
  <link href="../../Css/home.css" rel="stylesheet" />
    
    <div class="wrapper">
       
        <div class="container">
            <div class="row"><div class="col s12"></div></div>
            <div class="row"><div class="col s12"></div></div>
            <div class="row"><div class="col s12"></div></div>
            <div class="row"><div class="col s12"></div></div>
            <div class="row"><div class="col s12"></div></div>
            <div class="row"><div class="col s12"></div></div>
            <div class="row"><div class="col s12"></div></div>
            <div class = "card white grey lighten-5 z-depth-3">
            <div class="row"><div class="col s12"></div></div>
            <div class="row"><div class="col s12"><div class="center-align"><img class="responsive-img" src="../../Imagen/SSIS.png"></div></div></div>
            <div class="row"><div class="col s12"><h4 class ="blue-text text-darken-3 center-align">Recuperación de Contraseña</h4></div></div>


            <div class="row">
                  <div class="col s3"></div>
                  <div class="col s6">
                      <div class = "center-align">
                          <form action="/Home/RecuperacionContrasena" id="formLogin"  method="post" autocomplete="off">
                          <label>Ingresa tu correo electrónico y nombre de usuario para la recuperación de la contraseña</label>
                          <div class="input-field">
                 
                                    <i class="material-icons prefix">mail</i>
                                    <label for="Email">Correo Electrónico</label>
                                    <input type="text" id="Email" class="validate" name="Email">
               
                          </div><br />
                          <div class="input-field">
                 
                                    <i class="material-icons prefix">account_circle</i>
                                    <label for="Username">Nombre de Usuario</label>
                                    <input type="text" id="Username" class="validate" name="Username">
               
                          </div><br />
                          <button type = "button" class="btn waves-effect waves-light btn-large blue darken-3 z-depth-4 truncate" onclick="location.href='<%=Url.Action("Login", "Home")%>'"  id="Order"><i class="large material-icons left">arrow_back</i><b>ATRÁS</b></button>
                          <button type = "button" id="btnNext" class="btn waves-effect waves-light btn-large blue darken-3 z-depth-4 truncate" ><i class="large material-icons right"></i><b>Recuperar</b></button><br /><br /><br />
                      </form>                       
                          
                    </div>
                  </div>
                </div>
              </div>
            </div>
        </div>
</asp:Content>
