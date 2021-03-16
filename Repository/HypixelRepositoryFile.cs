using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_DeRidderJonas_HypixelApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DeRidderJonas_HypixelApi.Repository
{
    class HypixelRepositoryFile : IHypixelRepository
    {

        static private string _playerJsonResourceName = "Project_DeRidderJonas_HypixelApi.Resources.Data.HypixelUser.json";
        static private string _player2JsonResourceName = "Project_DeRidderJonas_HypixelApi.Resources.Data.HypixelUser2.json";
        static private string _onlineJsonResourceName = "Project_DeRidderJonas_HypixelApi.Resources.Data.HypixelOnline.json";
        static private string _leaderboardJsonResourceName = "Project_DeRidderJonas_HypixelApi.Resources.Data.Leaderboards.json";

        static private string _creatorUUID = "746b0410b2e347e7ad05a5473d35c097";

        private string _uuid;
        private Player _currentPlayer;
        private List<IGameModeStatistics> _gameModeStats;
        private List<Leaderboard> _leaderboards;


        public async Task<List<Leaderboard>> GetLeaderboards()
        {
            if (_leaderboards != null) return _leaderboards;

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            using(Stream stream = assembly.GetManifestResourceStream(_leaderboardJsonResourceName))
            {
                using(var reader = new StreamReader(stream))
                {
                    string json = await reader.ReadToEndAsync();
                    var result = JsonConvert.DeserializeObject<JObject>(json);
                    var leaderboards = result.SelectToken("leaderboards");

                    var gameModes = GameModeRepository.GetGameModes();
                    _leaderboards = gameModes.Select(gameMode =>
                    {
                        var leaderboardJson = leaderboards.SelectToken($"{gameMode.LeaderboardName}[0].leaders");
                        var leaders = leaderboardJson.ToList();

                        var leadersPlayerIds = leaders.Select(leader => new PlayerId() { Uuid = leader.ToObject<string>() }).ToList();
                        
                        return new Leaderboard() { Players = leadersPlayerIds, GameMode = gameMode };
                    }).ToList();

                    return _leaderboards;
                }
            }
        }

        public async Task<Leaderboard> GetLeaderboardForGameMode(GameMode gameMode)
        {
            if (_leaderboards == null) await GetLeaderboards();

            return _leaderboards.Where(leaderboard => leaderboard.GameMode == gameMode).FirstOrDefault();
        }

        public async Task<Player> GetPlayerInfoAsync(string uuid)
        {
            if (_currentPlayer != null && uuid == _uuid) return _currentPlayer;

            _uuid = uuid;

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            using(Stream stream = assembly.GetManifestResourceStream(uuid == _creatorUUID ? _playerJsonResourceName : _player2JsonResourceName))
            {
                using(var reader = new StreamReader(stream))
                {
                    string json = await reader.ReadToEndAsync();
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
            }

        }

        public async Task GetPlayerOnlineStatus()
        {
            if (_currentPlayer == null) return;

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            using(Stream stream = assembly.GetManifestResourceStream(_onlineJsonResourceName))
            {
                using(var reader = new StreamReader(stream))
                {
                    string json = await reader.ReadToEndAsync();
                    var result = JsonConvert.DeserializeObject<JObject>(json);
                    var onlineStatus = result.SelectToken("session.online").ToObject<bool>();

                    _currentPlayer.IsOnline = onlineStatus;
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
