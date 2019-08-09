
// funciones para la selección en el dropdownedit
var textSeparator = ";";

function OnListBoxSelectionChanged(listBox, args) {
    if (args.index == 0)
        args.isSelected ? listBox.SelectAll() : listBox.UnselectAll();
    UpdateSelectAllItemState();
    UpdateText();
}

function UpdateSelectAllItemState() {
    IsAllSelected() ? checkListBox.SelectIndices([0]) : checkListBox.UnselectIndices([0]);
}

function IsAllSelected() {
    for (var i = 1; i < checkListBox.GetItemCount() ; i++)
        if (!checkListBox.GetItem(i).selected)
            return false;
    return true;
}

function UpdateText() {
    var selectedItems = checkListBox.GetSelectedItems();
    checkComboBox.SetText(GetSelectedItemsText(selectedItems));
}

function SynchronizeListBoxValues(dropDown, args) {
    checkListBox.UnselectAll();
    var texts = dropDown.GetText().split(textSeparator);
    var values = GetValuesByTexts(texts);
    checkListBox.SelectValues(values);
    UpdateSelectAllItemState();
    UpdateText(); // for remove non-existing texts
}

function GetSelectedItemsText(items) {
    var texts = [];
    for (var i = 0; i < items.length; i++)
        if (items[i].index != 0)
            texts.push(items[i].text);
    return texts.join(textSeparator);
}

function GetValuesByTexts(texts) {
    var actualValues = [];
    var item;
    for (var i = 0; i < texts.length; i++) {
        item = checkListBox.FindItemByText(texts[i]);
        if (item != null)
            actualValues.push(item.value);
    }
    return actualValues;
}

// función para llamar a la función que obtiene las planillas y recarga el grid
function GetPlanillasAbiertas() {

    //debugger;
    var array = checkListBox.GetSelectedValues();
    var values = array.toString();

    //debugger;
    if (values.length > 0) {
        $.ajax({
            type: 'POST',
            //dataType: 'Json',
            traditional: true,
            //url: '/CierrePlanilla/getPlanillasAbiertas',
            //url: '/Contraloria/CierrePlanilla/getPlanillasAbiertas',
            url: '/Contraloria/CierrePlanilla/PartialGridViewPlanillas',
            data: { strDeptos: values },
            success: function (response) {
                if (response) {
                    $('#divGridView').html(response);
                    //flag = true;
                } else {
                    alert("Ocurrio un error al intentar traer la información de las planillas. Por favor intentelo de nuevo.");
                }
            }

        });
        //$('#divGridView').load('/CierrePlanilla/PartialGridView');
        //$('#divGridView').load('/Contraloria/CierrePlanilla/PartialGridView');
    } else {
        alert("No ha seleccionado ningun departamento");
    }

}

// función que se ejecuta antes cuando empieza el callback del gridview
function gdvOnBeginCallBack(s, e) {
    e.customArgs["deptos"] = checkListBox.GetSelectedValues();
}

// funciones para obtener las filas seleccionadas en el grid
var selectedIDs;
function OnBeginCallback(s, e) {
    //Pass all selected keys to GridView callback action
    e.customArgs["selectedIDs"] = selectedIDs;
}
function OnSelectionChanged(s, e) {
    debugger;
    s.GetSelectedFieldValues("cod_aldea", GetSelectedFieldValuesCallback);
}
function GetSelectedFieldValuesCallback(values) {
    //Capture all selected keys
    selectedIDs = values.join(',');
}
function OnClick(s, e) {
    //Show all selected keys on client side

    if (confirm("Esta seguro de cerrar las planillas")) {
        //alert(selectedIDs);
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            traditional: true,
            url: '/Contraloria/CierrePlanilla/cerrarPlanillas',
            data: { values: selectedIDs },
            success: function (response) {
                //debugger;
                if (response) {
                    
                    //$('#divGridView').load('/CierrePlanilla/PartialGridViewPlanillas');

                    //debugger;

                    if (response) {
                        var array = checkListBox.GetSelectedValues();
                        var values = array.toString();

                        $('#divGridView').load('/Contraloria/CierrePlanilla/PartialGridViewPlanillas?strDeptos=' + values);
                        alert("Planillas cerrardas exitosamente.");
                    } else {
                        alert("Ocurrio un error al cerrar las planillas.");
                    }
                    
                } else {
                    alert("Ocurrio un error al cerrar las planillas.");
                }
            }
        });
    }
}
function OnSubmitClick(s, e) {
    //Write all selected keys to hidden input. Pass them on post action.
    $("#selectedIDsHF").val(selectedIDs);
}





// función del cierre general del período
function btnCerrarClick(s, e) {

    if (confirm("Confirmación de cierre de periodo.\nPresione aceptar para confirmar el cierre del periodo.")) {
        $.ajax({
            type: 'POST',
            url: '/Contraloria/CierrePlanilla/cierrePeriodo',
            success: function (response) {
                debugger;
                if (response) {
                    alert("Periodo cerrado exitosamente.");
                    location.reload();
                } else {
                    alert("Ocurrio un error al cerrar el periodo.");
                }
            }

        });
    }
}