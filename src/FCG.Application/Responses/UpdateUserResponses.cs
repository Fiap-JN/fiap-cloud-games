using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.Responses
{
    public class UpdateUserResponses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Exists { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
