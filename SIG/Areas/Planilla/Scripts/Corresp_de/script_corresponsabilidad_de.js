var objCorrespDe = new Object();

objCorrespDe.TipoCorres = null;
objCorrespDe.CodTransferBuscar = null;
objCorrespDe.NuevoOrden = [];

objCorrespDe.BuscarCorresp = function () {
    var intIndiceSelect = GridViewTransferencias.GetFocusedRowIndex();
    objCorrespDe.CodTransferBuscar = GridViewTransferencias.GetRowKey(intIndiceSelect);
   
    if (objCorrespDe.CodTransferBuscar !== null) {
        GridViewOrdenCorresTransfer.PerformCallback({
            intTipoCorres: objCorrespDe.TipoCorres,
            intCodTransfer: objCorrespDe.CodTransferBuscar,
            intFocusIndice: -2,
            intCodKeyGrid: 0
        });
        $("#mensaje-pla").html("");
    } else {
        $("#mensaje-pla").html("*ERROR, para realizar una busqueda seleccione una fila desplegada en las TRANSFERENCIAS.");
        $("#mensaje-pla").css("color", "red");
    }
}
objCorrespDe.ReOrdenar = function () {
    var intCantRegistros = GridViewOrdenCorresTransfer.GetVisibleRowsOnPage();
    var arrOrderPrimaryKey = [];
    var arrURL = ["/Planilla/Parametros/fnc_reordenar_corresp_transferencia", "/Planilla/Parametros/fnc_reordenar_corresp_aperc"];

    if (intCantRegistros > 1) {
        for (var  i = 0;   i < intCantRegistros; i++) {
            arrOrderPrimaryKey.push(GridViewOrdenCorresTransfer.GetRowKey(i));
        }
        objCorrespDe.NuevoOrden = arrOrderPrimaryKey;

        $.ajax({
            type: 'POST',
            url: arrURL[objCorrespDe.TipoCorres],
            data: {
                strCodBonos: objCorrespDe.NuevoOrden.join(','),
                intCodTransferencia: objCorrespDe.CodTransferBuscar
            },
            success: function (response) {
                switch (response) {
                    case "1":
                        $("#mensaje-pla").html("*MENSAJE, la operación se realizó correctamente.");
                        $("#mensaje-pla").css("color", "green");
                        GridViewOrdenCorresTransfer.PerformCallback({
                            intTipoCorres: objCorrespDe.TipoCorres,
                            intCodTransfer: objCorrespDe.CodTransferBuscar,
                            intFocusIndice: -2,
                            intCodKeyGrid: 0
                        });
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
                        $("#mensaje-pla").html("*Error, imposible efectuar la acción el esquema ya fue ejecutado.");
                        $("#mensaje-pla").css("color", "red");
                        break;
                }
            }
        });
    } else {
        $("#mensaje-pla").html("*ERROR, no existe la suficiente cantidad de registros para realizar la reordenación de los datos.");
        $("#mensaje-pla").css("color", "red");
    }
}
objCorrespDe.Mover = function (intTipoMove) { // 1 Arriba y 2 Abajo
    var intIndiceSelect = GridViewOrdenCorresTransfer.GetFocusedRowIndex();
    var intNewPosicion = ((intTipoMove == 1) ? (intIndiceSelect + 1) - 1 : intIndiceSelect + 2);
    var intCodRowKey = GridViewOrdenCorresTransfer.GetRowKey(intIndiceSelect);
    var intCantRegistros = GridViewOrdenCorresTransfer.GetVisibleRowsOnPage();

    if (intNewPosicion > 0 && intNewPosicion <= intCantRegistros) {
        GridViewOrdenCorresTransfer.PerformCallback({
            intTipoCorres: objCorrespDe.TipoCorres,
            intCodTransfer: objCorrespDe.CodTransferBuscar,
            intFocusIndice: intNewPosicion,
            intCodKeyGrid: intCodRowKey
        });
    }
}
objCorrespDe.Agregar = function () {
    var arrURL = ["/Planilla/Parametros/fnc_agregar_corresp_transferencia", "/Planilla/Parametros/fnc_agregar_corresp_aperc"];
    var intCodCorres = AspxComboBoxCorresponsabilidad.GetValue();
    var intCodComp = AspxComboBoxComponente.GetValue();
    var intIndiceSelect = GridViewTransferencias.GetFocusedRowIndex();
    objCorrespDe.CodTransferBuscar = GridViewTransferencias.GetRowKey(intIndiceSelect);

    if (objCorrespDe.Transferencia.Codigos !== "No" && intCodCorres !== null) {
        if (objCorrespDe.CodTransferBuscar !== null) {
            $.ajax({
                type: 'POST',
                url: arrURL[objCorrespDe.TipoCorres],
                data: {
                    strCodTarnsferencias: objCorrespDe.Transferencia.Codigos,
                    intCodCorresp: intCodCorres,
                    intCodComponente: intCodComp
                },
                success: function (response) {
                    switch (response) {
                        case "1":
                            $("#mensaje-pla").html("*MENSAJE, la operación se realizó correctamente.");
                            $("#mensaje-pla").css("color", "green");
                            GridViewOrdenCorresTransfer.PerformCallback({
                                intTipoCorres: objCorrespDe.TipoCorres,
                                intCodTransfer: objCorrespDe.CodTransferBuscar,
                                intFocusIndice: -2,
                                intCodKeyGrid: 0
                            });
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
                            $("#mensaje-pla").html("*Error, imposible efectuar la acción uno o todos de los esquema ya fue ejecutado, favor seleccionar esquemas que no esten ejecutados.");
                            $("#mensaje-pla").css("color", "red");
                            break;
                        case "3":
                            $("#mensaje-pla").html("*Error, imposible efectuar la acción en alguna de las transferencias ya existe la corresponsabilidad asegurese de agregar solo corresponsabilidades que no existan en las transferencias.");
                            $("#mensaje-pla").css("color", "red");
                            break;
                    }
                }
            });
        } else {
            $("#mensaje-pla").html("*ERROR, para realizar la acción seleccione una fila desplegada en las TRANSFERENCIAS.");
            $("#mensaje-pla").css("color", "red");
        }
    } else {
        $("#mensaje-pla").html("*Error, favor seleccionar una o varias transferencias con el control ubicado en la columna AGREGAR CORRESPONSABILIDAD y seleccione una corresponsabilidad.");
        $("#mensaje-pla").css("color", "red");
    }
}
objCorrespDe.Borrar = function (intCodBono) {
    var arrURL = ["/Planilla/Parametros/fnc_borrar_corresp_transferencia", "/Planilla/Parametros/fnc_borrar_corresp_aperc"];
    $.ajax({
        type: 'POST',
        url: arrURL[objCorrespDe.TipoCorres],
        data: {
            intCodBono: intCodBono,
            intCodTransferencia: objCorrespDe.CodTransferBuscar
        },
        success: function (response) {
            switch (response) {
                case "1":
                    $("#mensaje-pla").html("*MENSAJE, registro borrado correctamente.");
                    $("#mensaje-pla").css("color", "green");
                    GridViewOrdenCorresTransfer.PerformCallback({
                        intTipoCorres: objCorrespDe.TipoCorres,
                        intCodTransfer: objCorrespDe.CodTransferBuscar,
                        intFocusIndice: -2,
                        intCodKeyGrid: 0
                    });
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
                    $("#mensaje-pla").html("*Error, imposible efectuar la acción el esquema ya fue ejecutado.");
                    $("#mensaje-pla").css("color", "red");
                    break;
            }
        }
    });

}

objCorrespDe.Transferencia = new Object();

objCorrespDe.Transferencia.Codigos = "No";
objCorrespDe.Transferencia.Obtener = function () {
    objCorrespDe.Transferencia.Codigos = "No";
    GridViewTransferencias.PerformCallback();
}
objCorrespDe.Transferencia.Agregar = function () {
    var objCkeckBox = document.getElementsByName("checkBoxBono");
    var arrSelect = [];

    for (var i = objCkeckBox.length - 1; i >= 0; i--) {
        if (objCkeckBox[i].checked == true) {
            arrSelect.push(objCkeckBox[i].value);
        }
    }

    objCorrespDe.Transferencia.Codigos = ((arrSelect.length > 0) ? arrSelect.join(',') : "No");
}




function fnc_cbxPagosIndexChaged() {
    try {
        $("#mensaje-pla").html("");
        cbxEsquemas.PerformCallback();
        objCorrespDe.CodTransferBuscar = null;

        objCorrespDe.Transferencia.Codigos = "No";
        objCorrespDe.Transferencia.Obtener();
        objCorrespDe.Transferencia.Agregar();

        GridViewOrdenCorresTransfer.PerformCallback({
            intTipoCorres: objCorrespDe.TipoCorres,
            intCodTransfer: -1,
            intFocusIndice: -2,
            intCodKeyGrid: 0
        });
    } catch (e) {
        console.log('No existe el ComboBox de los esquemas');
    }
}

function fnc_cbxEsquemasIndexChanged() {
    $("#mensaje-pla").html("");
    objCorrespDe.Transferencia.Codigos = "No";
    objCorrespDe.Transferencia.Obtener();
    objCorrespDe.Transferencia.Agregar();

    GridViewOrdenCorresTransfer.PerformCallback({
        intTipoCorres: objCorrespDe.TipoCorres,
        intCodTransfer: -1,
        intFocusIndice: -2,
        intCodKeyGrid: 0
    });
}