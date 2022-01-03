using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace dot_net_solution
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                label4.Text = "Enter UserName";
            }
            else if (textBox2.Text == "")
            {
                label5.Text = "Enter Password";
            }
            else
            {
                if (textBox1.Text == "srimathi" && textBox2.Text == "sri1234")
                {
                    MessageBox.Show("login succesfully");
                    Form3 f3 = new Form3(); //this is the change, code for redirect  
                    f3.ShowDialog();
                }
                else
                {
                    label6.Text = "Invalid Name or Password";
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
