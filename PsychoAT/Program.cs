using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PsychoAT
{
   
    internal static class Program
    {

        public class Current_test
        {

        }

        public static  int window = 0;
        
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
}
