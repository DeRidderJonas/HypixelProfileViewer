using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DeRidderJonas_HypixelApi.Model
{

    public class Arcade
    {
        public int bounty_kills_oneinthequiver { get; set; }
        public int coins { get; set; }
        public int deaths_oneinthequiver { get; set; }
        public int deaths_throw_out { get; set; }
        public int kills_dayone { get; set; }
        public int kills_dragonwars2 { get; set; }
        public int kills_oneinthequiver { get; set; }
        public int kills_throw_out { get; set; }
        public int wins_farm_hunt { get; set; }
        public int wins_oneinthequiver { get; set; }
        public int sw_kills { get; set; }
        public int sw_deaths { get; set; }
        public int wins_party { get; set; }
        public int sw_rebel_kills { get; set; }
        public int wins_dragonwars2 { get; set; }
        public int wins_soccer { get; set; }
        public int wins_ender { get; set; }
    }

    public class Hungergames
    {
        public float coins { get; set; }
        public int deaths { get; set; }
        public int kills { get; set; }
        public int wins { get; set; }
    }

    public class Paintball
    {
        public int coins { get; set; }
        public int deaths { get; set; }
        public int kills { get; set; }
        public int wins { get; set; }
    }

    public class Quake
    {
        public float coins { get; set; }
        public int deaths { get; set; }
        public int kills { get; set; }
    }

    public class Tntgames
    {
        public int coins { get; set; }
        public int deaths_bowspleef { get; set; }
        public int deaths_capture { get; set; }
        public int kills_capture { get; set; }
        public int wins_bowspleef { get; set; }
        public int wins_capture { get; set; }
        public int wins_tntrun { get; set; }
    }

    public class Skywars
    {
        public int coins { get; set; }
        public int wins { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }
    }

}
