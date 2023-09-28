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

        public override void CheckOut(int ID)
        {
            Console.Clear();
            int currencyCheck = 0;
            double price = 0;
            Console.WriteLine("What currency would you like to pay in?\n[1] SEK\n[2] USD\n[3] Euro");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    price = Item.ConvertToSEK(ShopingCart(ID));
                    break;
                case "2":
                    price = Item.ConvertToUSD(ShopingCart(ID));
                    break;
                default:
                    ShopingCart(ID);
                    break;
            }

            if (Level != MemberLevel.Iron)
            {
                double newprice = Discount(price);
                Console.WriteLine($"\nYour member rank at our store is {Level} and you get a discount of {100 - (int)Level}%");
                switch (choice)
                {   
                    case "1":
                        Console.WriteLine($"Total price including doscount is: {newprice} kr\n");
                        break;
                    case "2":
                        Console.WriteLine($"Total price including doscount is: ${newprice}\n");
                        break;
                    default:
                        Console.WriteLine($"Total price including doscount is: {(newprice)}\n");
                        break;

                }
            }

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
                string choice1 = Console.ReadLine();
                switch (choice1)
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

        public double Discount(double price) ////******************Broken
        {
            price = price * ((int)Level / 10);  
            return price;
        }
    }
}
