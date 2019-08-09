$(document).ready(function () {

    $('#MenuContainer').hide();

});

function btnActualizarClick(s, e) {

    $.ajax({
        url: '/Seguridad/Usuarios/actualizarContrasena',
        type: 'POST',
        async: false,
        //dataType: 'json',
        data: { pass: txtClave.GetText() },
        success: function (response) {

            if (response == 1) {
                alert("Contraseña actualizada correctamente.");
                window.location = "/Home/Login";
            } else {
                alert("No se pudo actualizar la contraseña.");
            }

        }
    });

}

function btnCancelarClick(s, e) {

    

}

function txtConfirmacionClaveChange(s, e) {

    if (txtClave.GetText() == txtConfirmacionClave.GetText()) {
        btnActualizar.SetEnabled(true);
        lblMensaje2.SetVisible(false);
    } else {
        lblMensaje2.SetVisible(true);
    }

}

function txtClaveChange(s, e) {

    var pass = txtClave.GetText();

    if (pass.length < 8) {
        lblMensaje.SetText('La contraseña debe poseer al menos 8 caracteres.');
        lblMensaje.SetVisible(true);
    } else if (tiene_mayusculas(pass) == 0) {
        lblMensaje.SetText('La contraseña debe poseer al menos una letra mayuscula.');
        lblMensaje.SetVisible(true);
    } else if (tiene_minusculas(pass) == 0) {
        lblMensaje.SetText('La contraseña debe poseer al menos una letra minuscula.');
        lblMensaje.SetVisible(true);
    } else if (tiene_numeros(pass) == 0) {
        lblMensaje.SetText('La contraseña debe tener al menos un número.');
        lblMensaje.SetVisible(true);
    } else {
        lblMensaje.SetText();
        lblMensaje.SetVisible(false);
    }   
}

function tiene_numeros(texto) {

    var numeros = "0123456789";

    for (i = 0; i < texto.length; i++) {
        if (numeros.indexOf(texto.charAt(i), 0) != -1) {
            return 1;
        }
    }
    return 0;
}

function tiene_minusculas(texto) {

    var letras = "abcdefghyjklmnñopqrstuvwxyz";

    for (i = 0; i < texto.length; i++) {
        if (letras.indexOf(texto.charAt(i), 0) != -1) {
            return 1;
        }
    }
    return 0;
}

function tiene_mayusculas(texto) {

    var letras_mayusculas = "ABCDEFGHYJKLMNÑOPQRSTUVWXYZ";

    for (i = 0; i < texto.length; i++) {
        if (letras_mayusculas.indexOf(texto.charAt(i), 0) != -1) {
            return 1;
        }
    }
    return 0;
}

