using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Loop_Legends
{
    internal class Initializer
    {
        public static void InitializerMethod()
        {
            UserManager c1 = new UserManager();
            c1.CreateInitialUsers();
        }
    }
}
