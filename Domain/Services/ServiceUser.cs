using Domain.Interfaces.InterfaceRepositories;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class ServiceUser : IServiceUser
    {
        private readonly IRepositoryUser _user;
        public ServiceUser(IRepositoryUser user)
        {
            _user = user;
        }
        public User GetByUserName(string UserName)
        {
           return _user.GetByUserName(UserName);
        }
    }
}
