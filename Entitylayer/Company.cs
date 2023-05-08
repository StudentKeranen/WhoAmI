using System;
using System.ComponentModel.DataAnnotations;

namespace Entitylayer
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int CompanyNumber { get; set; }
        public string? CompanyDescription { get; set;}

        public Company()
        {

        }
    }
}
