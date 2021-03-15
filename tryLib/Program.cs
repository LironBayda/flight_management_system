using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using flight_management_system;
using log4net.Config;
using log4net.Repository;

namespace tryLib
{
    class Program
    {

        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static flightManagementSystemAppConfig m_config;

       
            static void Main(string[] args)
        {
            m_config = new flightManagementSystemAppConfig();

            Console.WriteLine(m_config.AppName);

            ILoggerRepository repository = log4net.LogManager.GetRepository(Assembly.GetCallingAssembly());

            var fileInfo = new FileInfo(@"log4net.config");

            log4net.Config.XmlConfigurator.Configure(repository, fileInfo);

            my_logger.Info("******************** System startup");


            m_config = new flightManagementSystemAppConfig();

            /* Country c1 = new Country{
                 Id = 22,
                 Name = "russia"

             };

             Country c2 = new Country
             {
                 Id = 1,
                 Name = "US3"

             };

             Country c3 = new Country
             {
                 Id = 9,
                 Name = "US1"

             };

             CountryDAOPGSQL countryDAOPGSQ = new CountryDAOPGSQL();
           //  countryDAOPGSQ.Add(c1);
             //countryDAOPGSQ.Update(c2);
             Console.WriteLine("get:");
             Console.WriteLine(countryDAOPGSQ.Get(2).ToString());
           //  countryDAOPGSQ.Remove(c3);

             Console.WriteLine("get all");
             countryDAOPGSQ.GatAll().ForEach(c => Console.WriteLine(c.ToString()));
             Console.ReadLine();
             *?
            /*  Administrator a1 = new Administrator
              {

                  Id = 9,
                  FirstName = "avi",
                  LastName = "segev",
                  Level = 3,
                  UserId = 3


              };

              Administrator a2 = new Administrator
              {

                  Id = 6,
                  FirstName = "avia",
                  LastName = "segevit",
                  Level = 2,
                  UserId = 3


              };


              Administrator a3 = new Administrator
              {

                  Id =4,
                  FirstName = "danny",
                  LastName = "chon",
                  Level = 3,
                  UserId =1


              };

              AdminDAOPGSQL adminDAOPGSQL  = new AdminDAOPGSQL();
             // adminDAOPGSQL.Add(a1);
             // adminDAOPGSQL.Update(a2);
              Console.WriteLine("get:");
              Console.WriteLine(adminDAOPGSQL.Get(2).ToString());
            //  adminDAOPGSQL.Remove(a3);

              Console.WriteLine("get all");
              adminDAOPGSQL.GatAll().ForEach(c => Console.WriteLine(c.ToString()));
              Console.ReadLine();

              my_logger.Info("******************** System shutdown");
              */

            /*   User u1 = new User
               {
                   Id = 1,
                   Email = "ppvvvvppp",
                   Password = "ffvvvvffff",
                   Username = "sddsfvvvvdfj",
                   UserRoleId = 3,
               };

               User u2 = new User
               {
                   Id = 1,
                   Email = "ccc",
                   Password = "jccc",
                   Username = "cc",
                   UserRoleId = 3,
               };

               User u3 = new User
               {
                   Id = 6,
                   Email = "dd",
                   Password = "ddds",
                   Username = "dcvvvd",
                   UserRoleId = 3,
               };

               UserDAOPGSQL userDAOPGSQL = new UserDAOPGSQL();           
               userDAOPGSQL.Add(u1);
               userDAOPGSQL.Update(u3);
               Console.WriteLine("get:");
               Console.WriteLine(userDAOPGSQL.Get(2).ToString());
             //  userDAOPGSQL.Remove(u2);

               Console.WriteLine("get all");
               userDAOPGSQL.GetAll().ForEach(c => Console.WriteLine(c.ToString()));
               Console.ReadLine();
              */



            AirlineDAOPGSQL airlineDAOPGSQL = new AirlineDAOPGSQL();

            Console.WriteLine (airlineDAOPGSQL.Get(2).ToString());
            airlineDAOPGSQL.GetAll().ForEach(a=> Console.WriteLine(a));
            Console.WriteLine(airlineDAOPGSQL.GetAirlineByUserame("").ToString());
            airlineDAOPGSQL.GetAllAirlinesByCountry(2).ForEach(a => Console.WriteLine(a));

            FlightDAOPGSQL flightDAOPGSQL = new FlightDAOPGSQL();
           flightDAOPGSQL.GetAll().ForEach(a => Console.WriteLine(a));
           
            my_logger.Info("******************** System shutdown");
        }
    }
}
