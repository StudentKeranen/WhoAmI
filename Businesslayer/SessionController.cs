using System;
using System.Net;
using Entitylayer;
using Datalayer;

namespace Businesslayer
{
    public sealed class SessionController
    {
        public UnitOfWork unitOfWork = new UnitOfWork();

        private static SessionController? _instance;
        public static User? LoggedIn { get; private set; }
        private SessionController(User user) 
        {
            LoggedIn = user;
        }

        public static SessionController Instance(User user)
        {
            if (_instance == null)
            {
                _instance = new SessionController(user);
            }
            return _instance;
        }
        public static SessionController Instance()
        {
            if (null == _instance)
            {
                throw new ApplicationException("No user found!");
            }
            return _instance;
        }
        
        public void LogIn(string firstname, string password)
        {
            User credentials = unitOfWork.UserRepository.FirstOrDefault(u => u.UserId == Firstname);
            if (credentials != null && credentials.Password.Equals(password))
            {
                LoggedIn = credentials;
            }
            else
            {
                LoggedIn = null;
            }
        }

        public static void Terminate()
        {
            _instance = null;
            LoggedIn = null;
        }
    }
}
