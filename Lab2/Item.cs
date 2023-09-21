using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Item
    {
        public string name;
        public double price;
        public double priceOriginal;
        public int stack = 1;

        public Item(string name, double price)
        {
            this.name = name;
            this.price = price;
            this.priceOriginal = price;
        }

        public override string ToString()
        {
            return string.Format($"{name} = {priceOriginal}kr");
        }
    }
}
