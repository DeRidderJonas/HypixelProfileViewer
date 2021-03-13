using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DeRidderJonas_HypixelApi.Model
{
    class SkyWarsStatistics : IGameModeStatistics
    {
        [JsonIgnore]
        public GameMode GameMode { get; set; }
        [JsonProperty(PropertyName ="coins")]
        public int Coins { get; set; }
        [JsonProperty(PropertyName ="kills")]
        public int Kills { get; set; }
        [JsonProperty(PropertyName ="deaths")]
        public int Deaths { get; set; }
        [JsonProperty(PropertyName ="wins")]
        public int Wins { get; set; }

    }
}
