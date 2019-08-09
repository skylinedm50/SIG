
function RealizarApertura()
{

    $.ajax({
        type: 'Post',
        url: "/Incorporaciones/Incorporaciones/Crear_Apertura/",
        data: { "PeriodoNombre": txtApertura.GetText() },
        success: function (response) {
            if (response == "1") {
                swal(
                        "",
                        "La apertura de actualizaciones se realizó correctamente",
                        "success"
                    );
            } else if (response == "2")
            {
                $("#Error_text").text("ERROR! No se pudo realizar la apertura de actualización debido a que ya existe un periodo de actualización abierto")
                $(".error").show()
            } else if (response == "3")
            {
                $("#Error_text").text("ERROR! La apertura se realizo pero no se pudo trasladar todas las actualizaciones realizadas en forma temporal")
                $(".error").show()
            }
            else
            {
                $("#Error_text").text("ERROR! No se pudo realizar la apertura de actualización")
                $(".error").show()
            }
        },
        datatype: "json",
        traditional: true
    })

}