
function cbxPeriodoChanged(s, e) {
    //debugger;
    cbxFondo.PerformCallback();
    //cbxLeitz.SetValue(null);

    try {
        cbxLeitz.SetValue(null);
    }
    catch (err) {
        console.log('No existe el comboBox de los Leitz.')
    }
}

function cbxFondoBeginCallback(s, e) {
    //function(s, e) { e.customArgs['dpto'] = cbxDpto.GetValue();
    //debugger;
    e.customArgs['periodo'] = cbxPeriodo.GetValue();
}

function cbxFondoChanged(s, e) {

    try {
        cbxLeitz.PerformCallback();
        //adddlert("Welcome guest!");
    }
    catch (err) {
        //document.getElementById("demo").innerHTML = err.message;
    }
}

function cbxLeitzBeginCallback(s, e) {
    e.customArgs['periodo'] = cbxPeriodo.GetValue();
    e.customArgs['fondo'] = cbxFondo.GetValue();
}

function cbxLeitzEndCallback(s, e) {
    if (cbxLeitz.GetItemCount() === 0) {
        lblVacio.SetVisible(true);
    } else {
        lblVacio.SetVisible(false);
    }
}

/*
function btnAgregarDescClick(s, e) {
    txtDescripcion.SetText(null);
}


function btnAgregarLeitzClick(s, e) {

    var codPeriodo = cbxPeriodo.GetValue();
    var codFondo = cbxFondo.GetValue();
    var desc = txtDescripcion.GetText();

    if (codPeriodo !== null && codFondo !== null && desc !== "") {
        $.ajax({
            url: '/Contraloria/Escaneo/AgregarLeitz',
            type: 'POST',
            dataType: 'Json',
            //traditional: true,
            data: {
                codPeriodo: codPeriodo,
                codFondo: codFondo,
                desc: desc
            },
            success: function (response) {
                //debugger;
                if (response) {
                    cbxLeitz.PerformCallback();
                    $("div.close").click();
                } else {
                    alert("No se puedo agregar el Leitz");
                }
            }
        });
    } else {
        alert("Debe tener seleccionado un período, un fondo y haber ingresado una descripción para el Leitz.");
    }

    //$("div.close").click();
}
*/

function rbTipoValueChange(s, e) {

    try {
        var tipo = rbTipo.GetSelectedItem().text;
        if (tipo === "Reporte") {
            frmLayoutDocumento.GetItemByName("grupoFecha").SetVisible(true);
        } else {
            dtInicio.SetValue(null);
            dtFin.SetValue(null);
            frmLayoutDocumento.GetItemByName("grupoFecha").SetVisible(false);
        }
    }
    catch (err) {
        console.log('')
    }    
}

function ucTextChanged(s, e) {
    uc.Upload();
    //$('#divDocumento').html('/Escaneo/PartialVisorDocumento');

    $.ajax({
        url: '/Contraloria/Escaneo/PartialVisorDocumento',
        type: 'POST',
        dataType: 'html',
        //traditional: true,
        //data: { tipo: tipo },
        success: function (response) {
            if (response) {
                $('#divDocumento').html(response);
                flag = true;
            } else {
                alert("Ocurrio un error al intentar traer la información de los pagos. Por favor intentelo de nuevo.");
            }
        }
    });
}

function validarFecha(control) {
    var inicio = dtInicio.GetDate();
    var fin = dtFin.GetDate();

    if (control === 'inicio') {
        if (inicio > fin) {
            dtFin.SetDate(inicio);
        }
    } else if (control === 'fin') {
        if (inicio === null) {
            dtInicio.SetDate(fin);
        } else if (inicio > fin) {
            alert("La fecha final debe de ser posterior o igual a la fecha inicial.");
            dtFin.SetDate(inicio);
        }
    }
}

function btnGuardarDocumentoClick(s, e) {

    var leitz = cbxLeitz.GetValue();
    var dpto = cbxDepto.GetValue();
    var banco = cbxBanco.GetValue();
    var tipo = rbTipo.GetValue();
    var doc = $('#pdfViewer').attr('src');
    var inicio = dtInicio.GetText();
    var fin = dtFin.GetText();

    if (leitz !== null && banco !== null && dpto !== null && tipo !== null && doc !== "") {

        var tipoText = rbTipo.GetSelectedItem().text;

        if (tipoText === "Reporte") {

            if (inicio === "01/01/0100" || fin === "01/01/0100") {
                alert("En los Reportes debe debe seleccionar las fechas a lo que corresponden.");
            } else {

                $.ajax({
                    url: '/Contraloria/Escaneo/guardarDocumento',
                    type: 'POST',
                    dataType: 'Json',
                    traditional: true,
                    data: {
                        leitz: leitz,
                        dpto: dpto,
                        banco: banco,
                        tipo: tipo,
                        inicio: inicio,
                        fin: fin
                    },
                    success: function (response) {
                        if (response) {
                            alert("Documento guardado exitosamente")
                            //window.location.href = '/Escaneo/ViewEscaneo';
                            location.reload();
                            //$('#divDocumento').html(response);
                            //flag = true;
                        } else {
                            alert("Ocurrio un error al intentar almacenar el documento.");
                        }
                    }
                });

            }

        } else {

            $.ajax({
                url: '/Contraloria/Escaneo/guardarDocumento',
                type: 'POST',
                dataType: 'Json',
                traditional: true,
                data: {
                    leitz: leitz,
                    dpto: dpto,
                    banco: banco,
                    tipo: tipo,
                    inicio: inicio,
                    fin: fin
                },
                success: function (response) {
                    //debugger;
                    if (response) {
                        alert("Documento guardado exitosamente")
                        //window.location.href = '/Escaneo/ViewEscaneo';
                        location.reload();
                        //$('#divDocumento').html(response);
                        //flag = true;
                    } else {
                        alert("Ocurrio un error al intentar almacenar el documento.");
                    }
                }
            });
        }
    } else {
        alert("Debe seleccionar un Leitz, un Departamento, el tipo de archivo a guardar y haber seleccionado el archivo.");
    }
}

function btnConsultarClick(s, e) {

    var periodo = cbxPeriodo.GetValue();
    var fondo = cbxFondo.GetValue();
    var dpto = cbxDepto.GetValue();
    var banco = cbxBanco.GetValue();
    var tipo = rbTipo.GetValue();
    
    //var inicio = dtInicio.GetText();
    //var fin = dtFin.GetText();

    if (periodo !== null) {
        $.ajax({
            url: '/Contraloria/Escaneo/PartialGridMaestroLeitz',
            type: 'POST',
            dataType: 'html',
            traditional: true,
            data: {
                periodo: periodo,
                fondo: fondo,
                dpto: dpto,
                banco: banco,
                tipo: tipo

                //inicio: inicio,
                //fin: fin
            },
            success: function (response) {
                debugger;
                if (response) {
                    $('#divGridView').html(response);
                    //flag = true;
                } else {
                    alert("Ocurrio un error al intentar traer la información de los leitz.");
                }
            }
        });
    } else {
        alert('Debe seleccionar un período.');
    }

    //$.ajax({
    //    url: '/Escaneo/PartialGridMaestroLeitz',
    //    type: 'POST',
    //    dataType: 'html',
    //    traditional: true,
    //    data: {
    //        periodo: periodo,
    //        fondo: fondo,
    //        dpto: dpto,
    //        banco: banco,
    //        tipo: tipo

    //        //inicio: inicio,
    //        //fin: fin
    //    },
    //    success: function (response) {
    //        debugger;
    //        if (response) {
    //            $('#divGridView').html(response);
    //            //flag = true;
    //        } else {
    //            alert("Ocurrio un error al intentar traer la información de los leitz.");
    //        }
    //    }
    //});
}

function btnVerPDFClick(s, e) {

    var cod = s.GetRowKey(e.visibleIndex);
    window.open('/Contraloria/Escaneo/ViewDocumento?cod=' + cod);
    //$.ajax({
    //    type: 'POST',
    //    url: '/Contraloria/Escaneo/borrarArchivo'
    //});

}

// código js para ocultar o mostrar un item en un form layout
// frmLayoutDocumento.GetItemByName("grupoFecha").SetVisible(true)