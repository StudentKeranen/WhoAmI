using System;
using System.Net;
using Entitylayer;

namespace Businesslayer
{
    public class SessionController
    {
        public UnitOfWork unitOfWork = new UnitOfWork(); //Logiken/kontext lägger vi i UoW-klassen
        private User? loggedIn { get; set; }

        public void LogIn(string firstname, string password)
        {
            User credentials = unitOfWork.UserRepository.FirstOrDefault(u => u.UserId == Firstname);
            if (credentials != null && credentials.Password.Equals(password)) // Kollar att expedit e hittades och att lösenord stämmer överens
            {
                loggedIn = credentials;
            }
            else
            {
                loggedIn = null;
            }
        }

    }
}
