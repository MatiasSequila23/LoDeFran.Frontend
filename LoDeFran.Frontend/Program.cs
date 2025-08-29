using LoDeFran.Frontend;
using LoDeFran.Frontend.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;

// Cambiá esta IP y puerto por los de tu servidor IIS
//string servidorApi = "192.168.10.137";
//string puertoApi = "81";

//string puertoApi = "81";

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSyncfusionBlazor();


var environment = builder.HostEnvironment.Environment; // Esto devuelve "Development" o "Production"
// Detectar si estamos en localhost (ahora que builder ya existe)
bool esLocalhost = builder.HostEnvironment.BaseAddress.Contains("localhost");
string baseUrl;
if (esLocalhost)
{
    baseUrl = $"http://localhost:81/api/";
}
else
{
    if(environment == "Production")
    {
        baseUrl = $"http://192.168.10.190:81/api/";
    }
    else
    {
        baseUrl = $"http://192.168.10.190:88/api/";
    }
}


//if (environment == "Production")
//{
//    baseUrl = $"http://192.168.1.31:82/api/";
//}
//else
//{
//    baseUrl = esLocalhost
//     ? $"http://localhost:{puertoApi}/api/"
//     : $"http://{servidorApi}:{puertoApi}/api/";
//}
// Si está en localhost usa localhost, si no usa IP


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseUrl) });

// Registrar servicios
builder.Services.AddScoped<CajaService>();
builder.Services.AddScoped<CalleService>();
builder.Services.AddScoped<CategoriaProductoService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ComboService>();
builder.Services.AddScoped<DetallePedidoServicie>();
builder.Services.AddScoped<DiaService>();
builder.Services.AddScoped<EstadoInsumoService>();
builder.Services.AddScoped<FacturaService>();
builder.Services.AddScoped<InsumoService>();
builder.Services.AddScoped<MesaService>();
builder.Services.AddScoped<MetodoPagoService>();
builder.Services.AddScoped<MotivoMovimientoService>();
builder.Services.AddScoped<MovimientosCajaService>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<PromocionAplicacionService>();
builder.Services.AddScoped<PromocionService>();
builder.Services.AddScoped<ProveedorService>();
builder.Services.AddScoped<ServicioDeMesaService>();
builder.Services.AddScoped<SubcategoriaProductoService>();
builder.Services.AddScoped<TipoDescuentoService>();
builder.Services.AddScoped<UnidadMedidaService>();
builder.Services.AddScoped<UsuarioService>();

await builder.Build().RunAsync();
