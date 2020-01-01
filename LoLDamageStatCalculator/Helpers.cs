using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiotSharp.Endpoints.StaticDataEndpoint;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;
using RiotSharp.Endpoints.StaticDataEndpoint.Item;

namespace LoLDamageStatCalculator
{
    public static class Helpers
    {
        public static List<string> ExtractSpellStringList(string spellText)
        {
            List<string> results = new List<string>();
            int sIndex;
            int eIndex;

            for (int i = 0; i < spellText.LastIndexOf("{{"); i++)
            {
                sIndex = spellText.IndexOf("{{", i);
                eIndex = spellText.IndexOf("}}", i);

                if (sIndex != -1 && eIndex != -1)
                {
                    results.Add(spellText.Substring(sIndex, (eIndex - sIndex) + 2)); // 2 for the length of }}
                    i = eIndex + 1;
                }
            }

            return results;
        }

        public static double CalculateLevelStat(double baseStat, double perLevelStat, int champLevel)
        {
            return baseStat + (perLevelStat * (champLevel - 1));
        }
    }
}
