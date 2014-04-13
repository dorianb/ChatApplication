using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib
{
    public class User
    {
        public String name;
        public String password;

        public User()
        {

        }

        public User(String name, String password)
        {
            this.name = name;
            this.password = password;
        }
    }
}
