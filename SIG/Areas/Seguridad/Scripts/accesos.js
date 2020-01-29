var id_actividad_clicked;
function btnNuevoAccesoClick(s, e) {
    ObtenerDatos();
    popupNuevoAcceso.Show();
}

function btnSalirClick(s, e) {
    popupNuevoAcceso.Hide();
}

function GdvRolesChange(s, e) {

    Obtener_Accesos();

}

function Obtener_Accesos() {
    $.ajax({
        url: '/Seguridad/Accesos/PartialViewGridPantallasAccesos',
        type: 'POST',
        traditional: true,
        data: { id_rol: GdvRoles.GetRowKey(GdvRoles.GetFocusedRowIndex()) },
        success: function (response) {
            if (response) {
                if (response.length > 0) {

                    $('#divGdvPantallasAccesos').html(response);

                    if (GdvRoles.GetRowKey(GdvRoles.GetFocusedRowIndex()) != null) {
                        btnNuevoAcceso.SetEnabled(true);
                    } else { btnNuevoAcceso.SetEnabled(false); }
                }
            }
        }
    });
}

function btnSalirEliminarAccesoClick(s, e) {
    popupEliminarAcceso.Hide();
}

function GdvPantallaEliminarClick(id) {
    id_rol_actividad_clicked = id;
    $.ajax({
        url: '/Seguridad/Accesos/Obtener_info_rol_pantalla',
        type: 'POST',
        async: false,
        dataType: 'json',
        data: { id_rol_actividad: id },
        success: function (response) {
            if (response) {
                if (response.length > 0) {
              
                    lblmensajeEliminar.SetText("El rol <b>" + response[0].desc_rol + "</b> ya no tendrá acceso a la pantalla <b>" + response[0].desc_actividad + "</b> ¿Está seguro que desea realizar esta acción? ");
    
                }
            }
        }
    });
    popupEliminarAcceso.Show();

}

function cbModuloChange(s, e) {
    ObtenerDatos();
}

function cbOpcionChange(s, e) {
    ObtenerPantallas();
}

function ObtenerDatos() {
    $.ajax({
        url: '/Seguridad/Accesos/PartialViewCbOpciones',
        type: 'POST',
        traditional: true,
        data: { id_modulos: cbModulo.GetValue() },
        success: function (response) {
            if (response) {
                if (response.length > 0) {

                    $('#divCbOpciones').html(response);
                    ObtenerPantallas();                    
                }
            }
        }
    });
}

function ObtenerPantallas() {
    $.ajax({
        url: '/Seguridad/Accesos/PartialViewCbPantallas',
        type: 'POST',
        traditional: true,
        data: { id_opcion: cbOpcion.GetValue() },
        success: function (response) {
            if (response) {
                if (response.length > 0) {

                    $('#divCbPantallas').html(response);
                    
                }
            }
        }
    });
}

function btnAgregarAccesoClick(s, e) {
    var id_actividad = cbPantalla.GetValue();
    var id_rol = GdvRoles.GetRowKey(GdvRoles.GetFocusedRowIndex());
    if (id_actividad != null) {
        $.ajax({
            url: '/Seguridad/Accesos/Agregar_acceso',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: { id_actividad: id_actividad, id_rol: id_rol },
            success: function (response) {
                if (response) {
                    if (response.length > 0) {

                    }
                }
            }
        });
        popupNuevoAcceso.Hide();
        Obtener_Accesos();
    }
}

function btnEliminarAccesoClick(s, e) {

    $.ajax({
        url: '/Seguridad/Accesos/Eliminar_acceso',
        type: 'POST',
        async: false,
        dataType: 'json',
        data: { id_rol_actividad: id_rol_actividad_clicked },
        success: function (response) {
            if (response) {
                if (response.length > 0) {
                    
                }
            }
        }
    });
    popupEliminarAcceso.Hide();
    Obtener_Accesos();
}