using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
    public class AirlineCompany:IPoco,IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int UserId { get; set; }

        public Country CountryObj { get; set; }
        public User UserObj { get; set; }

     


        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public AirlineCompany()
        {

        }

        public AirlineCompany(int id, string name, int countryId, int userId, Country countryObj, User userObj)
        {
            Id = id;
            Name = name;
            CountryId = countryId;
            UserId = userId;
            CountryObj = countryObj;
            UserObj = userObj;
        }

        public static bool operator ==(AirlineCompany airlineCompany1, AirlineCompany airlineCompany2)
        {


            if (object.ReferenceEquals(airlineCompany1, null) && object.ReferenceEquals(airlineCompany2, null))
                return true;
            if (object.ReferenceEquals(airlineCompany1, null) || object.ReferenceEquals(airlineCompany2, null))
                return false;

            return airlineCompany1.Id == airlineCompany2.Id;
        }

        public static bool operator !=(AirlineCompany airlineCompany1, AirlineCompany airlineCompany2)
        {
            return !(airlineCompany1 == airlineCompany2);
        }

        public override bool Equals(object obj)
        {
            AirlineCompany airlineCompany = obj as AirlineCompany;
            if (airlineCompany != null)
                return this.Id == airlineCompany.Id;

            return false;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }

    }
}
