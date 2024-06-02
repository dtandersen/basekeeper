using Basekeeper.Entity;
using Basekeeper.Repository;

namespace Basekeeper.Command;

public record OrderItemCommand(string Item, int Quantity)
{
}

public class OrderItemCommandHandler : CommandHandler<OrderItemCommand>
{
    private OrderRepository requisitionRepository;

    public OrderItemCommandHandler(OrderRepository requisitionRepository)
    {
        this.requisitionRepository = requisitionRepository;
    }

    public void Handle(OrderItemCommand command)
    {
        requisitionRepository.Save(new List<LineItem> { new LineItem(Item: command.Item, Quantity: command.Quantity) });
    }
}
