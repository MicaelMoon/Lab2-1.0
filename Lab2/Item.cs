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
        public double priceOriginal;
        public double price;
        public int stack = 1;


        public Item(string name, double priceOriginal)
        {
            this.name = name;
            this.priceOriginal = priceOriginal;
            this.price = priceOriginal;
            stack = 1;
        }

        public override string ToString()
        {
            return string.Format($"{name} = {priceOriginal}");
        }

        //Default currency = Euro

        public double ConvertOriginalPriceToSEK()
        {
            return Math.Round(priceOriginal * 11.66, 2);
        }

        public double ConvertOriginalPriceToUSD()
        {
            return Math.Round(priceOriginal * 1.06, 2);
        }
        public double ConvertToSEK()
        {
            return Math.Round(price * 11.66, 2);
        }

        public double ConvertToUSD()
        {
            return Math.Round(price * 1.06, 2);
        }


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
