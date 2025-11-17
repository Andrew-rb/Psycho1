using System;
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
        private Button[] asnwer_buttons;

        public Test( Psycho_Test choosen_test)
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            this.choosen_test = choosen_test;
            this.num_of_quest = this.choosen_test.amm_of_questions;
            this.selected_answer_id = new int[this.num_of_quest];
            this.questions = new Question[this.num_of_quest];
            this.choosen_test.questions.CopyTo(this.questions);
            this.asnwer_buttons = new Button[6] { answer_1, answer_2, answer_3, answer_4, answer_5, answer_6 };
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
                this.selected_answer_id[this.current_question - 1] = (int)this.selectedAnswer.Tag;
                this.current_question -= 1;
                this.Show_question_and_answers();
            }
            // Навигация назад
            //MessageBox.Show("Переход к предыдущему вопросу");
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.num_of_quest == this.current_question)
            {
                this.selected_answer_id[this.current_question - 1] = (int)this.selectedAnswer.Tag;
                Result result = new Result();
                result.Show();
                this.Close();
            }
            else
            {
                this.selected_answer_id[this.current_question - 1] = (int)this.selectedAnswer.Tag;
                this.current_question += 1;
                this.Show_question_and_answers();
            }
        }

        private void Show_question_and_answers()
        {
            Answer[] ans_to_quest = this.questions[this.current_question].answers.ToArray();
            for (int i = 0; i < ans_to_quest.Length; i++)
            {
                this.asnwer_buttons[i].Content = ans_to_quest[i].text;
            }
            if (this.selected_answer_id[this.current_question-1] != 0)
            {
                this.asnwer_buttons[this.selected_answer_id[this.current_question-1]-1].Style = (Style)FindResource("SelectedAnswerButtonStyle");
            }
            selectedAnswer.Style = (Style)FindResource("AnswerButtonStyle");
            this.selectedAnswer = null;
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