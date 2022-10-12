using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Models
{
    internal class Contact // "mall" för hur vår kontakt ska se ut 
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // ger oss ett nytt unikt id direkt när en kontakt skapas
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
        public int PhoneNumber { get; set; }

    }
}
