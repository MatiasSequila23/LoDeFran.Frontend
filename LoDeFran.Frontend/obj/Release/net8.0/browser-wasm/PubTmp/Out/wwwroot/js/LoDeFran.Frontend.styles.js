// Esta función se utilizará para inicializar el DataTable en la tabla especificada
//function initializeDataTable(tableId) {
//    $("#" + tableId).DataTable();
//}

  window.imprimirModal = (selector) => {
    const modalContent = document.querySelector(selector + ' .modal-body').innerHTML;
    const win = window.open('', '', 'width=800,height=600');
    win.document.write('<html><head><title>Cliente</title></head><body>');
    win.document.write(modalContent);
    win.document.write('</body></html>');
    win.document.close();
    win.print();
};

window.openTicketWindow = (html) => {
    const win = window.open('', '_blank');
    win.document.write(html);
    win.document.close();
};
