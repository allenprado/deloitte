using Domain.Interfaces.InterfaceRepositories;
using Entities.Entities;
using Infra.Repository.Generics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.Repositories
{
    public class RepositoryToDoList : RepositoryGenerics<ToDoList>, IRepositoryToDoList
    {

    }
}
