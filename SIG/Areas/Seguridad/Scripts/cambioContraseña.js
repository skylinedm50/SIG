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
                lblmensaje.SetText("Contraseña actualizada correctamente");
                popupMensaje.Show();
            
            } else {
               
                lblmensaje.SetText("Hay un error, intente nuevamente");
                popupMensaje.Show();
            }

        }
    });

}



function txtConfirmacionClaveChange(s, e) {
    if (ratingControl.GetValue() > 3) {
        if (txtClave.GetText() == txtConfirmacionClave.GetText()) {
            btnActualizar.SetEnabled(true);
            lblMensaje2.SetVisible(true);
        } else {
            lblMensaje2.SetVisible(false);
            btnActualizar.SetEnabled(false);
        }
    } else {
        btnActualizar.SetEnabled(false);
        lblMensaje2.SetVisible(false);
    }
}


var passwordMinLength = 8;
function GetPasswordRating(password) {
    var result = 0;
    if (password) {
        result++;
        if (password.length >= passwordMinLength) {
            if (/[a-z]/.test(password))
                result++;
            if (/[A-Z]/.test(password))
                result++;
            if (/\d/.test(password))
                result++;
            if (!(/^[a-z0-9]+$/i.test(password)))
                result++;
        }
    }
    return result;
}

function OnPasswordTextBoxInit(s, e) {
    ApplyCurrentPasswordRating();

    
}

function OnPassChanged(s, e) {
    ApplyCurrentPasswordRating();
    if (ratingControl.GetValue() > 3) {
        if (txtClave.GetText() == txtConfirmacionClave.GetText()) {
            btnActualizar.SetEnabled(true);
            lblMensaje2.SetVisible(true);
        } else {
          //  lblMensaje2.SetVisible(true);
            btnActualizar.SetEnabled(false);
        }
    } else {
        btnActualizar.SetEnabled(false);
        lblMensaje2.SetVisible(false);
    }
}

function ApplyCurrentPasswordRating() {
    var password = txtClave.GetText();
    var passwordRating = GetPasswordRating(password);
    ApplyPasswordRating(passwordRating);
}

function ApplyPasswordRating(value) {
    ratingControl.SetValue(value);
    switch (value) {
        case 0:
            ratingLabel.SetValue("");
            break;
        case 1:
            ratingLabel.SetValue("Muy simple");
            break;
        case 2:
            ratingLabel.SetValue("No segura");
            break;
        case 3:
            ratingLabel.SetValue("Normal");
            break;
        case 4:
            ratingLabel.SetValue("Segura");
            break;
        case 5:
            ratingLabel.SetValue("Muy Segura");
            break;
        default:
            ratingLabel.SetValue("");
    }
}