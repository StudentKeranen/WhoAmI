using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitylayer
{
    /// <summary>
    /// Following usertypes is avaliable 1=Admin 2=User
    /// </summary>
    public class User
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        private int _userTypeValue;
        public int Usertype 
        {
            get{return _userTypeValue;}
            
            set
            {
                if (value < 1 || value > 2)
                {
                    throw new ArgumentOutOfRangeException((nameof(Usertype)));
                }

                _userTypeValue = value;
            }
        }
        private static int _userCount;

        public User()
        {
        }
        public User(string password, int usertype)
        {
            _userCount++;
            UserId = "U" + _userCount.ToString("000000");
            Password = password;
            Usertype = usertype;
        }
    }
}
