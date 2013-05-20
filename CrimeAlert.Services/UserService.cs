using System;
using CrimeAlert.DataContracts;
using CrimeAlert.DataEntities.Entities;
using CrimeAlert.ServiceContracts;
using CrimeAlert.Services.Exceptions;

namespace CrimeAlert.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User GetUser(string authToken)
        {
            try
            {
                return userRepository.GetUserByToken(authToken);
            }
            catch (Exception exception)
            {
                throw new UserServiceException("Failed to get user", exception); // TODO fix
            }
        }

        public User GetUser(int userId)
        {
            try
            {
                return userRepository.GetUser(userId);
            }
            catch (Exception exception)
            {
                throw new UserServiceException("failed to ret", exception);
            }
        }

        public User AddUser(string authToken, string firstName, string lastName, string email)
        {
            try
            {
                var user = new User
                    {
                        AuthToken = authToken,
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email
                    };
                userRepository.Save(user);

                return user;
            }
            catch (Exception exception)
            {
                throw new UserServiceException("Failed to create.", exception);
            }
        }

        public void Test()
        {

            var newUser = new User
                {
                    FirstName = "test1",
                    LastName = "test2",
                    Email = "aaa@aa.lt",
                    AuthToken = "test"
                };
            userRepository.Save(newUser);

            var user = userRepository.GetUser(5);
            user.Email = DateTime.Now.ToShortDateString();
            userRepository.Save(user);
        }
    }
}
