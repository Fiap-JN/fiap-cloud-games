using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.Responses
{
    public class CreateGameResponses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
