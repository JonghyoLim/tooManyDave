using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooManyDaves
{

    //
    // Class Dave Object will store the information for a "Dave".
    public class Dave : ICloneable
    {
        private String nickname { get; set; }   // Dave Nickname.
        private DateTime dob { get; set; }      // Dave Date Of Birth.
        private String description { get; set; } // Dave Description. e.g. Hobbies/Interests.


        //
        // Constructor: Requires - newNickname, newDateOfBirth & newDescription.
        public Dave(String newNickname, String newDob, String newDescription)
        {
            this.SetNickname(newNickname);  
            this.SetDOB(newDob);
            this.SetDescription(newDescription);
        }

        public String GetNickname()
        {
            return this.nickname;
        }

        public DateTime GetDOB()
        {
            return this.dob;
        }

        public String GetDescription()
        {
            return this.description;
        }

        public void SetNickname(String newNickname)
        {
            this.nickname = newNickname;
        }
        public void SetDOB(String newDob)
        {
            DateTime d = DateTime.Parse(newDob);
            this.dob = d;
        }

        public void SetDescription(String newDescription)
        {
            this.description = newDescription;
        }

        //
        // Method will return the "Day" Of the Week of the Daves DOB as a string. eg. "Mon".
        public string GetDay()
        {
            return this.dob.DayOfWeek.ToString();
        }

        //
        // Method will return an integer representing the day of the week of the Daves DOB.
        public int GetDayNum()
        {
            return this.dob.Day;
        }


        //
        // Method will return an integer representing the year of the Daves DOB. 
        public int GetYear()
        {
            return Convert.ToInt32(this.dob.Year);
        }
        

        //
        // Method will return an integer representing the Daves current age based
        // on the Date passed to the method.
        public int GetAge(DateTime time)
        {
            int i = time.Year - this.dob.Year;
            if (time.Month < this.dob.Month) // Decrement if year Birthday occured at earlier Month.
                i--;
            return i;
        }


        //
        // Method Will return an integer representing the age Dave Will be on the September
        // of the year passed into the method.
        public int GetSchoolAge(DateTime time)
        {
            int i = time.Year - this.dob.Year;
            if (time.Month > 9)
                i--;
            return i;
        }


        
        //
        // Method will return Nickname, DOB & Description.
        public string Print()
        {
            return "" + this.GetNickname() + "," + this.GetDOB().ToString() + "," + this.GetDescription() + "";
        }


        
        //
        // For iCompareable inteface.
        public int CompareTo(Object obj)
        {
            if (obj is Dave)
            {
                Dave s = obj as Dave;
                return DateTime.Compare(this.GetDOB(), s.GetDOB());
            }
            else
            {
                throw new ArgumentException("There is a problem Dave in here!");
            }
        }


        //
        // For use with iComparable interface.
        public class DaveCompareNickname : IComparer<Dave>
        {
            public int Compare(Dave d1, Dave d2)
            {
                return String.Compare(d1.GetNickname(), d2.GetNickname());
            }
        }


        //
        // Method will check Daves DOB for an upcoming Birthday within the next seven
        // days based on a date passed representing the current time.
        public bool HasBirthday(DateTime today)
        {
            

            if (this.dob.Month == today.Month && this.dob.Day >= today.Day && (this.dob.Day - today.Day < 8))
                return true;
            else
                return false;
        }

        //
        // Method will check if Dave is less than 18 years old based on a date passed.
        public bool Qulaifies(DateTime date)
        {
            return (this.GetAge(date) < 18) ? true : false;
        }


        //
        // Method will compare Dave's Nickname with a string and return a boolean.
        public bool IsIn(string name)
        {
            if (name == this.GetNickname())
                return true;
            else
                return false;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
