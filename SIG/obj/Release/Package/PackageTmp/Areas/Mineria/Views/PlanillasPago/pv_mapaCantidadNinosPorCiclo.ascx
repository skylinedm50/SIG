<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/highmaps.js")%>'></script>
<script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/drilldown.js")%>'></script>
<script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/hn-all.js")%>'></script>
<script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/Municipios.js")%>'></script>
<link href="https://netdna.bootstrapcdn.com/font-awesome/3.2.1/css/font-awesome.css" rel="stylesheet">

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
                            url: "/Mineria/PlanillasPago/pv_mapaCantidadNinosPorCicloDepartamento",
                            type: 'POST',
                            dataType: 'json',
                            //traditional: true,
                            data: {
                                pago: cbxPagosAno.GetValue(),
                                dpto: getCodigoDepartamento(mapKey)
                            }
                        })
                        .done(function (response) {

                            chart.hideLoading();
                            clearTimeout(fail);
                            chart.addSeriesAsDrilldown(e.point, {
                                //name: e.point.name,
                                name: 'Niños por Ciclo',
                                mapData: getMunicipios(mapKey),
                                data: $.parseJSON(response),
                                joinBy: 'codigo',
                                dataLabels: {
                                    enabled: true,
                                    format: '{point.name}'
                                },
                                tooltip: {
                                    pointFormat: '{point.name}<br/>0 a 4 años: {point.ciclo_1}<br/>5 a 6 años: {point.ciclo_2}<br/>7 a 11 años: {point.ciclo_3}<br/>12 años: {point.ciclo_4}<br/>13 a 15 años: {point.ciclo_5}<br/>16 a 17 años: {point.ciclo_6}'
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
            text: 'Cantidad de Niños por Ciclo de Vida'
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
                name: ''
            }, {
                from: 1,
                to: 1,
                name: '0 a 4 años'
            }, {
                from: 2,
                to: 2,
                name: '5 a 6 años'
            }, {
                from: 3,
                to: 3,
                name: '7 a 11 años'
            }, {
                from: 4,
                to: 4,
                name: '12 años'
            }, {
                from: 5,
                to: 5,
                name: '13 a 15 años'
            }, {
                from: 6,
                to: 6,
                name: '16 a 17 años'
            }]
        },

        legend: {
            title: {
                text: 'Ciclos de Vida',
                style: {
                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'black'
                }
            },
            layout: 'vertical',
            align: 'right',
            floating: true
        },

        ////agrego el evento click
        //plotOptions: {
        //    series: {
        //        cursor: 'pointer',
        //        events: {
        //            click: function (e) {
        //                alert("Haz hecho click en el departamento: " + e.point.name);
        //            }
        //        }
        //    }
        //},

        series: [{
            animation: {
                duration: 1000
            },
            data: data,
            mapData: Highcharts.maps['countries/hn/hn-all'],
            joinBy: 'hc-key',

            name: 'Niños por Ciclo',
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
                pointFormat: '{point.name}<br/>0 a 4 años: {point.ciclo_1}<br/>5 a 6 años: {point.ciclo_2}<br/>7 a 11 años: {point.ciclo_3}<br/>12 años: {point.ciclo_4}<br/>13 a 15 años: {point.ciclo_5}<br/>16 a 17 años: {point.ciclo_6}'
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