using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Member : Costumer
    {
        private MemberLevel _level;


        public Member(string username, string password, MemberLevel level) : base(username, password)
        {
            _level = level;
        }

        public MemberLevel Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public virtual void CheckOut(int ID)
        {
            double price = Discount(ShopingCart(ID));
            Console.WriteLine($"You are {Level} and get a discount of {1-(int)Level}%");

            if (VerifyPassword(Password))
            {
                userCart.Clear();
                Console.WriteLine("Thank you for shoping. Have a nice day!");
                Console.ReadKey();
                Program.Menu();
            }
            else
            {
                Console.WriteLine("Wrong password, would you like to try again?\n[1] Yes\n[2] No");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CheckOut(ID);
                        break;
                    default:
                        Program.LoggedIn(ID);
                        break;
                }
            }
        }

        public static double Discount(double price)
        {
            price *= ((int)MemberLevel.Bronze*100);
            return price;
        }
    }
}
