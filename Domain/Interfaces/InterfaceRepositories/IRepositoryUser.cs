using Domain.Interfaces.Generics;
using Entities.Entities;

namespace Domain.Interfaces.InterfaceRepositories
{
    public interface IRepositoryUser : IGeneric<User>
    {
        public User GetByUserName(string UserName);
    }
}
