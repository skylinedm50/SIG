function fnc_gif_carga_operacion(intOperacion) {
    if (intOperacion == 1) {
        $('#contenido-reporte').html("");
        $('#div-bloqueo-carga').css({ 'visibility': 'visible' });
        if (typeof AspxButtonGenerarReporte != 'undefined')
        {
            AspxButtonGenerarReporte.SetEnabled(false);
        }
        
    } else { //Cuando el valor es 0 se activa el botón y el gif se oculta.
        $('#div-bloqueo-carga').css({ 'visibility': 'hidden' });
        if (typeof AspxButtonGenerarReporte != 'undefined') {
            AspxButtonGenerarReporte.SetEnabled(true);
        }
    }
}

function fnc_car_comb_repor(codTipRepor, codCompo) //Funcion para cargar el comoboBox tipo de reporte. Este se activa por el comboBox de componente.
{
    $.ajax({
        type: 'POST',
        url: '/Corresponsabilidad/Reportes/fnc_car_comb_repo',
        data: { intCodTipRep: codTipRepor, intCodComp: codCompo },//Paramtro a enviar
        success: function (response) {
            for (var intI = 0; intI < response.length; intI++) {
                var arrJson = response[intI];
                var arrValCombo = [];
                var intIndex = 0;
                var strFiledValue = '';

                for (strIndice in arrJson) {
                    if (strIndice == 'cod_rep') {
                        strFiledValue = arrJson[strIndice];
                    } else {
                        arrValCombo[intIndex - 1] = arrJson[strIndice]; //Agregamos un nuevo elemento al arreglo.
                    }
                    intIndex++;
                }
                ASPxComboBoxReporDispo.InsertItem(intI, arrValCombo, strFiledValue);
            }
        }
    });
}

function fnc_identificar_reporte(intCodReporte)
{   
    switch (intCodReporte) {
        case 1:
            fnc_resumen_matricula_centro();
            break;
        case 2:
            fnc_obtener_controles_avance_academico_beneficiario();
            break;
        case 4:
            fnc_altas_bajas_matricula();
            break;
        case 3:
            fnc_cantidad_visitas_medicas_centro();
            break;
    }
}



function fnc_resumen_matricula_centro() {
    //Obtenemos los controles para el reporte.
    $.ajax({
        type: 'POST',
        url: '/Corresponsabilidad/Reportes/ControlesResumenMatriculaCentro',
        beforeSend: function () {
            fnc_gif_carga_operacion(1);
        },
        success: function (response)
        {
            fnc_gif_carga_operacion(0);
            $('#controles-reporte').html(response);
            $('#contenido-reporte').html('');
        }
    });

    
}

function fnc_obtener_grid_resumen_matri_centro()
{
    var intCodDepartamento = AspxComboBoxDepartamentoSACE.GetValue();
    var intCodMunicipio = AspxComboBoxMunicipioSACE.GetValue();
    var intCodAldea = AspxComboBoxAldeaSACE.GetValue();
    var intCodCaserio = AspxComboBoxCaserioSACE.GetValue();
    var intCodCentro = AspxComboBoxCentroEducativoSACE.GetValue();
    var intCodCorres = AspxComboBoxCorresponsabilidad.GetValue();

    intCodDepartamento = (intCodDepartamento == null ? -1 : intCodDepartamento);
    intCodMunicipio = (intCodMunicipio == null ? -1 : intCodMunicipio);
    intCodAldea = (intCodAldea == null ? -1 : intCodAldea);
    intCodCaserio = (intCodCaserio == null ? -1 : intCodCaserio);
    
    if (intCodDepartamento == -1 || intCodCentro == null || intCodCorres == null) {
        $('#contenido-reporte').html("<div id='error-reporte'>*Parámetros mínimos requeridos: departamento, municipio, centro educativo y la corresponsabilidad.</div>");
    } else
    {
        //Obtenemos el grid donde se muestra el reporte
        $.ajax({
            type: 'POST',
            url: '/Corresponsabilidad/Reportes/GridResumenMatriculaCentro',
            data: { intCodDep: intCodDepartamento, intCodMun: intCodMunicipio, intCodAld: intCodAldea, intCodCas: intCodCaserio, intCodCen: intCodCentro, intCodCorres: intCodCorres },
            beforeSend: function()
            {
                fnc_gif_carga_operacion(1);
            },
            success: function (response)
            {
                fnc_gif_carga_operacion(0);
                $('#contenido-reporte').html(response);
            }
        });
    }  
}


function fnc_obtener_controles_avance_academico_beneficiario() {
    $.ajax({
        type: 'POST',
        url: '/Corresponsabilidad/Reportes/ControlesAvanceAcademicoBeneficiario',
        beforeSend: function () {
            fnc_gif_carga_operacion(1);
        },
        success: function (response) {
            fnc_gif_carga_operacion(0);
            $('#controles-reporte').html(response);
            $('#contenido-reporte').html('');
        }
    });
}

function fnc_obtener_infor_avance_academico_beneficiario() {
    $.ajax({
        type: 'POST',
        url: '/Corresponsabilidad/Reportes/DocumentViewerAvanceAcadeBenefi',
        data: { strIdentidad: txtIdentidad.GetText(), intTipoOperacion: 0 },
        beforeSend: function () {
            fnc_gif_carga_operacion(1);
        },
        success: function (response) {
            fnc_gif_carga_operacion(0);
            $('#contenido-reporte').html(response);
        }
    });
}


function fnc_altas_bajas_matricula()
{
    $.ajax({
        type: 'POST',
        url: '/Corresponsabilidad/Reportes/ControlesAltasBajasMatricula',
        beforeSend: function () {
            fnc_gif_carga_operacion(1);
        },
        success: function (response)
        {
            fnc_gif_carga_operacion(0);
            $('#controles-reporte').html(response);
            $('#contenido-reporte').html('');
        }
    });
}

function fnc_obtener_grid_altas_bajas_matricula() {
    var strMat1 = AspxComboBoxMatricula1.GetText(); //Obtenemos el texto del la matricula
    var strMat2 = AspxComboBoxMatricula2.GetText();

    var intCodDepartamento = AspxComboBoxDepartamentoSACE.GetValue();
    var intCodMunicipio = AspxComboBoxMunicipioSACE.GetValue();
    var intCodAldea = AspxComboBoxAldeaSACE.GetValue();
    var intCodCaserio = AspxComboBoxCaserioSACE.GetValue();
    var intCodCentro = AspxComboBoxCentroEducativoSACE.GetValue();

    var arrMat1 = new Array;
    var arrMat2 = new Array;

    intCodDepartamento = (intCodDepartamento == null ? -1 : intCodDepartamento);
    intCodMunicipio = (intCodMunicipio == null ? -1 : intCodMunicipio);
    intCodAldea = (intCodAldea == null ? -1 : intCodAldea);
    intCodCaserio = (intCodCaserio == null ? -1 : intCodCaserio);


    arrMat1 = strMat1.split(' ', 3); //Creamos un arreglo apartir del nombre con 3 pocisiones.
    arrMat2 = strMat2.split(' ', 3);

    var intAñoMat1 = arrMat1[2];
    var intAñoMat2 = arrMat2[2];
    var intCodCentEdu = AspxComboBoxCentroEducativoSACE.GetValue();

    if (intAñoMat1 == undefined || intAñoMat2 == undefined || intCodCentEdu == null) {
        $('#contenido-reporte').html("<div id='error-reporte'>*Error campos necesarios: centro educativo, matrícula 1 y matrícula 2.</div>");
    } else {
        if (intAñoMat1 >= intAñoMat2) {
            $('#contenido-reporte').html("<div id='error-reporte'>*Error el año de la matrícula 1 no debe de ser mayor o igual al año de la matrícula 2. </div>");
        }
        else {
            $.ajax({
                type: 'POST',
                url: '/Corresponsabilidad/Reportes/GridAltasBajasMatricula',
                data: {
                    intAñoMat1: intAñoMat1, intAñoMat2: intAñoMat2, intCodCentEdu: intCodCentEdu, intDepartamento: intCodDepartamento,
                    intMunicipio: intCodMunicipio, intAldea: intCodAldea, intCaserio: intCodCaserio
                },
                beforeSend: function () {
                    fnc_gif_carga_operacion(1);
                },
                success: function (response) {
                    fnc_gif_carga_operacion(0);
                    $('#contenido-reporte').html(response);
                }
            });
        }
    }
}


function fnc_cantidad_visitas_medicas_centro() 
{

    $.ajax({
        type: 'POST',
        url: '/Corresponsabilidad/Reportes/ControlesCantVisitaMedicaCentro',
        beforeSend: function () {
            fnc_gif_carga_operacion(1);
        },
        success: function (response) 
        {
            fnc_gif_carga_operacion(0);
            $('#controles-reporte').html(response);
            $('#contenido-reporte').html('');
        }
    });
}

function fnc_obtener_reporte_cantidad_visitas_medicas_centro()
{
    var strDepartamento = AspxComboBoxDepartamentoSIG.GetValue();
    var strMunicipio = AspxComboBoxMunicipioSIG.GetValue();
    var strAldea = AspxComboBoxAldeaSIG.GetValue();
    var strCaserio = AspxComboBoxCaserioSIG.GetValue();

    strDepartamento = (strDepartamento == null ? "000" : strDepartamento);
    strMunicipio = (strMunicipio == null ? "000" : strMunicipio);
    strAldea = (strAldea == null ? "000" : strAldea);
    strCaserio = (strCaserio == null ? "000" : strCaserio);

    var intCodCentro = ((AspxComboBoxCentroSalud.GetValue() == null) ? -1 : AspxComboBoxCentroSalud.GetValue());
    var intCodFormReporte =  AspxComboBoxFormaReporte.GetValue();
    var intCodLevanCitaMedi = ((AspxComboBoxLevantaCitaMedica.GetValue() == null) ? 0 : AspxComboBoxLevantaCitaMedica.GetValue());

    var strFechIni = ((AspxDateEditFechaInicioCitaMedica.GetText() == "") ? "0" : AspxDateEditFechaInicioCitaMedica.GetText());
    var strFechFin = ((AspxDateEditFechaFinCitaMedica.GetText() == "") ? "0" : AspxDateEditFechaFinCitaMedica.GetText());
    var bolErrFech = false;


    //Validar el rango de fechas que este bien configurado.
    if (strFechIni !== "0" || strFechFin !== "0")
    {
        var objFechaIni = new Date(strFechIni);
        var objFechaFin = new Date(strFechFin);
        
        if ((objFechaIni > objFechaFin) == true) //Error la fecha inicio es mayor a la fecha fin.
        {
            var bolErrFech = true;
        }
    }

 
    if (intCodFormReporte !== null) //Validar que se ha seleccionado una forma de reporte.
    {
        switch (intCodFormReporte)
        {
            case 1: //Forma tipo tabla.
                if (intCodCentro > -1 ) //Codigo del centro necesario.
                {
                    if (bolErrFech == false) //Si el rango de fechas esta bien o no se utilizo ese parametro.
                    {
                        $.ajax({
                            type: 'POST',
                            url: '/Corresponsabilidad/Reportes/ObtTipoReporCantVistMedica',
                            data: {
                                strCodDepartamento: strDepartamento, strCodMunicipio: strMunicipio, strCodAldea: strAldea,
                                strCodCaserio: strCaserio, intCodCentroSal: intCodCentro, intCodLevanCitMed: intCodLevanCitaMedi,
                                intFormReport: intCodFormReporte, strFechIni: strFechIni, strFechFin: strFechFin
                            },
                            beforeSend: function () {
                                fnc_gif_carga_operacion(1);
                            },
                            success: function (response) {
                                fnc_gif_carga_operacion(0);
                                $('#contenido-reporte').html(response);
                            }
                        });
                    } else
                    {
                        $('#contenido-reporte').html("<div id='error-reporte'>*Error, la fecha inicio es mayor que la fecha fin de la cita médica.</div>");
                    }
                } else
                {
                    $('#contenido-reporte').html("<div id='error-reporte'>*Error, la forma de reporte: tabla requiere el código del centro.</div>");
                }
                break;
            default: //En losa casos que la forma de reporte sea 2, 3, 4 ó 5.
                if (bolErrFech == false) //Si el rango de fechas esta bien o no se utilizo ese parametro entonces es false.
                {
                   if (strDepartamento !== "000")
                   {
                       if (strCaserio !== "00" || strCaserio !== "000")
                       {
                           strCaserio = "00";
                       }

                        $.ajax({
                            type: 'POST',
                            url: '/Corresponsabilidad/Reportes/ObtTipoReporCantVistMedica',
                            data: {
                                strCodDepartamento: strDepartamento, strCodMunicipio: strMunicipio, strCodAldea: strAldea,
                                strCodCaserio: strCaserio, intCodCentroSal: intCodCentro, intCodLevanCitMed: intCodLevanCitaMedi,
                                intFormReport: intCodFormReporte, strFechIni: strFechIni, strFechFin: strFechFin
                            },
                            beforeSend: function () {
                                fnc_gif_carga_operacion(1);
                            },
                            success: function (response) {
                                fnc_gif_carga_operacion(0);
                                $('#contenido-reporte').html(response);
                            }
                        });
                    } else
                    {
                       $('#contenido-reporte').html("<div id='error-reporte'>*Error, para las formas gráficas de reporte es requerido como minimo el departamento.</div>");
                    }
                } else {
                    $('#contenido-reporte').html("<div id='error-reporte'>*Error, la fecha inicio es mayor que la fecha fin de la cita médica</div>");
                }
                break;
        }


    } else
    {
        $('#contenido-reporte').html("<div id='error-reporte'>*Error, debe elejir una forma de reporte.</div>");
    }
}



