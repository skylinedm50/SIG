<%@ Page Title="" Language="VB" MasterPageFile="~/Views/shared/site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
  <script type="text/javascript" src="../../Scripts/jquery.min.js"></script>
  <script type="text/javascript" src="../../Scripts/jquery.validate.min.js"></script>
  <link href="../../Css/icon.css" rel="stylesheet">
  <script type="text/javascript" src="../../Scripts/materialize.min.js"></script>
  <script type="text/javascript" src="../../Scripts/Home.js"></script>
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
            
            <div class = "card-panel white z-depth-3">
            <div class="row"><div class="col s12 left-align"></div><a href="/Home"><b><i class="Small material-icons left">arrow_back</i>Volver al inicio</b></a></div>
            <div class="row"><div class="col s12"></div></div>
            <div class="row"><div class="col s12"><div class="center-align"><img class="responsive-img" src="../../Imagen/SSIS.png"></div></div></div>
            <div class="row"><div class="col s12"><h4 class ="blue-text text-darken-3 center-align">Inicio de Sesión</h4></div></div>
         
              <div class="row">
                  <div class="col s3"></div>
                  <div class="col s6">
                                
                              <div class = "center-align">
                              <form action="/Home/Login" id="formLogin"  method="post" autocomplete="off">

          
                                  <div class="input-field">
                 
                                    <i class="material-icons prefix">account_circle</i>
                                    <label for="Username">Nombre de Usuario</label>
                                    <input type="text" id="Username" class="validate" name="Username">
               
                                  </div>
             
                                 
                                  <br />
                                  <div class="input-field">
    
                                    <i class="material-icons prefix">lock</i>
                                    <label for="Password">Contraseña</label>
                                    <input type="Password" id="Password" class="validate" name="Password">
                
                                  </div>
                               
          
                                  <label id="Message"></label>
                                  <br /> <br />
                                  <button class="btn waves-effect waves-light btn-large blue darken-3 z-depth-4 truncate" onclick="" id="Order"><i class="large material-icons right">arrow_forward</i><b>ENTRAR</b></button><br /><br /><br />
                                  <a class="flow-text center-align" href='<%=Url.Action("RecordarContrasena", "Home")%>' >¿Olvidaste la contraseña?</a>
     
                                <div id="MessageContainer">
                                    <%--Aqui va el Mensaje de Error--%>
                                </div>
                                     </form>
                                </div>

                      </div>
                  <div class="col s3"></div>
              </div>
                <div class="row"><div class="col s12"></div></div>
            <div class="row"><div class="col s12"></div></div>
                </div>
        </div>
    </div>


    
</asp:Content>

