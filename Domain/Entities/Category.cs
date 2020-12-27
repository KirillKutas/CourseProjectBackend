﻿
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();
    }
}
