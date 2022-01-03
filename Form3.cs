using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace dot_net_solution
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16};
private int BlockSize = 128;
    private void button2_Click(object sender, EventArgs e)
{

}

    private void button3_Click(object sender, EventArgs e)
    {
        if (textBoxPassword.Text == "") return;
        //Decrypt
        byte[] bytes = Convert.FromBase64String(richTextBox1.Text);
        SymmetricAlgorithm crypt = Aes.Create();
        HashAlgorithm hash = MD5.Create();
        crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(textBoxPassword.Text));
        crypt.IV = IV;

        using (MemoryStream memoryStream = new MemoryStream(bytes))
        {
            using (CryptoStream cryptoStream =
               new CryptoStream(memoryStream, crypt.CreateDecryptor(), CryptoStreamMode.Read))
            {
                byte[] decryptedBytes = new byte[bytes.Length];
                cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                richTextBox1.Text = Encoding.Unicode.GetString(decryptedBytes);
            }
        }
    }

    

   

    private void button2_Click_1(object sender, EventArgs e)
    {
        if (textBoxPassword.Text == "") return;
        byte[] bytes = Encoding.Unicode.GetBytes(richTextBox1.Text);
        //Encrypt
        SymmetricAlgorithm crypt = Aes.Create();
        HashAlgorithm hash = MD5.Create();
        crypt.BlockSize = BlockSize;
        crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(textBoxPassword.Text));
        crypt.IV = IV;

        using (MemoryStream memoryStream = new MemoryStream())
        {
            using (CryptoStream cryptoStream =
               new CryptoStream(memoryStream, crypt.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cryptoStream.Write(bytes, 0, bytes.Length);
            }

            richTextBox1.Text = Convert.ToBase64String(memoryStream.ToArray());
        }
    }

    private void button3_Click_1(object sender, EventArgs e)
    {
        if (textBoxPassword.Text == "")
        {
            MessageBox.Show("Enter the secret key");
            return;
        }
        //Decrypt
        byte[] bytes = Convert.FromBase64String(richTextBox1.Text);
        SymmetricAlgorithm crypt = Aes.Create();
        HashAlgorithm hash = MD5.Create();
        crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(textBoxPassword.Text));
        crypt.IV = IV;

        using (MemoryStream memoryStream = new MemoryStream(bytes))
        {
            using (CryptoStream cryptoStream =
               new CryptoStream(memoryStream, crypt.CreateDecryptor(), CryptoStreamMode.Read))
            {
                byte[] decryptedBytes = new byte[bytes.Length];
                cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                richTextBox1.Text = Encoding.Unicode.GetString(decryptedBytes);
            }
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Form4 f4 = new Form4();
        f4.Show();
    }

    private void Form3_Load(object sender, EventArgs e)
    {

    }
    }
}
 
        
    

