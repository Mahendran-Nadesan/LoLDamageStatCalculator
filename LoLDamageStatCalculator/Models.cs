using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiotSharp;
using RiotSharp.Endpoints.Interfaces.Static;
using RiotSharp.Endpoints.StaticDataEndpoint;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;
using RiotSharp.Endpoints.StaticDataEndpoint.Item;
using RiotSharp.Misc;


namespace LoLDamageStatCalculator
{
    public class Models
    {
        public List<DComboBox> ChampionsViewable;

        public List<ChampionStatic> ChampionData;

        public ChampionStatic Champion;

        public int ChampionLevel;

        public List<ItemStatic> ChampionItems = new List<ItemStatic>();

        public ChampionSpellStatic ChampionSpell;

        public Dictionary<string, int> ChampionSpellLevels = new Dictionary<string, int>();

        public int ChampionSpellLevel;

        public Constants.SummaryType ChampionSummaryType;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSpellStrings()
        {
            string spellText = ChampionSpell.Tooltip;

            List<string> spellStrings = Helpers.ExtractSpellStringList(spellText);
            List<string> finalStrings = new List<string>();
            string finalString = spellText;

            foreach (string placeholderString in spellStrings)
            {
                finalStrings.Add(GetSpellString(placeholderString));
            }

            for (int i = 0; i < spellStrings.Count; i++)
            {
                finalString = finalString.Replace(spellStrings[i], finalStrings[i]);
            }

            return finalString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="placeholderString"></param>
        /// <returns></returns>
        /// <PossibleValues>
        /// 
        /// </PossibleValues>
        public string GetSpellString(string placeholderString)
        {
            double size = 0;

            if (placeholderString.Contains("e") && placeholderString.Length == 8) // need a better check than length
            {
                int num = Convert.ToInt32(placeholderString.Substring(4, 1));
                size = GetSpellEffectValue(num);
            }
            else if (placeholderString.Contains("a") && placeholderString.Length == 8)
            {
                size = GetVarSpellValue(placeholderString.Substring(3, 2));
            }
            else if (placeholderString.Contains("f") && placeholderString.Length == 8)
            {
                // needs to be implemented when Riot actually put this data in the API
            }
            else
            {
                return "?";
            }

            return size.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="effectNumber">1</param>
        /// <param name="spellLevel">3</param>
        /// <param name="spell"></param>
        /// <returns></returns>
        public double GetSpellEffectValue(int effectNumber)
        {
            return ChampionSpell.Effects[effectNumber][ChampionSpellLevel - 1];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="varSpell">"a1"</param>
        /// <param name="spellLevel"></param>
        /// <param name="spell"></param>
        /// <returns></returns>
        public double GetVarSpellValue(string varSpell)
        {
            var coeff = ChampionSpell.Vars.Where(c => c.Key == varSpell).FirstOrDefault();

            if (coeff == null)
                return 0;

            // todo: factor in spell level                        
            double spellCoeff = Convert.ToDouble(coeff.Coeff); // can't do a simple cast, because coeff is boxed
            string spellAttr = coeff.Link;
            double effect = GetVarSpellEffectValue(spellCoeff, spellAttr);

            return effect;
        }

        // basic formula:
        // (base stat + items stats + runes stat) * stat coeff
        // still need to work out how to get % based stats in here
        public double GetVarSpellEffectValue(double spellCoeff, string spellAttr)
        {
            switch (spellAttr)
            {
                case "spelldamage":
                    return (CalculateAttributeStats("AP") + CalculateItemStats("AP") + 0) * spellCoeff;
                case "bonusattackdamage":
                    return (0 + CalculateItemStats("AD") + 0) * spellCoeff;
                case "attackdamage":
                    return (CalculateAttributeStats("AD") + CalculateItemStats("AD") + 0) * spellCoeff;
                case "bonusarmor":
                    return 0;
                case "bonusspellblock":
                    return 0;
                case "armor":
                    return 0;
                case "bonushealth":
                    return 0;
                case "health":
                    return 0;
                default:
                    return 0;
            }
        }

        // for now not using the itemAttributeNames, but should use reflection and get the field's value from the string
        // fieldinfo, reflection, invoke?
        public double CalculateItemStats(string attrType)
        {
            if (ChampionItems.Count == 0)
            {
                return 0;
            }

            List<string> itemAttributeNames = new List<string>();

            // will need markus' help to map these
            switch (attrType)
            {
                case "AP":
                    itemAttributeNames.Add("FlatMagicDamageMod");
                    return ChampionItems.Sum(c => c.Stats.FlatMagicDamageMod);
                case "AD":
                    itemAttributeNames.Add("FlatPhysicalDamageMod");
                    return ChampionItems.Sum(c => c.Stats.FlatPhysicalDamageMod);
                case "HP":
                    return ChampionItems.Sum(c => c.Stats.FlatHPPoolMod);
                case "MP":
                    return ChampionItems.Sum(c => c.Stats.FlatMPPoolMod);
                case "ARM":
                    itemAttributeNames.Add("FlatArmorMod");
                    return ChampionItems.Sum(c => c.Stats.FlatArmorMod);
                case "MR":
                    return ChampionItems.Sum(c => c.Stats.FlatSpellBlockMod);
                default:
                    return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attrType"></param>
        /// <returns></returns>
        public double CalculateAttributeStats(string attrType)
        {
            switch (attrType)
            {
                case "AP":
                    return 0; // there is no basic AP value, so it's 0, runes not yet implemented, so 0
                case "AD":
                    return Helpers.CalculateLevelStat(Champion.Stats.AttackDamage, Champion.Stats.AttackDamagePerLevel, ChampionLevel);
                case "HP":
                    return Helpers.CalculateLevelStat(Champion.Stats.Hp, Champion.Stats.HpPerLevel, ChampionLevel);
                case "MP":
                    return Helpers.CalculateLevelStat(Champion.Stats.Mp, Champion.Stats.MpPerLevel, ChampionLevel);
                case "ARM":
                    return Helpers.CalculateLevelStat(Champion.Stats.Armor, Champion.Stats.ArmorPerLevel, ChampionLevel);
                case "MR":
                    return Helpers.CalculateLevelStat(Champion.Stats.SpellBlock, Champion.Stats.SpellBlockPerLevel, ChampionLevel);
                default:
                    return 0;
            }
        }
    }

    public class DComboBox
    {
        public string ID { get; set; }

        public string Name { get; set; }
    }
}
