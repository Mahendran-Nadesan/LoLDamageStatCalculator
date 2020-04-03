using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoLDamageStatCalculator
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        #region LOADING

        private void MainMenu_Load(object sender, EventArgs e)
        {
            // set to online mode on start for now, later add a setting to load last mode
            rbOffline.Enabled = false;
            rbOnline.Checked = true;
            MainInstance.MainMenu = this;
            SetOnlineMode(true);

            // not sure if we should pre-load data, but here goes

            // initial loading - include pre-loading of champion, item, and rune data
            try
            {
                LoadInitialState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetOnlineMode(bool isOnline = true)
        {
            if (isOnline)
            {
                MainInstance.IsOnline = true;
                MainInstance.API = new OnlineMode();
            }
            else
            {
                MainInstance.IsOnline = false;
                MainInstance.API = new OfflineMode();
            }
        }

        private async void LoadInitialState()
        {
            MainInstance.Data = new Models
            {
                ChampionSummaryType = Constants.SummaryType.PassiveStats
            };

            // async stuff
            // todo: item data, runes data
            await GetChampionData();
        }

        #endregion

        private void btnChampions_Click(object sender, EventArgs e)
        {
            if (MainInstance.Data.DataLoaded("Champion"))
            {
                Form cForm = new Champions();
                cForm.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Data still being fetched");
            }
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            if (MainInstance.Data.DataLoaded("Item"))
            {
                Form cForm = new Items();
                cForm.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Data still being fetched");
            }
        }

        private void btnRunes_Click(object sender, EventArgs e)
        {
            if (MainInstance.Data.DataLoaded("Rune"))
            {
                Form cForm = new Runes();
                cForm.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Data still being fetched");
            }
        }

        private void btnChampionBuilder_Click(object sender, EventArgs e)
        {
            Form cbForm = new Main();
            cbForm.Show();
            this.Hide();
        }

        private async Task GetChampionData() // marked as Task to ensure we wait on it
        {
            var rawChampionData = await MainInstance.API.GetChampions();
            MainInstance.Data.ChampionData = rawChampionData.Champions.Select(c => c.Value).ToList();
        }

        
    }
}
