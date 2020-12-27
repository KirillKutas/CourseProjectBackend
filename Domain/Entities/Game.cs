
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Game
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string GameName { get; set; }
        [Required]
        public int Price { get; set; }
        [NotMapped]
        public string StringReleaseDate => ReleaseDate.ToShortDateString();
        public DateTime ReleaseDate { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public int CountOfBuy => Users == null ? -1 : Users.Count;
        

        [Required]
        public string MinSysReqOc { get; set; }
        [Required]
        public string MinSysReqProcessor { get; set; }
        [Required]
        public string MinSysReqRAM { get; set; }
        [Required]
        public string MinSysReqVideoCard { get; set; }

        [Required]
        public string RecSysReqOc { get; set; }
        [Required]
        public string RecSysReqProcessor { get; set; }
        [Required]
        public string RecSysReqRAM { get; set; }
        [Required]
        public string RecSysReqVideoCard { get; set; }

        [Required]
        public string DiskSpace { get; set; }

        public List<User> Users { get; set; } = new List<User>();
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
