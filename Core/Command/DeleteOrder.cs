using Basekeeper.Repository;

namespace Basekeeper.Command;

public class DeleteOrderCommandHandler : CommandHandler<DeleteOrderCommand>
{
    private OrderRepository orderRepository;

    public DeleteOrderCommandHandler(OrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public void Handle(DeleteOrderCommand command)
    {
        orderRepository.Delete(command.Item);
    }
}

public record DeleteOrderCommand(String Item)
{
}
