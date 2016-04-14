using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Services
{
    public interface IGreeter
    {
        String getGreeting();

    }



    public class Greeter : IGreeter
    {
        private string _greeting;

        public Greeter(IConfiguration configuration)
       {
            _greeting = configuration["greeting"];

       }
        public string getGreeting()
        {
            return _greeting;
        }


       
    }

}
