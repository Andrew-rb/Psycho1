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
    public partial class Test_choice : Form
    {
        public Test_choice()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Test_choice_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button19_Click(object sender, EventArgs e)
        {
            Program.w_Test_Choice.Hide();
            Program.w_Start.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Program.w_Test_Choice.Hide();
            Program.w_Test_Start.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
