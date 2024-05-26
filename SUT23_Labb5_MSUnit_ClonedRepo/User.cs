using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Loop_Legends
{
    public class User
    {
        public string _name { get; set; }
        public string _password { get; set; }

        public User(string name, string password)
        {
            this._name = name;
            this._password = password;
        }
    }
}
