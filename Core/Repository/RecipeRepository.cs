
using Basekeeper.Entity;

namespace Basekeeper.Repository;

public interface RecipeRepository
{
    void Create(Recipe recipe);

    Recipe? FindByName(string product);
}
