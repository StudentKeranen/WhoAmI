using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitylayer
{
    public class PersonalData
    {
        public int PersonalDataId { get; set; }
        public bool FoundData { get; set; }
        public string DataName { get; set; }
        public DataCategory DataCategories { get; set; }
    }
}
