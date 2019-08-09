
function btnConsultarElegibleVsProgramadosClick(s, e) {

    var pago = cbxPagosAno.GetValue();

    if (pago != null) {
        $.ajax({
            url: "/Mineria/Proyecciones/pv_pvgElegiblesVsProgramado",
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

///////////////////////////////////////////////////////////////////////////////////////////////

function btnConsultarRazonesExclusionHogaresClick(s, e) {
    var pago = cbxPagosAno.GetValue();

    if (pago != null) {
        $.ajax({
            url: "/Mineria/Proyecciones/pv_pvgRazonesExclusionHogares",
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






