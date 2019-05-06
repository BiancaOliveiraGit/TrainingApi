using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingApi.Data
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HomeAddress { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public int Mobile { get; set; }

        [Required]
        public string ObjectIdentifier { get; set; }
    }
}
