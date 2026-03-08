using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LawRobot.Core.Enums;

namespace LawRobot.Data.Entities;

[Table("Representatives")]
public class Representative
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Party { get; set; }
    public long? District { get; set; }
    public string? ProfileLink { get; set; }
    public LegislativeChamber? Chamber { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public ICollection<Bill> SponsoredBills { get; set; } = new List<Bill>();
}
