using Wolverine;
using Contratos;

public class ProcessPedidoHandler
{
    public static Task Handle(PedidoCreated mensagem)
    {
        Console.WriteLine($"[Pagamentos] Processando pagamento do pedido {mensagem.Id} - Produto: {mensagem.Produto} - Valor: {mensagem.Valor:C}");
        return Task.CompletedTask;
    }
}