using CardActions.API.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace CardActions.API.Services;

internal class ActionsService(ILogger<ActionsService> logger, ApplicationDbContext context) : IActionsService
{
    public async Task<string[]> GetActionsAsync(CardDetails cardDetails, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Actions
                .Where(a => a.Permissions!
                    .Any(p => p.CardType == cardDetails.CardType 
                        && p.CardStatus == cardDetails.CardStatus 
                        && (p.IsPinWanted == null || !p.IsPinWanted == cardDetails.IsPinSet)))
                .Select(a => a.Name)
                .ToArrayAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while getting actions for card {CardNumber}", cardDetails.CardNumber);
        }

        return [];
    }
}