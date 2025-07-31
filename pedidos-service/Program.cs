using Marten;
using Wolverine;
using Contratos;
using JasperFx.Events;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMarten(options =>
{
    options.Connection("Host=localhost;Port=5432;Database=pedidos;Username=postgres;Password=postgres");

    options.Events.AddEventType(typeof(PedidoCreated));
    options.Events.StreamIdentity = StreamIdentity.AsString;

    options.CreateDatabasesForTenants(c =>
    {
        // Specify a db to which to connect in case database needs to be created.
        // If not specified, defaults to 'postgres' on the connection for a tenant.
        c.MaintenanceDatabase("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres");

        c.ForTenant()
            .CheckAgainstPgDatabase()
            .WithOwner("postgres")
            .WithEncoding("UTF-8")
            .ConnectionLimit(-1);
    });
});


// Ativa o Wolverine para mensageria
builder.Host.UseWolverine();

var app = builder.Build();

// Endpoint de criação de pedido
app.MapPost("/pedidos", PedidoEndpoints.CreatePedido);

app.Run();
