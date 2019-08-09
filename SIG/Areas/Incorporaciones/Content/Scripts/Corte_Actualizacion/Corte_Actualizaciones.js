
var selectedIDs

function OnSelectionChanged(s, e)
{
    s.GetSelectedFieldValues("cod_actualizacion_periodo", GetSelectedFieldValuesCallback);
}

function GetSelectedFieldValuesCallback(values)
{
    selectedIDs = values.join(',');
}


function RealizarCierre()
{
    $.ajax({
        type: 'Post',
        url: "/Incorporaciones/Incorporaciones/Realizar_Cierre/",
        data: { "periodo": selectedIDs },
        success: function (response) {
            if (response == '1') {
                swal(
                        "",
                        "El cierre se realizó correctamente",
                        "success"
                      );
            } else {
                $("#Error_text").text("ERROR! El cierre no se pudo realizar correctamente.")
                $(".error").show()
                $("#Error_text_H").hide()

            }
            
        },
        datatype: "json",
        traditional: true
    })

}


