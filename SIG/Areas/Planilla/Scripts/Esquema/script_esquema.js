$(document).ready(function () {
    fnc_buscar_ultima_pag_grid();
    AspxLabelError.GetMainElement().style.color = 'Red';
});

var intCodDeleteEsquema = 0;

function fnc_tipo_operacion(s, e)
{
    $("#mensaje-pla").html("");
    var intCodEsquema = s.GetRowKey(e.visibleIndex);
    var strAccion = e.buttonID;

    fnc_limpiar_controles();

    switch (strAccion) {
        case "btnNew":
            AspxButtonCrearEsquema.SetVisible(true);
            AspxPopupControlEditEsquema.Show();
            break;
        case "btnUpdate":
            fnc_buscar_info_esquema(intCodEsquema);
            AspxPopupControlEditEsquema.Show();
            break;
        case "btnDelete":
            intCodDeleteEsquema = parseInt(intCodEsquema);
            AspxPopupConfirBorrarEsquema.Show();
            break;
    }
}

function fnc_buscar_info_esquema(intCodEsquema)
{
    $.ajax({
        type: 'POST',
        url: '/Planilla/Parametros/fnc_obtener_info_esquema',
        data: { intCodEsquema: intCodEsquema },
        success: function (response) {
            if (response.length == 0) {
                //Error
            } else {
                for (var intI = 0; intI < response.length; intI++) {
                    var arrJsonValue = response[intI];
                    
                    switch (intI) {
                        case 0:
                            AspxLabelCodEsquema.SetText(response[intI]);
                            break;
                        case 1:
                            cbxPagos.SetValue(response[intI]);
                            break;
                        case 2:
                            AspxMemoEsquema.SetText(response[intI]);
                            break;
                        case 3:
                            AspxComboxCenso.SetValue(response[intI]);
                            break;
                        case 4:
                            var dateFechaIni = new Date(response[intI]);
                            AspxComboxMesIni.SetValue(dateFechaIni.getMonth() + 1);
                            break;
                        case 5:
                            var dateFechaFin = new Date(response[intI]);
                            AspxComboxMesFin.SetValue(dateFechaFin.getMonth() + 1);
                            break;
                        case 6:
                            AspxLabelMesesCubrir.SetText(response[intI]);
                            break;
                        case 7:
                            AspxComboxIntervaTiempo.SetValue(response[intI]);
                            break;
                        case 8:
                            AspxComboxTipoPago.SetValue(response[intI]);
                            break;
                        case 9:
                            AspxDateEditFechaElegibilidad.SetDate(new Date(response[intI]));
                            break;
                        case 10:
                            AspxSpinEditAñoEsquema.SetValue(parseInt(response[intI]));
                            break;
                        case 11:                            
                            AspxLabelCantTransEsquema.SetText(response[intI]);
                            break;
                        case 12:
                            AspxLabelDetalleMeses.SetText(response[intI]);
                            break;
                        case 13:
                            AspxMemoObservacion.SetText(response[intI]);
                            break;
                        case 14:
                            $("#" + response[intI]).prop("checked", true);
                            break;
                        case 15:
                            AspxComboxTipoEsquema.SetValue(response[intI]);
                            break;
                        case 16:
                            AspxSpinEditNumero.SetValue(parseInt(response[intI]));
                            break;
                    }
                }
            }
            AspxButtonActualizarEsquema.SetVisible(true);
        }
    });
}

function fnc_obtener_ultimo_día_mes(intAño, intMes)
{
    var dateFecha = new Date(intAño , intMes, 0);
    return dateFecha.getFullYear() + '-' + intMes + '-' + dateFecha.getDate();
}

function fnc_limpiar_controles()
{
    var dateFechaActu = new Date();

    AspxLabelCodEsquema.SetText(null);
    cbxPagos.SetValue(null);
    AspxMemoEsquema.SetText(null);
    AspxComboxCenso.SetValue(null);
    AspxComboxMesIni.SetValue(null);
    AspxComboxMesFin.SetValue(null);
    AspxLabelMesesCubrir.SetText(null);
    AspxComboxIntervaTiempo.SetValue(null);
    AspxComboxTipoPago.SetValue(null);
    
    AspxLabelCantTransEsquema.SetText(null);
    AspxLabelDetalleMeses.SetText(null);
    AspxMemoObservacion.SetText(null);
    AspxComboxTipoEsquema.SetValue(null);
    AspxLabelError.SetText(null);
    AspxLabelCodEsquema.SetText(null);
    AspxSpinEditNumero.SetValue(null);
   
    AspxSpinEditAñoEsquema.SetValue(dateFechaActu.getFullYear());
    AspxDateEditFechaElegibilidad.SetDate(new Date(dateFechaActu.getFullYear(), 0, 1));
    $("input[name=radioButtonConfMonto]").attr("checked", false);

    AspxButtonActualizarEsquema.SetVisible(false);
    AspxButtonCrearEsquema.SetVisible(false);
}

function fnc_operacion_con_meses()
{
    var arrMeses = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"]
    var intMesIni = new Number(AspxComboxMesIni.GetValue());
    var intMesFin = new Number(AspxComboxMesFin.GetValue());
    var intCantMeses = new Number(0);
    var strDetMeses = new String("");



    if (intMesFin < intMesIni) {
        AspxLabelMesesCubrir.SetText("ERROR");
    } else {
        //Obtener la cantidad de meses.
        for (var i = intMesIni - 1; i < intMesFin; i++) {
            intCantMeses++;
            if (intMesIni.valueOf() == intMesFin.valueOf()) { //Obtener el detalle si solo se selecciono un mes.
                strDetMeses = arrMeses[intMesIni.valueOf() - 1];
            } else {
                if ((i + 2) == intMesFin.valueOf()) { //En caso que falte un ciclo más se asume que es el ultimo.
                    strDetMeses += arrMeses[i] + " y ";
                } else {
                    if (i == (intMesFin.valueOf() - 1)) { //En caso que el ciclo se emcuentre en su ultima vuelta.
                        strDetMeses += arrMeses[i];
                    } else { //Cuando aun faltan varias vueltas.
                        strDetMeses += arrMeses[i] + ", ";
                    }
                }
            }
        }
        AspxLabelMesesCubrir.SetText(intCantMeses);
        AspxLabelDetalleMeses.SetText(strDetMeses);
        fnc_obtener_cantidad_transferencias();
    }
}

function fnc_obtener_cantidad_transferencias()
{
    var intMesesCubrir = AspxLabelMesesCubrir.GetValue();
    var intCodIntervaloTiem = AspxComboxIntervaTiempo.GetValue();
    var intCantidadBono = new Number(0);

    if (intCodIntervaloTiem !== null) {
        $.ajax({
            type: 'POST',
            url: '/Planilla/Parametros/fnc_obtener_cantidad_meses',
            data: { intCodInterTiemp: intCodIntervaloTiem },
            success: function (response) {

                intCantidadBono = intMesesCubrir / response;

                if ((intCantidadBono % 1) == 0) {
                    AspxLabelCantTransEsquema.SetText(intCantidadBono);
                } else {
                    AspxLabelCantTransEsquema.SetText("ERROR");
                }
            }
        });
    } else {
        AspxLabelCantTransEsquema.SetText("ERROR");
    }
}

function fnc_transaccion_esquema(intCodTipTrans)
{
    var intCodEsquema =  AspxLabelCodEsquema.GetValue();
    var intCodPago = cbxPagos.GetValue();
    var strNombreEsquema = AspxMemoEsquema.GetValue();
    var strCenso = AspxComboxCenso.GetValue();
    var intMesInici = AspxComboxMesIni.GetValue();
    var intMesFin = AspxComboxMesFin.GetValue();
    var intMesesCubrir = AspxLabelMesesCubrir.GetValue();
    var intTipoInterval = AspxComboxIntervaTiempo.GetValue();
    var strTipoPago = AspxComboxTipoPago.GetValue();
    var strFechaElegibili = AspxDateEditFechaElegibilidad.GetText();
    var intAño = AspxSpinEditAñoEsquema.GetValue();
    var intCantTrans = AspxLabelCantTransEsquema.GetValue();
    var strDetalleMeses = AspxLabelDetalleMeses.GetValue();
    var strObservacion = (( AspxMemoObservacion.GetValue() == null) ? 'SIN COMENTARIOS' : AspxMemoObservacion.GetValue());
    var intCodConfMonto = $('input:radio[name=radioButtonConfMonto]:checked').val();
    var intCodTipEsquema = AspxComboxTipoEsquema.GetValue();
    var intNumEsquema = AspxSpinEditNumero.GetValue();
    var strFechaIni = null;
    var strFechaFin = null;
    var patt = /^\s/g;


    if (intCodPago == null || strNombreEsquema == null || strCenso == null || intMesInici == null || intMesFin == null || intMesesCubrir == null
        || intTipoInterval == null || strTipoPago == null || strFechaElegibili == null || intAño == null || intCantTrans == null || strDetalleMeses == null
        || intCodConfMonto == null || intCodTipEsquema == null) {
        AspxLabelError.SetText("*ERROR, campos vacios, favor correjir el error.");
    } else {
        if (patt.test(strNombreEsquema) == true) {
            AspxLabelError.SetText("*ERROR, en el nombre del esquema existe un espacio en blanco al inicio, favor eliminarlo.");
        } else {
            if (parseInt(intMesInici) > parseInt(intMesFin)) {
                AspxLabelError.SetText("*ERROR, el mes inicio no debe ser mayor al mes fin, correjir el error.");
            } else {
                if (intCantTrans == 'ERROR') {
                    AspxLabelError.SetText("*ERROR, el intervalo de tiempo seleccionado no es correcto para la cantidad de meses disponibles.");
                } else {
                    if (intCodConfMonto == null) {
                        AspxLabelError.SetText("*ERROR, favor seleccionar una configuración de montos.");
                    } else {
                        strFechaIni = intAño + '-'+ intMesInici +'-1';
                        strFechaFin = fnc_obtener_ultimo_día_mes(intAño, intMesFin);
                        switch (intCodTipTrans) {
                            case 1: //En caso que se un nuevo registro.
                                $.ajax({
                                    type: 'POST',
                                    url: '/Planilla/Parametros/fnc_ingresar_esquema',
                                    data: {
                                        intCodPago: intCodPago,
                                        strNombreEsq: strNombreEsquema,
                                        strCenso: strCenso,
                                        strFechaIni: strFechaIni,
                                        strFechaFin: strFechaFin,
                                        intMeses: intMesesCubrir,
                                        intTipInterva: intTipoInterval,
                                        strTipoPago: strTipoPago,
                                        strFechaElegibi: strFechaElegibili,
                                        intAño: intAño,
                                        intCantBono: intCantTrans,
                                        strDetMeses: strDetalleMeses,
                                        intTipEsquema: intCodTipEsquema,
                                        strObser: strObservacion,
                                        intCodCofMonto: intCodConfMonto
                                    },
                                    success: function (response) {
                                        switch (parseInt(response)) {
                                            case 0:
                                                AspxLabelError.SetText("*ERROR, no fue posible ingresar el esquema, comuniquese con el administrador del sistema.");
                                                break;
                                            case 4:
                                                AspxLabelError.SetText("*ERROR, la sesión para realizar la acción ha expirado, vuelva a ingresar al sistema e intentelo de nuevo.");
                                                break;
                                            case 1:
                                                AspxGridViewEsquema.Refresh();
                                                fnc_buscar_ultima_pag_grid();
                                                AspxPopupControlEditEsquema.Hide();
                                                break;
                                        }
                                    }
                                });
                                break;
                            case 2: //En caso que se actualize un registro.
                                if (intNumEsquema == null || strObservacion == null) {
                                    AspxLabelError.SetText("*ERROR, cuando se actualiza un registro es necesario ingresar una observación y el número del esquema.");
                                } else {
                                    if (patt.test(strObservacion) == true) {
                                        AspxLabelError.SetText("*ERROR, en la observación del esquema existe un espacio en blanco al inicio, favor eliminarlo.");
                                    } else {
                                        $.ajax({
                                            type: 'POST',
                                            url: '/Planilla/Parametros/fnc_actualizar_esquema',
                                            data: {intCodEsquema: intCodEsquema,
                                                intCodPago: intCodPago,
                                                strNombreEsq: strNombreEsquema,
                                                strCenso: strCenso,
                                                strFechaIni: strFechaIni,
                                                strFechaFin: strFechaFin,
                                                intMeses: intMesesCubrir,
                                                intTipInterva: intTipoInterval,
                                                strTipoPago: strTipoPago,
                                                strFechaElegibi: strFechaElegibili,
                                                intAño: intAño,
                                                intCantBono: intCantTrans,
                                                strDetMeses: strDetalleMeses,
                                                intTipEsquema: intCodTipEsquema,
                                                strObser: strObservacion,
                                                intNumEsque: intNumEsquema,
                                                intCodCofMonto: intCodConfMonto},
                                            success: function (response) {
                                                switch (parseInt(response)) {
                                                    case 0:
                                                        AspxLabelError.SetText("*ERROR, no fue posible actualizar el esquema, comuniquese con el administrador del sistema.");
                                                        break;
                                                    case 1:
                                                        AspxGridViewEsquema.Refresh();
                                                        fnc_buscar_ultima_pag_grid();
                                                        AspxPopupControlEditEsquema.Hide();
                                                        break;
                                                    case 2:
                                                        AspxLabelError.SetText("*ERROR, imposible actualizar el número del esquema ese número ya lo posee otro esquema del mismo pago.");
                                                        break;
                                                    case 3:
                                                        alert("*Advertencia, el esquema ya fue ejecutado por lo que las actualizaciones validas fueron el nombre y el número.");
                                                        AspxGridViewEsquema.Refresh();
                                                        fnc_buscar_ultima_pag_grid();
                                                        AspxPopupControlEditEsquema.Hide();
                                                        break;
                                                    case 4:
                                                        AspxLabelError.SetText("*ERROR, la sesión para realizar la acción ha expirado, vuelva a ingresar al sistema e intentelo de nuevo.");
                                                        break;
                                                    case 5:
                                                        AspxLabelError.SetText("*Advertencia, se detectarón cambios en las fechas o en el intervalo de tiempo del esquema y se encontró que hay registros en Transferencias Actuales por lo que antes de actualizar debe de borrar dichos registros para que el cambio en fechas del esquema sea valido. En caso que esto sea falso favor comunicarse con el administrador del sistema.");
                                                        break;
                                                }
                                            }
                                        });
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}

function fnc_buscar_ultima_pag_grid()
{
    var intNumPagi = AspxGridViewEsquema.GetPageCount();
    AspxGridViewEsquema.GotoPage(intNumPagi);
}



function fnc_eliminar_esquema() {
    $.ajax({
        type: 'POST',
        url: '/Planilla/Parametros/fnc_borrar_esquema',
        data: { intCodEsquema: intCodDeleteEsquema },
        success: function (response) {
            switch (parseInt(response)) {
                case 0:
                    $("#mensaje-pla").html("*ERROR, no fue posible borrar el esquema, comuniquese con el administrador del sistema.");
                    $("#mensaje-pla").css("color", "red");
                    break;
                case 1:
                    AspxPopupConfirBorrarEsquema.Hide();
                    AspxGridViewEsquema.Refresh();
                    fnc_buscar_ultima_pag_grid();
                    $("#mensaje-pla").html("*MENSAJE, esquema borrado satisfactoriamente.");
                    $("#mensaje-pla").css("color", "green");
                    break;
                case 2:
                    $("#mensaje-pla").html("*ERROR, imposible borrar esquema, ya se ha ejecutado en una planilla.");
                    $("#mensaje-pla").css("color", "red");
                    break;
                case 4:
                    $("#mensaje-pla").html("*ERROR, la sesion para realizar la acción ha expirado.");
                    $("#mensaje-pla").css("color", "red");
                    break;
            }
        }
    });



}

