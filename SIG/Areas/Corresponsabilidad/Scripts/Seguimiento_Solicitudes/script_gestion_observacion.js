objGestionSegumiento = new Object();
objGestionSegumiento.intCodSolicitud = null;
objGestionSegumiento.strObservacion = null;
objGestionSegumiento.patt = /^\s/g;
objGestionSegumiento.patt2 = /$\s/g;

objGestionSegumiento.validar = function () {
    var bolError = false;
    objGestionSegumiento.intCodSolicitud = AspxSpinEditCodSolicitud.GetValue();
    objGestionSegumiento.strObservacion = AspxMemoObservacion.GetText();

    if (objGestionSegumiento.intCodSolicitud == null) {
        AspxLabelError.SetValue("*ERROR, favor ingresar un código de solicitud.");
        bolError = true;
    } else if (objGestionSegumiento.strObservacion == null || objGestionSegumiento.strObservacion == "") {
        AspxLabelError.SetValue("*ERROR, favor ingresar la observación.");
        bolError = true;
    } else if (objGestionSegumiento.patt.test(objGestionSegumiento.strObservacion) == true || objGestionSegumiento.patt2.test(objGestionSegumiento.strObservacion) == true) {
        AspxLabelError.SetValue("*ERROR, la observación no deben contener espacios vacios al inicio o al final.");
        bolError = true;
    }

    return bolError
}
objGestionSegumiento.ingresar = function () {
    if (objGestionSegumiento.validar() == false) {
        $.ajax({
            type: 'POST',
            url: '/Corresponsabilidad/SeguimientoSolicitudes/fnc_ingresar_observacion',
            data: {
                intCodSolicitud: objGestionSegumiento.intCodSolicitud,
                strObservacion: objGestionSegumiento.strObservacion
            },
            success: function (response) {
                switch (parseInt(response)) {
                    case 0:
                        AspxLabelError.SetText("*ERROR, no se logró realizar la acción, comunicarse con el encargado del sistema.");
                        break;
                    case 1:
                        objGestionSegumiento.limpiar();
                        AspxGridViewSeguimientoObservaciones.Refresh();
                        break;
                    case 2:
                        AspxLabelError.SetText("*ERROR, la sesión a expirado, favor volver a identificarse como un usuario de este sistema.");
                        break;
                }
            }
        });
    }
}

objGestionSegumiento.limpiar = function () {
    AspxSpinEditCodSolicitud.SetValue(null);
    AspxMemoObservacion.SetText(null);
    AspxLabelError.SetValue(null);
}


