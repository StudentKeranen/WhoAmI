using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitylayer
{
    public class PersonalData
    {
        [Key]
        public int PersonalDataId { get; set; }
        public string DataValue { get; set; }
        public string DataName { get; set; }
        public DataCategory DataCategories { get; set; }
        public User User { get; set; }
    }
}
