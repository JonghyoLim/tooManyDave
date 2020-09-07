using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TooManyDaves
{
    public partial class FormInfoGraphic : Form
    {

        McCaveFamily family = new McCaveFamily();
        string uploadString = "";

        public FormInfoGraphic()
        {
            InitializeComponent();
            family.UpLoad(ref uploadString);


            Dictionary<string, int> HobbieList = family.GatherChartDataForHobbies();

            foreach (KeyValuePair<string, int> k in HobbieList)
                this.chart1.Series["Hobbies"].Points.AddXY(k.Key,k.Value);

            Dictionary<string, int> EductationList = family.GatherEductionChartData();
            foreach (KeyValuePair<string, int> k in EductationList)
                this.chart2.Series["Eduction Level"].Points.AddXY(k.Key, k.Value);

            Dictionary<string, int> AgeList = family.GatherDateForAge();
            foreach (KeyValuePair<string, int> k in AgeList)
                this.chart3.Series["Children"].Points.AddXY(k.Key, k.Value);

            Dictionary<string, int> BirthGroup = family.GatherDataForBirthGroupings();
            foreach (KeyValuePair<string, int> kvp in BirthGroup)
                this.chart4.Series["BirthGroups"].Points.AddXY(kvp.Key, kvp.Value);
        }

        private void btnExit_Close(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}