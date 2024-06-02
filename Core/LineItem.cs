namespace Basekeeper.Entity;

public record LineItem(String Item, int Quantity)
{
    public LineItem() : this("", 0)
    {
    }

    public override string ToString() => $"LineItem {{ Item={Item}, Quantity={Quantity} }}";
};
