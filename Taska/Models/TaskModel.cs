using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taska.Models
{
    public class TaskModel
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
