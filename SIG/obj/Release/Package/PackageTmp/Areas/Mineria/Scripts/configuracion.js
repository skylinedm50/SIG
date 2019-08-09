
function btnActualizarAñoValidoClick(s, e) {

    $.ajax({
        url: '/Mineria/Configuracion/actualizarAnoElegibilidadFichas',
        type: 'POST',
        dataType: 'Javascript',
        data: {
            ano: txtAñoFichas.GetText()
        },
        beforeSend: function () {
            $(".modal").show();
        },
        complete: function () {
            $(".modal").hide();
        }
    })
    .done(function (response) {
        if (response == 1) {
            alert("Año de elegibilidad de Fichas Actualizado.");
        }
    })
    .fail(function () {
        console.log("error");
    })
    .always(function () {
        console.log("complete");
    });
}