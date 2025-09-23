using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace PsychoAT
{

    /*Классы для хранения тестов*/

    //Test storage
    public class Psycho_Test
    {

        //для конкретного теста
        public Psycho_Test(int id, string title, string type = "none", string author = "none")
        {
            this.id = id;
            this.name = title;
            this.type = type;
            this.author = author;
        }

        public int id = -1; //id в БД
        public string name = "none"; // Название
        public string type = "none"; // Тип
        public string author = "none"; // Автор
        public int amm_of_questions = 0; // Количество вопросов в тесте

        public List<Question> questions = new List<Question>(0); //список вопросов

    }
    //Question storage
    public class Question
    {
        public int id = -1;
        public string text = "none";
        public List<Answer> answers = new List<Answer>(0);

    }
    //Answer storage
    public class Answer
    {
        public int id = -1;
        public string text = "none";
        public List<Points_cods> points_cods = new List<Points_cods>();
    }
    //Storage of type and value of points of current answer
    public class Points_cods
    {
        public int id = -1;
        public string type = "none";
        public int value = -1001;
    }



    /*Базовые установки
    1) Значения в классах по умолчанию означают, что они будут игнорироваться (далее пример)
    2) При загрузке всех тестов, загружается всё кроме набора вопросов к ним
    3) Комментарии пишем на русском


    /*Классы и методы загрузки тестов из БД*/
    public class DB_work
    {

        public List<Psycho_Test> tests = new List<Psycho_Test>(0);
        public Psycho_Test current_test = null;

        public string version = "3";
        private string dbPath = "Data Source=Psycho1\\PsychoAT\\tests.db;Version=3;";
        //that won't work, but latter it wil be correctly initialized
        private string connectionString = "";
        //command to connect
        //automatic search of PATH
        private void init_db_path()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            dbPath = Path.Combine(baseDir, @"..\..\tests.db");
            connectionString = $"Data Source={dbPath};Version={version};";
            MessageBox.Show(connectionString);
        }

        public void load_all_tests()
        {

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string table = "SELECT * FROM tests";   // Команда на таблицу тестов
                using (SQLiteCommand cmd = new SQLiteCommand(table, conn))
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {


                        int testId = Convert.ToInt32(reader["id"]);
                        string title = reader["title"].ToString();
                        string type = reader["type"].ToString();
                        string author = reader["author"].ToString();

                        tests.Add(new Psycho_Test(testId, title, type, author));

                        using (SQLiteCommand countCmd = new SQLiteCommand("SELECT COUNT(*) FROM questions WHERE test_id = @id", conn))
                        {
                            countCmd.Parameters.AddWithValue("@id", testId);
                            tests[tests.Count - 1].amm_of_questions = Convert.ToInt32(countCmd.ExecuteScalar());
                        }
                    }
                }
            }
        }
        public void load_current_test(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM tests WHERE id = @id";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int testId = Convert.ToInt32(reader["id"]);
                            string title = reader["title"].ToString();
                            string type = reader["type"].ToString();
                            string author = reader["author"].ToString();
                            current_test = new Psycho_Test(testId, title, type, author);

                            using (SQLiteCommand countCmd = new SQLiteCommand("SELECT COUNT(*) FROM questions WHERE test_id = @id", conn))
                            {
                                countCmd.Parameters.AddWithValue("@id", testId);
                                current_test.amm_of_questions = Convert.ToInt32(countCmd.ExecuteScalar());
                            }

                            return;
                        }
                    }
                }
            }
            current_test = null; // если нет такого id
            return;
        }

        public void show_all_tests()
        {
            string output = "";
            foreach (Psycho_Test a in tests)
            {
                output += a.id.ToString() + " | " + a.name + " | " + current_test.type + " | " + current_test.author + "| вопросов: " + a.amm_of_questions + "\n";
            }
            MessageBox.Show(output);
        }
        public void show_current_test()
        {
            if (current_test == null)
            {
                MessageBox.Show("NULL");
                return;
            }
            string output = current_test.id.ToString() + " | " + current_test.name + " | " + current_test.type + " | " + current_test.author + "| вопросов: " + current_test.amm_of_questions + "\n";
            MessageBox.Show(output);
        }

        public DB_work()
        {
            init_db_path();
        }
    }





    internal static class Program
    {
        public static DB_work db = new DB_work();
        public static Work_with_test_choice_page Test_choise_logic = new Work_with_test_choice_page();


        public class Current_test
        {

        }

        public static int window = 0;

        public static Start w_Start;
        public static Test_choice w_Test_Choice;
        public static Test_Start w_Test_Start;
        public static Test w_Test;
        public static Result w_Result;
        public static Statistics w_Stat;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            db.load_all_tests();
            db.load_current_test(1);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            w_Start = new Start();
            w_Test_Choice = new Test_choice();
            w_Test = new Test();
            w_Test_Start = new Test_Start();
            w_Result = new Result();
            w_Stat = new Statistics();


            Application.Run(w_Start);


        }
    }

    class Work_with_test_choice_page
    {
        public Work_with_test_choice_page() { }
        private short size = 0, pages = 0, size_last = 0, size_already = 0;

        private void Init_size_and_pages(DB_work DB_data)
        {
            foreach (Psycho_Test a in DB_data.tests)
            {
                this.size++;
            }
            this.size_last = this.size;
            this.pages = (short)(this.size / 5);
        }

        private short Adapt_button_on_page()
        {
            if (this.size_last <= 5)
                return this.size_last;
            else {
                this.size_last -= 5;
                this.size_already = 5;
                return 5;
            }
        }

        public void Initial_Show_tests_on_page(DB_work DB_data, Test_choice Test_choise_window)
        {
            this.Init_size_and_pages(DB_data);
            short buttons_on_page = this.Adapt_button_on_page();
            
        }
    }
}
