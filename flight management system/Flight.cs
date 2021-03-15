using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
    public class Flight: IPoco
    {
        public int Id { get; set; }
        public int AirlineCompanyId { get; set; }
        public int OriginCountryId { get; set; }
        public int DestinationCountryId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
        public int RemainingTIckets { get; set; }

        public AirlineCompany AirlineCompanyObj  { get; set; }
        public Country OriginCountryObj { get; set; }
        public Country DestinationCountryObj { get; set; }


        public User UserObj { get; set; }


        public override string ToString()
        {
            return base.ToString() + JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public Flight()
        {

        }

        public Flight(int id, int airlineCompanyId, int originCountryId, int destinationCountryId, DateTime departureTime, DateTime landingTime, int remainingTIckets, AirlineCompany airlineCompanyObj, Country originCountryObj, Country destinationCountryObj, User userObj)
        {
            Id = id;
            AirlineCompanyId = airlineCompanyId;
            OriginCountryId = originCountryId;
            DestinationCountryId = destinationCountryId;
            DepartureTime = departureTime;
            LandingTime = landingTime;
            RemainingTIckets = remainingTIckets;
            AirlineCompanyObj = airlineCompanyObj;
            OriginCountryObj = originCountryObj;
            DestinationCountryObj = destinationCountryObj;
            UserObj = userObj;
        }

        public static bool operator ==(Flight flight1, Flight flight2)
        {


            if (object.ReferenceEquals(flight1, null) && object.ReferenceEquals(flight2, null))
                return true;
            if (object.ReferenceEquals(flight1, null) || object.ReferenceEquals(flight2, null))
                return false;

            return flight1.Id == flight2.Id;
        }

        public static bool operator !=(Flight flight1, Flight flight2)
        {
            return !(flight1 == flight2);
        }

        public override bool Equals(object obj)
        {
            Flight flight = obj as Flight;
            if (flight != null)
                return this.Id == flight.Id;

            return false;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }

    }
}
