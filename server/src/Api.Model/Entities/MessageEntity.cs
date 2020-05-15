using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Model.Entities
{
    public class MessageEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("ChatEntity")]
        public int IdChat { get; set; }

        [ForeignKey("UserEntity")]
        public int IdUser { get; set; }

        public string Text { get; set; }

        public DateTime? Time { get; set; }
    }
}
