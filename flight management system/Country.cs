using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
    public class Country:IPoco
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public Country()
        {
                
        }

        public Country(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static bool operator ==(Country country1, Country country2)
        {


            if (object.ReferenceEquals(country1, null) && object.ReferenceEquals(country2, null))
                return true;
            if (object.ReferenceEquals(country1, null) || object.ReferenceEquals(country2, null))
                return false;

            return country1.Id == country2.Id;
        }

        public static bool operator !=(Country country1, Country country2)
        {
            return !(country1 == country2);
        }

        public override bool Equals(object obj)
        {
            Country country = obj as Country;
            if (country != null)
                return this.Id == country.Id;

            return false;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }

    }
}
