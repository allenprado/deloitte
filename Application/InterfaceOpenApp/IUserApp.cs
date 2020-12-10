using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.InterfaceOpenApp
{
    public interface IUserApp : IGenericApp<User>
    {
        public User GetByUserName(string UserName);
    }
}
