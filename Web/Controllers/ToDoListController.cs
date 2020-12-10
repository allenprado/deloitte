using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.InterfaceOpenApp;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IToDoListApp _toDoList;
        
        public ToDoListController(IHttpContextAccessor accessor, IToDoListApp toDoList)
        {
            _accessor = accessor;
            _toDoList = toDoList;
        }

        public async Task<IActionResult> Index()
        {
            this.IsLogged();
            List<ToDoListViewModel> listToDoVM = new List<ToDoListViewModel>();

            foreach (var item in await _toDoList.List())
            {
                listToDoVM.Add(new ToDoListViewModel
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    Title = item.Title,
                    Description = item.Description,
                    Date = item.Date,
                    Done = item.Done
                });
            }

            return View(listToDoVM);
        }

        public IActionResult Create()
        {
            this.IsLogged();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoListViewModel todolist)
        {
            ToDoList todoListDB = new ToDoList();
            if (ModelState.IsValid)
            {
                todoListDB.Title = todolist.Title;
                todoListDB.UserName = _accessor.HttpContext.Session.GetString("UserNameSession");
                todoListDB.Description = todolist.Description;
                todoListDB.Date = DateTime.Now;
                todoListDB.Done = false;

                TempData["success"] = "Task has been created!";
                await _toDoList.Add(todoListDB);
                return RedirectToAction("Index");
            }
            TempData["error"] = "Something went wrong!";
            return View(todolist);
        }

        public async Task<IActionResult> Edit(int id)
        {
            this.IsLogged();
            var todoList = await _toDoList.GetEntityById(id);
            ToDoListViewModel todoListVM = new ToDoListViewModel();

            todoListVM.Id = todoList.Id;
            todoListVM.Title = todoList.Title;
            todoListVM.Description = todoList.Description;
            todoListVM.UserName = todoList.UserName;
            todoListVM.Date = todoList.Date;
            todoListVM.Done = todoList.Done;

            return View(todoListVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ToDoListViewModel todolist)
        {
            var todoListDB = await _toDoList.GetEntityById(todolist.Id);

            if (ModelState.IsValid)
            {
                todoListDB.Title = todolist.Title;
                todoListDB.Done = todolist.Done;
                todoListDB.Date = DateTime.Now;
                todoListDB.UserName = _accessor.HttpContext.Session.GetString("UserNameSession");

                TempData["success"] = "Task has been Edited!";
                await _toDoList.Update(todoListDB);
                return RedirectToAction("Index");

            }
            TempData["error"] = "Something went wrong!";
            return View(todolist);
        }

        public async Task<IActionResult> Details(int id)
        {
            var todoList = await _toDoList.GetEntityById(id);
            ToDoListViewModel todoListVM = new ToDoListViewModel();

            todoListVM.Id = todoList.Id;
            todoListVM.Title = todoList.Title;
            todoListVM.Description = todoList.Description;
            todoListVM.UserName = todoList.UserName;
            todoListVM.Date = todoList.Date;
            todoListVM.Done = todoList.Done;

            return View(todoListVM);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var todoList = await _toDoList.GetEntityById(Id);
            ToDoListViewModel todoListVM = new ToDoListViewModel();

            todoListVM.Id = todoList.Id;
            todoListVM.Title = todoList.Title;
            todoListVM.Description = todoList.Description;
            todoListVM.UserName = todoList.UserName;
            todoListVM.Date = todoList.Date;
            todoListVM.Done = todoList.Done;

            return View(todoListVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var todoList = await _toDoList.GetEntityById(Id);

            await _toDoList.Delete(todoList);

            TempData["success"] = "Task has been deleted";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Done(int id)
        {

            var todoList = await _toDoList.GetEntityById(id);

            if (ModelState.IsValid)
            {
                todoList.Date = DateTime.Now;
                todoList.Done = true;

                await _toDoList.Update(todoList);
                TempData["success"] = "This task is done!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "something went wrong";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Undone(int id)
        {

            var todoList = await _toDoList.GetEntityById(id);

            if (ModelState.IsValid)
            {
                todoList.Date = DateTime.Now;
                todoList.Done = false;

                await _toDoList.Update(todoList);
                TempData["success"] = "This task is done!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "something went wrong";
            return RedirectToAction("Index");
        }

        public void IsLogged()
        {
            var isLogged = _accessor.HttpContext.Session.GetInt32("IsLoggedSession");
            if (isLogged != 1)
            {
                RedirectToAction("Login", "Account");
            }
            
        }
    }
}
