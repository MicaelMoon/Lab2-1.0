namespace Lab2
{
    class Account
    {
        //Tydligen behövs forfatande något här?
        private string username;
        private string password;
        public List<Item> userCart = new List<Item>();


        public Account(string username, string password)
        {
            this.username = username;
            this.password = password;
        }


        public string Username
        {    //Accesers = Special method
            get { return username; }
        }
        public string Password //?????
        {
            get { return this.password; }
            set { password = value; } 
        }
        public bool VerifyPassword(string password)
        {
            if(Password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void GoShopping(List<Item> itemList, Account ID)
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


            int choice = Convert.ToInt32(Console.ReadLine());

            try
            {
                if (choice != 0)
                {
                    userCart.Add(itemList[choice - 1]);
                    GoShopping(itemList, ID);
                }
                else if (choice == 0)
                {
                    ID.CompressStacks();
                    Program.LoggedIn(ID);
                }
                else
                {
                    ID.GoShopping(itemList, ID);
                }
            }
            catch
            {
                ID.GoShopping(itemList, ID);
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
        public void ShopingCart(Account ID)
        {
            double totalCost = 0;
            Console.WriteLine("\nYour shoping cart contains\n");
            for (int i = 0; i < userCart.Count; i++)
            {
                Console.WriteLine($"{userCart[i].name} | Quantity price = {userCart[i].priceOriginal}\n" +
                    $"Quantity: {userCart[i].stack} | Combined price = {userCart[i].price}\n");
            }

            foreach (Item i in userCart)
            {
                totalCost += i.price;
            }

            Console.WriteLine($"Total = {Math.Round(totalCost, 2)}");
        }
        public double CheckMemberShip(MemberBronze ID)
        {

        }
        public void CheckOut(Account ID)
        {
            ShopingCart(ID);
            
            ID.Discount
        }
    }
}
