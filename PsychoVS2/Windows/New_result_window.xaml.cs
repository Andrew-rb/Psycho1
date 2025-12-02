using PsychoVS2;
using PsychoVS2.Windows;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace PsychoVS2
{
    /// <summary>
    /// Interaction logic for New_result_window.xaml
    /// </summary>
    public partial class New_result_window : Window
    {
        private int test_id;
        private Dictionary<string, int> points;

        public New_result_window(Dictionary<string, int> input_points, int test_id)
        {
            InitializeComponent();
            this.test_id = test_id;
            this.points = input_points;
            WindowState = WindowState.Maximized;
            Show_results();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Test_choice choise_in = new Test_choice();
            choise_in.Show();
            this.Close();
        }

        private void Show_results()
        {
            List<string> Array_of_already_done_points = new List<string>();
            Results[] array_of_a_resaults = PsychoVS2.Windows.Test_choice.db.get_results(this.test_id);
            string text = "";
            foreach (var vk in this.points)
            {
                text += vk.Key.ToString() + vk.Value.ToString();
            }
            MessageBox.Show(text, "dadsad");
            foreach (Results result in array_of_a_resaults)
            {
                if (Array_of_already_done_points.Count != 0 && Array_of_already_done_points.Contains("")) break;
                if (Array_of_already_done_points.Count != 0 && Array_of_already_done_points.Contains(result.point_type)) continue;
                switch (this.check_condition(result.condition))
                {
                    case -1:
                        MessageBox.Show("Invalid condition, check BD!!!", "Condition failure");
                        Application.Current.Shutdown();
                        break;
                    case 0:
                        break;
                    case 1:
                        Array_of_already_done_points.Add(result.point_type);
                        Border resultContainer = new Border
                        {
                            CornerRadius = new CornerRadius(10),
                            Background = Brushes.White,
                            BorderBrush = new SolidColorBrush(Color.FromRgb(224, 224, 224)),
                            BorderThickness = new Thickness(1),
                            Padding = new Thickness(20),
                            Margin = new Thickness(0, 0, 0, 15),
                            Effect = new DropShadowEffect
                            {
                                BlurRadius = 8,
                                Opacity = 0.05,
                                ShadowDepth = 2
                            }
                        };
                        StackPanel contentPanel = new StackPanel
                        {
                            Orientation = Orientation.Vertical
                        };
                        TextBlock titleText = new TextBlock
                        {
                            Text = result.title,
                            FontSize = 18,
                            FontWeight = FontWeights.SemiBold,
                            Foreground = new SolidColorBrush(Color.FromRgb(25, 118, 210)),
                            Margin = new Thickness(0, 0, 0, 8),
                            TextAlignment = TextAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center
                        };
                        TextBlock descriptionText = new TextBlock
                        {
                            Text = result.description,
                            FontSize = 16,
                            Foreground = new SolidColorBrush(Color.FromRgb(51, 51, 51)),
                            TextWrapping = TextWrapping.Wrap,
                            TextAlignment = TextAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center
                        };
                        Border separator = new Border
                        {
                            Height = 1,
                            Background = new SolidColorBrush(Color.FromRgb(224, 224, 224)),
                            Margin = new Thickness(0, 10, 0, 10),
                            HorizontalAlignment = HorizontalAlignment.Stretch
                        };
                        contentPanel.Children.Add(titleText);
                        contentPanel.Children.Add(separator);
                        contentPanel.Children.Add(descriptionText);

                        resultContainer.Child = contentPanel;
                        this.Labels_Panel.Children.Add(resultContainer);
                        break;
                    default:
                        break;
                }
            }

            /*if (this.Labels_Panel.Children.Count == 0)
            {
                TextBlock noResultsText = new TextBlock
                {
                    Text = "Нет результатов для отображения",
                    FontSize = 18,
                    Foreground = new SolidColorBrush(Color.FromRgb(117, 117, 117)),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontStyle = FontStyles.Italic
                };

                this.Labels_Panel.Children.Add(noResultsText);
            }*/
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