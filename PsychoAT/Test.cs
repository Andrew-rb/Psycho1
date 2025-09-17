using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PsychoAT
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Test_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Test_Load(object sender, EventArgs e)
        {

        }

        private void Answer1_Click(object sender, EventArgs e)
        {
            Answer1.Enabled = false;
            Answer2.Enabled = true;
            Answer3.Enabled = true;
            Answer4.Enabled = true;
            Answer5.Enabled = true;
            Answer6.Enabled = true;

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Answer2_Click(object sender, EventArgs e)
        {
            Answer1.Enabled = true;
            Answer2.Enabled = false;
            Answer3.Enabled = true;
            Answer4.Enabled = true;
            Answer5.Enabled = true;
            Answer6.Enabled = true;
        }

        private void Answer3_Click(object sender, EventArgs e)
        {
            Answer1.Enabled = true;
            Answer2.Enabled = true;
            Answer3.Enabled = false;
            Answer4.Enabled = true;
            Answer5.Enabled = true;
            Answer6.Enabled = true;
        }

        private void Answer4_Click(object sender, EventArgs e)
        {
            Answer1.Enabled = true;
            Answer2.Enabled = true;
            Answer3.Enabled = true;
            Answer4.Enabled = false;
            Answer5.Enabled = true;
            Answer6.Enabled = true;
        }

        private void Answer5_Click(object sender, EventArgs e)
        {
            Answer1.Enabled = true;
            Answer2.Enabled = true;
            Answer3.Enabled = true;
            Answer4.Enabled = true;
            Answer5.Enabled = false;
            Answer6.Enabled = true;
        }

        private void Answer6_Click(object sender, EventArgs e)
        {
            Answer1.Enabled = true;
            Answer2.Enabled = true;
            Answer3.Enabled = true;
            Answer4.Enabled = true;
            Answer5.Enabled = true;
            Answer6.Enabled = false;
        }
    }
}
