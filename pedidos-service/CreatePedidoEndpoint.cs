using Marten;
using Wolverine;
using Contratos;

public static class PedidoEndpoints
{
    public static async Task<IResult> CreatePedido(
        Pedido pedido,
        IDocumentSession session,
        IMessageBus bus
    )
    {
        session.Store(pedido);
        await session.SaveChangesAsync();

        var pedidoCreated = new PedidoCreated(pedido.Id, pedido.Produto, pedido.Valor);
        await bus.PublishAsync(pedidoCreated);

        return Results.Created($"/pedidos/{pedido.Id}", pedido);
    }
}