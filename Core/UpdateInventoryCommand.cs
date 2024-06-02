

using Core;

public class UpdateInventoryCommand
{
    private InventoryRepository emailService;

    public UpdateInventoryCommand(InventoryRepository emailService)
    {
        this.emailService = emailService;
    }

    public async void Execute(List<LineItem> lineItems)
    {
        emailService.Save(lineItems);
    }
}