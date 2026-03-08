using LawRobot.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LawRobot.Data;

public class LawRobotDbContext : DbContext
{
    public LawRobotDbContext(DbContextOptions<LawRobotDbContext> options) : base(options)
    {
    }

    public DbSet<Bill> Bills { get; set; }
    public DbSet<Revision> Revisions { get; set; }
    public DbSet<RevisionText> RevisionTexts { get; set; }
    public DbSet<Summary> Summaries { get; set; }
    public DbSet<Representative> Representatives { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>(b =>
        {
            b.HasIndex(x => x.LegislativeId).IsUnique();
            b.Property(x => x.Chamber).HasConversion<string>();
            b.Property(x => x.SessionType).HasConversion<int>();
        });

        modelBuilder.Entity<Revision>(r =>
        {
            r.HasIndex(x => x.RevisionGuid).IsUnique();
            r.HasOne(x => x.Bill)
                .WithMany(b => b.Revisions)
                .HasForeignKey(x => x.BillInternalId);
            r.HasOne(x => x.RevisionText)
                .WithOne(rt => rt.Revision)
                .HasForeignKey<Revision>(x => x.RtUniqueId);
        });

        modelBuilder.Entity<Summary>(s =>
        {
            s.HasOne(x => x.Revision)
                .WithMany(r => r.Summaries)
                .HasForeignKey(x => x.RevisionInternalId);

            s.HasIndex(x => new { x.RevisionInternalId, x.IsActiveSummary })
                .HasFilter("\"IsActiveSummary\" = true")
                .IsUnique();
        });

        modelBuilder.Entity<Representative>(rep =>
        {
            rep.HasMany(x => x.SponsoredBills)
                .WithOne(b => b.PrimarySponsor)
                .HasForeignKey(b => b.PrimarySponsorId);
            rep.Property(x => x.Chamber).HasConversion<string>();
        });
    }
}
