using PsychoVS2.Windows;
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

namespace PsychoVS2
{
    /// <summary>
    /// Логика взаимодействия для New_result_window.xaml
    /// </summary>
    public partial class New_result_window : Window
    {
        private Dictionary<string, int> points;
        private int test_id;
        public New_result_window(Dictionary<string, int> points_dict, int test_id)
        {
            this.test_id = test_id;
            this.points = points_dict;
            InitializeComponent();
            this.Results();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Test_choice testChoiceWindow = new Test_choice();
            testChoiceWindow.Show();
            this.Close();
        }

        public void Results()
        {
            List<String> Array_of_already_done_points = new List<String>();
            Results[] array_of_a_resaults = PsychoVS2.Windows.Test_choice.db.get_results(this.test_id);
            string text = "";
            foreach (var vk in this.points)
            {
                text += vk.Key.ToString() + vk.Value.ToString();
            }
            MessageBox.Show(text, "dadsad");
            foreach (Results result in array_of_a_resaults)
            {
            MessageBox.Show(result.condition);
            if (Array_of_already_done_points.Count != 0 && Array_of_already_done_points.Contains("")) { break; }
            else if (Array_of_already_done_points.Count != 0 && Array_of_already_done_points.Contains(result.)) { continue; }
                switch (this.check_condition(result.condition))
                {
                    case -1:
                        MessageBox.Show("Invalid condition, check BD!!!", "Condition failure");
                        Application.Current.Shutdown();
                        break;
                    case 0:
                        break;
                    case 1:
                        Array_of_already_done_points.Add(""/*result.smt_for_point*/);
                        Label new_res = new Label
                        {
                            Content = result.result,
                            FontSize = 14,
                            Background = Brushes.Gray
                            
                        };
                        this.Labels_Panel.Children.Add(new_res);
                        break;
                    default:
                        break;
                    }
            }
        }

        private int check_condition(string expression)
        {
            try
            {
                NCalc.Expression expr = new NCalc.Expression(expression);
                foreach (var kv in this.points)
                {
                    expr.Parameters[kv.Key] = kv.Value;
                }
                object result = expr.Evaluate();
                if (result is bool b) // result возвращается как object
                {
                    return b ? 1 : 0;
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
        }
    }
}
