using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class CreatePokemonModel : PageModel
{
    private readonly HttpClient _httpClient;

    public CreatePokemonModel(IConfiguration defaultConfig)
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(defaultConfig.GetValue<String>("apiHost")) };
    }

    public async Task<IActionResult> OnPostCreate(string name, string description, string typeOne, string typeTwo,
        int hp, int speed, int attack, int speAtt, int defense, int speDef)
    {
        typeTwo = String.IsNullOrEmpty(typeTwo) ? "null" : "\"" + typeTwo + "\"";
        var json = "{" +
                   $""" "name": "{name}", "description": "{description}", "type1": "{typeOne}",""" +
                   $""" "type2": {typeTwo}, "hp": {hp}, "def": {defense}, "defSpe": {speDef},""" +
                   $""" "attack": {attack},  "attackSpe": {speAtt}, "speed": {speed} , "imagePath": null""" +
                   "}";
        HttpContent body = new StringContent(json, Encoding.UTF8, "application/json");
        var pokemonResponse = await _httpClient.PostAsync("api/Pokemon", body);

        Pokemon? pokemon = null;
        if (pokemonResponse.IsSuccessStatusCode)
            pokemon = await pokemonResponse.Content.ReadFromJsonAsync<Pokemon>();
        
        if (pokemon == null)
            return Page();
        
        return Redirect($"~/Info/{pokemon.Id}"); 
    }
}