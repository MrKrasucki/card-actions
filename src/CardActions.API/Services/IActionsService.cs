using CardActions.API.Models.Services;

namespace CardActions.API.Services;

public interface IActionsService
{
    Task<string[]> GetActionsAsync(CardDetails cardDetails, CancellationToken cancellationToken = default);
}