using CardActions.API.Models.Common;
using CardActions.API.Models.Persistence;

namespace CardActions.API.Persistence;

internal static class Seed 
{
    internal static void SeedDatabase(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        Enumerable.Range(1, 13).ToList().ForEach(i =>
        {
            dbContext.Actions.Add(new Models.Persistence.Action { Id = i, Name = $"ACTION{i}" });
        });

        foreach (CardType cardType in Enum.GetValues(typeof(CardType)))
        {
            dbContext.ActionPermissions.AddRange([
                new ActionPermission { ActionId = 1, CardType = cardType, CardStatus = CardStatus.Active }, // ACTION1
                new ActionPermission { ActionId = 2, CardType = cardType, CardStatus = CardStatus.Inactive }, // ACTION2
            ]);

            foreach (CardStatus cardStatus in Enum.GetValues(typeof(CardStatus)))
            {
                dbContext.ActionPermissions.AddRange([
                    new ActionPermission { ActionId = 3, CardType = cardType, CardStatus = cardStatus }, // ACTION3
                    new ActionPermission { ActionId = 4, CardType = cardType, CardStatus = cardStatus }, // ACTION4
                ]);
            }

            // ACTION6
            dbContext.ActionPermissions.AddRange([
                new ActionPermission { ActionId = 6, CardType = cardType, CardStatus = CardStatus.Ordered, IsPinWanted = true },
                new ActionPermission { ActionId = 6, CardType = cardType, CardStatus = CardStatus.Inactive, IsPinWanted = true },
                new ActionPermission { ActionId = 6, CardType = cardType, CardStatus = CardStatus.Active, IsPinWanted = true },
                new ActionPermission { ActionId = 6, CardType = cardType, CardStatus = CardStatus.Blocked, IsPinWanted = true },
            ]);
        
            // ACTION7
            dbContext.ActionPermissions.AddRange([
                new ActionPermission { ActionId = 7, CardType = cardType, CardStatus = CardStatus.Ordered, IsPinWanted = false },
                new ActionPermission { ActionId = 7, CardType = cardType, CardStatus = CardStatus.Inactive, IsPinWanted = false },
                new ActionPermission { ActionId = 7, CardType = cardType, CardStatus = CardStatus.Active, IsPinWanted = false },
                new ActionPermission { ActionId = 7, CardType = cardType, CardStatus = CardStatus.Blocked, IsPinWanted = true },
            ]);

            // ACTION8
            dbContext.ActionPermissions.AddRange([
                new ActionPermission { ActionId = 8, CardType = cardType, CardStatus = CardStatus.Ordered },
                new ActionPermission { ActionId = 8, CardType = cardType, CardStatus = CardStatus.Inactive },
                new ActionPermission { ActionId = 8, CardType = cardType, CardStatus = CardStatus.Active },
                new ActionPermission { ActionId = 8, CardType = cardType, CardStatus = CardStatus.Blocked },
            ]);

            // ACTION9
            foreach (CardStatus cardStatus in Enum.GetValues(typeof(CardStatus)))
            {
                dbContext.ActionPermissions.AddRange([
                    new ActionPermission { ActionId = 9, CardType = cardType, CardStatus = cardStatus },
                ]);
            }

            dbContext.ActionPermissions.AddRange([
                // ACTION10
                new ActionPermission { ActionId = 10, CardType = cardType, CardStatus = CardStatus.Ordered },
                new ActionPermission { ActionId = 10, CardType = cardType, CardStatus = CardStatus.Inactive },
                new ActionPermission { ActionId = 10, CardType = cardType, CardStatus = CardStatus.Active },
                // ACTION11
                new ActionPermission { ActionId = 11, CardType = cardType, CardStatus = CardStatus.Inactive },
                new ActionPermission { ActionId = 11, CardType = cardType, CardStatus = CardStatus.Active },
                // ACTION12
                new ActionPermission { ActionId = 12, CardType = cardType, CardStatus = CardStatus.Ordered },
                new ActionPermission { ActionId = 12, CardType = cardType, CardStatus = CardStatus.Inactive },
                new ActionPermission { ActionId = 12, CardType = cardType, CardStatus = CardStatus.Active },
                // ACTION13
                new ActionPermission { ActionId = 13, CardType = cardType, CardStatus = CardStatus.Ordered },
                new ActionPermission { ActionId = 13, CardType = cardType, CardStatus = CardStatus.Inactive },
                new ActionPermission { ActionId = 13, CardType = cardType, CardStatus = CardStatus.Active },
            ]);
        }

        // ACTION5
        foreach (CardStatus cardStatus in Enum.GetValues(typeof(CardStatus)))
        {
            dbContext.ActionPermissions.AddRange([
                new ActionPermission { ActionId = 5, CardType = CardType.Credit, CardStatus = cardStatus },
            ]);
        }

        dbContext.SaveChanges();
    }
}