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
        Task<ChampionListStatic> GetChampionList();
                
        Task<List<DComboBox>> GetViewableChampionList(); // consider moving logic of list to viewable list into a helper class...if it doesn't end up working differently for offline and online modes

        Task<ChampionStatic> GetChampion(string ChampionName);
    }
}
