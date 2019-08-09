<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>


<script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/highmaps.js")%>'></script>
<script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/drilldown.js")%>'></script>
<script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/hn-all.js")%>'></script>
<script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/Municipios.js")%>'></script>

<div id="container" style="width: 700px; height: 500px; margin: 0 auto"></div>

<script>

    //var data = new Array();
    var jsonString = '<%= ViewData("jsonString").ToString()%>';
    jsonString = jsonString.replace("[", "");
    jsonString = jsonString.replace("]", "");
    var data = $.parseJSON('[' + jsonString + ']');

    $.each(data, function (i) {
        this.drilldown = this["hc-key"];
    });

    $('#container').highcharts('Map', {

        chart: {
            borderWidth: 1,
            events: {
                drilldown: function (e) {

                    if (!e.seriesOptions) {
                        var chart = this,
                            mapKey = e.point.drilldown,
                            // Handle error, the timeout is cleared on success
                            fail = setTimeout(function () {
                                if (!Highcharts.maps[mapKey]) {
                                    chart.showLoading('<i class="icon-frown"></i> Failed loading ' + e.point.name);

                                    fail = setTimeout(function () {
                                        chart.hideLoading();
                                    }, 1000);
                                }
                            }, 3000);

                        // Show the spinner
                        chart.showLoading('<i class="icon-spinner icon-spin icon-3x"></i>'); // Font Awesome spinner

                        $.ajax({
                            url: "/Mineria/Corresponsabilidad/pv_mapaDiferenciaAltasBajasSaludDepartamento",
                            type: 'POST',
                            dataType: 'json',
                            data: {
                                strPagos: planillas.join(","),
                                departamento: getCodigoDepartamento(mapKey)
                            }
                        })
                        .done(function (response) {

                            chart.hideLoading();
                            clearTimeout(fail);
                            chart.addSeriesAsDrilldown(e.point, {
                                name: 'Diferencia entre Altas y Bajas de Niños en Salud',
                                mapData: getMunicipios(mapKey),
                                data: $.parseJSON(response),
                                joinBy: 'codigo',
                                dataLabels: {
                                    enabled: true,
                                    format: '{point.name}'
                                },
                                tooltip: {
                                    pointFormat: '{point.name}<br/>Diferecia: {point.value}'
                                },
                                states: {
                                    hover: {
                                        color: '#BADA55'
                                    }
                                },
                            });

                        })
                        .fail(function () {
                            console.log("error");
                        })
                        .always(function () {
                            console.log("complete");
                        });
                    }
                    this.setTitle(null, { text: e.point.name });
                },
                drillup: function () {
                    this.setTitle(null, { text: 'Honduras' });
                }
            }
        },

        //titulo arriba del mapa
        title: {
            text: 'Diferencia entre Altas y Bajas de Niños en Salud'
        },

        //bótones de zoom
        mapNavigation: {
            enabled: true,
            buttonOptions: {
                verticalAlign: 'bottom'
            },
            enableDoubleClickZoom: false
        },

        colorAxis: {
            dataClasses: [{
                to: 0,
                color: '#ff9d92',
                name: 'Mayor número de Bajas'
            }, {
                from: 0,
                color: '#bde6ac',
                name: 'Mayor número de Altas'
            }]
        },

        legend: {
            title: {
                text: 'Diferencia entre Altas y Bajas de Niños en Salud',
                style: {
                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'black'
                }
            },
            layout: 'vertical',
            align: 'right',
            floating: true
        },

        /*
        //agrego el evento click
        plotOptions: {
            series: {
                cursor: 'pointer',
                events: {
                    click: function (e) {
                        alert("Haz hecho click en el departamento: " + e.point.name);
                    }
                }
            }
        },
        */

        series: [{
            animation: {
                duration: 1000
            },
            data: data,
            mapData: Highcharts.maps['countries/hn/hn-all'],
            joinBy: 'hc-key',

            name: 'Diferencia entre Altas y Bajas',
            states: {
                hover: {
                    color: '#BADA55'
                }
            },

            dataLabels: {
                enabled: true,
                format: '{point.name}'
            },

            tooltip: {
                pointFormat: '{point.name}<br/>Diferencia: {point.value}'
            }
        }],

        drilldown: {
            //series: drilldownSeries,
            activeDataLabelStyle: {
                color: '#FFFFFF',
                textDecoration: 'none',
                textShadow: '0 0 3px #000000'
            },
            drillUpButton: {
                relativeTo: 'spacingBox',
                position: {
                    x: 0,
                    y: 60
                }
            }
        }
    });
</script>