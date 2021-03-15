using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
    public class UserRole:IPoco
    {
        public int Id { get; set; }
        public string RoleName { get; set; }


        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public UserRole()
        {

        }

        public UserRole(int id)
        {
            Id = id;

            switch (id) {
                case 1:
                    RoleName = "Administrator";
                    break;
                case 2:
                    RoleName = "AirLineCompany";
                    break;
                case 3:
                    RoleName ="Customer";
                    break;
            }
        }

        public static bool operator ==(UserRole user_roles1, UserRole user_roles2)
        {


            if (object.ReferenceEquals(user_roles1, null) && object.ReferenceEquals(user_roles2, null))
                return true;
            if (object.ReferenceEquals(user_roles1, null) || object.ReferenceEquals(user_roles2, null))
                return false;

            return user_roles1.Id == user_roles2.Id;
        }

        public static bool operator !=(UserRole user_roles1, UserRole user_roles2)
        {
            return !(user_roles1 == user_roles2);
        }

        public override bool Equals(object obj)
        {
            UserRole user_roles = obj as UserRole;
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
