using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Trivia2._0.Models;

namespace Trivia2._0.Services
{
    public class TriviaService
    {
        public User LoggedPlayer;
        HttpClient httpClient;
        JsonSerializerOptions options;
        const string URL = $@"https://qsc714b9-7128.euw.devtunnels.ms/TriviaApi/";
        public TriviaService()
        {
            httpClient = new();
            options = new()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }
        public async Task<bool> LogPlayer(string email, string password)
        {
            try
            {
                JsonSerializerOptions newOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                UserLog ul = new()
                {
                    Email = email,
                    Password = password
                };
                string json = JsonSerializer.Serialize(ul, newOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{URL}Login", content);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    UserLogResponse ulr = JsonSerializer.Deserialize<UserLogResponse>(await response.Content.ReadAsStringAsync(), options);
                    Rank uRank = new Rank()
                    {
                        Rankid = ulr.playerRank.rankId,
                        RankName = ulr.playerRank.rankName
                    };
                    LoggedPlayer = new()
                    {
                        Id = ulr.playerId,
                        Email = ulr.playerEmail,
                        Pswrd = ulr.playerPassword,
                        Username = ulr.playerName,
                        Points = ulr.playerScore,
                        Questionsadded = ulr.questions.Length,
                        Rank = uRank,
                        Rankid = uRank.Rankid
                    };
                    return true;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    return false;
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
