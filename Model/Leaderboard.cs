using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DeRidderJonas_HypixelApi.Model
{
    class Leaderboard
    {
        [JsonIgnore]
        public GameMode GameMode { get; set; }
        public List<PlayerId> Players { get; set; }
    }
}
