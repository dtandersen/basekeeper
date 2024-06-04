namespace Basekeeper.Entity
{
    public record Recipe(string Product, List<LineItem> Ingredients)
    {
        public Recipe() : this("", new List<LineItem>())
        {
        }

        public override string ToString() => $"Recipe {{ Product={Product}, Ingredients={string.Join(", ", Ingredients)} }}";
    };
}
