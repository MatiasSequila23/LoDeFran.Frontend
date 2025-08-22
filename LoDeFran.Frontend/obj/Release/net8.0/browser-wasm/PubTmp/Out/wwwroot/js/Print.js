window.printIframe = function (content) {
    const iframe = document.getElementById('print-frame');
    const doc = iframe.contentWindow.document;

    doc.open();
    doc.write('<html><head><title>Imprimir</title>');
    doc.write('<style>@media print { @page { margin: 0; } body { margin: 0; font-family: Arial, sans-serif; } }</style>');
    doc.write('</head><body>');
    doc.write(content);
    doc.write('</body></html>');
    doc.close();

    iframe.contentWindow.focus();
    iframe.contentWindow.print();
};

window.imprimirTicket = function (divId) {
    var div = document.getElementById(divId);
    if (!div) return;

    var contenido = div.innerHTML;
    var iframe = document.getElementById('print-frame');
    var doc = iframe.contentWindow.document;

    doc.open();
    doc.write('<html><head><title>Imprimir</title>');
    doc.write('<style>@media print { @page { margin: 0; } body { margin: 0; font-family: Arial, sans-serif; } }</style>');
    doc.write('</head><body>');
    doc.write(contenido);
    doc.write('</body></html>');
    doc.close();

    iframe.contentWindow.focus();
    iframe.contentWindow.print();
}