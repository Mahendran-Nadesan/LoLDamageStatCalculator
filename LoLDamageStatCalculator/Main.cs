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

namespace LoLDamageStatCalculator
{
    public partial class Main : Form
    {
        RiotSharpAPI _api = null;

        public Main()
        {
            InitializeComponent();
        }

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
            if (_api == null)
            {
                MessageBox.Show("No key to work with");
            }
            else
            {
                List<string> champs = await _api.GetChampionList();
                cbxChampions.DataSource = champs;
            }
        }
    }
}
