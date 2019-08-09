
function btnVerActaClick(s, e) {
    //debugger;
    crearActa();
}

function crearActa() {

    //debugger;
    //var usuario;
    //$.ajax({
    //    url: '/Contraloria/Actas/nombreUsuario',
    //    type: 'Json',
    //    success: function (response) {
    //        if (response) {
    //            usuario = response;
    //        } else {
    //            alert('problemas al obtener el nombre del usuario.');
    //        }
    //    }
    //});

    // datos introducidos por el usuario
    var nombre = txtNombre.GetText(),
        grado = cbxGrado.GetText(),
        actas = cbxActas.GetText(),
        reportes = cbxReportes.GetText(),
        proyecto = cbxProyecto.GetText(),
        banco = cbxBanco.GetText(),
        agencia = cbxSucursal.GetText(),
        inversion = txtInversion.GetText(),
        recibos = cbxRecibos.GetText(),
        notas = cbxNotasCargo.GetText(),
        dpto = cbxDpto.GetText(),
        fondo = cbxFondo.GetText(),
        entrega = cbxPeriodo.GetText(),
        inicio = dteFechaInicio.GetDate(),
        fin = dteFechaFin.GetDate();

    //obtener fecha larga
    var meses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
    var f = new Date();
    var fecha = f.getDate() + " de " + meses[f.getMonth()] + " de " + f.getFullYear();

    /*
    var dd = {
        pageSize: 'letter',
        pageOrientation: 'portrait',

        content: [
        ],

        styles: {
        }
    }
    */
         
    var pdf = new jsPDF('p', 'in', 'letter')
    , sizes = [12]
    , fonts = [['Times', 'Roman']]
    , font, size, lines, loremipsum
    , margin = 1 // inches on a 8.5 x 11 inch sheet.
    , verticalOffset = margin

    // \n para el cambio de linea, \t para la tabulación
    loremipsum = '\n\nTegucigalpa, M.D.C. ' + fecha + '\n\n' + grado + '\n' + nombre + '\nCoordinador Proyecto ' + proyecto + '\n\n';
    loremipsum += 'Presente\nEstimado ' + grado + ' ' + nombre + ':\n\nDe acuerdo a la documentación entregada el ' + fecha;
    loremipsum += ' se llevó a cabo la recepción de los documentos del Proyecto ' + proyecto + ' pago cajero móvil pagado como ';
    loremipsum += 'Agencia realizado por ' + banco + ' Correspondiente a la ' + entrega + ' entrega pagado con fondos ' + fondo;
    loremipsum += 'los cuales se detallan a continuación:\n\n\t' + dpto + ', Agencia ' + agencia + ', presenta los siguientes ';
    loremipsum += 'documentos: ' + actas + ' acta de cierre, ' + notas + ' notas de cargo, ' + reportes + ' de la fecha ';
    loremipsum += inicio.getDate() + ' de ' + meses[inicio.getMonth()] + ' de ' + inicio.getFullYear() + ' al ';
    loremipsum += fin.getDate() + ' de ' + meses[fin.getMonth()] + ' de ' + fin.getFullYear() + ', ' + recibos + ' recibos ';
    loremipsum += 'pagados una copia por fecha. Inversión de ' + inversion + '.\n\nEn el caso de encontrarse inconsistencias en los documentos ';
    loremipsum += 'antes señalados, mismos serán devueltos al coordinador para subsanar aquellos errores ya sean estos de forma o fondo que se pudieran ';
    loremipsum += 'haber dado con el proceso de pago a través del Banco.\n\nAgradeciendo su atención a la presente y esperando que esta ';
    loremipsum += 'información sea de conformidad, me suscribo.\n\nAtentamente.\n\nCC. ' + grado + ' ' + nombre + ' coordinador proyecto ';
    loremipsum += proyecto + '.\n\nCC. Contraloría y Cierre SSIS/PRAF.';

    var contenido = loremipsum;

    for (var i in fonts) {
        if (fonts.hasOwnProperty(i)) {
            font = fonts[i]
            size = sizes[i]

            lines = pdf.setFont(font[0], font[1])
                        .setFontSize(size)
                        .splitTextToSize(loremipsum, 7.5) //el número es para hacer mas larga o corta la linea
            pdf.text(0.7, verticalOffset + size / 72, lines)
            verticalOffset += (lines.length + 0.5) * size / 72
        }
    }

    pdf.text(3.2, verticalOffset + 2.2, '__________________');
    pdf.text(3.2, verticalOffset + 2.4, usuario);
    pdf.text(2.6, verticalOffset + 2.6, 'Auxiliar de Contraloría y Cierre SSIS/PRAF');

    //var string = pdf.output('datauristring');
    //$('iframe').attr('src', string);


    var dd = {
        pageSize: 'letter',
        pageOrientation: 'portrait',
        alignment: 'justify',

        content: [
            contenido,
            {
                text: '\n\n\n__________________\n' + usuario + '\nAuxiliar de Contrloría y Cierre SSIS/PRAF',
                alignment: 'center'
            }
        ],

        styles: {
            header: {
                fontSize: 18,
                bold: true
            }
        }
    }

    pdfMake.createPdf(dd).getDataUrl(function (data) {
        $('iframe').attr('src', data);
    });
}