using CardActions.API.Models.Common;

namespace CardActions.API.Models.Services;

public sealed record CardDetails(string CardNumber, CardType CardType, CardStatus CardStatus, bool IsPinSet);
