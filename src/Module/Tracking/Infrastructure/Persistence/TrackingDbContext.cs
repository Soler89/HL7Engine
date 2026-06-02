using Microsoft.EntityFrameworkCore;
 


namespace HL7Engine.Module.Tracking.Infrastructure.Persistence;

public sealed class TrackingDbContext(DbContextOptions<TrackingDbContext> options) : DbContext(options)
{
    public DbSet<MessageTracking> MessageTrackings => Set<MessageTracking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MessageTracking>(entity =>
        {
            entity.ToTable("MessageTrackings");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasConversion<string>().HasMaxLength(32);
            entity.Property(e => e.ParsedJson).HasColumnType("TEXT");
            entity.Property(e => e.ErrorsJson).HasColumnType("TEXT");
            entity.Property(e => e.AckMessage).HasColumnType("TEXT");
        });
    }
}
