using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
    interface IAirlineDAO:IBasicDb<AirlineCompany>
    {
        AirlineCompany GetAirlineByUserame(string name);
        List<AirlineCompany> GetAllAirlinesByCountry(int countryId);
    }
}
