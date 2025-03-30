using CardActions.API.Models.Services;

namespace CardActions.API.Services;

internal interface ICardService
{
    Task<CardDetails?> GetCardDetailsAsync(string userId, string cardNumber);
}