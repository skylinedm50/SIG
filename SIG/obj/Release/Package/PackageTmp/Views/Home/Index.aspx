<%@ Page Title="" Language="VB" MasterPageFile="~/Views/shared/site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

    <script type="text/javascript" src="../../Scripts/materialize.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-2.1.4.js"></script>
   
    <link href="../../Css/materialize.min.css" rel="stylesheet" />
     <link href="../../Css/home.css" rel="stylesheet" />

    
        <div class="wrapper">
           
            <div class="container">
                <div class="row"><div class="col s12"></div></div>
                <div class="row"><div class="col s12"></div></div>
                <div class="row"><div class="col s12"><div class="center-align"><img class="responsive-img" src="../../Imagen/BannerLogin.png"></div></div></div>
                <div class="row">
                    
                    <div class="col s12" >
                            <div class="center-align">
                               <a class="btn waves-effect waves-light btn-large blue darken-3 z-depth-4" style ="width:300px; margin-bottom:10px"  href='<%=Url.Action("Login", "Home")%>'><b class="truncate">ENTRAR A SIG</b><a>
                               <a class="btn waves-effect waves-light btn-large blue darken-3 z-depth-4" style ="width:300px; margin-bottom:10px" href='http://192.168.41.8:82/areas/siap/MAP'><b class="truncate">ENTRAR A MAP</b><a>
                               <a class="btn waves-effect waves-light btn-large blue darken-3 z-depth-4" style ="width:300px; margin-bottom:10px" href='http://192.168.41.13:85/Core/Areas/SIAP/mapxd/'><b class="truncate">EXPEDIENTES DIGITALES</b><a>
                               <a class="btn waves-effect waves-light btn-large blue darken-3 z-depth-4" style ="width:300px; margin-bottom:10px" href='https://play.google.com/store/apps/details?id=com.map_movil.map_movil'><b class="truncate">DESCARGAR APLICACIÓN MAP</b><a>
                    </div>
                </div>
                    
              </div>
               
              
    </div>
             </div>
       
</asp:Content>

