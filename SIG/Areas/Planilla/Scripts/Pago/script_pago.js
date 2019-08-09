function fnc_ingresar_pago()
{
    var intAño = ((AspxSpinEditAñoPago.GetNumber() == null) ? 0 : AspxSpinEditAñoPago.GetNumber());
    var strNombre = AspxTextBoxNombrePago.GetValue();
    var strDescripcion = AspxMemoDescripPago.GetValue();
    var patt = /^\s/g;

    if (strNombre == null || strDescripcion == null || intAño == 0) {
        $("#mensaje-pla").html("*ERROR, el año, nombre y la descripción del pago no deben estar vacios.");
        $("#mensaje-pla").css("color", "red");
    } else if (patt.test(strNombre) == true || patt.test(strDescripcion) == true) {
        $("#mensaje-pla").html("*ERROR, el nombre así como la descripción no deben contener espacios vacios al inicio.");
        $("#mensaje-pla").css("color", "red");
    } else {
        $.ajax({
            type: 'POST',
            url: '/Planilla/Parametros/GridViewPartialNewPago',
            data: {
                intAñoPago: intAño, strNombrePago: strNombre.toUpperCase(), strDescripcionPago: strDescripcion.toUpperCase()
            },
            success: function (response) {
                switch (parseInt(response)) {
                    case 1:
                        AspxGridViewPagos.Refresh();
                        $("#mensaje-pla").html("*Registros ingresado satisfactoriamente.");
                        $("#mensaje-pla").css("color", "green");
                        fnc_limpiar_campos();
                        break;
                    case 2:
                        $("#mensaje-pla").html("*Error, la sessión expiro, el usuario no pertenece al sistema favor vuelva a ingresar al sistema e intentelo nuevamente.");
                        $("#mensaje-pla").css("color", "red");
                        break;
                    case 3:
                        $("#mensaje-pla").html("*ERROR, existe otro registro con el mismo nombre.");
                        $("#mensaje-pla").css("color", "red");
                        break;
                    default:
                        $("#mensaje-pla").html("*ERROR, imposible ingresar el registro comuniquese con el administrador del sistema.");
                        $("#mensaje-pla").css("color", "red");
                }
            }
        });
    }
}

function fnc_borrar(s, e) {
    var intCodPago = s.GetRowKey(e.visibleIndex);

    if (intCodPago !== null) {
        if (confirm("¿Aún desea borrar el pago?") == true) {
            $.ajax({
                type: 'POST',
                url: '/Planilla/Parametros/GridViewPartialDeletePago',
                data: { pag_codigo: intCodPago },
                success: function (response) {
                    switch (parseInt(response)) {
                        case 0:
                            $("#mensaje-pla").html("*ERROR, no fue posible borrar el pago, comuniquese con el administrador del sistema.");
                            $("#mensaje-pla").css("color", "red");
                            break;
                        case 1:
                            AspxGridViewPagos.Refresh();
                            $("#mensaje-pla").html("");
                            break;
                        case 3:
                            $("#mensaje-pla").html("*ERROR, imposible borrar, solo es posible borrar los pagos de mayor a menor según el número del pago.");
                            $("#mensaje-pla").css("color", "red");
                            break;
                        case 2:
                            $("#mensaje-pla").html("*Error, la sessión expiro, el usuario no pertenece al sistema favor vuelva a ingresar al sistema e intentelo nuevamente.");
                            $("#mensaje-pla").css("color", "red");
                            break;
                        case 5:
                            $("#mensaje-pla").html("*ERROR, imposible borrar, existen esquemas que dependen de este pagos.");
                            $("#mensaje-pla").css("color", "red");
                            break;
                    }
                }
            });
        }
    } else {
        $("#mensaje-pla").html("*ERROR, no se logró identificar el pago, comuniquese con el administrador del sistema.");
        $("#mensaje-pla").css("color", "red");
    }
}


function fnc_limpiar_campos()
{
    AspxSpinEditAñoPago.SetValue(null);
    AspxTextBoxNombrePago.SetValue(null);
    AspxMemoDescripPago.SetValue(null);
}

