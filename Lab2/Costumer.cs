﻿using System.IO;

namespace Lab2
{
    internal abstract class Costumer
    {
        private string username;
        private string password;
        public Currency _currency = Currency.SEK;
        public List<Item> userCart = new List<Item>();


        public Costumer(string username, string password)
        {
            this.username = username;
            this.password = password;
        }


        public string Username
        {
            get { return username; }
        }

        public string Password
        {
            get { return this.password; }
            set { password = value; } 
        }

        public override string ToString()
        {
            return $"Username: {Username}\nPassword: {Password}\nItem amount: {userCart.Count}";
        }

        public bool VerifyPassword(string password)
        {
            Console.Write("Please verify your password: ");
            string atempt = Console.ReadLine();

            if (atempt == Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GoShopping(int ID)
        {
            Console.Clear();
            Console.WriteLine("//Items avalable\\\\");
            Console.WriteLine("[0] Press to go back\n");

            //Currency check
            switch (_currency)
            {
                case Currency.SEK:
                    for (int i = 0; i < Item.itemList.Count; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {Item.itemList[i].name} = {Math.Round(Item.itemList[i].ConvertOriginalPriceToSEK(), 2)} kr");
                    }
                    break;
                case Currency.USD:
                    for (int i = 0; i < Item.itemList.Count; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {Item.itemList[i].name} = | $ {Math.Round(Item.itemList[i].ConvertOriginalPriceToUSD(), 2)}");
                    }
                    break;
                case Currency.Euro:
                    for (int i = 0; i < Item.itemList.Count; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {Item.itemList[i].name} = {Math.Round(Item.itemList[i].priceOriginal, 2)} euro");
                    }
                    break;
            }

            //Prints out what you've baught so far
            CompressStacks();

            Console.WriteLine($"********************************************************" +
                $"\n//Your cart contains\\\\\n");
            for (int i = 0; i < userCart.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {userCart[i].name} x{userCart[i].stack}");
            }

            //Adds chosen item to your cart
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice != 0)
                {
                    userCart.Add(Item.itemList[choice - 1]);
                    GoShopping(ID);
                }
                else if (choice == 0)
                {
                    CompressStacks();
                    Program.LoggedIn(ID);
                }
                else
                {
                    GoShopping(ID);
                }
            }
            catch
            {
                GoShopping(ID);
            }
        }

        public void CompressStacks()
        {
            for (int i = 0; i < userCart.Count; i++)    //Stacks all  equal items
            {
                for (int j = i + 1; j < userCart.Count; j++)
                {
                    if (userCart[i].name == userCart[j].name)
                    {
                        userCart[i].stack++;
                        userCart.RemoveAt(j);
                        j--;
                    }
                }
                userCart[i].price = userCart[i].priceOriginal * userCart[i].stack;
            }
        }       

        public double ShopingCart(int ID)
        {
            double totalCost = 0;
            Console.WriteLine("\nYour shoping cart contains\n");
            for (int i = 0; i < userCart.Count; i++)
            {
                Console.Write($"{userCart[i].name} x{userCart[i].stack}");

                switch (_currency)
                {
                    case Currency.SEK:
                        Console.WriteLine($" | {Math.Round(userCart[i].ConvertOriginalPriceToSEK(), 2)} kr a piece |" +
                    $" Stack price = {Math.Round(userCart[i].ConvertToSEK(), 2)}\n");
                        break;
                    case Currency.USD:
                        Console.WriteLine($" | ${Math.Round(userCart[i].ConvertOriginalPriceToUSD(), 2)} a piece |" +
                    $" Stack price = {Math.Round(userCart[i].ConvertToUSD(), 2)}\n");
                        break;
                    case Currency.Euro:
                        Console.WriteLine($" | {Math.Round(userCart[i].priceOriginal, 2)} euro a piece |" +
                    $" Stack price = {Math.Round(userCart[i].price, 2)}\n");
                        break;
                }   
            }

            foreach (Item i in userCart)
            {
                totalCost += i.price;
            }

            switch (_currency)
            {
                case Currency.SEK:
                    Console.WriteLine($"Total = {Math.Round(Item.ConvertToSEK(totalCost), 2)}");
                    break;
                case Currency.USD:
                    Console.WriteLine($"Total = {Math.Round(Item.ConvertToUSD(totalCost), 2)}");
                    break;
                case Currency.Euro:
                    Console.WriteLine($"Total = {Math.Round(totalCost, 2)}");
                    break;
            }

            return Math.Round(totalCost, 2);
        }

        public double ConvertCurency(double price)
        {
            Console.WriteLine("What curency would you like to use\n[1] SEK\n[2] USD\n[3] Euro");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    return Item.ConvertToSEK(price);
                    break;
                case "2":
                    return Item.ConvertToUSD(price);
                default:
                    return price;
                    break;
            }
        }
        public abstract void CheckOut(int ID);
    }
}
