using Domain;
using Domain.ResultDomain;
using DTO.GetGamesRespDTO;
using GameOddApplication.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace GameOddAPI;

public class UpdateGames
{
    static HttpClient client = new HttpClient();
    readonly IGameOddFacade gameOddFacade;

    public UpdateGames(IGameOddFacade gameOddFacade)
    {
        this.gameOddFacade = gameOddFacade;
        client.BaseAddress = new Uri("http://ucras.di.uminho.pt/v1/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
    }

    static async Task<ICollection<GameDTO>> GetGamesAsync(string path)
    {
        ICollection<GameDTO> games = new List<GameDTO>();
        //JsonSerializer serializer = new JsonSerializer();
        //StreamReader r = new StreamReader(@"C:\Users\Miguel\Downloads\response_1667329076073.json");
        //string json = r.ReadToEnd();
        //games = JsonConvert.DeserializeObject<ICollection<GameDTO>>(json);
        HttpResponseMessage response = await client.GetAsync(path);
        response.EnsureSuccessStatusCode();

        games = await response.Content.ReadAsAsync<ICollection<GameDTO>>();

        return games;
    }

    public async Task Update()
    {
        try
        {
            ICollection<GameDTO> g = await GetGamesAsync("games/");
            await gameOddFacade.UpdateGameOdd(g, "Football");

        }
        catch (Exception)
        {

        }

    }

    public void Thread1()
    {
        TimeSpan t = new TimeSpan(0, 10, 0);
        while (true)
        {
            Task.Run(() => Update());
            Thread.Sleep(t);
        }
    }
}