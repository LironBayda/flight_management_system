using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
    class TicketDAOPGSQL:ITicketDAO
    {

        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static flightManagementSystemAppConfig m_config;

        NpgsqlConnection conn;

        public TicketDAOPGSQL()
        {

            m_config = new flightManagementSystemAppConfig();
            conn = new NpgsqlConnection(m_config.ConnectionString);




        }
        public void Add(Administrator item)
        {
            string sp_name = "add_administrator";
            if (m_config.AllowDBWrite)
            {
                try
                {

                    conn.Open();


                    NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddRange(new NpgsqlParameter[]
                    {
                    new NpgsqlParameter("_first_name", item.FirstName),
                    new NpgsqlParameter("_last_name", item.LastName),
                    new NpgsqlParameter("_administrator_level", item.Level),
                    new NpgsqlParameter("_user_id", item.UserId),


                    });

                    command.ExecuteNonQuery();

                    my_logger.Info($"add item: First_Name={item.FirstName}, Last_Name={item.LastName}");


                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to add administrator. Error : {ex}");
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



        public List<Administrator> GetAll()
        {
            List<Administrator> administrators = new List<Administrator>();

            string sp_name = "get_administrators";

            try
            {

                conn.Open();


                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    my_logger.Info("in while- function GetAll return value");
                    administrators.Add(getAdministratorFromReader(reader));

                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get all a. Error : {ex}");
                my_logger.Error($"Run GetAll" +
                    $": [{sp_name}]");
            }

            conn.Close();

            return administrators;

        }

        public Administrator Get(int id)
        {
            Administrator administrator = new Administrator();

            string sp_name = "get_administrator";

            try
            {

                conn.Open();


                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                command.Parameters.AddRange(new NpgsqlParameter[]
               {
                    new NpgsqlParameter("administrator_id",id)
               });


                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    my_logger.Info("in if- function Get return value");

                    administrator = getAdministratorFromReader(reader);
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get dministrator. Error : {ex}");
                my_logger.Error($"Run Get" +
                    $": [{sp_name}]");
            }


            conn.Close();

            return administrator;
        }


        Ticket getAdministratorFromReader(NpgsqlDataReader reader)
        {
            FlightDAOPGSQL flightDAOPGSQL = new FlightDAOPGSQL();
                
                return new Ticket
            {
                Id = Convert.ToInt32(reader["id"]),
                FlightId= Convert.ToInt32(reader["id"]),
                CustomerId= Convert.ToInt32(reader["id"]),
                //CustomerObj=
                FlightObj=flightDAOPGSQL.Get(Convert.ToInt32(reader["id"]))
            };



        }




        public void Remove(Administrator item)
        {
            if (m_config.AllowDBWrite)
            {
                string sp_name = "remove_administrator";
                try
                {

                    conn.Open();


                    NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddRange(new NpgsqlParameter[]
                    {
                    new NpgsqlParameter("administrator_id", item.Id)
                    });

                    command.ExecuteNonQuery();
                    my_logger.Info($"remove item: id={item.Id}");

                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to remove administrator. Error : {ex}");
                    my_logger.Error($"Run Remove" +
                        $": [{sp_name}]");
                }
                conn.Close();

            }
            else
            {
                my_logger.Info("Tried to write into Db while in read-pnly mode");
                Console.WriteLine($"Not allow to write into DB. check config");
            }
        }

        public void Update(Administrator item)
        {

            if (m_config.AllowDBWrite)
            {
                string sp_name = "update_administrator";
                try
                {

                    conn.Open();


                    NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddRange(new NpgsqlParameter[]
                    {
                    new NpgsqlParameter("_administrator_id", item.Id   ),
                    new NpgsqlParameter("_first_name", item.FirstName),
                    new NpgsqlParameter("_last_name", item.LastName),
                    new NpgsqlParameter("_administrator_level", item.Level),
                    new NpgsqlParameter("_user_id", item.UserId),

                    });

                    command.ExecuteNonQuery();
                    my_logger.Info($"update item: id={item.Id},  frist_name={item.FirstName}");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to update administrator. Error : {ex}");
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
