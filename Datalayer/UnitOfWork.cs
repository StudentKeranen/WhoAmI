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
        protected AppDbContext appDbContext { get; }
        public Repository<User> UserRepository { get; private set; } = null!;
        public Repository<Company> CompanyRepository { get; private set; } = null!;
        public UnitOfWork()
        {
            appDbContext = new AppDbContext();

            UserRepository = new Repository<User>(appDbContext);
            CompanyRepository = new Repository<Company>(appDbContext);
        }

        public void Save()
        {
            appDbContext.SaveChanges();
        }
    }
}
