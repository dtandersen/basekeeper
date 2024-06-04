using Basekeeper.Entity;
using Basekeeper.Repository;
using TelemRec;

namespace Basekeeper.Command;

public record CreateOrderCommand(string Item, int Quantity)
{
}

public class CreateOrderCommandHandler : CommandHandler<CreateOrderCommand>
{
    private OrderRepository orderRepository;
    private Logger logger;

    public CreateOrderCommandHandler(OrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
        logger = LogFactory.GetLogger(GetType());
    }

    public void Handle(CreateOrderCommand command)
    {
        orderRepository.Create(new Order(Item: command.Item, Quantity: command.Quantity, Components: new List<LineItem>()));
    }
}
