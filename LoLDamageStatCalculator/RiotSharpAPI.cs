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
    public class RiotSharpAPI
    {
        private IStaticDataEndpoints ApiInstance;
        private string StaticChampionVersion = "9.24.2"; // need to find out how to get the latest from RiotSharp, or directly from RiotAPI. See https://ddragon.leagueoflegends.com/realms/euw.json and https://ddragon.leagueoflegends.com/api/versions.json

        public RiotSharpAPI(string ApiKey) // maybe remove this if static api doesn't use a key
        {
            ApiInstance = StaticDataEndpoints.GetInstance(true); // uses cache
        }

        public async Task<List<string>> GetChampionList()
        {
            ChampionListStatic champs = await ApiInstance.Champions.GetAllAsync(StaticChampionVersion, Language.en_US, false);
            var x = champs.Champions;
            var y = x.Select(c => c.Value.Name);
            return y.ToList();
        }

        
    }
}
