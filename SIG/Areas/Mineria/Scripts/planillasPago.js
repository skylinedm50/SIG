
//variable para saber cuales son las planillas selecciondas, llenada con el evento de cambio del
//gridlookup, esto porque la manera en que se extrae los valores de este elemnto funciona de manera
//asyncrona (paralela), y cuando la solicito desde el ajax la respuesta tarda y el valor al 
//controlador llega vacio.
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

function UpdateChart() {
    chart.PerformCallback();
}


///////////////////////////////////////////////////////////////////////////////////////////////

function btnListadoParticipantes(s, e) {

    if (cbxPagosAno.GetValue() !== null) {
        $.ajax({
            url: '/Mineria/PlanillasPago/pv_gdvMaestroListadoTitulares',
            type: 'POST',
            dataType: 'html',
            data: {
                pago: cbxPagosAno.GetValue(),
                tipo: rbTipo.GetValue(),
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

////////////////////////////////////////////////////////////////////////////////////////////////

function btnConsultarConsolidadoPagoClick(s, e) {

    var pago = cbxPagosAno.GetValue();

    if (pago !== null) {
        $.ajax({
            url: "/Mineria/PlanillasPago/pv_pvgConsolidadoPago",
            type: 'POST',
            dataType: 'html',
            data: {
                pago: pago
            },
            beforeSend: function () {
                $(".modal").show();
            },
            complete: function () {
                $(".modal").hide();
            }
        })
        .done(function (response) {
            $('#divChart').show();
            $('#divGridView').html(response);
            //chart.PerformCallback();
            //UpdateChart();
        })
        .fail(function () {
            console.log("error");
        })
        .always(function () {
            console.log("complete");
        });
    }
}

function OnBeginChartConsolidadoCallback(s, e) {
    try {
        pvgConsolidado.FillStateObject(e.customArgs);
        e.customArgs['tipoGrafico'] = cbxTipoGrafico.GetValue();
    } catch (err) {
        console.log("No se encontro el pivotgrid del consolidado");
    }
    
}

///////////////////////////////////////////////////////////////////////////////////////////////////

var obtenerRazones = true;

function btnConsultarComparacionClick(s, e) {

    //gdlPagos.GetGridView().GetSelectedFieldValues("cod_pago", GotSelectedValues);

    if (planillas.length === 2) {
        $.ajax({
            url: "/Mineria/PlanillasPago/pv_pvgComparacionPlanillas",
            type: 'POST',
            dataType: 'html',
            data: { strPagos: planillas.join(",") },
            beforeSend: function () {
                $(".modal").show();
            }
        })
        .done(function (response) {
            //console.log("success");
            txtPagosSeleccionados2.SetText(planillas.join(","));
            $('#divGridView').html(response);
            frmLayoutButtons.GetItemByName('itemBtnRazonCaida').SetVisible(true);
            chart.PerformCallback();
            $('#divChart').show();

            $.ajax({
                url: "/Mineria/PlanillasPago/pv_mapaDiferenciaAltasBajasPlanillas",
                type: 'POST',
                datatype: 'html',
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
            $(".modal").hide();
        })
        .always(function () {
            console.log("complete");
        });
    } else {
        //mensaje avisando que solo tiene que seleccionar 2 planillas
    }
}

$(document).on('click', '.close', function () {
    jQuery('body').css('overflow', 'visible');
    $("#myModal").css("display", "none");
});

window.onclick = function (event) {
    if (event.target === document.getElementById('myModal')) {
        jQuery('body').css('overflow', 'visible');
        $("#myModal").css("display", "none");
    }
}

function btnRazonCaidaHogaresClick(s, e) {

    if (obtenerRazones && planillas.length === 2) {

        $.ajax({
            url: "/Mineria/PlanillasPago/pv_pvgRazonCaidaHogares",
            type: 'POST',
            dataType: 'html',
            data: { strPagos: planillas.toString() },
            beforeSend: function () {
                $(".modal").show();
            }
        })
        .done(function (response) {
            txtPagosSeleccionados.SetText(planillas.join(","));
            $(".modal").hide();
            jQuery('body').css('overflow', 'hidden');
            $("#myModal").css("display", "block");
            $('#divRazonCaida').html(response);
            obtenerRazones = false;

        })
        .fail(function () {
            console.log("error");
            $(".modal").hide();
        })
        .always(function () {
            console.log("complete");
        });

    } else {
        jQuery('body').css('overflow', 'hidden');
        $("#myModal").css("display", "block");
    }

}

function OnBeginChartComparacionCallback(s, e) {
    try {
        pvgComparacionPlanillas.FillStateObject(e.customArgs);
        e.customArgs['tipoGrafico'] = cbxTipoGrafico.GetValue();
        e.customArgs['strPagos'] = planillas.toString();
    } catch (err) {
        console.log("No se encontro el consolidado de arrastre, altas y bajas entre pagos");
    }
}

////////////////////////////////////////////////////////////////////////////////////////////////

function btnConsultarElegibleVsProgramadosClick(s, e) {

    var pago = cbxPagosAno.GetValue();

    if (pago !== null) {
        $.ajax({
            url: "/Mineria/PlanillasPago/pv_pvgElegiblesVsProgramado",
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

    if (pago !== null) {
        $.ajax({
            url: "/Mineria/PlanillasPago/pv_pvgRazonesExclusionHogares",
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

function btnConsultarFichasCensoAnoClick(s, e) {
    var pago = cbxPagosAno.GetValue();

    if (pago !== null) {
        $.ajax({
            url: "/Mineria/PlanillasPago/pv_pgvFichasCensoAno",
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

function btnConsultarNinosPagadosCicloClick(s, e , variante) {
    var pago = cbxPagosAno.GetValue();
  
    if (pago !== null) {
        $.ajax({
            url: "/Mineria/PlanillasPago/pv_pgvNinosPagadosPorCiclos",
            type: 'POST',
            dataType: 'html',
            data: { pago: pago, variante: variante },
            beforeSend: function () {
                $(".modal").show();
            }
        })
        .done(function (response) {
            //console.log("success");
            $('#divGridView').html(response);

            $.ajax({
                url: "/Mineria/PlanillasPago/pv_mapaCantidadNinosPorCiclo",
                type: 'POST',
                datatype: 'html',
                traditional: true,
                //async: false,
                data: { pago: pago },
                complete: function () {
                    $(".modal").hide();
                }
            })
            .done(function (mapa) {
                $('#divMapa').html(mapa);
                $("#hide").hide()
            });

        })
        .fail(function () {
            console.log("error");
        })
        .always(function () {
            console.log("complete");
        });
    }
}

////////////////////////////////////////////////////////////////////////////////////////////////

function btnConsultarListadoNinosPagoClick(s, e) {

    if (cbxPagosAno.GetValue() !== null) {
        $.ajax({
            url: '/Mineria/PlanillasPago/pv_gdvListadoNinosPago',
            type: 'POST',
            dataType: 'html',
            data: {
                pago: cbxPagosAno.GetValue(),
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

////////////////////////////////////////////////////////////////////////////////////////////////

function btnNucleoFamiliarMuestraClick(s, e) {

    if (cbxPagosAno.GetValue() !== null) {

        var hogares;
        hogares = txtHogares.GetText();
        hogares = hogares.replace(/\r?\n|\r/g, ",");
        hogares = hogares.replace(/\s/g, "");

        txtHogaresHiden.SetText(hogares);

        $.ajax({
            url: '/Mineria/PlanillasPago/pv_pvgNucleoFamiliarMuestra',
            type: 'POST',
            dataType: 'html',
            data: {
                pago: cbxPagosAno.GetValue(),
                hogares: hogares
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

////////////////////////////////////////////////////////////////////////////////////////////////

function btnConsultarBasePagadoClick(s, e) {

    if (cbxPagosAno.GetValue() !== null) {

        $.ajax({
            url: '/Mineria/PlanillasPago/pv_pvgBasePagado',
            type: 'POST',
            dataType: 'html',
            data: {
                pago: cbxPagosAno.GetValue()
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
        .fail(function (error) {
            console.log("error"+error);
        })
        .always(function () {
            console.log("complete");
        });

    }
}

//////////////////////////////////////////////////////////////////////////////////////
/*
//variable para saber cuales son las planillas selecciondas, llenada con el evento de cambio del
//gridlookup, esto porque la manera en que se extrae los valores de este elemnto funciona de manera
//asyncrona (paralela), y cuando la solicito desde el ajax la respuesta tarda y el valor al 
//controlador llega vacio.
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
*/
function rblPeriodoTiempoChanged(s, e) {

    var periodo = s.GetValue();
    var url, flag;

    switch (periodo) {
        case "Anual":
            $('#divFiltro').empty();
            flag = false;
            break;
        case "Semestral":
            $('#divFiltro').empty();
            flag = false;
            break;
        case "Acumulado Planillas":
            url = "/Mineria/PlanillasPago/pv_ControlesAcumuladoPlanillas";
            flag = true;
            break;
        case "Rango de Fechas":
            url = "/Mineria/PlanillasPago/pv_ControlesRangoFecha";
            flag = true;
            break;
    }

    if (flag) {

        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'html',
        })
        .done(function (response) {
            //console.log("success");
            $('#divFiltro').html(response);
        })
        .fail(function () {
            console.log("error");
        })
        .always(function () {
            console.log("complete");
        });
    }
}

function validarFecha(control) {
    var inicio = deInicio.GetDate();
    var fin = deFin.GetDate();

    if (control === 'inicio') {
        if (inicio > fin) {
            deFin.SetDate(inicio);
        }
    } else if (control === 'fin') {
        if (inicio === null) {
            deInicio.SetDate(fin);
        } else if (inicio > fin) {
            //alert("La fecha final debe de ser posterior o igual a la fecha inicial.");
            deFin.SetDate(inicio);
        }
    }
}

function btnConsultarHogPagadosClick(s, e) {

    var tipo = rblPeriodoTiempo.GetValue();
    var url, data, flag;

    debugger;

    switch (tipo) {
        case "Anual":

            url = "/Mineria/PlanillasPago/pv_pvgHogaresPagadosAnual";
            data = { tipo: tipo };
            flag = true;

            break;
        case "Semestral":
            url = "/Mineria/PlanillasPago/pv_pvgHogaresPagadosSemestral";
            data = { tipo: tipo };
            flag = true;
            break;
        case "Acumulado Planillas":

            if (planillas.length > 0) {

                url = "/Mineria/PlanillasPago/pv_pvgHogaresPagadosAcumuladoPlanillas";
                data = {
                    tipo: tipo,
                    planilla: planillas.toString()
                };
                flag = true;
            } else {
                flag = false;
            }
            break;
        case "Rango de Fechas":

            var inicio = deInicio.GetText();
            var fin = deFin.GetText();

            if (inicio !== "01/01/0100" && fin !== "01/01/0100") {
                url = "/Mineria/PlanillasPago/pv_pvgHogaresPagadosRangoFechas";
                data = {
                    tipo: tipo,
                    inicio: inicio,
                    fin: fin
                };
                flag = true;
            } else {
                flag = false;
            }
            break;
    }

    if (flag) {
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'html',
            traditional: true,
            data: data,
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