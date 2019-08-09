objBusqCorresBenef = new Object()

objBusqCorresBenef.Session = new Object()
objBusqCorresBenef.Session.intCodBenef = 0
objBusqCorresBenef.Session.intCodBenefRUP = 0
objBusqCorresBenef.Session.strIdentBenef = "null"
objBusqCorresBenef.Session.strNom1Benef = "null"
objBusqCorresBenef.Session.strNom2Benef = "null"
objBusqCorresBenef.Session.strApe1Benef = "null"
objBusqCorresBenef.Session.strApe2Benef = "null"
objBusqCorresBenef.Session.strIdentTitu = "null"
objBusqCorresBenef.Session.intCodHogRUP = 0
objBusqCorresBenef.Session.intCodHogSIG = 0
objBusqCorresBenef.Session.intCodBenefBusCorres = 0



objBusqCorresBenef.SelectBeneficiario = function (intCodBenef) {
    objBusqCorresBenef.Session.intCodBenefBusCorres = intCodBenef;
    AspxDocumentViewerBusqCorresBenef.PerformDataCallback();
    AspxDocumentViewerBusqCorresBenef.Refresh();
    AspxPopupControlReportCorresBenef.Show();
   
}

objBusqCorresBenef.BuscarCorresBenef = function () {
    var patt = /^\s/g;  
    var intCodBenef = AspxSpintEditCodBenef.GetValue();
    var intCodBenefRUP = AspxSpintEditCodRUPBenef.GetValue();

    var strIdentBenef = AspxTextBoxIdentBenef.GetValue();
    var strNom1Benef = AspxTextBoxNom1Benef.GetValue();
    var strNom2Benef = AspxTextBoxNom2Benef.GetValue();
    var strApe1Benef = AspxTextBoxApelli1Benef.GetValue();
    var strApe2Benef = AspxTextBoxApelli2Benef.GetValue();
    var strIdentTitu = AspxTextBoxIdentTitular.GetValue();
    var intCodHogRUP = AspxSpintEditCodRUPHog.GetValue();
    var intCodHogSIG = AspxSpintEditCodSIGHog.GetValue();

    if (patt.test(strIdentBenef) == true || patt.test(strNom1Benef) == true || patt.test(strNom2Benef) == true || patt.test(strApe1Benef) == true || patt.test(strApe2Benef) == true || patt.test(strIdentTitu) == true) {
        AspxLabelError.SetText("*ERROR, en alguno de los campos existe un espacio en blanco.");
    } else {
        if (intCodBenef == null && intCodBenefRUP == null && strIdentBenef == null && strNom1Benef == null && strNom2Benef == null && strApe1Benef == null && strApe2Benef == null && strIdentTitu == null && intCodHogRUP == null && intCodHogSIG == null) {
            AspxLabelError.SetText("*ERROR, imposible realizar una búsqueda ingrese por lo menos algún parametros para realizar la operación.");
        } else {
            objBusqCorresBenef.Session.intCodBenef = ((intCodBenef == null) ? 0 : intCodBenef);
            objBusqCorresBenef.Session.intCodBenefRUP = ((intCodBenefRUP == null) ? 0 : intCodBenefRUP);
            objBusqCorresBenef.Session.strIdentBenef = ((strIdentBenef == null) ? "null" : strIdentBenef);
            objBusqCorresBenef.Session.strNom1Benef = ((strNom1Benef == null) ? "null" : strNom1Benef);
            objBusqCorresBenef.Session.strNom2Benef = ((strNom2Benef == null) ? "null" : strNom2Benef);
            objBusqCorresBenef.Session.strApe1Benef = ((strApe1Benef == null) ? "null" : strApe1Benef);
            objBusqCorresBenef.Session.strApe2Benef = ((strApe2Benef == null) ? "null" : strApe2Benef);
            objBusqCorresBenef.Session.strIdentTitu = ((strIdentTitu == null) ? "null" : strIdentTitu);
            objBusqCorresBenef.Session.intCodHogRUP = ((intCodHogRUP == null) ? 0 : intCodHogRUP);
            objBusqCorresBenef.Session.intCodHogSIG = ((intCodHogSIG == null) ? 0 : intCodHogSIG);
            AspxLabelError.SetText("")
            AspxGridViewResultBusqCorrBenef.PerformCallback();
        }
    }
}