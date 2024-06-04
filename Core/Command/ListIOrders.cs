using Basekeeper.Entity;
using Basekeeper.Repository;

namespace Basekeeper.Command;

public class ListOrdersQuery { }


public class ListOrdersQueryHandler : QueryHandler<ListOrdersQuery, List<OrderItemDto>>
{
    private readonly OrderRepository orderRepository;

    public ListOrdersQueryHandler(OrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public List<OrderItemDto> Handle(ListOrdersQuery query)
    {
        var orders = orderRepository.All();
        return orders.Select(order => new OrderItemDto(order.Product, order.Quantity)).ToList();
    }
}

public record OrderItemDto(string Item, int Quantity);
