using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_DeRidderJonas_HypixelApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Project_DeRidderJonas_HypixelApi.Repository
{
    class HypixelRepositoryWeb : IHypixelRepository
    {
        private static string _apiKey = "72bd5838-1016-4a63-90c0-8c29d61154d1";
        private string _uuid;
        private Player _currentPlayer;
        private List<IGameModeStatistics> _gameModeStats;
        private List<Leaderboard> _leaderboards;


        public async Task<List<Leaderboard>> GetLeaderboards()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string endpoint = $"https://api.hypixel.net/leaderboards?key={_apiKey}";
                    var response = await client.GetAsync(endpoint);

                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException(response.ReasonPhrase);

                    string json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<JObject>(json);
                    var leaderboardsJson = result.SelectToken("leaderboards");

                    var gameModes = GameModeRepository.GetGameModes();

                    List<Task<Leaderboard>> leaderboardTasks = new List<Task<Leaderboard>>();
                    gameModes.ForEach(gameMode => leaderboardTasks.Add(GetLeaderboard(leaderboardsJson, gameMode)));

                    await Task.WhenAll(leaderboardTasks);
                    List<Leaderboard> leaderboards = new List<Leaderboard>();
                    leaderboardTasks.ForEach(task => leaderboards.Add(task.Result));

                    _leaderboards = leaderboards;

                    return _leaderboards;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"HypixelRepositoryWeb::GetLeaderboards > {e.Message}");
                    throw;
                }
            }
        }

        private async Task<Leaderboard> GetLeaderboard(JToken leaderboardsJson, GameMode gm)
        {
            var leaderboardJson = leaderboardsJson.SelectToken($"{gm.LeaderboardName}[0].leaders");
            var leaders = leaderboardJson.ToList();

            List<Task<PlayerId>> playerIdTasks = new List<Task<PlayerId>>();
            leaders.ForEach(leader => playerIdTasks.Add(GetPlayerId(leader.ToObject<string>())));

            await Task.WhenAll(playerIdTasks);
            List<PlayerId> leaderPlayerIds = new List<PlayerId>();
            playerIdTasks.ForEach(task => leaderPlayerIds.Add(task.Result));

            return new Leaderboard() { GameMode = gm, Players = leaderPlayerIds };
        }

        private async Task<PlayerId> GetPlayerId(string uuid)
        {
            string playerName = await GetPlayerName(uuid);
            return new PlayerId() { Name = playerName, Uuid = uuid };
        }

        public async Task<Leaderboard> GetLeaderboardForGameMode(GameMode gameMode)
        {
            if (_leaderboards == null) await GetLeaderboards();

            return _leaderboards.Where(leaderboard => leaderboard.GameMode == gameMode).FirstOrDefault();
        }

        public async Task<string> GetPlayerName(string uuid)
        {
            using(HttpClient client = new HttpClient())
            {
                try
                {
                    string endpoint = $"https://api.mojang.com/user/profiles/{uuid}/names";
                    var response = await client.GetAsync(endpoint);

                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException(response.ReasonPhrase);

                    string json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<JArray>(json);
                    int namesCount = result.Count;

                    return result.ElementAt(namesCount - 1).SelectToken("name").ToObject<string>();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"HypixelRepositoryWeb::GetPlayerName > {e.Message}");
                    throw;
                }
            }
        }

        public async Task<Player> GetPlayerInfoAsync(string uuid)
        {
            if (_currentPlayer != null && _uuid == uuid) return _currentPlayer;

            _uuid = uuid;

            using(HttpClient client = new HttpClient())
            {
                try
                {
                    string endpoint = $"https://api.hypixel.net/player?key={_apiKey}&uuid={_uuid}";
                    var response = await client.GetAsync(endpoint);

                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException(response.ReasonPhrase);

                    string json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<JObject>(json);
                    var player = result.SelectToken("player");

                    _currentPlayer = player.ToObject<Player>();
                    await GetPlayerOnlineStatus();

                    var stats = player.SelectToken("stats");
                    var gameModes = GameModeRepository.GetGameModes();
                    _gameModeStats = gameModes.Select(gameMode => {
                        var statJson = stats.SelectToken(gameMode.Name);
                        var stat = statJson.ToObject(gameMode.Type) as IGameModeStatistics;
                        stat.GameMode = gameMode;
                        return stat;
                    }).ToList();

                    return _currentPlayer;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"HypixelRepositoryWeb::GetPlayerInfoAsync > {e.Message}");
                    throw;
                }
            }
        }

        private async Task GetPlayerOnlineStatus()
        {
            if (_currentPlayer == null) return;

            using(HttpClient client = new HttpClient())
            {
                try
                {
                    string endpoint = $"https://api.hypixel.net/status?key={_apiKey}&uuid={_uuid}";
                    var response = await client.GetAsync(endpoint);

                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException(response.ReasonPhrase);

                    string json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<JObject>(json);

                    var onlineStatus = result.SelectToken("session.online").ToObject<bool>();

                    _currentPlayer.IsOnline = onlineStatus;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"HypixelRepositoryWeb::GetPlayerOnlineStatus > {e.Message}");
                    throw;
                }
            }
        }

        public async Task<IGameModeStatistics> GetStatisticsForGameMode(GameMode gameMode)
        {
            if (_currentPlayer == null) await GetPlayerInfoAsync(_uuid);

            return _gameModeStats.Where(stat => stat.GameMode == gameMode).FirstOrDefault();
        }
    }
}
