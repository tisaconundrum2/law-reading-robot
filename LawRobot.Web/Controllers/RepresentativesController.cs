using LawRobot.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LawRobot.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RepresentativesController : ControllerBase
{
    private readonly LawRobotDbContext _db;

    public RepresentativesController(LawRobotDbContext db)
    {
        _db = db;
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var rep = await _db.Representatives
            .AsNoTracking()
            .Include(r => r.SponsoredBills)
            .FirstOrDefaultAsync(r => r.Id == id);

        return rep is null ? NotFound() : Ok(rep);
    }
}
