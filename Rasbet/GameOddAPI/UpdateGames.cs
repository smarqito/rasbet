using DTO.GetGamesRespDTO;
using GameOddApplication.Repositories;
using System.Net.Http.Headers;


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

    static async Task<ICollection<GameDTO>> GetGamesAsync(string path)
    {
        ICollection<GameDTO> games = new List<GameDTO>();
        HttpResponseMessage response = await client.GetAsync(path);
        response.EnsureSuccessStatusCode();
    
         games = await response.Content.ReadAsAsync<ICollection<GameDTO>>();

        return games;
    }

    public static async Task Update()
    {
        try
        {
            ICollection<GameDTO> g = await GetGamesAsync("games/");
            //foreach(Games game in g)
            //{
            //
            //}
        }
        catch (Exception)
        {
            
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
