var objManejoEsquema = new Object();

objManejoEsquema.Esquemas = new Object();
objManejoEsquema.Esquemas.Select = ["0"];
objManejoEsquema.Esquemas.IdentifcarEsquemasSelect = function () {
    var objCheckBox = document.getElementsByName("checkBoxEsqueByPago");
    var arrSelect = [];
    var arrNameSelect = [];

    for (var i = objCheckBox.length - 1; i >= 0; i--) {
        if (objCheckBox[i].checked == true) {
            arrSelect.push(objCheckBox[i].id);
            arrNameSelect.push(objCheckBox[i].value);
        }
    }

    objManejoEsquema.Esquemas.Select = ((arrSelect.length > 0) ? arrSelect : ["0"]);
}
objManejoEsquema.Esquemas.IntialBuscaEsquema = function () {
    objManejoEsquema.Esquemas.IdentifcarEsquemasSelect();
    AspxGridViewDetalleTransfeActual.PerformCallback(); //Forzamos a realizar el BEGINCALLBACK del gridview.
}
objManejoEsquema.Esquemas.SelectEsquemaByBuscar = function (objCheckBoxSelect) {
    
}

objManejoEsquema.Esquemas.Generar = function () {
    objManejoEsquema.Esquemas.IdentifcarEsquemasSelect();
    objManejoEsquema.Esquemas.Select.sort();
    strCodEsqSelect = objManejoEsquema.Esquemas.Select.join(',');

    if (strCodEsqSelect !== "0") {
        $.ajax({
            type: 'POST',
            url: '/Planilla/Parametros/fnc_generar_bonos',
            data: { strCodEsquemas: strCodEsqSelect },
            success: function (response) {
                switch (response) {
                    case "1":
                        $("#mensaje-pla").html("*MENSAJE, se generaron correctamente los bonos de los esquemas seleccionados.");
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
                        $("#mensaje-pla").html("*MENSAJE, no a todos los esquemas se les generaron los bono debido a que en algunos caso ya se le habian generado, favor verificar.");
                        $("#mensaje-pla").css("color", "orange");
                        break;
                    case "2":
                        $("#mensaje-pla").html("*MENSAJE, a ninguno de los esquemas seleccionados se le generaron los bonos.");
                        $("#mensaje-pla").css("color", "orange");
                        break;
                    case "6":
                        $("#mensaje-pla").html("*MENSAJE, se generaron algunos bonos pero ocurrio un error y no se completo, si esto continua favor comunicarse con el administrador del sistema.");
                        $("#mensaje-pla").css("color", "orange");
                        break;
                    case "5":
                        $("#mensaje-pla").html("*ERROR, imposible realizar la acción uno o varios esquemas seleccionados ya se han generado.");
                        $("#mensaje-pla").css("color", "red");
                        break;
                }
                objManejoEsquema.Esquemas.IntialBuscaEsquema();
            }
        });
    } else {
        $("#mensaje-pla").html("*ERROR, imposible realizar la acción favor seleccionar por lo menos un esquema.");
        $("#mensaje-pla").css("color", "red");
    }
}
objManejoEsquema.Esquemas.BorrarBono = function (intCodBono) {
    $.ajax({
        type: 'POST',
        url: '/Planilla/Parametros/fnc_borrar_bonos',
        data: { intCodBono: intCodBono },
        success: function (response) {
            switch (response) {
                case "1":
                    $("#mensaje-pla").html("*MENSAJE, el o los bonos fuerón eliminados correctamente.");
                    $("#mensaje-pla").css("color", "green");
                    objManejoEsquema.Esquemas.IntialBuscaEsquema();
                    break;
                case "0":
                    $("#mensaje-pla").html("*Error, no se pudo efectuar la acción favor comunicarse con el administrador del sistema.");
                    $("#mensaje-pla").css("color", "red");
                    break;
                case "4":
                    $("#mensaje-pla").html("*Error, la sessión expiro, el usuario no pertenece al sistema favor vuelva a ingresar al sistema e intentelo nuevamente.");
                    $("#mensaje-pla").css("color", "red");
                    break;
                case "2":
                    $("#mensaje-pla").html("*MENSAJE, no se puede realizar la acción debido a que el esquema ya ha sido generado.");
                    $("#mensaje-pla").css("color", "orange");
                    break;
            }
        }
    });
}

function fnc_cbxPagosIndexChaged() {
    try {
        GridViewEsquemasPorPago.PerformCallback();
        AspxGridViewDetalleTransfeActual.PerformCallback();
    } catch (e) {
        console.log('No existe el gridview de los esquemas');
    }
}

