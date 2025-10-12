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
    public partial class Result : Form
    {
        private Results resault;
        public Result()
        {
            InitializeComponent();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Program.w_Result.Hide();
            Program.w_Test_Choice.Show();
        }

        private void Result_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Internal_set_result_for_text(Results resault)
        {
            this.resault= resault;
            this.Resault_text.Text = this.resault.result;
        }

        public void Set_resault(Results resault)
        {
            this.Internal_set_result_for_text(resault);
        }
    }
}
