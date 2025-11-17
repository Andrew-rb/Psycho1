using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using NCalc;

namespace PsychoVS2.Windows
{
    public partial class Result : Window
    {
        private Answer[] answers_array;
        private Dictionary<string, int> points;
        private int test_id;
        public Result(Answer[] array_of_answ, int test_id) {
            this.test_id = test_id;
            this.answers_array = array_of_answ;
            this.points = new Dictionary<string, int>();
            InitializeComponent();
            Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            // Запуск анимаций при загрузке окна
            this.Results();
            StartAnimations();
        }

        private void StartAnimations()
        {
            // Анимация появления заголовка
            var fadeIn = (Storyboard)FindResource("FadeInAnimation");
            fadeIn.Begin(TitleLabel);

            // Анимация появления блоков с результатами
            var slideIn1 = (Storyboard)FindResource("SlideInAnimation");
            Storyboard.SetTarget(slideIn1, GeneralResultsBorder);
            slideIn1.Begin();

            var slideIn2 = (Storyboard)FindResource("SlideInAnimation");
            Storyboard.SetTarget(slideIn2, DetailedResultsBorder);
            slideIn2.BeginTime = TimeSpan.FromSeconds(0.3);
            slideIn2.Begin();

            // Пульсирующая анимация для кнопки
            var pulse = (Storyboard)FindResource("PulseAnimation");
            pulse.RepeatBehavior = RepeatBehavior.Forever;
            pulse.Begin(ExitButton);

            // Анимация появления графика
            AnimateChart();
        }

        private void AnimateChart()
        {
            // Анимация столбцов графика
            var bar1Animation = new DoubleAnimation
            {
                To = 1,
                Duration = TimeSpan.FromSeconds(1),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                BeginTime = TimeSpan.FromSeconds(0.5)
            };

            var bar2Animation = new DoubleAnimation
            {
                To = 1,
                Duration = TimeSpan.FromSeconds(1),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                BeginTime = TimeSpan.FromSeconds(0.7)
            };

            var bar3Animation = new DoubleAnimation
            {
                To = 1,
                Duration = TimeSpan.FromSeconds(1),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                BeginTime = TimeSpan.FromSeconds(0.9)
            };

            var bar4Animation = new DoubleAnimation
            {
                To = 1,
                Duration = TimeSpan.FromSeconds(1),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                BeginTime = TimeSpan.FromSeconds(1.1)
            };

            Bar1.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, bar1Animation);
            Bar2.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, bar2Animation);
            Bar3.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, bar3Animation);
            Bar4.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, bar4Animation);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Анимация закрытия окна
            var closeAnimation = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            closeAnimation.Completed += (s, _) => Close();

            BeginAnimation(OpacityProperty, closeAnimation);
            Test_choice testChoiceWindow = new Test_choice();
            testChoiceWindow.Show();
            this.Close();
        }

        public void Results()
        {
            int length = this.answers_array.Length;
            for (int i = 0; i < length; i++)
            {
                Points_cods[] temp = this.answers_array[i].points_cods.ToArray();
                foreach (Points_cods point_code in temp)
                {
                    if (this.points.ContainsKey(point_code.type))
                    {
                        this.points[point_code.type] += point_code.value;
                        continue;
                    }
                    this.points.Add(point_code.type, point_code.value);
                }
            }
            bool alreafy = false;
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
                if (alreafy)
                    break;
                switch (this.check_condition(result.condition))
                {
                    case -1:
                        MessageBox.Show("Invalid condition, check BD!!!", "Condition failure");
                        Application.Current.Shutdown();
                        break;
                    case 0:
                        break;
                    case 1:
                        alreafy = true;
                        this.temp_storage_for_result.Content = result.result;
                        ///when desided what where, uncomment 
                        this.show_res_on_page_temp(result);
                        break;
                    default:
                        break;

                }
            }
        }

        /// uncomment when desided
        private void show_res_on_page_temp(Results valid_res)
        {
            this.temp_storage_for_result.Content = valid_res.result;
            this.Data_label.Content = $"Дата прохождения: {DateTime.Now:dd.MM.yyyy HH:mm}";
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