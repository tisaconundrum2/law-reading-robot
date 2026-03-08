using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawRobot.Data.Entities;

[Table("Revisions")]
public class Revision
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long RevisionInternalId { get; set; }

    [Required]
        /// <summary>
    /// Full PA legislature revision identifier, e.g. "20230HB1234P1".
    /// Format: bill identifier + printer/revision suffix (P + number).
    /// </summary>
[RegularExpression(@"^\d{4}[0-1][HS][BR]\d+P\d+$")]
    public string RevisionGuid { get; set; } = string.Empty;

    public long? PrinterNo { get; set; }

    public string? FullTextLink { get; set; }

    public DateOnly? PublicationDate { get; set; }

    public string? Description { get; set; }

    public long BillInternalId { get; set; }

    public long? RtUniqueId { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public Bill Bill { get; set; } = null!;
    public RevisionText? RevisionText { get; set; }
    public ICollection<Summary> Summaries { get; set; } = new List<Summary>();
}
