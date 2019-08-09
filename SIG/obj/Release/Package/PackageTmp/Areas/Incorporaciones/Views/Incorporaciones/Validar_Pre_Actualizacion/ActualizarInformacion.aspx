<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Incorporaciones/Views/Shared/site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<style>

      .info, .success, .warning, .error, .validation {
            border: 1px solid;
            margin: 10px 0px;
            padding:15px 10px 15px 50px;
            background-repeat: no-repeat;
            background-position: 10px center;
            display: none;
        }

        .info {
            color: #00529B;
            background-color: #BDE5F8;
            background-image: url("/Areas/Incorporaciones/Content/Images/info.png");
        }
        .success {
            color: #4F8A10;
            background-color: #DFF2BF;
            background-image:url("/Areas/Incorporaciones/Content/Images/success.png");
        }
        .warning 
        {
            color: #9F6000;
            background-color: #FEEFB3;
            background-image: url( "/Areas/Incorporaciones/Content/Images/warning.png");
        }
        .error 
        {
            color: #D8000C;
            background-color: #FFBABA;
            background-image: url("/Areas/Incorporaciones/Content/Images/error.png");
        }

        .hidden 
        {
            display:none;
        }

        .Boton 
        {
            background:#1787C8;
            color:#fff;
            border : none;
            font-family : "Arial" Georgia;
            width:200px;
            height:40px;
            vertical-align:central;
            text-align:center;
        }

        .Boton:hover
        {
            background:#fff;
            border: 1px solid #1787C8;
            cursor:pointer;
            color:#1787C8;
            box-sizing:border-box;
        }

       .spinner {
		    position: fixed;
		    z-index: 999;
		    height: 100%;
		    width: 100%;
		    top: 0;
		    left: 0;
		    background-color: Black;
		    filter: alpha(opacity=60);
		    opacity: 0.6;
		    -moz-opacity: 0.8;
	    }
	    .center	{
		    z-index: 1000;
		    margin: 300px auto;
		    padding: 10px;
		    width: 130px;
		    /*background-color: White;*/
		    border-radius: 10px;
		    filter: alpha(opacity=100);
		    opacity: 1;
		    -moz-opacity: 1;
	    }
	    .center img	{
		    height: 128px;
		    width: 128px;
	    }

</style>



    <div id="Buscar-Informacion">
         <%
                                      Html.DevExpress().FormLayout(Sub(setting)
                                                                 setting.Name = "frmpre_actualizaciones"
                                                                 setting.ColCount = 1
                                                                 setting.Items.Add(Sub(itemSetting)
                                                                                       itemSetting.SetNestedContent(Sub()
                                                                                                                        Html.DevExpress().TextBox(Sub(txtsetting)
                                                                                                                                                      txtsetting.Name = "txtHogar"
                                                                                                                                                      txtsetting.Properties.ClientSideEvents.KeyUp = "function(s,e){Fnc_ValidarDatos(2,s)}"
                                                                                                                                                  End Sub).GetHtml()
                                                                                                                                                                   %>
<br />              <%
                                                                                                                    End Sub)
                                                                                       itemSetting.Caption = "NÚMERO DE HOGAR"
                                                                                   End Sub)

                                                                 setting.Items.Add(Sub(itemSetting)
                                                                                       itemSetting.SetNestedContent(Sub()
                                                                                                                        Html.DevExpress().Button(Sub(btnBuscar)
                                                                                                                                                     btnBuscar.Text = "Buscar"
                                                                                                                                                     btnBuscar.Name = "btnRegistrar"
                                                                                                                                                     btnBuscar.EnableTheming = False
                                                                                                                                                     btnBuscar.ControlStyle.Font.Size = 9
                                                                                                                                                     btnBuscar.ControlStyle.CssClass = "Boton"
                                                                                                                                                     btnBuscar.ClientSideEvents.Click = "function(e,s){fnc_buscarHogarActualizarInformacionExpediente()}"
                                                                                                                                                 End Sub).GetHtml()
                                                                                                                    End Sub)
                                                                                       itemSetting.Caption = "       "
                                                                                   End Sub)
                                                             End Sub).GetHtml()
         %>
    </div>
    <div id="InformacionHogar">

    </div>
    <div id="divMensaje">
        <div class="warning" id="Warning_text_H" ></div>
        <div class="error" id="Error_text_H"></div>
    </div>
    <div class="spinner" style="display: none;">
	  <div class="center">
		<img src="/Areas/Incorporaciones/Content/Images/loader.gif">
	  </div>
    </div>
</asp:Content>
