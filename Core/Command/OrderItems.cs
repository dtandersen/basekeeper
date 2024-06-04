using Basekeeper.Entity;
using Basekeeper.Repository;

namespace Basekeeper.Command;

public record OrderItemsCommand(List<Order> Items)
{
}

public class OrderItemsCommandHandler : CommandHandler<OrderItemsCommand>
{
    private OrderRepository orderRepository;

    public OrderItemsCommandHandler(OrderRepository requisitionRepository)
    {
        this.orderRepository = requisitionRepository;
    }

    public void Handle(OrderItemsCommand command)
    {
        orderRepository.ReplaceAll(command.Items);
    }
}
