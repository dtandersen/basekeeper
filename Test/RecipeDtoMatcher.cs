using Basekeeper.Command;
using Basekeeper.Entity;
using NHamcrest.Core;

namespace Basekeeper.Matcher;

public class RecipeDtoMatcher : Matcher<RecipeDto>
{
    private readonly RecipeDto expected;

    public RecipeDtoMatcher(RecipeDto expected)
    {
        this.expected = expected;
    }

    public override void DescribeTo(IDescription description)
    {
        description.AppendText(expected.ToString());
    }

    public override bool Matches(RecipeDto actual)
    {
        List<IMatcher<LineItemDto>> matchers = expected.Components.Select(x => Is.EqualTo(x)).ToList();

        return actual.Product == expected.Product
            && Has.Items(matchers.ToArray()).Matches(actual.Components);
    }
}
