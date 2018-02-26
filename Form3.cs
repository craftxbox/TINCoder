using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace tincoder
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            Form3 Form3 = this;
            Form.CheckForIllegalCrossThreadCalls = false;
        }
        private static Bitmap encodedImage = new Bitmap(1,1);

        private void button1_Click(object sender, System.EventArgs e)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                this.Text = "Tincoder :: STARTING";
                VecInt2 current = new VecInt2(0, 0);
                List<char> chars = new List<char>();
                while (true)
                {
                    Color px = encodedImage.GetPixel(current.Val1, current.Val2);
                    if(checkBox1.Checked == false)
                    {
                        if (px.A == 0 && px.R == 0 && px.G == 0 && px.B == 0)
                        {
                            break;
                        }
                    }
                    chars.Add((char)px.A);
                    chars.Add((char)px.R);
                    chars.Add((char)px.G);
                    chars.Add((char)px.B);
                    if (checkBox2.Checked && checkBox4.Checked)
                        richTextBox1.Text = string.Join("", chars);
                    current.Val1++;
                    if (current.Val1 == encodedImage.Width)
                    {
                        if (checkBox3.Checked)
                            this.Text = "Tincoder :: " + current.Val1 + "/" + current.Val2;
                        if (checkBox2.Checked)
                            richTextBox1.Text = string.Join("", chars);
                        current.Val2++;
                        current.Val1 = 1;
                        if (current.Val2 == encodedImage.Height)
                        {
                            break;
                        }
                    }
                    if (checkBox3.Checked && checkBox4.Checked)
                        this.Text = "Tincoder :: " + current.Val1 + "/" + current.Val2;
                }
                richTextBox1.Text = string.Join("", chars);
                this.Text = "Tincoder :: DONE";
            }).Start();
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stream myStream = null;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "JPEG Image files|*.jpg|PNG Image files|*.png|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        encodedImage = new Bitmap(myStream);
                        pictureBox1.Image = encodedImage;
                        myStream.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2.backMenu = true;
            this.Close();
        }
    }
}
