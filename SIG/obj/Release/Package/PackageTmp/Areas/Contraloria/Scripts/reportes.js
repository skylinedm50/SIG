//////////////////////////////////////////////////////////////////////////////////////////////////////
// funciones de recibos pagados

//window.onload = "cambiarGrid";
var flag = false;

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
            alert("La fecha final debe de ser posterior o igual a la fecha inicial.");
            deFin.SetDate(inicio);
        }
    }
}

function FiltroValueChange(s, e) {
    if (s.GetValue() === 'fecha') {
        $('#divFecha').removeAttr('hidden');
        $('#divPeriodo').attr('hidden', 'true');
        cbxPeriodo.SetValue(null);
        cbxPeriodo.SetText(null);
    } else {
        $('#divFecha').attr('hidden', 'true');
        $('#divPeriodo').removeAttr('hidden');
        deInicio.SetValue(null);
        deFin.SetValue(null);
    }
}

function consultarClick(s, e) {

    var dpto = cbxDpto.GetValue();
    var muni = cbxMuni.GetValue();
    var aldea = cbxAldea.GetValue();
    var fondo = cbxFondo.GetValue();
    var banco = cbxBanco.GetValue();
    var sucursal = cbxSucursal.GetValue();
    var inicio = deInicio.GetText();
    var fin = deFin.GetText();
    var periodo = cbxPeriodo.GetValue();
    var esquema = cbxEsquemas.GetValue();
    var tipo = rbtTipo.GetValue();
    var filtro = rbtFiltro.GetValue();

    if (dpto === null && fondo === null && banco === null) {
        alert("Debe seleccionar al menos una opción para filtrar los datos. \nDebe seleccionar por lo menos un Departamento, un Fondo o un Banco");
    } else {

        if (filtro === 'fecha' && inicio === null && fin === null) {
            alert("Debe elegir una fecha de inicio y final.");
        } else if (filtro === 'periodo' && periodo === null) {
            alert("Debe elegir un periodo.");
        } else {
            debugger;
            $.ajax({
                //url: '/Reportes/returnHtmlGrid',
                url: '/Contraloria/Reportes/PartialGridViewRecibosPagados',
                type: 'POST',
                dataType: 'html',
                traditional: true,
                data: {
                    dpto: dpto,
                    muni: muni,
                    aldea: aldea,
                    fondo: fondo,
                    banco: banco,
                    sucursal: sucursal,
                    inicio: inicio,
                    fin: fin,
                    periodo: periodo,
                    esquema: esquema,
                    tipo: tipo,
                    filtro: filtro
                },
                success: function (response) {
                    if (response) {
                        $('#divGridView').html(response);
                        flag = true;
                    } else {
                        alert("Ocurrio un error al intentar traer la información de los pagos. Por favor intentelo de nuevo.");
                    }
                }
            });
        }
    }
}

function rbtTipoChange(s, e) {

    var tipo = s.GetValue();
    $.ajax({
        //url: '/Reportes/PartialGridView',
        url: '/Contraloria/Reportes/PartialGridView',
        type: 'POST',
        dataType: 'html',
        traditional: true,
        data: { tipo: tipo },
        success: function (response) {
            if (response) {
                $('#divGridView').html(response);
                flag = true;
            } else {
                alert("Ocurrio un error al intentar traer la información de los pagos. Por favor intentelo de nuevo.");
            }
        }
    });
}

function cambiarGrid() {
    //debugger;
    var tipo = rbtTipo.GetValue();
    $.ajax({
        url: '/Contraloria/Reportes/returnPartialGridView',
        type: 'POST',
        dataType: 'html',
        traditional: true,
        data: { tipo: tipo },
        success: function (response) {
            if (response) {
                $('#divGridView').html(response);
                flag = true;
            } else {
                alert("Ocurrio un error al intentar traer la información de los pagos. Por favor intentelo de nuevo.");
            }
        }
    });
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////
//funciones para reporte de consolidado

//funciones para obtener los códigos de periodo seleccionados
var selectedIDs;
function OnBeginCallback(s, e) {
    //Pass all selected keys to GridView callback action
    e.customArgs["selectedIDs"] = selectedIDs;
}
function OnSelectionChanged(s, e) {
    s.GetSelectedFieldValues("cod_periodo", GetSelectedFieldValuesCallback);
}
function GetSelectedFieldValuesCallback(values) {
    //Capture all selected keys
    //debugger;
    selectedIDs = values.join(',');
}
function OnClick(s, e) {
    //Show all selected keys on client side
    alert(selectedIDs);
}
function OnSubmitClick(s, e) {
    //Write all selected keys to hidden input. Pass them on post action.
    $("#selectedIDsHF").val(selectedIDs);
}
//fin para obtener los códigos de periodo seleccionados

function obtenerConsolidado(s, e) {

    var selected = gvPeriodos.GetSelectedKeysOnPage();
    var pago = selected[0];

    if (pago !== null) {
        $.ajax({
            url: "/Contraloria/Reportes/pv_pvgConsolidado",
            type: 'POST',
            dataType: 'html',
            data: {
                pago: pago
            },
            //beforeSend: function () {
            //    $(".modal").show();
            //},
            //complete: function () {
            //    $(".modal").hide();
            //}
        })
        .done(function (response) {
            txtPago.SetText(pago);
            $('#divGridConsolidado').html(response);
        })
        .fail(function () {
            console.log("error");
        })
        .always(function () {
            console.log("complete");
        });
    }
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////
//funciones para reporte de cuentas activadas por pago

function btnCuentasActivadasClick(s, e) {

    var pago = cbxPagos.GetValue()

    if (pago !== null) {
        $.ajax({
            url: "/Contraloria/Reportes/pv_pvgCuentasActivadas",
            type: 'POST',
            dataType: 'html',
            data: {
                pago: pago
            },
            beforeSend: function () {
                $(".modal").show();
            }
        })
        .done(function (response) {
            
            $('#divReporte').show()
            $('#divGridView').html(response)
            $(".modal").hide()
        })
    }

}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////
//funciones para reporte detalle cuentas básicas

function btnCuentasBasicasClick(s, e) {

    var pago = cbxPagos.GetValue()

    if (pago !== null) {
        $.ajax({
            url: "/Contraloria/Reportes/pv_gdvCuentasBasicas",
            type: 'POST',
            dataType: 'html',
            data: {
                pago: pago
            },
            beforeSend: function () {
                $(".modal").show();
            }
        })
        .done(function (response) {

            $('#divReporte').show()
            $('#divGridView').html(response)
            $(".modal").hide()
        })
    }

}