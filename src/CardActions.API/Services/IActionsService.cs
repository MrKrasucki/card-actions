using CardActions.API.Models.Services;

namespace CardActions.API.Services;

public interface IActionsService
{
    Task<string[]> GetActionAsync(CardDetails cardDetails, CancellationToken cancellationToken = default);
}