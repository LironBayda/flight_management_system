using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
    public class CountryDAOPGSQL : IcountryDAO
    {

        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static flightManagementSystemAppConfig m_config;

        NpgsqlConnection conn;

        public CountryDAOPGSQL()
        {

            m_config = new flightManagementSystemAppConfig();
            conn = new NpgsqlConnection(m_config.ConnectionString);




        }
        public void Add(Country item)
        {
            string sp_name = "add_country";
            if (m_config.AllowDBWrite)
            {
                try
                {

                    conn.Open();


                    NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddRange(new NpgsqlParameter[]
                    {
                    new NpgsqlParameter("name", item.Name)
                    });

                    command.ExecuteNonQuery();

                    my_logger.Info($"add item: Name={item.Name}");


                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to add country. Error : {ex}");
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



        public List<Country> GetAll()
        {
            List<Country> countries = new List<Country>();

            string sp_name = "get_countries";

            try
            {

                conn.Open();


                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    my_logger.Info("in while- function GetAll return value");
                    countries.Add(getCountryFromReader(reader));


                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get all countries. Error : {ex}");
                my_logger.Error($"Run GetAll" +
                    $": [{sp_name}]");
            }

            conn.Close();
            
            return countries;

        }

        public Country Get(int id)
        {
            Country country = new Country();

            string sp_name = "get_country";

            try
            {

                conn.Open();


                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                command.Parameters.AddRange(new NpgsqlParameter[]
               {
                    new NpgsqlParameter("country_id",id)
               });


                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    my_logger.Info("in if- function Get return value");

                    country = getCountryFromReader( reader);
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get country. Error : {ex}");
                my_logger.Error($"Run Get" +
                    $": [{sp_name}]");
            }


            conn.Close();

            return country;
        }

        public void Remove(Country item)
        {
            if (m_config.AllowDBWrite)
            {
                string sp_name = "remove_country";
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
                    my_logger.Error($"Failed to remove country. Error : {ex}");
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

        Country getCountryFromReader(NpgsqlDataReader reader)
        {

            return new Country
            {
                Id = Convert.ToInt32(reader["id"]),
                Name = reader["country_name"].ToString()
            };

        }

        public void Update(Country item)
        {

            if (m_config.AllowDBWrite)
            {
                string sp_name = "update_country";
                try
                {

                    conn.Open();


                    NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddRange(new NpgsqlParameter[]
                    {
                    new NpgsqlParameter("country_id", item.Id),
                    new NpgsqlParameter("name", item.Name)

                    });

                    command.ExecuteNonQuery();
                    my_logger.Info($"update item: id={item.Id},  Name={item.Name}");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to update country. Error : {ex}");
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
