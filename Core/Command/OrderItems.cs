using Basekeeper.Entity;
using Basekeeper.Repository;

namespace Basekeeper.Command;

public record OrderItemsCommand(List<LineItem> Items)
{
}

public class OrderItemsCommandHandler : CommandHandler<OrderItemsCommand>
{
    private OrderRepository requisitionRepository;

    public OrderItemsCommandHandler(OrderRepository requisitionRepository)
    {
        this.requisitionRepository = requisitionRepository;
    }

    public void Handle(OrderItemsCommand command)
    {
        requisitionRepository.Save(command.Items);
    }
}
