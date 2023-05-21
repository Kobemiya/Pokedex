using System.Text;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages;

public class EditModel : PageModel
{
    private readonly HttpClient _httpClient;
    
    public Pokemon? CurrentPokemon;
    public IEnumerable<Attack> RegisteredAttacks;
    public String SelectedAttackId;

    public EditModel(IConfiguration defaultConfig)
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(defaultConfig.GetValue<String>("apiHost")) };
    }

    private async Task FetchCurrentPokemon(long id)
    {
        CurrentPokemon = null;
        var pokemonResponse = await _httpClient.GetAsync($"api/Pokemon/{id}");
        if (!pokemonResponse.IsSuccessStatusCode) return; 
        CurrentPokemon = await pokemonResponse.Content.ReadFromJsonAsync<Pokemon>();
    }

    private async Task FetchAttacklist()
    {
        var attacksResponse = await _httpClient.GetAsync("api/Attack");
        if (attacksResponse.IsSuccessStatusCode)
            RegisteredAttacks = await attacksResponse.Content.ReadFromJsonAsync<IEnumerable<Attack>>();
        else
            RegisteredAttacks = Enumerable.Empty<Attack>();
    }

    public async Task<IActionResult> OnGet(long pokemonId)
    {
        await FetchCurrentPokemon(pokemonId);
        await FetchAttacklist();
        if (CurrentPokemon == null)
            return Redirect("~/Index");
        return Page();
    }

    public async Task<IActionResult> OnPostSave(long pokemonId, string name, string description, string typeOne, string typeTwo,
        int hp, int speed, int attack, int speAtt, int defense, int speDef)
    {
        typeTwo = String.IsNullOrEmpty(typeTwo) ? "null" : "\"" + typeTwo + "\"";
        var json = "{" +
                   $""" "name": "{name}", "description": "{description}", "type1": "{typeOne}",""" +
                   $""" "type2": {typeTwo}, "hp": {hp}, "def": {defense}, "defSpe": {speDef},""" +
                   $""" "attack": {attack},  "attackSpe": {speAtt}, "speed": {speed} , "imagePath": null""" +
                   "}";
        HttpContent body = new StringContent(json, Encoding.UTF8, "application/json");
        var pokemonResponse = await _httpClient.PutAsync($"api/Pokemon/{pokemonId}", body);

        if (!pokemonResponse.IsSuccessStatusCode)
        {
            await FetchCurrentPokemon(pokemonId);
            await FetchAttacklist();
            return Page();
        }
        
        return Redirect($"~/Info/{pokemonId}"); 
    }

    public async Task OnPostAddAttack(long pokemonId, string attackId)
    {
        await FetchCurrentPokemon(pokemonId);
        await FetchAttacklist();
        var response = await _httpClient.PutAsync($"api/Pokemon/{pokemonId}/attacks/{attackId}", null);
        if (response.IsSuccessStatusCode && !(CurrentPokemon?.Attacks.Contains(long.Parse(attackId)) ?? true))
            CurrentPokemon?.Attacks.Add(long.Parse(attackId));
    }

    public async Task OnPostRemoveAttack(long pokemonId, string attackId)
    {
        await FetchCurrentPokemon(pokemonId);
        await FetchAttacklist();
        var response = await _httpClient.DeleteAsync($"api/Pokemon/{pokemonId}/attacks/{attackId}");
        if (response.IsSuccessStatusCode)
            CurrentPokemon?.Attacks.Remove(long.Parse(attackId));
    }

    public async Task<IActionResult> OnPostDeletePokemon(long pokemonId)
    {
        var response = await _httpClient.DeleteAsync($"api/Pokemon/{pokemonId}");
        if (!response.IsSuccessStatusCode)
        {
            await FetchCurrentPokemon(pokemonId);
            await FetchAttacklist();
            return Page();
        }
        
        return Redirect("~/Index");
    }
}