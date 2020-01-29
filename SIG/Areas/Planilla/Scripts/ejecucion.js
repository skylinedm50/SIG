

var selectedIDs;
//function OnBeginCallback(s, e) {
//    //Pass all selected keys to GridView callback action
//    e.customArgs["selectedIDs"] = selectedIDs;
//}
function OnSelectionChanged(s, e) {
    s.GetSelectedFieldValues("esq_codigo", GetSelectedFieldValuesCallback);
}
function GetSelectedFieldValuesCallback(values) {
    //Capture all selected keys
    selectedIDs = values.join(',');
    txtSelectedIdsHidden.SetText(selectedIDs);
}
//function OnClick(s, e) {
//    //Show all selected keys on client side
//    alert(selectedIDs);
//}
//function OnSubmitClick(s, e) {
//    //Write all selected keys to hidden input. Pass them on post action.
//    $("#selectedIDsHF").val(selectedIDs);
//}


function fnc_cbxPagosIndexChaged() {
    try {
        gdvEsquemas.PerformCallback();
    } catch (e) {
        console.log('No existe el gridview de los esquemas');
    }

    try {
        cbxEsquemas.PerformCallback();
    } catch (e) {
        console.log('No existe el combobox de los esquemas');
    }    
}



function btnEjecutarPlanillaClick() {

    if (selectedIDs.length > 0) {

        enviarSignalRId()


        /*
        $.ajax({
            url: "/Planilla/Ejecucion/ejecutarPlanilla",
            type: 'POST',
            dataType: 'html',
            data: {
                esquemas: selectedIDs
            },
            beforeSend: function () {
                $(".modal").show();
            },
        })
        .done(function (response) {

            var json = $.parseJSON('[' + response + ']');
            var contador = [0, 0];
            var mostrarProgramado = false;
            var mensaje = "";

            $.each(json, function (i, item) {
                if (json[i].estado === 1) {
                    contador[0] += 1;
                } else {
                    contador[1] += 1;
                }
            });

            if (contador[0] === json.length) {

                if (json.length === 1) {
                    $('.success').html("Planilla generada exitosamente.");
                    mostrarProgramado = true;
                } else {
                    $('.success').html("Planillas generadas exitosamente.");
                    mostrarProgramado = true;
                }
                $('.success').show();

            } else if (contador[1] === json.length) {
                
                $.each(json, function (i, item) {

                    switch (json[i].estado) {
                        case 0:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". Error durante la generación, no se ha podido determinar la causa del fallo."
                            break;
                        case 1:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". Se genero correctamente."
                            break;
                        case 2:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". La planilla no se ha generado debido a que no se ha podido comprobar la aprobación de los parámetros de la planilla."
                            break;
                        case 3:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". Los parámetros estan aprobados pero la planilla fue generada anteriormente."
                            break;
                        case 4:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". La planilla ya esta generada y falta la aprobación de los parámetros"
                            break;
                        default:
                            break;
                    }
                    $('.error').html(mensaje);
                    $('.error').show();
                    $(".modal").hide();
                });

            } else {

                $.each(json, function (i, item) {

                    switch (json[i].estado) {
                        case 0:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". Error durante la generación, no se ha podido determinar la causa del fallo."
                            break;
                        case 1:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". Se genero correctamente."
                            break;
                        case 2:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". La planilla no se ha generado debido a que no se ha podido comprobar la aprobación de los parámetros de la planilla."
                            break;
                        case 3:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". Los parámetros estan aprobados pero la planilla fue generada anteriormente."
                            break;
                        case 4:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". La planilla ya esta generada y falta la aprobación de los parámetros"
                            break;
                        default:
                            break;
                    }
                });
                mostrarProgramado = true;
                $('.warning').html(mensaje);
                $('.warning').show();

            }

            if (mostrarProgramado) {

                $.ajax({
                    url: "/Planilla/Ejecucion/pv_gdvPlanillaGenerada",
                    type: 'POST',
                    datatype: 'html',
                    traditional: true,
                    data: { esquemas: selectedIDs },
                    complete: function () {
                        $(".modal").hide();
                    }
                })
                .done(function (response) {
                    $('#divPivotGrid').html(response);
                });
            }
        })
        .fail(function () {
            console.log("error");
        })
        .always(function () {
            console.log("complete");
        });
        */

        $.ajax({
            url: "/Planilla/Ejecucion/ejecutarPlanilla",
            type: 'POST',
            dataType: 'html',
            data: {
                esquemas: selectedIDs
            },
            beforeSend: function () {
                $(".modal").show();
            },
        })
    }
}

function btnVerResumenClick() {

    $.ajax({
        url: "/Planilla/Ejecucion/pv_gdvPlanillaGenerada",
        type: 'POST',
        datatype: 'html',
        traditional: true,
        data: { strEsquemas: selectedIDs },
        complete: function () {
            $(".modal").hide();
        },
        beforeSend: function () {
            $(".modal").show();
        }
    })
    .done(function (response) {
        $('#divPivotGrid').html(response);
    });

}

function enviarSignalRId() {

    $.ajax({
        type: 'POST',
        url: '/Planilla/Ejecucion/recibirSignalRId',
        async: false,
        data: {
            id: $.connection.hub.id
        }
    });
}

///////////////////////////////////////////////////////////////////////////
//función para el SignalR
$(function () {
    try {
        var connection = $.connection.signalREjecucion;

        $.connection.hub.start().done(function (str) {
            //alert("Conectado a Signal R");
            console.log("Conectado a Signal R, connection ID: " + $.connection.hub.id);
        });

        /*
        connection.client.incrementarProgressBar = function (progreso) {
            pbCarga.SetPosition(progreso);
            if (progreso === 100) {
                detener();
                //recargar();
                recargarMemo();
            }
        };

        connection.client.finPrecarga = function () {
            //recargar();
            recargarMemo();
        };
        */

        connection.client.finEjecucion = function (response) {
            debugger

            var json = $.parseJSON('[' + response + ']');
            var contador = [0, 0];
            //var mostrarProgramado = false;
            var mensaje = "";

            $.each(json, function (i, item) {
                if (json[i].estado === 1) {
                    contador[0] += 1;
                } else {
                    contador[1] += 1;
                }
            });

            if (contador[0] === json.length) {

                if (json.length === 1) {
                    $('.success').html("Planilla generada exitosamente.");
                    //mostrarProgramado = true;
                } else {
                    $('.success').html("Planillas generadas exitosamente.");
                    //mostrarProgramado = true;
                }
                //$(".modal").hide();
                $('.success').show();

            } else if (contador[1] === json.length) {

                $.each(json, function (i, item) {

                    switch (json[i].estado) {
                        case 0:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". Error durante la generación, no se ha podido determinar la causa del fallo."
                            break;
                        case 1:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". Se genero correctamente."
                            break;
                        case 2:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". La planilla no se ha generado debido a que no se ha podido comprobar la aprobación de los parámetros de la planilla."
                            break;
                        case 3:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". Los parámetros estan aprobados pero la planilla fue generada anteriormente."
                            break;
                        case 4:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". La planilla ya esta generada y falta la aprobación de los parámetros"
                            break;
                        default:
                            break;
                    }
                    $('.error').html(mensaje);
                    $('.error').show();
                    //$(".modal").hide();
                });

            } else {

                $.each(json, function (i, item) {

                    switch (json[i].estado) {
                        case 0:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". Error durante la generación, no se ha podido determinar la causa del fallo."
                            break;
                        case 1:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". Se genero correctamente."
                            break;
                        case 2:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". La planilla no se ha generado debido a que no se ha podido comprobar la aprobación de los parámetros de la planilla."
                            break;
                        case 3:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". Los parámetros estan aprobados pero la planilla fue generada anteriormente."
                            break;
                        case 4:
                            mensaje += "<br />La planilla de código: " + json[i].codigo + "y nombre: " + json[i] + ". La planilla ya esta generada y falta la aprobación de los parámetros"
                            break;
                        default:
                            break;
                    }
                });
                //mostrarProgramado = true;
                $('.warning').html(mensaje);
                $('.warning').show();

            }

            $(".modal").hide();

            /*
            if (mostrarProgramado) {

                $.ajax({
                    url: "/Planilla/Ejecucion/pv_gdvPlanillaGenerada",
                    type: 'POST',
                    datatype: 'html',
                    traditional: true,
                    data: { esquemas: selectedIDs },
                    complete: function () {
                        $(".modal").hide();
                    }
                })
                .done(function (response) {
                    $('#divPivotGrid').html(response);
                });
            }
            */
        };


    } catch (err) {
        console.log(err);
    }


});
///////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function btnAnularPlanillaClick(s, e) {

    if (cbxEsquemas.GetValue() !== null) {
        $.ajax({
            url: "/Planilla/Ejecucion/anularEsquema",
            type: 'POST',
            dataType: 'html',
            data: {
                esquema: cbxEsquemas.GetValue(),
                observacion: txtRazonAnulacion.GetText()
            },
            beforeSend: function () {
                $(".modal").show();
            },
            complete: function () {
                $(".modal").hide();
            }
        })
        .done(function (response) {

            if (response === "1") {
                $('.success').html("La planilla se anulo exitosamente.");
                $('.success').show();
            } else {
                $('.error').html("Ocurrio un error, no se puedo anular la planilla.<br />Puede que la planilla ya este anulada, no se haya generado aún o que ya se hayan cargado pagos de esta planilla.");
                $('.error').show();
            }
        })
        .fail(function () {
            console.log("error");
        })
        .always(function () {
            console.log("complete");
        });
    }

}