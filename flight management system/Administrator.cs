using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
    public class Administrator : IPoco, IUser
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Level { get; set; }
        public int User_Id { get; set; }

        public User user { get; set; }


        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public Administrator()
        {

        }

        public Administrator(int id, string first_Name, string last_Name, int level, int user_id, User user)
        {
            Id = id;
            First_Name = first_Name;
            Last_Name = last_Name;
            Level = level;
            User_Id = user_id;
            this.user = user;
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
