using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
    interface IFlightDAO:IBasicDb<Flight>
    {
        Dictionary<Flight, int> GetAllFlightsVacancy();
        List<Flight> GetFlightsByOriginCountry(int countryCode);
        List<Flight> GetFlightsByDestinationCountry(int countryCode);
        List<Flight> GetFlightsByDepatrureDate(DateTime departureDate);
        List<Flight> GetFlightsByLandingDate(DateTime landingDate);
        List<Flight> GetFlightsByCustomer(Customer customer);
    }
}
