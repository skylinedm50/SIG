objTrasnferAcumu = new Object();
objTrasnferAcumu.CodigosEsqAdd = [0];
objTrasnferAcumu.UltimaBusqueda = null;



objTrasnferAcumu.Ver = function () {
    objTrasnferAcumu.CodigosEsqAdd = [0];
    objTrasnferAcumu.UltimaBusqueda = cbxEsquemas.GetValue();

    if (objTrasnferAcumu.UltimaBusqueda !== null) {
        GridViewDetalleEsquemaAcumulado.PerformCallback({
            strCodEsquemaAdd: objTrasnferAcumu.CodigosEsqAdd.join(','),
            intCodEsqBuscar: objTrasnferAcumu.UltimaBusqueda
        });
    } else {
        $("#mensaje-pla").html("*ERROR, para realizar una busqueda favor seleccionar un esquema de la lista desplegable.");
        $("#mensaje-pla").css("color", "red");
    }
}
objTrasnferAcumu.BuscarEsquemaAcumu = function () {
    var intIndiceSelect = GridViewPagoAcumulado.GetFocusedRowIndex();
    var intCodPago = GridViewPagoAcumulado.GetRowKey(intIndiceSelect);

    intCodPago = (intCodPago == null) ? 0 : intCodPago;

    GridViewEsquemaAcumulado.PerformCallback({
        intCodPago: intCodPago
    });
}

objTrasnferAcumu.Agregar = function () {
    var intIndiceSelect = GridViewEsquemaAcumulado.GetFocusedRowIndex();
    var intCodEsquema = GridViewEsquemaAcumulado.GetRowKey(intIndiceSelect);
    var intCodEsquemaPadre = cbxEsquemas.GetValue();
    var bolYaEsta = false;
    objTrasnferAcumu.UltimaBusqueda = intCodEsquemaPadre;

    if (intCodEsquema !== null) {
        if (intCodEsquemaPadre !== null) {
            $.ajax({
                type: 'POST',
                url: '/Planilla/Parametros/fnc_validar_ejecu_esquema',
                data: {
                    intCodEsquema: intCodEsquema
                },
                success: function (response) {
                    if (response == "False") {
                        $("#mensaje-pla").html("*ERROR, el esquema que desea acumular aun no ha sido ejecutado.");
                        $("#mensaje-pla").css("color", "red");
                    } else {
                        //Verificar que el codigo del esquema a agregar no se encuentre en la BD.
                        $.ajax({
                            type: 'POST',
                            url: '/Planilla/Parametros/fnc_validar_exist_esq_acumu',
                            data: {
                                intCodEsqPadre: intCodEsquemaPadre,
                                intCodEsqHaAcumu: intCodEsquema
                            },
                            success: function (response) {
                                if (response == "False") {
                                    for (var i = 0; i < objTrasnferAcumu.CodigosEsqAdd.length; i++) {
                                        if (objTrasnferAcumu.CodigosEsqAdd[i] == intCodEsquema) {
                                            bolYaEsta = true;
                                            break;
                                        }
                                    }

                                    if (bolYaEsta == false) {
                                        objTrasnferAcumu.CodigosEsqAdd.push(intCodEsquema);

                                        GridViewDetalleEsquemaAcumulado.PerformCallback({
                                            strCodEsquemaAdd: objTrasnferAcumu.CodigosEsqAdd.join(','),
                                            intCodEsqBuscar: intCodEsquemaPadre
                                        });

                                        $("#mensaje-pla").html('');
                                    } else {
                                        $("#mensaje-pla").html("*ERROR, el esquema que desea acumular ya existe, favor seleccionar otro.");
                                        $("#mensaje-pla").css("color", "red");
                                    }
                                } else {
                                    $("#mensaje-pla").html("*ERROR, el esquema que desea acumular ya existe, favor seleccionar otro.");
                                    $("#mensaje-pla").css("color", "red");
                                }
                            }
                        });
                    }
                }
            });
        } else {
            $("#mensaje-pla").html("*ERROR, favor seleccionar un esquema al cual se le va ha acumular el esquema seleccionado.");
            $("#mensaje-pla").css("color", "red");
        }
    } else {
        $("#mensaje-pla").html("*ERROR, favor seleccionar un esquema a acumular.");
        $("#mensaje-pla").css("color", "red");
    }
}
objTrasnferAcumu.BorrarTemporal = function (intCodEsquema) {
    for (var i = 0; i < objTrasnferAcumu.CodigosEsqAdd.length; i++) {
        if (objTrasnferAcumu.CodigosEsqAdd[i] == intCodEsquema) {
            objTrasnferAcumu.CodigosEsqAdd.splice(i, 1);

            GridViewDetalleEsquemaAcumulado.PerformCallback({
                strCodEsquemaAdd: objTrasnferAcumu.CodigosEsqAdd.join(','),
                intCodEsqBuscar: objTrasnferAcumu.UltimaBusqueda
            });
            break;
        }
    }
}

objTrasnferAcumu.BorrarBD = function (intCodBono) {
    $.ajax({
        type: 'POST',
        url: '/Planilla/Parametros/fnc_borrar_esquema_acumulado',
        data: {
            intCodBonoAcumu: intCodBono
        },
        success: function (response) {
            switch (response) {
                case "1":
                    $("#mensaje-pla").html("*MENSAJE, el registro se borro correctamente.");
                    $("#mensaje-pla").css("color", "green");

                    GridViewDetalleEsquemaAcumulado.PerformCallback({
                        strCodEsquemaAdd: objTrasnferAcumu.CodigosEsqAdd.join(','),
                        intCodEsqBuscar: objTrasnferAcumu.UltimaBusqueda
                    });
                    break;
                case "4":
                    $("#mensaje-pla").html("*Error, la sessión expiro, el usuario no pertenece al sistema favor vuelva a ingresar al sistema e intentelo nuevamente.");
                    $("#mensaje-pla").css("color", "red");
                    break;
                case "0":
                    $("#mensaje-pla").html("*Error, no se pudo efectuar la acción favor comunicarse con el administrador del sistema.");
                    $("#mensaje-pla").css("color", "red");
                    break;
                case "2":
                    $("#mensaje-pla").html("*Error, no se pudo efectuar la acción el esquema ya fue ejecutado.");
                    $("#mensaje-pla").css("color", "red");
                    break;
            }
        }
    });
}

objTrasnferAcumu.Guardar = function () {
    if (objTrasnferAcumu.CodigosEsqAdd[1] == undefined && objTrasnferAcumu.CodigosEsqAdd.length > 1) {
        $("#mensaje-pla").html("*ERROR, aun no se han agregados esquemas temporales acumulados.");
        $("#mensaje-pla").css("color", "red");
    } else {
        if (objTrasnferAcumu.UltimaBusqueda !== null) {
            $.ajax({
                type: 'POST',
                url: '/Planilla/Parametros/fnc_guardar_esquemas_acumulados',
                data: {
                    strCodEsquemasAcumu: objTrasnferAcumu.CodigosEsqAdd.join(','),
                    intCodEsquema: objTrasnferAcumu.UltimaBusqueda
                },
                success: function (response) {
                    switch (response) {
                        case "1":
                            $("#mensaje-pla").html("*MENSAJE, registros almacenados satisfactoriamente.");
                            $("#mensaje-pla").css("color", "green");

                            objTrasnferAcumu.CodigosEsqAdd = [0];

                            GridViewDetalleEsquemaAcumulado.PerformCallback({
                                strCodEsquemaAdd: objTrasnferAcumu.CodigosEsqAdd.join(','),
                                intCodEsqBuscar: objTrasnferAcumu.UltimaBusqueda
                            });
                            break;
                        case "4":
                            $("#mensaje-pla").html("*Error, la sessión expiro, el usuario no pertenece al sistema favor vuelva a ingresar al sistema e intentelo nuevamente.");
                            $("#mensaje-pla").css("color", "red");
                            break;
                        case "0":
                            $("#mensaje-pla").html("*Error, no se pudo efectuar la acción favor comunicarse con el administrador del sistema.");
                            $("#mensaje-pla").css("color", "red");
                            break;
                        case "2":
                            $("#mensaje-pla").html("*Error, no se pudo efectuar la acción el esquema al que se le quiere acumular esquemas ya fue ejecutado.");
                            $("#mensaje-pla").css("color", "red");
                            break;
                    }
                }
            });
        } else {
            $("#mensaje-pla").html("*ERROR, favor seleccionar de la lista desplegable el esquema al que se le van a asignar el o los esquemas acumulados.");
            $("#mensaje-pla").css("color", "red");
        }
    }
}

function fnc_cbxPagosIndexChaged()
{
    cbxEsquemas.PerformCallback();
    GridViewPagoAcumulado.PerformCallback({intCodPago: 0});
    GridViewEsquemaAcumulado.PerformCallback({intCodPago: 0});
    GridViewDetalleEsquemaAcumulado.PerformCallback({ strCodEsquemaAdd: "0", intCodEsqBuscar: 0 });
    objTrasnferAcumu.CodigosEsqAdd = [0];
    objTrasnferAcumu.UltimaBusqueda = null;
    $("#mensaje-pla").html("");
}

function fnc_cbxEsquemasIndexChanged() {
    var intCodPago  = cbxPagos.GetValue();
    var intCodEsquema = cbxEsquemas.GetValue();

        GridViewPagoAcumulado.PerformCallback({
            intCodPago: intCodPago
        });

        GridViewEsquemaAcumulado.PerformCallback({ intCodPago: 0 });
        GridViewDetalleEsquemaAcumulado.PerformCallback({ strCodEsquemaAdd: "0", intCodEsqBuscar: 0 });
        objTrasnferAcumu.CodigosEsqAdd = [0];
        objTrasnferAcumu.UltimaBusqueda = null;
        $("#mensaje-pla").html("");
}