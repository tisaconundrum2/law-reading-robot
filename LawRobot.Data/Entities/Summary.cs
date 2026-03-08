using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawRobot.Data.Entities;

[Table("Summaries")]
public class Summary
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long SummaryId { get; set; }

    [Required]
    public long RevisionInternalId { get; set; }

    [Required]
    public string SummaryText { get; set; } = string.Empty;

    public bool IsActiveSummary { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public Revision Revision { get; set; } = null!;
}
