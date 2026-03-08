using LawRobot.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LawRobot.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RevisionsController : ControllerBase
{
    private readonly LawRobotDbContext _db;

    public RevisionsController(LawRobotDbContext db)
    {
        _db = db;
    }

    [HttpGet("{guid}")]
    public async Task<IActionResult> GetByGuid(string guid)
    {
        var revision = await _db.Revisions
            .AsNoTracking()
            .Include(r => r.RevisionText)
            .Include(r => r.Summaries.Where(s => s.IsActiveSummary))
            .FirstOrDefaultAsync(r => r.RevisionGuid == guid);

        return revision is null ? NotFound() : Ok(revision);
    }

    [HttpGet("feed")]
    public async Task<IActionResult> Feed()
    {
        var feed = await _db.Revisions
            .AsNoTracking()
            .Select(r => new
            {
                revisionGuid = r.RevisionGuid,
                fullTextLink = r.FullTextLink,
                summaryText = r.Summaries
                    .Where(s => s.IsActiveSummary)
                    .OrderByDescending(s => s.CreatedAt)
                    .Select(s => s.SummaryText)
                    .FirstOrDefault()
            })
            .ToListAsync();

        return Ok(feed);
    }
}
