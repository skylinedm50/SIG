
function fnc_cargar_municipios(strCodDepartamento)
{
    AspxComboBoxMunicipioSIG.ClearItems(); //Limpiamos de registros el combobox de los municipios.
    AspxComboBoxAldeaSIG.ClearItems();
    AspxComboBoxCaserioSIG.ClearItems();

    if (typeof AspxComboBoxCentroSalud !== 'undefined')
    {
        AspxComboBoxCentroSalud.ClearItems();
    }

    if (strCodDepartamento !== null ) {
        $.ajax({
            type: 'POST',
            url: '/Corresponsabilidad/Reportes/fnc_car_combo_muni',
            data: { strCodDepartamento: strCodDepartamento },
            success: function (response) {
                for (var intI = 0; intI < response.length; intI++) {
                    var arrJson = response[intI];
                    var strValueFiled = '';
                    var strTextField = '';

                    for (strIndice in arrJson) {
                        if (strIndice == 'cod_municipio') {
                            strValueFiled = arrJson[strIndice];
                        } else {
                            strTextField = arrJson[strIndice];
                        }
                    }
                    AspxComboBoxMunicipioSIG.AddItem(strTextField, strValueFiled);          
                }
                fnc_preSelect_all(2);
            }
        });
    }
}

function fnc_cargar_aldea(strCodMunicipio)
{
    AspxComboBoxAldeaSIG.ClearItems();
    AspxComboBoxCaserioSIG.ClearItems();

    if (typeof AspxComboBoxCentroSalud !== 'undefined')
    {
        AspxComboBoxCentroSalud.ClearItems();
    }

    if (strCodMunicipio !== null)
    {
        $.ajax({
            type: 'POST',
            url: '/Corresponsabilidad/Reportes/fnc_car_combo_alde',
            data: { strCodMunicipio: strCodMunicipio },
            success: function (response)
            {
                for (var intI = 0; intI < response.length; intI++)
                {
                    var arrJson = response[intI];
                    var strValueFiled = '';
                    var strTextField = '';

                    for (strIndice in arrJson)
                    {
                        if (strIndice == 'cod_aldea')
                        {
                            strValueFiled = arrJson[strIndice];
                        } else
                        {
                            strTextField = arrJson[strIndice];
                        }
                    }
                    AspxComboBoxAldeaSIG.AddItem(strTextField, strValueFiled);                   
                }
                fnc_preSelect_all(3);
            }
        });

        if (typeof AspxComboBoxCentroSalud !== 'undefined')
        {
            fnc_cargar_combox_centro_salud();
        }

    }
}

function fnc_cargar_caserio(strCodAldea)
{
    AspxComboBoxCaserioSIG.ClearItems();

    if (typeof AspxComboBoxCentroSalud !== 'undefined')
    {
        AspxComboBoxCentroSalud.ClearItems();
    }

    if (strCodAldea !== '00')
    {
        $.ajax({
            type: 'POST',
            url: '/Corresponsabilidad/Reportes/fnc_car_combo_case',
            data: { strCodAldea: strCodAldea },
            success: function (response) {
                for (var intI = 0; intI < response.length; intI++)
                {
                    var arrJson = response[intI];
                    var strValueFiled = '';
                    var strTextField = '';

                    for (strIndice in arrJson) {

                        if (strIndice == 'cod_caserio') {
                            strValueFiled = arrJson[strIndice];
                        } else {
                            strTextField = arrJson[strIndice];
                        }
                    }
                    AspxComboBoxCaserioSIG.AddItem(strTextField, strValueFiled);
                }
                fnc_preSelect_all(4);
            }
        });

        if (typeof AspxComboBoxCentroSalud !== 'undefined')
        {
            fnc_cargar_combox_centro_salud();
        }
    }
}

function fnc_cargar_centro_salud_por_caserio(strCodCaserio)
{
    if (typeof AspxComboBoxCentroSalud !== 'undefined')
    {
        AspxComboBoxCentroSalud.ClearItems();

        if (strCodCaserio !== '00') {
            fnc_cargar_combox_centro_salud();
        } else {
            AspxComboBoxCentroSalud.AddItem('(Todos)', '00');
            AspxComboBoxCentroSalud.SetValue(0);
        }
    }
}

function fnc_cargar_combox_centro_salud()
{
    var strCodMunicipio = AspxComboBoxMunicipioSIG.GetValue();
    var strCodAldea = AspxComboBoxAldeaSIG.GetValue();
    var strCodCaserio = AspxComboBoxCaserioSIG.GetValue();

    strCodMunicipio = (strCodMunicipio == null ? '-1' : strCodMunicipio);
    strCodAldea = (strCodAldea == null ? '-1' : strCodAldea);
    strCodCaserio = (strCodCaserio == null ? '-1' : strCodCaserio);

        $.ajax({
            type: 'POST',
            url: '/Corresponsabilidad/Reportes/fnc_car_combo_centro_salud',
            data: { strCodMunicipio: strCodMunicipio, strCodAldea: strCodAldea, strCodCaserio: strCodCaserio },
            success: function (response) {
                for (var intI = 0; intI < response.length; intI++) {
                    var arrJson = response[intI];
                    var strValueFiled = '';
                    var strTextField = '';

                    for (strIndice in arrJson)
                    {
                        if (strIndice == 'cod_cen_sal')
                        {
                            strValueFiled = arrJson[strIndice];
                        } else
                        {
                            strTextField = arrJson[strIndice];
                        }
                    }
                    AspxComboBoxCentroSalud.AddItem(strTextField, strValueFiled);

                }
            }
        });
}

function fnc_preSelect_all(intCodTipUbi) {
    switch (intCodTipUbi) {
        case 2:
            AspxComboBoxMunicipioSIG.AddItem('(Todos)', '00');
            AspxComboBoxMunicipioSIG.SetValue(0);

            AspxComboBoxAldeaSIG.AddItem('(Todos)', '00');
            AspxComboBoxAldeaSIG.SetValue(0);

            AspxComboBoxCaserioSIG.AddItem('(Todos)', '00');
            AspxComboBoxCaserioSIG.SetValue(0);

            if (typeof AspxComboBoxCentroSalud !== 'undefined') {
                AspxComboBoxCentroSalud.AddItem('(Todos)', '00');
                AspxComboBoxCentroSalud.SetValue(0);
            }
            break;
        case 3:
            AspxComboBoxAldeaSIG.AddItem('(Todos)', '00');
            AspxComboBoxAldeaSIG.SetValue(0);

            AspxComboBoxCaserioSIG.AddItem('(Todos)', '00');
            AspxComboBoxCaserioSIG.SetValue(0);

            if (typeof AspxComboBoxCentroSalud !== 'undefined') {
                AspxComboBoxCentroSalud.AddItem('(Todos)', '00');
                AspxComboBoxCentroSalud.SetValue(0);
            }
            break;
        case 4:
            AspxComboBoxCaserioSIG.AddItem('(Todos)', '00');
            AspxComboBoxCaserioSIG.SetValue(0);

            if (typeof AspxComboBoxCentroSalud !== 'undefined') {
                AspxComboBoxCentroSalud.AddItem('(Todos)', '00');
                AspxComboBoxCentroSalud.SetValue(0);
            }
            break;
    }
}