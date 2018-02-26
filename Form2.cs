using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tincoder
{
    public partial class Form2 : Form
    {
        public static bool backMenu = false;
        public Form2()
        {
            InitializeComponent();
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Visible = false;
            f.ShowDialog();
            if (backMenu == false)
                this.Close();
            else this.Visible = true;
            backMenu = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            this.Visible = false;
            f.ShowDialog();
            if (backMenu == false)
                this.Close();
            else this.Visible = true;
            backMenu = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderColor = BackColor;
            button3.FlatAppearance.MouseOverBackColor = BackColor;
            button3.FlatAppearance.MouseDownBackColor = BackColor;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            this.Visible = false;
            f.ShowDialog();
            if (backMenu == false)
                this.Close();
            else this.Visible = true;
            backMenu = false;
        }
    }
}
