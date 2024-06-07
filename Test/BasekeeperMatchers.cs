using Basekeeper.Command;
using Basekeeper.Entity;

namespace Basekeeper.Matcher;

public class BasekeeperMatchers
{
    public static IMatcher<Order> EqualToOrder(Order expected)
    {
        Func<Order, int> func = (order) => order.Quantity;
        FeatureMatcher<Order, string> matcher = new FeatureMatcher<Order, string>() { FeatureName = "Item", Func = (order) => order.Item, Matcher = Is.EqualTo(expected.Item) };
        FeatureMatcher<Order, int> matcher2 = new FeatureMatcher<Order, int>() { FeatureName = "Quantity", Func = func, Matcher = Is.EqualTo(expected.Quantity) };

        return Matches.AllOf(matcher, matcher2);
    }

    public static RecipeMatcher EqualToRecipe(Recipe expected)
    {
        return new RecipeMatcher(expected);
    }

    public static RecipeDtoMatcher EqualToRecipeDto(RecipeDto expected)
    {
        return new RecipeDtoMatcher(expected);
    }

    public static IMatcher<OrderDto> EqualToOrderDto(OrderDto expected)
    {
        FeatureMatcher<OrderDto, string> matcher = new FeatureMatcher<OrderDto, string>() { FeatureName = "Item", Func = (order) => order.Item, Matcher = Is.EqualTo(expected.Item) };
        FeatureMatcher<OrderDto, float> matcher2 = new FeatureMatcher<OrderDto, float>() { FeatureName = "Quantity", Func = (order) => order.Quantity, Matcher = Is.EqualTo(expected.Quantity) };
        List<IMatcher<LineItemDto>> listmatcher = expected.Components.Select(x => Is.EqualTo(x)).ToList();

        FeatureMatcher<OrderDto, List<LineItemDto>> matcher3 = new FeatureMatcher<OrderDto, List<LineItemDto>>() { FeatureName = "LineItems", Func = (order) => order.Components, Matcher = Has.Items(listmatcher.ToArray()) };

        return Matches.AllOf(matcher, matcher2, matcher3);
    }
}

public class Xyz
{
    public static FeatureMatcher<T, U> Of<T, U>(String featureName, Func<T, U> func, IMatcher<U> matcher)
    {
        return new FeatureMatcher<T, U> { FeatureName = featureName, Func = func, Matcher = matcher };
    }
}

public class Feature<T, U>
{
    public required String Name { get; set; }
    public required Func<T, U> Func { get; set; }
    public required IMatcher<U> Matcher { get; set; }
}

public class FeatureMatcher<T, U> : IMatcher<T>
{
    public required String FeatureName { get; set; }
    public required Func<T, U> Func { get; set; }
    public required IMatcher<U> Matcher { get; set; }

    public FeatureMatcher()
    {
    }

    public static FeatureMatcher<T, U> Of(String featureName, Func<T, U> func, IMatcher<U> matcher)
    {
        return new FeatureMatcher<T, U> { FeatureName = featureName, Func = func, Matcher = matcher };
    }

    public void DescribeTo(IDescription description)
    {
        description.AppendText($"{FeatureName}=");
        Matcher.DescribeTo(description);
    }

    public void DescribeMismatch(T item, IDescription mismatchDescription)
    {
        U featureValue = Func(item);
        // mismatchDescription.AppendValue(item);
        mismatchDescription.AppendText($"was {FeatureName} ");
        Matcher.DescribeMismatch(featureValue, mismatchDescription);
    }

    public bool Matches(T actual)
    {
        U featureValue = Func(actual);

        if (!Matcher.Matches(featureValue))
        {
            return false;
        }

        return true;
    }
}