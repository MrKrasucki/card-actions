using Action = CardActions.API.Models.Persistence.Action;
using CardActions.API.Models.Persistence;
using Microsoft.EntityFrameworkCore;

internal sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Action
        modelBuilder.Entity<Action>().HasKey(e => e.Id);
        
        modelBuilder.Entity<Action>().Property(e => e.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Action>().Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        // ActionPermission
        modelBuilder.Entity<ActionPermission>()
            .HasKey(ap => new { ap.ActionId, ap.CardType, ap.CardStatus });

        modelBuilder.Entity<ActionPermission>().Property(e => e.CardType).HasConversion<string>();
        modelBuilder.Entity<ActionPermission>().Property(e => e.CardStatus).HasConversion<string>();

        modelBuilder.Entity<ActionPermission>().HasOne(ap => ap.Action)
            .WithMany(a => a.Permissions)
            .HasForeignKey(ap => ap.ActionId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<Action> Actions { get; set; } = null!;
    public DbSet<ActionPermission> ActionPermissions { get; set; } = null!;
}