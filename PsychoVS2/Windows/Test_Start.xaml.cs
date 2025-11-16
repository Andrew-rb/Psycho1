using Antlr.Runtime.Tree;
using System.Windows;

namespace PsychoVS2.Windows
{
    /// <summary>
    /// Логика взаимодействия для Test_choice.xaml
    /// </summary>
    public partial class Test_Start : Window
    {

        private Psycho_Test choosen_one;

        public Test_Start(Psycho_Test choosen_test)
        {
            this.choosen_one = choosen_test;
            InitializeComponent();
            this.Time_button.Content += "123 минуты" /*this.choosen_one.time*/;
            this.Quastion_quantity.Content += this.choosen_one.amm_of_questions.ToString() + " Вопросв";
            WindowState = WindowState.Maximized;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Test_choice testChoiceWindow = new Test_choice();
            testChoiceWindow.Show();
            this.Close();
        }

        private void StartTestButton_Click(object sender, RoutedEventArgs e)
        {
            
            Test Test = new Test(this.choosen_one);
            Test.Show();
            this.Close();
            
        }

    }
}