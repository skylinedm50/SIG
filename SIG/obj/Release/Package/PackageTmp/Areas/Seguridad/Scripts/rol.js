function btnNuevoRolClick(s, e) {
    Reset_TextBox();
    MdlNuevoRol.Show();
}

function btnSalirClick(s, e) {
    MdlNuevoRol.Hide();
}

function btnSalirEditarClick(s, e) {
    MdlEditarRol.Hide();
}

function Reset_TextBox() {
    txtRol.SetText("");
    txtEditarRol.SetText("");
}

function GdvRolEditClick(id) {
    Reset_TextBox();
    $.ajax({
        url: '/Seguridad/Roles/Obtener_info_rol',
        type: 'POST',
        async: false,
        dataType: 'json',
        data: { id_rol: id },
        success: function (response) {
            if (response) {
                if (response.length > 0) {
                    txtEditarRol.SetText(response[0].desc_rol);
                    cbEditarModulo.SetValue(response[0].id_modulo);
                    if (response[0].estado_rol == 0) {
                        if (chkEditarhabilitar.GetChecked()) { chkEditarhabilitar.SetChecked(false); }
                    }
                    if (response[0].estado_rol == 1) {
                        if (!chkEditarhabilitar.GetChecked()) { chkEditarhabilitar.SetChecked(true); }
                    }
                }
            }
        }
    });
    MdlEditarRol.Show();
}

function Cargar_roles() {
    $.ajax({
        url: '/Seguridad/Roles/PartialViewGridRoles',
        type: 'POST',
        traditional: true,
        success: function (response) {
            if (response) {
                if (response.length > 0) {
                    $('#divGdvRoles').html(response);
                }
            }
        }
    });
}

function btnAgregarRolClick(s, e) {

    if (txtRol.GetText() != "") {
        var rol = txtRol.GetText();
        var id_modulo = cbNuevoModulo.GetValue();
        var estado_rol;

        if (chkhabilitar.GetChecked()) { estado_rol = 1 }
        else { estado_rol = 0 }

            $.ajax({
                url: '/Seguridad/Roles/Agregar_rol',
                type: 'POST',
                async: false,
                dataType: 'json',
                data: { desc_rol: rol, id_modulo: id_modulo, estado_rol: estado_rol },
                success: function (response) {
                    if (response) {
                        if (response.length > 0) {
                        
                        }
                    }
                }
            });
            MdlNuevoRol.Hide();
            Cargar_roles();
    }
}

function btnEditarRolClick(s, e) {

    if (txtEditarRol.GetText() != "") {
        var rol = txtEditarRol.GetText();
        var id_modulo = cbEditarModulo.GetValue();
        var estado_rol;
        var id_rol = GdvRoles.GetRowKey(GdvRoles.GetFocusedRowIndex());

        if (chkEditarhabilitar.GetChecked()) { estado_rol = 1 }
        else { estado_rol = 0 }

        $.ajax({
            url: '/Seguridad/Roles/Actualizar_rol',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: { desc_rol: rol, id_modulo: id_modulo, estado_rol: estado_rol, id_rol: id_rol },
            success: function (response) {
                if (response) {
                    if (response.length > 0) {

                    }
                }
            }
        });
        MdlEditarRol.Hide();
        Cargar_roles();
    }
}