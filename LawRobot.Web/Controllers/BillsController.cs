using LawRobot.Core.Enums;
using LawRobot.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LawRobot.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillsController : ControllerBase
{
    private readonly LawRobotDbContext _db;

    public BillsController(LawRobotDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] LegislativeChamber? chamber,
        [FromQuery] string? session,
        [FromQuery] string? q,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 25)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var query = _db.Bills.AsNoTracking().AsQueryable();

        if (chamber.HasValue)
        {
            query = query.Where(x => x.Chamber == chamber.Value);
        }

        if (!string.IsNullOrWhiteSpace(session))
        {
            query = query.Where(x => x.LegislativeSession == session);
        }

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim();
            query = query.Where(x => x.LegislativeId.Contains(term) || (x.StatusText != null && x.StatusText.Contains(term)));
        }

        var total = await query.CountAsync();
        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new { total, page, pageSize, items });
    }

    [HttpGet("{legislativeId}")]
    public async Task<IActionResult> GetByLegislativeId(string legislativeId)
    {
        var bill = await _db.Bills
            .AsNoTracking()
            .Include(x => x.Revisions)
            .ThenInclude(r => r.Summaries.Where(s => s.IsActiveSummary))
            .FirstOrDefaultAsync(x => x.LegislativeId == legislativeId);

        return bill is null ? NotFound() : Ok(bill);
    }
}
