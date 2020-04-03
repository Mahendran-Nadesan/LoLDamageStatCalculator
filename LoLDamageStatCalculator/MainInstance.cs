using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoLDamageStatCalculator
{
    static class MainInstance
    {
        public static bool IsOnline { get; set; }

        public static IMode API { get; set; }

        public static Models Data { get; set; }

        public static Form MainMenu { get; set; }

        public static Form ChampionsForm { get; set; }

        public static Form ItemsForm { get; set; }

        public static Form RunesForm { get; set; }

        public static Form ChampionBuilderForm { get; set; }
    }
}
