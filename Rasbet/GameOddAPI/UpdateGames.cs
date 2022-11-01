using GameOddApplication.Repositories;
using System.Net.Http.Headers;

namespace GameOddAPI;

public class Outcomes
{
    public string Name { get; set; }
    public double Price { get; set; }
}

public class Market 
{
    public string Key { get; set; }
    public ICollection<Outcomes> Outcomes { get; set; }

}

public class Bookmaker
{
    public string Key { get; set; }
    public DateTime LastUpdate { get; set; }
    public ICollection<Market> Markets { get; set; }

}

public class Games
{
    public string Id { get; set; }
    public string HomeTeam { get; set; }
    public string AwayTeam { get; set; }
    public DateTime CommenceTime { get; set; }
    public bool Completed { get; set; }
    public string Scores { get; set; }
    public ICollection<Bookmaker> Bookmakers { get; set; }

}
public class UpdateGames
{
    static HttpClient client = new HttpClient();
    private readonly GameRepository gameRepository;

    public UpdateGames()
    {
        client.BaseAddress = new Uri("http://ucras.di.uminho.pt/v1/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
    }

    static async Task<ICollection<Games>> GetGamesAsync(string path)
    {
        ICollection<Games> games = new List<Games>();
        HttpResponseMessage response = await client.GetAsync(path);
        if (response.IsSuccessStatusCode)
        {
            games = await response.Content.ReadAsAsync<ICollection<Games>>();
        }
        return games;
    }

    public static async Task Update()
    {
        try
        {
            ICollection<Games> g = await GetGamesAsync("games/");
            foreach(Games game in g)
            {

            }
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public void Thread1()
    {
        TimeSpan t = new TimeSpan(0, 0, 10);
        while (true)
        {
            Task.Run(() => Update());
            Thread.Sleep(t);
        }
    }
}
