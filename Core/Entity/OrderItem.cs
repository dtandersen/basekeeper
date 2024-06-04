namespace Basekeeper.Entity;

public record Order(string Product, int Quantity, List<LineItem> Ingredients)
{
    public Order() : this("", 0, new List<LineItem>())
    {
    }

    //tostring
    public override string ToString()
    {
        return $"Order {{ Product={Product}, Quantity={Quantity}, Ingredients=[{string.Join(", ", Ingredients)}] }}";
    }
}
