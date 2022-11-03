using System.Net.Http.Json;

namespace BetFacade;

public class APIService
{
    private HttpClient _httpClientUser;
    private HttpClient _httpClientGameOdd;

    public APIService()
    {
        _httpClientUser = new ();
        _httpClientUser.BaseAddress = new("http://localhost:5000/");
        _httpClientGameOdd = new ();
        _httpClientGameOdd.BaseAddress = new("http://localhost:5002/");
    }

    public async Task<double> GetOdd(int betTypeId, int oddId)
    {
        //HttpResponseMessage resp = await _httpClientGameOdd.GetAsync($"gameodd/odd?betTypeId={betTypeId}&oddId={oddId}");
        HttpResponseMessage resp = await _httpClientGameOdd.PatchAsync($"gameodd/finish?gameId=1&result=1&specialistId=1", null);
        resp.EnsureSuccessStatusCode();
        //return double.Parse(await resp.Content.ReadFromJsonAsync<>());
        return double.Parse(await resp.Content.ReadAsStringAsync());
    }

}
