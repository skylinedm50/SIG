
var selectedIDs;

function OnSelectionChanged(s, e) {
    s.GetSelectedFieldValues("esq_codigo", GetSelectedFieldValuesCallback);
}

function GetSelectedFieldValuesCallback(values) {
    selectedIDs = values.join(',');
}

function fnc_cbxPagosIndexChaged(s, e) {
    try {
        gdvEsquemas.PerformCallback();
    } catch (e) {
        console.log('No existe el gridview de los esquemas');
    }

    try {
        cbxEsquemas.PerformCallback();
    } catch (e) {
        console.log('No existe el combobox de los esquemas');
    }
}

function btnGenerarDocumentosClick(s, e) {

    $.ajax({
        url: '/Planilla/GeneracionDocumentos/generarArchivos',
        type: 'POST',
        dataType: 'html',
        data: {
            strEsquemas: selectedIDs
        },
        beforeSend: function () {
            $(".modal").show();
        },
        complete: function () {
            $(".modal").hide();
        }
    })
    .done(function (response) {

        if (response.length > 0) {
            $('#divFileManager').html(response);
        } else {
            $('.error').html("Los esquemas seleccionados deben ser del mismo pago.");
            $('.error').show();
        }

    })
    .fail(function () {
        console.log("error");
    })
    .always(function () {
        console.log("complete");
    });

}