function fnc_ingresar_regis_levanta_cita(intTipOper)
{
    
    var intCodLevanCita = AspxLabelCodLevanCita.GetText();
    var strNomLevanCita = AspxTextBoxNombreLevanCita.GetValue();
    var strFechIniLevanCita = AspxDateEditFechaInicio.GetText();
    var strFechFinLevanCita = AspxDateEditFechaFin.GetText();
    
    if (fnc_validar_levan_cita(strNomLevanCita, strFechIniLevanCita, strFechFinLevanCita)) //Valida que los registros esten completos.
    {
        if (intTipOper == 1) //Nuevo ingreso de un levantamiento de cita médica.
        {
            $.ajax({
                type: 'POST',
                url: '/Corresponsabilidad/Configuracion/fnc_proceso_levanta_cita',
                data:
                {
                    intCodLevanCita: 0, strNomLevanCita: strNomLevanCita, strFechIniLevanCita: strFechIniLevanCita, strFechFinLevanCita: strFechFinLevanCita, intTipoOper: intTipOper
                },
                success: function (response)
                {
                    AspxLabelErroLevanCita.SetText("");
                    AspxLabelErroLevanCita.SetVisible(false);
                    fnc_limpiar_contro_levan_cita();
                    AspxPopupControlEditLevantaCitaMedica.Hide();
                    AspxGridViewLevantamientoCitas.Refresh();
                }
            });
        } else //Actualización de registro.
        {
            $.ajax({
                type: 'POST',
                url: '/Corresponsabilidad/Configuracion/fnc_proceso_levanta_cita',
                data: {
                    intCodLevanCita: intCodLevanCita, strNomLevanCita: strNomLevanCita, strFechIniLevanCita: strFechIniLevanCita, strFechFinLevanCita: strFechFinLevanCita, intTipoOper: intTipOper
                },
                success: function (response)
                {
                    AspxLabelErroLevanCita.SetText("");
                    AspxLabelErroLevanCita.SetVisible(false);
                    fnc_limpiar_contro_levan_cita();
                    AspxPopupControlEditLevantaCitaMedica.Hide();
                    AspxGridViewLevantamientoCitas.Refresh();
                }
            });
        }
    }
}

function fnc_editar_levan_cita(intCodLevanCita, strNomLevanCita, strFechIniLevanCita, strFechFinLevanCita)
{
    fnc_limpiar_contro_levan_cita();

    AspxLabelCodLevanCita.SetText(intCodLevanCita);
    AspxTextBoxNombreLevanCita.SetText(strNomLevanCita);
    AspxDateEditFechaInicio.SetDate(new Date(strFechIniLevanCita));
    AspxDateEditFechaFin.SetDate(new Date(strFechFinLevanCita));

    AspxButtonActaulizarLevanCita.SetEnabled(true);
    AspxButtonActaulizarLevanCita.SetVisible(true);

    AspxButtonLimpiarLevanCita.SetEnabled(false);
    AspxButtonLimpiarLevanCita.SetVisible(false);

    AspxButtonCrearLevanCita.SetEnabled(false);
    AspxButtonCrearLevanCita.SetVisible(false);

    AspxPopupControlEditLevantaCitaMedica.Show();
}

function fnc_validar_levan_cita(strNomLevanCita, strFechIniLevanCita, strFechFinLevanCita)
{
    if (strNomLevanCita == null ) //Validar campos principales que se encuentren ingresados.
    {
        AspxLabelErroLevanCita.SetText("*Los campos minimos requeridos no fuerón ingresados, nombre del levanmineto, fecha inicio o fecha fin.");
        AspxLabelErroLevanCita.SetVisible(true);
        return 0
    } else
    {
        return fnc_val_fech_ingre(strFechIniLevanCita, strFechFinLevanCita);
    }
}

function fnc_val_fech_ingre(strFechIni, strFechFin)
{
    if (strFechIni == "" || strFechFin == "")
    {
        AspxLabelErroLevanCita.SetText("*Error no se ingreso alguna de las fechas.");
        AspxLabelErroLevanCita.SetVisible(true);
        return 0;
    } else
    {
        var objFechaIni = new Date(strFechIni);
        var objFechaFin = new Date(strFechFin);

        if (objFechaIni.getFullYear() !== objFechaFin.getFullYear() || objFechaIni > objFechaFin)
        {
            AspxLabelErroLevanCita.SetText("*El año de las fechas no coincide o la fecha inicio es mayor que la fecha fin.");
            AspxLabelErroLevanCita.SetVisible(true);
            return 0;
        } else
        {
            return 1;
        }
    }
}

function fnc_limpiar_contro_levan_cita()
{
    //Limpiar controles
    AspxLabelCodLevanCita.SetValue(null);
    AspxTextBoxNombreLevanCita.SetValue(null);
    AspxDateEditFechaInicio.SetValue(null);
    AspxDateEditFechaFin.SetValue(null);

    AspxButtonActaulizarLevanCita.SetEnabled(false);
    AspxButtonActaulizarLevanCita.SetVisible(false);

    AspxButtonLimpiarLevanCita.SetEnabled(true);
    AspxButtonLimpiarLevanCita.SetVisible(true);

    AspxButtonCrearLevanCita.SetEnabled(true);
    AspxButtonCrearLevanCita.SetVisible(true);

    AspxLabelErroLevanCita.SetText("");
    AspxLabelErroLevanCita.SetVisible(false);
}




