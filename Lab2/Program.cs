using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;

namespace Lab2  //For next time: plays appropriet methods inside appropriate class as class method
{               //Future potention problem | When sutomer is done shoping the price after increasing stack might need to be reset
    internal class Program
    {
        public static List<Item> itemList = new List<Item>();
        public static List<Costumer> userList = new List<Costumer>();
        public static int userAmount = 3;


        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("[1] Log in\n[2] Sign up");
            string choice = (Console.ReadLine());

            switch (choice)
            {
                case "1":
                    LogIn();
                    break;
                case "2":
                    SignUp();
                    break;
                default:
                    Menu();
                    break;
            }
        }

        public static void LogIn()
        {
            Console.Clear();
            Console.Write("Username: ");
            string username = Console.ReadLine();///////////////////////////////
            Console.Write("Password: ");
            string password = Console.ReadLine();

            for (int i = 0; i < userList.Count; i++)
            {
                if(username == userList[i].Username)
                {
                    if (userList[i].VerifyPassword(password))
                    {
                        LoggedIn(i);
                    }

                    Console.WriteLine("Incorrect password was entered.\nPlease try again\n\nPress any key to continiue...");
                    Console.ReadKey();
                    LogIn();
                }
            }


            Console.WriteLine("Username was not found in our system.\nWould you like to regiter?\n[1] Yes\n[2] No");
            int choice= Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    SignUp();
                    break;
                case 2:
                    LogIn();
                    break;
                default:
                    Menu();
                    break;
            }
            Menu();
        }
            
        public static  void SignUp()
        {
            Console.Clear();
            Console.Write("Choose your username: ");
            string username = Console.ReadLine();
            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i].Username == username)
                {
                    Console.WriteLine("Username is already taken. Please choose another\n\nPress any key");
                    Console.ReadKey();
                    SignUp();

                }
            }

            Console.Write("Choose your password: ");
            string password = Console.ReadLine();
            if (password.Length < 3)
            {
                Console.WriteLine("Your password needs to have at least 3 characters\n\nPress any key");
                Console.ReadKey();
                SignUp();
            }

            Console.Write("Confirm password: ");
            string passwordConfirm= Console.ReadLine();

            
            if (passwordConfirm == password)
            {
                Member account = new Member(username, password, MemberLevel.Iron);
                userList.Add(account);

                Console.WriteLine("What membership level would you like?\n" +
                "[0] Iron | No discounts\n" +
                "[1] Bronze | 5% discount on purcheses\n" +
                "[2] Silver | 10% discount on purcheses\n" +
                "[3] Gold | 15% discount on purcheses");
                int level = Convert.ToInt32(Console.ReadLine());

                switch (level)
                {
                    case 0:
                        break;
                    case 1:
                        account.Level=MemberLevel.Bronze;
                        break;
                    case 2:
                        account.Level = MemberLevel.Silver;
                        break;
                    case 3:
                        account.Level = MemberLevel.Gold;
                        break;
                    default:
                        Console.WriteLine("Incorrect information was inserted, starting over");
                        SignUp();
                        break;
                }


                Console.WriteLine($"\nWelcome {userList[userAmount].Username} \nYour account was sucessfully registered!\nYour rank is {account.Level}\n\nPress any key...");
                userAmount++;
                Console.ReadKey();
                Menu();
            }
            else
            {
                Console.WriteLine("Your passwords didnt match. Try again\n\nPress any key to continue");
                Console.ReadKey();
                SignUp();
            }
        }   //Make requirements for character length and other stuff //

        public static void LoggedIn(int ID)
        {
            Console.Clear();
            Console.WriteLine($"Welcome {userList[ID].Username}\nChoose what you would like to do next\n" +
                $"[0] Log out\n[1] Go shoping\n[2] See shoping cart\n[3] Go to checkout");

            string choice= (Console.ReadLine());

            switch (choice)
            {
                case "0":
                    Console.WriteLine("\n\nAre you sure you want to log out?\n[1] Yes\n[2] No");
                    string choice1=(Console.ReadLine());

                    switch (choice1)
                    {
                        case "1":
                            Menu();
                            break;
                        case "2":
                            LoggedIn(ID);
                            break;
                    }
                    break;
                case "1":
                    Console.Clear();
                    userList[ID].GoShopping(itemList, ID);    //Spammar man en tom readline i metoden loggas den ut OCH den tar bot varor från litan!!!!
                    break;
                case "2":
                    userList[ID].ShopingCart(ID);
                    Console.WriteLine("\nPress any key to go back...");
                    Console.ReadKey();
                    LoggedIn(ID);
                    break;
                case "3":
                    userList[ID].CheckOut(ID); //***************************
                    break;
                default:
                    LoggedIn(ID);
                    break;
            }
        }




        
        public void LoadItems()
        {
            Item apple = new Item("Apple", 7.59);
            Item sandwitch = new Item("Sandwitch", 27.99);
            Item soda = new Item("Soda", 5.99);

            itemList.Add(apple);
            itemList.Add(sandwitch);
            itemList.Add(soda);

            LoadUsers();
        }
        public void LoadUsers()
        {
            Member kund1 = new Member("Knatte", "123", MemberLevel.Bronze);
            Member kund2 = new Member("Fnatte", "321", MemberLevel.Bronze);
            Member kund3 = new Member("Tjatte", "213", MemberLevel.Bronze);

            userList.Add(kund1);
            userList.Add(kund2);
            userList.Add(kund3);

            Menu();
        }
        public static void Main(string[] args)
        {
            var minButik = new Program();
            minButik.LoadItems();
        }
    }
}