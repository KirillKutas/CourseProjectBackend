using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Games.Commands.CRUDGames
{
    public class CrudGameModel
    {
        public string Id { get; set; }
        public string GameName { get; set; }
        public int Price { get; set; }
        public string StringReleaseDate { get; set; }
    
        public DateTime ReleaseDate { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public int CountOfBuy { get; set; }
        public string MinSysReqOc { get; set; }
        public string MinSysReqProcessor { get; set; }
        public string MinSysReqRAM { get; set; }
        public string MinSysReqVideoCard { get; set; }

        public string RecSysReqOc { get; set; }
        public string RecSysReqProcessor { get; set; }
        public string RecSysReqRAM { get; set; }
        public string RecSysReqVideoCard { get; set; }

        public string DiskSpace { get; set; }

        public List<User> Users { get; set; } = new List<User>();
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Domain.Entities.Comment> Comments { get; set; } = new List<Domain.Entities.Comment>();
    }
}
