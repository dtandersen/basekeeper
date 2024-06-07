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
        // var items=
        return orders.Select(order => new OrderDto(order.Item, order.Quantity, order.Components.Select(

            item => new LineItemDto(Item: item.Item, Quantity: item.Quantity)).ToList())).ToList();
    }
}

public record OrderDto(string Item, float Quantity, List<LineItemDto> Components)
{
    public override string ToString()
    {
        return $"OrderDto {{ Item={Item}, Quantity={Quantity}, Components={string.Join(",", Components)} }}";
    }
}