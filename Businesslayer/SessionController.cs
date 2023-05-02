using System;
using System.Net;
using Entitylayer;

namespace Businesslayer
{
    public sealed class SessionController
    {
        private static SessionController? _instance;

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
        public UnitOfWork unitOfWork = new UnitOfWork(); //Logiken/kontext lägger vi i UoW-klassen
        private User? LoggedIn { get; set; }

        public void LogIn(string firstname, string password)
        {
            User credentials = unitOfWork.UserRepository.FirstOrDefault(u => u.UserId == Firstname);
            if (credentials != null && credentials.Password.Equals(password)) // Kollar att expedit e hittades och att lösenord stämmer överens
            {
                LoggedIn = credentials;
            }
            else
            {
                LoggedIn = null;
            }
        }

    }
}
