

//////////////////////////////////////////////////////////// Funciones JavaScript Que Manejan los Eventos en Tiempo  Real Para Generar Los Graficos //////////////////////////////////////////




function Fnc_GenerarGraficoHogarExcluido(str_localidad)
{

    var Str_municipio = (MuniCb.GetSelectedItem() == null) ? "" : MuniCb.GetSelectedItem().value;
    var Str_aldea = (AldeasCb.GetSelectedItem() == null) ? "" : AldeasCb.GetSelectedItem().value;
    var Str_caserio = (CaserioCb.GetSelectedItem() == null) ? "" : CaserioCb.GetSelectedItem().value;
    var Str_departamento = (CbDepto.GetSelectedItem() == null) ? "" : CbDepto.GetSelectedItem().value;
    var FchIncicio = fchInicio.GetText();
    var FchFinal = fchFinal.GetText();

    var Fecha_Actual = new Date();
    var Dia = Fecha_Actual.getDate();
    var mes = Fecha_Actual.getMonth() + 1;
    var anio = Fecha_Actual.getFullYear();

    var Str_Parametos = { 'departamento': Str_departamento, "municipio": Str_municipio, "aldea": Str_aldea, "caserio": Str_caserio, "str_tipo_chart": CbTipoCombo.GetSelectedItem().value, "Str_fecha_Inicio": FchIncicio, "Str_Fecha_final": FchFinal }

    if (FchFinal == (mes + '/' + Dia + '/' + anio) ||  FchFinal == "" ) {
        if (Str_departamento == "") {
            Fnc_GenerarGraficoAjax(Str_Parametos)
        }
        else if (Str_departamento == JSON.parse(str_localidad)[0]) {
            if (Str_municipio == "") {
                Fnc_GenerarGraficoAjax(Str_Parametos)
            } else if (Str_municipio == JSON.parse(str_localidad)[1]) {
                if (Str_aldea == "") {
                    Fnc_GenerarGraficoAjax(Str_Parametos)
                } else if (Str_aldea == JSON.parse(str_localidad)[2]) {
                    if (Str_caserio == "") {
                        Fnc_GenerarGraficoAjax(Str_Parametos)
                    } else if (Str_caserio == JSON.parse(str_localidad)[3]) {
                        Fnc_GenerarGraficoAjax(Str_Parametos)
                    }
                }
            }
        }
    } else 
    {
       
       // Fnc_GenerarGraficoAjax(Str_Parametos)
    }

}



function Fnc_GenerarGraficoHogarsExistentes(str_localidad)
{
    
    var Str_municipio = (MuniCb.GetSelectedItem() == null) ? "" : MuniCb.GetSelectedItem().value;
    var Str_aldea = (AldeasCb.GetSelectedItem() == null) ? "" : AldeasCb.GetSelectedItem().value;
    var Str_caserio = (CaserioCb.GetSelectedItem() == null) ? "" : CaserioCb.GetSelectedItem().value;
    var Str_departamento = (CbDepto.GetSelectedItem() == null) ? "" : CbDepto.GetSelectedItem().value;
    var FchIncicio = fchInicio.GetText();
    var FchFinal = fchFinal.GetText();
    var Fecha_Actual = new Date();
    var Dia = Fecha_Actual.getDate();
    var mes = Fecha_Actual.getMonth() + 1;
    var anio = Fecha_Actual.getFullYear();

    var Str_Parametos = { 'departamento': Str_departamento, "municipio": Str_municipio, "aldea": Str_aldea, "caserio": Str_caserio, "str_tipo_chart": CbTipoCombo.GetSelectedItem().value, "Str_fecha_Inicio": FchIncicio, "Str_Fecha_final": FchFinal }

    if (FchFinal == (mes + '/' + Dia + '/' + anio)) {
        if (Str_departamento == "") {
            Fnc_ReporteNuevoHogares(Str_Parametos)
        }
        else if (Str_departamento == JSON.parse(str_localidad)[0]) {
            if (Str_municipio == "") {
                Fnc_ReporteNuevoHogares(Str_Parametos)
            } else if (Str_municipio == JSON.parse(str_localidad)[1]) {
                if (Str_aldea == "") {
                    Fnc_ReporteNuevoHogares(Str_Parametos)
                } else if (Str_aldea == JSON.parse(str_localidad)[2]) {
                    if (Str_caserio == "") {
                        Fnc_ReporteNuevoHogares(Str_Parametos)
                    } else if (Str_caserio == JSON.parse(str_localidad)[3]) {
                        Fnc_ReporteNuevoHogares(Str_Parametos)
                    }
                }
            }
        }
    } else if (FchFinal == "")
    {
        Fnc_ReporteNuevoHogares(Str_Parametos)
    }

}

/*
$(function () {
    var Conexion = $.connection
    Conexion.notificadorMensajes.client.broadcastMessage = function (Int_Notificacion, str_localidad) {
        //if (document.getElementById("Grafico-Seccion"))
        //{
          
            if (Int_Notificacion == 1) {
                if ($("#WebChart").is(":visible")) {
                    Fnc_GenerarGraficoHogarExcluido(str_localidad);
                }
               
            }
            else if(Int_Notificacion==2)
            {
                if ($("#WebChart").is(":visible")) {
                    Fnc_GenerarGraficoHogarsExistentes(str_localidad);
                }
            }
       // }

    }

    $.connection.hub.start().done(function () {

    });

});

*/



