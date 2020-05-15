using System;

namespace Api.Model.Entities
{
    public class ChatEntity : BaseEntity
    {
        public ChatEntity()
        {
            base.CreateAt = DateTime.Now;
        }
        public string Name { get; set; }
    }
}
