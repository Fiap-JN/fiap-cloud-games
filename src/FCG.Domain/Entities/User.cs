using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Domain.Entities
{
    public class User
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; }
        public DateTime DeletedDate { get; set; }


        public static User Create(string name, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome é obrigatório");

            if (!IsValidEmail(email))
                throw new ArgumentException("E-mail inválido");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Senha é obrigatória");

            return new User
            {
                Name = name,
                Email = email,
                Password = password
            };
        }

        private static bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        //private static bool isValidPassword(string password)
        //{
        //    return new p().IsValid(password);
        //}
    }
}
