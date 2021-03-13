using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DeRidderJonas_HypixelApi.Model
{
    class TNTGamesStatistics : IGameModeStatistics
    {
        [JsonIgnore]
        public GameMode GameMode { get; set; }
        [JsonProperty(PropertyName = "coins")]
        public int Coins { get; set; }
        [JsonProperty(PropertyName = "kills_capture")]
        public int Kills { get; set; }
        [JsonIgnore]
        public int Deaths { get {
                return DeathsBowSpleef + DeathsCapture;
            } 
        }
        [JsonIgnore]
        public int Wins { get {
                return WinsBowspleef + WinsCapture + WinsTntRun;    
            } 
        }

        [JsonProperty(PropertyName = "deaths_bowspleef")]
        public int DeathsBowSpleef { get; set; }
        [JsonProperty(PropertyName = "deaths_captures")]
        public int DeathsCapture { get; set; }
        [JsonProperty(PropertyName = "wins_bowspleef")]
        public int WinsBowspleef { get; set; }
        [JsonProperty(PropertyName = "wins_capture")]
        public int WinsCapture { get; set; }
        [JsonProperty(PropertyName = "wins_tntrun")]
        public int WinsTntRun { get; set; }

    }
}
