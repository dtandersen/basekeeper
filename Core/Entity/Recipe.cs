namespace Basekeeper.Entity
{
    public record Recipe(String Product, List<LineItem> Ingredients)
    {
        public Recipe() : this("", new List<LineItem>())
        {
        }

        public override string ToString() => $"Recipe {{ Name={Product}, Ingredients={Ingredients} }}";
    };
}
