<%@ Page Title="" Language="VB" MasterPageFile="~/Views/shared/site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
  <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.validate/1.5.5/jquery.validate.min.js"></script>
  <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
  <script type="text/javascript" src="../../Scripts/materialize.min.js"></script>
  <link href="../../Css/materialize.min.css" rel="stylesheet" />
  <link href="../../Css/home.css" rel="stylesheet" />

  <script type="text/javascript">
      $(document).ready(function () {
          // Initialize validation on the entire ASP.NET form
          $("#formLogin").validate({
              // This prevents validation from running on every
              //  form submission by default.
              rules: {

                  Username: "required",
                  Password: "required"
              },

              messages: {

                  Username: "",
                  Password: "",

              },

              onsubmit: false

          });



          $("#Order").click(function (evt) {
              // Validate the form and retain the result.
              var isValid = $("#formLogin").valid();

              // If the form didn't validate, prevent the
              //  form submission.
              if (!isValid)
                  evt.preventDefault();
          });
      });
  </script>

<script type = "text/javascript">

    $(document).ready(function () {
        setTimeout(function () {
            $(".Message").fadeOut(1500);
        }, 3000);

        $('#Order').click(function () {
            $(".Message").show();
            setTimeout(function () {
                $(".Message").fadeOut(1500);
            }, 3000);
        });
    });

</script>



    <div class="wrapper">
       
        <div class="container">
            <div class="row"><div class="col s12"></div></div>
            <div class="row"><div class="col s12"></div></div>
            <div class="row"><div class="col s12"></div></div>
            <div class="row"><div class="col s12"></div></div>
            <div class="row"><div class="col s12"></div></div>
            <div class="row"><div class="col s12"></div></div>
            <div class="row"><div class="col s12"></div></div>
            <div class="row"><div class="col s12"></div></div>
            <div class="row"><div class="col s12"><div class="center-align"><img class="responsive-img" src="../../Imagen/SSIS.png"></div></div></div>
            <div class="row"><div class="col s12"><h4 class ="blue-text text-darken-3 flow-text center-align">INICIO DE SESIÓN</h4></div></div>
         
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
             
                                  <input id="username2" type="text" class="visually-hidden" name="fakeusernameremembered">
                                  <br />
                                  <div class="input-field">
    
                                    <i class="material-icons prefix">lock</i>
                                    <label for="Password">Contraseña</label>
                                    <input type="Password" id="Password" class="validate" name="Password">
                
                                  </div>
                                    <input  type="password" class="visually-hidden" value="_">
          
                                  <label id="Message"></label>
                                  <br /> <br />
                                  <button class="btn waves-effect waves-light btn-large blue darken-3 z-depth-4 truncate" onclick="" id="Order"><i class="large material-icons right">arrow_forward</i><b>ENTRAR</b></button>

     
                                <div id="MessageContainer">
                                    <%--Aqui va el Mensaje de Error--%>
                                </div>
     

                                <script>
                                    $("#MessageContainer").ready(function () {
                                        //$("#MenuContainer").append($("#cssmenu"))
                                        document.getElementById("MessageContainer").appendChild(document.getElementById("Message"))
                                    });
                                </script>

                                </form>
                                </div>

                      </div>
                  <div class="col s3"></div>
              </div>
        </div>
    </div>


    
</asp:Content>

