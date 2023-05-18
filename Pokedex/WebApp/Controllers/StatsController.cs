using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApp.DataAccess.EfModels;
using WebApp.DataAccess.Interfaces;

namespace WebApp.Controllers;

public class StatForUrl
{
    public string Url { get; set; }
    public int Hit { get; set; }
}

[ApiController]
[Route("[controller]")]
public class StatsController : Controller
{
    private readonly PokedexContext _context;
    private readonly IMapper _mapper;
    private readonly IPokemonsRepository _pokemonsRepo;

    public StatsController(PokedexContext context, IMapper mapper, IPokemonsRepository pokemonsRepository)
    {
        _context = context;
        _mapper = mapper;
        _pokemonsRepo = pokemonsRepository;
    }
    
    [HttpGet("{sessionId}")]
    public async Task<IActionResult> GetBySession(string sessionId)
    { 
        return Ok(_context.TShortcuts
            .Where(s => s.SessionId == sessionId)
            .Select(s => new StatForUrl{ Url = s.Url, Hit = s.TStats.Count}));
    }
}