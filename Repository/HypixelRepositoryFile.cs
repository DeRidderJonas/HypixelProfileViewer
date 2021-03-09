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

        private Player _currentPlayer;

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
                    return _currentPlayer;
                }
            }

        }
    }
}
