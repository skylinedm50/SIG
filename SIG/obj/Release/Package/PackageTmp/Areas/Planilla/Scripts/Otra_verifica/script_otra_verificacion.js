objOtraVeri = new Object();

objOtraVeri.Verificacion = new Object();
objOtraVeri.Verificacion.Codigos = [];
objOtraVeri.Verificacion.Agregar = function () {
    var objCheckBox = document.getElementsByName('checkboxOtraVeri');
    var intTamaño = objCheckBox.length;

    objOtraVeri.Verificacion.Codigos = [];

    for (var i = 0; i < intTamaño; i++) {
        if (objCheckBox[i].checked == true) {
            objOtraVeri.Verificacion.Codigos.push(objCheckBox[i].value);
        }
    }
}
objOtraVeri.Verificacion.SelectAll = function (boolEsatdo) {
    var objCheckBox = document.getElementsByName('checkboxOtraVeri');
    var intTamaño = objCheckBox.length;

    if (intTamaño > 0) {
        for (var i = 0; i < intTamaño; i++) {
            objCheckBox[i].checked = boolEsatdo;
        }

        objOtraVeri.Verificacion.Agregar();
    } else {
        $("#mensaje-pla").html("*ERROR, no se pueden seleccionar las verificaciones, favor comunicarse con el encargado del sistema.");
        $("#mensaje-pla").css("color", "red");
    }
}
objOtraVeri.Verificacion.Guardar = function () {
    objOtraVeri.Verificacion.Agregar();

    if (objOtraVeri.Esquemas.Codigos !== '0') {
        $.ajax({
            type: 'POST',
            url: '/Planilla/Parametros/fnc_verificar_enlace',
            data: {
                strCodEsquemas: objOtraVeri.Esquemas.Codigos
            },
            success: function (response) {
                if (response == "1") {
                    $.ajax({
                        type: 'POST',
                        url: '/Planilla/Parametros/fnc_guardar_verificaciones',
                        data: {
                            strCodEsquemas: objOtraVeri.Esquemas.Codigos,
                            strCodVerificacion: ((objOtraVeri.Verificacion.Codigos.length > 0) ? objOtraVeri.Verificacion.Codigos.join(',') : "0")
                        },
                        success: function (response) {
                            switch (response) {
                                case "1":
                                    $("#mensaje-pla").html("*MENSAJE, acción realizada satisfactoriamente satisfactoriamente.");
                                    $("#mensaje-pla").css("color", "green");
                                    break;
                                case "0":
                                    $("#mensaje-pla").html("*Error, no se pudo efectuar la acción favor comunicarse con el administrador del sistema.");
                                    $("#mensaje-pla").css("color", "red");
                                    break;
                                case "4":
                                    $("#mensaje-pla").html("*Error, la sessión expiro, el usuario no pertenece al sistema favor vuelva a ingresar al sistema e intentelo nuevamente.");
                                    $("#mensaje-pla").css("color", "red");
                                    break;
                                case "3":
                                    $("#mensaje-pla").html("*Error, algunos de los esquemas seleccionados ya fue ejecutados, imposible realizar la acción.");
                                    $("#mensaje-pla").css("color", "red");
                                    break;
                                case "5":
                                    $("#mensaje-pla").html("MENSAJE, la acción no se realizó por que no hay verificaciones selecciondas y en el sistema no se identificaron registros.");
                                    $("#mensaje-pla").css("color", "orange");
                                    break;
                                case "6":
                                    $("#mensaje-pla").html("MENSAJE, la acción se realizó correctamente los esquemas seleccionados contenian verificaciones en el sistema y se eliminarón.");
                                    $("#mensaje-pla").css("color", "green");
                                    break;
                            }
                        }
                    });
                }else {
                    $("#mensaje-pla").html("*Error, imposible realizar la acción los esquemas seleccionados no comporten las mismas verificaciones.");
                    $("#mensaje-pla").css("color", "red");
                }
            }
         });
    }else {
        $("#mensaje-pla").html("*ERROR, favor seleccionar uno o varios esquemas para guardar las verificaciones.");
        $("#mensaje-pla").css("color", "red");
    }
}
objOtraVeri.Verificacion.Ver = function () {
    if (objOtraVeri.Esquemas.Codigos !== '0') {
        $.ajax({
            type: 'POST',
            url: '/Planilla/Parametros/fnc_verificar_enlace',
            data: {
                strCodEsquemas: objOtraVeri.Esquemas.Codigos
            },
            success: function (response) {
                if (response == "1") {
                    GridViewOtrasVerificaciones.PerformCallback({
                        strCodEsquemas: objOtraVeri.Esquemas.Codigos
                    });
                    $("#mensaje-pla").html("");                    
                } else {
                    $("#mensaje-pla").html("*MENSAJE, no se puedo realizar la busqueda debido a que los esquemas seleccionados no comparten verificaciones.");
                    $("#mensaje-pla").css("color", "orange");
                }
            }
        });
    } else {
        $("#mensaje-pla").html("*ERROR, favor seleccionar uno o varios esquemas para realizar la busqueda.");
        $("#mensaje-pla").css("color", "red");
    }
}


objOtraVeri.Esquemas = new Object();
objOtraVeri.Esquemas.Codigos = '0';
objOtraVeri.Esquemas.Agregar = function (arrValues) {
    objOtraVeri.Esquemas.Codigos = ((arrValues.length > 0) ? arrValues.join(',') : '0');
}



function OnSelectionChanged(s, e)
{
    s.GetSelectedFieldValues('esq_codigo', objOtraVeri.Esquemas.Agregar);
}

function fnc_cbxPagosIndexChaged() {    
    gdvEsquemas.PerformCallback();
    gdvEsquemas.UnselectRows();
    objOtraVeri.Esquemas.Codigos = '0';
    objOtraVeri.Verificacion.SelectAll(false);
}