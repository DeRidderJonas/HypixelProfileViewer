using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DeRidderJonas_HypixelApi.Model
{
    class ArcadeStatistics : IGameModeStatistics
    {
        [JsonIgnore]
        public GameMode GameMode { get; set;}
        [JsonProperty(PropertyName = "coins")]
        public int Coins { get; set; }
        [JsonIgnore]
        public int Kills { get {
                return KillsOneInTheQuiver + KillsDayOne + KillsThrowOut + KillsDragonWars + StarwarsKills;
            } 
        }
        [JsonIgnore]
        public int Deaths { get {
                return DeathsOneInTheQuiver + DeathsThrowOut + StarWarsDeaths;    
            } 
        }
        [JsonIgnore]
        public int Wins { get {
                return WinsFarmHunt + WinsOneInTheQuiver + WinsParty + WinsSoccer + WinsEnder;
            } 
        }

        //All arcade games Kills
        [JsonProperty(PropertyName = "kills_oneinthequiver")]
        public int KillsOneInTheQuiver { get; set; }
        [JsonProperty(PropertyName = "kills_dayone")]
        public int KillsDayOne { get; set; }
        [JsonProperty(PropertyName = "kills_throw_out")]
        public int KillsThrowOut { get; set; }
        [JsonProperty(PropertyName = "kills_dragonwars2")]
        public int KillsDragonWars { get; set; }
        [JsonProperty(PropertyName = "sw_kills")]
        public int StarwarsKills { get; set; }

        //All arcade games deaths
        [JsonProperty(PropertyName = "deaths_oneinthequiver")]
        public int DeathsOneInTheQuiver { get; set; }
        [JsonProperty(PropertyName = "deaths_throw_out")]
        public int DeathsThrowOut { get; set; }
        [JsonProperty(PropertyName = "sw_deaths")]
        public int StarWarsDeaths { get; set; }

        //All arcade games Wins
        [JsonProperty(PropertyName = "wins_farm_hunt")]
        public int WinsFarmHunt { get; set; }
        [JsonProperty(PropertyName = "wins_oneinthequiver")]
        public int WinsOneInTheQuiver { get; set; }
        [JsonProperty(PropertyName = "wins_party")]
        public int WinsParty { get; set; }
        [JsonProperty(PropertyName = "wins_soccer")]
        public int WinsSoccer { get; set; }
        [JsonProperty(PropertyName = "wins_ender")]
        public int WinsEnder { get; set; }


    }
}
