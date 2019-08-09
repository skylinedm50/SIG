


$(document).ready(function () {

    var countRecibos = new Array(),
        countReportes = new Array(),
        countNotas = new Array(),
        flagRecibos;

    $('#<%=btnVerActa.ClientID()%>').click(function (event) {
        crearActa();
        //alert('hola');
    });

    $('#<%=btnDetalles.ClientID()%>').click(function (event) {
        countRecibos[0] = $('#<%=txtRSH.ClientID()%>').find("input").val();
        countRecibos[1] = $('#<%=txtRRI.ClientID()%>').find("input").val();
        countRecibos[2] = $('#<%=txtRHI.ClientID()%>').find("input").val();
        countRecibos[3] = $('#<%=txtRSC.ClientID()%>').find("input").val();
        countRecibos[4] = $('#<%=txtRSS.ClientID()%>').find("input").val();
        countRecibos[5] = $('#<%=txtRPR.ClientID()%>').find("input").val();
        countRecibos[6] = $('#<%=txtRMI.ClientID()%>').find("input").val();
        countRecibos[7] = $('#<%=txtRFC.ClientID()%>').find("input").val();
        countRecibos[8] = $('#<%=txtRD.ClientID()%>').find("input").val();
        countRecibos[9] = $('#<%=txtRMC.ClientID()%>').find("input").val();
        countRecibos[10] = $('#<%=txtRR.ClientID()%>').find("input").val();

        //si existe por lo menos un control de los recibos con número muestra el de los recibos
        for (i = 0; i <= countRecibos.length; i++) {
            if (countRecibos[i] > 0) {
                $('#divRecibos').attr('hidden', false);
                flagRecibos = true;
            }
        }

        if (flagRecibos) {

            if (countRecibos[0] > 0) {
                for (i = 1; i <= countRecibos[0]; i++) {
                    //verifica si existe un control con el número del ciclo, esto para no agregar controles repetidos con los mismo id's
                    if (!($("#div_RSH_" + i).length)) {
                        htmlRecibos('sin huella', 'RSH', i);
                    }
                }
            }

            if (countRecibos[1] > 0) {
                for (i = 1; i <= countRecibos[1]; i++) {
                    if (!($("#div_RRI_" + i).length)) {
                        htmlRecibos('con referencia ilegible', 'RRI', i);
                    }
                }
            }

            if (countRecibos[2] > 0) {
                for (i = 1; i <= countRecibos[2]; i++) {
                    if (!($("#div_RHI_" + i).length)) {
                        htmlRecibos('con huella ilegible', 'RHI', i);
                    }
                }
            }

            if (countRecibos[3] > 0) {
                for (i = 1; i <= countRecibos[3]; i++) {
                    if (!($("#div_RSC_" + i).length)) {
                        htmlRecibos('sin corresponsabilidad', 'RSC', i);
                    }
                }
            }

            if (countRecibos[4] > 0) {
                for (i = 1; i <= countRecibos[4]; i++) {
                    if (!($("#div_RSS_" + i).length)) {
                        htmlRecibos('sin sello', 'RSS', i);
                    }
                }
            }

            if (countRecibos[5] > 0) {
                for (i = 1; i <= countRecibos[5]; i++) {
                    if (!($("#div_RPR_" + i).length)) {
                        htmlRecibos('con papel reciclado', 'RPR', i);
                    }
                }
            }

            if (countRecibos[6] > 0) {
                for (i = 1; i <= countRecibos[6]; i++) {
                    if (!($("#div_RMI_" + i).length)) {
                        htmlRecibos('mal impreso', 'RMI', i);
                    }
                }
            }

            if (countRecibos[7] > 0) {
                for (i = 1; i <= countRecibos[7]; i++) {
                    if (!($("#div_RFC_" + i).length)) {
                        htmlRecibos('que fecha no coincide', 'RFC', i);
                    }
                }
            }

            if (countRecibos[8] > 0) {
                for (i = 1; i <= countRecibos[8]; i++) {
                    if (!($("#div_RD_" + i).length)) {
                        htmlRecibos('duplicado', 'RD', i);
                    }
                }
            }

            if (countRecibos[9] > 0) {
                for (i = 1; i <= countRecibos[9]; i++) {
                    if (!($("#div_RMC_" + i).length)) {
                        htmlRecibos('que municipio no coincide', 'RMC', i);
                    }
                }
            }

            if (countRecibos[10] > 0) {
                for (i = 1; i <= countRecibos[10]; i++) {
                    if (!($("#div_RR_" + i).length)) {
                        htmlRecibos('roto', 'RR', i);
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
    });

    function crearActa() {

        var nombre = $('#<%=txtNombre.ClientID()%>').find("input").val(),
            dpto = cbxDpto.GetText(),
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

        loremipsum = 'En la Ciudad de Tegucigalpa, Municipio del Distrito Central a los ' + fecha + '. Reunidos en las oficinas de la Secretaía de ';
        loremipsum += 'Desarrollo e inclusión Social en el Departamento de Contraloría y Cierre, las  siguientes personas en su orden y puesto: [usuario] en su condición ';
        loremipsum += 'de Representante de Contraloría y Cierre y ' + nombre + ', en su condición de contralor Bono Vida Mejor. Para llevar a cabo la recepción de ';
        loremipsum += 'los documentos con errores corregidos del Departamentos de ' + dpto + ' que contan de:\n';

        addText(loremipsum, 7.5, 0.7, verticalOffset);

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
        }

        loremipsum = '\nY firman para constancia en la ciudad de Tegucigalpa, Municipio del Distrito Central a los ' + fecha + '.\n\n\n\n';
        loremipsum += '\t\t\t\t_______________\t\t\t\t\t\t______________\n';
        loremipsum += '\t\t\t\t    [usuario]  \t\t\t\t\t\t    ' + nombre + '   \n';
        loremipsum += '\t\t\t\t   Contraloría y Cierre\t\t\t\t\tContralor\n';

        addText(loremipsum, 7.5, 0.7, verticalOffset);

        var string = pdf.output('datauristring');
        $('iframe').attr('src', string);


        function agregarRecibo(numRecibos, tipo, recibo, num) {

            loremipsum = '*' + countRecibos[num] + ' Recibos ' + tipo + '.';
            addText(loremipsum, 7, 1, verticalOffset);
            //pdf.text(1, verticalOffset, loremipsum);

            for (i = 1; i <= numRecibos; i++) {
                loremipsum = "";
                loremipsum += '- Recibo ' + tipo + ' nro ' + i + ' (' + $('#txtRef_' + recibo + '_' + i).val() + ').';
                addText(loremipsum, 6.5, 1.3, verticalOffset);
            }
            //addText(loremipsum, 6.5, 1.3, verticalOffset);
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


        /*
        function agregarRecibo(numRecibos, tipo, recibo) {

            pdf.text(1, verticalOffset + size / 72, '*' + countRecibos[0] + ' Recibos ' + tipo + '.\n');
            verticalOffset += 0.2;
            loremipsum = "";

            for (i = 1; i <= numRecibos; i++) {
                loremipsum += '- Recibo ' + tipo + ' nro ' + i + ' (' + $('#txtRef_' + recibo + '_' + i).val() + ').\n';
            }

            for (var i in fonts) {
                if (fonts.hasOwnProperty(i)) {
                    font = fonts[i]
                    size = sizes[i]

                    lines = pdf.setFont(font[0], font[1])
                                .setFontSize(size)
                                .splitTextToSize(loremipsum, 6.5) //el número es para hacer mas larga o corta la linea
                    pdf.text(1.3, verticalOffset + size / 72, lines)
                    verticalOffset += (lines.length + 0.5) * size / 72
                    if (verticalOffset >= 9.7) {
                        pdf.addPage();
                        verticalOffset = 0.5;
                    }
                    //alert(verticalOffset);
                }
            }
        }
        */
    }
});