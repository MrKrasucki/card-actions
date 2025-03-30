namespace CardActions.API.Models.Persistence;

public class Action 
{
    public required int Id { get; set; }
    public required string Name { get; set; }

    public virtual ICollection<ActionPermission>? Permissions { get; set; }
}