

/*
    abreviaturas para los ids de los documentos
    recibos
    RSH - recibo sin huella
    RRI - recibo con referencia ilegible
    RHI - recibo con huella ilegible
    RSC - recibo sin corresponsabilidad
    RSS - recibo sin sello
    RPR - recibo con papel reciclado
    RMI - recibo mal impreso
    RFC - recibo fecha no coincide
    RD - recibo duplicado
    RMC - recibo municipio no coincide
    RR - recibo roto
    RF - recibo faltante

    actas de cierre

    reportes
    REI - reportes ilegibles
    RESS - reporte sin sello
    RESF - reporte sin firma

    notas de cargo

    */

var countRecibos = new Array(),
    countActas = new Array(),
    countReportes = new Array(),
    countNotas = new Array(),
    flagRecibos, flagActas, flagReportes, flagNotas;

function btnVerActaClick(s, e) {
    crearActa();
}

function btnDetallesClick(s, e) {

    //contador de los recibos y código para agregar los controles para los agregar los detalles de los recibos
    countRecibos[0] = txtRSH.GetText();
    countRecibos[1] = txtRRI.GetText();
    countRecibos[2] = txtRHI.GetText();
    countRecibos[3] = txtRSC.GetText();
    countRecibos[4] = txtRSS.GetText();
    countRecibos[5] = txtRPR.GetText();
    countRecibos[6] = txtRMI.GetText();
    countRecibos[7] = txtRFC.GetText();
    countRecibos[8] = txtRD.GetText();
    countRecibos[9] = txtRMC.GetText();
    countRecibos[10] = txtRR.GetText();
    countRecibos[11] = txtRF.GetText();

    //si existe por lo menos un control de los recibos con número muestra el de los recibos
    for (i = 0, long = countRecibos.length; i <= long; i++) {
        if (countRecibos[i] > 0) {
            $('#divRecibos').attr('hidden', false);
            flagRecibos = true;
        }
    }

    if (flagRecibos) {

        //condición if para agregar los controles para ingresar los datos de los recibos sin huella
        if (countRecibos[0] > 0) {
            for (i = 1; i <= countRecibos[0]; i++) {
                //verifica si existe un control con el número del ciclo, esto para no agregar controles repetidos con los mismo id's
                if (!($("#div_RSH_" + i).length)) {
                    htmlRecibos('sin huella', 'RSH', i);
                }
            }
        }

        //condición if para agregar los controles para ingresar los datos de los recibos con referencia ilegible
        if (countRecibos[1] > 0) {
            for (i = 1; i <= countRecibos[1]; i++) {
                if (!($("#div_RRI_" + i).length)) {
                    htmlRecibos('con referencia ilegible', 'RRI', i);
                }
            }
        }

        //condición if para agregar los controles para ingresar los datos de los recibos con huella ilegible
        if (countRecibos[2] > 0) {
            for (i = 1; i <= countRecibos[2]; i++) {
                if (!($("#div_RHI_" + i).length)) {
                    htmlRecibos('con huella ilegible', 'RHI', i);
                }
            }
        }

        //condición if para agregar los controles para ingresar los datos de los recibos sin corresponsabilidad
        if (countRecibos[3] > 0) {
            for (i = 1; i <= countRecibos[3]; i++) {
                if (!($("#div_RSC_" + i).length)) {
                    htmlRecibos('sin corresponsabilidad', 'RSC', i);
                }
            }
        }

        //condición if para agregar los controles para ingresar los datos de los recibos sin sello
        if (countRecibos[4] > 0) {
            for (i = 1; i <= countRecibos[4]; i++) {
                if (!($("#div_RSS_" + i).length)) {
                    htmlRecibos('sin sello', 'RSS', i);
                }
            }
        }

        //condición if para agregar los controles para ingresar los datos de los recibos con papel reciclado
        if (countRecibos[5] > 0) {
            for (i = 1; i <= countRecibos[5]; i++) {
                if (!($("#div_RPR_" + i).length)) {
                    htmlRecibos('con papel reciclado', 'RPR', i);
                }
            }
        }

        //condición if para agregar los controles para ingresar los datos de los recibos mal impreso
        if (countRecibos[6] > 0) {
            for (i = 1; i <= countRecibos[6]; i++) {
                if (!($("#div_RMI_" + i).length)) {
                    htmlRecibos('mal impreso', 'RMI', i);
                }
            }
        }

        //condición if para agregar los controles para ingresar los datos de los recibos en los que la fecha no coincide
        if (countRecibos[7] > 0) {
            for (i = 1; i <= countRecibos[7]; i++) {
                if (!($("#div_RFC_" + i).length)) {
                    htmlRecibos('que fecha no coincide', 'RFC', i);
                }
            }
        }

        //condición if para agregar los controles para ingresar los datos de los recibos duplicados
        if (countRecibos[8] > 0) {
            for (i = 1; i <= countRecibos[8]; i++) {
                if (!($("#div_RD_" + i).length)) {
                    htmlRecibos('duplicado', 'RD', i);
                }
            }
        }

        //condición if para agregar los controles para ingresar los datos de los recibos que municipio no coincide
        if (countRecibos[9] > 0) {
            for (i = 1; i <= countRecibos[9]; i++) {
                if (!($("#div_RMC_" + i).length)) {
                    htmlRecibos('que municipio no coincide', 'RMC', i);
                }
            }
        }

        //condición if para agregar los controles para ingresar los datos de los recibos roto
        if (countRecibos[10] > 0) {
            for (i = 1; i <= countRecibos[10]; i++) {
                if (!($("#div_RR_" + i).length)) {
                    htmlRecibos('roto', 'RR', i);
                }
            }
        }

        //condición if para agregar los controles para ingresar los datos de los recibos faltantes
        if (countRecibos[11] > 0) {
            for (i = 1; i <= countRecibos[11]; i++) {
                if (!($("#div_RF_" + i).length)) {
                    htmlRecibos('faltante', 'RF', i);
                }
            }
        }
    }

    function htmlRecibos(tipo, recibo, i) {
        var html = '<div id="div_' + recibo + '_' + i + '"><fieldset class="field-recNro"><legend>Recibo ' + tipo + ' nro ' + i + '</legend> \
                    <div><table><tr><td><label>Referencia: </label></td><td><input id="txtRef_' + recibo + '_' + i + '" type="text"> \
                    </td></tr></table></div></fieldset></div><br />';

        $('#div_' + recibo).append(html);
    }


    // inicio de código para agregar los controles para actas de cierre

    countActas[0] = txtACFormatoIncorrecto.GetText();
    countActas[1] = txtACFechaIncorrecta.GetText();
    countActas[2] = txtACFondoIncorrecto.GetText();
    countActas[3] = txtACOtros.GetText();

    //si existe por lo menos un control de las actas de cierre con número muestra el div de las actas de cierre
    for (i = 0, long = countActas.length; i <= long; i++) {
        if (countActas[i] > 0) {
            $('#divActasCierre').attr('hidden', false);
            flagActas = true;
        }
    }

    if (flagActas) {

        //condición if para agregar los controles para ingresar los datos de las actas de cierre con formato incorrecto
        if (countActas[0] > 0) {
            for (i = 1; i <= countActas[0]; i++) {
                if (!($("#div_ACFORI_" + i).length)) {
                    htmlActas('formato incorrecto', 'ACFORI', i);
                }
            }
        }

        //condición if para agregar los controles para ingresar los datos de las actas de cierre con fecha incorrecta
        if (countActas[1] > 0) {
            for (i = 1; i <= countActas[1]; i++) {
                if (!($("#div_ACFECI_" + i).length)) {
                    htmlActas('fecha incorrecta', 'ACFECI', i);
                }
            }
        }

        //condición if para agregar los controles para ingresar los datos de las actas de cierre con fondo incorrecto
        if (countActas[2] > 0) {
            for (i = 1; i <= countActas[2]; i++) {
                if (!($("#div_ACFONI_" + i).length)) {
                    htmlActas('fondo incorrecto', 'ACFONI', i);
                }
            }
        }

        //condición if para agregar los controles para ingresar los datos de las actas de cierre con otros tipos de errores
        if (countActas[3] > 0) {
            for (i = 1; i <= countActas[3]; i++) {
                if (!($("#div_ACO_" + i).length)) {
                    htmlActas('otros', 'ACO', i);
                }
            }
        }
    }

    // función para agregar los input al div donde se encuentran los detalles de las actas de cierre, recibe 3 parametros, el primero el el tipo (ej. roto, duplicado, fecha no coincide),
    // el segundo es la abreveación que asignada al tipo de acta y el ultimo es el número
    function htmlActas(tipo, acta, i) {

        //if (tipo == "formato incorrecto" || tipo == "fecha incorrecta" || tipo == "otros") {
        //    var html = '<div id="div_' + acta + '_' + i + '"><fieldset><legend>Acta de Cierre con ' + tipo + ' nro ' + i + '</legend><table border="0"><tbody><tr><td><label>Tipo de pago:</label></td><td> \
        //                <select id="cbxPago_' + acta + '_' + i + '"><option value="movil">Móvil</option><option value="agencia">Agencia</option></select></td></tr><tr><td> \
        //                <label>Departamento:</label></td><td><select id="cbxDpto_' + acta + '_' + i + '"><option>ATLANTIDA</option><option>COLON</option><option>COMAYAGUA</option> \
        //                <option>COPAN</option><option>CORTES</option><option>CHOLUTECA</option><option>EL PARAISO</option><option>FRANCISCO MORAZAN</option> \
        //                <option>GRACIAS A DIOS</option><option>INTIBUCA</option><option>ISLAS DE LA BAHIA</option><option>LA PAZ</option><option>LEMPIRA</option> \
        //                <option>OCOTEPEQUE</option><option>OLANCHO</option><option>SANTA BARBARA</option><option>VALLE</option><option>YORO</option></select> \
        //                </td></tr><tr><td colspan="2"><label>Descripción</label></td></tr><tr><td colspan="2"><textarea id="txtDesc_' + acta + '_' + i + '"></textarea></td></tr> \
        //                </tbody></table></fieldset></div>';

            //var html = '';
        //}

        //if (tipo == "fondo incorrecto") {
            //var html = '<div id="div_' + acta + '_' + i + '"><fieldset><legend>Acta de Cierre con ' + tipo + ' nro ' + i + '</legend><table border="0"><tbody><tr><td> \
            //            <label>Tipo de pago:</label></td><td><select id="cbxPago_' + acta + '_' + i + '"><option value="movil">Móvil</option><option value="agencia">Agencia</option> \
            //            </select></td><td><label>Departamento:</label></td><td><select id="cbxDpto_' + acta + '_' + i + '"><option>ATLANTIDA</option><option>COLON</option> \
            //            <option>COMAYAGUA</option><option>COPAN</option><option>CORTES</option><option>CHOLUTECA</option><option>EL PARAISO</option><option>FRANCISCO MORAZAN</option> \
            //            <option>GRACIAS A DIOS</option><option>INTIBUCA</option><option>ISLAS DE LA BAHIA</option><option>LA PAZ</option><option>LEMPIRA</option><option>OCOTEPEQUE</option> \
            //            <option>OLANCHO</option><option>SANTA BARBARA</option><option>VALLE</option><option>YORO</option></select></td></tr><tr><td><label>Fondo incorrecto:</label> \
            //            </td><td><input id="txtFonInc_' + acta + '_' + i + '" type="text" placeholder="Escriba el fondo del acta"></td><td><label>Fondo correcto:</label></td><td> \
            //            <input id="txtFonCor_' + acta + '_' + i + '" type="text" placeholder="Escriba el fondo correcto"></td></tr><tr><td colspan="4"><label>Descripción</label> \
            //            </td></tr><tr><td colspan="2"><textarea id="txtDesc_' + acta + '_' + i + '"></textarea></td></tr></tbody></table></fieldset></div>';
        //}

        var html = '<div id="div_' + acta + '_' + i + '"><fieldset><legend>Acta de Cierre con ' + tipo + ' nro ' + i + '</legend><table border="0"><tbody><tr><td><label>Tipo de pago:</label></td><td> \
                    <select id="cbxPago_' + acta + '_' + i + '"><option value="movil">Móvil</option><option value="agencia">Agencia</option></select></td></tr><tr><td> \
                    <label>Departamento:</label></td><td><select id="cbxDpto_' + acta + '_' + i + '"><option>ATLANTIDA</option><option>COLON</option><option>COMAYAGUA</option> \
                    <option>COPAN</option><option>CORTES</option><option>CHOLUTECA</option><option>EL PARAISO</option><option>FRANCISCO MORAZAN</option> \
                    <option>GRACIAS A DIOS</option><option>INTIBUCA</option><option>ISLAS DE LA BAHIA</option><option>LA PAZ</option><option>LEMPIRA</option> \
                    <option>OCOTEPEQUE</option><option>OLANCHO</option><option>SANTA BARBARA</option><option>VALLE</option><option>YORO</option></select> \
                    </td></tr><tr><td colspan="2"><label>Descripción</label></td></tr><tr><td colspan="2"><textarea id="txtDesc_' + acta + '_' + i + '"></textarea></td></tr> \
                    </tbody></table></fieldset></div>';

        //agrega el html al final del div
        $('#div_' + acta).append(html);
    }

    // fin de código para agregar los controles para actas de cierre

    // inicio de código para agregar los controles de los reportes

    countReportes[0] = txtReportesIlegibles.GetText();
    countReportes[1] = txtReportesSinSello.GetText();
    countReportes[2] = txtReportesSinFirma.GetText();

    //si existe por lo menos un control de los reportes con número muestra el div de los reportes
    for (i = 0, long = countReportes.length; i <= long; i++) {
        if (countReportes[i] > 0) {
            $('#divReportes').attr('hidden', false);
            flagReportes = true;
        }
    }

    if (flagReportes) {

        //condición if para agregar los controles para ingresar los datos de los reportes ilegibles
        if (countReportes[0] > 0) {
            for (i = 1; i <= countReportes[0]; i++) {
                //verifica si existe un control con el número del ciclo, esto para no agregar controles repetidos con los mismo id's
                if (!($("#div_REI_" + i).length)) {
                    htmlReportes('sin huella', 'REI', i);
                }
            }
        }

        //condición if para agregar los controles para ingresar los datos de los reportes sin sello
        if (countReportes[1] > 0) {
            for (i = 1; i <= countReportes[1]; i++) {
                if (!($("#div_RESS_" + i).length)) {
                    htmlReportes('reportes sin sello', 'RESS', i);
                }
            }
        }

        //condición if para agregar los controles para ingresar los datos de los reportes sin firma
        if (countReportes[2] > 0) {
            for (i = 1; i <= countReportes[2]; i++) {
                if (!($("#div_RESF_" + i).length)) {
                    htmlReportes('sin firma', 'RESF', i);
                }
            }
        }
    }

    // función para agregar los input al div donde se encuentran los detalles de los reportes, recibe 3 parametros, el primero el el tipo (ej. roto, duplicado, fecha no coincide),
    // el segundo es la abreveación que asignada al tipo de recibo (ej. RSH, RR, RMC) y el ultimo es el número del recibo
    function htmlReportes(tipo, reporte, i) {

        var html = '<div id="div_' + reporte + '_' + i + '"><fieldset><legend>Reporte ' + tipo + ' nro ' + i + '</legend><div><label>Fecha: \
                    <input type="date" id="txtFecha_' + reporte + '_' + i + '"></label><br /><br /><label>Descrición:</label><br /> \
                    <textarea id="txtDsc_' + reporte + '_' + i + '"></textarea></div></fieldset></div><br>';

        //agrega el html al final del div
        $('#div_' + reporte).append(html);
    }

    // fin de código para agregar los controles de los reportes

    // inicio de código para agregar los controles de las notas de cargo

    countNotas[0] = txtNotasSinSello.GetText();
    countNotas[1] = txtNotasSinFirma.GetText();
    countNotas[2] = txtNotasFaltantes.GetText();

    //si existe por lo menos un control de las notas con número muestra el div de las notas
    for (i = 0, long = countNotas.length; i <= long; i++) {
        if (countNotas[i] > 0) {
            $('#divNotasCargo').attr('hidden', false);
            flagNotas = true;
        }
    }

    if (flagNotas) {

        // notas de cargo sin sello
        if (countNotas[0] > 0) {
            for (i = 1; i <= countNotas[0]; i++) {
                if (!($("#div_NCSS_" + i).length)) {
                    htmlNotasCargo('sin sello', 'NCSS', i);
                }
            }
        }

        // notas de cargo sin firma
        if (countNotas[1] > 0) {
            for (i = 1; i <= countNotas[1]; i++) {
                if (!($("#div_NCSF_" + i).length)) {
                    htmlNotasCargo('sin firma', 'NCSF', i);
                }
            }
        }

        // notas de cargo faltantes
        if (countNotas[2] > 0) {
            for (i = 1; i <= countNotas[2]; i++) {
                if (!($("#div_NCF_" + i).length)) {
                    htmlNotasCargo('faltante', 'NCF', i);
                }
            }
        }

    }

    function htmlNotasCargo(tipo, nota, i) {

        var html = '<div id="div_' + nota + '_' + i + '"><fieldset><legend>Nota ' + tipo + ' nro ' + i + '</legend><table><tr><td> \
                    <label>Descripción: </label></td></tr><tr><td><textarea id="txtDesc_' + nota + '_' + i + '"></textarea></td> \
                    </tr></table></fieldset></div>';

        $('#div_' + nota).append(html);
    }

    // fin de código para agregar los controles de las notas de cargo
}


function crearActa() {

    //debugger;

    //var usuario;

    //$.ajax({
    //    url: '/Contraloria/Actas/nombreUsuario',
    //    //type: 'Json',
    //    success: function (response) {
    //        if (response) {
    //            usuario = response;
    //        } else {
    //            alert('problemas al obtener el nombre del usuario.');
    //        }
    //    }
    //});

    var nombre = txtNombre.GetText(),
        dpto = cbxDepartamento.GetText(),
        fchRecepcion = dteInconsistencias.GetDate();

    //obtener fecha larga
    var meses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
    var f = new Date();
    var fecha = f.getDate() + " de " + meses[f.getMonth()] + " de " + f.getFullYear();

    var pdf = new jsPDF('p', 'in', 'letter'),
        font = "times",
        size = 12,
        margin = 1.5,
        verticalOffset = margin,
        arregloString = new Array(),
        reCalcular = false,
        loremipsum;

    pdf.setFontSize(16);
    pdf.setFontType("bold");
    pdf.text(1.5, 1, 'ACTA DE INCONCISTENCIAS CORREGIDAS BONO VIDA MEJOR');
    pdf.setFontType("normal");

    //loremipsum = 'En la Ciudad de Tegucigalpa, Municipio del Distrito Central a los ' + fecha + '. Reunidos en las oficinas de la Secretaía de ';
    //loremipsum += 'Desarrollo e inclusión Social en el Departamento de Contraloría y Cierre, las  siguientes personas en su orden y puesto: ' + usuario + ' en su condición ';
    //loremipsum += 'de Representante de Contraloría y Cierre y ' + nombre + ', en su condición de contralor Bono Vida Mejor. Para llevar a cabo la recepción de ';
    //loremipsum += 'los documentos con errores corregidos del Departamentos de ' + dpto + ' que constan de:\n';

    loremipsum = 'En la Ciudad de Tegucigalpa, Municipio del Distrito Central a los ' + fecha + '. Reunidos en las oficinas de la Secretaía de ';
    loremipsum += 'Desarrollo e inclusión Social en el Departamento de Contraloría y Cierre, las  siguientes personas en su orden y puesto: ' + usuario + ' en su condición ';
    loremipsum += 'de Representante de Contraloría y Cierre y ' + nombre + ', en su condición de contralor Bono Vida Mejor. Para llevar a cabo la recepción de ';
    loremipsum += 'los documentos con errores corregidos';

    if (dpto != "") {
        loremipsum += ' del Departamento de ' + dpto;
    }

    loremipsum += 'que constan de:\n';

    addText(loremipsum, 7.5, 0.7, verticalOffset);


    //contenido del encabezado
    var contenidoEncabezado = loremipsum;
    
    var dd = {
        pageSize: 'letter',
        pageOrientation: 'portrait',
        alignment: 'justify',

        content: [
            {
                text: 'ACTA DE INCONCISTENCIAS CORREGIDAS BONO VIDA MEJOR\n\n',
                style: 'header',
                alignment: 'center'
            },
            contenidoEncabezado
        ],

        styles: {
            header: {
                fontSize: 18,
                bold: true
            }
        }
    }
    
    if (flagRecibos) {

        if (countRecibos[0] > 0) {
            agregarRecibo(countRecibos[0], 'con sin huella', 'RSH', 0);
        }

        if (countRecibos[1] > 0) {
            agregarRecibo(countRecibos[1], 'con referencia ilegible', 'RRI', 1);
        }

        if (countRecibos[2] > 0) {
            agregarRecibo(countRecibos[2], 'con huella ilegible', 'RHI', 2);
        }

        if (countRecibos[3] > 0) {
            agregarRecibo(countRecibos[3], 'sin corresponsabilidad', 'RSC', 3);
        }

        if (countRecibos[4] > 0) {
            agregarRecibo(countRecibos[4], 'sin sello', 'RSS', 4);
        }

        if (countRecibos[5] > 0) {
            agregarRecibo(countRecibos[5], 'con papel reciclado', 'RPR', 5);
        }

        if (countRecibos[6] > 0) {
            agregarRecibo(countRecibos[6], 'mal impreso', 'RMI', 6);
        }

        if (countRecibos[7] > 0) {
            agregarRecibo(countRecibos[7], 'que fecha no coincide', 'RFC', 7);
        }

        if (countRecibos[8] > 0) {
            agregarRecibo(countRecibos[8], 'duplicado', 'RD', 8);
        }

        if (countRecibos[9] > 0) {
            agregarRecibo(countRecibos[9], 'que municipio no coincide', 'RMC', 9);
        }

        if (countRecibos[10] > 0) {
            agregarRecibo(countRecibos[10], 'roto', 'RR', 10);
        }

        if (countRecibos[11] > 0) {
            agregarRecibo(countRecibos[11], 'faltante', 'RF', 11);
        }
    }

    if (flagActas) {

        if (countActas[0] > 0) {

            var countMovil = new Array(),
                countAgencia = new Array();

            for (i = 1, num = countActas[0]; i <= num; i++) {

                if ($('#cbxPago_ACFORI_' + i).val() == "movil") {
                    countMovil.push(i);
                }

                if ($('#cbxPago_ACFORI_' + i).val() == "agencia") {
                    countAgencia.push(i);
                }
            }

            agregarActa(countActas[0], 'formato incorrecto', 'ACFORI', 0, countMovil, countAgencia);
        }
    }

    if (countActas[1] > 0) {

        var countMovil = new Array(),
            countAgencia = new Array();

        for (i = 1, num = countActas[1]; i <= num; i++) {

            if ($('#cbxPago_ACFECI_' + i).val() == "movil") {
                countMovil.push(i);
            }

            if ($('#cbxPago_ACFECI_' + i).val() == "agencia") {
                countAgencia.push(i);
            }
        }

        agregarActa(countActas[1], 'fecha incorrecta', 'ACFECI', 1, countMovil, countAgencia);

    }

    if (countActas[2] > 0) {

        var countMovil = new Array(),
            countAgencia = new Array();

        for (i = 1, num = countActas[2]; i <= num; i++) {

            if ($('#cbxPago_ACFONI_' + i).val() == "movil") {
                countMovil.push(i);
            }

            if ($('#cbxPago_ACFONI_' + i).val() == "agencia") {
                countAgencia.push(i);
            }
        }

        agregarActa(countActas[2], 'fondo incorrecto', 'ACFONI', 2, countMovil, countAgencia);

    }

    if (countActas[3] > 0) {
        agregarActa(countActas[3], 'otros', 'ACO', 3, null, null);
    }
//}

    if (flagReportes) {

        if (countReportes[0] > 0) {
            agregarReporte(countReportes[0], 'ilegible', 'REI', 0);
        }

        if (countReportes[1] > 0) {
            agregarReporte(countReportes[1], 'sin sello', 'RESS', 1);
        }

        if (countReportes[2] > 0) {
            agregarReporte(countReportes[2], 'sin firma', 'RESF', 2);
        }

    }

    //debugger;
    if (flagNotas) {

        if (countNotas[0] > 0) {
            agregarNotaCargo(countNotas[0], 'sin sello', 'NCSS', 0);
        }

        if (countNotas[1] > 0) {
            agregarNotaCargo(countNotas[1], 'sin firma', 'NCSF', 1);
        }

        if (countNotas[2] > 0) {
            agregarNotaCargo(countNotas[2], 'faltante', 'NCF', 2);
        }
    }

    loremipsum = '\nY firman para constancia en la ciudad de Tegucigalpa, Municipio del Distrito Central a los ' + fecha + '.\n\n\n\n';
    loremipsum += '\t\t\t\t_______________\t\t\t\t\t\t______________\n';
    loremipsum += '\t\t\t\t' + usuario + '\t\t\t\t\t\t' + nombre + '   \n';
    loremipsum += '\t\t\t\t   Contraloría y Cierre\t\t\t\t\tContralor\n';

    addText(loremipsum, 7.5, 0.7, verticalOffset);

    dd.content.push('\nY firman para constancia en la ciudad de Tegucigalpa, Municipio del Distrito Central a los ' + fecha + '.');
    dd.content.push(
        {
            alignment: 'center',
            columns: [
                {
                    text: '\n\n\n___________________\n' + usuario + '\nContraloría y Cierre'
                },
                {
                    text: '\n\n\n___________________\n' + nombre + '\nContralor de Pago'
                }
            ]
        }
    );

    //var string = pdf.output('datauristring');
    //$('iframe').attr('src', string);

    pdfMake.createPdf(dd).getDataUrl(function (data) {
        $('iframe').attr('src', data);
    });


    function agregarRecibo(numRecibos, tipo, recibo, num) {

        loremipsum = '\n* ' + countRecibos[num] + ' Recibos ' + tipo + '.';
        addText(loremipsum, 7, 1, verticalOffset);
        //pdf.text(1, verticalOffset, loremipsum);

        dd.content.push(
            {
                text: loremipsum,
                margin: [15, 0]
            }
        );

        for (i = 1; i <= numRecibos; i++) {
            loremipsum = "";
            loremipsum += '- Recibo ' + tipo + ' nro ' + i + ' (' + $('#txtRef_' + recibo + '_' + i).val() + ').';
            addText(loremipsum, 6.5, 1.3, verticalOffset);

            dd.content.push(
                {
                    text: loremipsum,
                    margin: [25, 0]
                }
            );
        }
        //addText(loremipsum, 6.5, 1.3, verticalOffset);
    }

    //función que construye el texto para agregar el detalle de las actas de cierre al acta de inconsistencias
    function agregarActa(numActa, tipo, acta, num, countMovil, countAgencia) {

        if (tipo == "formato incorrecto" || tipo == "fecha incorrecta" || tipo == "fondo incorrecto") {

            if (tipo == "formato incorrecto") {
                // encabezado para las actas de cierre con formato incorrecto

                //loremipsum = 'Las Actas de Cierre presentan una diferencia en el formato de las columnas del cuadro resumen del pago, debiendo ser lo correcto la columna de lo programado primera, luego ';
                //loremipsum += 'la columna de lo ejecutado segunda y las columna de lo no ejecutado de tercera. Siendo este el formato estandar para las Actas de Cierre. Los departamentos que aparecen ';
                //loremipsum += 'con esta inconsistenica son los siguientes:';
                //addText(loremipsum, 7, 1, verticalOffset);

                loremipsum = '\nSe corrigió el formato de las columnas del cuadro resumen en las Actas de Cierre de los siguientes Departamentos:';
                addText(loremipsum, 7, 1, verticalOffset);

                dd.content.push(
                    {
                        text: loremipsum,
                        margin: [15, 0]
                    }
                );
            }

            if (tipo == "fecha incorrecta") {
                // encabezado para las actas de cierre con fecha incorrecta

                //loremipsum = 'Los siguientes Departamentos presentan las Actas de Cierre con fecha incorrecta:';
                //addText(loremipsum, 7, 1, verticalOffset);

                loremipsum = '\nSe corrigió la fecha en las Actas de Cierre de los Departamentos:';
                addText(loremipsum, 7, 1, verticalOffset);

                dd.content.push(
                    {
                        text: loremipsum,
                        margin: [15, 0]
                    }
                );
            }

            if (tipo == "fondo incorrecto") {
                // encabezado para las actas de cierre con el fondo incorrecto

                //loremipsum = 'Los siguientes Departamentos presentan en el Acta de Cierre el Fondo Incorrecto:';
                //addText(loremipsum, 7, 1, verticalOffset);

                loremipsum = '\nSe corrigió el fondo en las Acta de Cierre de los siguientes Departamentos:';
                addText(loremipsum, 7, 1, verticalOffset);

                dd.content.push(
                    {
                        text: loremipsum,
                        margin: [15, 0]
                    }
                );
            }

            // condición para agregar las actas de pago móvil con inconsistencias
            if (countMovil.length > 0) {
                loremipsum = '\nDel pago Cajero Móvil:';
                addText(loremipsum, 6.5, 1.3, verticalOffset);

                dd.content.push(
                    {
                        text: loremipsum,
                        margin: [25, 0]
                    }
                );

                // ciclo para agregar cada una de las actas
                for (i = 1; i <= countMovil.length; i++) {

                    // variable para obtener el número de control almacenado en el arreglo.
                    var actaNum = countMovil[i - 1];

                    loremipsum = i + '. ' + $('#cbxDpto_' + acta + '_' + actaNum).val();

                    // condición para identificar si la inconsistencia es de fondo incorrecto para agregar el detalle de los fondos
                    //if (tipo == "fondo incorrecto") {

                    //    //loremipsum += ', el Acta de Cierre aparece con el fondo ' + $('#txtFonInc_' + acta + '_' + actaNum).val() + ' siendo el Fondo correcto ';
                    //    //loremipsum += $('#txtFonCor_' + acta + '_' + actaNum).val() + ' de acuerdo al sistema SIG, reportes y recibos.';

                    //    loremipsum += ', el Acta de Cierre ';
                    //}

                    // condición para agregar la descripción existente
                    var desc = $('#txtDesc_' + acta + '_' + actaNum).val();
                    if (desc != "") {
                        loremipsum += ' (' + desc + ')';
                    }
                    addText(loremipsum, 6.5, 1.5, verticalOffset);

                    dd.content.push(
                        {
                            text: loremipsum,
                            margin: [30, 0]
                        }
                    );
                }
            }

            // condición para agregar las actas de pago de agencia con inconsistencias
            if (countAgencia.length > 0) {
                loremipsum = '\nDel pago de Agencia:';
                addText(loremipsum, 6.5, 1.3, verticalOffset);

                dd.content.push(
                    {
                        text: loremipsum,
                        margin: [25, 0]
                    }
                );

                for (i = 1; i <= countAgencia.length; i++) {

                    // variable para obtener el número de control almacenado en el arreglo.
                    var actaNum = countAgencia[i - 1];

                    loremipsum = i + '. ' + $('#cbxDpto_' + acta + '_' + actaNum).val();

                    //if (tipo == "fondo incorrecto") {
                    //    loremipsum += ', el Acta de Cierre aparece con el fondo ' + $('#txtFonInc_' + acta + '_' + actaNum).val() + ' siendo el Fondo correcto ';
                    //    loremipsum += $('#txtFonCor_' + acta + '_' + actaNum).val() + ' de acuerdo al sistema SIG, reportes y recibos.';
                    //}

                    var desc = $('#txtDesc_' + acta + '_' + actaNum).val();
                    if (desc != "") {
                        loremipsum += ' (' + desc + ')';
                    }
                    addText(loremipsum, 6.5, 1.5, verticalOffset);

                    dd.content.push(
                        {
                            text: loremipsum,
                            margin: [30, 0]
                        }
                    );
                }
            }
        } else {

            for (i = 1; i <= numActa; i++) {

                loremipsum = '\nEl Acta de Cierre de pago ';

                if ($('#cbxPago_' + acta + '_' + i).val() == "movil") {
                    loremipsum += 'cajero móvil ';
                } else {
                    loremipsum += 'de agencia ';
                }

                loremipsum += 'del Departamento de ' + $('#cbxDpto_' + acta + '_' + i).val() + ' ' + $('#txtDesc_' + acta + '_' + i).val();
                addText(loremipsum, 7, 1, verticalOffset);

                dd.content.push(
                    {
                        text: loremipsum,
                        margin: [15, 0]
                    }
                );
            }
        }
    }

    // función que contruye el texto para agregar el reporte al acta
    function agregarReporte(numReportes, tipo, reporte, num) {

        loremipsum = '\nSe corrigío' + countReportes[num] + ' Reporte ' + tipo + '.';
        addText(loremipsum, 7, 1, verticalOffset);

        dd.content.push(
            {
                text: loremipsum,
                margin: [15, 0]
            }
        );

        for (i = 1; i <= numReportes; i++) {
            loremipsum = '';
            loremipsum += '- Reporte ' + tipo + ' de la fecha: ' + $('#txtFecha_' + reporte + '_' + i).val() + '; ' + $('#txtDsc_' + reporte + '_' + i).val();
            addText(loremipsum, 6.5, 1.3, verticalOffset);

            dd.content.push(
                {
                    text: loremipsum,
                    margin: [25, 0]
                }
            );
        }
    }

    function agregarNotaCargo(numNotas, tipo, nota, num) {
        //debugger;
        loremipsum = '\nSe corrigío ' + countNotas[num] + ' Notas de Cargo ' + tipo + '.';
        addText(loremipsum, 7, 1, verticalOffset);

        dd.content.push(
            {
                text: loremipsum,
                margin: [15, 0]
            }
        );

        for (i = 1; i <= numNotas; i++) {
            loremipsum = '';
            loremipsum += '- Nota de Cargo ' + tipo + '\n' + $('#txtDesc_' + nota + '_' + i).val();
            addText(loremipsum, 6.5, 1.3, verticalOffset);

            dd.content.push(
                {
                    text: loremipsum,
                    margin: [25, 0]
                }
            );
        }
    }

    
    function addText(parrafo, largo, x, y) {
        var lines = pdf.setFont(font)
                    .setFontSize(size)
                    .splitTextToSize(parrafo, largo);

        pdf.text(x, y + size / 72, lines);
        //alert(verticalOffset + ' +=  (' + lines.length + ' + 0.5) * ' + (size + 1.15) + ' / 72');
        verticalOffset += (lines.length + 0.5) * (size + 1.15) / 72;
        if (verticalOffset > 9) {
            pdf.addPage();
            verticalOffset = 0.5;
        }
    }


    
}