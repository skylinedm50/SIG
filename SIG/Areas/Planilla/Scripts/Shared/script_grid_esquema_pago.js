function fnc_cbxPagosIndexChaged() {
    try {
        GridViewEsquemasPorPago.PerformCallback();
    } catch (e) {
        console.log('No existe el gridview de los esquemas');
    }
}


