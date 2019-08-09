objReportCAI = new Object();
objReportCAI.Corresponsabilidad = new Object();
objReportCAI.Pago = new Object();

objReportCAI.Corresponsabilidad.Codigos = '0';
objReportCAI.Pago.Codigos = '0';

objReportCAI.SessionData = new Object();
objReportCAI.SessionData.Corres = null;
objReportCAI.SessionData.Pago = null;
objReportCAI.SessionData.TipoBusq = null;
objReportCAI.SessionData.Año = null;

objReportCAI.SessionData.Dep = null;
objReportCAI.SessionData.Muni = null;
objReportCAI.SessionData.Ald = null;
objReportCAI.SessionData.Cas = null;

$(document).ready(function (){  
    $('#div-bloqueo-carga').css({ 'visibility': 'hidden' });
});

objReportCAI.BuscarInfo = function () {
    var bolError = null;

    AspxButtonBuscar.SetEnabled(false);

    if (AspxSpinEditAño.GetValue() == null) {
        AspxLabelError.SetText("*ERROR, favor ingresar un año por lo menos.");
        bolError = true;
    } else {
        if (AspxRadioButtonListTipBusq.GetValue() == 0) {
            if (objReportCAI.SelectCheckBox(1) !== '0') { //Verificar códigos de los pagos.
                bolError = false
            } else {
                AspxLabelError.SetText("*ERROR, el año seleccionado no contiene ningún pago asociado, intentelo con otro año.");
                bolError = true;
            }
        } else {
            if (objReportCAI.SelectCheckBox(2) !== '0') {//Verificar códigos de las corresponsabilidades.
                bolError = false;
            } else {
                AspxLabelError.SetText("*ERROR, es necesario seleccionar un o más corresponsabilidades.");
                bolError = true;
            }
        }
    }

    if (bolError == false) {
        AspxLabelError.SetText("");
        objReportCAI.SessionData.Corres = objReportCAI.SelectCheckBox(2);
        objReportCAI.SessionData.Pago = objReportCAI.SelectCheckBox(1);
        objReportCAI.SessionData.TipoBusq = AspxRadioButtonListTipBusq.GetValue();
        objReportCAI.SessionData.Año = AspxSpinEditAño.GetValue();
        objReportCAI.SessionData.Dep = ((AspxComboBoxDepartamentoSIG.GetValue() == null) ? -1 : AspxComboBoxDepartamentoSIG.GetValue());
        objReportCAI.SessionData.Muni = ((AspxComboBoxMunicipioSIG.GetValue() == null) ? -1 : AspxComboBoxMunicipioSIG.GetValue());
        objReportCAI.SessionData.Ald = ((AspxComboBoxAldeaSIG.GetValue() == null) ? -1 : AspxComboBoxAldeaSIG.GetValue());
        objReportCAI.SessionData.Cas = ((AspxComboBoxCaserioSIG.GetValue() == null) ? -1 : AspxComboBoxCaserioSIG.GetValue());

        $.ajax({
            type: 'POST',
            url: '/Corresponsabilidad/Reportes/AspxGridViewCAI',
            data: {
                intCodTipBusq: objReportCAI.SessionData.TipoBusq,
                intAño: 0,
                strCodPago: 0,
                strCodCorres: 0,
                strCodDep: '-1',
                strCodMun: '-1',
                strCodAld: '-1',
                strCodCas: '-1'
            },
            beforeSend: function () {
                $("#div-report").html("");
                $('#div-bloqueo-carga').css({ 'visibility': 'visible' });
            },
            success: function (response) {
                $('#div-bloqueo-carga').css({ 'visibility': 'hidden' });
                $("#div-report").html(response);
            },
            complete: function () {
                AspxGridViewCAI.PerformCallback();
            },
            error: function () {
                AspxLabelError.SetText("*ERROR, no se logró establecer la conexión con el servidor, favor comunicarse con el administrador del sistema.");
                $('#div-bloqueo-carga').css({ 'visibility': 'hidden' });
                $("#div-report").html('');
                AspxButtonBuscar.SetEnabled(true);
            }
        });
    } else {
        AspxButtonBuscar.SetEnabled(true);
    }
}


objReportCAI.SelectAño = function () { 
    AspxGridViewPago.PerformCallback();
    AspxGridViewCorres.PerformCallback();      

    objReportCAI.Corresponsabilidad.Codigos = '0';
    objReportCAI.Pago.Codigos = '0';
}


objReportCAI.SelectCheckBox = function (intTipCheck) {
    var arrCheckBox = null;
    var arrCodigos = new Array();

    if (intTipCheck == 1) {
        arrCheckBox = document.getElementsByName('checkBoxCodPagos');
    } else {
        arrCheckBox = document.getElementsByName('checkBoxCodCorres');
    }
    
    if (arrCheckBox.length > 0) {
        for (var i = 0; i < arrCheckBox.length; i++) {
            if (arrCheckBox[i].checked == true) {
                arrCodigos.push(arrCheckBox[i].value);
            }
        }
        return ((arrCodigos.length > 0 )? arrCodigos.join(','): 0);
    } else {
        return 0
    }
}


objReportCAI.SelectTipBusq = function () {
    AspxSpinEditAño.SetNumber(null);
    AspxGridViewPago.PerformCallback();
    AspxGridViewCorres.PerformCallback();

    AspxComboBoxDepartamentoSIG.SelectIndex(-1);
    AspxComboBoxMunicipioSIG.ClearItems();
    AspxComboBoxAldeaSIG.ClearItems();
    AspxComboBoxCaserioSIG.ClearItems();
}


