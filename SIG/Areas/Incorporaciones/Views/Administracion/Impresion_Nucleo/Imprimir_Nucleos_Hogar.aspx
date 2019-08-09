<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Incorporaciones/Views/Shared/site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<style>
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



    <div class="Center-layout">
                 <%
                                    Html.DevExpress().FormLayout(Sub(setting)
                                                                     setting.Name = "frm_buscar_hogar"
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
                                                                                                                            Html.DevExpress().TextBox(Sub(txtSetting)
                                                                                                                                                          txtSetting.Name = "txtTitular"
                                                                                                                                                          txtSetting.Properties.ClientSideEvents.KeyUp = "function(s,e){Fnc_ValidarDatos(2,s)}"
                                                                                                                                                      End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                           itemSetting.Caption = "NÚMERO DE IDENTIDAD"
                                                                                       End Sub)
                                                                     setting.Items.Add(Sub(itemSetting)
                                                                                           itemSetting.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().Button(Sub(btnBuscar)
                                                                                                                                                         btnBuscar.Text = "Buscar"
                                                                                                                                                         btnBuscar.Name = "btnRegistrar"
                                                                                                                                                         btnBuscar.EnableTheming = False
                                                                                                                                                         btnBuscar.ControlStyle.Font.Size = 9
                                                                                                                                                         btnBuscar.ControlStyle.CssClass = "Boton"
                                                                                                                                                         btnBuscar.ClientSideEvents.Click = "function(e,s){ fnc_buscar_Nucleo_Imprimir()}"
                                                                                                                                                     End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                           itemSetting.Caption = "       "
                                                                                       End Sub)
                                                                 End Sub).GetHtml()
                 %>
             </div>


     <div id="hogar_impresion">
     
    </div>
    <div class="spinner" style="display: none;">
	  <div class="center">
		<img src="/Areas/Incorporaciones/Content/Images/loader.gif">
	  </div>
    </div>
</asp:Content>
