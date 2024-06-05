using Basekeeper.Entity;
using TelemRec;

namespace Basekeeper.Repository.Yaml;

public class YamlRecipeRepository : RecipeRepository
{
    private const string RECIPE_YAML = "recipe.yaml";
    private readonly Logger logger;

    public YamlRecipeRepository()
    {
        logger = LogFactory.GetLogger(GetType());
    }

    public List<Recipe> All()
    {
        var recipes = YamlHelper.Read<List<Recipe>>(RECIPE_YAML, new List<Recipe>());

        return recipes;
    }

    public void Create(Recipe recipe)
    {
        var recipes = All();
        var newrecipes = recipes.Append(recipe);
        YamlHelper.Write(RECIPE_YAML, newrecipes);
    }

    public Recipe? FindByName(string product)
    {
        logger.Info($"Finding recipe for {product}");
        var recipes = YamlHelper.Read<List<Recipe>>(RECIPE_YAML, new List<Recipe>());
        logger.Info($"Found recipes: {string.Join(", ", recipes)}");
        var recipe = recipes.FirstOrDefault(r => r.Product == product);
        logger.Info($"Found recipe: {recipe}");
        return recipe;
    }

    public void Reset()
    {
        File.Delete(RECIPE_YAML);
    }
}
