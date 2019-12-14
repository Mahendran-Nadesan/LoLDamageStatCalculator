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

namespace LoLDamageStatCalculator
{
    public partial class Main : Form
    {
        // wherever possible, pass/use data objects/fields, not the form control's value - changes to a form control's value should update a data object/field
        #region PERSISTENT OBJECTS

        RiotSharpAPI _api = null;
        IMode api;
        public Models data;

        #endregion

        public Main()
        {
            InitializeComponent();

            // temp - while we are not sure if we need the api key
            txtAPIKey.Enabled = false;
            btnSubmitAPIKey.Enabled = false;
            btnGetData.Enabled = false;

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

        private void LoadInitialState()
        {
            data = new Models();
            LoadChampionDropdown();
            LoadLevelDropdown();
        }

        private async void LoadChampionDropdown()
        {
            data.ChampionsViewable = await api.GetViewableChampionList();

            if (data.ChampionsViewable.Count > 0)
            {
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
        private void SetSummary(Constants.SummaryType summaryType)
        {
            txtSummary.Clear();

            switch (summaryType)
            {
                case Constants.SummaryType.BaseStats:
                    var stats = data.Champion.Stats;
                    txtSummary.Text += string.Format("Base Stats at level {0}" + Environment.NewLine, data.ChampionLevel);
                    txtSummary.Text += string.Format("HP: {0}" + Environment.NewLine, CalculateLevelStat(stats.Hp, stats.HpPerLevel));
                    txtSummary.Text += string.Format("Resource: {0}" + Environment.NewLine, CalculateLevelStat(stats.Mp, stats.MpPerLevel));
                    txtSummary.Text += string.Format("AD: {0}" + Environment.NewLine, CalculateLevelStat(stats.AttackDamage, stats.AttackDamagePerLevel));
                    txtSummary.Text += string.Format("ARM: {0}" + Environment.NewLine, CalculateLevelStat(stats.Armor, stats.ArmorPerLevel));
                    txtSummary.Text += string.Format("MR: {0}" + Environment.NewLine, CalculateLevelStat(stats.SpellBlock, stats.SpellBlockPerLevel));
                    txtSummary.Text += string.Format("MS: {0}" + Environment.NewLine, CalculateLevelStat(stats.MoveSpeed, 0));
                    break;
                case Constants.SummaryType.SpellStats:
                    break;
                default:
                    break;
            }
        }

        private async void cbxChampions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxChampions.SelectedIndex != 0)
            {
                string championName = cbxChampions.SelectedValue.ToString();
                btnChamp.Text = ((DComboBox)cbxChampions.SelectedItem).Name;

                try
                {
                    data.Champion = await api.GetChampion(championName);
                    SetSummary(Constants.SummaryType.BaseStats);
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
                SetSummary(Constants.SummaryType.BaseStats);
        }
        
        private void btnChamp_Click(object sender, EventArgs e)
        {
            if (data.Champion != null)
                SetSummary(Constants.SummaryType.BaseStats);
        }

        #endregion

        #region HELPER METHODS

        private double CalculateLevelStat(double baseStat, double perLevelStat)
        {
            return baseStat + (perLevelStat * (data.ChampionLevel - 1));
        }

        #endregion
        
        #region UNUSED, TRASH

        private void btnSubmitAPIKey_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAPIKey.Text))
            {
                _api = new RiotSharpAPI(txtAPIKey.Text);
            }
            else
            {
                MessageBox.Show("Please enter a valid key");
            }
        }

        private async void btnGetData_Click(object sender, EventArgs e)
        {


        }


        #endregion

        private void btnPassive_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet!");
        }

        private void btnQ_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This one as well...");
        }

        private void btnW_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Are you going to try all the buttons?");
        }

        private void btnE_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Stop touching me!!");
        }

        private void btnR_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fun's over, bugger off!");
        }
    }
}
