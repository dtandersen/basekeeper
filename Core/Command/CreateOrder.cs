using Basekeeper.Entity;
using Basekeeper.Repository;
using TelemRec;

namespace Basekeeper.Command;

public record CreateOrderCommand(string Item, int Quantity)
{
}

public class CreateOrderCommandHandler : CommandHandler<CreateOrderCommand>
{
    private readonly OrderRepository orderRepository;
    private readonly RecipeRepository recipeRepository;
    private Logger logger;

    public CreateOrderCommandHandler(OrderRepository orderRepository, RecipeRepository recipeRepository)
    {
        this.orderRepository = orderRepository;
        this.recipeRepository = recipeRepository;
        logger = LogFactory.GetLogger(GetType());
    }

    public void Handle(CreateOrderCommand command)
    {
        var recipe = recipeRepository.FindByName(command.Item);
        logger.Info($"Found recipe {recipe}");

        if (recipe == null)
        {
            logger.Warn($"Recipe not found for {command.Item}");
            orderRepository.Create(new Order(Item: command.Item, Quantity: command.Quantity, Components: new List<LineItem>() { }));
            return;
        }

        var ingredients = recipe.Ingredients.Select(ingredient => new LineItem(Item: ingredient.Item, Quantity: ingredient.Quantity * command.Quantity)).ToList();

        orderRepository.Create(new Order(Item: command.Item, Quantity: command.Quantity, Components: ingredients));
    }
}
