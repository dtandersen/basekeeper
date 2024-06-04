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
        var items = orderRepository.All();
        logger.Info($"Adding {command.Item} x{command.Quantity}");
        logger.Debug($"Existing {string.Join(", ", items)}");
        var newitems = items.Append(new LineItem(Item: command.Item, Quantity: command.Quantity));
        logger.Debug($"New {string.Join(", ", newitems)}");
        orderRepository.Save(newitems);
    }
}
