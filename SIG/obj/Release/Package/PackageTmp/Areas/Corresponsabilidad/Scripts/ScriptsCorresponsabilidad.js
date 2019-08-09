$(document).ready(function (){
    $("#AspxGridViewDetalleCorresponsabilidad_col0").find("tr").html("<div id='divNueRegisCorres'>Nuevo Registro</div>").css({ "color": "#1b3f91" });
    $('#divNueRegisCorres').click(function () {
        objCorrespo.ResetControles(true);
        AspxPopupControlEditCorrespo.Show();
        $("#divCrearCorres").show();
        $("#divActualiCorres").hide();
    });
});

var objCorrespo = new Object();

objCorrespo.intCodCorres = new Number(null);
objCorrespo.intTipCorre = new Number(null);
objCorrespo.intAño = new Number(null);
objCorrespo.intParciIni = new Number(null);
objCorrespo.intParciFin = new Number(null);
objCorrespo.dateFechIni = new Date(null);
objCorrespo.dateFechFin = new Date(null);

objCorrespo.ResetControles = function (boolClearTipoCorr) {
    var objDate = new Date();

    if (boolClearTipoCorr == true) {
        AspxComboxTipoCorrespo.SetSelectedIndex(-1);
    }

    AspxSpinEditAñoCorrespo.SetEnabled(false);
    AspxDateEditFechaInicio.SetEnabled(false);
    AspxDateEditFechaFin.SetEnabled(false);
    AspxSpinEditParciInicio.SetEnabled(false);
    AspxSpinEditParciFin.SetEnabled(false);

    AspxSpinEditAñoCorrespo.SetValue(objDate.getFullYear());
    AspxDateEditFechaInicio.SetValue(null);
    AspxDateEditFechaFin.SetValue(null);
    AspxSpinEditParciInicio.SetValue(null);
    AspxSpinEditParciFin.SetValue(null);
    AspxLabelError.SetText('');
    AspxLabelCodCorrres.SetText('0');
}

objCorrespo.SelectTipoCorres = function (intCodTipo) {
    objCorrespo.ResetControles(false);
    switch (intCodTipo) {
        case 1: //Matricula.
            AspxSpinEditAñoCorrespo.SetEnabled(true);
            break;
        case 2: //Asistencia.
            AspxSpinEditAñoCorrespo.SetEnabled(true);
            AspxSpinEditParciInicio.SetEnabled(true);
            AspxSpinEditParciFin.SetEnabled(true);
            break;
        case 3://Visita médica.
            AspxDateEditFechaInicio.SetEnabled(true);
            AspxDateEditFechaFin.SetEnabled(true);
            break;
    }
}


objCorrespo.Operaciones = new Object();

objCorrespo.Operaciones.Main = function (s, e) {
    var intCodCorres = s.GetRowKey(e.visibleIndex);
    var strAccion = e.buttonID;
    objCorrespo.intCodCorres = new Number(intCodCorres);

    switch (strAccion) {
        case "btnUpdate":
            objCorrespo.ResetControles(true);
            if (objCorrespo.intCodCorres.valueOf() > 0) {
                objCorrespo.Operaciones.ObtenerInfoCorres();
                AspxPopupControlEditCorrespo.Show();
                $("#divCrearCorres").hide();
                $("#divActualiCorres").show();
            } else {
                console.log('No se encontro la llave primaria.');
            }
            break;
        case "btnDelete":
            if (objCorrespo.intCodCorres > 0) {
                AspxLabelMensaBorr.SetText("")
                AspxPopupControlBorrarCorrespo.Show();
            } else {
                console.log('No se encontro la llave primaria.');
            }
            break;
    }
}




objCorrespo.Operaciones.AgregarActualizar = function (intTipoOperacion) {
    objCorrespo.Operaciones.ObtenerValActuales(((intTipoOperacion == 2)? true : false));

    if(objCorrespo.Operaciones.ValidarCamposCorrec() == true){
        $.ajax({
            type: 'POST',
            url: '/Corresponsabilidad/Configuracion/fnc_operacion_corres',
            data: {
                cod_det_cor: objCorrespo.intCodCorres.valueOf(),
                cod_tip_cor: objCorrespo.intTipCorre.valueOf(),
                num_det_cor: 0,
                año_det_cor: objCorrespo.intAño.valueOf(),
                par_ini_det_cor: objCorrespo.intParciIni.valueOf(),
                par_fin_det_cor: objCorrespo.intParciFin.valueOf(),
                fec_ini_det_cor: ((objCorrespo.dateFechIni.getFullYear() == 1969) ? '01/01/1900' : objCorrespo.dateFechIni.toLocaleDateString()),
                fec_fin_det_cor: ((objCorrespo.dateFechFin.getFullYear() == 1969) ? '01/01/1900' : objCorrespo.dateFechFin.toLocaleDateString()),
                intCodOper: intTipoOperacion
            },
            success: function (response) {
                switch (parseInt(response)) {
                    case 0:
                        AspxLabelError.SetText("*ERROR, no se logró realizar la acción, comunicarse con el encargado del sistema.");
                        break;
                    case 1:
                        AspxGridViewDetalleCorresponsabilidad.PerformCallback();
                        AspxPopupControlEditCorrespo.Hide();
                        break;
                    case 2:
                        AspxLabelError.SetText("*ERROR, ya existe una corresponsabilidad con esa información.");
                        break;
                    case 3:
                        AspxLabelError.SetText("*ERROR, la sesión a expirado, favor volver a identificarse como un usuario de este sistema.");
                        break;
                    case 4:
                        AspxLabelError.SetText("*ERROR, imposible realizar la acción la corresponsabilidad posee datos relacionados con el módulo de planilla.");
                        break;
                }
            }
        });
    }
}

objCorrespo.Operaciones.Borrar = function () {
    $.ajax({
        type: 'POST',
        url: '/Corresponsabilidad/Configuracion/fnc_borrar_corres',
        data: {
            cod_det_cor: objCorrespo.intCodCorres.valueOf()
        },
        success: function (response) {
            switch (parseInt(response)) {
                case 0:
                    AspxLabelMensaBorr.SetText("*ERROR, no se logró realizar la acción, comunicarse con el encargado del sistema.");
                    break;
                case 1:
                    AspxGridViewDetalleCorresponsabilidad.PerformCallback();
                    AspxPopupControlBorrarCorrespo.Hide();
                    break;
                case 2:
                    AspxLabelMensaBorr.SetText("*ERROR, la sesión a expirado, favor volver a identificarse como un usuario de este sistema.");
                    break;
                case 3:
                    AspxLabelMensaBorr.SetText("*ERROR, imposible realizar la acción la corresponsabilidad posee datos relacionados con el módulo de planilla.");
                    break;
            }
        }
    });
}

objCorrespo.Operaciones.ObtenerValActuales = function (boolActulizacion) {
    objCorrespo.intCodCorres = ((boolActulizacion == true )? objCorrespo.intCodCorres : new Number(AspxLabelCodCorrres.GetText()));
    objCorrespo.intTipCorre = new Number(AspxComboxTipoCorrespo.GetValue());
    objCorrespo.intAño = new Number(AspxSpinEditAñoCorrespo.GetNumber());
    objCorrespo.intParciIni = new Number(AspxSpinEditParciInicio.GetNumber());
    objCorrespo.intParciFin = new Number(AspxSpinEditParciFin.GetNumber());
    objCorrespo.dateFechIni = new Date(AspxDateEditFechaInicio.GetDate());
    objCorrespo.dateFechFin = new Date(AspxDateEditFechaFin.GetDate());
}

objCorrespo.Operaciones.ValidarCamposCorrec = function () {
    if (objCorrespo.intTipCorre.valueOf() == 0) //Validar el tipo de corresponsabilidad.
    {
        AspxLabelError.SetText("*ERROR, favor seleccionar el tipo de corresponsabilidad.");
        return false;
    } else {
        switch (objCorrespo.intTipCorre.valueOf()) {
            case 1: //Tipo matrícula.
                if (objCorrespo.intAño.valueOf() == 0) {
                    AspxLabelError.SetText("*ERROR, favor ingresar el año.");
                    return false;
                } else {
                    return true;
                }
                break;
            case 2://Tipo asistencia.
                return objCorrespo.Operaciones.ValidarParciales();
                break;
            case 3: //Tipo visitas medicas
                return objCorrespo.Operaciones.ValidarFechas();
                break;
            default:
                console.log("*ERROR, tipo de corresponsabilidad no soportado por el sistema.");
                break;
        }
    }
}

objCorrespo.Operaciones.ValidarParciales = function(){
    if ((objCorrespo.intParciFin.valueOf() !== 0 && objCorrespo.intParciFin.valueOf() < objCorrespo.intParciIni.valueOf()) || (objCorrespo.intParciFin.valueOf() !== 0 && objCorrespo.intParciIni.valueOf() == 0)) {
            AspxLabelError.SetText("*ERROR, sí se ingresa una parcial fin no debe ser menor que el parcial inicio y el parcial inicio no debe estar vacio.");
            return false;
    } else if (objCorrespo.intParciIni.valueOf() !== 0 && objCorrespo.intParciFin.valueOf() !== 0 && objCorrespo.intParciIni.valueOf() == objCorrespo.intParciFin.valueOf()) {
            AspxLabelError.SetText("*ERROR, el valor del parcial inicio no debe de ser igual al parcial fin. Si no es necesario no ingrese ningun valor en el parcial fin.");
            return false;
    } else if (objCorrespo.intParciIni.valueOf() == 0 || objCorrespo.intAño.valueOf() == 0) {
        AspxLabelError.SetText("*ERROR, debido al tipo de corresponsabilidad es necesario el año y por lo menos el parcial inicio.");
        return false;
    } else {
        return true;
    }
}

objCorrespo.Operaciones.ValidarFechas = function () {
    if (objCorrespo.dateFechFin.getFullYear() == 1969 || objCorrespo.dateFechIni.getFullYear() == 1969) {
        AspxLabelError.SetText("*ERROR, favor ingresar ambas fechas.");
        return false;
    } else {
        if (objCorrespo.dateFechIni > objCorrespo.dateFechFin) {
            AspxLabelError.SetText("*ERROR, la fecha inicio es mayor que la fecha fin.");
            return false;
        } else {
            return true;
        }
    }
}

objCorrespo.Operaciones.ObtenerInfoCorres = function () {
    $.ajax({
        type: 'POST',
        url: '/Corresponsabilidad/Configuracion/fnc_obtener_infor_det_corr',
        data: {
            intCodDetCorr: objCorrespo.intCodCorres.valueOf()
        },
        success: function (response) {
            var objInfoDetCorr = response[0];

            AspxComboxTipoCorrespo.SetValue(objInfoDetCorr.cod_tip_cor);
            objCorrespo.SelectTipoCorres(objInfoDetCorr.cod_tip_cor);

            switch (objInfoDetCorr.cod_tip_cor) {
                case 1:                    
                    AspxSpinEditAñoCorrespo.SetValue(objInfoDetCorr.año_det_cor);
                    break;
                case 2:
                    AspxSpinEditAñoCorrespo.SetValue(objInfoDetCorr.año_det_cor);
                    AspxSpinEditParciInicio.SetValue(objInfoDetCorr.par_ini_det_cor);
                    AspxSpinEditParciFin.SetValue(objInfoDetCorr.par_fin_det_cor);
                    break;
                case 3:                    
                    objInfoDetCorr.fec_ini_det_cor = objInfoDetCorr.fec_ini_det_cor.replace('/Date(', '');
                    objInfoDetCorr.fec_ini_det_cor = objInfoDetCorr.fec_ini_det_cor.replace(')/', '');

                    objInfoDetCorr.fec_fin_det_cor = objInfoDetCorr.fec_fin_det_cor.replace('/Date(', '');
                    objInfoDetCorr.fec_fin_det_cor = objInfoDetCorr.fec_fin_det_cor.replace(')/', '');

                    AspxDateEditFechaInicio.SetDate(new Date(parseInt(objInfoDetCorr.fec_ini_det_cor)));
                    AspxDateEditFechaFin.SetDate(new Date(parseInt(objInfoDetCorr.fec_fin_det_cor)));
                    break;
            }
        }
    });
}
