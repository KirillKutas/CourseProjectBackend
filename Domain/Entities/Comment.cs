using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        [NotMapped]
        public string StringPublicationDate => PublicationDate.ToShortDateString();
        public DateTime PublicationDate { get; set; }
        public User User { get; set; }
        public Game Game { get; set; }
    }
}
