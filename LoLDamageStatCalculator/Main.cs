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

        RiotSharpAPI _api = null;
        IMode api;
        public Models data;

        #endregion

        public Main()
        {
            InitializeComponent();

            // set to online mode on start for now, later add a setting to load last mode
            rbOffline.Enabled = false;
            rbOnline.Checked = true;
            SetOnlineMode(true);

            // initial loading
            try
            {
                LoadInitialState();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

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

        private async void LoadInitialState()
        {
            data = new Models
            {
                ChampionSummaryType = Constants.SummaryType.PassiveStats
            };
            await GetChampionData();
            LoadChampionDropdown();
            LoadLevelDropdown();

            //btnQ.Click += RefreshButtons;
            //btnW.Click += RefreshButtons;
            //btnE.Click += RefreshButtons;
            //btnR.Click += RefreshButtons;

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
            txtSpellSummary.Clear();
            Constants.SummaryType summaryType = data.ChampionSummaryType;

            var champ = data.Champion;

            // the button color code is shit
            switch (summaryType)
            {
                case Constants.SummaryType.QStats:
                    txtSpellSummary.Text += string.Format("{0} Q: {1}", champ.Name, champ.Spells[0].Name);
                    txtSpellSummary.Text += Environment.NewLine;
                    txtSpellSummary.Text += champ.Spells[0].Description;
                    btnW.BackColor = DefaultBackColor;
                    btnE.BackColor = DefaultBackColor;
                    btnR.BackColor = DefaultBackColor;
                    break;
                case Constants.SummaryType.WStats:
                    txtSpellSummary.Text += string.Format("{0} W: {1}", champ.Name, champ.Spells[1].Name);
                    txtSpellSummary.Text += Environment.NewLine;
                    txtSpellSummary.Text += champ.Spells[1].Description;
                    btnQ.BackColor = DefaultBackColor;
                    btnE.BackColor = DefaultBackColor;
                    btnR.BackColor = DefaultBackColor;
                    break;
                case Constants.SummaryType.EStats:
                    txtSpellSummary.Text += string.Format("{0} E: {1}", champ.Name, champ.Spells[2].Name);
                    txtSpellSummary.Text += Environment.NewLine;
                    txtSpellSummary.Text += champ.Spells[2].Description;
                    btnQ.BackColor = DefaultBackColor;
                    btnW.BackColor = DefaultBackColor;
                    btnR.BackColor = DefaultBackColor;
                    break;
                case Constants.SummaryType.RStats:
                    txtSpellSummary.Text += string.Format("{0} R: {1}", champ.Name, champ.Spells[3].Name);
                    txtSpellSummary.Text += Environment.NewLine;
                    txtSpellSummary.Text += champ.Spells[3].Description;
                    btnQ.BackColor = DefaultBackColor;
                    btnW.BackColor = DefaultBackColor;
                    btnE.BackColor = DefaultBackColor;
                    break;
                default:
                    break;
            }
        }

        private void cbxChampions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxChampions.SelectedIndex != 0)
            {
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
        }

        private void cbxLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.ChampionLevel = (int)cbxLevels.SelectedValue;

            if (data.Champion != null)
                SetBasicStatsSummary();
        }

        private void btnChamp_Click(object sender, EventArgs e)
        {
            if (data.Champion != null)
                SetBasicStatsSummary();
        }

        #endregion

        #region HELPER METHODS

        private double CalculateLevelStat(double baseStat, double perLevelStat)
        {
            return baseStat + (perLevelStat * (data.ChampionLevel - 1));
        }

        #endregion

        #region UNUSED, TRASH

        #endregion

        private void btnPassive_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet!");
        }

        private void btnQ_Click(object sender, EventArgs e)
        {
            if (cbxChampions.SelectedIndex != 0 && data.Champion != null)
            {
                data.ChampionSummaryType = Constants.SummaryType.QStats;
                btnQ.BackColor = Color.Red;
                SetSpellSummary();
            }
        }

        private void btnW_Click(object sender, EventArgs e)
        {
            if (cbxChampions.SelectedIndex != 0 && data.Champion != null)
            {
                data.ChampionSummaryType = Constants.SummaryType.WStats;
                btnW.BackColor = Color.Red;
                SetSpellSummary();
            }
        }

        private void btnE_Click(object sender, EventArgs e)
        {
            if (cbxChampions.SelectedIndex != 0 && data.Champion != null)
            {
                data.ChampionSummaryType = Constants.SummaryType.EStats;
                btnE.BackColor = Color.Red;
                SetSpellSummary();
            }
        }

        private void btnR_Click(object sender, EventArgs e)
        {
            if (cbxChampions.SelectedIndex != 0 && data.Champion != null)
            {
                data.ChampionSummaryType = Constants.SummaryType.RStats;
                btnR.BackColor = Color.Red;
                SetSpellSummary();
            }
        }

        void RefreshButtons(object sender, EventArgs e)
        {
            btnQ.BackColor = Color.Gray;
            btnW.BackColor = Color.Gray;
            btnE.BackColor = Color.Gray;
            btnR.BackColor = Color.Gray;
        }
    
    }
}
