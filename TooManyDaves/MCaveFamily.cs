using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooManyDaves
{
    public class McCaveFamily : ICloneable
    {
        
        private int numDaves { get; set; } // number of Dave objects in DaveList<Dave>.
        private List<Dave> DavesList; // Generic collection of Daves.


        //
        // Constructor: Initialises the structure to an empty list with zero Members. 
        public McCaveFamily()
        {
            this.numDaves = 0;
            this.DavesList = new List<Dave>();
        }


        //
        // Method will add a 'Dave' to the DaveList: member for 
        // recording numDaves will only increment when reading from file.
        public void AddDave(Dave d)
        {
            this.DavesList.Add(d);
        }


        //
        // Method will check if data has been loaded into the List.
        public bool ContainsData()
        {
            return (numDaves > 0) ? true : false;
        }


        //
        // Method will write the Contents of the Daves List to
        // Text File'Daves.txt'
        public void WriteToPersistantStorage()
        {
            
            StreamWriter sr = new StreamWriter("Daves.txt") ;
            
             foreach (Dave d in DavesList)
             {
                    sr.WriteLine(d.Print());
             }

            sr.Close();
        }


        //
        // Method will read data from Daves.txt File, line by line &
        // insert these values into the DavesList as Dave objects
        // & return a string for display purposes.
        public void UpLoad(ref string s)
        {
            if (this.numDaves == 0) // If empty.
            {
                String[] strArr = new String[4]; // initialise a string array.
                List<String> input = new List<string>();

                StreamReader sr = new StreamReader("Daves.txt"); // Open a stream to read Daves.txt

                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    strArr = line.Split(',');
                    Dave temp = new Dave(strArr[0], strArr[1], strArr[2]);
                    this.AddDave(temp);
                    this.numDaves++;
                    strArr = null;
                }

                 s = "Upload Successful.";
                 sr.Close();


             }
             else
                s = "File has already been Uploaded.";
            
            
        }


        //
        // Method will scan DavesList for multiple Births & append
        // txtOut with members of any/all multiple births.
        public void PrintMultipleBirths(ref string txtOut)
        {
            this.OrganiseBirths(); // Sort List By D.O.B.

            // Set a temp List
            List<Dave> tempList = this.Clone() as List<Dave>;//new List<Dave>();

            //foreach (Dave d in DavesList)
                //tempList.Add(new Dave(d.GetNickname(), d.GetDOB().ToString(), d.GetNickname()));



            // Outer foreach Loop will check every Dave in List with The 
            // Inner foreach Loop.
            foreach (Dave d in tempList)
            {
                int birth = 1;

                foreach (Dave s in tempList)
                {
                    if (d.GetDOB() == s.GetDOB() && d.GetNickname() != s.GetNickname())
                    {
                        txtOut += "" + s.GetNickname() + ",";
                        s.SetNickname(d.GetNickname());
                        birth++;
                    }
                }
                if (birth > 1)
                {
                    txtOut += "" + d.GetNickname() + ": ";
                    switch (birth)
                    {
                        case 2:
                            txtOut += "Twins\n\n";
                            break;
                        case 3:
                            txtOut += "Triplets\n\n";
                            break;
                        case 4:
                            txtOut += "Quadruplets\n\n";
                            break;
                        case 5:
                            txtOut += "Quinetruplets\n\n";
                            break;
                        default:
                            txtOut += "Too Many Daves.\n\n";
                            break;
                    }
                }
            }
        }





        public string GetYearFor(string s)
        {
            foreach(Dave d in DavesList)
            {
                if (d.GetNickname() == s)
                {
                    DateTime time = DateTime.Now;
                    int i = d.GetAge(time);
                    return i.ToString();
                }
            }
            return "sad";
        }











        //
        // Calculate Childrens Allowance for Month
        public void ChildrensAllowenceForMonth(ref string txtOut)
        {
            OrganiseBirths();

            // Set a temp List
            List<Dave> tempList = new List<Dave>();
            DateTime today = DateTime.Now;


            foreach (Dave d in DavesList)
                if(d.Qulaifies(today))
                    tempList.Add(new Dave(d.GetNickname(), d.GetDOB().ToString(), d.GetDescription()));

            string tempString = "";


            int singles = 0;
            int twins = 0;
            int multiples = 0;


            // Outer foreach Loop will check every Dave in List with The 
            // Inner foreach Loop.
            foreach (Dave dv in tempList)
            {
                int birth = 1;

                if (dv.GetNickname() != "CHECKED")
                {
                    foreach (Dave s in tempList)
                    {
                        if (dv.GetDOB() == s.GetDOB() && dv.GetNickname() != s.GetNickname() && (s.GetNickname() != "CHECKED") && (dv.GetNickname() != "CHECKED"))
                        {
                            tempString += "" + s.GetNickname() + ",";
                            s.SetNickname("CHECKED");
                            birth++;
                        }
                    }
                    if (birth > 1)
                    {
                        tempString += "" + dv.GetNickname() + ": ";
                        switch (birth)
                        {
                            case 2:
                                tempString += "Twins: " + ((140 * 1.5) * birth).ToString("c2") + "\n\n";
                                twins += birth;
                                break;
                            case 3:
                                tempString += "Triplets: " + ((140 * 2) * birth).ToString("c2") + "\n\n";
                                multiples += birth;
                                break;
                            case 4:
                                tempString += "Quadruplets: " + ((140 * 2) * birth).ToString("c2") + "\n\n";
                                multiples += birth;
                                break;
                            case 5:
                                tempString += "Quinetruplets: " + ((140 * 2) * birth).ToString("c2") + "\n\n";
                                multiples += birth;
                                break;
                            default:
                                tempString += "Too Many Daves: " + ((140 * 2) * birth).ToString("c2") + "\n\n";
                                multiples += birth;
                                break;
                        }
                    }
                    else
                    {
                        if (dv.GetNickname() != "CHECKED")
                        {
                            tempString += "" + dv.GetNickname() + " Single : " + (140).ToString("c2") + "\n\n";
                            singles++;
                        }
                    }
                }


            }
            tempString += "+----------------------------------------+\n   :Final Figures:\n+----------------------------------------+\n";
            tempString += "Total: "+((140 * singles)  +  ((140 * 1.5) * twins) +  ((140 * 2) * multiples)).ToString("c2")+"\n\nTotal Sigles: "+singles.ToString() +", twins : "+ twins.ToString() 
                + " multiples : " + multiples.ToString() +"";
            txtOut = tempString;
        }








        //
        // Calculate Childrens Allowance for Year
        public void ChildrensAllowenceForYear(ref string txtOut)
        {
            OrganiseBirths();

            // Set a temp List
            List<Dave> tempList = new List<Dave>();
            DateTime today = DateTime.Now;


            foreach (Dave d in DavesList)
                if (d.Qulaifies(today))
                    tempList.Add(new Dave(d.GetNickname(), d.GetDOB().ToString(), d.GetDescription()));

            string tempString = "";


            int singles = 0;
            int twins = 0;
            int multiples = 0;


            // Outer foreach Loop will check every Dave in List with The 
            // Inner foreach Loop.
            foreach (Dave dv in tempList)
            {
                int birth = 1;

                if (dv.GetNickname() != "CHECKED")
                {
                    foreach (Dave s in tempList)
                    {
                        if (dv.GetDOB() == s.GetDOB() && dv.GetNickname() != s.GetNickname() && (s.GetNickname() != "CHECKED") && (dv.GetNickname() != "CHECKED"))
                        {
                            tempString += "" + s.GetNickname() + ",";
                            s.SetNickname("CHECKED");
                            birth++;
                        }
                    }
                    if (birth > 1)
                    {
                        tempString += "" + dv.GetNickname() + ": ";
                        switch (birth)
                        {
                            case 2:
                                tempString += "Twins: " + (((140 * 1.5) * birth)*12).ToString("c2") + "\n\n";
                                twins += birth;
                                break;
                            case 3:
                                tempString += "Triplets: " + (((140 * 2) * birth)*12).ToString("c2") + "\n\n";
                                multiples += birth;
                                break;
                            case 4:
                                tempString += "Quadruplets: " + (((140 * 2) * birth) * 12).ToString("c2") + "\n\n";
                                multiples += birth;
                                break;
                            case 5:
                                tempString += "Quinetruplets: " + (((140 * 2) * birth) * 12).ToString("c2") + "\n\n";
                                multiples += birth;
                                break;
                            default:
                                tempString += "Too Many Daves: " + (((140 * 2) * birth) * 12).ToString("c2") + "\n\n";
                                multiples += birth;
                                break;
                        }
                    }
                    else
                    {
                        if (dv.GetNickname() != "CHECKED")
                        {
                            tempString += "" + dv.GetNickname() + " Single : " + (140 * 12).ToString("c2") + "\n\n";
                            singles++;
                        }
                    }
                }


            }
            tempString += "+----------------------------------------+\n   :Final Figures:\n+----------------------------------------+\n";
            tempString += "Total: " + (((140 * singles) + ((140 * 1.5) * twins) + ((140 * 2) * multiples)) * 12).ToString("c2") + "\n\nTotal Sigles: " + singles.ToString() + ", twins : " + twins.ToString()
                + " multiples : " + multiples.ToString() + "";
            txtOut = tempString;
        }







        // 
        // Planning school times.
        public void PlanSchoolTimes(ref string txtOut, string strDate)
        {
            DateTime date = Convert.ToDateTime(strDate);

            int preSchoolers = 0;
            int primarySchoolers = 0;
            int secondarySchoolers = 0;
            int thirdLevel = 0;
            int extras = 0;

            foreach(Dave d in DavesList)
            {
                if (d.GetSchoolAge(date) < 23 && d.GetSchoolAge(date) > 0)
                {
                    if (d.GetSchoolAge(date) < 5 && d.GetSchoolAge(date) > 0)
                        preSchoolers++;
                    else if (d.GetSchoolAge(date) < 13)
                        primarySchoolers++;
                    else if (d.GetSchoolAge(date) < 19)
                        secondarySchoolers++;
                    else if (d.GetSchoolAge(date) < 23 && d.GetSchoolAge(date) > 0)
                        thirdLevel++;
                    else
                        extras++;
                }
            }


            txtOut += "Preschoolers : "+ preSchoolers.ToString()+"\n";
            foreach(Dave d in DavesList)
            {
                if (d.GetSchoolAge(date) < 5 && d.GetSchoolAge(date) > 0)
                    txtOut += "\t"+d.GetNickname()+", age "+d.GetSchoolAge(date).ToString()+"\n";
            }


            txtOut += "Primary schoolers : " + primarySchoolers.ToString() + "\n";
            foreach (Dave d in DavesList)
            {
                if (d.GetSchoolAge(date) < 13 && d.GetSchoolAge(date) > 0 && d.GetSchoolAge(date) >= 5)
                    txtOut += "\t" + d.GetNickname() + ", age " + d.GetSchoolAge(date).ToString() + "\n";
            }


            txtOut += "Secondary schoolers : " + secondarySchoolers.ToString() + "\n";
            foreach (Dave d in DavesList)
            {
                if (d.GetSchoolAge(date) < 19 && d.GetSchoolAge(date) > 0 && d.GetSchoolAge(date) >= 13)
                    txtOut += "\t" + d.GetNickname() + ", age " + d.GetSchoolAge(date).ToString() + "\n";
            }


            txtOut += "Third Level : " + thirdLevel.ToString() + "\n";
            foreach (Dave d in DavesList)
            {
                if (d.GetSchoolAge(date) < 23 && d.GetSchoolAge(date) > 0 && d.GetSchoolAge(date) >= 19)
                    txtOut += "\t" + d.GetNickname() + ", age " + d.GetSchoolAge(date).ToString() + "\n";
            }


        }





        public void NameTheBaby(ref string txtOut)
        {
            // Method will return a random Name.

             
            //txtOut = i.ToString();
            string name = "";
            ProcessBabyFirstname(GenerateRandom(), ref name);
            ProcessBabySecondname(GenerateRandom(), ref name);

            if (HasName(name))
            {
                txtOut = "";
                NameTheBaby(ref txtOut);
            }
            else
                txtOut = name;
            
        }

        public void ProcessBabyFirstname(int i, ref string s)
        {
            switch(i)
            {
                case 1:
                    s = "Dopey";
                    break;
                case 2:
                    s = "Sneezie";
                    break;
                case 3:
                    s = "Prance";
                    break;
                case 4:
                    s = "Sir";
                    break;
                case 5:
                    s = "Bashfull";
                    break;
                case 6:
                    s = "Roonie";
                    break;
                case 7:
                    s = "Sam";
                    break;
                case 8:
                    s = "Snoggy";
                    break;
                case 9:
                    s = "Pie Head";
                    break;
                case 10:
                    s = "McMullin";
                    break;
                case 11:
                    s = "Roudalph";
                    break;
                case 12:
                    s = "Hopper";
                    break;
                case 13:
                    s = "Trump Mc";
                    break;
                case 14:
                    s = "Jeungeun";
                    break;
                case 15:
                    s = "Matteo";
                    break;
                case 16:
                    s = "Claudia";
                    break;
                case 17:
                    s = "Gian";
                    break;
                case 18:
                    s = "Bucks";
                    break;
                case 19:
                    s = "Mauro";
                    break;
                case 20:
                    s = "Mia";
                    break;
                case 21:
                    s = "Hyokun";
                    break;
                case 22:
                    s = "Jamie";
                    break;
                case 23:
                    s = "McMullin Muc Muc";
                    break;
                case 24:
                    s = "Scooby doo bi doo";
                    break;
                case 25:
                    s = "Lovle";
                    break;
                case 26:
                    s = "Kiho";
                    break;
                case 0:
                    s = "Jason";
                    break;
            }
        }

        public void ProcessBabySecondname(int i , ref string s)
        {
            Random rnd = new Random();
            int j = rnd.Next(5);

            switch(j)
            {
                case 0:
                    s += " Van Der ";
                    break;
                case 1:
                    s += " Mc ";
                    break;
                case 2:
                    s += " Of ";
                    break;
                case 3:
                case 4:
                case 5:
                    s += " ";
                    break;
            }

            i = GenerateRandom();

            switch (i)
            {
                case 1:
                    s += "Frumpy Head";
                    break;
                case 2:
                    s += "Donald Mc Nugget";
                    break;
                case 3:
                    s += "Caltrop McGee";
                    break;
                case 4:
                    s += "Kim";
                    break;
                case 5:
                    s += "Lim";
                    break;
                case 6:
                    s += "Cho";
                    break;
                case 7:
                    s += "Choi";
                    break;
                case 8:
                    s += "Park";
                    break;
                case 9:
                    s += "Moon";
                    break;
                case 10:
                    s += "Lee";
                    break;
                case 11:
                    s += "Ryu";
                    break;
                case 12:
                    s += "Min";
                    break;
                case 13:
                    s += "Bobby";
                    break;
                case 14:
                    s += "Samson";
                    break;
                case 15:
                    s += "Der Burg";
                    break;
                case 16:
                    s += "Butterfingers";
                    break;
                case 17:
                    s += "Rowligs";
                    break;
                case 18:
                    s += "Big Nose";
                    break;
                case 19:
                    s += "Choo Choo";
                    break;
                case 20:
                    s += "Buffon";
                    break;
                case 21:
                    s += "Gatuso";
                    break;
                case 22:
                    s += "Icardi";
                    break;
                case 23:
                case 24:
                case 25:
                case 26:
                case 0:
                    s += "";
                    break;
            }
        }

        public int GenerateRandom()
        {
            Random n = new Random();
            return n.Next(25);
            
        }
        
        public bool HasName(string name)
        {
            bool found = false;
            foreach (Dave d in DavesList)
            {
                if(d.IsIn(name))
                {
                    found = true;
                    break;
                }
                  
            }
            return found;
        }







        //
        // Method Will Sort Births By Date.
        public void OrganiseBirths()
        {
            this.DavesList.Sort(new DaveCompareBirths());
        }

        
        //
        // Method will Sort DavesList By Nickname.
        public void OrganiseNicknames()
        {
            DavesList.Sort(new DaveCompareNickname());
        }

        
        //
        // Method will organise DavesList by Descriptions.
        public void OrganiseDescriptions()
        {
            DavesList.Sort(new DaveCompareDescription());
        }





        //
        // Method will Print Each daves Nickname followed by age.
        public void PrintByAge(ref string txtOut)
        {
            this.OrganiseBirths();
            DateTime time = DateTime.Now;
            foreach (Dave d in DavesList)
                txtOut += "" + d.GetNickname() + ", age: " + d.GetAge(time).ToString() + "\n";
        }




        public void PrintByNickname(ref string txtOut)
        {
            this.OrganiseNicknames();
            DateTime time = DateTime.Now;
            foreach (Dave d in DavesList)
                txtOut += "" + d.GetNickname() + ".\n";
        }




        //
        // Method will Print All DavesList Members Details.
        public void Print(ref String s)
        {
            string temp = "";

            foreach (Dave d in DavesList)
            {
                temp += "" + d.GetNickname() + "," + d.GetDOB().ToString() + "," + d.GetDescription() + "\n";
            }
            s = temp;
            //return s;
        }

        public void Print()
        {
            string temp = "";

            foreach (Dave d in DavesList)
            {
                temp += "" + d.GetNickname() + "," + d.GetDOB().ToString() + "," + d.GetDescription() + "\n";
            }
            
            
        }


        //
        // Gather Data for Age Graph.
        public Dictionary<string, int> GatherDateForAge()
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();
            DateTime now = DateTime.Now;
            foreach (Dave d in DavesList)
                temp.Add(d.GetNickname(), d.GetAge(now));
            return temp;
        }

        //
        // Gather Data for Graph.
        public Dictionary<string, int> GatherEductionChartData()
        {
            DateTime today = DateTime.Now;
            int preschoolers = 0;
            int primarySchool = 0;
            int secondarySchool = 0;
            int thirdLevel = 0;

            foreach (Dave d in this.DavesList)
            {
                if (d.GetAge(today) < 5)
                    preschoolers++;
                else if (d.GetAge(today) < 13)
                    primarySchool++;
                else if (d.GetAge(today) < 18)
                    secondarySchool++;
                else if (d.GetAge(today) < 23)
                    thirdLevel++;

            }

            Dictionary<string, int> temp = new Dictionary<string, int>();
            temp.Add("Preschoolers", preschoolers);
            temp.Add("Primary Schoolers", primarySchool);
            temp.Add("Secondary Schoolers", secondarySchool);
            temp.Add("Third Level", thirdLevel);

            return temp;
        }

        //
        // Gather Data for Birth Groups.
        public Dictionary<string,int> GatherDataForBirthGroupings()
        {
            DateTime now = DateTime.Now;
            int singles = 0;
            int twins = 0;
            int triplets = 0;
            int quadruplets = 0;
            int quinetruplets = 0;
            int tooManyDaves = 0;
            Dictionary<string, int> tempDictionary = new Dictionary<string, int>();
            List<Dave> tempList = this.Clone() as List<Dave>;//new List<Dave>();

            foreach (Dave dv in tempList)
            {
                int birth = 1;

                if (dv.GetNickname() != "CHECKED")
                {
                    foreach (Dave s in tempList)
                    {
                        if (dv.GetDOB() == s.GetDOB() && dv.GetNickname() != s.GetNickname() && (s.GetNickname() != "CHECKED") && (dv.GetNickname() != "CHECKED"))
                        {
                            s.SetNickname("CHECKED");
                            birth++;
                        }
                    }
                    if (birth > 1)
                    {
                        switch (birth)
                        {
                            case 2:
                                twins += birth;
                                break;
                            case 3:
                                triplets += birth;
                                break;
                            case 4:
                                quadruplets += birth;
                                break;
                            case 5:
                                quinetruplets += birth;
                                break;
                            default:
                                tooManyDaves += birth;
                                break;
                        }
                    }
                    else
                    {
                        if (dv.GetNickname() != "CHECKED")
                        {
                            singles++;
                        }
                    }
                }


            }

            tempDictionary.Add("Single Births", singles);
            tempDictionary.Add("Twins", twins);
            tempDictionary.Add("Triplets", triplets);
            tempDictionary.Add("Quadruplets", quadruplets);
            tempDictionary.Add("Quinetruplets", quinetruplets);
            tempDictionary.Add("Too Many Daves", tooManyDaves);

            return tempDictionary;
        }


        //
        // Methods will Print details of Daves with Birthday occuring in the next Seven  days.
        public void PrintUpcomingBirthdays(ref string s, ref bool found)
        {
            OrganiseBirths();
            DateTime today = DateTime.Now;
            string tempStr = "";
            int i = 1;
            foreach (Dave d in DavesList)
            {
                if (d.HasBirthday(today))
                {
                    found = true;
                    int dYear = d.GetYear();
                    int todayYear = today.Year;
                    int age = todayYear - dYear;
                    age++;
                    tempStr += ""+i.ToString()+".-> " + d.GetNickname()+ ", will be " +age.ToString()+" on " +d.GetDay()+".\n ---->"+ d.GetNickname()+"'s Hobbies : " +d.GetDescription()+".\n\n";
                    i++;
                }
            }
            s = tempStr;

        }

        //
        // Gather Data for Hobbies Chart.
        public Dictionary<string,int> GatherChartDataForHobbies()
        {
            Dictionary<string, int> TempDictionary = new Dictionary<string, int>();


            List<GraphCustomList> graphList = new List<GraphCustomList>();

            foreach (Dave d in DavesList)
            {
                bool isIn = false;

                foreach (GraphCustomList g in graphList)
                {
                   if (g.HasMember(d.GetDescription()))
                      isIn = true;
                }

                switch (isIn)
                {
                    case false:
                       graphList.Add(new GraphCustomList(d.GetDescription(), 1));
                       break;
                    case true:
                       foreach (GraphCustomList g in graphList)
                         if (g.HasMember(d.GetNickname()))
                            g.Increment();
                       break;
                 }

            }


            foreach (GraphCustomList gr in graphList)
                TempDictionary.Add(gr.GetMember(), gr.GetRecord());

            return TempDictionary;
        }


        
        public object Clone()
        {
            List<Dave> list = new List<Dave>();

            foreach (Dave d in DavesList)
                list.Add(d.Clone() as Dave);
            return list;
        }

    }









    public class DaveCompareNickname : IComparer<Dave>
    {
        public int Compare(Dave d1, Dave d2)
        {
            return String.Compare(d1.GetNickname(), d2.GetNickname());
        }
    }

    public class DaveCompareDescription : IComparer<Dave>
    {
        public int Compare(Dave d1, Dave d2)
        {
            return String.Compare(d1.GetDescription(), d2.GetDescription());
        }
    }

    public class DaveCompareBirths : IComparer<Dave>
    {
        public int Compare(Dave d1, Dave d2)
        {
            return DateTime.Compare(d1.GetDOB(), d2.GetDOB());
        }
    }

    //
    // Custom struct for graph data.
    struct GraphCustomList
    {
        private string name;
        private byte record;

        public GraphCustomList(string name, byte record)
        {
            this.name = name;
            this.record = record;
        }

        public bool HasMember(string member)
        {
            return (member == name) ? true : false;
        }

        public String GetMember()
        {
            return this.name;
        }

        public byte GetRecord()
        {
            return this.record;
        }

        public void Increment()
        {
            this.record++;
        }
    }
    
}

