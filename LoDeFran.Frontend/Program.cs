using LoDeFran.Frontend;
using LoDeFran.Frontend.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:82/api/") });
builder.Services.AddScoped<CategoriaProductoService>();
builder.Services.AddScoped<DetallePedidoServicie>();
builder.Services.AddScoped<EstadoInsumoService>();
builder.Services.AddScoped<InsumoService>();
builder.Services.AddScoped<MesaService>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<ProveedorService>();
builder.Services.AddScoped<ServicioDeMesaService>();
builder.Services.AddScoped<UnidadMedidaService>();
await builder.Build().RunAsync();
