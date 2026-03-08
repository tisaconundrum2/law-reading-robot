using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LawRobot.Core.Enums;

namespace LawRobot.Data.Entities;

[Table("Bills")]
public class Bill
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long BillInternalId { get; set; }

    [Required]
        /// <summary>
    /// Full PA legislature bill identifier, e.g. "20230HB1234".
    /// Format: year + session type digit + chamber + bill type + number.
    /// </summary>
[RegularExpression(@"^\d{4}[0-1][HS][BR]\d+$")]
    public string LegislativeId { get; set; } = string.Empty;

    [Required]
    public string LegislativeSession { get; set; } = string.Empty;

    [Required]
    public SessionType SessionType { get; set; }

    [Required]
    public LegislativeChamber Chamber { get; set; }

    [Required]
    public long BillNumber { get; set; }

    public long? PrimarySponsorId { get; set; }

    public string? StatusText { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public Representative? PrimarySponsor { get; set; }
    public ICollection<Revision> Revisions { get; set; } = new List<Revision>();
}
