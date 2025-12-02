using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PsychoVS2.Windows
{
    public partial class Test : Window
    {
        private Button selectedAnswer;
        private Psycho_Test choosen_test;
        private int num_of_quest, current_question = 1;
        private int[] selected_answer_id;
        private Question[] questions;
        private Button[] answer_buttons;
        private Dictionary<string, int> points;
        private float step_for_progress_bar;

        public Test( Psycho_Test choosen_test)
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            this.choosen_test = choosen_test;
            this.init_for_internal_arrays();
        }

        private void init_for_internal_arrays()
        {
            this.num_of_quest = this.choosen_test.amm_of_questions;
            this.selected_answer_id = Enumerable.Repeat(-1,this.num_of_quest).ToArray();
            this.questions = this.choosen_test.questions.ToArray(); //new Question[this.num_of_quest];
            this.step_for_progress_bar = 100 / this.num_of_quest + 1;
            this.Progress_bar.Value = step_for_progress_bar;
            //this.choosen_test.questions.CopyTo(this.questions);
            this.answer_buttons = new Button[6] { answer_1, answer_2, answer_3, answer_4, answer_5, answer_6 };
            for (int i = 0; i < 6; i++)
            {
                this.answer_buttons[i].Tag = i;
            }
            Task.Run(() => this.init_of_points());
            this.Show_question_and_answers();
        }

        private void init_of_points()
        {
            this.points = this.for_res();
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            // Снимаем выделение с предыдущего выбранного ответа
            if (selectedAnswer != null)
            {
                selectedAnswer.Style = (Style)FindResource("AnswerButtonStyle");
            }

            // Выделяем новый выбранный ответ
            selectedAnswer = (Button)sender;
            selectedAnswer.Style = (Style)FindResource("SelectedAnswerButtonStyle");

            // Активируем кнопку "Далее"
            NextButton.IsEnabled = true;
            SelectionIndicator.Content = "✓ Ответ выбран";
            SelectionIndicator.Foreground = new System.Windows.Media.SolidColorBrush(
                System.Windows.Media.Color.FromArgb(0xFF, 0xFC, 0xCC, 0x3C));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.current_question != 1)
            {
                if(this.selectedAnswer != null)
                    this.selected_answer_id[this.current_question - 1] = (int)this.selectedAnswer.Tag;
                this.current_question -= 1;
                this.Show_question_and_answers();
                this.Progress_bar.Value -= this.step_for_progress_bar;
            }
            // Навигация назад
            //MessageBox.Show("Переход к предыдущему вопросу");
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.num_of_quest == this.current_question)
            {
                this.selected_answer_id[this.current_question - 1] = (int)this.selectedAnswer.Tag;
                //Result result = new Result(this.points, this.choosen_test.id);
                this.update_points();
                New_result_window Result_window = new New_result_window(this.points, this.choosen_test.id);
                Result_window.Show();
                this.Close();
            }
            else if(this.selectedAnswer != null)
            {
                NextButton.IsEnabled = false;
                this.selected_answer_id[this.current_question - 1] = (int)this.selectedAnswer.Tag;
                this.current_question += 1;
                this.Progress_bar.Value += this.step_for_progress_bar;
                this.Show_question_and_answers();
            }
        }

        private Dictionary<string, int> for_res()
        {
            Dictionary<string, int> ret_dic = new Dictionary<string, int>();
            foreach (Question quest in this.questions)
            {
                foreach (Answer answ in quest.answers)
                {
                    foreach (Points_cods point in answ.points_cods)
                    {
                        if (!ret_dic.ContainsKey(point.type))
                            ret_dic[point.type] = 0;
                    }
                }
            }
            return ret_dic;
        }

        private void update_points() {
            Answer[] selected_answers = new Answer[this.num_of_quest];
            for (short i = 0; i < this.num_of_quest; i++)
            {
                selected_answers[i] = this.questions[i].answers.ToArray()[this.selected_answer_id[i]];
            }
            foreach (Answer ans in selected_answers)
            {
                foreach (Points_cods points in ans.points_cods)
                {
                    this.points[points.type] += points.value;
                }
            }
        }

        private void Show_question_and_answers()
        {
            this.Answer_counter.Content = $"Вопрос {this.current_question} из {this.num_of_quest}";
            Answer[] ans_to_quest = new Answer[6];
            this.questions[this.current_question-1].answers.ToArray().CopyTo(ans_to_quest,0);
            this.Question.Text = this.questions[this.current_question-1].text;
            for (int i = 0; i < ans_to_quest.Length; i++)
            {
                if (ans_to_quest[i] != null)
                {
                    this.answer_buttons[i].Visibility = Visibility.Visible;
                    var text_box = this.answer_buttons[i].Content as TextBlock;
                    text_box.Text = ans_to_quest[i].text;
                    //this.answer_buttons[i].Content = ans_to_quest[i].text;
                    continue;
                }
                else
                    this.answer_buttons[i].Visibility = Visibility.Hidden;
            }
            if (this.selected_answer_id[this.current_question-1] != -1)
            {
                this.NextButton.IsEnabled = true;
                if(selectedAnswer != null) selectedAnswer.Style = (Style)FindResource("AnswerButtonStyle");
                this.answer_buttons[this.selected_answer_id[this.current_question-1]].Style = (Style)FindResource("SelectedAnswerButtonStyle");
                this.selectedAnswer = this.answer_buttons[this.selected_answer_id[this.current_question - 1]];
                SelectionIndicator.Content = "✓ Ответ выбран";
                SelectionIndicator.Foreground = new System.Windows.Media.SolidColorBrush(
                System.Windows.Media.Color.FromArgb(0xFF, 0xFC, 0xCC, 0x3C));
            }
            else if (selectedAnswer != null)
            {
                selectedAnswer.Style = (Style)FindResource("AnswerButtonStyle");
                this.selectedAnswer = null;
            }
            else
            {
                SelectionIndicator.Content = "Выберите вариант ответа";
                SelectionIndicator.Foreground = new System.Windows.Media.SolidColorBrush(
                System.Windows.Media.Color.FromArgb(204, 255, 255, 255));
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Выход из теста
            var result = MessageBox.Show("Вы уверены, что хотите выйти из теста?",
                "Подтверждение выхода",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Test_Start testStartWindow = new Test_Start(this.choosen_test);
                testStartWindow.Show();
                this.Close();
            }
        }
    }
}