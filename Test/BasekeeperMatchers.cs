using Basekeeper.Command;
using Basekeeper.Entity;

namespace Basekeeper.Matcher;

public class BasekeeperMatchers
{
    public static OrderMatcher EqualToOrder(Order expected)
    {
        return new OrderMatcher(expected);
    }

    public static RecipeMatcher EqualToRecipe(Recipe expected)
    {
        return new RecipeMatcher(expected);
    }

    public static RecipeDtoMatcher EqualToRecipeDto(RecipeDto expected)
    {
        return new RecipeDtoMatcher(expected);
    }
}
