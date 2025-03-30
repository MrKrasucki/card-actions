using CardActions.API.Models.Common;

namespace CardActions.API.Models.Persistence;

public class ActionPermission
{
    public int ActionId { get; set; }
    public virtual Action? Action { get; set; }
    
    public CardType CardType { get; set; }
    public CardStatus CardStatus { get; set; }
    public bool? IsPinWanted { get; set; }
}