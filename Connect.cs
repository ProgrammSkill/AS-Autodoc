using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS_Autodoc
{
    public class Connect
    {
        public static string getConnect()
        {
            string con = @"Data Source=.\SQLEXPRESS;Initial Catalog=Auto_parts_shop;Integrated Security=True";
            return con;
        }
    }
}
