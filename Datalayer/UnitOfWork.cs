using Entitylayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalayer
{
    public class UnitOfWork
    {
        public Repository<User> UserRepository { get; private set; }
        public Repository<Company> CompanyRepository { get; private set; }
    }
}
