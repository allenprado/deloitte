using Application.InterfaceOpenApp;
using Domain.Interfaces.InterfaceRepositories;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.OpenApp
{
    public class ToDoListApp : IToDoListApp
    {
        IRepositoryToDoList _toDo;
        public ToDoListApp(IRepositoryToDoList toDo)
        {
            _toDo = toDo;
        }

        public async Task Add(ToDoList Object)
        {
            await _toDo.Add(Object);
        }

        public async Task Delete(ToDoList Object)
        {
            await _toDo.Delete(Object);
        }

        public async Task<ToDoList> GetEntityById(int Id)
        {
            return await _toDo.GetEntityById(Id);
        }

        public async Task<List<ToDoList>> List()
        {
            return await _toDo.List();
        }

        public async Task Update(ToDoList Object)
        {
            await _toDo.Update(Object);
        }
    }
}
