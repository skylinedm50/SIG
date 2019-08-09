$(document).ready(function () {
    AspxListBoxEsquemasSelect.AddItem("Sin datos para mostrar.", 0);

    if (typeof AspxGridViewCargas !== "undefined") {
        objManejoEsquema.Esquemas.TipoEnlace = 0;
    } else if (typeof AspxComboBoxFondo !== "undefined") {
        objManejoEsquema.Esquemas.TipoEnlace = 1;
    } else if (typeof AspxComboBoxPagador !== "undefined") {
        objManejoEsquema.Esquemas.TipoEnlace = 2;
    }
});

var objManejoEsquema = new Object();
var patt = /^\s/g;

objManejoEsquema.Planillas = new Object(); //Propieadad para manejar los códigos de las planillas.
objManejoEsquema.Planillas.HaSalido = [];
objManejoEsquema.Planillas.NoHaSalido = [];
objManejoEsquema.Planillas.ManejoRadioButton = function (objRadioButton) {
    if (objRadioButton.value == '1') {
        if (objRadioButton.checked == true) {
            objManejoEsquema.Planillas.HaSalido.push(objRadioButton.id);

            for (var i = 0; i < objManejoEsquema.Planillas.NoHaSalido.length; i++) {
                if (objManejoEsquema.Planillas.NoHaSalido[i] == objRadioButton.id) {
                    objManejoEsquema.Planillas.NoHaSalido.splice(i, 1);
                    break;
                }
            }
        }
    } else {
        if (objRadioButton.checked == true) {
            objManejoEsquema.Planillas.NoHaSalido.push(objRadioButton.id);
            for (var i = 0; i < objManejoEsquema.Planillas.HaSalido.length; i++) {
                if (objManejoEsquema.Planillas.HaSalido[i] == objRadioButton.id) {
                    objManejoEsquema.Planillas.HaSalido.splice(i, 1);
                    break;
                }
            }
        }
    }
};
objManejoEsquema.Planillas.Checked = function () {
    var strParteNombre = "radioHasalido_";

    for (var i = 0; i < objManejoEsquema.Planillas.HaSalido.length; i++) {
        var objRadioButtom = document.getElementsByName(strParteNombre + objManejoEsquema.Planillas.HaSalido[i]);
        if (objRadioButtom.length > 0) {
            objRadioButtom[0].checked = true;
        }
    }
    for (var i = 0; i < objManejoEsquema.Planillas.NoHaSalido.length; i++) {
        var objRadioButtom = document.getElementsByName(strParteNombre + objManejoEsquema.Planillas.NoHaSalido[i]);
        if (objRadioButtom.length > 0) {
            objRadioButtom[1].checked = true;
        }
    }
};

objManejoEsquema.Cargas = new Object(); //Propiedad para manejar los código de las cargas.
objManejoEsquema.Cargas.Codigos = "No";
objManejoEsquema.Cargas.Agregar = function (arrValues) {
    objManejoEsquema.Cargas.Codigos = ((arrValues.length > 0) ? arrValues.join(',') : "No");
};

objManejoEsquema.Esquemas = new Object();
objManejoEsquema.Esquemas.LastCheckBoxSelect = new Object();
objManejoEsquema.Esquemas.LastEstadoCheckBoxDesplegados = [];
objManejoEsquema.Esquemas.NombresSelect = ["Sin datos para mostrar."];
objManejoEsquema.Esquemas.Select = ["0"]; //Arreglo que contiene los esquemas selecionados.
objManejoEsquema.Esquemas.Enlazados = []; //Arreglo que contiene los esquemas enlazados proveninetes de la base de datos.
objManejoEsquema.Esquemas.EstanEnlazados = false; //Propiedad que determina si el esquema actual selecionado esta enlazado con otros.
objManejoEsquema.Esquemas.RegistroUnico = false; //Indica si el esquema ya se encuentra en la base datos pero no esta enlazado.
objManejoEsquema.Esquemas.TipoEnlace = null; //Indica el tipo de formulario que se ha desplegado.
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
    objManejoEsquema.Esquemas.NombresSelect = ((arrNameSelect.length > 0) ? arrNameSelect : ["Sin datos para mostrar."]);
}
objManejoEsquema.Esquemas.IntialBuscaEsquema = function () {
    objManejoEsquema.Esquemas.IdentifcarEsquemasSelect();

    if (objManejoEsquema.Esquemas.Select[0] !== "0") {
        AspxGridViewDetalleEsquemaEnlazadoLocalizacion.PerformCallback(); //Forzamos a realizar el BEGINCALLBACK del gridview.
        AspxListBoxEsquemasSelect.ClearItems(); //Limpiamos lo que este seleccionado.

        if (objManejoEsquema.Esquemas.NombresSelect.length > 0) {
            for (var i = 0; i < objManejoEsquema.Esquemas.NombresSelect.length; i++) {
                AspxListBoxEsquemasSelect.AddItem(objManejoEsquema.Esquemas.NombresSelect[i], 0);
            }
        } else {
            AspxListBoxEsquemasSelect.AddItem("Sin datos para mostrar.", 0);
        }
    } else {
        $("#mensaje-pla").html("*ERROR, para realizar una busqueda favor seleccionar un o varios esquemas esto dependerá si estan enlazados o no.");
        $("#mensaje-pla").css("color", "red");
    }
}
objManejoEsquema.Esquemas.Borrar = function (s, e) {
    var strDescripcion = ((AspxMemoDescripcion.GetValue() == null) ? 'SIN DESCRIPCIÓN' : AspxMemoDescripcion.GetValue());
    if (patt.test(strDescripcion) == true) {
        window.scrollTo(0, 0);
        $("#mensaje-pla").html("*ERROR, en la descripción existe un espacio en blanco al inicio, favor eliminarlo.");
        $("#mensaje-pla").css("color", "red");
    }
    else {
        $.ajax({
            type: 'POST',
            url: '/Planilla/Shared/fnc_borrar_localizacion_esquema_enlazado',
            data: { intCodPrincipal: s.GetRowKey(e.visibleIndex), intTipoEnlace: objManejoEsquema.Esquemas.TipoEnlace, strDescripcion: strDescripcion },
            success: function (response) {
                switch (response) {
                    case "1":
                        $("#mensaje-pla").html("*MENSAJE, la localización fue borrada correctamente.");
                        $("#mensaje-pla").css("color", "green");
                        break;
                    case "0":
                        window.scrollTo(0, 0);
                        $("#mensaje-pla").html("*Error, no se pudo efectuar la acción favor comunicarse con el administrador del sistema.");
                        $("#mensaje-pla").css("color", "red");
                        break;
                    case "4":
                        window.scrollTo(0, 0);
                        $("#mensaje-pla").html("*Error, la sessión expiro, el usuario no pertenece al sistema favor vuelva a ingresar al sistema e intentelo nuevamente.");
                        $("#mensaje-pla").css("color", "red");
                        break;
                    case "5":
                        window.scrollTo(0, 0);
                        switch (objManejoEsquema.Esquemas.TipoEnlace) {
                            case 0:
                                $("#mensaje-pla").html("*Error, imposible ejecutar la acción el esquema ya fue ejecutado.");
                                break;
                            default:
                                $("#mensaje-pla").html("*Error, el esquema ya fue ejecutado y es necesario si desea realizar la acción ingresar una DESCRIPCIÓN.");
                                break;
                        }
                        $("#mensaje-pla").css("color", "red");
                        break;
                }
                objManejoEsquema.Esquemas.IntialBuscaEsquema();
            }
        });
    }
}
objManejoEsquema.Esquemas.LimpiarCheckBoxEsquemaByPago = function () {
    for (var i = 0; i < document.getElementsByName('checkBoxEsqueByPago').length; i++) {
        document.getElementsByName('checkBoxEsqueByPago')[i].checked = false;
    }
}
objManejoEsquema.Esquemas.SelectEsquemaByBuscar = function (objCheckBoxSelect) {
    objManejoEsquema.Esquemas.LastCheckBoxSelect = objCheckBoxSelect;
    objManejoEsquema.Esquemas.LastEstadoCheckBoxDesplegados = [];

    for (var i = 0; i < document.getElementsByName("checkBoxEsqueByPago").length; i++) {//Identificamos los esquemas seleccionados
        if (document.getElementsByName("checkBoxEsqueByPago")[i].checked == true) {
            objManejoEsquema.Esquemas.LastEstadoCheckBoxDesplegados.push(document.getElementsByName("checkBoxEsqueByPago")[i].id)
        }
    }

    if (objCheckBoxSelect.checked == true) {
        $.ajax({
            type: 'POST',
            url: '/Planilla/Shared/fnc_obtener_esquemas_enlazados',
            data: {
                intCodEsquema: objManejoEsquema.Esquemas.LastCheckBoxSelect.id,
                intCodTipoEnlace: objManejoEsquema.Esquemas.TipoEnlace
            },
            success: function (response) {
                objManejoEsquema.Esquemas.LimpiarCheckBoxEsquemaByPago();

                if (response !== "0") {
                    var arrCodigos = response.split(" ");
                    objManejoEsquema.Esquemas.EstanEnlazados = true;
                    objManejoEsquema.Esquemas.RegistroUnico = false;

                    for (var i = 0; i < document.getElementsByName("checkBoxEsqueByPago").length; i++) {
                        for (var j = 0; j < arrCodigos.length; j++) {
                            if (arrCodigos[j] == document.getElementsByName("checkBoxEsqueByPago")[i].id) {
                                document.getElementsByName("checkBoxEsqueByPago")[i].checked = true;
                                break;
                            }
                        }
                    }
                } else {
                    objManejoEsquema.Esquemas.EstanEnlazados = false;

                    $.ajax({
                        type: 'POST',
                        url: '/Planilla/Shared/fnc_existe_registro_esquema',
                        data: { intCodEsquema: objManejoEsquema.Esquemas.LastCheckBoxSelect.id, intCodTipoEnlace: objManejoEsquema.Esquemas.TipoEnlace },
                        success: function (response) {
                            if (response == "1") {
                                objManejoEsquema.Esquemas.LastCheckBoxSelect.checked = true;
                                objManejoEsquema.Esquemas.RegistroUnico = true;
                            } else {
                                if (objManejoEsquema.Esquemas.RegistroUnico == false) {
                                    for (var i = 0; i < document.getElementsByName("checkBoxEsqueByPago").length; i++) {
                                        for (var j = 0; j < objManejoEsquema.Esquemas.LastEstadoCheckBoxDesplegados.length; j++) {
                                            if (objManejoEsquema.Esquemas.LastEstadoCheckBoxDesplegados[j] == document.getElementsByName("checkBoxEsqueByPago")[i].id) {
                                                document.getElementsByName("checkBoxEsqueByPago")[i].checked = true;
                                            }
                                        }
                                    }
                                } else {
                                    objManejoEsquema.Esquemas.LastCheckBoxSelect.checked = true;
                                    objManejoEsquema.Esquemas.RegistroUnico = false;
                                }
                            }
                        }
                    });
                }
            }
        });
    } else {
        if (objManejoEsquema.Esquemas.EstanEnlazados == true && objManejoEsquema.Esquemas.RegistroUnico == false) {
            AspxPopupDesEnlazarEsquema.Show();
        }
    }
}
objManejoEsquema.Esquemas.Desenlazar = function () {
    $.ajax({
        type: 'POST',
        url: '/Planilla/Shared/fnc_desenlzar_esquema',
        data: { intCodEsquema: objManejoEsquema.Esquemas.LastCheckBoxSelect.id, intCodTipoEnlace: objManejoEsquema.Esquemas.TipoEnlace },
        success: function (response) {
            switch (response) {
                case "1":
                    $("#mensaje-pla").html("*MENSAJE, el esquema se desenlazo correctamente.");
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
                case "5":
                    $("#mensaje-pla").html("*Error, imposible efectuar la acción el esquema ya fue ejecutado.");
                    $("#mensaje-pla").css("color", "red");
                    break;
            }
            AspxPopupDesEnlazarEsquema.Hide();
        }
    });
}

objManejoEsquema.Esquemas.Guardar = function () { }


function fnc_cbxPagosIndexChaged() {
    try {
        GridViewEsquemasPorPago.PerformCallback();
        AspxListBoxEsquemasSelect.ClearItems();
        objManejoEsquema.Esquemas.Select = ["0"];
        AspxGridViewDetalleEsquemaEnlazadoLocalizacion.PerformCallback();
    } catch (e) {
        console.log('No existe el gridview de los esquemas');
    }
}