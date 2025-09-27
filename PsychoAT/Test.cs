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
        Button[] Array_of_buttons;
        public Test()
        {
            InitializeComponent();
            this.Array_of_buttons = new Button[6] { Answer1, Answer2, Answer3, Answer4, Answer5, Answer6 };
        }

        private void Test_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            this.Show_answers_on_page();
        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
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

        private void Previous_question_button_Click(object sender, EventArgs e)
        {
            Program.Main_test_page.Go_to_the_previous_question();
            this.Bottom_buttons_logic();
            this.Show_answers_on_page();
        }

        private void Next_question_Click(object sender, EventArgs e)
        {
            Program.Main_test_page.Go_to_the_next_question();
            this.Bottom_buttons_logic();
            this.Show_answers_on_page();
        }

        private void Show_answers_on_page()
        {
            this.Question_title.Text = Program.Main_test_page.Get_question_text();
            this.Answer_texts(Program.Main_test_page.Get_array_of_answers());
        }
        private void Answer_texts(Answer[] array_of_answerss)
        {
            short size_of_answer_array = (short)array_of_answerss.Length;
            for (int i = 0; i < size_of_answer_array; i++)
            {
                this.Array_of_buttons[i].Text = array_of_answerss[i].text;
                this.Array_of_buttons[i].Visible = true;
                this.Array_of_buttons[i].Enabled = true;
            }
            if (size_of_answer_array < 6) this.Hide_unused_buttons(size_of_answer_array - 1);
        }
        private void Hide_unused_buttons(int index_from_witch_needs_to_be_hide)
        {
            for (int i = index_from_witch_needs_to_be_hide; i < 6; i++)
            {
                this.Array_of_buttons[i].Visible = false;
                this.Array_of_buttons[i].Enabled = false;
            }
        }

        private void Bottom_buttons_logic()
        {
            if (Program.Main_test_page.Is_it_first_question())
            {
                this.Previous_question_button.Enabled = false;
                this.Next_question.Enabled = true;
                return;
            }
            else if (Program.Main_test_page.Is_it_last_question())
            {
                this.Previous_question_button.Enabled = true;
                this.Next_question.Enabled = false;
                return;
            }
            if (Program.Main_test_page.Is_there_only_one_page())
            {
                this.Previous_question_button.Enabled = false;
                this.Next_question.Enabled = false;
                return;
            }
            this.Previous_question_button.Enabled = true;
            this.Next_question.Enabled = true;
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Program.w_Test.Hide();
            Program.w_Test_Choice.Show();
        }
    }
}
