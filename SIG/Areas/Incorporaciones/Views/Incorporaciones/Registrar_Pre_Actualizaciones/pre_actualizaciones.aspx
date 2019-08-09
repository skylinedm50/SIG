<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Incorporaciones/Views/Shared/site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="div-main-content">
        <form id="form1" runat="server">          
             <div class="Center-layout">
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
                                                                                                                                                     btnBuscar.ClientSideEvents.Click = "function(e,s){buscar_para_pre_actualizacion()}"
                                                                                                                                                 End Sub).GetHtml()
                                                                                                                    End Sub)
                                                                                       itemSetting.Caption = "       "
                                                                                   End Sub)
                                                             End Sub).GetHtml()
                 %>
             </div>
        </form>
          <div id="infor_hogar">

          </div>
        </div>
</asp:Content>
