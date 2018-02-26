using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace tincoder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Form1 form1 = this;
            Form.CheckForIllegalCrossThreadCalls = false;
        }
        private static Bitmap encodedImage = new Bitmap(1,1);

        private void button1_Click(object sender, System.EventArgs e)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                encodedImage = new Bitmap(encodedImage, 1, 1);
                encodedImage.SetPixel(0, 0, Color.Transparent);
                encodedImage = new Bitmap(encodedImage, 1024, 1024);
                button1.Enabled = false;
                var current = new VecInt2(0, 0);
                List<string> list = (new List<string>(richTextBox1.Text.SplitInParts(4)));
                Text = "Tincoder :: STARTING";
                foreach (string c in list)
                {
                    List<char> chars = new List<char>(c.ToCharArray());
                    while (chars.Count < 4)
                    {
                        int i = 0;
                        while (i < (5 - chars.Count))
                        {
                            chars.Add('\0');
                            i++;
                        }
                    }
                    List<int> ints = new List<int>();
                    foreach (char a in chars)
                    {
                        ints.Add(MathExtensions.Clamp<int>(Convert.ToInt32(a), 0, 255));
                    }

                    encodedImage.SetPixel(current.Val1, current.Val2, Color.FromArgb(ints[0], ints[1], ints[2], ints[3]));
                    if (checkBox1.Checked && checkBox3.Checked)
                    {
                        pictureBox1.Image = encodedImage;
                        Thread.Sleep(7);
                    }
                    if(checkBox2.Checked && checkBox3.Checked)
                        Text = "Tincoder :: " + current.Val1 + "/" + current.Val2;
                    current.Val1++;
                    if (current.Val1 == 1024)
                    {
                        if (checkBox1.Checked)
                        {
                            pictureBox1.Image = encodedImage;
                            Thread.Sleep(7);
                        }
                        if (checkBox2.Checked)
                            Text = "Tincoder :: " + current.Val1 + "/" + current.Val2;
                        current.Val2++;
                        current.Val1 = 1;
                        if (current.Val2 == 1024)
                        {
                            break;
                        }
                    }
                }
                //end of list foreach
                pictureBox1.Image = encodedImage;
                Thread.Sleep(7);
                Text = "Tincoder :: DONE";
                button1.Enabled = true;
            }).Start();
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stream myStream = null;

            SaveFileDialog openFileDialog1 = new SaveFileDialog();

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
                        encodedImage.Save(myStream,ImageFormat.Png);
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
    static class StringExtensions
    {

        public static IEnumerable<String> SplitInParts(this string s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

    }
    static class MathExtensions
    {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
    }
    public class VecInt2
    {
        public int Val1 { get; set; } 
        public int Val2 { get; set; }
        public VecInt2(int Val1, int Val2)
        {
            this.Val1 = Val1;
            this.Val2 = Val2;
        }

    }

}
