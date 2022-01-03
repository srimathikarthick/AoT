using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace dot_net_solution
{
    public partial class Form4 : Form
    {
        byte[] abc;
        byte[,] table;
        int segundo = 0;
        int undo1 = 0;
        DateTime dt = new DateTime();
        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
        public Form4()
        {
            InitializeComponent();
        }
        
        

        private void Form4_Load(object sender, EventArgs e)
        {
            rbEncrypt.Checked = true;

            // init abc and table
            abc = new byte[256];
            for (int i = 0; i < 256; i++)
                abc[i] = Convert.ToByte(i);

            table = new byte[256, 256];
            for (int i = 0; i < 256; i++)
                for (int j = 0; j < 256; j++)
                {
                    table[i, j] = abc[(i + j) % 256];
                }
        }
       

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Multiselect = false;
            if (od.ShowDialog() == DialogResult.OK)
            {
                tbPath.Text = od.FileName;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //Check input values
            timer1.Enabled = true;
            //Time.Text = dt.AddSeconds(segundo).ToString("HH:mm:ss");
            if (!File.Exists(tbPath.Text))
            {
                MessageBox.Show("File does not exist.");
                return;
            }
            if (String.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show("Password empty. Please enter your password");
                return;
            }

            // Get file content and key for encrypt/decrypt
            try
            {
                byte[] fileContent = File.ReadAllBytes(tbPath.Text);
                byte[] passwordTmp = Encoding.ASCII.GetBytes(tbPassword.Text);
                byte[] keys = new byte[fileContent.Length];
                for (int i = 0; i < fileContent.Length; i++)
                    keys[i] = passwordTmp[i % passwordTmp.Length];

                // Encrypt
                byte[] result = new byte[fileContent.Length];
                //string fileName = @"C:\Users\RAJESH\Desktop\trail.docx";
                //FileInfo fi = new FileInfo(fileName);
                long size = fileContent.Length;
                string s = ConvertBytesToMegabytes(fileContent.Length).ToString("0.00");
                label4.Text = "File Size in Bytes: " + size ;
                label5.Text = "File Size in MB: " + s;
                label7.Text = "256 Bit Length Key is Used";
                
                if (rbEncrypt.Checked)
                {
                    for (int i = 0; i < fileContent.Length; i++)
                    {
                        byte value = fileContent[i];
                        byte key = keys[i];
                        int valueIndex = -1, keyIndex = -1;
                        for (int j = 0; j < 256; j++)
                            if (abc[j] == value)
                            {
                                valueIndex = j;
                                break;
                            }
                        for (int j = 0; j < 256; j++)
                            if (abc[j] == key)
                            {
                                keyIndex = j;
                                break;
                            }
                        result[i] = table[keyIndex, valueIndex];
                        
                    }
                     
                }
                // Decrypt
                else
                {

                    label4.Text = "File Size in Bytes: " + fileContent.Length;
                    label5.Text = "File Size in MB: " + ConvertBytesToMegabytes(fileContent.Length).ToString("0.00");
                    label7.Text = "256 Bit Length Key is Used";
                    
                    for (int i = 0; i < fileContent.Length; i++)
                    {
                        byte value = fileContent[i];
                        byte key = keys[i];
                        int valueIndex = -1, keyIndex = -1;
                        for (int j = 0; j < 256; j++)
                            if (abc[j] == key)
                            {
                                keyIndex = j;
                                break;
                            }
                        for (int j = 0; j < 256; j++)
                            if (table[keyIndex, j] == value)
                            {
                                valueIndex = j;
                                break;
                            }
                        result[i] = abc[valueIndex];
                    }
                    
                }

                // Save result to new file with the same extension
                String fileExt = Path.GetExtension(tbPath.Text);
                SaveFileDialog sd = new SaveFileDialog();
                sd.Filter = "Files (*" + fileExt + ") | *" + fileExt;
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    
                    File.WriteAllBytes(sd.FileName, result);
                    timer1.Enabled = false;
                }

            }
            catch
            {
                MessageBox.Show("File is in use.Close other program is using this file and try again.");
                return;
            }
                    }

        private void rbEncrypt_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEncrypt.Checked)
            {
                rbDecrypt.Checked = false;
            }
        }

        private void rbDecrypt_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbDecrypt.Checked)
                rbEncrypt.Checked = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Check input values
            timer2.Enabled = true;
            if (!File.Exists(tbPath1.Text))
            {
                MessageBox.Show("File does not exist.");
                return;
            }
            if (String.IsNullOrEmpty(tbPassword1.Text))
            {
                MessageBox.Show("Password empty. Please enter your password");
                return;
            }

            // Get file content and key for encrypt/decrypt
            try
            {
                byte[] fileContent = File.ReadAllBytes(tbPath1.Text);
                byte[] passwordTmp = Encoding.ASCII.GetBytes(tbPassword1.Text);
                byte[] keys = new byte[fileContent.Length];
                for (int i = 0; i < fileContent.Length; i++)
                    keys[i] = passwordTmp[i % passwordTmp.Length];

                // Encrypt
                byte[] result = new byte[fileContent.Length];
                long size = fileContent.Length;
                string s = ConvertBytesToMegabytes(fileContent.Length).ToString("0.00");
                Byte.Text = "File Size in Bytes: " + fileContent.Length;
                mb.Text = "File Size in MB: " + ConvertBytesToMegabytes(fileContent.Length).ToString("0.00");
                key1.Text = "256 Bit Length Key is Used";
                //Time1.Text = "Time taken: 00:00:03";
                if (radioEncrypt2.Checked)
                {
                    for (int i = 0; i < fileContent.Length; i++)
                    {
                        byte value = fileContent[i];
                        byte key = keys[i];
                        int valueIndex = -1, keyIndex = -1;
                        for (int j = 0; j < 256; j++)
                            if (abc[j] == value)
                            {
                                valueIndex = j;
                                break;
                            }
                        for (int j = 0; j < 256; j++)
                            if (abc[j] == key)
                            {
                                keyIndex = j;
                                break;
                            }
                        result[i] = table[keyIndex, valueIndex];

                    }

                }
                // Decrypt
                else
                {

                    Byte.Text = "File Size in Bytes: " + fileContent.Length;
                    mb.Text = "File Size in MB: " + ConvertBytesToMegabytes(fileContent.Length).ToString("0.00");
                    key1.Text = "256 Bit Length Key is Used";
                    //Time1.Text = "Time taken : 00:00:03";
                    for (int i = 0; i < fileContent.Length; i++)
                    {
                        byte value = fileContent[i];
                        byte key = keys[i];
                        int valueIndex = -1, keyIndex = -1;
                        for (int j = 0; j < 256; j++)
                            if (abc[j] == key)
                            {
                                keyIndex = j;
                                break;
                            }
                        for (int j = 0; j < 256; j++)
                            if (table[keyIndex, j] == value)
                            {
                                valueIndex = j;
                                break;
                            }
                        result[i] = abc[valueIndex];
                    }

                }

                // Save result to new file with the same extension
                String fileExt = Path.GetExtension(tbPath1.Text);
                SaveFileDialog sd = new SaveFileDialog();
                sd.Filter = "Files (*" + fileExt + ") | *" + fileExt;
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(sd.FileName, result);
                    timer2.Enabled = false;
                }

            }
            catch
            {
                MessageBox.Show("File is in use.Close other program is using this file and try again.");
                return;
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Multiselect = false;
            if (od.ShowDialog() == DialogResult.OK)
            {
                tbPath1.Text = od.FileName;
            }
        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

       

        private void radioDecrypt2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioDecrypt2.Checked)
                radioEncrypt2.Checked = false;
        }

        private void radioEncrypt2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioEncrypt2.Checked)
            {
                radioDecrypt2.Checked = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            segundo++;
            //Time.Text = dt.AddSeconds(segundo).ToString("HH:mm:ss");
            Time.Text = "Time in Nano Seconds:" +dt.AddMilliseconds(segundo).ToString("fff");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void Time1_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            undo1=undo1+2;
            Time1.Text = "Time in Nano Seconds:" + dt.AddMilliseconds(undo1).ToString("fff");
        }

        
       

    }
}
