using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Lab2  //For next time: plays appropriet methods inside appropriate class as class method
{               //Future potention problem | When sutomer is done shoping the price after increasing stack might need to be reset
    internal class Program
    {
        public static List<Item> itemList = new List<Item>();
        public static List<Costumer> userList = new List<Costumer>();
        public static int userAmount = 0;


        

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
                    if (password == userList[i].Password)
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
                //Sets fileName to a specifik file on harddrive
                //FIle.AppendAlltext = a class method puts in your chosen text into text at file location
                string fileName = "C:\\Users\\frans\\OneDrive\\Skrivbord\\Lab2-f4f724fafc093e198567414b4e804e6c66635dac\\Lab2\\TextFile1.txt";
                File.AppendAllText(fileName, $"{username},{password},{account.Level}\n");


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
                    userList[ID].GoShopping(ID);    //Spammar man en tom readline i metoden loggas den ut OCH den tar bot varor från litan!!!!
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
            string level = Convert.ToString(MemberLevel.Bronze);
        }
        
        public void LoadItems()
        {
            Item apple = new Item("Apple", 0.59);
            Item sandwitch = new Item("Sandwitch", 1.69);
            Item soda = new Item("Soda", 2);

            itemList.Add(apple);
            itemList.Add(sandwitch);
            itemList.Add(soda);

            LoadUsers();
        }
        public void LoadUsers()
        {
            string userFile = "C:\\Users\\frans\\OneDrive\\Skrivbord\\Lab2-f4f724fafc093e198567414b4e804e6c66635dac\\Lab2\\TextFile1.txt";

            userList = UpploadCostumerFromTextFile(userFile);

            userAmount = userList.Count();
            Menu();
        }

        static List<Costumer> UpploadCostumerFromTextFile(string fileName)
        {
            List<Costumer> costumerInFile = new List<Costumer>();

            string[] lines = File.ReadAllLines(fileName);

            foreach(string line in lines)
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    string[] userData = line.Split(",");

                    if (userData.Length == 3)
                    {
                        string username = userData[0];
                        string password = userData[1];
                        Member member = new Member(username, password, MemberLevel.Iron);

                        MemberLevel level = MemberLevel.Iron;
                        switch (userData[2])
                        {
                            case "Iron":
                                member.Level = MemberLevel.Iron;
                                break;
                            case "Bronze":
                                member.Level = MemberLevel.Bronze;
                                break;
                            case "Silver":
                                member.Level = MemberLevel.Silver;
                                break;
                            case "Gold":
                                member.Level = MemberLevel.Gold;
                                break;
                        }  
                        costumerInFile.Add(member);

                    }
                }
            }
            return costumerInFile;
        }

        public static void Main(string[] args)
        {
            var minButik = new Program();
            minButik.LoadItems();
        }
    }
}