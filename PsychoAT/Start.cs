using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PsychoAT
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) 
        {
        
        }
        private void button2_Click(object sender, EventArgs e) 
        { 

        }
        private void button2_Click_1(object sender, EventArgs e) 
        {

        }
        private void Start_Load(object sender, EventArgs e) 
        {
        
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void Тесты_Click(object sender, EventArgs e)
        {
            Program.w_Start.Hide();
            Program.w_Test_Choice.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Program.db.show_current_test();
            Program.db.show_all_tests();
            
            Program.w_Start.Hide();
            Program.w_Stat.Show();
        }
    }
}