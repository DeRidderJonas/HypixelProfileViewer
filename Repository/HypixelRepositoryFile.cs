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
        static private string _onlineJsonResourceName = "Project_DeRidderJonas_HypixelApi.Resources.Data.HypixelOnline.json";

        private Player _currentPlayer;
        private List<IGameModeStatistics> _gameModeStats;

        public async Task<Player> GetPlayerInfoAsync()
        {
            if (_currentPlayer != null) return _currentPlayer;

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            using(Stream stream = assembly.GetManifestResourceStream(_playerJsonResourceName))
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
            if (_currentPlayer == null) await GetPlayerInfoAsync();

            return _gameModeStats.Where(stat => stat.GameMode == gameMode).FirstOrDefault();
        }
    }
}
