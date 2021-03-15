using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
    public class User : IPoco
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
        public int UserRoleId { get; set; }

        public UserRole UserRoleObj { get; set; }

        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public User()
        {

        }

        public User(int id, string username, string password, string email, int userRoleId, UserRole userRoleObj)
        {
            Id = id;
            Username = username;
            Password = password;
            Email = email;
            UserRoleId = userRoleId;
            UserRoleObj = userRoleObj;
        }

        public static bool operator ==(User user1, User user2)
        {


            if (object.ReferenceEquals(user1, null) && object.ReferenceEquals(user2, null))
                return true;
            if (object.ReferenceEquals(user1, null) || object.ReferenceEquals(user2, null))
                return false;

            return user1.Id == user2.Id;
        }

        public static bool operator !=(User user1, User user2)
        {
            return !(user1 == user2);
        }

        public override bool Equals(object obj)
        {
            User user = obj as User;
            if (user != null)
                return this.Id == user.Id;

            return false;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }

    }
}
