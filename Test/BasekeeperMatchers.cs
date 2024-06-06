using Basekeeper.Command;
using Basekeeper.Entity;
using NHamcrest.Core;

namespace Basekeeper.Matcher;

public class BasekeeperMatchers
{
    public static IMatcher<Order> EqualToOrder(Order expected)
    {
        Func<Order, int> func = (order) => order.Quantity;
        FeatureMatcher<Order, string> matcher = new FeatureMatcher<Order, string>() { FeatureName = "Item", Func = (order) => order.Item, Matcher = Is.EqualTo(expected.Item) };
        FeatureMatcher<Order, int> matcher2 = new FeatureMatcher<Order, int>() { FeatureName = "Quantity", Func = func, Matcher = Is.EqualTo(expected.Quantity) };

        // FeatureMatcher<Order, int> matcher3 = Xyz.Of("Quantity", (Order order) => order.Quantity, Is.EqualTo(expected.Quantity));

        return Matches.AllOf(matcher, matcher2);
        // Feature<string> f1 = new Feature<string> { Name = "Item", Value = expected.Item, Matcher = Is.EqualTo(expected.Item) };
        // matcher.Add(f1);
        // Matches.AllOf();

        // List<object> features = new List<object>();
        // Func<Order, string> itemfunc = (expected) => expected.Item;
        // features.Add(new Feature<Order, object> { Name = "Item", Func = itemfunc, Matcher = (IMatcher<object>)Is.EqualTo(expected.Item) });

        // matcher.Add(new Feature<Order, object> { Name = "Item", Func = itemfunc, Matcher = (IMatcher<object>)Is.EqualTo(expected.Item) });

        // // matcher.Matches(expected);
        // // matchers.Add(Is.EqualTo(1));

        // // features[0]



        // List<object> matchers = new List<object>();
        // matchers.Add(Is.EqualTo("a"));
        // matchers.Add(Is.EqualTo(1));

        // Assert.That("a", (IMatcher<string>)matchers[0]);
        // Assert.That(1, (IMatcher<int>)matchers[1]);

        // return matcher;
        // return new OrderMatcher(expected);
    }

    public static RecipeMatcher EqualToRecipe(Recipe expected)
    {
        return new RecipeMatcher(expected);
    }

    public static RecipeDtoMatcher EqualToRecipeDto(RecipeDto expected)
    {
        return new RecipeDtoMatcher(expected);
    }

    public static OrderDtoMatcher EqualToOrderDto(OrderDto expected)
    {
        return new OrderDtoMatcher(expected);
    }
}

// public class Feature
// {
//     public String Name;
//     public IMatcher<object> Matcher;
//     public object Value;
// }

// public interface Feature
// {
//     public String Name { get; set; }
//     // public IMatcher<T> Matcher { get; set; }
//     // public T Value { get; set; }
// }

public class Xyz
{
    public static FeatureMatcher<T, U> Of<T, U>(String featureName, Func<T, U> func, IMatcher<U> matcher)
    {
        return new FeatureMatcher<T, U> { FeatureName = featureName, Func = func, Matcher = matcher };
    }
}

public class Feature<T, U>
// {//}: Feature
{
    public required String Name { get; set; }
    public required Func<T, U> Func { get; set; }
    public required IMatcher<U> Matcher { get; set; }
    // public IMatcher<T> Matcher { get; set; }
    // public T Value { get; set; }
}

public class FeatureMatcher<T, U> : IMatcher<T>//, ISelfDescribing//,//Matcher<T>, IMatcher<T>, ISelfDescribing
{
    // private T? actual2;
    // private IDescription mismatchDescription;

    // private readonly T expected;
    // private readonly Matcher<U> matcher;

    public required String FeatureName { get; set; }
    public required Func<T, U> Func { get; set; }
    public required IMatcher<U> Matcher { get; set; }

    public FeatureMatcher()
    {
        // features = new List<object>();
        // this.expected = expected;
        // mismatchDescription = new StringDescription();
    }

    public static FeatureMatcher<T, U> Of(String featureName, Func<T, U> func, IMatcher<U> matcher)
    {
        return new FeatureMatcher<T, U> { FeatureName = featureName, Func = func, Matcher = matcher };
    }

    public void DescribeTo(IDescription description)
    {
        // description.AppendText($"zzz {Name}={Func.Invoke(actual2)}");

        description.AppendText($"{FeatureName}=");
        Matcher.DescribeTo(description);
        // description.AppendText($"DescribeTo");
    }

    public void DescribeMismatch(T item, IDescription mismatchDescription)
    {
        // mismatchDescription.AppendText($"yyy was {Name}={Func.Invoke(item)}");
        // mismatchDescription.
        U featureValue = Func(item);
        mismatchDescription.AppendText($"{FeatureName} ");
        Matcher.DescribeMismatch(featureValue, mismatchDescription);
        // mismatchDescription.AppendText($"DescribeMismatch");
    }

    public bool Matches(T actual)
    {
        // actual2 = actual;
        U featureValue = Func(actual);

        if (!Matcher.Matches(featureValue))
        {
            // mismatchDescription.AppendText($"yyy was {Name}={featureValue}");
            // Matcher.DescribeMismatch(featureValue, mismatchDescription);
            return false;
        }

        return true;
        // NHamcrest.Matches.And
        // foreach (Feature<T> feature in features)
        // {
        //     // Assert.That(null, Has.AllOf(feature.Matcher).Matches(feature.Value));
        //     Is.EqualTo(feature.Value).DescribeTo(new StringDescription());
        //     if (!feature.Matcher.Matches(feature.Value))
        //     {
        //         return false;
        //     }
        // }

        // return true;
    }

    // internal void Add(object feature)
    // {
    //     features.Add(feature);
    // }
}