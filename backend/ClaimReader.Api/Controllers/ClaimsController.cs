using ClaimReader.Api.Data;
using ClaimReader.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClaimReader.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClaimsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Claim>>> GetAll([FromQuery] ClaimStatus? status)
    {
        var query = db.Claims.AsQueryable();
        if (status is not null)
            query = query.Where(c => c.Status == status);

        return await query.OrderByDescending(c => c.CreatedAt).ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Claim>> GetById(int id)
    {
        var claim = await db.Claims.FindAsync(id);
        return claim is null ? NotFound() : claim;
    }
}