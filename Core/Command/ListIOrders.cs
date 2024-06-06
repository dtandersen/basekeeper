using Basekeeper.Repository;

namespace Basekeeper.Command;

public class ListOrdersQuery { }


public class ListOrdersQueryHandler : QueryHandler<ListOrdersQuery, List<OrderDto>>
{
    private readonly OrderRepository orderRepository;

    public ListOrdersQueryHandler(OrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public List<OrderDto> Handle(ListOrdersQuery query)
    {
        var orders = orderRepository.All();
        return orders.Select(order => new OrderDto(order.Item, order.Quantity)).ToList();
    }
}

public record OrderDto(string Item, float Quantity);
