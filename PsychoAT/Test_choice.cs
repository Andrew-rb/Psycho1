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
        List<Guna.UI2.WinForms.Guna2Button> Button_list;
        public Test_choice()
        {
            InitializeComponent();
            this.Button_list = new List<Guna.UI2.WinForms.Guna2Button> { test_choise_1,test_choise_2,test_choise_3,test_choise_4,test_choise_5};
            Program.Test_choise_logic.Initial_Show_tests_on_page(Program.db, Program.w_Test_Choice);
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


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Test_choice_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        //first_test_button
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Program.w_Test_Choice.Hide();
            Program.w_Test_Start.Show();
        }

        //second_test_button
        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void test_choise_3_Click(object sender, EventArgs e)
        {

        }

        private void test_choise_4_Click(object sender, EventArgs e)
        {

        }

        private void test_choise_5_Click(object sender, EventArgs e)
        {

        }

        private void test_page_next_Click(object sender, EventArgs e)
        {

        }

        private void test_page_back_Click(object sender, EventArgs e)
        {

        }
    }
}
