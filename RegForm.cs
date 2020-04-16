//B8030
//Program 3
// CIS 199-02
// Due: 10/18/2018
// By: B8030

// This application calculates the earliest registration date
// and time for an undergraduate student given their class standing
// and last name.
// Decisions based on UofL Spring 2019 Priority Registration Schedule

// Solution 3
// This solution keeps the first letter of the last name as a char
// and uses if/else logic for the times.
// It uses defined strings for the dates and times to make it easier
// to maintain.
// It only uses programming elements introduced in the text or
// in class.
// This solution takes advantage of the fact that there really are
// only two different time patterns used. One for juniors and seniors
// and one for sophomores and freshmen. The pattern for sophomores
// and freshmen is complicated by the fact that certain letter ranges
// get one date and other letter ranges get another date.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog2
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        // Find and display earliest registration time
        private void findRegTimeBtn_Click(object sender, EventArgs e)
        {
            const string SENIORDAY = "November 2"; // 1st day of registration
            const string JUNIORDAY = "November 5"; // 2nd day of registration
            const string SOPH1 = "November 6";     // 3rd day of registration
            const string SOPH2 = "November 7";     // 4th day of registration
            const string FRESH1 = "November 8";    // 5th day of registration
            const string FRESH2 = "November 9";    // 6th day of registration

            const string TIME1 = "8:30 AM";  // 1st time block
            const string TIME2 = "10:00 AM"; // 2nd time block
            const string TIME3 = "11:30 AM"; // 3rd time block
            const string TIME4 = "2:00 PM";  // 4th time block
            const string TIME5 = "4:00 PM";  // 5th time block
            

            string lastNameStr;       // Entered last name
            char lastNameLetterCh;    // First letter of last name, as char
            string dateStr = "Error"; // Holds date of registration
            string timeStr = "Error"; // Holds time of registration
            bool isUpperClass;        // Upperclass or not?

            bool foundSenJun = false;//setting senior and junior to boolean false
            bool foundSopFresh = false;// setting boolean for sophomore and freshman

            char[] seniorJuniorL =   {  'A',   'E',   'J',   'P',   'T' };// holds the senior & junior letters
            string[] seniorJuniorT = { TIME5, TIME1, TIME2, TIME3, TIME4 };// senior&junior time parallel to those letter

            char[] SopFreshL =   { 'A',    'C',   'E',   'G',   'J',   'M',   'P',   'R',   'T',   'W' }; //holds sopomore and freshman letter
            string[] sopFreshT = { TIME4, TIME5, TIME1, TIME2, TIME3, TIME4, TIME5, TIME1, TIME2, TIME3 };//parallel to those letter

            int indexSopFresh = SopFreshL.Length - 1;// setting it lower limit, becuase its easier
            int indexsenJun = seniorJuniorL.Length - 1;// setting it lower limit, becuase its easier


            lastNameStr = lastNameTxt.Text;
            if (lastNameStr.Length > 0) // Empty string?
            {
                lastNameLetterCh = lastNameStr[0];   // First char of last name

                if (char.IsLetter(lastNameLetterCh)) // Is it a letter?
                {
                    lastNameLetterCh = char.ToUpper(lastNameLetterCh); // Ensure upper case

                    isUpperClass = (seniorRBtn.Checked || juniorRBtn.Checked);

                    // Juniors and Seniors share same schedule but different days
                    if (isUpperClass)
                    {
                        if (seniorRBtn.Checked)
                            dateStr = SENIORDAY;
                        else // Must be juniors
                            dateStr = JUNIORDAY;

                        while (indexsenJun >= 0 && !foundSenJun)//conditon index is greater or equal to 0 and not found senior and junior
                        {
                            if (lastNameLetterCh >= seniorJuniorL[indexsenJun])// execute if the letter matches or greater to senior junior array
                                foundSenJun = true;// if found true ends the loop display statement 2
                            else
                                --indexsenJun;// decrementing so it count backward
                        }
                        if (foundSenJun)// if found, 
                            timeStr = seniorJuniorT[indexsenJun];//will execute the statement.

                    }
                    // Sophomores and Freshmen
                    else // Must be soph/fresh
                    {
                        if (sophomoreRBtn.Checked)
                        {
                            // E-Q on one day
                            if ((lastNameLetterCh >= 'E') && // >= E and
                                (lastNameLetterCh <= 'Q'))   // <= Q
                                dateStr = SOPH1;
                            else // All other letters on next day
                                dateStr = SOPH2;
                        }
                        else // must be freshman
                        {
                            // E-Q on one day
                            if ((lastNameLetterCh >= 'E') && // >= E and
                                (lastNameLetterCh <= 'Q'))   // <= Q
                                dateStr = FRESH1;
                            else // All other letters on next day
                                dateStr = FRESH2;
                        }

                        
                        while (indexSopFresh >= 0 & !foundSopFresh) // strats by testing the condition.
                        {
                            if (lastNameLetterCh >= SopFreshL[indexsenJun])// if true will execute
                                foundSopFresh = true;// if true stops the loop and display statement 2.
                            else
                                --indexSopFresh; // decrements
                        }
                        if (foundSopFresh)
                            timeStr = sopFreshT[indexSopFresh];
                    }

                    // Output results
                    dateTimeLbl.Text = $"{dateStr} at {timeStr}";
                }
                else // First char not a letter
                    MessageBox.Show("Make sure last name starts with a letter");
            }
            else // Empty textbox
                MessageBox.Show("Enter a last name!");
        }

        private void lastNameTxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
