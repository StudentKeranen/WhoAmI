using System;
using Entitylayer;

namespace Businesslayer
{
    public class SessionController
    {
        public UnitOfWork unitOfWork = new UnitOfWork(); //Logiken/kontext l�gger vi i UoW-klassen
        public User loggedIn { get; set; }

        public User LogIn(string firstname, string password)
        {
            User credentials = unitOfWork.UserRepository.FirstOrDefault(u => u.UserId == Firstname);
            if (credentials != null && credentials.Password.Equals(password)) // Kollar att expedit e hittades och att l�senord st�mmer �verens
            {
                return credentials;
            }
            else
            {
                return null;
            }
        }

    }
}
