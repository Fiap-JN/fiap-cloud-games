using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.Requests
{
    public class CreateGameRequest
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Gender { get; set; }
    }
}
