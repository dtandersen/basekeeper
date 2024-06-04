using Basekeeper.Entity;
using Basekeeper.Repository;
using TelemRec;

namespace Basekeeper.Command;

public record OrderItemCommand(string Item, int Quantity)
{
}

public class OrderItemCommandHandler : CommandHandler<OrderItemCommand>
{
    private OrderRepository orderRepository;
    private Logger logger;

    public OrderItemCommandHandler(OrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
        logger = LogFactory.GetLogger(GetType());
    }

    public void Handle(OrderItemCommand command)
    {
        orderRepository.Create(new Order(Product: command.Item, Quantity: command.Quantity, Ingredients: new List<LineItem>()));
    }
}
