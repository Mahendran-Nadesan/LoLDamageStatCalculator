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
    public class OnlineMode : IMode
    {
        private IStaticDataEndpoints ApiInstance;
     
        public OnlineMode()
        {
            ApiInstance = StaticDataEndpoints.GetInstance(true); // uses cache
            //var api = RiotApi.GetDevelopmentInstance("dsags");
            //var x = api.StaticData.
        }

        public async Task<ChampionStatic> GetChampion(string ChampionName)
        {
            try
            {
                ChampionStatic Champion = await ApiInstance.Champions.GetByKeyAsync(ChampionName, Constants.StaticChampionVersion);
                return Champion;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            
        }

        public async Task<ChampionListStatic> GetChampions()
        {
            ChampionListStatic ChampionsList = await ApiInstance.Champions.GetAllAsync(Constants.StaticChampionVersion, Language.en_US, true);
            return ChampionsList;
        }
    }
}
