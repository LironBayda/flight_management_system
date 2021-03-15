using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
    public class UserDAOPGSQL:IUserDAO
    {

        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static flightManagementSystemAppConfig m_config;

        NpgsqlConnection conn;

        public UserDAOPGSQL()
        {

            m_config = new flightManagementSystemAppConfig();
            conn = new NpgsqlConnection(m_config.ConnectionString);

        }
        public void Add(User item)
        {
            string sp_name = "add_user";
            if (m_config.AllowDBWrite)
            {
                try
                {

                    conn.Open();


                    NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddRange(new NpgsqlParameter[]
                    {
                    new NpgsqlParameter("_username", item.Username),
                    new NpgsqlParameter("_user_password", item.Password),
                    new NpgsqlParameter("_email", item.Email),
                    new NpgsqlParameter("_user_role", item.UserRoleId),
              

                    });

                    command.ExecuteNonQuery();

                    my_logger.Info($"add item: User name={item.Username}");


                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to add user. Error : {ex}");
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



        public List<User> GetAll()
        {
            List<User> users   = new List<User>();

            string sp_name = "get_users";

            try
            {

                conn.Open();


                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    my_logger.Info("in while- function GetAll return value");
                    users.Add(getUserFromReader( reader));

                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get all users. Error : {ex}");
                my_logger.Error($"Run GetAll" +
                    $": [{sp_name}]");
            }

            conn.Close();

            return users;

        }

        public User Get(int id)
        {
            User user = new User();

            string sp_name = "get_user";

            try
            {

                conn.Open();


                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                command.Parameters.AddRange(new NpgsqlParameter[]
               {
                    new NpgsqlParameter("user_id",id)
               });


                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    my_logger.Info("in if- function Get return value");

                    user = getUserFromReader( reader);
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get user. Error : {ex}");
                my_logger.Error($"Run Get" +
                    $": [{sp_name}]");
            }


            conn.Close();

            return user;
        }
        
        
        User getUserFromReader(NpgsqlDataReader reader)

        {
        return new User
        {
            Id = Convert.ToInt32(reader["id"]),
            Username = reader["username"].ToString(),
            Password = reader["password"].ToString(),
            Email = reader["email"].ToString(),
            UserRoleId = Convert.ToInt32(reader["user_role"]),
            UserRoleObj=new UserRole(Convert.ToInt32(reader["user_role"]))
        };

         


        }
        public void Remove(User item)
        {
            if (m_config.AllowDBWrite)
            {
                string sp_name = "remove_user";
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
                    my_logger.Error($"Failed to remove user. Error : {ex}");
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

        public void Update(User item)
        {

            if (m_config.AllowDBWrite)
            {
                string sp_name = "update_user";
                try
                {

                    conn.Open();


                    NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddRange(new NpgsqlParameter[]
                    {
                    new NpgsqlParameter("_user_id", item.Id),
                    new NpgsqlParameter("_username", item.Username),
                    new NpgsqlParameter("_user_password", item.Password),
                    new NpgsqlParameter("_email", item.Email),
                    new NpgsqlParameter("_user_role", item.UserRoleId),


                    });

                    command.ExecuteNonQuery();
                    my_logger.Info($"update item: id={item.Id},  Username={item.Username}");
                }
                catch (Exception ex)
                {
                    my_logger.Error($"Failed to update user. Error : {ex}");
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
