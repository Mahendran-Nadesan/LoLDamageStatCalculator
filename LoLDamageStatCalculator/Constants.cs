using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLDamageStatCalculator
{
    public static class Constants
    {
        public const int MaxChampionLevel = 18;

        public const string StaticChampionVersion = "9.24.2"; // need to find out how to get the latest from RiotSharp, or directly from RiotAPI. See https://ddragon.leagueoflegends.com/realms/euw.json and https://ddragon.leagueoflegends.com/api/versions.json

        public enum SummaryType
        {
            BaseStats,
            PassiveStats,
            QStats,
            WStats,
            EStats,
            RStats
        }
            

    }
}

// todo:
//
// Move these constants to a settings file, and call them from there.
// Some of these should be pulled from an api call though.
//
// Work out all summary types