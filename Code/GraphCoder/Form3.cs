using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GraphCoder
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            trackBar1.Value = Info.scale;
            label2.Text = Info.scale.ToString();
            textBox1.Text = Info.name;
            switch (Info.size.Height)
            {
                case 300: comboBox1.Text = "300x300"; break;
                case 600: comboBox1.Text = "600x600"; break;
                case 900: comboBox1.Text = "900x900"; break;
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label2.Text = ((int)trackBar1.Value).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Info.name = textBox1.Text;
            switch (comboBox1.Text)
            {
                case "300x300": Info.size = new Size(300, 300); break;
                case "600x600": Info.size = new Size(600, 600); break;
                case "900x900": Info.size = new Size(900, 900); break;
            }

            Info.scale = (int)trackBar1.Value;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
