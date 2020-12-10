using Domain.Interfaces.InterfaceRepositories;
using Entities.Entities;
using Infra.Configuration;
using Infra.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository.Repositories
{
    public class RepositoryUser : RepositoryGenerics<User>, IRepositoryUser
    {

        private readonly DbContextOptions<ContextBase> _optionBuilder;
        public RepositoryUser()
        {
            _optionBuilder = new DbContextOptions<ContextBase>();
        }
        public User GetByUserName(string UserName)
        {
            using (var db = new ContextBase(_optionBuilder))
            {
               return db.Users.Where(x => x.UserName == UserName).FirstOrDefault();
            }
        }
    }
}
