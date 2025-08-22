window.mostrarConfirmacion = async function (titulo, mensaje) {
    const result = await Swal.fire({
        title: titulo,
        text: mensaje,
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, confirmar',
        cancelButtonText: 'Cancelar'
    });

    return result.isConfirmed;
};
window.mostrarAlerta = async function (titulo, mensaje, tipo) {
    await Swal.fire({
        title: titulo,
        text: mensaje,
        icon: tipo, // "success", "error", "warning", "info", "question"
        confirmButtonText: 'Aceptar'
    });
};
