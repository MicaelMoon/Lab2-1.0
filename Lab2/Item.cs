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
            return string.Format($"{name} = {priceOriginal}");
        }

        //Kanske kan flytta dessa metoder
        //Default currency = Euro
        public static double ConvertToSEK(double price)
        {
            return Math.Round((price * 11.66), 2);
        }

        public static double ConvertToUSD(double price)
        {
            return Math.Round((price * 1.06), 2);
        }
    }
}
