using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RiotSharp;
using RiotSharp.Endpoints.Interfaces.Static;
using RiotSharp.Endpoints.StaticDataEndpoint;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;
using RiotSharp.Endpoints.StaticDataEndpoint.Item;
using RiotSharp.Misc;


// Notes:
//
// 1. Don't return values unless you have to - since this stuff is pretty encapsulated, setting object/field values inside void methods will suffice
// 2. Wherever possible, pass/use data objects/fields, not the form control's value - changes to a form control's value should update a data object/field
namespace LoLDamageStatCalculator
{
    public partial class Main : Form
    {
        #region PERSISTENT OBJECTS

        IMode api;
        public Models data;

        #endregion

        public Main()
        {
            InitializeComponent();
        }

        #region MODE & MODE CHANGING EVENTS

        private void rbOnline_CheckedChanged(object sender, EventArgs e)
        {
            SetOnlineMode(true);

        }

        private void rbOffline_CheckedChanged(object sender, EventArgs e)
        {
            SetOnlineMode(false);
        }

        private void SetOnlineMode(bool isOnline = true)
        {
            if (isOnline)
            {
                api = new OnlineMode();
            }
            else
            {
                api = new OfflineMode();
            }
        }

        #endregion

        #region LOAD METHODS

        private void Main_Load(object sender, EventArgs e)
        {
            // set to online mode on start for now, later add a setting to load last mode
            rbOffline.Enabled = false;
            rbOnline.Checked = true;
            SetOnlineMode(true);

            // initial loading
            try
            {
                LoadInitialState();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void LoadInitialState()
        {
            data = new Models
            {
                ChampionSummaryType = Constants.SummaryType.PassiveStats
            };

            // non-async stuff
            LoadLevelDropdown();
            LoadSpellLevelDropdown();
            cbxLevels.Enabled = false;

            // event handlers
            //cbxQLevel.SelectedIndexChanged += SetSpellLevel;
            //cbxWLevel.SelectedIndexChanged += SetSpellLevel;
            //cbxELevel.SelectedIndexChanged += SetSpellLevel;
            //cbxRLevel.SelectedIndexChanged += SetSpellLevel;

            // async stuff
            await GetChampionData();

            LoadChampionDropdown();

            //btnQ.Click += RefreshButtons;
            //btnW.Click += RefreshButtons;
            //btnE.Click += RefreshButtons;
            //btnR.Click += RefreshButtons;

            // temp
            // GetAllSpellTypes();

        }

        private void GetAllSpellTypes()
        {
            if (data.ChampionData.Count == 0)
            {
                webStats.DocumentText = "No data";
            }

            List<string> spells = new List<string>();

            foreach (var champ in data.ChampionData)
            {
                for (int i = 0; i < champ.Spells.Count; i++)
                {
                    if (champ.Spells[i].Vars.Count > 0)
                    {
                        foreach (var vars in champ.Spells[i].Vars)
                        {
                            spells.Add(vars.Link);
                        }

                    }
                }
            }

            var s = spells.Distinct().ToList();
            foreach (var item in s)
            {
                Console.WriteLine(item);
            }

        }

        private async Task GetChampionData() // marked as Task to ensure we wait on it
        {
            var rawChampionData = await api.GetChampions();
            data.ChampionData = rawChampionData.Champions.Select(c => c.Value).ToList();
        }

        private void LoadChampionDropdown()
        {
            if (data.ChampionData.Count > 0)
            {
                data.ChampionsViewable = data.ChampionData.Select(c => new DComboBox { ID = c.Key, Name = c.Name }).ToList(); // consider moving to a helper class
                data.ChampionsViewable.Insert(0, new DComboBox { ID = "None", Name = "Select a champion" });
                cbxChampions.DisplayMember = "Name";
                cbxChampions.ValueMember = "ID";
                cbxChampions.DataSource = data.ChampionsViewable;
            }
            else
            {
                MessageBox.Show("No champions returned");
            }
        }

        private void LoadLevelDropdown()
        {
            List<int> levels = new List<int>();
            levels.AddRange(Enumerable.Range(1, Constants.MaxChampionLevel));
            cbxLevels.DataSource = levels; // doesn't need a dummy "Select a ..." option, default must be 1

            data.ChampionLevel = (int)cbxLevels.SelectedValue;
        }

        private void LoadSpellLevelDropdown()
        {
            // did not use addrange(enumerable.range because the levels end up SHARING the datasource)

            cbxQLevel.SelectedIndexChanged -= SetSpellLevel;
            cbxWLevel.SelectedIndexChanged -= SetSpellLevel;
            cbxELevel.SelectedIndexChanged -= SetSpellLevel;
            cbxRLevel.SelectedIndexChanged -= SetSpellLevel;

            cbxQLevel.DataSource = new List<int>() { 0, 1, 2, 3, 4, 5 };
            cbxWLevel.DataSource = new List<int>() { 0, 1, 2, 3, 4, 5 };
            cbxELevel.DataSource = new List<int>() { 0, 1, 2, 3, 4, 5 };
            cbxRLevel.DataSource = new List<int>() { 0, 1, 2, 3 };

            data.ChampionSpellLevels["Q"] = 0;
            data.ChampionSpellLevels["W"] = 0;
            data.ChampionSpellLevels["E"] = 0;
            data.ChampionSpellLevels["R"] = 0;

            cbxQLevel.SelectedIndexChanged += SetSpellLevel;
            cbxWLevel.SelectedIndexChanged += SetSpellLevel;
            cbxELevel.SelectedIndexChanged += SetSpellLevel;
            cbxRLevel.SelectedIndexChanged += SetSpellLevel;
        }

        #endregion

        #region SUMMARY & SUMMARY CHANGING EVENTS

        // do not add parameters which can be easily gotten through the current data model, e.g. data.Champion, data.Items
        // do not check for empty objs here, that must be done in the calling methods/events
        private void SetBasicStatsSummary()
        {
            txtSummary.Clear();

            var stats = data.Champion.Stats;

            txtSummary.Text += string.Format("Base Stats at level {0}" + Environment.NewLine, data.ChampionLevel);

            txtSummary.Text += string.Format("HP: {0} ({1} + {2} + {3} + {4})" + Environment.NewLine, CalculateLevelStat(stats.Hp, stats.HpPerLevel), stats.Hp, CalculateLevelStat(0, stats.HpPerLevel), "0", "0");

            txtSummary.Text += string.Format("Resource: {0} ({1} + {2} + {3} + {4})" + Environment.NewLine, CalculateLevelStat(stats.Mp, stats.MpPerLevel), stats.Mp, CalculateLevelStat(0, stats.MpPerLevel), "0", "0");

            txtSummary.Text += string.Format("AD: {0} ({1} + {2} + {3} + {4})" + Environment.NewLine, CalculateLevelStat(stats.AttackDamage, stats.AttackDamagePerLevel), stats.AttackDamage, CalculateLevelStat(0, stats.AttackDamagePerLevel), "0", "0");

            txtSummary.Text += string.Format("ARM: {0} ({1} + {2} + {3} + {4})" + Environment.NewLine, CalculateLevelStat(stats.Armor, stats.ArmorPerLevel), stats.Armor, CalculateLevelStat(0, stats.ArmorPerLevel), "0", "0");

            txtSummary.Text += string.Format("MR: {0} ({1} + {2} + {3} + {4})" + Environment.NewLine, CalculateLevelStat(stats.SpellBlock, stats.SpellBlockPerLevel), stats.SpellBlock, CalculateLevelStat(0, stats.SpellBlockPerLevel), "0", "0");

            txtSummary.Text += string.Format("MS: {0} ({1} + {2} + {3} + {4})" + Environment.NewLine, CalculateLevelStat(stats.MoveSpeed, 0), stats.MoveSpeed, 0, "0", "0");
        }

        private void SetSpellSummary()
        {
            webStats.DocumentText = "";
            Constants.SummaryType summaryType = data.ChampionSummaryType;

            if (summaryType == Constants.SummaryType.QStats || summaryType == Constants.SummaryType.WStats || summaryType == Constants.SummaryType.EStats || summaryType == Constants.SummaryType.RStats)
            {
                var champ = data.Champion;

                // the button color code is shit
                switch (summaryType)
                {
                    case Constants.SummaryType.QStats:
                        // plan:
                        // get string list of all spells, e.g. [0] = "{{ e1 }}", [1] = "{{ a1 }}"
                        // loop and replace values:
                        // for x in list, string.replace([0], GetSpellEffect(...)) etc.

                        // new section - so we can remove the switch statement in the model's functions
                        data.ChampionSpell = champ.Spells[0];
                        data.ChampionSpellLevel = data.ChampionSpellLevels["Q"];
                        btnW.BackColor = DefaultBackColor;
                        btnE.BackColor = DefaultBackColor;
                        btnR.BackColor = DefaultBackColor;
                        break;
                    case Constants.SummaryType.WStats:
                        data.ChampionSpell = champ.Spells[1];
                        data.ChampionSpellLevel = data.ChampionSpellLevels["W"];
                        btnQ.BackColor = DefaultBackColor;
                        btnE.BackColor = DefaultBackColor;
                        btnR.BackColor = DefaultBackColor;
                        break;
                    case Constants.SummaryType.EStats:
                        data.ChampionSpell = champ.Spells[2];
                        data.ChampionSpellLevel = data.ChampionSpellLevels["E"];
                        btnQ.BackColor = DefaultBackColor;
                        btnW.BackColor = DefaultBackColor;
                        btnR.BackColor = DefaultBackColor;
                        break;
                    case Constants.SummaryType.RStats:
                        data.ChampionSpell = champ.Spells[3];
                        data.ChampionSpellLevel = data.ChampionSpellLevels["R"];
                        btnQ.BackColor = DefaultBackColor;
                        btnW.BackColor = DefaultBackColor;
                        btnE.BackColor = DefaultBackColor;
                        break;
                    default:
                        break;
                }

                webStats.DocumentText = data.GetSpellStrings();
            }

        }

        private void cbxChampions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxChampions.SelectedIndex != 0)
            {
                cbxLevels.Enabled = true;
                string championKey = cbxChampions.SelectedValue.ToString();

                try
                {
                    data.Champion = data.ChampionData.Where(c => c.Key == championKey).First();
                    SetBasicStatsSummary();
                    SetSpellSummary();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                cbxLevels.Enabled = false;
            }
        }

        private void cbxLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.ChampionLevel = (int)cbxLevels.SelectedValue;

            if (data.Champion != null)
                SetBasicStatsSummary();

            LoadSpellLevelDropdown(); // reset spell levels every time the champ level changes
        }

        private void btnChamp_Click(object sender, EventArgs e)
        {
            if (data.Champion != null)
                SetBasicStatsSummary();
        }

        #endregion

        #region SPELL & SPELL CHANGING EVENTS

        private void SetSpellLevel(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;

            if ((int)cbx.SelectedValue > 0)
            {
                int totalSpellLevels = (int)cbxQLevel.SelectedValue + (int)cbxWLevel.SelectedValue + (int)cbxELevel.SelectedValue + (int)cbxRLevel.SelectedValue;

                if (totalSpellLevels > (int)cbxLevels.SelectedValue)
                {
                    MessageBox.Show("Champion spell levels cannot be greater than champion level!");

                    // make sure spell level reverts to old value
                    switch (cbx.Name)
                    {
                        case "cbxQLevel":
                            cbx.SelectedItem = data.ChampionSpellLevels["Q"];
                            break;
                        case "cbxWLevel":
                            cbx.SelectedItem = data.ChampionSpellLevels["W"];
                            break;
                        case "cbxELevel":
                            cbx.SelectedItem = data.ChampionSpellLevels["E"];
                            break;
                        case "cbxRLevel":
                            cbx.SelectedItem = data.ChampionSpellLevels["R"];
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (cbx.Name)
                    {
                        case "cbxQLevel":
                            data.ChampionSpellLevels["Q"] = (int)cbx.SelectedValue;
                            data.ChampionSummaryType = Constants.SummaryType.QStats;
                            break;
                        case "cbxWLevel":
                            data.ChampionSpellLevels["W"] = (int)cbx.SelectedValue;
                            data.ChampionSummaryType = Constants.SummaryType.WStats;
                            break;
                        case "cbxELevel":
                            data.ChampionSpellLevels["E"] = (int)cbx.SelectedValue;
                            data.ChampionSummaryType = Constants.SummaryType.EStats;
                            break;
                        case "cbxRLevel":
                            data.ChampionSpellLevels["R"] = (int)cbx.SelectedValue;
                            data.ChampionSummaryType = Constants.SummaryType.RStats;
                            break;
                        default:
                            break;
                    }

                    SetSpellSummary();
                }
            }
        }

        private void btnPassive_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet!");
        }

        private void btnQ_Click(object sender, EventArgs e)
        {
            if ((int)cbxQLevel.SelectedValue < 1)
            {
                cbxQLevel.SelectedItem = 1;
            }

            if (cbxChampions.SelectedIndex != 0 && data.Champion != null)
            {
                data.ChampionSummaryType = Constants.SummaryType.QStats;
                btnQ.BackColor = Color.Red;
                SetSpellSummary();
            }
        }

        private void btnW_Click(object sender, EventArgs e)
        {
            if ((int)cbxWLevel.SelectedValue < 1)
            {
                cbxWLevel.SelectedItem = 1;
            }

            if (cbxChampions.SelectedIndex != 0 && data.Champion != null)
            {
                data.ChampionSummaryType = Constants.SummaryType.WStats;
                btnW.BackColor = Color.Red;
                SetSpellSummary();
            }
        }

        private void btnE_Click(object sender, EventArgs e)
        {
            if ((int)cbxELevel.SelectedValue < 1)
            {
                cbxELevel.SelectedItem = 1;
            }

            if (cbxChampions.SelectedIndex != 0 && data.Champion != null)
            {
                data.ChampionSummaryType = Constants.SummaryType.EStats;
                btnE.BackColor = Color.Red;
                SetSpellSummary();
            }
        }

        private void btnR_Click(object sender, EventArgs e)
        {
            if ((int)cbxRLevel.SelectedValue < 1)
            {
                cbxRLevel.SelectedItem = 1;
            }

            if (cbxChampions.SelectedIndex != 0 && data.Champion != null)
            {
                data.ChampionSummaryType = Constants.SummaryType.RStats;
                btnR.BackColor = Color.Red;
                SetSpellSummary();
            }
        }

        #endregion

        #region HELPER METHODS

        private double CalculateLevelStat(double baseStat, double perLevelStat)
        {
            return baseStat + (perLevelStat * (data.ChampionLevel - 1));
        }

        private string ParseSpellText(ChampionSpellStatic champSpell)
        {
            string spellText = champSpell.Tooltip;

            //List<int> startBrackets = spellText.F

            return "";
        }

        #endregion

        #region UNUSED, TRASH

        #endregion

        

        void RefreshButtons(object sender, EventArgs e)
        {
            btnQ.BackColor = Color.Gray;
            btnW.BackColor = Color.Gray;
            btnE.BackColor = Color.Gray;
            btnR.BackColor = Color.Gray;
        }


    }
}
