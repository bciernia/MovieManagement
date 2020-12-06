using MovieManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain.Entity
{
    public class MenuAction : BaseEntity
    {
        public MenuAction(int id, string name, string menuName)
        {
            Id = id;
            Name = name;
            MenuName = menuName;
        }
        public string Name { get; set; }
        public string MenuName { get; set; }
    }
}
