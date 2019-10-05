using System;

namespace Blazedo.Models
{
    public class Todo
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}
