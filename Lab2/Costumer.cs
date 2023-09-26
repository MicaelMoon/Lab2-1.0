namespace Lab2
{
    internal abstract class Costumer
    {
        //Tydligen behövs forfatande något här?
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

        public string Password //?????
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
        public void GoShopping(List<Item> itemList, int ID)
        {
            Console.Clear();
            Console.WriteLine("//Items avalable\\\\");
            Console.WriteLine("[0] Press to go back\n");
            for (int i = 0; i < itemList.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {itemList[i].name} = {itemList[i].price}");
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
                    userCart.Add(itemList[choice - 1]);
                    GoShopping(itemList, ID);
                }
                else if (choice == 0)
                {
                    CompressStacks();
                    Program.LoggedIn(ID);
                }
                else
                {
                    GoShopping(itemList, ID);
                }
            }
            catch
            {
                GoShopping(itemList, ID);
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
        public virtual double ShopingCart(int ID) //Broken********************
        {
            double totalCost = 0;
            Console.WriteLine("\nYour shoping cart contains\n"); //////////////////////////////////////////////////////////////////
            for (int i = 0; i < userCart.Count; i++)
            {
                Console.WriteLine($"{userCart[i].stack} {userCart[i].name} " +
                    $"| {userCart[i].priceOriginal} a piece | Combined price = {userCart[i].price}\n");
            }

            foreach (Item i in userCart)
            {
                totalCost += i.price;
            }
            Console.WriteLine($"Total = {Math.Round(totalCost, 2)}"); // onödig kod?
            return Math.Round(totalCost, 2);
        }

        public virtual void CheckOut(int ID)    //************************* Needed?
        {
            double price = ShopingCart(ID);

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
                int choice = Convert.ToInt32(Console.ReadLine);
                switch (choice)
                {
                    case 1:
                        CheckOut(ID);
                        break;
                    default:
                        Program.LoggedIn(ID);
                        break;
                }
            }
        }
    }
}
