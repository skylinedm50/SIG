

window.onload = function () {
    //ponerAceros();
}

function verificar() {
    if ((cbxBanco.GetValue() !== null) && (cbxPeriodo.GetValue() !== null) && (txtNombre.GetText() !== "") && (rbtTipoFecha.GetValue() !== null) && (rbtTipoCarga.GetValue() !== null)) {
        
        enviarSignalRId();
        uc.Upload();
        txtNombre.SetEnabled(false);
        cbxBanco.SetEnabled(false);
        cbxPeriodo.SetEnabled(false);
        rbtTipoFecha.SetEnabled(false);

        if (rbtTipoCarga.GetValue() === "Rapida") {
            $(".modal").show();
        } else {
            pbCarga.SetVisible(true);
            lblCronometro.SetVisible(true);
            ponerAceros();
            iniciar();
        }
    }
}

function cargar() {
    
    if (LblPoderCargar.innerHTML !== "0") {
        if ((LblFchFueraProgramacion.innerHTML !== 0 && rbtFchFueraProgramacion.GetValue() !== null) || LblFchFueraProgramacion.innerHTML === 0) {
            pbCarga.SetVisible(true);
            lblCronometro.SetVisible(true);
            ponerAceros();
            iniciar();
            btnCargar.SetEnabled(false);
            $.ajax({
                type: 'POST',
                dataType: 'Json',                
                data: { tipoFecha: rbtTipoFecha.GetValue(), PermitirFueraProgramacion: rbtFchFueraProgramacion.GetSelectedIndex() },

                url: '/Contraloria/CargarArchivo/guardarCarga'
            });
        } else {
            alert("Error: Hay registros en que la fecha de pago no esta dentro de la programación, debe de especificar si se van a cargar o a excluir");
        }
    } else {
        alert("No existen registros que se puedan cargar");
        document.location.reload();
    }
    //recargar();
}

function recargar() {
    //debugger;
    //$('#divGridView').load('/Contraloria/CargarArchivo/PagosGridViewPartial');

    //$('#divResumen').load('/Contraloria/CargarArchivo/PartialMemo');

    $.ajax({
        type: 'POST',
        dataType: 'html',
        url: '/Contraloria/CargarArchivo/PartialMemo',
        success: function (response) {
            if (response) {
                $('#divResumen').html(response);
                flag = true;
            } else {
                alert("Ocurrio un error al intentar traer la información de los pagos. Por favor intentelo de nuevo.");
            }
        }
    });

    $.ajax({
        type: 'POST',
        dataType: 'html',
        //async: false,
        url: '/Contraloria/CargarArchivo/PagosGridViewPartial',
        beforeSend: function () {
            $(".modal").show();
        },
        complete: function () {
            $(".modal").hide();
        },
        success: function (response) {
            if (response) {
                $('#divGridView').html(response);
            } else {
                alert("Ocurrio un error al intentar traer la información de los pagos. Por favor intentelo de nuevo.");
            }
        }
    });
}

function recargarMemo() {

    $.ajax({
        url: '/Contraloria/CargarArchivo/PartialMemo',
        type: 'POST',
        dataType: 'html',
        beforeSend: function () {
            $(".modal").show();
        },
        complete: function () {
            $(".modal").hide();
        }
    })
    .done(function (response) {
        $('#divResumen').html(response);
    })
    .fail(function () {
        console.log("error");
    })
    .always(function () {
        console.log("complete");
    });

}

function recargarGridView() {

    $.ajax({
        url: '/Contraloria/CargarArchivo/PagosGridViewPartial',
        type: 'POST',
        dataType: 'html',
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

function enviarSignalRId() {
    debugger;
    $.ajax({
        type: 'POST',
        url: '/Contraloria/CargarArchivo/recibirSignalRId',
        async: false,
        data: {
            id: $.connection.hub.id
        }
    });
}

///////////////////////////////////////////////////////////////////////////
//función para el SignalR
$(function () {
    try {
        var connection = $.connection.progressClass;

        $.connection.hub.start().done(function (str) {
            //alert("Conectado a Signal R");
            debugger;
            console.log("Conectado a Signal R, connection ID: " + $.connection.hub.id);
        });
        
        connection.client.incrementarProgressBar = function (progreso) {          
            pbCarga.SetPosition(progreso);
            if (progreso === 100) {
                detener();
                //recargar();
                recargarMemo();
            }
        };

        connection.client.finPrecarga = function () {
            //recargar();
            recargarMemo();
        };

        
    } catch (err) {
        console.log(err);
    }

    
});
///////////////////////////////////////////////////////////////////////////


/////////////////////////////////////////////////////////////////////////////


//  FUNCIONES PARA EL HISTORIAL DE CARGAS //
function btnConsultarClick(s, e) {
    $.ajax({
        //traditional: true,        
        type: 'POST',
        dataType: 'html',
        url: '/Contraloria/CargarArchivo/retornarCargas',
        data: {  tipo: rbTipo.GetValue(), periodo: cbxPeriodo.GetValue(), banco: cbxBanco.GetValue() },
        success: function (response) {            
            if (response) {                
                $('#divGridCargas').html(response);
            } else {
                alert("Error al obtener la información.");
            }
        }
    });
}


function btnRegistrosClick(s, e) {

    $("#loadingIco").show();

    $.ajax({
        type: 'POST',
        traditional: true,
        //url: '/CargarArchivo/retornarRegistrosPreCargas',
        url: '/Contraloria/CargarArchivo/retornarRegistrosPreCargas',
        data: {
            tipo: rbTipo.GetValue(),
            cod: gvCargas.GetSelectedKeysOnPage()
        },
        success: function (response) {            
            $("#loadingIco").hide();
            if (response) {
                $('#divGridView').html(response);
            } else {
                alert("Error al obtener la información.");
            }
        }
    }).fail(function () {
        $("#loadingIco").hide();
    });
}

function btnDescargarClick(s, e) {
    
    var selected = gvCargas.GetSelectedKeysOnPage();
    var cod = selected[0];

    window.location.href = '/CargarArchivo/descargarArchivo?tipo=' + rbTipo.GetValue() + '&cod=' + cod;

    $.ajax({
        type: 'POST',
        url: '/Contraloria/CargarArchivo/borrarArchivo'
    });
}

function descargar(url) {
    debugger;
    var selected = gvCargas.GetSelectedKeysOnPage();
    var cod = selected[0];

    var destUrl = url + "?codPreCarga=" + cod;
    window.location.href = destUrl;
    //window.location.href = "/CargarArchivo/descargarArchivo/";
}


function Fnc_ExportarHistorialCarga()
{
    if (rbTipo.GetValue() !== null)
    {
        banco = cbxBanco.GetValue();
        if (banco === null)
        {
            banco=""
        }
        var url = "/Contraloria/CargarArchivo/ExportarHistorialCargas?tipo=" + rbTipo.GetValue() + "&periodo=" + cbxPeriodo.GetValue() + "&banco=" + banco
        window.location.href = url        
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////
// FUNCIONES PARA LA CARGA DE ARCHIVOS DE BANCARIZACIÓN

function btnArchivosBancarizacionClick() {

    if (cbxPeriodo.GetValue() !== null && cbxBanco.GetValue() !== null) {
        $.ajax({
            type: 'POST',
            traditional: true,
            url: '/Contraloria/CargarArchivo/pv_gdvArchivosBancarizacion',
            data: { periodo: cbxPeriodo.GetValue(), banco: cbxBanco.GetValue() },
            success: function (response) {
                if (response) {
                    $('#divGdvArchivos').html(response);
                } else {
                    alert("Error al obtener la información.");
                }
            }
        });
    }    
}

function btnConsolidadoCargaClick() {

    var slc = gdvArchivosBancarizacion.GetSelectedKeysOnPage();

    if (cbxPeriodo.GetValue() !== null && cbxBanco.GetValue() !== null && slc.length > 0) {

        $.ajax({
            type: 'POST',
            traditional: true,
            url: '/Contraloria/CargarArchivo/pv_pvgConsolidadoCargaBancarizacion',
            data: {
                periodo: cbxPeriodo.GetValue(),
                banco: cbxBanco.GetValue(),
                archivo: slc[0]
            },
            beforeSend: function () {
                $(".modal").show();
            },
            complete: function () {
                $(".modal").hide();
            },
            success: function (response) {
                if (response) {
                    $('#divContenedorPivot').show();
                    $('#divPivotGrid').html(response);                    
                } else {
                    alert("Error al obtener la información.");
                }
            }
        });
    }
    
}

function btnCargarPagosClick() {

    var slc = gdvArchivosBancarizacion.GetSelectedKeysOnPage();

    if (cbxPeriodo.GetValue() !== null && cbxBanco.GetValue() !== null && slc.length > 0) {

        $.ajax({
            type: 'POST',
            traditional: true,
            url: '/Contraloria/CargarArchivo/cargarPagosBancarizacion',
            data: {
                pago: cbxPeriodo.GetValue(),
                banco: cbxBanco.GetValue(),
                archivo: slc[0]
            },
            beforeSend: function () {
                $(".modal").show();
            },
            complete: function () {
                $(".modal").hide();
            },
            success: function (response) {
                if (response) {
                    if (response > 0) {
                        alert("Se cargaron " + response + " pagos.");
                        gdvArchivosBancarizacion.PerformCallback();
                    } else {
                        alert("No se cargo ningun pago.");
                    }
                } else {
                    alert("Error al obtener la información.");
                }
            }
        });
    }    
}

function gdvArchivosBancarizacionSelectionChanged() {

    var slc = gdvArchivosBancarizacion.GetSelectedKeysOnPage();

    if (slc.length > 0) {
        
        $.ajax({
            url: '/Contraloria/CargarArchivo/verificarArchDetallePagos',
            type: 'POST',
            dataType: 'html',
            data: { archivo: slc[0] }
        })
        .done(function (response) {
            txtPagoSeleccionado.SetText(slc[0]);
            if (response === "True") {
                btnConsolidadoCarga.SetEnabled(true);
                //btnListadoTitulares.SetEnabled(true);
            } else {
                btnConsolidadoCarga.SetEnabled(false);
                //btnListadoTitulares.SetEnabled(false);
            }
        })
        .fail(function () {
            console.log("error");
        })
        .always(function () {
            console.log("complete");
        });
    }

}

function btnResumenClick() {

    var slc = gdvArchivosBancarizacion.GetSelectedKeysOnPage();

    if (slc.length > 0) {
        $.ajax({
            url: '/Contraloria/CargarArchivo/pv_memoBancarizacion',
            type: 'POST',
            dataType: 'html',
            data: { archivo: slc[0] },
            beforeSend: function () {
                $(".modal").show();
            },
            complete: function () {
                $(".modal").hide();
            }
        })
        .done(function (response) {
            $('#divResumen').html(response);
        })
        .fail(function () {
            console.log("error");
        })
        .always(function () {
            console.log("complete");
        });
    }
}

function btnDetalleClick() {

    var slc = gdvArchivosBancarizacion.GetSelectedKeysOnPage();

    if (slc.length > 0) {
        $.ajax({
            url: '/Contraloria/CargarArchivo/pv_gdvDetalleArchivoBancarizacion',
            type: 'POST',
            dataType: 'html',
            data: { archivo: slc[0] },
            beforeSend: function () {
                $(".modal").show();
            },
            complete: function () {
                $(".modal").hide();
            }
        })
        .done(function (response) {
            $('#divContenedorGrid').show();
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

function btnExportarCuadroArchivosClick() {

    if (cbxPeriodo.GetValue() !== null && cbxBanco.GetValue() !== null) {
        var url = "/Contraloria/CargarArchivo/exportListaArchivos?pago=" + cbxPeriodo.GetValue() + "&banco=" + cbxBanco.GetValue();
        window.location.href = url;
    }
}

function btnExportarGridClick() {

    var slc = gdvArchivosBancarizacion.GetSelectedKeysOnPage();

    if (slc.length > 0) {
        var url = "/Contraloria/CargarArchivo/exportDetalleGrid?archivo=" + slc[0];
        window.location.href = url;
    }
}

function btnListadoTitularesClick() {

    var slc = gdvArchivosBancarizacion.GetSelectedKeysOnPage();

    if (slc.length > 0) {
        var url = "/Contraloria/CargarArchivo/exportListadoTitulares?pago=" + cbxPeriodo.GetValue() + "&banco=" + cbxBanco.GetValue() + "&archivo=" + slc[0];
        window.location.href = url;
    }

}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// FUNCIONES PARA LA VERIFICACIÓN DE ARCHIVOS DE BANCARIZACIÓN


function btnSincronizarClick() {

    $.ajax({
        url: '/Contraloria/CargarArchivo/sincronizar',
        type: 'POST',
        dataType: 'json',
        beforeSend: function () {
            $(".modal").show()
        },
        complete: function () {
            $(".modal").hide()
        }
    })
    .done(function (response) {
        //debugger

        if (response === "0") {
            alert("Ocurrío un error durante la sincronización, no se pudieron descargar todos los archivos.")
        }

        fmArchivos.PerformCallback()
    })

}

function btnHistorialClick() {

    if (fmArchivos.GetCurrentFolderId() === 'Recepcion' && fmArchivos.GetItems().length > 0) {
        cbxAños.SetValue(null)
        cbxPagos.SetValue(null)
        cbxPagos.ClearItems()
        pcModal.Show()
    }
}

function btnProcesarClick() {

    $.ajax({
        url: '/Contraloria/CargarArchivo/procesar',
        type: 'POST',
        dataType: 'json',
        beforeSend: function () {
            $(".modal").show()
        }
    })
    .done(function (response) {

        if (response === "1") {

            $.ajax({
                url: '/Contraloria/CargarArchivo/pv_gdvArchivosProcesados',
                type: 'POST',
                dataType: 'html',
                beforeSend: function () {
                    $(".modal").show()
                },
                complete: function () {
                    $(".modal").hide()
                }
            })
            .done(function (response) {
                $('#divGdvArchivos').html(response)
            })

        } else {
            //$(".modal").hide()
            alert("Ocurrio un error al procer los archivos.")
        }
    })

}

function cbxAñosChanged(s, e) {

    cbxPagos.ClearItems()

    for (let i = 0, len = data.length; i < len; i++) {

        if (data[i].pag_anyo === s.GetValue()) {
            cbxPagos.AddItem(data[i].pag_nombre, data[i].pag_codigo)
        }
    }

}

function btnMoverClick() {

    $.ajax({
        url: '/Contraloria/CargarArchivo/moverArchivosHistorial',
        type: 'POST',
        dataType: 'html',
        data: {
            año: cbxAños.GetValueString(),
            pago: cbxPagos.GetText()
        },
        beforeSend: function () {
            $(".modal").show();
        },
        complete: function () {
            $(".modal").hide();
        }
    })
    .done(function (response) {
        pcModal.Hide()
        fmArchivos.PerformCallback()
    })

}


