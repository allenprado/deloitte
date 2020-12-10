using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool Done { get; set; }

    }
}
