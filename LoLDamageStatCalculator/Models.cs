using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiotSharp;
using RiotSharp.Endpoints.Interfaces.Static;
using RiotSharp.Endpoints.StaticDataEndpoint;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;
using RiotSharp.Misc;


namespace LoLDamageStatCalculator
{
    public class Models
    {
        public List<DComboBox> ChampionsViewable;

        public List<ChampionStatic> ChampionData;

        public ChampionStatic Champion;

        public int ChampionLevel;

        public Constants.SummaryType ChampionSummaryType;
    }

    public class DComboBox
    {
        public string ID { get; set; }

        public string Name { get; set; }
    }
}
