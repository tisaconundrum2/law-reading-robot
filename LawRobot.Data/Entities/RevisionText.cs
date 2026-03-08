using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawRobot.Data.Entities;

[Table("Revision_Text")]
public class RevisionText
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long RtUniqueId { get; set; }

    [Required]
    public string FullText { get; set; } = string.Empty;

    [Required]
    [MaxLength(64)]
    public string ContentHash { get; set; } = string.Empty;

    public DateTimeOffset ExtractedAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public Revision? Revision { get; set; }
}
