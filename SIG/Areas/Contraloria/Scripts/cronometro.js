var miCrono = null;
var enmarcha = false;
var segundos, minutos, horas;
var puntos;

function detener() {
    if (enmarcha) {
        clearTimeout(miCrono);
        enmarcha = false;
    }
}

function ponerAceros() { //inicializa contadores globales{
    //puntos = false;
    segundos = 0;
    minutos = 0;
    horas = 0;
    //document.crono.display.value = "00:00:00";
}

function mostrar() {//incrementa el crono y configura la salida{
    if (puntos == true) {
        segundos++;
        if (segundos > 59) {
            segundos = 0;
            minutos++;
            if (minutos > 59) {
                minutos = 0;
                horas++;
                if (horas > 99) {
                    alert("No hay más tiempo...");
                    detener();
                    return true;
                }
            }
        }
    }

    var texto = "";
    horas = horas.toString();

    if (horas.length == 1) horas = "0" + horas;
    minutos = minutos.toString();

    if (minutos.length == 1) minutos = "0" + minutos;
    segundos = segundos.toString();

    if (segundos.length == 1) segundos = "0" + segundos;

    if (puntos == false) {
        //texto += horas + " " + minutos + " " + segundos;
        //document.crono.display.value = texto;
        texto += horas + ":" + minutos + ":" + segundos;
        lblCronometro.SetText(texto);
        puntos = true;
    } else {
        texto += horas + ":" + minutos + ":" + segundos;
        //document.crono.display.value = texto;
        lblCronometro.SetText(texto);
        puntos = false;
    }

    miCrono = setTimeout("mostrar()", 500);
    enmarcha = true;
    return true;
}

function iniciar() {
    puntos = false;
    mostrar();
}