using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
   public class FlightDAOPGSQL:IFlightDAO
    {

        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static flightManagementSystemAppConfig m_config;

        NpgsqlConnection conn;

        public FlightDAOPGSQL()
        {

            m_config = new flightManagementSystemAppConfig();
            conn = new NpgsqlConnection(m_config.ConnectionString);

        }
        public void Add(Flight item)
        {
            string sp_name = "add_flight";
            if (m_config.AllowDBWrite)
            {
                try
                {
                    conn.Open();


                    NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddRange(new NpgsqlParameter[]
                    {
                    new NpgsqlParameter("_airline_company_id", item.AirlineCompanyId),
                    new NpgsqlParameter("_origin_country_id", item.OriginCountryId),
                    new NpgsqlParameter("_destination_country_id", item.DestinationCountryId),
                    new NpgsqlParameter("_departure_time", item.DepartureTime),
                    new NpgsqlParameter("_landing_time", item.LandingTime),
                    new NpgsqlParameter("_remaining_tickets", item.RemainingTIckets),

                    });

                    command.ExecuteNonQuery();

                    my_logger.Info($"add item:  id={item.Id}");


                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to add flight. Error : {ex}");
                    my_logger.Error($"Run Add" +
                        $": [{sp_name}]");

                }
            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
            }

            conn.Close();

        }



        public List<Flight> GetAll()
        {
            List<Flight>  flights = new List<Flight>();

            string sp_name = "get_flights";

            try
            {

                conn.Open();


                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var reader = command.ExecuteReader();


                while (reader.Read())
                {
                    my_logger.Info("in while- function GetAll return value");
                    flights.Add(getFlightFromReader(reader));

                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get all flights. Error : {ex}");
                my_logger.Error($"Run GetAll" +
                    $": [{sp_name}]");
            }

            conn.Close();

            return flights;

        }

        public Flight Get(int id)
        {
            Flight flight = new Flight();

            string sp_name = "get_flight";

            try
            {

                conn.Open();


                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                command.Parameters.AddRange(new NpgsqlParameter[]
               {
                    new NpgsqlParameter("flight_id",id)
               });


                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    my_logger.Info("in if- function Get return value");

                 
                    flight = getFlightFromReader(reader);
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get flight. Error : {ex}");
                my_logger.Error($"Run Get" +
                    $": [{sp_name}]");
            }


            conn.Close();

            return flight;
        }

        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            throw new NotImplementedException();
        }

        public List<Flight> GetFlightsByCustomer(Customer customer)
        {
            List<Flight> flights = new List<Flight>();

            string sp_name = "get_flight_by_customer";

            try
            {

                conn.Open();


                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                command.Parameters.AddRange(new NpgsqlParameter[]
               {
                    new NpgsqlParameter("_customer_id",customer.Id)
               });


                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    my_logger.Info("in while- function GetFlightsByCustomer return value");

                 
                    flights.Add(getFlightFromReader(reader)) ; 
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get flight by depature time. Error : {ex}");
                my_logger.Error($"Run GetFlightsByCustomer" +
                    $": [{sp_name}]");
            }


            conn.Close();

            return flights;
        }

        private Flight getFlightFromReader(NpgsqlDataReader reader)
        {
            CountryDAOPGSQL countryDAOPGSQL = new CountryDAOPGSQL();
            AirlineDAOPGSQL airlineDAOPGSQL = new AirlineDAOPGSQL();

            return new Flight
            {



                Id = Convert.ToInt32(reader["id"]),
                AirlineCompanyId = Convert.ToInt32(reader["airline_company_id"]),
                OriginCountryId = Convert.ToInt32(reader["origin_country_id"]),
                DestinationCountryId = Convert.ToInt32(reader["destination_country_id"]),
                DepartureTime = DateTime.Parse(reader["departure_time"].ToString()),
                LandingTime = DateTime.Parse(reader["landing_time"].ToString()),
                RemainingTIckets = Convert.ToInt32(reader["remaining_tickets"]),




                AirlineCompanyObj = airlineDAOPGSQL.Get(Convert.ToInt32(reader["airline_company_id"])),
                OriginCountryObj = countryDAOPGSQL.Get(Convert.ToInt32(reader["origin_country_id"])),
                DestinationCountryObj = countryDAOPGSQL.Get(Convert.ToInt32(reader["destination_country_id"])),


            };
        }

        public List<Flight> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            List<Flight> flights = new List<Flight>();

            string sp_name = "get_flight_by_depature_time";

            try
            {

                conn.Open();


                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                command.Parameters.AddRange(new NpgsqlParameter[]
               {
                    new NpgsqlParameter("_departure_time",departureDate)
               });


                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    my_logger.Info("in while- function get_flight_by_depature_time return value");

                

                    flights.Add(
                        getFlightFromReader(reader)
                        );
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get flight by depature time. Error : {ex}");
                my_logger.Error($"Run GetFlightsByDepatrureDate" +
                    $": [{sp_name}]");
            }


            conn.Close();

            return flights;
        }

        public List<Flight> GetFlightsByDestinationCountry(int countryCode)
        {
            List<Flight> flights = new List<Flight>();

            string sp_name = "get_flight_by_destination_country";

            try
            {

                conn.Open();


                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                command.Parameters.AddRange(new NpgsqlParameter[]
               {
                    new NpgsqlParameter("_destination_country_id",countryCode)
               });


                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    my_logger.Info("in while- function get flights by destination country return value");


                    flights.Add(getFlightFromReader(reader));
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get flight by destination country. Error : {ex}");
                my_logger.Error($"Run GetFlightsByDestinationCountry" +
                    $": [{sp_name}]");
            }


            conn.Close();

            return flights;
        }

        public List<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            List<Flight> flights = new List<Flight>();

            string sp_name = "get_flight_by_landing_time";

            try
            {

                conn.Open();


                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                command.Parameters.AddRange(new NpgsqlParameter[]
               {
                    new NpgsqlParameter("_landing_time",landingDate)
               });


                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    my_logger.Info("in while- function get_flight_by_landing_time return value");

                  
                    flights.Add(getFlightFromReader(reader));
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get flight by landing time. Error : {ex}");
                my_logger.Error($"Run GetFlightsByLandingDate" +
                    $": [{sp_name}]");
            }


            conn.Close();

            return flights;
        }

        public List<Flight> GetFlightsByOriginCountry(int countryCode)
        {
            List<Flight> flights = new List<Flight>();

            string sp_name = "get_flight_by_origin_country";

            try
            {

                conn.Open();


                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                command.Parameters.AddRange(new NpgsqlParameter[]
               {
                    new NpgsqlParameter("_origin_country_id",countryCode)
               });


                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    my_logger.Info("in while- function GetFlightsByOriginCountry return value");

                

                    flights.Add (getFlightFromReader( reader))
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get flights by origin country. Error : {ex}");
                my_logger.Error($"Run GetFlightsByOriginCountry" +
                    $": [{sp_name}]");
            }


            conn.Close();

            return flights;
        }

        public void Remove(Flight item)
        {
            if (m_config.AllowDBWrite)
            {
                string sp_name = "remove_flight";
                try
                {

                    conn.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddRange(new NpgsqlParameter[]
                    {
                    new NpgsqlParameter("remove_id", item.Id)
                    });

                    command.ExecuteNonQuery();
                    my_logger.Info($"remove item: id={item.Id}");

                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to remove flight. Error : {ex}");
                    my_logger.Error($"Run Remove" +
                        $": [{sp_name}]");
                }
                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
            }
        }

        public void Update(Flight item)
        {

            if (m_config.AllowDBWrite)
            {
                string sp_name = "update_flight";
                try
                {

                    conn.Open();


                    NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddRange(new NpgsqlParameter[]
                   {
                    new NpgsqlParameter("_id", item.Id),
                    new NpgsqlParameter("_airline_company_id", item.AirlineCompanyId),
                    new NpgsqlParameter("_origin_country_id", item.OriginCountryId),
                    new NpgsqlParameter("_destination_country_id", item.DestinationCountryId),
                    new NpgsqlParameter("_departure_time", item.DepartureTime),
                    new NpgsqlParameter("_landing_time", item.LandingTime),
                    new NpgsqlParameter("_remaining_tickets", item.RemainingTIckets),

                    });

                    command.ExecuteNonQuery();
                    my_logger.Info($"update item: id={item.Id} ");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to update flight. Error : {ex}");
                    my_logger.Error($"Run Update" +
                        $": [{sp_name}]");
                }

                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");

            }
        }
    }
}
