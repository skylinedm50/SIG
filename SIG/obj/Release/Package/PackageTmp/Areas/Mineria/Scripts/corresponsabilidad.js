
var planillas = new Array();

//esta función obtiene los valores selectos en el gridlookup
// se llama así 
//gdlPagos.GetGridView().GetSelectedFieldValues("cod_pago", GotSelectedValues)
function GotSelectedValues(selectedValues) {
    planillas = selectedValues;
}

function gdlPagosValueChanged(s, e) {
    gdlPagos.GetGridView().GetSelectedFieldValues("cod_pago", GotSelectedValues);
}

///////////////////////////////////////////////////////////////////////////////////////////////////

function btnConsultarPorcentajeNinosIncumplenClick(s, e) {

    var pago = cbxPagosAno.GetValue();

    if (pago != null) {
        $.ajax({
            url: '/Mineria/Corresponsabilidad/pv_gdvNinoIncumplenComp',
            type: 'POST',
            dataType: 'html',
            data: {
                pago: cbxPagosAno.GetValue(),
                hogares: rbTipo.GetValue(),
                departamento: cbxDpto.GetValue(),
                municipio: cbxMuni.GetValue(),
                aldea: cbxAldea.GetValue()
            },
            beforeSend: function () {
                $(".modal").show();
            },
            complete: function () {
                $(".modal").hide();
            }
        })
        .done(function (response) {
            //console.log("success");
            $('#divGridView').html(response);
        })
        .fail(function () {
            console.log("error");
        })
        .always(function () {
            console.log("complete");
        });
    }
}

///////////////////////////////////////////////////////////////////////////////////////////////////

function btnConsultarArrastreAltasBajasEducacionClick(s, e) {

    if (planillas.length == 2) {
        $.ajax({
            url: "/Mineria/Corresponsabilidad/pv_pvgArrastreAltasBajasEduacion",
            type: 'POST',
            dataType: 'html',
            data: { strPagos: planillas.join(",") },
            beforeSend: function () {
                $(".modal").show();
            }
        })
        .done(function (response) {
            txtPagosSeleccionados.SetText(planillas.join(","));
            $('#divGridView').html(response);
            
            $.ajax({
                url: "/Mineria/Corresponsabilidad/pv_mapaDiferenciaAltasBajasEducacion",
                type: 'POST',
                datatype: 'html',
                //traditional: true,
                //async: false,
                data: { strPagos: planillas.join(",") },
                complete: function () {
                    $(".modal").hide();
                }
            })
            .done(function (mapa) {
                $('#divMapa').html(mapa);
            });
            
        })
        .fail(function () {
            console.log("error");
        })
        .always(function () {
            console.log("complete");
        });
    } else {
        //mensaje avisando que solo tiene que seleccionar 2 planillas
    }
}

///////////////////////////////////////////////////////////////////////////////////////////////////

function btnConsultarArrastreAltasBajasSaludClick(s, e) {

    if (planillas.length == 2) {
        $.ajax({
            url: "/Mineria/Corresponsabilidad/pv_pvgArrastreAltasBajasSalud",
            type: 'POST',
            dataType: 'html',
            data: { strPagos: planillas.join(",") },
            beforeSend: function () {
                $(".modal").show();
            }
        })
        .done(function (response) {
            txtPagosSeleccionados.SetText(planillas.join(","));
            $('#divGridView').html(response);
            
            $.ajax({
                url: "/Mineria/Corresponsabilidad/pv_mapaDiferenciaAltasBajasSalud",
                type: 'POST',
                datatype: 'html',
                //traditional: true,
                data: { strPagos: planillas.join(",") },
                complete: function () {
                    $(".modal").hide();
                }
            })
            .done(function (mapa) {
                $('#divMapa').html(mapa);
            });
            
        })
        .fail(function () {
            console.log("error");
        })
        .always(function () {
            console.log("complete");
        });
    } else {
        //mensaje avisando que solo tiene que seleccionar 2 planillas
    }
}


////////////////////////////////////////////////////////////////////////////////////////////////

function btnConsultarTotalesNinosComponenteClick(s, e) {

    var pago = cbxPagosAno.GetValue();

    if (pago != null) {
        $.ajax({
            url: "/Mineria/Corresponsabilidad/pv_pvgTotalesNinosComponente",
            type: 'POST',
            dataType: 'html',
            data: { pago: pago },
            beforeSend: function () {
                $(".modal").show();
            },
            complete: function () {
                $(".modal").hide();
            }
        })
        .done(function (response) {
            //console.log("success");
            $('#divGridView').html(response);
        })
        .fail(function () {
            console.log("error");
        })
        .always(function () {
            console.log("complete");
        });
    }
}