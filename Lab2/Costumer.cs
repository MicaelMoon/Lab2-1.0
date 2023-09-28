using System.IO;

namespace Lab2
{
    internal abstract class Costumer
    {
        private string username;
        private string password;
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
            for (int i = 0; i < Program.itemList.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {Program.itemList[i].name} = {Math.Round(Program.itemList[i].price, 2)}");
            }
            Console.WriteLine();
            for (int i = 0; i < userCart.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {userCart[i].name}");
            }

            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice != 0)
                {
                    userCart.Add(Program.itemList[choice - 1]);
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
                userCart[i].price = userCart[i].price * userCart[i].stack;
            }
        }       

        public double ShopingCart(int ID) //Broken********************
        {
            double totalCost = 0;
            Console.WriteLine("\nYour shoping cart contains\n"); //////////////////////////////////////////////////////////////////
            for (int i = 0; i < userCart.Count; i++)
            {
                Console.WriteLine($"{userCart[i].stack} {userCart[i].name} " +
                    $"| {Math.Round(userCart[i].priceOriginal, 2)} a piece |" +
                    $" Stack price = {Math.Round(userCart[i].price, 2)}\n");
            }

            foreach (Item i in userCart)
            {
                totalCost += i.price;
            }
            Console.WriteLine($"Total = {Math.Round(totalCost, 2)}"); // onödig kod?
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
