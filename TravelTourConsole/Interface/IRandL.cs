using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelTourConsole.Interface
{
    public interface IRandL
    {
        public bool login(string UserName, string password);
        public void Registration(string UserName, string Password);


    } 
}
