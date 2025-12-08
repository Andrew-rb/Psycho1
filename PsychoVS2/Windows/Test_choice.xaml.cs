using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

        private Psycho_Test[] tests;

        public static DB_work db = new DB_work();
        public Test_choice()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            this.tests = db.tests.ToArray();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            Button selectedTest = (Button)sender;
            Test_Start testStartWindow = new Test_Start(db.load_current_test(this.tests[(int)selectedTest.Tag].id));
            testStartWindow.Show();
            this.Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Show_test_on_page();
        }

        private void Show_test_on_page()
        {
            for (int i = 0; i < this.tests.Length; i++)
            {
                /*if (this.tests[i] != null)
                {
                    this.Cards[i].Visibility = Visibility.Visible;
                    this.Authors_labels[i].Content = this.tests[i].author;
                    this.Descriptions_labels[i].Content = this.tests[i].description;
                    this.Test_names_labels[i].Content = this.tests[i].name;
                    this.Numb_of_questions_labels[i].Content = this.tests[i].amm_of_questions.ToString() + " вопросов";
                    this.Type_of_test_labels[i].Content = this.tests[i].type;
                    this.Estemated_timr_labels[i].Content = this.tests[i].estemated_time; // rewrite after bd update
                    this.Images[i].Source = this.tests[i].image;
                }
                else
                {
                    this.Cards[i].Visibility = Visibility.Hidden;
                }*/
                this.Panel_for_buttons.Children.Add(this.Create_button(i, this.tests[i]));
            }
        }

        private Button Create_button(int id_of_but, Psycho_Test test_for_content)
        {
            var button = new Button
            {
                Tag = id_of_but,
                Name = $"Card_{id_of_but}",
                Width = 349,
                Margin = new Thickness(2),
                Height = 448,
                Background = Brushes.White,
                Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF3A454B"),
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };

            button.Click += AnswerButton_Click;

            var template = new ControlTemplate(typeof(Button));

            // Root: rounded border (no shadow)
            var border = new FrameworkElementFactory(typeof(Border));
            border.Name = "border";
            border.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Button.BackgroundProperty));
            border.SetValue(Border.CornerRadiusProperty, new CornerRadius(20));
            border.SetValue(Border.SnapsToDevicePixelsProperty, true);

            // Grid layout
            var grid = new FrameworkElementFactory(typeof(Grid));
            grid.Name = "Card_1_Grid";

            AddRow(grid, new GridLength(180));                 // Image
            AddRow(grid, new GridLength(30));                  // Badges
            AddRow(grid, new GridLength(30));                  // Test name
            AddRow(grid, new GridLength(145));                 // Description
            AddRow(grid, new GridLength(20));                  // Spacer
            AddRow(grid, new GridLength(1));                   // Line
            AddRow(grid, new GridLength(5));                   // Spacer
            AddRow(grid, new GridLength(1, GridUnitType.Star));// Authors

            // Lower background block
            var lowerBg = new FrameworkElementFactory(typeof(Border));
            lowerBg.SetValue(Grid.RowProperty, 2);
            lowerBg.SetValue(Grid.RowSpanProperty, 7);
            lowerBg.SetValue(Border.BackgroundProperty, (SolidColorBrush)new BrushConverter().ConvertFromString("#FF657C89"));
            lowerBg.SetValue(Border.CornerRadiusProperty, new CornerRadius(0, 0, 20, 20));
            lowerBg.SetValue(FrameworkElement.MarginProperty, new Thickness(0, -20, 0, 0));
            grid.AppendChild(lowerBg);

            // Center image
            var image = new FrameworkElementFactory(typeof(Image));
            image.Name = "Card_1_Image";
            image.SetValue(Image.SourceProperty, test_for_content.image);
            image.SetValue(FrameworkElement.WidthProperty, 167.0);
            image.SetValue(FrameworkElement.HeightProperty, 200.0);
            image.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            image.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);
            image.SetValue(Image.StretchProperty, Stretch.Uniform);
            grid.AppendChild(image);

            // Time badge (top-right)
            var timeBorder = new FrameworkElementFactory(typeof(Border));
            timeBorder.Name = "Card_1_BorderTime";
            timeBorder.SetValue(Grid.RowProperty, 0);
            timeBorder.SetValue(Border.BackgroundProperty, (SolidColorBrush)new BrushConverter().ConvertFromString("#FF6590A7"));
            timeBorder.SetValue(Border.CornerRadiusProperty, new CornerRadius(10));
            timeBorder.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Right);
            timeBorder.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Top);
            timeBorder.SetValue(FrameworkElement.MarginProperty, new Thickness(15, 15, 15, 0));
            timeBorder.SetValue(Border.PaddingProperty, new Thickness(8, 4, 8, 4));

            var timeStack = new FrameworkElementFactory(typeof(StackPanel));
            timeStack.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            var timePath = new FrameworkElementFactory(typeof(System.Windows.Shapes.Path));
            timePath.SetValue(System.Windows.Shapes.Path.DataProperty, Geometry.Parse("M12,2A10,10 0 0,0 2,12A10,10 0 0,0 12,22A10,10 0 0,0 22,12A10,10 0 0,0 12,2M16.2,16.2L11,13V7H12.5V12.2L17,14.9L16.2,16.2Z"));
            timePath.SetValue(Shape.FillProperty, (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFFCA28"));
            timePath.SetValue(FrameworkElement.WidthProperty, 12.0);
            timePath.SetValue(FrameworkElement.HeightProperty, 12.0);
            timePath.SetValue(FrameworkElement.MarginProperty, new Thickness(0, 0, 4, 0));

            var timeLabel = new FrameworkElementFactory(typeof(Label));
            timeLabel.Name = "Card_1_LabelTime";
            timeLabel.SetValue(ContentControl.ContentProperty, $"{test_for_content.estemated_time} мин");
            timeLabel.SetValue(Control.FontSizeProperty, 11.0);
            timeLabel.SetValue(Control.FontWeightProperty, FontWeights.Medium);
            timeLabel.SetValue(Control.ForegroundProperty, Brushes.White);
            timeLabel.SetValue(Control.FontFamilyProperty, new FontFamily("Montserrat"));
            timeLabel.SetValue(Control.PaddingProperty, new Thickness(0));
            timeLabel.SetValue(Control.BackgroundProperty, Brushes.Transparent);
            timeLabel.SetValue(Control.BorderThicknessProperty, new Thickness(0));

            timeStack.AppendChild(timePath);
            timeStack.AppendChild(timeLabel);
            timeBorder.AppendChild(timeStack);
            grid.AppendChild(timeBorder);

            // Badges row under image
            var badgesRow = new FrameworkElementFactory(typeof(StackPanel));
            badgesRow.SetValue(Grid.RowProperty, 1);
            badgesRow.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
            badgesRow.SetValue(FrameworkElement.MarginProperty, new Thickness(12, 5, 12, 0));
            if (test_for_content.type != "")
            {
                var typeBorder = new FrameworkElementFactory(typeof(Border));
                typeBorder.Name = "Card_1_BorderTypeTest";
                typeBorder.SetValue(Border.BackgroundProperty, (SolidColorBrush)new BrushConverter().ConvertFromString("#FF6590A7"));
                typeBorder.SetValue(Border.CornerRadiusProperty, new CornerRadius(10));
                typeBorder.SetValue(Border.PaddingProperty, new Thickness(8, 4, 8, 4));
                typeBorder.SetValue(FrameworkElement.MarginProperty, new Thickness(0, 0, 8, 0));

                var typeStack = new FrameworkElementFactory(typeof(StackPanel));
                typeStack.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

                var typeLabel = new FrameworkElementFactory(typeof(Label));
                typeLabel.Name = "Card_1_LabelTypeTest";
                typeLabel.SetValue(ContentControl.ContentProperty, test_for_content.type);
                typeLabel.SetValue(Control.FontSizeProperty, 11.0);
                typeLabel.SetValue(Control.FontWeightProperty, FontWeights.Medium);
                typeLabel.SetValue(Control.ForegroundProperty, Brushes.White);
                typeLabel.SetValue(Control.FontFamilyProperty, new FontFamily("Montserrat"));
                typeLabel.SetValue(Control.PaddingProperty, new Thickness(0));
                typeLabel.SetValue(Control.BackgroundProperty, Brushes.Transparent);
                typeLabel.SetValue(Control.BorderThicknessProperty, new Thickness(0));

                typeStack.AppendChild(typeLabel);
                typeBorder.AppendChild(typeStack);
                badgesRow.AppendChild(typeBorder);
            }

            var qBorder = new FrameworkElementFactory(typeof(Border));
            qBorder.Name = "Card_1_BorderQuestions";
            qBorder.SetValue(Border.BackgroundProperty, (SolidColorBrush)new BrushConverter().ConvertFromString("#FF6590A7"));
            qBorder.SetValue(Border.CornerRadiusProperty, new CornerRadius(10));
            qBorder.SetValue(Border.PaddingProperty, new Thickness(8, 4, 8, 4));
            qBorder.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Right);

            var qStack = new FrameworkElementFactory(typeof(StackPanel));
            qStack.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            var qPath = new FrameworkElementFactory(typeof(System.Windows.Shapes.Path));
            qPath.SetValue(System.Windows.Shapes.Path.DataProperty, Geometry.Parse("M15.07,11.25L14.17,12.17C13.45,12.89 13,13.5 13,15H11V14.5C11,13.39 11.45,12.39 12.17,11.67L13.41,10.41C13.78,10.05 14,9.55 14,9C14,7.89 13.1,7 12,7A2,2 0 0,0 10,9H8A4,4 0 0,1 12,5A4,4 0 0,1 16,9C16,9.88 15.64,10.67 15.07,11.25M13,19H11V17H13M12,2A10,10 0 0,0 2,12A10,10 0 0,0 12,22A10,10 0 0,0 22,12C22,6.47 17.5,2 12,2Z"));
            qPath.SetValue(Shape.FillProperty, (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFFCA28"));
            qPath.SetValue(FrameworkElement.WidthProperty, 12.0);
            qPath.SetValue(FrameworkElement.HeightProperty, 12.0);
            qPath.SetValue(FrameworkElement.MarginProperty, new Thickness(0, 0, 4, 0));

            var qLabel = new FrameworkElementFactory(typeof(Label));
            qLabel.Name = "Card_1_LabelQuestions";
            qLabel.SetValue(ContentControl.ContentProperty, $"{test_for_content.amm_of_questions} вопросов");
            qLabel.SetValue(Control.FontSizeProperty, 11.0);
            qLabel.SetValue(Control.FontWeightProperty, FontWeights.Medium);
            qLabel.SetValue(Control.ForegroundProperty, Brushes.White);
            qLabel.SetValue(Control.FontFamilyProperty, new FontFamily("Montserrat"));
            qLabel.SetValue(Control.PaddingProperty, new Thickness(0));
            qLabel.SetValue(Control.BackgroundProperty, Brushes.Transparent);
            qLabel.SetValue(Control.BorderThicknessProperty, new Thickness(0));

            qStack.AppendChild(qPath);
            qStack.AppendChild(qLabel);
            qBorder.AppendChild(qStack);

            badgesRow.AppendChild(qBorder);
            grid.AppendChild(badgesRow);

            // Test name
            var nameLabel = new FrameworkElementFactory(typeof(Label));
            nameLabel.Name = "Card_1_LabelNameTest";
            nameLabel.SetValue(Grid.RowProperty, 2);
            nameLabel.SetValue(ContentControl.ContentProperty, test_for_content.name);
            nameLabel.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Left);
            nameLabel.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);
            nameLabel.SetValue(Control.FontSizeProperty, 17.0);
            nameLabel.SetValue(Control.FontWeightProperty, FontWeights.Medium);
            nameLabel.SetValue(Control.ForegroundProperty, Brushes.White);
            nameLabel.SetValue(FrameworkElement.MarginProperty, new Thickness(12, 0, 0, 0));
            nameLabel.SetValue(Control.FontFamilyProperty, new FontFamily("Montserrat"));
            nameLabel.SetValue(Control.PaddingProperty, new Thickness(0));
            nameLabel.SetValue(Control.BackgroundProperty, Brushes.Transparent);
            nameLabel.SetValue(Control.BorderThicknessProperty, new Thickness(0));
            grid.AppendChild(nameLabel);

            // Description (simplified: TextBlock inside ScrollViewer with same styling)
            var sv = new FrameworkElementFactory(typeof(ScrollViewer));
            sv.SetValue(Grid.RowProperty, 3);
            sv.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            sv.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
            sv.SetValue(FrameworkElement.MarginProperty, new Thickness(13, 2, 15, 0));

            var descText = new FrameworkElementFactory(typeof(TextBlock));
            descText.SetValue(TextBlock.TextProperty, test_for_content.description);
            descText.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
            descText.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Justify);
            descText.SetValue(TextBlock.FontFamilyProperty, new FontFamily("Montserrat"));
            descText.SetValue(TextBlock.FontSizeProperty, 14.0);
            descText.SetValue(TextBlock.ForegroundProperty, (SolidColorBrush)new BrushConverter().ConvertFromString("#FFB6B6B6"));

            sv.AppendChild(descText);
            grid.AppendChild(sv);

            // Bottom separator line
            var line = new FrameworkElementFactory(typeof(Rectangle));
            line.SetValue(Grid.RowProperty, 5);
            line.SetValue(FrameworkElement.HeightProperty, 1.0);
            line.SetValue(Shape.FillProperty, (SolidColorBrush)new BrushConverter().ConvertFromString("#FFE0E0E0"));
            line.SetValue(FrameworkElement.MarginProperty, new Thickness(15, 0, 15, 0));
            line.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Bottom);
            grid.AppendChild(line);

            // Authors row
            var authorsRow = new FrameworkElementFactory(typeof(StackPanel));
            authorsRow.SetValue(Grid.RowProperty, 7);
            authorsRow.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
            authorsRow.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Left);
            authorsRow.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);
            authorsRow.SetValue(FrameworkElement.MarginProperty, new Thickness(12, 0, 0, 0));

            var authorPath = new FrameworkElementFactory(typeof(System.Windows.Shapes.Path));
            authorPath.SetValue(System.Windows.Shapes.Path.DataProperty, Geometry.Parse("M12,4A4,4 0 0,1 16,8A4,4 0 0,1 12,12A4,4 0 0,1 8,8A4,4 0 0,1 12,4M12,14C16.42,14 20,15.79 20,18V20H4V18C4,15.79 7.58,14 12,14Z"));
            authorPath.SetValue(Shape.FillProperty, (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFFCA28"));
            authorPath.SetValue(FrameworkElement.WidthProperty, 14.0);
            authorPath.SetValue(FrameworkElement.HeightProperty, 14.0);
            authorPath.SetValue(FrameworkElement.MarginProperty, new Thickness(0, 0, 6, 0));
            authorPath.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);

            var authorLabel = new FrameworkElementFactory(typeof(Label));
            authorLabel.Name = "Card_1_LabelAuthor";
            authorLabel.SetValue(ContentControl.ContentProperty, test_for_content.author);
            authorLabel.SetValue(Control.FontSizeProperty, 11.0);
            authorLabel.SetValue(Control.FontWeightProperty, FontWeights.Medium);
            authorLabel.SetValue(Control.ForegroundProperty, Brushes.White);
            authorLabel.SetValue(Control.FontFamilyProperty, new FontFamily("Montserrat"));
            authorLabel.SetValue(Control.PaddingProperty, new Thickness(2, 0, 2, 0));
            authorLabel.SetValue(Control.BackgroundProperty, Brushes.Transparent);
            authorLabel.SetValue(Control.BorderThicknessProperty, new Thickness(0));
            authorLabel.SetValue(FrameworkElement.WidthProperty, 303.0);

            authorsRow.AppendChild(authorPath);
            authorsRow.AppendChild(authorLabel);
            grid.AppendChild(authorsRow);

            // Compose template
            border.AppendChild(grid);
            template.VisualTree = border;

            // No triggers or animations; keep template static
            button.Template = template;

            return button;
        }

        private static void AddRow(FrameworkElementFactory grid, GridLength height)
        {
            var rd = new FrameworkElementFactory(typeof(RowDefinition));
            rd.SetValue(RowDefinition.HeightProperty, height);
            grid.AppendChild(rd);
        }
    }
}