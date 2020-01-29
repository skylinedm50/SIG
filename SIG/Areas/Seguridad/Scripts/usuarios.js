
var passwordMinLength = 8;
var id_roles;
var cod_usr;

function OnInit(s, e) {
   
    rbNo.SetChecked(true);
    rbSi.SetChecked(false);
}

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
}

function ApplyCurrentPasswordRating() {
    var password = passwordTextBox.GetText();
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

function validarCampos() {
    if (txtIdentidad.GetText().length < 13) {
        alert("El número de identidad debe tener 13 dígitos");
    } else if (!(/\d/.test(txtIdentidad.GetText()))) {
        alert("No es un número de identidad");
    } else if (txtNombre1.GetText().length == 0) {
        alert("Ingrese el primer nombre");
    } else if (txtApellido1.GetText().length == 0) {
        alert("Ingrese el primer apellido");
    } else if (txtTelefono.GetText().length <= 7) {
        alert("El número de telefono debe poseer al menos 7 dígitos.");
    } else if (txtCorreo.GetText().length == 0 || txtCorreo.GetText().search('@') == -1 || txtCorreo.GetText().search('.') == -1) {
        alert("El correo debe tener un formato: ejemplo@dominio.com");
    } else if (txtCorreoConf.GetText() !== txtCorreo.GetText()) {
        alert("El correo no concuerda con su confirmación");
    } else if (typeof id_roles === "undefined") {
        alert("Debe seleccionar al menos un rol");
        id_roles = 0;
    } else if (id_roles == 0) {
        alert("Debe seleccionar al menos un rol");
    } else {
        return true;
    }
    return false;
}



function Prueba(s, e) {
    s.GetSelectedFieldValues("id_rol", GetSelectedFieldValuesCallback);
}

function GetSelectedFieldValuesCallback(value) {
    id_roles = value;
}

function btnRegistrarClick() {

    if (validarCampos()) {
        identidad = txtIdentidad.GetText();
        nombre1 = txtNombre1.GetText();
        nombre2 = txtNombre2.GetText();
        apellido1 = txtApellido1.GetText();
        apellido2 = txtApellido2.GetText();
        telefono = txtTelefono.GetText();
        correo = txtCorreo.GetText();
        nroCritico = chkNroCritico.GetText();
        id_unidad = CbProyecto.GetValue();
        if (rbSi.GetChecked()) {
            jefe = 1;
        } else { jefe = 0;}
        $.ajax({
            url: '/Seguridad/Usuarios/RegistrarUsuario',
            type: 'POST',
            async: false,
            dataType: 'json',
            traditional: true,
            data: {
                identidad: identidad,
                nombre1: nombre1,
                nombre2: nombre2,
                apellido1: apellido1,
                apellido2: apellido2,
                telefono: telefono,
                correo: correo,
                nroCritico: nroCritico,
                roles: id_roles,
                jefe: jefe,
                id_unidad: id_unidad
            },
            success: function (response) {
                if (response) {
                    if (response = 1) {
                        lblmensaje.SetText("El usuario se ha registrado exitosamente, se ha enviado un correo eléctronico al usuario con su usuario y contraseña generada aleatoriamente para que pueda ingresar al sistema");
                        popupMensaje.Show();
                        Reset();
                    } else if (response = 0) {
                        lblmensaje.SetText("Error de Conexión, asegurese de tener conexión a internet para ingresar el usuario al sistema");
                        popupMensaje.Show();
                    }
                } else {
                    lblmensaje.SetText("Error de Conexión, asegurese de tener conexión a internet para ingresar el usuario al sistema");
                    popupMensaje.Show();
                }
            }
        });
       
    }
}

function Reset() {
    txtIdentidad.SetText("");
    txtNombre1.SetText("");
    txtNombre2.SetText("");
    txtApellido1.SetText("");
    txtApellido2.SetText("");
    txtCorreo.SetText("");
    txtTelefono.SetText("");
    txtCorreoConf.SetText("");
    rbSi.SetChecked(false);
    rbNo.SetChecked(true);
   
}

function rbSiChange(s, e) {

    rbNo.SetChecked(false);
   
}

function rbNoChange(s, e) {

    rbSi.SetChecked(false);
  
}

function chkNroCriticoChange(s, e) {
    var itemcritico = frmLayout.GetItemByName("itemcritico");
    if (!chkNroCritico.GetChecked()) {
    //    itemNroCritico.SetVisible(true);
        itemcritico.SetVisible(true);
    } else { itemcritico.SetVisible(false);}
}

function GdvUsuariosEditClick(id) {
    cod_usr = id;
    $.ajax({
        url: '/Seguridad/Usuarios/obtener_info_usuario',
        type: 'POST',
        async: false,
        dataType: 'json',
        data: { cod_usuario: id },
        success: function (response) {
            if (response) {
                if (response.length > 0) {

                    MdlEditarUsuario.Show();

                    txtEditUsuario.SetText(response[0].nom_usuario);
                    txtEditPnombre.SetText(response[0].nom1_usr_persona);
                    txtEditSnombre.SetText(response[0].nom2_usr_persona);
                    txtEditPapellido.SetText(response[0].ape1_usr_persona);
                    txtEditSapellido.SetText(response[0].ape2_usr_persona);
                    txtEditIdentidad.SetText(response[0].ide_usr_persona);
                    txtEditEmail.SetText(response[0].email_usr_persona);
                    txtEditTelefono.SetText(response[0].num_tel_usr_persona);
                    CbProyecto.SetValue(response[0].id_unidad);

                    if (response[0].Estado == 0) {
                        if (cbEditHabilitado.GetChecked()) { cbEditHabilitado.SetChecked(false); }
                    }else if (response[0].Estado == 1) {
                        if (!cbEditHabilitado.GetChecked()) { cbEditHabilitado.SetChecked(true); }
                    }

                    if (response[0].usr_jefe == 0) {
                        if (cbJefe.GetChecked()) { cbJefe.SetChecked(false); }
                    } else if (response[0].usr_jefe == 1) {
                        if (!cbJefe.GetChecked()) { cbJefe.SetChecked(true); }
                    }
                    
                }
            }
        }
    });
}

function GdvRolesEditClick(id) {
    cod_usr = id;
    $.ajax({
        url: '/Seguridad/Usuarios/PVGPermisosEditarUsuario',
        type: 'POST',
        traditional: true,
        data: { cod_usuario: id },
        success: function (response) {
            if (response) {
                if (response.length > 0) {
                    MdlEditRoles.Show();
                    $('#divGdvPermisosEdit').html(response);

                }
            }
        }
    });
}

function btnAgregardeptoClick(s, e) {

   
    if (txtDepartamento.GetText() != "") {
        var desc_unidad = txtDepartamento.GetText();
        var estado;

        if (cbHabilitado.GetChecked()) { estado = 1 }
        else { estado = 0 }

        $.ajax({
            url: '/Seguridad/Usuarios/guardar_departamento',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: { desc_unidad: desc_unidad, estado: estado },
            success: function (response) {
                if (response) {
                    if (response.length == 0) {

                        alert("Cambios realizados satisfactoriamente");

                    }
                }
            }
        });
        Obtener_deptos();
        MdlNuevoDepartamento.Hide();
    }
    txtDepartamento.SetText("");
    
}

function GdvDepartamentoEditClick(id) {

   
    
    $.ajax({
        url: '/Seguridad/Usuarios/Obtener_depto',
        type: 'POST',
        async: false,
        dataType: 'json',
        data: { id_unidad: id },
        success: function (response) {
            if (response) {
                if (response.length > 0) {

                    txteditDepartamento.SetText(response[0].desc_unidad);

                    if (response[0].estado == 0) {
                        if (chkeditHabilitado.GetChecked()) { chkeditHabilitado.SetChecked(false); }
                    }
                    if (response[0].estado == 1) {
                        if (!chkeditHabilitado.GetChecked()) { chkeditHabilitado.SetChecked(true); }
                    }


                    MdlEditarDepartamento.Show();
                }
            }
        }
    });
}

function btnActualizardeptoClick(s, e) {

    if (txteditDepartamento.GetText() != "") {
        var id_unidad = GdvDepartamentos.GetRowKey(GdvDepartamentos.GetFocusedRowIndex());
        var desc_unidad = txteditDepartamento.GetText();
        var estado;
        if (chkeditHabilitado.GetChecked()) { estado = 1 }
        else { estado = 0 }

        $.ajax({
            url: '/Seguridad/Usuarios/actualizar_departamento',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: { id_unidad: id_unidad, desc_unidad: desc_unidad, estado: estado },
            success: function (response) {
                if (response) {
                    if (response == 0) {

                        alert("Cambios realizados satisfactoriamente");

                    }
                }
            }
        });
        MdlEditarDepartamento.Hide();
        txteditDepartamento.SetText("");
        Obtener_deptos();
    }
}


function Obtener_deptos() {
    $.ajax({
        url: '/Seguridad/Usuarios/PVGDepartamentos',
        type: 'POST',
        traditional: true,
        success: function (response) {
            if (response) {
                if (response.length > 0) {

                    $('#divGdvDepartamentos').html(response);

                }
            }
        }
    });
}

function btnSalirClick(s, e) {
    MdlEditarUsuario.Hide();
}

var rolesEditar;

function chkHabilitadoEditChange(id_rol, value) {

    if (value=="True") {
        
        $.ajax({
            url: '/Seguridad/Usuarios/EliminarRolporUsuario',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: { id_rol:id_rol, cod_usuario:cod_usr },
            success: function (response) {
                if (response) {
            
                }
            }
        });
    } else {

        $.ajax({
            url: '/Seguridad/Usuarios/AgregarRolporUsuario',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: { id_rol: id_rol, cod_usuario: cod_usr },
            success: function (response) {
                if (response) {
     
                }
            }
        });
    }
    GdvRolesEditClick(cod_usr);

}


function btnActualizarUsuarioClick(s, e) {
    var identidad = txtEditIdentidad.GetText();
    var nombre1 = txtEditPnombre.GetText();
    var nombre2 = txtEditSnombre.GetText();
    var apellido1 = txtEditPapellido.GetText();
    var apellido2 = txtEditSapellido.GetText();
    var correo = txtEditEmail.GetText();
    var telefono = txtEditTelefono.GetText();
    var id_unidad = CbProyecto.GetValue();
    var habilitado;
    var jefe;

    if (cbJefe.GetChecked()) { jefe = 1; }
    else { jefe = 0; }

    if (cbEditHabilitado.GetChecked()) { habilitado = 1; }
    else { habilitado = 0; }


    $.ajax({
        url: '/Seguridad/Usuarios/actualizar_usuario',
        type: 'POST',
        async: false,
        dataType: 'json',
        traditional: true,
        data: {
            identidad: identidad,
            estado: habilitado,
            nombre1: nombre1,
            nombre2: nombre2,
            apellido1: apellido1,
            apellido2: apellido2,
            telefono: telefono,
            correo: correo,
            codUsuario: cod_usr,
            jefe: jefe,
            id_unidad: id_unidad
        },
        success: function (response) {
            location.reload();               
        }
    });
    MdlEditarUsuario.Hide();
  
}

function btnRecordarClick(s, e) {
    $.ajax({
        url: '/Seguridad/Usuarios/RecordarContrasena',
        type: 'POST',
        async: false,
        dataType: 'json',
        data: { cod_usuario: GdvUsuarios.GetRowKey(GdvUsuarios.GetFocusedRowIndex()) },
        success: function (response) {
            if (response == 0) {
                alert("Se ha enviado un correo con la contraseña actual");
            }
        }
    });
}

function btnCambiarContrasenaClick(s, e) {

    $.ajax({
        url: '/Seguridad/Usuarios/CambiarContrasena',
        type: 'POST',
        async: false,
        dataType: 'json',
        data: { cod_usuario: GdvUsuarios.GetRowKey(GdvUsuarios.GetFocusedRowIndex()) },
        success: function (response) {
            if (response == 0) {
                alert("El cambio de contraseña para este usuario se realizará en su próximo inicio de sesión");
                MdlCambioContrasena.Hide();
            }
        }
    });
} 

function PopupCambioContrasenaClick(id) {
    MdlCambioContrasena.Show();
}

function btnLimpiarClick(s, e) {
    Reset();
}

function btnExportarClick(s, e) {
    $.ajax({
        url: '/Seguridad/Usuarios/Exportar_Grid_Usuarios',
        type: 'POST',
        traditional : true,
        success: function (response) {
            
        }
    });
}