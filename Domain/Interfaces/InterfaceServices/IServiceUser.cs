using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceUser
    {
        public User GetByUserName(string UserName);
    }
}
