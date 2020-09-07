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
    public partial class Form1 : Form
    {

        McCaveFamily family = new McCaveFamily();
        bool displayDaveEntry = false;
        bool displayYearPlan = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

            string txtOut = ""; // Set an empty string for display on form.
            family.UpLoad(ref txtOut); // Load list into family.
            rtxt1.Text = txtOut;
        }

        private void btnUpcomingBirthdays_Click(object sender, EventArgs e)
        {
            string txtOut = "";
            bool found = false;

            if (displayDaveEntry)
                ToggleDaveDisplayEntry();
            if (displayYearPlan)
                TogglePlanSchoolYear();

            if (family.ContainsData())
            {
                family.PrintUpcomingBirthdays(ref txtOut, ref found);

                switch(found)
                {
                    case true:
                        rtxt1.Text = txtOut;
                        break;
                    case false:
                        txtOut = "There are currently no upcoming birthdays for \nthe next seven days.";
                        break;
                }
            }
            else
                txtOut = "There is currently no data loaded, please upload data to continue.";

            rtxt1.Text = txtOut;
        }

        private void btnListByAge_Click(object sender, EventArgs e)
        {

            if (displayDaveEntry)
                ToggleDaveDisplayEntry();
            if (displayYearPlan)
                TogglePlanSchoolYear();

            string txtOut = "";

            if(family.ContainsData())
            {
                family.PrintByAge(ref txtOut);
            }
            else
                txtOut = "There is currently no data loaded, please upload data to continue.";

            rtxt1.Text = txtOut;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnListAlpabetically_Click(object sender, EventArgs e)
        {
            if (displayDaveEntry)
                ToggleDaveDisplayEntry();
            if (displayYearPlan)
                TogglePlanSchoolYear();


            string txtOut = "";

            if (family.ContainsData())
            {
                family.PrintByNickname(ref txtOut);
            }
            else
                txtOut = "There is currently no data loaded, please upload data to continue.";

            rtxt1.Text = txtOut;
        }

        private void btnListMultipleBirths_Click(object sender, EventArgs e)
        {
            if (displayDaveEntry)
                ToggleDaveDisplayEntry();
            if (displayYearPlan)
                TogglePlanSchoolYear();


            string txtOut = "";

            if (family.ContainsData())
            {
                family.PrintMultipleBirths(ref txtOut);
            }
            else
                txtOut = "There is currently no data loaded, please upload data to continue.";

            rtxt1.Text = txtOut;
        }

        private void btnGenBabyName_Click(object sender, EventArgs e)
        {
            if (displayDaveEntry)
                ToggleDaveDisplayEntry();
            if (displayYearPlan)
                TogglePlanSchoolYear();

            string txtOut = "";

            if (family.ContainsData())
            {
                family.NameTheBaby(ref txtOut);
            }
            else
                txtOut = "There is currently no data loaded, please upload data to continue.";

            rtxt1.Text = txtOut;
        }

        private void btnCalcAllowMonth_Click(object sender, EventArgs e)
        {
            if (displayDaveEntry)
                ToggleDaveDisplayEntry();
            if (displayYearPlan)
                TogglePlanSchoolYear();

            string txtOut = "";

            if (family.ContainsData())
            {
                family.ChildrensAllowenceForMonth(ref txtOut);
            }
            else
                txtOut = "There is currently no data loaded, please upload data to continue.";

            rtxt1.Text = txtOut;
        }

        private void btnCalcAllowYear_Click(object sender, EventArgs e)
        {
            if (displayDaveEntry)
                ToggleDaveDisplayEntry();
            if (displayYearPlan)
                TogglePlanSchoolYear();

            string txtOut = "";

            if (family.ContainsData())
            {
                family.ChildrensAllowenceForYear(ref txtOut);
            }
            else
                txtOut = "There is currently no data loaded, please upload data to continue.";

            rtxt1.Text = txtOut;
        }

        private void btnAddNewChild_Click(object sender, EventArgs e)
        {
            string txtOut = "";

            if (family.ContainsData())
            {
                if (displayYearPlan)
                    TogglePlanSchoolYear();

                ToggleDaveDisplayEntry();
                txtOut += "";
            }
            else
                txtOut = "There is currently no data loaded, please upload data to continue.";

            //rtxt1.Text = txtOut;
        }

        private void ToggleDaveDisplayEntry()
        {
            switch(displayDaveEntry)
            {
                case false:
                    lblDaveName.Visible = true;
                    lblDaveDOB.Visible = true;
                    lblDaveDesc.Visible = true;

                    txtName.Visible = true;
                    txtDOB.Visible = true;
                    txtDESC.Visible = true;

                    btnClearFields.Visible = true;
                    btnGenName.Visible = true;
                    btnUseCurrentDate.Visible = true;
                    btnAddDave.Visible = true;
                    displayDaveEntry = true;
                    break;
                case true:
                    lblDaveName.Visible = false;
                    lblDaveDOB.Visible = false;
                    lblDaveDesc.Visible = false;

                    txtName.Visible = false;
                    txtDOB.Visible = false;
                    txtDESC.Visible = false;

                    btnClearFields.Visible = false;
                    btnGenName.Visible = false;
                    btnUseCurrentDate.Visible = false;
                    btnAddDave.Visible = false;
                    displayDaveEntry = false;
                    break;
            }
        }

        private void TogglePlanSchoolYear()
        {
            switch(displayYearPlan)
            {
                case false:
                    lblSchoolPlan.Visible = true;

                    txtSchoolPlan.Visible = true;

                    btnPlanYearDate.Visible = true;
                    btnPlanYear.Visible = true;

                    displayYearPlan = true;
                    break;
                case true:
                    lblSchoolPlan.Visible = false;

                    txtSchoolPlan.Visible = false;

                    btnPlanYearDate.Visible = false;
                    btnPlanYear.Visible = false;

                    displayYearPlan = false;
                    break;
            }
        }
        

        private void btnClearFields_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtDOB.Text = "";
            txtDESC.Text = "";
        }

        private void btnGenName_Click(object sender, EventArgs e)
        {
            string txtOut = "";
            family.NameTheBaby(ref txtOut);

            txtName.Text = txtOut;
        }

        private void btnUseCurrentDate_Click(object sender, EventArgs e)
        {
            txtDOB.Text = DateTime.Now.ToShortDateString();
        }

        private void btnAddDave_Click(object sender, EventArgs e)
        {
            Dave d = new Dave(txtName.Text, txtDOB.Text, txtDESC.Text);
            if (txtName.Text != "" && txtDOB.Text != "" && txtDESC.Text != "")
            {
                family.AddDave(d);
                rtxt1.Text = "Successfully added " + d.GetNickname() + " to the family.";
                family.WriteToPersistantStorage();
                
            }
            else
                rtxt1.Text = "Please make sure to add accurate date to the fields.";
        }

        private void btnPlanSchool_Click(object sender, EventArgs e)
        {
            string txtOut = "";
            if (displayDaveEntry)
                ToggleDaveDisplayEntry();
            

            if (family.ContainsData())
            {
                TogglePlanSchoolYear();
            }
            else
                txtOut = "There is currently no data loaded, please upload data to continue.";

            rtxt1.Text = txtOut;
        }

        private void btnPlanYearDate_Click(object sender, EventArgs e)
        {
            txtSchoolPlan.Text = DateTime.Now.ToShortDateString();
        }

        private void btnPlanYear_Click(object sender, EventArgs e)
        {
            string txtOut = "";

            if (family.ContainsData())
            {
                if(txtSchoolPlan.Text != "")
                {
                    try
                    {
                        DateTime txtTemp = Convert.ToDateTime(txtSchoolPlan.Text);
                        family.PlanSchoolTimes(ref txtOut, txtTemp.ToShortDateString());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Please enter only valid Dates in the TextBox\nUse the format Day/Month/Year");
                    }
                 }
            }
            else
                txtOut = "There is currently no data loaded, please upload data to continue.";

            rtxt1.Text = txtOut;
        }

        private void btnDisplayInfoGraphic_Click(object sender, EventArgs e)
        {
            string txtOut = "";
            if (displayDaveEntry)
                ToggleDaveDisplayEntry();
            if (displayYearPlan)
                TogglePlanSchoolYear();

            if (family.ContainsData())
                new FormInfoGraphic().Show();
            else
            {
                txtOut = "You must Upload Data to this program before data can be displayed in an info graphic.";
                rtxt1.Text = txtOut;
            }
        }
    }
}