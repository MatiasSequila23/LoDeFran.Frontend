using LoDeFran.Frontend.Models;
using LoDeFran.Frontend.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using LoDeFran.Frontend.Utlis;

public class PedidoBase : ComponentBase
{
    [Inject] protected ServicioDeMesaService ServicioDeMesaService { get; set; } = default!;
    [Inject] protected ClienteService ClienteService { get; set; } = default!;
    [Inject] protected NavigationManager Nav { get; set; } = default!;
    [Inject] protected IJSRuntime JS { get; set; } = default!;

    protected List<TipoPedidoViewModel> TiposPedido = new();
    protected List<PedidoViewModel> Pedidos = new();
    protected int? TipoPedidoSeleccionado;

    protected string BusquedaCliente = "";
    protected List<ClienteViewModel> ClientesEncontrados = new();
    protected int? ClienteSeleccionadoId;
    protected ClienteViewModel NuevoCliente = new();
    protected ClienteViewModel? ClienteSeleccionado;

    protected async Task BuscarClienteAsync()
    {
        ClientesEncontrados = await ServicioDeMesaService.BuscarClientesAsync(BusquedaCliente.Trim());
    }

    protected async Task CargarPedidos()
    {
        Pedidos = await ServicioDeMesaService.PedidosFueraDeSalon();
        StateHasChanged();
    }

    protected RenderFragment MostrarTablaPedidos() => builder =>
    {
        if (Pedidos is null || !Pedidos.Any())
        {
            builder.AddMarkupContent(0, "<div class='alert alert-info'>No hay pedidos disponibles.</div>");
            return;
        }

        builder.AddMarkupContent(1, $@"
        <table class='table table-bordered'>
            <thead class='table-dark'>
                <tr>
                    <th>#</th><th>Cliente</th><th>Tipo</th><th>Fecha</th><th>Estado</th><th>Acciones</th>
                </tr>
            </thead>
            <tbody>");

        foreach (var pedido in Pedidos)
        {
            builder.AddMarkupContent(2, $@"
                <tr>
                    <td>{pedido.Id}</td>
                    <td>{pedido.ClienteNombre}</td>
                    <td>{pedido.TipoPedidoNombre}</td>
                    <td>{pedido.FechaPedido?.ToString("dd/MM/yyyy HH:mm")}</td>
                    <td>{((Enums.EstadoPedido)pedido.EstadoId)}</td>
                    <td>
                        <button class='btn btn-sm btn-outline-info me-1' onclick=""location.href='/Pedido/PedidoBuilder/{pedido.Id}'"">
                            <i class='fas fa-eye'></i>
                        </button>
                    </td>
                </tr>");
        }

        builder.AddMarkupContent(3, "</tbody></table>");
    };

    protected RenderFragment MostrarFormularioCliente() => builder =>
    {
        if (ClientesEncontrados.Any())
        {
            builder.AddMarkupContent(0, $@"
                <label>Seleccionar Cliente</label>
                <select class='form-control' @bind='ClienteSeleccionadoId'>
                    <option value=''>-- Seleccionar --</option>");

            foreach (var cliente in ClientesEncontrados)
            {
                builder.AddMarkupContent(1, $@"<option value='{cliente.Id}'>{cliente.Nombre} {cliente.Apellido} - {cliente.Telefono}</option>");
            }

            builder.AddMarkupContent(2, "</select>");
        }
        else
        {
            builder.AddMarkupContent(3, $@"
                <div class='mt-3'>
                    <h5>Nuevo Cliente</h5>
                    <input class='form-control mb-2' placeholder='Nombre' @bind='NuevoCliente.Nombre' />
                    <input class='form-control mb-2' placeholder='Apellido' @bind='NuevoCliente.Apellido' />
                    <input class='form-control mb-2' placeholder='Teléfono' @bind='NuevoCliente.Telefono' />
                    <input class='form-control mb-2' placeholder='Email' @bind='NuevoCliente.Email' />
                    <input class='form-control mb-2' placeholder='Calle' @bind='NuevoCliente.Calle' />
                    <input class='form-control mb-2' placeholder='Altura' @bind='NuevoCliente.Altura' />
                    <input class='form-control mb-2' placeholder='Piso' @bind='NuevoCliente.Piso' />
                </div>");
        }
    };
}
