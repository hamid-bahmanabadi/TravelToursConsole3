using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelTourConsole.Interface;

namespace TravelTourConsole.Services
{
    public class Registration_Login_Service : IRandL
    {
        public bool login(string UserName, string password)
        {
            if (UserName == "admin" && password == "1234")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("welcome back {0}", UserName);
                Console.WriteLine("Continue... Tep Enter");
                Console.ReadKey();
                return true;
               

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The password or username is incorrect.");
                Console.WriteLine("came back...Tap Enter");
                Console.ReadKey();
                return false;
            }
        }

        public void Registration(string UserName, string Password)
        {
            Console.WriteLine("Welcome {0}",UserName);
            Console.WriteLine("Countine... Tap Enter");
            Console.ReadKey();
          
        }










        public void Run()
        {
            Console.Clear();
            Console.WriteLine("******* SubMenu *******");
            Console.WriteLine("11.Registeration");
            Console.WriteLine("12.Login");
            string input = Console.ReadLine();
            if (input == "11" || input == "Registeration")
            {

                Console.WriteLine("Enter UserName?");
                string input_UserName = Console.ReadLine();
                Console.WriteLine("Enter password?");
                string input_Password = Console.ReadLine();
                Registration(input_UserName, input_Password);
                

            }
            else if(input == "12" || input == "Login")
            {
                Console.WriteLine("Enter UserName?");
                string input_UserName = Console.ReadLine();
                Console.WriteLine("Enter password?");
                string input_Password = Console.ReadLine();
                login(input_UserName, input_Password);
                //میتونیم این تابع ای که تعریف کردم رو بریزم تو متغیر بول

                // ولی گفتم اصول شی گرایی رو رعایت کنم

                //مثال

                //bool islogin = login(input_UserName, input_Password);
                //if(islogin==true){پیام تبریک بازگشت}
                //else{رمز ی نام کاربری اشتباه است}

            }
        }
        
    }
}
