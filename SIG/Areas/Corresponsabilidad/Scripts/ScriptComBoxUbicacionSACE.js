function fnc_cargar_municipio_sace(intCodDepartamento)//Función para cargar datos al combobox de los municipios de SACE.
{
    
    AspxComboBoxMunicipioSACE.ClearItems(); //Limpiamos de registros el combobox de los municipios.
    AspxComboBoxAldeaSACE.ClearItems();
    AspxComboBoxCaserioSACE.ClearItems();
    AspxComboBoxCentroEducativoSACE.ClearItems();

    if (intCodDepartamento > 0) {
        $.ajax({
            type: 'POST',
            url: '/Corresponsabilidad/Reportes/fnc_car_combo_muni_sace',
            data: { intCodDepar: intCodDepartamento },
            success: function (response) {
                
                for (var intI = 0; intI < response.length; intI++) {
                    var arrJson = response[intI];
                    var strValueFiled = '';
                    var strTextField = '';

                    for (strIndice in arrJson) {
                        if (strIndice == 'cod_sac_mun_sac') {
                            strValueFiled = arrJson[strIndice];
                        } else {
                            strTextField = arrJson[strIndice];
                        }
                    }
                    AspxComboBoxMunicipioSACE.AddItem(strTextField, strValueFiled);
                    
                }
            }
        });
    } else
    {
        AspxComboBoxMunicipioSACE.AddItem('(Todos)', 0);
        AspxComboBoxMunicipioSACE.SetValue(0);

        AspxComboBoxAldeaSACE.AddItem('(Todos)', 0);
        AspxComboBoxAldeaSACE.SetValue(0);

        AspxComboBoxCaserioSACE.AddItem('(Todos)', 0);
        AspxComboBoxCaserioSACE.SetValue(0);

        AspxComboBoxCentroEducativoSACE.AddItem('(Todos)', 0);
        AspxComboBoxCentroEducativoSACE.SetValue(0);
    }
}

function fnc_cargar_aldea_sace(intCodMunicipio)
{
    AspxComboBoxAldeaSACE.ClearItems();
    AspxComboBoxCaserioSACE.ClearItems();
    AspxComboBoxCentroEducativoSACE.ClearItems();

    if (intCodMunicipio > 0)
    {
        $.ajax({
            type: 'POST',
            url: '/Corresponsabilidad/Reportes/fnc_car_combo_aldea_sace',
            data: { intCodMuni: intCodMunicipio },
            success: function (response) {

                for (var intI = 0; intI < response.length; intI++) {
                    var arrJson = response[intI];
                    var strValueFiled = '';
                    var strTextField = '';

                    for (strIndice in arrJson) {
                        if (strIndice == 'cod_sac_ald_sac') {
                            strValueFiled = arrJson[strIndice];
                        } else {
                            strTextField = arrJson[strIndice];
                        }
                    }
                    AspxComboBoxAldeaSACE.AddItem(strTextField, strValueFiled);

                }
            }
        });
        fnc_cargar_combox_centro_educativo();
    } else
    {
        AspxComboBoxAldeaSACE.AddItem('(Todos)', 0);
        AspxComboBoxAldeaSACE.SetValue(0);

        AspxComboBoxCaserioSACE.AddItem('(Todos)', 0);
        AspxComboBoxCaserioSACE.SetValue(0);

        AspxComboBoxCentroEducativoSACE.AddItem('(Todos)', 0);
        AspxComboBoxCentroEducativoSACE.SetValue(0);
    } 
}

function fnc_cargar_caserio_sace(intCodAldea)
{
    AspxComboBoxCaserioSACE.ClearItems();
    AspxComboBoxCentroEducativoSACE.ClearItems();

    if (intCodAldea > 0) {
        $.ajax({
            type: 'POST',
            url: '/Corresponsabilidad/Reportes/fnc_car_combo_caserio_sace',
            data: { intCodAld: intCodAldea },
            success: function (response) {
                for (var intI = 0; intI < response.length; intI++) {
                    var arrJson = response[intI];
                    var strValueFiled = '';
                    var strTextField = '';

                    for (strIndice in arrJson) {
                        if (strIndice == 'cod_sac_cas_sac') {
                            strValueFiled = arrJson[strIndice];
                        } else {
                            strTextField = arrJson[strIndice];
                        }
                    }
                    AspxComboBoxCaserioSACE.AddItem(strTextField, strValueFiled);
                }
            }
        });
        fnc_cargar_combox_centro_educativo();
    } else
    {
        AspxComboBoxCaserioSACE.AddItem('(Todos)', 0);
        AspxComboBoxCaserioSACE.SetValue(0);

        AspxComboBoxCentroEducativoSACE.AddItem('(Todos)', 0);
        AspxComboBoxCentroEducativoSACE.SetValue(0);
    }  
}

function fnc_cargar_combox_centro_educativo()
{
    var intCodMunicipio = AspxComboBoxMunicipioSACE.GetValue();
    var intCodAldea = AspxComboBoxAldeaSACE.GetValue();
    var intCodCaserio = AspxComboBoxCaserioSACE.GetValue();
    intCodMunicipio = (intCodMunicipio == null ? -1 : intCodMunicipio);
    intCodAldea = (intCodAldea == null ? -1 : intCodAldea);
    intCodCaserio = (intCodCaserio == null ? -1 : intCodCaserio);

        $.ajax({
            type: 'POST',
            url: '/Corresponsabilidad/Reportes/fnc_car_combo_centro_educativo',
            data: { intCodMuni: intCodMunicipio, intCodAld: intCodAldea, intCodCase: intCodCaserio },
            success: function (response) {
                for (var intI = 0; intI < response.length; intI++) {
                    var arrJson = response[intI];
                    var strValueFiled = '';
                    var strTextField = '';

                    for (strIndice in arrJson) {
                        if (strIndice == 'cod_sac_cen_edu') {
                            strValueFiled = arrJson[strIndice];
                        } else {
                            strTextField = arrJson[strIndice];
                        }
                    }
                    AspxComboBoxCentroEducativoSACE.AddItem(strTextField, strValueFiled);

                }
            }
        });
}

function fnc_cargar_centro_educativo_por_caserio(intCodCaserio)
{
    AspxComboBoxCentroEducativoSACE.ClearItems();

    if (intCodCaserio > 0)
    {
        fnc_cargar_combox_centro_educativo();
    } else
    {
        AspxComboBoxCentroEducativoSACE.AddItem('(Todos)', 0);
        AspxComboBoxCentroEducativoSACE.SetValue(0);
    }
}

