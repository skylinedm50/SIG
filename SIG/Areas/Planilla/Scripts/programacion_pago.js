function fnc_cbxPagosIndexChaged() {

    $.ajax({
        url: "/Planilla/ProgramacionPago/pv_gdvPeriodoApertura",
        type: 'POST',
        dataType: 'html',
        data: {
            pago: cbxPagos.GetValue()
        },
        beforeSend: function () {
            $(".modal").show();
        },
        complete: function () {
            $(".modal").hide();
        }
    })
    .done(function (response) {
        $('#divGridView').html(response);
    })
    .fail(function () {
        console.log("error");
    })
    .always(function () {
        console.log("complete");
    });
}

function fnc_validarFechas(control) {
    var inicio = new Date(txtFechaInicio.GetDate());
    var fin = new Date(txtFechaFin.GetDate());

    if (control == "inicio" && txtFechaFin.GetText() != "") {
        if (inicio > fin) {
            txtFechaInicio.SetDate(null);
        }
    } else if (control == "fin") {
        if (fin < inicio) {
            txtFechaFin.SetDate(null);
        }
    }
}