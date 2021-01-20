using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
    public class User_Role:IPoco
    {
        public int Id { get; set; }
        public string Role_Name { get; set; }


        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public User_Role()
        {

        }

        public User_Role(int id, string role_Name)
        {
            Id = id;
            Role_Name = role_Name;
        }

        public static bool operator ==(User_Role user_roles1, User_Role user_roles2)
        {


            if (object.ReferenceEquals(user_roles1, null) && object.ReferenceEquals(user_roles2, null))
                return true;
            if (object.ReferenceEquals(user_roles1, null) || object.ReferenceEquals(user_roles2, null))
                return false;

            return user_roles1.Id == user_roles2.Id;
        }

        public static bool operator !=(User_Role user_roles1, User_Role user_roles2)
        {
            return !(user_roles1 == user_roles2);
        }

        public override bool Equals(object obj)
        {
            User_Role user_roles = obj as User_Role;
            if (user_roles != null)
                return this.Id == user_roles.Id;

            return false;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }

    }
}
