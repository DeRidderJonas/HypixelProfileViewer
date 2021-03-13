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
                new GameMode(){DisplayName="Blitz Survival Games", Name="HungerGames", ShortName="Hunger Games"},
                new GameMode(){DisplayName="Arcade", Name="Arcade", ShortName="Arcade"},
                new GameMode(){DisplayName="Paintball", Name="Paintball", ShortName="Paintball"},
                new GameMode(){DisplayName="QuakeCraft", Name="Quake", ShortName="Quake"},
                new GameMode(){DisplayName="SkyWars", Name="SkyWars", ShortName="SkyWars"},
                new GameMode(){DisplayName="TNT Games", Name="TNTGames", ShortName="TNT Games"}
            };

        public static List<GameMode> GetGameModes()
        {
            return _gameModes;
        }
    }
}
