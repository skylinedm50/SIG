objManejoEsquema.Esquemas.Guardar = function ()
{
    objManejoEsquema.Esquemas.IdentifcarEsquemasSelect();
    objManejoEsquema.Esquemas.Select.sort();

    var intCodPagador = AspxComboBoxPagador.GetValue();
    var strSigno = AspxRadioBUttonSigno.GetItemValue(AspxRadioBUttonSigno.GetSelectedIndex());
    var strCodEsqSelect = objManejoEsquema.Esquemas.Select.join(',');
    var objUbicacionGeogra = fnc_obtener_valores_seleccionados();
    var strDescripcion = ((AspxMemoDescripcion.GetValue() == null) ? 'SIN DESCRIPCIÓN' : AspxMemoDescripcion.GetValue());
    var bolEstanEnlazadosBD = objManejoEsquema.Esquemas.EstanEnlazados;
    var patt = /^\s/g;


    if (objManejoEsquema.Esquemas.Select[0] == "0") {
        $("#mensaje-pla").html("*Error, no se ha selecionado ningun esquema, favor corregir.");
        $("#mensaje-pla").css("color", "red");
    } else {
        if (objUbicacionGeogra.Departamento[0] == "00" && objUbicacionGeogra.Municipio[0] == "0000" && objUbicacionGeogra.Aldea[0] == "000000" && objUbicacionGeogra.Caserio[0] == "000000000") {
            $("#mensaje-pla").html("*Error, para poder ingresar una cobertura debe de seleccionar por lo menos una ubicación geográfica, favor corregir.");
            $("#mensaje-pla").css("color", "red");
        } else {
            if (intCodPagador == null) {
                $("#mensaje-pla").html("*Error, debe seleccionar un pagador.");
                $("#mensaje-pla").css("color", "red");
            } else {
                if (patt.test(strDescripcion) == true) {
                    $("#mensaje-pla").html("*ERROR, en la descripción existe un espacio en blanco al inicio, favor eliminarlo.");
                    $("#mensaje-pla").css("color", "red");
                } else {
                    $.ajax({
                        type: 'POST',
                        url: '/Planilla/Parametros/fnc_agregar_modificar_mecani_pago',
                        data: {
                            strCodEsquema: strCodEsqSelect,
                            intCodPagador: intCodPagador,
                            strDescripcion: strDescripcion.toUpperCase(),
                            strSigno: strSigno,
                            strDepar: objUbicacionGeogra.Departamento.join(','),
                            strMun: objUbicacionGeogra.Municipio.join(','),
                            strAld: objUbicacionGeogra.Aldea.join(','),
                            strCase: objUbicacionGeogra.Caserio.join(','),
                            bolEnlazadoBD: bolEstanEnlazadosBD
                        },
                        success: function (response) {
                            switch (response) {
                                case "1":
                                    $("#mensaje-pla").html("*MENSAJE, registro almacenado correctamente.");
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
                                case "6":
                                    $("#mensaje-pla").html("*Error, imposible ejecutar la acción el esquema ya fue ejecutado y no se ingreso la descripción de cambio.");
                                    $("#mensaje-pla").css("color", "red");
                                    break;
                                case "7":
                                    $("#mensaje-pla").html("*MENSAJE, la operación se realizo correctamente, pero en la configuración existia una configuración ya almacenada por lo que no se logro guardar. En caso de requerir el cambio borre el registro e ingresolo nuevamente.");
                                    $("#mensaje-pla").css("color", "orange");
                                    break;
                            }
                            objManejoEsquema.Esquemas.IntialBuscaEsquema();
                        }
                    });
                }
            }
        }
    }

}


