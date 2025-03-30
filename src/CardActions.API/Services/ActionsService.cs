using CardActions.API.Models.Services;

namespace CardActions.API.Services;

internal class ActionsService : IActionsService
{
    public async Task<string[]> GetActionAsync(CardDetails cardDetails, CancellationToken cancellationToken)
    {
        return [];
    }
}