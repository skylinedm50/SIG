function btnNuevoModuloClick(s, e) {
    ResetTextBox();
    MdlNuevoModulo.Show();
}

function btnSalirEditarOpcionClick(s, e) {
    MdlEditarOpcion.Hide();
}

function btnNuevaOpcionClick(s, e) {
    ResetTextBox();
    MdlNuevaOpcion.Show();
}

function btnNuevaPantallaClick(s, e) {
    ResetTextBox();
    MdlNuevaPantalla.Show();
}

function btnSalirModuloClick(s, e) {
    MdlNuevoModulo.Hide();
}


function btnSalirOpcionClick(s, e) {
    MdlNuevaOpcion.Hide();
}

function btnSalirPantallaClick(s, e) {
    MdlNuevaPantalla.Hide();
}

function btnSalirEditarModuloClick(s, e) {
    MdlEditarModulo.Hide();
}

function btnSalirEditarPantallaClick(s, e) {
    MdlEditarPantalla.Hide();
}

function GdvModulosChange(s, e) {

    Cargar_lista_opciones();
}

function GdvOpcionesChange(s, e) {

    Cargar_lista_pantallas();    
}

function ResetTextBox() {
    txtEditModulo.SetText("");
    txtEditModuloURL.SetText("");
    txtEditIcono.SetText("");
    txtEditOpcion.SetText("");
    txtEditPantalla.SetText("");
    txtEditPantallaURL.SetText("");
    txtModulo.SetText("");
    txtIcono.SetText("");
    txtOpcion.SetText("");
    txtPantalla.SetText("");
    txtUrlPantalla.SetText("");
}

function GdvModuloEditClick(id) {
    
    ResetTextBox();
        $.ajax({
            url: '/Seguridad/Modulos/Obtener_info_modulo',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: { id_modulo: id },
            success: function (response) {
                if (response) {
                    if (response.length > 0) {
                                               
                        MdlEditarModulo.Show();
                        txtEditModulo.SetText(response[0].nom_modulo);
                        txtEditModuloURL.SetText(response[0].url);
                        txtEditIcono.SetText(response[0].icono);

                        if (response[0].estado_modulo == 0) {
                            if (chkEdithabilitarModulo.GetChecked()) { chkEdithabilitarModulo.SetChecked(false); }
                        }
                        if (response[0].estado_modulo == 1) {
                            if (!chkEdithabilitarModulo.GetChecked()) { chkEdithabilitarModulo.SetChecked(true); }
                        }
         

                       

                    }
                }
            }
        });
        
    
}

function GdvOpcionEditClick(id) {
    ResetTextBox();
        $.ajax({
            url: '/Seguridad/Modulos/Obtener_info_opcion',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: { id_opcion: id},
            success: function (response) {
                if (response) {
                    if (response.length > 0) {

                        MdlEditarOpcion.Show();
                        txtEditOpcion.SetText(response[0].desc_opcion);

                        if (response[0].estado_opcion == 0) {
                            if (chkEdithabilitarOpcion.GetChecked()) { chkEdithabilitarOpcion.SetChecked(false); }
                        }
                        if (response[0].estado_opcion == 1) {
                            if (!chkEdithabilitarOpcion.GetChecked()) { chkEdithabilitarOpcion.SetChecked(true); }
                        }
                        
                       
                    }
                }
            }
        });
}

function GdvPantallaEditClick(id) {
    ResetTextBox();
    $.ajax({
        url: '/Seguridad/Modulos/Obtener_info_pantalla',
        type: 'POST',
        async: false,
        dataType: 'json',
        data: { id_pantalla: id },
        success: function (response) {
            if (response) {
                if (response.length > 0) {

                    MdlEditarPantalla.Show();
                    txtEditPantalla.SetText(response[0].desc_actividad);
                    txtEditPantallaURL.SetText(response[0].url);

                    if (response[0].estado_actividad == 0) {
                        if (chkEdithabilitarPantalla.GetChecked()) { chkEdithabilitarPantalla.SetChecked(false); }
                    }
                    if (response[0].estado_actividad == 1) {
                        if (!chkEdithabilitarPantalla.GetChecked()) { chkEdithabilitarPantalla.SetChecked(true); }
                    }
                    

                }
            }
        }
    });
}

function btnAgregarModuloClick(s, e) {
    if (txtModulo.GetText() != "") {
        var nom_modulo = txtModulo.GetText();
        var icono = txtIcono.GetText();
        var estado_modulo;

        if (chkhabilitarModulo.GetChecked()) { estado_modulo = 1 }
        else { estado_modulo = 0 }

        $.ajax({
            url: '/Seguridad/Modulos/Agregar_modulo',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: { nom_modulo: nom_modulo, estado_modulo: estado_modulo, icono: icono },
            success: function (response) {
                if (response) {


                }
            }
        });
        Cargar_lista_modulos();
        MdlNuevoModulo.Hide();
 
    }
}

function btnAgregarOpcionClick(s, e) {

    if (txtOpcion.GetText() != "") {
        var desc_opcion = txtOpcion.GetText();
        var estado_opcion;
        var id_modulo = GdvModulos.GetRowKey(GdvModulos.GetFocusedRowIndex());

        if (chkhabilitarOpcion.GetChecked()) { estado_opcion = 1 }
        else { estado_opcion = 0 }

        $.ajax({
            url: '/Seguridad/Modulos/Agregar_opcion',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: { desc_opcion: desc_opcion, estado_opcion: estado_opcion, id_modulo: id_modulo },
            success: function (response) {
                if (response) {


                }
            }
        });
        Cargar_lista_opciones();
        MdlNuevaOpcion.Hide();

    }
}

function btnAgregarPantallaClick(s, e) {

    if (txtPantalla.GetText() != "" && txtUrlPantalla.GetText() != "") {
        var desc_actividad = txtPantalla.GetText();
        var id_opcion = GdvOpciones.GetRowKey(GdvOpciones.GetFocusedRowIndex());
        var url = txtUrlPantalla.GetText();
        var estado_actividad;

        if (chkhabilitarPantalla.GetChecked()) { estado_actividad = 1 }
        else { estado_actividad = 0 }

        $.ajax({
            url: '/Seguridad/Modulos/Agregar_pantalla',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: { desc_actividad: desc_actividad, id_opcion: id_opcion, estado_actividad: estado_actividad, url: url },
            success: function (response) {
                if (response) {
                }
            }
        });
        Cargar_lista_pantallas();
        MdlNuevaPantalla.Hide();
    }
}

function btnActualizarModuloClick(s, e) {
    if (txtEditModulo.GetText() != "" && txtEditIcono.GetText() != "") {
        var nom_modulo = txtEditModulo.GetText();
        var icono = txtEditIcono.GetText();
        var estado_modulo;
        var id_modulo = GdvModulos.GetRowKey(GdvModulos.GetFocusedRowIndex());
        if (chkEdithabilitarModulo.GetChecked()) { estado_modulo = 1 }
        else { estado_modulo = 0 }

        $.ajax({
            url: '/Seguridad/Modulos/Actualizar_modulo',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: { nom_modulo: nom_modulo, icono: icono, estado_modulo: estado_modulo, id_modulo: id_modulo },
            success: function (response) {
                if (response) {
                    
                }
            }
        });
        Cargar_lista_modulos();
        MdlEditarModulo.Hide();
    }
}

function btnActualizarOpcionClick(s, e) {
    if (txtEditOpcion.GetText() != "") {
        var desc_opcion = txtEditOpcion.GetText();
        var id_modulo = GdvModulos.GetRowKey(GdvModulos.GetFocusedRowIndex());
        var id_opcion = GdvOpciones.GetRowKey(GdvOpciones.GetFocusedRowIndex());
        var estado_opcion;

        if (chkEdithabilitarOpcion.GetChecked()) { estado_opcion = 1 }
        else { estado_opcion = 0 }

        $.ajax({
            url: '/Seguridad/Modulos/Actualizar_opcion',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: { desc_opcion: desc_opcion, estado_opcion: estado_opcion, id_modulo: id_modulo, id_opcion: id_opcion },
            success: function (response) {
                if (response) {
                }
            }
        });
        Cargar_lista_opciones();
        MdlEditarOpcion.Hide();
    }
}

function btnActualizarPantallaClick(s, e) {
    if (txtEditPantallaURL.GetText() != "" && txtEditPantalla.GetText() != "") {
        var desc_actividad = txtEditPantalla.GetText();
        var url = txtEditPantallaURL.GetText();
        var id_opcion = GdvOpciones.GetRowKey(GdvOpciones.GetFocusedRowIndex());
        var id_actividad = GdvPantalla.GetRowKey(GdvPantalla.GetFocusedRowIndex());
        var estado_actividad;

        if (chkEdithabilitarPantalla.GetChecked()) { estado_actividad = 1 }
        else { estado_actividad = 0 }

        $.ajax({
            url: '/Seguridad/Modulos/Actualizar_pantalla',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: { desc_actividad: desc_actividad, id_opcion: id_opcion, estado_actividad: estado_actividad, url: url, id_actividad: id_actividad },
            success: function (response) {
                if (response) {
                }
            }
        });
        Cargar_lista_pantallas();
        MdlEditarPantalla.Hide();
    }
}

function Cargar_lista_modulos() {
    $.ajax({
        url: '/Seguridad/Modulos/PartialViewGridModulos',
        type: 'POST',
        traditional: true,
        success: function (response) {
            if (response) {
                if (response.length > 0) {

                    $('#divGdvModulos').html(response);
                }
            }
        }
    });
}

function Cargar_lista_opciones() {
 

    $.ajax({
        url: '/Seguridad/Modulos/PartialViewGridOpciones',
        type: 'POST',
        traditional: true,
        data: { id_modulos: GdvModulos.GetRowKey(GdvModulos.GetFocusedRowIndex()) },
        success: function (response) {
            if (response) {
                if (response.length > 0) {

                    $('#divGdvOpciones').html(response);
                    if (GdvModulos.pageRowCount > 0) {
                        btnNuevaOpcion.SetEnabled(true);
                    } else { btnNuevaOpcion.SetEnabled(false); }

                    
                }
            }
        }
    });
    


}

function Cargar_lista_pantallas() {
    $.ajax({
        url: '/Seguridad/Modulos/PartialViewGridPantallas',
        type: 'POST',
        traditional: true,
        data: { id_opcion: GdvOpciones.GetRowKey(GdvOpciones.GetFocusedRowIndex()) },
        success: function (response) {
            if (response) {
                if (response.length >= 0) {


                    $('#divGdvPantallas').html(response);
                    if (GdvOpciones.pageRowCount > 0) {
                        btnNuevaPantalla.SetEnabled(true);
                    } else { btnNuevaPantalla.SetEnabled(false); }
                }
            }
        }
    });
}