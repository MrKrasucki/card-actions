using CardActions.API.Services;
using Microsoft.AspNetCore.Mvc;

internal static class ActionsEndpoints
{
    private static readonly string Get = "api/actions";

    public static WebApplication MapActionEndpoints(this WebApplication app)
    {
        app.MapGet(Get, async (
            [AsParameters] GetActionsRequest request, 
            [FromServices] ICardService cardService, 
            [FromServices] IActionsService actionsService) =>
        {
            var cardDetails = await cardService.GetCardDetailsAsync(request.UserId, request.CardNumber);
            
            if (cardDetails is null)
            {
                return Results.BadRequest("Card not found for specified parameters.");
            }

            var allowedActions = await actionsService.GetActionAsync(cardDetails);
            return Results.Ok(new GetActionsResponse(allowedActions));
        })
        .WithName("GetActions")
        .WithDescription("Get the actions that can be performed on a card.")
        .WithOpenApi()
        .Produces<GetActionsResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);

        return app;
    }
}