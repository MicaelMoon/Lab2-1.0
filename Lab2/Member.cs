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

        public override void CheckOut()
        {
            Console.Clear();
            double price = ShopingCart();

            if (Level != MemberLevel.Iron)
            {
                double newprice = Discount(price);
                Console.WriteLine($"\nYour member rank at our store is {Level} and you get a discount of {100 - (int)Level}%");
                switch (_currency)
                {
                    case Currency.SEK:
                        Console.WriteLine($"Total price including doscount is: {Math.Round(Item.ConvertToSEK(newprice), 2)} kr\n");
                        break;
                    case Currency.USD:
                        Console.WriteLine($"Total price including doscount is: ${Math.Round(Item.ConvertToUSD(newprice), 2)}\n");
                        break;
                    case Currency.Euro:
                        Console.WriteLine($"Total price including doscount is: {Math.Round(newprice, 2)} euro\n");
                        break;

                }
            }

            if (VerifyPassword(Password))
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    userCart[i].stack = 1;
                }

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
                        CheckOut();
                        break;
                    default:
                        Program.LoggedIn();
                        break;
                }
            }
        }

        public double Discount(double price)
        {
            double levelToDouble = (int)Level;
            price = price * (levelToDouble / 100);
            return price;
        }
    }
}
