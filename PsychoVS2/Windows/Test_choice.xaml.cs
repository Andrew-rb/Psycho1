using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PsychoVS2.Windows
{
    /// <summary>
    /// Логика взаимодействия для Test_Start.xaml
    /// </summary>
    public partial class Test_choice : Window
    {


        /// <summary>
        /// ----------------------------Егору----------------------
        /// Если сильно задолбают надписи отладки, убери из события loaded здесь message box.
        /// А из кнопки MainWindow команды на show test
        /// </summary>
        /// 
        ///Card_1_LabelAuthor, Card_1_LabelDescription, Card_1_LabelNameTest, Card_1_LabelQuestions, Card_1_LabelTypeTest, Card_1_LabelTime, Card_1_Image

        private Label[] Authors_labels;
        private Label[] Descriptions_labels;
        private Label[] Test_names_labels;
        private Label[] Numb_of_questions_labels;
        private Label[] Type_of_test_labels;
        private Label[] Estemated_timr_labels;
        private Image[] Images;
        private Psycho_Test[] tests = new Psycho_Test[db.number_of_pages*8];
        private Button[] Cards;
        private int current_page_numb = 1;

        public static DB_work db = new DB_work();
        public Test_choice()
        {
            db.load_all_tests();
            InitializeComponent();
            this.Authors_labels = new Label[8] { Card_1.Template.FindName("Card_1_LabelAuthor", Card_1) as Label, Card_2.Template.FindName("Card_2_LabelAuthor", Card_2) as Label,
                Card_3.Template.FindName("Card_3_LabelAuthor", Card_3) as Label, Card_4.Template.FindName("Card_4_LabelAuthor", Card_4) as Label,
                Card_5.Template.FindName("Card_5_LabelAuthor", Card_5) as Label, Card_6.Template.FindName("Card_6_LabelAuthor", Card_6) as Label, 
                Card_7.Template.FindName("Card_7_LabelAuthor", Card_7) as Label, Card_8.Template.FindName("Card_8_LabelAuthor", Card_8) as Label };
            this.Descriptions_labels = new Label[8] { Card_1.Template.FindName("Card_1_LabelDescription", Card_1) as Label, Card_2.Template.FindName("Card_2_LabelDescription", Card_2) as Label,
                Card_3.Template.FindName("Card_3_LabelDescription", Card_3) as Label, Card_4.Template.FindName("Card_4_LabelDescription", Card_4) as Label,
                Card_5.Template.FindName("Card_5_LabelDescription", Card_5) as Label, Card_6.Template.FindName("Card_6_LabelDescription", Card_6) as Label,
                Card_7.Template.FindName("Card_7_LabelDescription", Card_7) as Label, Card_8.Template.FindName("Card_8_LabelDescription", Card_8) as Label };
            this.Test_names_labels = new Label[8] { Card_1.Template.FindName("Card_1_LabelNameTest", Card_1) as Label, Card_2.Template.FindName("Card_2_LabelNameTest", Card_2) as Label,
                Card_3.Template.FindName("Card_3_LabelNameTest", Card_3) as Label, Card_4.Template.FindName("Card_4_LabelNameTest", Card_4) as Label,
                Card_5.Template.FindName("Card_5_LabelNameTest", Card_5) as Label, Card_6.Template.FindName("Card_6_LabelNameTest", Card_6) as Label,
                Card_7.Template.FindName("Card_7_LabelNameTest", Card_7) as Label, Card_8.Template.FindName("Card_8_LabelNameTest", Card_8) as Label };
            this.Numb_of_questions_labels = new Label[8] { Card_1.Template.FindName("Card_1_LabelQuestions", Card_1) as Label, Card_2.Template.FindName("Card_2_LabelQuestions", Card_2) as Label,
                Card_3.Template.FindName("Card_3_LabelQuestions", Card_3) as Label, Card_4.Template.FindName("Card_4_LabelQuestions", Card_4) as Label,
                Card_5.Template.FindName("Card_5_LabelQuestions", Card_5) as Label, Card_6.Template.FindName("Card_6_LabelQuestions", Card_6) as Label,
                Card_7.Template.FindName("Card_7_LabelQuestions", Card_7) as Label, Card_8.Template.FindName("Card_8_LabelQuestions", Card_8) as Label };
            this.Type_of_test_labels = new Label[8] { Card_1.Template.FindName("Card_1_LabelTypeTest", Card_1) as Label, Card_2.Template.FindName("Card_2_LabelTypeTest", Card_2) as Label,
                Card_3.Template.FindName("Card_3_LabelTypeTest", Card_3) as Label, Card_4.Template.FindName("Card_4_LabelTypeTest", Card_4) as Label,
                Card_5.Template.FindName("Card_5_LabelTypeTest", Card_5) as Label, Card_6.Template.FindName("Card_6_LabelTypeTest", Card_6) as Label,
                Card_7.Template.FindName("Card_7_LabelTypeTest", Card_7) as Label, Card_8.Template.FindName("Card_8_LabelTypeTest", Card_8) as Label };
            this.Estemated_timr_labels= new Label[8] { Card_1.Template.FindName("Card_1_LabelTime", Card_1) as Label, Card_2.Template.FindName("Card_2_LabelTime", Card_2) as Label,
                Card_3.Template.FindName("Card_3_LabelTime", Card_3) as Label, Card_4.Template.FindName("Card_4_LabelTime", Card_4) as Label,
                Card_5.Template.FindName("Card_5_LabelTime", Card_5) as Label, Card_6.Template.FindName("Card_6_LabelTime", Card_6) as Label,
                Card_7.Template.FindName("Card_7_LabelTime", Card_7) as Label, Card_8.Template.FindName("Card_8_LabelTime", Card_8) as Label };
            this.Images = new Image[8] { Card_1.Template.FindName("Card_1_Image", Card_1) as Image, Card_2.Template.FindName("Card_2_Image", Card_2) as Image,
                Card_3.Template.FindName("Card_3_Image", Card_3) as Image, Card_4.Template.FindName("Card_4_Image", Card_4) as Image,
                Card_5.Template.FindName("Card_5_Image", Card_5) as Image, Card_6.Template.FindName("Card_6_Image", Card_6) as Image,
                Card_7.Template.FindName("Card_7_Image", Card_7) as Image, Card_8.Template.FindName("Card_8_Image", Card_8) as Image };
            this.Cards = new Button[8] { Card_1, Card_2, Card_3, Card_4, Card_5, Card_6, Card_7, Card_8 };
            db.tests.CopyTo(this.tests);
            this.Show_test_on_page();
            WindowState = WindowState.Maximized;
            

        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            Button selectedTest = (Button)sender;
            Test_Start testStartWindow = new Test_Start(db.load_current_test(this.tests[(int)selectedTest.Tag].id));
            testStartWindow.Show();
            this.Close();
        }

        private void Button_One_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var img = Card_2.Template.FindName("Card_2_Image", Card_2) as Image;
            if (img != null)
            {
                img.Source = db.current_test.image;
                MessageBox.Show("loaded");
            }
        }

        private void Show_test_on_page()
        {
            int last_Test = current_page_numb * 8;
            for (int i = (current_page_numb - 1) * 8; i < last_Test; i++)
            {
                if (this.tests[i] != null)
                {
                    this.Cards[i].Visibility = Visibility.Visible;
                    this.Authors_labels[i].Content = this.tests[i].author;
                    this.Descriptions_labels[i].Content = this.tests[i].description;
                    this.Test_names_labels[i].Content = this.tests[i].name;
                    this.Numb_of_questions_labels[i].Content = this.tests[i].amm_of_questions.ToString() + " впросов";
                    this.Type_of_test_labels[i].Content = this.tests[i].type;
                    this.Estemated_timr_labels[i].Content = "20 минут"; // rewrite after bd update
                    this.Images[i].Source = this.tests[i].image;
                }
                else
                {
                    this.Cards[i].Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
