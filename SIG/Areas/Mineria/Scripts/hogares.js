


function btnBuscarPersonaClick(s, e) {

    $.ajax({
        url: '/Mineria/Hogares/pv_gdvPersonas',
        type: 'POST',
        dataType: 'html',
        data: {
            identidad: txtIdentidad.GetValue(),
            nombre: txtNombre.GetValue()
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
        floBuscarPersona.GetItemByName("itemBtnConsultar").SetVisible(true);
        $('.info').html("En caso de no encontrar la persona deseada, es necesario realizar un levantamiento de ficha.");
        $('.info').show();
        $('#divGridPersonas').html(response);
    })
    .fail(function () {
        console.log("error");
    })
    .always(function () {
        console.log("complete");
    });
}

function btnConsularHistorialClick(s, e) {

    var hogar = gdvPersonas.GetSelectedKeysOnPage();

    $.ajax({
        url: '/Mineria/Hogares/pv_gdvHistorialPagos',
        type: 'POST',
        dataType: 'html',
        data: {
            hogar: hogar[0]
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

/////////////////////////////////////////////////////////////////////////////////////////////////////////////

function btnConsultarFichasNoRemitidasClick(s, e) {
    
    $.ajax({
        url: '/Mineria/Hogares/pv_crtFichasNoRemitidas',
        type: 'POST',
        dataType: 'html',
        data: {
            departamento: cbxDpto.GetValue(),
            municipio: cbxMuni.GetValue(),
            aldea: cbxAldea.GetValue(),
            fecha: cbxFechas.GetValue()
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
        $('#divGrafico').html(response);
    })
    .fail(function () {
        console.log("error");
    })
    .always(function () {
        console.log("complete");
    });
}



