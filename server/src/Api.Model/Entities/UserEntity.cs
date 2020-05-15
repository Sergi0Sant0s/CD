using System;

namespace Api.Model.Entities
{
    public class UserEntity : BaseEntity
    {
        public UserEntity()
        {
            base.CreateAt = DateTime.Now;
        }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string FolderPath { get; set; }

        public string ImagePath { get; set; }
    }
}
