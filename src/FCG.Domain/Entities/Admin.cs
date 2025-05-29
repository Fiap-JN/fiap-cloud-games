using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FCG.Domain.Entities
{
    public class Admin
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Gender { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public bool IsAdmin { get; set; } = false;
        public DateTime UpdateDate { get; set; }
        public DateTime DeletedDate { get; set; }

        public static Admin Create(string name, double price, string gender)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome é obrigatório");

            if (double.IsNaN(price))
                throw new ArgumentException("Preço é deve ser um valor válido");

            if (string.IsNullOrWhiteSpace(gender))
                throw new ArgumentException("Genero do jogo é obrigatório");

            return new Admin
            {
                Name = name,
                Price = price,
                Gender = gender
            };
        }

        public static Admin UpdateUser(int id)
        {
            if (int.IsNegative(id))
                throw new ArgumentException("Preço é deve ser um valor válido");

            return new Admin
            {
                Id = id
            };
        }
    }
}
