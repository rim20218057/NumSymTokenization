using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace BonusQuestion
{
    public partial class Form1 : Form
    {
        Hashtable callsTable;
        public Form1()
        {
            InitializeComponent();

            callsTable = new Hashtable();
            StreamReader sr = new StreamReader("C:\\Users\\Rima\\Calls.txt");
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(':');
                string phone = tokens[0];
                int duration = int.Parse(tokens[1]);
                float cost = float.Parse(tokens[2]);
                float totalCost = cost * duration;


                //Adding a new phone
                if(!callsTable.ContainsKey(phone))
                    callsTable.Add(phone, totalCost);
                else
                {
                    //Adding additional cost to existing phones
                    float temp = (float)callsTable[phone];
                    temp = temp + cost;
                    callsTable[phone] = temp;
                }

                sr.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("C:\\Users\\Rima\\Bills.txt");
            StreamReader sr = new StreamReader("C:\\Users\\Rima\\Names.txt");
            string line;

            while((line = sr.ReadLine())!= null){
                string[] tokens = line.Split(':');
                string name = tokens[0];
                string phone = tokens[1];

                if (callsTable.ContainsKey(phone))
                {
                    string r = name + " " + callsTable[phone].ToString();
                    listBox3.Items.Add(r);
                    sw.WriteLine(r);
                }
                else
                {
                    string r = name + " 0.00";
                    listBox3.Items.Add(r);
                    sw.WriteLine(r);
                }

            }

            sr.Close();
            sw.Close();
        }
    }
}
