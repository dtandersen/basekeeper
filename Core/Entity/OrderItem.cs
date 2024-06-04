namespace Basekeeper.Entity;

public record Order(string Item, int Quantity, List<LineItem> Components)
{
    public Order() : this("", 0, new List<LineItem>())
    {
    }

    public override string ToString()
    {
        return $"Order {{ Item={Item}, Quantity={Quantity}, Components=[{string.Join(", ", Components)}] }}";
    }
}
