using Basekeeper.Entity;
using Basekeeper.Repository;

namespace Basekeeper.Command;

public class ListRecipesQuery { }


public class ListRecipesQueryHandler : QueryHandler<ListRecipesQuery, List<RecipeDto>>
{
    // private readonly OrderRepository orderRepository;
    private readonly RecipeRepository recipeRepository;

    public ListRecipesQueryHandler(RecipeRepository recipeRepository)
    {
        this.recipeRepository = recipeRepository;
        // this.orderRepository = orderRepository;

    }

    public List<RecipeDto> Handle(ListRecipesQuery query)
    {
        var recipes = recipeRepository.All();
        var dtos = recipes.Select(recipe => new RecipeDto(
                Product: recipe.Product,
                Components: recipe.Ingredients.Select(
                    item => new LineItemDto(Item: item.Item, Quantity: item.Quantity)).ToList()))
            .ToList();
        return dtos;
    }
}

public record RecipeDto(string Product, List<LineItemDto> Components)
{
    public override string ToString()
    {
        return $"RecipeDto {{ }}";
    }
}

public record LineItemDto(String Item, float Quantity)
{
    public override string ToString()
    {
        return $"LineItemDto {{ Item={Item}, Quantity={Quantity} }}";
    }
}
