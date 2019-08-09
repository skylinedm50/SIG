var objUbicaSIG = new Object();
objUbicaSIG.SelectAllOrNot = 0;
objUbicaSIG.Departamento = new Object();
objUbicaSIG.Municipio = new Object();
objUbicaSIG.Aldea = new Object();
objUbicaSIG.Caserio = new Object();

objUbicaSIG.Departamento.Codigos = ["00"];
objUbicaSIG.Municipio.Codigos = [["00", ["0000"]]];
objUbicaSIG.Aldea.Codigos = [["0000", ["000000"]]];
objUbicaSIG.Caserio.Codigos = [["000000", ["000000000"]]];

objUbicaSIG.Departamento.CodHijos = "0000"; //Cadena de texto donde se almacena los códigos de los municipios seleccionados.
objUbicaSIG.Departamento.ConHijos = "00"; //Cadena de texto donde se almacena los código de los departamentos con al menos un municipio seleccionado.
objUbicaSIG.Municipio.CodHijos = "000000";
objUbicaSIG.Municipio.ConHijos = "0000";
objUbicaSIG.Aldea.CodHijos = "000000000";
objUbicaSIG.Aldea.ConHijos = "000000";


objUbicaSIG.Municipio.CodLastPadre = "00"; //Código del ultimo padre que llamo al hijo.
objUbicaSIG.Aldea.CodLastPadre = "0000";
objUbicaSIG.Caserio.CodLastPadre = "000000";

objUbicaSIG.Municipio.LastPadreChecked = false; //Estado que identifica si el padre que llamo al hijo esta seleccionado.
objUbicaSIG.Aldea.LastPadreChecked = false;
objUbicaSIG.Caserio.LastPadreChecked = false;


objUbicaSIG.Municipio.CodigosSelectLastPadre = "0000" //Códigos seleccionasdos por el departamento que realizo el BeginCallBack.
objUbicaSIG.Aldea.CodigosSelectLastPadre = "000000"
objUbicaSIG.Caserio.CodigosSelectLastPadre = "000000000"



var strColorChecked = "#f2f2f2"; //Color hexadecimal cuando una fila esta seleccionada.
var strColorUnChecked = "#ffffff"; //Color hexadecimal cuando una fila no esta seleccionada.


//Función cuando un checkBox sufre algun cambio por parte del usuario.
function fnc_cambio_checked(objCheckBox, intCodCheckBox) {
    switch (intCodCheckBox) {
        case 1:
            AspxGridViewDepartamentoSIG.GetRow(parseInt(objCheckBox.id)).style.backgroundColor = ((objCheckBox.checked == true) ? strColorChecked : strColorUnChecked);
            break;
        case 2:
            AspxGridViewMunicipioSIG.GetRow(parseInt(objCheckBox.id)).style.backgroundColor = ((objCheckBox.checked == true) ? strColorChecked : strColorUnChecked);
            break;
        case 3:
            AspxGridViewAldeaSIG.GetRow(parseInt(objCheckBox.id)).style.backgroundColor = ((objCheckBox.checked == true) ? strColorChecked : strColorUnChecked);
            break;
        case 4:            
            AspxGridViewCaserioSIG.GetRow(parseInt(objCheckBox.id)).style.backgroundColor = ((objCheckBox.checked == true) ? strColorChecked : strColorUnChecked);
            break;
    }
    fnc_select_todos_hijos_visibles(intCodCheckBox, objCheckBox.checked, objCheckBox.value);
    fnc_check_unckeck_padre(objCheckBox.checked, intCodCheckBox);
    fnc_almacenar_checked();
    if (objCheckBox.checked == false) {
        fnc_eliminar_posicion_matriz(intCodCheckBox, objCheckBox.value);
        document.getElementsByName('selectDeSelect')[0].checked = false;
        objUbicaSIG.SelectAllOrNot = 0;
    } else {

        objUbicaSIG.SelectAllOrNot = 1;
    }
}


//Funciones para poder modificar desde los padres a los hijos.
function fnc_select_todos_hijos_visibles(intCodHijo, boChecked, strCodPadre){
    switch (intCodHijo) {
        case 1: //Cuando se selecciona el departamento y estan abiertos los hijos(municipios, aldea y caserio).
            fnc_recorrer_checkbox_checked(document.getElementsByName('checkboxMunicipio'), boChecked, 2, strCodPadre, 2);
            fnc_recorrer_checkbox_checked(document.getElementsByName('checkboxAldea'), boChecked, 3, strCodPadre, 2);
            fnc_recorrer_checkbox_checked(document.getElementsByName('checkboxCaserio'), boChecked, 4, strCodPadre, 2);
            break;
        case 2: //Cuando se selecciona el municipio y estan abiertos los hijos(aldea y caserio).
            fnc_recorrer_checkbox_checked(document.getElementsByName('checkboxAldea'), boChecked, 3, strCodPadre, 4);
            fnc_recorrer_checkbox_checked(document.getElementsByName('checkboxCaserio'), boChecked, 4, strCodPadre, 4);
            break;
        case 3: //Cuando se selecciona una aldea y estan abierto el hijos(caserio).
            fnc_recorrer_checkbox_checked(document.getElementsByName('checkboxCaserio'), boChecked, 4, strCodPadre, 6);
            break;
    }
}

function fnc_recorrer_checkbox_checked(objHtml, bolChecked, intCodGrid, strCodPadre, intCodRecortarStrCod)
{
    var inCantidad = 0;
    var strCod = null;
    inCantidad = objHtml.length;

    if (inCantidad > 0){
        strCod = objHtml[0].value.substring(0, intCodRecortarStrCod);

        if (strCod == strCodPadre) {
            for (var i = 0; i < inCantidad; i++) {
                objHtml[i].checked = bolChecked;
                switch (intCodGrid) {
                    case 1:
                        AspxGridViewDepartamentoSIG.GetRow(parseInt(objHtml[i].id)).style.backgroundColor = ((bolChecked == true) ? strColorChecked : strColorUnChecked);
                        break;
                    case 2:
                        AspxGridViewMunicipioSIG.GetRow(parseInt(objHtml[i].id)).style.backgroundColor = ((bolChecked == true) ? strColorChecked : strColorUnChecked);
                        break;
                    case 3:
                        AspxGridViewAldeaSIG.GetRow(parseInt(objHtml[i].id)).style.backgroundColor = ((bolChecked == true) ? strColorChecked : strColorUnChecked);
                        break;
                    case 4:
                        AspxGridViewCaserioSIG.GetRow(parseInt(objHtml[i].id)).style.backgroundColor = ((bolChecked == true) ? strColorChecked : strColorUnChecked);
                        break;
                }
            }
        }
    }
}


//Funciones para modificar desde los hijos a los padres.
function fnc_check_unckeck_padre(bolChecked, intCodHijo)
{
    var objCheckBoxDepartamento = null;
    var objCheckBoxMunicipio = null;
    var objCheckBoxAldea = null;
    var objCheckBoxCaserio = null;

    var strCodCaserio = null;
    var strCodAldea = null;
    var strCodMunicipio = null;
    var strCodDepartamento = null;


    if (intCodHijo > 1) { //El 1 representa el Departamento y por no tener padre no se toma en cuenta.
        objCheckBoxCaserio = document.getElementsByName('checkboxCaserio');
        objCheckBoxAldea = document.getElementsByName('checkboxAldea');
        objCheckBoxMunicipio = document.getElementsByName('checkboxMunicipio');
        objCheckBoxDepartamento = document.getElementsByName('checkboxDepartamento');

        strCodCaserio = ((objCheckBoxCaserio.length > 0) ? objCheckBoxCaserio[0].value.substring(0, 6) : null);
        strCodAldea = ((objCheckBoxAldea.length > 0) ? objCheckBoxAldea[0].value.substring(0, 4) : null);
        strCodMunicipio = ((objCheckBoxMunicipio.length > 0) ? objCheckBoxMunicipio[0].value.substring(0, 2) : null);
        
        switch (intCodHijo) {
            case 4:
                fnc_ckecked_padre(objCheckBoxCaserio, objCheckBoxAldea, 4, intCodHijo, strCodCaserio);
                fnc_ckecked_padre(objCheckBoxAldea, objCheckBoxMunicipio, 3, intCodHijo, strCodAldea);
                fnc_ckecked_padre(objCheckBoxMunicipio, objCheckBoxDepartamento, 2, intCodHijo, strCodMunicipio);
                break;
            case 3:
                fnc_ckecked_padre(objCheckBoxAldea, objCheckBoxMunicipio, 3, intCodHijo, strCodAldea);
                fnc_ckecked_padre(objCheckBoxMunicipio, objCheckBoxDepartamento, 2, intCodHijo, strCodMunicipio);
            case 2:
                fnc_ckecked_padre(objCheckBoxMunicipio, objCheckBoxDepartamento, 2, intCodHijo, strCodMunicipio);
                break;
        }
    }

}

function fnc_ckecked_padre(objCheckBoxHijo, objCheckBoxPadre, intCodHijo, intHijoEjecutor, strCodHijo) { //Función para manipular el padre desde los hijos.
    var intCantCheckedHijo = null; //Variable que almacena la cantidad de checked que contiene un hijo.

    //Indetificar la cantidad de checked = true del hijo o del que disparo el evento.
    for (var i = 0; i < objCheckBoxHijo.length; i++) {
        if (objCheckBoxHijo[i].checked == true) {
            intCantCheckedHijo++;
        }
    }

    if (intCodHijo == intHijoEjecutor) //Identificar el hijo que sufrio cambios, de esa forma identificar la cantidad de checked que posee.
    {
        intExistCheckedHijoMenor = intCantCheckedHijo;
    }

        for (var i = 0; i < objCheckBoxPadre.length; i++) { //Recorremos el padre a fin de poderlo manipular dependiendo de los checked del hijo.
            switch (intCodHijo) {
                case 4: //En caso de que el hijo sea el Caserio y el padre la Aldea
                    if (objCheckBoxPadre[i].value == strCodHijo) //Identificamos que el código del hijo sea igual al del padre para afectar solo al padre de ese hijo..
                    {
                        if (intCantCheckedHijo == objCheckBoxHijo.length) {//Identificar que la cantidad de checked del hijo sea total o completa.
                            objCheckBoxPadre[i].checked = true;
                            AspxGridViewAldeaSIG.GetRow(parseInt(objCheckBoxPadre[i].id)).style.backgroundColor = strColorChecked;
                        } else {//En caso que que la cantidad de checked sea nula o alguna por lo menos.
                            if (intCantCheckedHijo == null && intExistCheckedHijoMenor == null) { //En caso que no se ha seleccionado nada del hijo.
                                objCheckBoxPadre[i].checked = false;
                                AspxGridViewAldeaSIG.GetRow(parseInt(objCheckBoxPadre[i].id)).style.backgroundColor = strColorUnChecked;
                            }
                            else { //En caso que algun valor del hijo se selecciono.
                                objCheckBoxPadre[i].checked = false;
                                AspxGridViewAldeaSIG.GetRow(parseInt(objCheckBoxPadre[i].id)).style.backgroundColor = strColorChecked;
                            }                            
                        }
                    }
                    break;
                case 3: //En caso de que el hijo sea la Aldea y el padre el Municipio
                    if (objCheckBoxPadre[i].value == strCodHijo) {
                        if (intCantCheckedHijo == objCheckBoxHijo.length) {
                            objCheckBoxPadre[i].checked = true;
                            AspxGridViewMunicipioSIG.GetRow(parseInt(objCheckBoxPadre[i].id)).style.backgroundColor = strColorChecked;
                        } else {
                            if (intCantCheckedHijo == null && intExistCheckedHijoMenor == null) {
                                objCheckBoxPadre[i].checked = false;
                                AspxGridViewMunicipioSIG.GetRow(parseInt(objCheckBoxPadre[i].id)).style.backgroundColor = strColorUnChecked;
                            }
                            else {
                                objCheckBoxPadre[i].checked = false;
                                AspxGridViewMunicipioSIG.GetRow(parseInt(objCheckBoxPadre[i].id)).style.backgroundColor = strColorChecked;
                            }
                        }
                    }
                    break;
                case 2: //En caso de que el hijo sea el Municipio y el padre el Departamento
                    if (objCheckBoxPadre[i].value == strCodHijo) {
                        if (intCantCheckedHijo == objCheckBoxHijo.length) {
                            objCheckBoxPadre[i].checked = true;
                            AspxGridViewDepartamentoSIG.GetRow(parseInt(objCheckBoxPadre[i].id)).style.backgroundColor = strColorChecked;
                        } else {
                            if (intCantCheckedHijo == null && intExistCheckedHijoMenor == null) {
                                objCheckBoxPadre[i].checked = false;
                                AspxGridViewDepartamentoSIG.GetRow(parseInt(objCheckBoxPadre[i].id)).style.backgroundColor = strColorUnChecked;
                            }
                            else {
                                objCheckBoxPadre[i].checked = false;
                                AspxGridViewDepartamentoSIG.GetRow(parseInt(objCheckBoxPadre[i].id)).style.backgroundColor = strColorChecked;
                            }
                        }
                    }
                    break;
            }
        }
}


//Función para poder guardar los valores y mantenerlos en el navegador y enviarlos al servidor.
function fnc_almacenar_checked()
{
    var objCheckBox = [];
    var strIndice = null;
    var arrConCodigos = [];

    for (var i = 0; i < 4; i++)
    {
        switch (i) {
            case 0:
                objCheckBox = document.getElementsByName('checkboxCaserio');
                strIndice = ((objCheckBox.length > 0) ? objCheckBox[0].value.substring(0, 6) : "000000");
                arrConCodigos = fnc_obtener_codigo_select(objCheckBox);

                //objUbicaSIG.Caserio.Codigos[strIndice] = ((arrConCodigos.length > 0) ? arrConCodigos : ['000000000']);
                objUbicaSIG.Caserio.Codigos = fnc_agregar_posicion_matriz(i, objUbicaSIG.Caserio.Codigos, strIndice, arrConCodigos);
                break;
            case 1:
                objCheckBox = document.getElementsByName('checkboxAldea');
                strIndice = ((objCheckBox.length > 0) ? objCheckBox[0].value.substring(0, 4) : "0000");
                arrConCodigos = fnc_obtener_codigo_select(objCheckBox);

                //objUbicaSIG.Aldea.Codigos[strIndice] = ((arrConCodigos.length > 0) ? arrConCodigos : ['000000']);

                objUbicaSIG.Aldea.Codigos = fnc_agregar_posicion_matriz(i, objUbicaSIG.Aldea.Codigos, strIndice, arrConCodigos);
                break;
            case 2:
                objCheckBox = document.getElementsByName('checkboxMunicipio');
                strIndice = ((objCheckBox.length > 0) ? objCheckBox[0].value.substring(0, 2) : "00");
                arrConCodigos = fnc_obtener_codigo_select(objCheckBox);

                objUbicaSIG.Municipio.Codigos = fnc_agregar_posicion_matriz(i, objUbicaSIG.Municipio.Codigos, strIndice, arrConCodigos);
                break;
            case 3:
                objCheckBox = document.getElementsByName('checkboxDepartamento');                
                arrConCodigos = fnc_obtener_codigo_select(objCheckBox);
                objUbicaSIG.Departamento.Codigos = fnc_agregar_departamento(objUbicaSIG.Departamento.Codigos, arrConCodigos);
                break;
        }
    }
}

function fnc_obtener_codigo_select(objCheckBox) //Función para obtener los códigos de los checkBox seleccionados.
{
    var arrCodigos = []; //Arreglo donde se almacena los códigos seleccionados.

    for (var i = 0; i < objCheckBox.length; i++) { //Se recorre el objeto recibido.
        if (objCheckBox[i].checked == true) {
            arrCodigos.push(objCheckBox[i].value);
        }
    }    
    return arrCodigos;
}



function fnc_agregar_posicion_matriz(intCod, objArreglo, strCodBuscar, objArrAgregar) {
    var strCodNull = null;
    var bolYaExiste = false;
    var intIndiceExiste = null;

    switch (intCod) {
        case 2:
            strCodNull = "0000";
            break;
        case 1:
            strCodNull = "000000";
            break;
        case 0:
            strCodNull = "000000000";
            break;
    }

    if (objArreglo.length > 0) {
        for (var i = 0; i < objArreglo.length; i++) {
            if (objArreglo[i][0] == strCodBuscar) {
                bolYaExiste = true;
                intIndiceExiste = i;
                break;
            }
        }

        if (bolYaExiste !== true) {
            objArreglo.push(((objArrAgregar.length > 0) ? new Array(strCodBuscar, objArrAgregar) : new Array(strCodBuscar, new Array(strCodNull))));
        } else {
            objArreglo[intIndiceExiste][1] = ((objArrAgregar.length > 0) ? objArrAgregar : new Array(strCodNull));
        }
    } else {
        objArreglo.push(((objArrAgregar.length > 0) ? new Array(strCodBuscar, objArrAgregar) : new Array(strCodBuscar, new Array(strCodNull))));
    }

    return objArreglo;
}

function fnc_agregar_departamento(objArreglo, objArrAgregar) {
    var bolYaExiste = false;
    var intIndiceExiste = null;

    for (var i = 0; i < objArrAgregar.length; i++) {
        for (var j = 0; j < objArreglo.length; j++) {
            if ( objArrAgregar[i] == objArreglo[j]) {
                bolYaExiste = true;
            }
        }
        if (bolYaExiste !== true) {
            objArreglo.push(objArrAgregar[i]);            
        }
        bolYaExiste = false;
    }
    return objArreglo;
}



function fnc_eliminar_posicion_matriz(intCod, strCodigo) {
    switch (intCod) {
        case 1:
            objUbicaSIG.Departamento.Codigos = fnc_borrar_indice_matriz(objUbicaSIG.Departamento.Codigos, strCodigo, null, 1, null);
            objUbicaSIG.Municipio.Codigos = fnc_borrar_indice_matriz(objUbicaSIG.Municipio.Codigos, strCodigo, 2, 2, false);
            objUbicaSIG.Aldea.Codigos = fnc_borrar_indice_matriz(objUbicaSIG.Aldea.Codigos, strCodigo, 2, 2, false);
            objUbicaSIG.Caserio.Codigos = fnc_borrar_indice_matriz(objUbicaSIG.Caserio.Codigos, strCodigo, 2, 2, false);
        case 2:
            objUbicaSIG.Departamento.Codigos = fnc_borrar_indice_matriz(objUbicaSIG.Departamento.Codigos, strCodigo.substring(0, 2), null, 1, null);
            objUbicaSIG.Municipio.Codigos = fnc_borrar_indice_matriz(objUbicaSIG.Municipio.Codigos, strCodigo, 4, 2, true);
            objUbicaSIG.Aldea.Codigos = fnc_borrar_indice_matriz(objUbicaSIG.Aldea.Codigos, strCodigo, 4, 2, false);
            objUbicaSIG.Caserio.Codigos = fnc_borrar_indice_matriz(objUbicaSIG.Caserio.Codigos, strCodigo, 4, 2, false);
            break;
        case 3:
            objUbicaSIG.Departamento.Codigos = fnc_borrar_indice_matriz(objUbicaSIG.Departamento.Codigos, strCodigo.substring(0, 2), null, 1, null);
            objUbicaSIG.Municipio.Codigos = fnc_borrar_indice_matriz(objUbicaSIG.Municipio.Codigos, strCodigo.substring(0, 4), 4, 2, true);
            objUbicaSIG.Aldea.Codigos = fnc_borrar_indice_matriz(objUbicaSIG.Aldea.Codigos, strCodigo, 6, 2, true);
            objUbicaSIG.Caserio.Codigos = fnc_borrar_indice_matriz(objUbicaSIG.Caserio.Codigos, strCodigo, 6, 2, false);
            break;
        case 4:
            objUbicaSIG.Departamento.Codigos = fnc_borrar_indice_matriz(objUbicaSIG.Departamento.Codigos, strCodigo.substring(0, 2), null, 1, null);
            objUbicaSIG.Municipio.Codigos = fnc_borrar_indice_matriz(objUbicaSIG.Municipio.Codigos, strCodigo.substring(0, 4), 4, 2, true);
            objUbicaSIG.Aldea.Codigos = fnc_borrar_indice_matriz(objUbicaSIG.Aldea.Codigos, strCodigo, 6, 2, true);
            objUbicaSIG.Caserio.Codigos = fnc_borrar_indice_matriz(objUbicaSIG.Caserio.Codigos, strCodigo, 9, 2, true);
            break;
    }
}

function fnc_borrar_indice_matriz(objArr, strCodigo, intSubString, intCodTipoMatriz, bolBorrarPosionNoArray) {
    switch (intCodTipoMatriz) {
        case 1: //Para arreglo unidimensionales
            for (var i = 0; i < objArr.length; i++) {
                if (objArr[i] == strCodigo) {
                    objArr.splice(i, 1);
                    break;
                }
            }
            break;
        case 2: //Para matrices
            for (var i = 0; i < objArr.length; i++) {
                if (objArr[i][0].substring(0, intSubString) == strCodigo) {
                    if (bolBorrarPosionNoArray == false) {
                        objArr.splice(i, 1);
                        break;
                    } else {
                        for (var j = 0; j < objArr[i][0].length; j++) {
                            if (objArr[i][1][j] == strCodigo) {
                                objArr[i][1].splice(j, 1);
                                break;
                            }
                        }
                    }
                }
                //}
            }
            break;
    }
    return objArr;
}


function fnc_identificar_codigos(intCod, strCodigo) { //Función para poder preparar los códigos que se enviaran a cada vista esto depende del objeto que llama la función.
    var strRespuesta = new String(null);

    var objArrCodHijosSelect = []; //Arrgelo que contiene los códigos de los hijos seleccionados.
    var objArrCodPadreConHijos = [];  //Arreglo que contiene los códigos de los padres que tienen algún hijo.
    var objArrCodNietos = []; //Arreglo donde se almacena la información de los nietos de un padre.


    switch (intCod) {
        case 1:
            objArrCodPadreConHijos = ["00"];
            objArrCodHijosSelect = ["0000"];
            objArrCodNietos = ["0000"];

            //Identificar los departamentos con hijos.
            objArrCodPadreConHijos = fnc_identificar_codigo_padre_con_hijos(objUbicaSIG.Caserio.Codigos, objArrCodPadreConHijos, 2, null, "000000000");
            objArrCodPadreConHijos = fnc_identificar_codigo_padre_con_hijos(objUbicaSIG.Aldea.Codigos, objArrCodPadreConHijos, 2, null, "000000");
            objArrCodPadreConHijos = fnc_identificar_codigo_padre_con_hijos(objUbicaSIG.Municipio.Codigos, objArrCodPadreConHijos, 2, null, "0000");

            //Identificar los municipios del departamento cuales estan seleccionados.
            objArrCodHijosSelect = fnc_identificar_codigo_padre_con_hijos(objUbicaSIG.Municipio.Codigos, objArrCodHijosSelect, 2, strCodigo, null);

            //Identificar los municipios con hijos.
            objArrCodNietos = fnc_identificar_codigo_padre_con_hijos(objUbicaSIG.Caserio.Codigos, objArrCodNietos, 4, null, "000000000");
            objArrCodNietos = fnc_identificar_codigo_padre_con_hijos(objUbicaSIG.Aldea.Codigos, objArrCodNietos, 4, null, "000000");

            objUbicaSIG.Departamento.CodHijos = objArrCodHijosSelect.join(',');
            objUbicaSIG.Departamento.ConHijos = objArrCodPadreConHijos.join(',');

            objUbicaSIG.Municipio.ConHijos = objArrCodNietos.join(',');
            objUbicaSIG.Municipio.CodLastPadre = strCodigo;
            break;
        case 2:
            objArrCodPadreConHijos = ["0000"];
            objArrCodHijosSelect = ["000000"];
            objArrCodNietos = ["000000"];

            //Identificar los municipios con hijos.
            objArrCodPadreConHijos = fnc_identificar_codigo_padre_con_hijos(objUbicaSIG.Caserio.Codigos, objArrCodPadreConHijos, 4, null, "000000000");
            objArrCodPadreConHijos = fnc_identificar_codigo_padre_con_hijos(objUbicaSIG.Aldea.Codigos, objArrCodPadreConHijos, 4, null, "000000");

            //Identificar las aldeas del municipio cuales estan seleccionadas.
            objArrCodHijosSelect = fnc_identificar_codigo_padre_con_hijos(objUbicaSIG.Aldea.Codigos, objArrCodHijosSelect, 4, strCodigo, null);

            //Identificar los aldeas con hijos.
            objArrCodNietos = fnc_identificar_codigo_padre_con_hijos(objUbicaSIG.Caserio.Codigos, objArrCodNietos, 6, null, "000000000");

            objUbicaSIG.Municipio.CodHijos = objArrCodHijosSelect.join(',');
            objUbicaSIG.Municipio.ConHijos = objArrCodPadreConHijos.join(',');

            objUbicaSIG.Aldea.ConHijos = objArrCodNietos.join(',');
            objUbicaSIG.Aldea.CodLastPadre = strCodigo;
            break;
        case 3:
            objArrCodPadreConHijos = ["000000"];
            objArrCodHijosSelect = ["000000000"];


            //Identificar las aldeas con hijos.
            objArrCodPadreConHijos = fnc_identificar_codigo_padre_con_hijos(objUbicaSIG.Caserio.Codigos, objArrCodPadreConHijos, 6, null, "000000000");

            //Identificar los caserios de las aldeas que estan seleccionadas.
            objArrCodHijosSelect = fnc_identificar_codigo_padre_con_hijos(objUbicaSIG.Caserio.Codigos, objArrCodHijosSelect, 6, strCodigo, null);

            objUbicaSIG.Aldea.CodHijos = objArrCodHijosSelect.join(',');
            objUbicaSIG.Aldea.ConHijos = objArrCodPadreConHijos.join(',');

            objUbicaSIG.Caserio.CodLastPadre = strCodigo;
            break;
    }
}

function fnc_identificar_codigo_padre_con_hijos(objArrHijo, objArrAlmaCodPAdre, intNumeSubString, strCodBuscar, strCodNoPermitido) { //Función para poder identificar los  códigos de padre que contiene algun hijo.
    var bolYaExiste = false;
    var strCodInicial = new String("0000000000");
    var objArrCodHijosEnPadre = [strCodInicial.substring(0, intNumeSubString)];

    for (var i = 0; i < objArrHijo.length; i++) {
        for (var j = 0; j < objArrAlmaCodPAdre.length; j++) {
            if (objArrHijo[i][0].substring(0, intNumeSubString) == objArrAlmaCodPAdre[j]) { //Identificar sin en la matriz en la posición 0 donde estan el código del padre.
                bolYaExiste = true;
            }
        }

        if (bolYaExiste !== true && objArrHijo[i][1][0] !== strCodNoPermitido) {
            objArrAlmaCodPAdre.push(objArrHijo[i][0].substring(0, intNumeSubString));
        }

        bolYaExiste = false;

        if (strCodBuscar !== null && objArrHijo[i][0] == strCodBuscar) {
            objArrCodHijosEnPadre = objArrHijo[i][1];
        }
    }

    return ((strCodBuscar == null ) ? objArrAlmaCodPAdre : objArrCodHijosEnPadre);
}

function fnc_llamar_funciones_main(intCod, strCodigo)
{
    fnc_almacenar_checked();
    fnc_verificar_padres_select(intCod);

    if (strCodigo !== null) {
        fnc_identificar_codigos(intCod, strCodigo);
    }

    objUbicaSIG.Municipio.CodigosSelectLastPadre = fnc_identificar_en_matriz_codigos_select(objUbicaSIG.Municipio.Codigos, objUbicaSIG.Municipio.CodLastPadre);
    objUbicaSIG.Aldea.CodigosSelectLastPadre = fnc_identificar_en_matriz_codigos_select(objUbicaSIG.Aldea.Codigos, objUbicaSIG.Aldea.CodLastPadre);
    objUbicaSIG.Caserio.CodigosSelectLastPadre = fnc_identificar_en_matriz_codigos_select(objUbicaSIG.Caserio.Codigos, objUbicaSIG.Caserio.CodLastPadre);
}


function fnc_verificar_padres_select(intCod) {
    switch (intCod) {
        case 2:
            for (var i = 0; i < objUbicaSIG.Departamento.Codigos.length; i++) {
                if (objUbicaSIG.Departamento.Codigos[i] !== "00" && objUbicaSIG.Municipio.CodLastPadre == objUbicaSIG.Departamento.Codigos[i]) {
                    objUbicaSIG.Municipio.LastPadreChecked = true;
                    break;
                }
            }            
            break;
        case 3:
            for (var i = 0; i < objUbicaSIG.Municipio.Codigos.length; i++) {
                if (objUbicaSIG.Municipio.Codigos[i][0] !== "0000" && objUbicaSIG.Aldea.CodLastPadre == objUbicaSIG.Municipio.Codigos[i][0]) {
                    objUbicaSIG.Aldea.LastPadreChecked = true;
                    break;
                }
            }
        case 4:
            for (var i = 0; i < objUbicaSIG.Aldea.Codigos.length; i++) {
                if (objUbicaSIG.Aldea.Codigos[i][0] !== "000000" && objUbicaSIG.Caserio.CodLastPadre == objUbicaSIG.Aldea.Codigos[i][0]) {
                    objUbicaSIG.Caserio.LastPadreChecked = true;
                    break;
                }
            }
            break;
    }
}

function fnc_identificar_en_matriz_codigos_select(objArr, strCodLastPadre) {
    var strCodNull = new String("000000000");
    var objArrRepuesta = [strCodNull.substring(0, strCodLastPadre.length)];

    for (var i = 0; i < objArr.length; i++) {
        if (strCodLastPadre == objArr[i][0]) {
            objArrRepuesta = objArr[i][1];
            break;
        }
    }
    return objArrRepuesta.join(',');
}


function fnc_obtener_valores_seleccionados() {

    var objRespuesta = new Object();
    objRespuesta.Departamento = objUbicaSIG.Departamento.Codigos;
    objRespuesta.Municipio = objUbicaSIG.Municipio.Codigos;
    objRespuesta.Aldea = objUbicaSIG.Aldea.Codigos;
    objRespuesta.Caserio = objUbicaSIG.Caserio.Codigos;

   
    for (var i = 0; i < objRespuesta.Departamento.length; i++) {
        for (var j = 0; j < objRespuesta.Municipio.length; j++) {
            if (objRespuesta.Departamento[i] == objRespuesta.Municipio[j][0].substring(0, 2)) {
                objRespuesta.Municipio.splice(j, 1);
            }
            for (var k = 0; k < objRespuesta.Aldea.length; k++) {
                if (objRespuesta.Departamento[i] == objRespuesta.Aldea[k][0].substring(0, 2)) {
                    objRespuesta.Aldea.splice(k, 1);
                }
                for (var l = 0; l < objRespuesta.Caserio.length; l++) {
                    if (objRespuesta.Departamento[i] == objRespuesta.Caserio[l][0].substring(0, 2)) {
                        objRespuesta.Caserio.splice(l, 1);
                    }
                }
            }
        }
        if (objRespuesta.Departamento[i] == "00") {
            objRespuesta.Departamento.splice(i, 1);
        }
    }

    
    for (var i = 0; i < objRespuesta.Municipio.length; i++) {
        for (var j = 0; j < objRespuesta.Municipio[i][1].length; j++) {
            for (var k = 0; k < objRespuesta.Aldea.length; k++) {
                if (objRespuesta.Municipio[i][1][j] == objRespuesta.Aldea[k][0].substring(0, 4)) {
                    objRespuesta.Aldea.splice(k, 1);
                }
                for (var l = 0; l < objRespuesta.Caserio.length; l++) {
                    if (objRespuesta.Municipio[i][1][j] == objRespuesta.Caserio[l][0].substring(0, 4)) {
                        objRespuesta.Caserio.splice(l, 1);
                    }
                }
            }
        }
    }


    for (var i = 0; i < objRespuesta.Aldea.length; i++) {
        for (var j = 0; j < objRespuesta.Aldea[i][1].length; j++) {
            for (var k = 0; k < objRespuesta.Caserio.length; k++) {
                if (objRespuesta.Aldea[i][1][j] == objRespuesta.Caserio[k][0].substring(0, 6)) {
                    objRespuesta.Caserio.splice(k, 1);
                }
            }
        }
    }



    objRespuesta.Departamento = ((objRespuesta.Departamento.length > 0) ? objRespuesta.Departamento : ['00']);
    var arrTemporal = [];
    for (var i = 0; i < objRespuesta.Municipio.length; i++) {
        if (objRespuesta.Municipio[i][0] !== "00" && objRespuesta.Municipio[i][1][0] !== "0000")
        {
            for (var j = 0; j < objRespuesta.Municipio[i][1].length; j++) {
                arrTemporal.push(objRespuesta.Municipio[i][1][j]);
            }
        }
    }
    objRespuesta.Municipio = ((arrTemporal.length > 0) ? arrTemporal : ['0000']);

    arrTemporal = [];
    for (var i = 0; i < objRespuesta.Aldea.length; i++) {
        if (objRespuesta.Aldea[i][0] !== "0000" && objRespuesta.Aldea[i][1][0] !== "000000") {
            for (var j = 0; j < objRespuesta.Aldea[i][1].length; j++) {
                arrTemporal.push(objRespuesta.Aldea[i][1][j]);
            }
        }
    }
    objRespuesta.Aldea = ((arrTemporal.length > 0) ? arrTemporal : ['000000']);

    arrTemporal = [];
    for (var i = 0; i < objRespuesta.Caserio.length; i++) {
        if (objRespuesta.Caserio[i][0] !== "000000" && objRespuesta.Caserio[i][1][0] !== "000000000") {
            for (var j = 0; j < objRespuesta.Caserio[i][1].length; j++) {
                arrTemporal.push(objRespuesta.Caserio[i][1][j]);
            }
        }
    }
    objRespuesta.Caserio = ((arrTemporal.length > 0) ? arrTemporal : ['000000000']);

    
    return objRespuesta;
}




function fnc_manejo_select_de_select_all(objCheckeBox)
{
    var boolValor = objCheckeBox.checked;
    var objDepar = document.getElementsByName('checkboxDepartamento');

    for (var i = 0; i < objDepar.length; i++) {
        objDepar[i].checked = boolValor;
        fnc_cambio_checked(objDepar[i], 1);
    }
}
