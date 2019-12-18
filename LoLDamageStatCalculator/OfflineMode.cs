using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiotSharp;
using RiotSharp.Caching; // find a way to use later
using RiotSharp.Endpoints.Interfaces.Static;
using RiotSharp.Endpoints.StaticDataEndpoint;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;
using RiotSharp.Misc;

namespace LoLDamageStatCalculator
{
    public class OfflineMode : IMode
    {
        private IStaticDataEndpoints ApiInstance;
        private string StaticChampionVersion = "9.24.2"; // need to find out how to get the latest from RiotSharp, or directly from RiotAPI. See https://ddragon.leagueoflegends.com/realms/euw.json and https://ddragon.leagueoflegends.com/api/versions.json


        public OfflineMode()
        {

        }

        public Task<ChampionStatic> GetChampion(string ChampionName)
        {
            throw new NotImplementedException();
        }

        public Task<ChampionListStatic> GetChampions()
        {
            throw new NotImplementedException();
        }

        public Task<List<DComboBox>> GetViewableChampionList()
        {
            throw new NotImplementedException();
        }
    }
}
