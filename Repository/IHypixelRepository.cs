using Project_DeRidderJonas_HypixelApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DeRidderJonas_HypixelApi.Repository
{
    interface IHypixelRepository
    {
        Task<Player> GetPlayerInfoAsync();
    }
}
