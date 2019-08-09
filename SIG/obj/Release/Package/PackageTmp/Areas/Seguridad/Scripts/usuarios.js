

var identidad, estado, nombre1, nombre2, apellido1, apellido2, telefono, correo, usuario,
    clave, nroCritico, claveDef, codPersona, roles = new Array, flag = false;

function txtIdentidadChange(s, e) {

    var array = new Array();
    //gdvPermisos.UnselectRows();
    //gdvPermisos.ExpandAll();
    
    $.ajax({
        url: '/Seguridad/Usuarios/obtenerInfoPersona',
        type: 'POST',
        async: false,
        dataType: 'json',
        data: { identidad: txtIdentidad.GetText() },
        success: function (response) {
            if (response) {

                if (response.length == 1) {
                    txtIdentidad.SetText(response[0].ide_usr_persona);
                    chkEstado.SetChecked(response[0].Estado);
                    txtNombre1.SetText(response[0].nom1_usr_persona);
                    txtNombre2.SetText(response[0].nom2_usr_persona);
                    txtApellido1.SetText(response[0].ape1_usr_persona);
                    txtApellido2.SetText(response[0].ape2_usr_persona);
                    txtTelefono.SetText(response[0].num_tel_usr_persona);
                    txtCorreo.SetText(response[0].email_usr_persona);
                    txtUsuario.SetText(response[0].nom_usuario);
                    txtClave.SetText(response[0].clv_usuario);
                    txtNroCritico.SetNumber(response[0].cod_critico_usuario);
                    chkClave.SetChecked(response[0].clave_def_usuario);
                    txtCodPersona.SetText(response[0].cod_usr_persona);

                    $.ajax({
                        url: '/Seguridad/Usuarios/obtenerRolesPersona',
                        type: 'POST',
                        async: false,
                        dataType: 'json',
                        data: { identidad: txtIdentidad.GetText() },
                        success: function (response) {
                            if (response) {
                                debugger;

                                $.each(response, function (idx, obj) {
                                    array.push(obj.id_rol);
                                });
                            }
                        }
                    });

                    gdvPermisos.UnselectAllRowsOnPage();
                    gdvPermisos.SelectRowsByKey(array);

                    btnActualizar.SetEnabled(true);
                    btnGuardar.SetEnabled(false);

                } else if (response.length > 1) {

                    alert("Existe más de una persona con ese número de identidad.");
                    gdvPermisos.UnselectAllRowsOnPage();
                    btnActualizar.SetEnabled(false);
                    btnGuardar.SetEnabled(false);

                } else if (response.length == 0) {
                    //txtIdentidad.SetText();
                    chkEstado.SetChecked(false);
                    txtNombre1.SetText();
                    txtNombre2.SetText();
                    txtApellido1.SetText();
                    txtApellido2.SetText();
                    txtTelefono.SetText();
                    txtCorreo.SetText();
                    txtUsuario.SetText();
                    txtClave.SetText();
                    txtNroCritico.SetNumber(0);
                    chkClave.SetChecked(false);
                    txtCodPersona.SetText();

                    gdvPermisos.UnselectAllRowsOnPage();

                    btnActualizar.SetEnabled(false);
                    btnGuardar.SetEnabled(true);
                }
            } 
        }
    });
}

function btnActualizarClick(s, e) {

    identidad = txtIdentidad.GetText(),
    //estado = chkEstado.GetValue();
    estado = chkEstado.GetValue() ? 1 : 0;
    nombre1 = txtNombre1.GetText(),
    nombre2 = txtNombre2.GetText(),
    apellido1 = txtApellido1.GetText(),
    apellido2 = txtApellido2.GetText(),
    telefono = txtTelefono.GetText(),
    correo = txtCorreo.GetText(),
    usuario = txtUsuario.GetText(),
    clave = txtClave.GetText(),
    nroCritico = txtNroCritico.GetText(),
    //claveDef = chkClave.GetValue(),
    claveDef = chkClave.GetValue() ? 1 : 0;
    codPersona = txtCodPersona.GetText(),
    roles = gdvPermisos.GetSelectedKeysOnPage(),
    flag = false;

    

    if (validarCampos()) {

        $.ajax({
            url: '/Seguridad/Usuarios/actualizarUsuario',
            type: 'POST',
            async: false,
            dataType: 'json',
            traditional: true,
            data: {
                identidad: identidad,
                estado: estado,
                nombre1: nombre1,
                nombre2: nombre2,
                apellido1: apellido1,
                apellido2: apellido2,
                telefono: telefono,
                correo: correo,
                usuario: usuario,
                clave: clave,
                nroCritico: nroCritico,
                claveDef: claveDef,
                codPersona: codPersona,
                roles: roles
            },
            success: function (response) {
                if (response) {
                    //alert(response);
                    alert("Usuario Guardado Correctamente");
                    window.location = "/Seguridad/Usuarios/ViewAdministrarUsuarios";
                } else {
                    alert("error al guardar")
                }
            }
        });

    }

}

function btnGuardarClick(s, e) {

    identidad = txtIdentidad.GetText(),
    nombre1 = txtNombre1.GetText(),
    nombre2 = txtNombre2.GetText(),
    apellido1 = txtApellido1.GetText(),
    apellido2 = txtApellido2.GetText(),
    telefono = txtTelefono.GetText(),
    correo = txtCorreo.GetText(),
    usuario = txtUsuario.GetText(),
    clave = txtClave.GetText(),
    nroCritico = txtNroCritico.GetText(),
    roles = gdvPermisos.GetSelectedKeysOnPage(),
    flag = false;

    if (validarCampos()) {
        //alert("guardar");
        
        $.ajax({
            url: '/Seguridad/Usuarios/guardarUsuario',
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
                usuario: usuario,
                clave: clave,
                nroCritico: nroCritico,
                roles: roles
            },
            success: function (response) {
                if (response) {
                    //alert(response);
                    if (response = 1) {
                        alert("Usuario Guardado Correctamente");
                        window.location = "/Seguridad/Usuarios/ViewAdministrarUsuarios";
                    }
                } else {
                    alert("error al guardar")
                }
            }
        });
        
    } else {
        alert("no guardar");
    }

}

function validarCampos() {
    if (identidad.length < 13) {
        alert("El número de identidad debe tener 13 dígitos.");
    } else if (nombre1.length == 0) {
        alert("El primer nombre no puede ir vacio.");
    } else if (apellido1.length == 0) {
        alert("El primer apellido no puede if vacio.");
    } else if (apellido2.length == 0) {
        alert("El segundo apellido no puede ir vacio.");
    } else if (telefono.length != 8) {
        alert("El número de telefono debe poseer 8 dígitos.");
    } else if (correo.length == 0 || correo.search('@') == -1 || correo.search('.') == -1) {
        alert("El correo debe de ser con formato ejemplo@dominio.com");
    } else if (usuario.length == 0) {
        alert("El usuario no debe de ir vacio.");
    } else if (clave.length == 0) {
        alert("La clave no debe de ir vacia.");
    } else if (roles.length == 0) {
        alert("Debe seleccionar al menos un rol");
    } else {
        return true;
    }
    return false;
}


