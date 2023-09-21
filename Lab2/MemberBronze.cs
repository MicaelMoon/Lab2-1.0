using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class MemberBronze : Account
    {
        protected MemberLevel Level; //Might not be needed

        public MemberBronze(string username, string password, MemberLevel level):base(username, password)
        {
            Level = level;
        }



        public double Discount()
        {
            return (int)MemberLevel.Bronze;
        }
    }
}
