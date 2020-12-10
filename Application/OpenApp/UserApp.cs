
using Application.InterfaceOpenApp;
using Domain.Interfaces.InterfaceRepositories;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.OpenApp
{
    public class UserApp : IUserApp
    {
        IRepositoryUser _users;
        IServiceUser _serviceUser;
        public UserApp(IRepositoryUser users, IServiceUser serviceUser)
        {
            _users = users;
            _serviceUser = serviceUser;
        }
        
        public async Task Add(User Object)
        {
            await _users.Add(Object);
        }

        public async Task Delete(User Object)
        {
            await _users.Delete(Object);
        }

        public User GetByUserName(string UserName)
        {
            return _serviceUser.GetByUserName(UserName);
        }

        public async Task<User> GetEntityById(int Id)
        {
            return await _users.GetEntityById(Id);
        }

        public async Task<List<User>> List()
        {
            return await _users.List();
        }

        public async Task Update(User Object)
        {
            await _users.Update(Object);
        }
    }
}
