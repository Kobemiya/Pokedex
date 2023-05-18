using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using WebApp.DataAccess.Dbo;
using WebApp.DataAccess.Interfaces;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IPokemonsRepository _pokemonsRepo;
    private readonly IStatsRepository _statsRepo;

    public Task<IEnumerable<Pokemon>> RegisteredPokemons;

    public IndexModel(ILogger<IndexModel> logger, IPokemonsRepository pokemonsRepo, IStatsRepository statsRepository)
    {
        _logger = logger;
        _pokemonsRepo = pokemonsRepo;
        _statsRepo = statsRepository;
    }

    private string GenerateRandom() =>
        new(".....".Select(c => "abcedfghijklmnopqrstuvwxyz0123456789"[new Random().Next(36)]).ToArray());
    
    public async Task<IActionResult> OnGet(string? hash)
    {
        if (hash.IsNullOrEmpty())
        {
            RegisteredPokemons = _pokemonsRepo.Get();
            HttpContext.Session.SetString("id", GenerateRandom());
            return Page();
        }
        
        var found = _pokemonsRepo.GetByHash(hash!);
        if (found == null)
            return NotFound();
        await _statsRepo.Insert(new Stat
        {
            Date = DateTime.Now,
            IdUrl = found.Id
        });
        return Redirect(found.Url);
    }

    public async Task OnPost()
    {
        var hash = GenerateRandom();
        while (_pokemonsRepo.GetByHash(hash) != null)
            hash = GenerateRandom();
        await _pokemonsRepo.Insert(new Pokemon
        {
            Url = Request.Form["url"],
            Hash = hash,
            SessionId = HttpContext.Session.GetString("id")!
        });
        RegisteredPokemons = _pokemonsRepo.Get();
    }
}