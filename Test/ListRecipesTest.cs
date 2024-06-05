using Basekeeper.Command;
using Basekeeper.Entity;
using Basekeeper.Matcher;
using Basekeeper.Repository;
using Basekeeper.Repository.Yaml;
using TelemRec;
using Xunit.Abstractions;

namespace Basekeeper.Tests;

//https://stackoverflow.com/questions/1408175/execute-unit-tests-serially-rather-than-in-parallel
[Collection("Sequential")]
public class ListRecipesTest
{
    private InventoryRepository inventoryRepository;
    private OrderRepository orderRepository;
    private YamlRecipeRepository recipeRepository;
    private List<RecipeDto> result;

    public ListRecipesTest(ITestOutputHelper output)
    {
        XunitLogger.Init(output);
        inventoryRepository = new YamlInventoryRepository();
        orderRepository = new YamlOrderRepository();
        recipeRepository = new YamlRecipeRepository();

        inventoryRepository.Reset();
        orderRepository.Reset();
        recipeRepository.Reset();
    }

    [Fact]
    public void Test1()
    {
        recipeRepository.Create(new Recipe(Product: "Furnace", Ingredients: new List<LineItem>() {
            new LineItem(Item: "Iron", Quantity: 20),
            new LineItem(Item: "Copper", Quantity: 10)
        }));

        recipeRepository.Create(new Recipe(Product: "Steel", Ingredients: new List<LineItem>() {
            new LineItem(Item: "Iron", Quantity: .75f),
            new LineItem(Item: "Carbon", Quantity: .25f)
        }));

        WhenListRecipes(new ListRecipesQuery());

        Assert.That(result, Is.OfLength(2));
        Assert.That(result, Has.Item(BasekeeperMatchers.EqualToRecipeDto(new RecipeDto(Product: "Furnace", Components: new List<LineItemDto>() {
            new LineItemDto(Item: "Iron", Quantity: 20),
            new LineItemDto(Item: "Copper", Quantity: 10)
        }))));

        Assert.That(result, Has.Item(BasekeeperMatchers.EqualToRecipeDto(new RecipeDto(Product: "Steel", Components: new List<LineItemDto>() {
            new LineItemDto(Item: "Iron", Quantity: .75f),
            new LineItemDto(Item: "Carbon", Quantity: .25f)
        }))));
    }

    private void WhenListRecipes(ListRecipesQuery listRecipesQuery)
    {
        var handler = new ListRecipesQueryHandler(recipeRepository);
        result = handler.Handle(listRecipesQuery);
    }
}
