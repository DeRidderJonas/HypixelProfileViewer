using Project_DeRidderJonas_HypixelApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DeRidderJonas_HypixelApi.Repository
{
    class GameModeRepository
    {
        private static List<GameMode> _gameModes = new List<GameMode>()
            {
                new GameMode(){DisplayName="Blitz Survival Games", Name="HungerGames", ShortName="Hunger Games", LeaderboardName="SURVIVAL_GAMES"},
                new GameMode(){DisplayName="Arcade", Name="Arcade", ShortName="Arcade", LeaderboardName="ARCADE"},
                new GameMode(){DisplayName="Paintball", Name="Paintball", ShortName="Paintball", LeaderboardName="PAINTBALL"},
                new GameMode(){DisplayName="QuakeCraft", Name="Quake", ShortName="Quake", LeaderboardName="QUAKECRAFT"},
                new GameMode(){DisplayName="SkyWars", Name="SkyWars", ShortName="SkyWars", LeaderboardName="SKYWARS"},
                new GameMode(){DisplayName="TNT Games", Name="TNTGames", ShortName="TNT Games", LeaderboardName="TNTGAMES"}
            };

        public static List<GameMode> GetGameModes()
        {
            return _gameModes;
        }
    }
}
