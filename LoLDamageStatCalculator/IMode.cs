using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiotSharp.Endpoints.StaticDataEndpoint;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;
using RiotSharp.Misc;

namespace LoLDamageStatCalculator
{
    // note: interfaces and classes that implement it use UpperCase for arguments and variables
    public interface IMode
    {
        Task<ChampionListStatic> GetChampions();

        Task<ChampionStatic> GetChampion(string ChampionName);
    }
}
