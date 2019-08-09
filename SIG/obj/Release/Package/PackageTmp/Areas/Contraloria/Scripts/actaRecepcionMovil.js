

function btnVerActaClick(s, e) {
    //debugger;
    crearActa();
}

function usuario() {
    $.ajax({
        url: '/Contraloria/Actas/nombreUsuario',
        type: 'Json',
        success: function (response) {
            if (response) {
                return response;
            } else {
                alert('problemas al obtener el nombre del usuario.');
            }
        }
    })
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

    var nombre = txtNombre.GetText(),
        actas = cbxActas.GetText(),
        reportes = cbxReportes.GetText(),
        banco = cbxBanco.GetText(),
        recibos = cbxRecibos.GetText(),
        notas = cbxNotasCargo.GetText(),
        dpto = cbxDpto.GetText(),
        fondo = cbxFondo.GetText(),
        periodo = cbxPeriodo.GetText();

    //obtener fecha larga
    var meses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
    var f = new Date();
    var fecha = f.getDate() + " dias de " + meses[f.getMonth()] + " de " + f.getFullYear();

    var pdf = new jsPDF('p', 'in', 'letter')
    , sizes = [12]
    , fonts = [['Times', 'Roman']]
    , font, size, lines, loremipsum
    , margin = 1.5 // inches on a 8.5 x 11 inch sheet.
    , verticalOffset = margin

    pdf.setFontSize(16);
    pdf.setFontType("bold");
    pdf.text(2, 1, 'ACTA DE RECEPCIÓN BONO VIDA MEJOR');
    pdf.setFontType("normal");

    loremipsum = 'En la Ciudad de Tegucigalpa, Municipio del Distrito Central a los ' + fecha + ', ';
    loremipsum += 'Reunidos en las oficinas de La Sub Secretaria de Desarrollo e Inclusión Social, ';
    loremipsum += 'en el Departamento de Contraloría y Cierre de Bonos Fondos Externos las siguientes ';
    loremipsum += 'personas en su orden y puesto: ' + usuario + ' y ' + nombre + '; la primera en su condición ';
    loremipsum += 'de Representante de Contraloría y Cierre de Bonos Fondos Externos y la segunda en su ';
    loremipsum += 'condición de Contralor de Pago del Proyecto "Bono Vida Mejor". Para llevar a cabo la ';
    loremipsum += 'recepción de los siguientes documentos: ' + actas + ' Acta de Cierre, ' + reportes + ' Reportes de ' + banco + ', ';
    //loremipsum += recibos + ' copia de recibos por cada fecha, ' + notas + ' Notas de Cargo de fechas del departamentos de ';
    loremipsum += recibos + ' copia de recibos por cada fecha, ' + notas + ' Notas de Cargo de fechas del departamentos de ';

    if (recibos > 0) {
        loremipsum += recibos + ' copia de recibos por cada fecha, ';
    }

    loremipsum += notas + ' Notas de Cargo de fechas del departamentos de ';
    loremipsum += dpto + '. Es de observar que todo el proceso de pago se efectuó con fondos ' + fondo + ', ';
    //loremipsum += 'correspondiente a la ' + entrega + ' entrega del ' + f.getFullYear() + '. Esta documentación resulta de la ';
    loremipsum += 'correspondiente al período' + periodo + '. Esta documentación resulta de la ';
    loremipsum += 'entrega de Transferencias Monetarias Condicionadas, la que  será sometida a un proceso ';
    loremipsum += 'de revisión en la cual se verificarán las cantidades y montos de acuerdo con la asignación ';
    loremipsum += 'establecida para cada municipio. En el caso de encontrarse inconsistencias en los documentos ';
    loremipsum += 'antes señalados, los mismos serán devueltos al Contralor del pago, para subsanar aquellos ';
    loremipsum += 'errores ya sean estos de forma o de fondo que se pudieran haber dado con el proceso de pago a ';
    loremipsum += 'través del Banco.\n\nY, firma para constancia en la ciudad de Tegucigalpa, Municipio del Distrito Central a los [fecha].\n\n\n';

    var contenido = loremipsum;

    for (var i in fonts) {
        if (fonts.hasOwnProperty(i)) {
            font = fonts[i]
            size = sizes[i]

            lines = pdf.setFont(font[0], font[1])
                        .setFontSize(size)
                        .splitTextToSize(loremipsum, 7.5) //el número es para hacer mas larga o corta la linea
            pdf.text(0.5, verticalOffset + size / 72, lines)
            verticalOffset += (lines.length + 0.5) * size / 72
        }
    }

    pdf.text(1.4, verticalOffset + 1.2, '__________________					    	__________________');
    pdf.text(1.4, verticalOffset + 1.4, ' ' + usuario + '							   	 ' + nombre);
    pdf.text(1.4, verticalOffset + 1.6, 'Contraloría y Cierre 					                 Contralor de Pago');

    //var string = pdf.output('datauristring');
    //$('iframe').attr('src', string);



    var dd = {
        pageSize: 'letter',
        pageOrientation: 'portrait',
        alignment: 'justify',

        content: [
            {
                text: 'ACTA DE RECEPCIÓN BONO VIDA MEJOR\n\n',
                style: 'header',
                alignment: 'center'
            },
            contenido,
            {
                alignment: 'center',
                columns: [
                    {
                        text: '________________\n' + usuario + '\nContraloría y Cierre'
                    },
                    {
                        text: '________________\n' + nombre + '\nContralor de Pago'
                    }
                ]
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