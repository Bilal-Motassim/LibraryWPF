using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWPF
{
    internal class User
    {
        public int id { get; set; }
        public string nom { get; set; }

        public User(int a, string b)
        {
            id = a; nom = b;
        }
    }
}
