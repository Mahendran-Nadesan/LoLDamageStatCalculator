using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoLDamageStatCalculator
{
    public partial class Champions : Form
    {
        public Champions()
        {
            InitializeComponent();
        }

        private void Champions_Load(object sender, EventArgs e)
        {
            MainInstance.ChampionsForm = this;

            // load champion data
            if (!MainInstance.Data.DataLoaded("Champion"))
            {
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

            // todo: check for folder with images, then check for each champ image
            // download images
            // todo: progress bar/loading form
            string currFolder = System.IO.Directory.GetCurrentDirectory();

            if (System.IO.Directory.Exists(System.IO.Path.Combine(currFolder, "img")))
            {

            }
            else
            {
                string imgFolder = System.IO.Directory.CreateDirectory(System.IO.Path.Combine(currFolder, "img")).FullName;
                

                using (WebClient client = new WebClient())
                {
                    foreach (var champion in MainInstance.Data.ChampionData)
                    {
                        string champName = champion.Image.Full;
                        string group = champion.Image.Group;
                        string imageUrl = Constants.StaticDataURL + Constants.StaticChampionVersion + "/img/" + group + "/" + champName;

                        client.DownloadFile(new Uri(imageUrl), System.IO.Path.Combine(imgFolder, champName));

                    }
                }

                
            }
                

            
        }

        private void Champions_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainInstance.MainMenu.Show();
        }

        private async void LoadInitialState()
        {
            MainInstance.Data = new Models
            {
                ChampionSummaryType = Constants.SummaryType.PassiveStats
            };

            // async stuff
            await GetChampionData();

          

           


            
        }

        private async Task GetChampionData() // marked as Task to ensure we wait on it
        {
            var rawChampionData = await MainInstance.API.GetChampions();
            MainInstance.Data.ChampionData = rawChampionData.Champions.Select(c => c.Value).ToList();
        }



    }
}
