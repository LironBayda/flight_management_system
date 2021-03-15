using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
    public class Administrator : IPoco, IUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Level { get; set; }
        public int UserId { get; set; }

        public User UserObj { get; set; }


        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public Administrator()
        {

        }

        public Administrator(int id, string firstName, string lastName, int level, int userId, User userObj)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Level = level;
            UserId = userId;
            UserObj = userObj;
        }

        public static bool operator ==(Administrator administrator1, Administrator administrator2)
        {


            if (object.ReferenceEquals(administrator1, null) && object.ReferenceEquals(administrator2, null))
                return true;
            if (object.ReferenceEquals(administrator1, null) || object.ReferenceEquals(administrator2, null))
                return false;

            return administrator1.Id == administrator2.Id;
        }

        public static bool operator !=(Administrator administrator1, Administrator administrator2)
        {
            return !(administrator1 == administrator2);
        }

        public override bool Equals(object obj)
        {
            Administrator administrator = obj as Administrator;
            if (administrator != null)
                return this.Id == administrator.Id;

            return false;
        }

        public override int GetHashCode()
        {
            return this.Id ;
        }

    }
}
