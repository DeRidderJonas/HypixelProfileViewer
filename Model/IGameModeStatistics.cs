using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DeRidderJonas_HypixelApi.Model
{
    interface IGameModeStatistics
    {
        GameMode GameMode { get; set; }
        int Coins { get; }
        int Kills { get; }
        int Deaths { get; }
        int Wins { get; }
    }
}
