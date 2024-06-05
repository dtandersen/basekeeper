
using Basekeeper.Entity;

namespace Basekeeper.Repository;

public interface RecipeRepository
{
    List<Recipe> All();

    void Create(Recipe recipe);

    Recipe? FindByName(string product);
}
