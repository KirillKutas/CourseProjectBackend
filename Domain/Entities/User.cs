using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
        public Role Role { get; set; }
        public int Invoice { get; set; }
        public string Image { get; set; }

        public List<Game> Games { get; set; } = new List<Game>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
