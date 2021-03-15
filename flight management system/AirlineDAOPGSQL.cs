using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
   public class AirlineDAOPGSQL:IAirlineDAO
    {

        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static flightManagementSystemAppConfig m_config;

        NpgsqlConnection conn;

        public AirlineDAOPGSQL()
        {

            m_config = new flightManagementSystemAppConfig();
            conn = new NpgsqlConnection(m_config.ConnectionString);

        }
        public void Add(AirlineCompany item)
        {
            string sp_name = "add_airline_company";
            if (m_config.AllowDBWrite)
            {
                try
                {

                    conn.Open();


                    NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddRange(new NpgsqlParameter[]
                    {
                    new NpgsqlParameter("_airline_company_name", item.Name),
                    new NpgsqlParameter("_country_id", item.CountryId),
                    new NpgsqlParameter("_user_id", item.UserId),

                    });

                    command.ExecuteNonQuery();

                    my_logger.Info($"add item:  name={item.Name}");


                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to add airline company. Error : {ex}");
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



        public List<AirlineCompany> GetAll()
        {
            List<AirlineCompany> airlineCompanies = new List<AirlineCompany>();

            string sp_name = "get_airline_companies";

            try
            {

                conn.Open();


                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var reader = command.ExecuteReader();

                //we use it to get user and country object
                CountryDAOPGSQL countryDAOPGSQL = new CountryDAOPGSQL();
                UserDAOPGSQL userDAOPGSQL = new UserDAOPGSQL();

                while (reader.Read())
                {
                    my_logger.Info("in while- function GetAll return value");
                    airlineCompanies.Add(getAirlineCompanyFromReader(reader));

                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get all airline_companies. Error : {ex}");
                my_logger.Error($"Run GetAll" +
                    $": [{sp_name}]");
            }

            conn.Close();

            return airlineCompanies;

        }

        public AirlineCompany Get(int id)
        {
            AirlineCompany airlineCompany = new AirlineCompany();

            string sp_name = "get_airline_company_by_Id";

            try
            {

                conn.Open();


                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                command.Parameters.AddRange(new NpgsqlParameter[]
               {
                    new NpgsqlParameter("airline_company_id",id)
               });


                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    my_logger.Info("in if- function Get return value");


                    airlineCompany = getAirlineCompanyFromReader(reader);
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get user. Error : {ex}");
                my_logger.Error($"Run Get" +
                    $": [{sp_name}]");
            }


            conn.Close();

            return airlineCompany;
        }

        public AirlineCompany GetAirlineByUserame(string name)
        {
           AirlineCompany airlineCompany = new AirlineCompany();

            string sp_name = "get_airline_company_by_username";

            try
            {

                conn.Open();


                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                command.Parameters.AddRange(new NpgsqlParameter[]
               {
                    new NpgsqlParameter("_username",name)
               });


                var reader = command.ExecuteReader();
               

                if (reader.Read())
                {
                    my_logger.Info("in if- function GetAirlineByUserame return value");
                    airlineCompany = getAirlineCompanyFromReader(reader);
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get airline company by username. Error : {ex}");
                my_logger.Error($"Run GetAirlineByUserame" +
                    $": [{sp_name}]");
            }


            conn.Close();

            return airlineCompany;
        }


        public List<AirlineCompany> GetAllAirlinesByCountry(int countryId)
        {
            List<AirlineCompany> airlineCompanies = new List<AirlineCompany>();

            string sp_name = "get_airline_company_by_country";

            try
            {

                conn.Open();


                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                command.Parameters.AddRange(new NpgsqlParameter[]
               {
                    new NpgsqlParameter("_country_id",countryId)
               });


                var reader = command.ExecuteReader();
              

                while (reader.Read())
                {
                    my_logger.Info("in while- function GetAllAirlinesByCountry return value");
                    airlineCompanies.Add(getAirlineCompanyFromReader( reader));
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get airline company by country. Error : {ex}");
                my_logger.Error($"Run GetAllAirlinesByCountry" +
                    $": [{sp_name}]");
            }


            conn.Close();

            return airlineCompanies;
        }


        private AirlineCompany getAirlineCompanyFromReader(NpgsqlDataReader reader)
        {
            CountryDAOPGSQL countryDAOPGSQL = new CountryDAOPGSQL();
            UserDAOPGSQL userDAOPGSQL = new UserDAOPGSQL();
            return new AirlineCompany
            {

                Id = Convert.ToInt32(reader["id"]),
                Name = reader["airline_company_name"].ToString(),
                CountryId = Convert.ToInt32(reader["country_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                CountryObj = countryDAOPGSQL.Get(Convert.ToInt32(reader["country_id"])),
                UserObj = userDAOPGSQL.Get(Convert.ToInt32(reader["user_id"]))
                    

            };
        }


        public void Remove(AirlineCompany item)
        {
            if (m_config.AllowDBWrite)
            {
                string sp_name = "remove_airline_company";
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
                    my_logger.Error($"Failed to remove Airline Company. Error : {ex}");
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

        public void Update(AirlineCompany item)
        {

            if (m_config.AllowDBWrite)
            {
                string sp_name = "update_airline_company";
                try
                {

                    conn.Open();


                    NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddRange(new NpgsqlParameter[]
                    {
                    new NpgsqlParameter("_id", item.Id),
                    new NpgsqlParameter("_airline_company_name", item.Name),
                    new NpgsqlParameter("_country_id", item.CountryId),
                    new NpgsqlParameter("_user_id", item.UserId),


                    });

                    command.ExecuteNonQuery();
                    my_logger.Info($"update item: id={item.Id},  name={item.Name}");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to update airline company. Error : {ex}");
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
