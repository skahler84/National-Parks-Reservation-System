using Capstone.DAL;
using Capstone.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the connection string from the appsettings.json file
                 IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("CampGDB");

            IParkDAO parkDAO = new ParkDAO(connectionString);
            ICampGroundDAO campGroundDAO = new CampgroundDAO(connectionString);                
            ISiteDAO siteDAO = new SiteDAO(connectionString);
            IReservationDAO reservationDAO = new ReservationDAO(connectionString);

            NationParkCLI nationParkCLI = new NationParkCLI(parkDAO, campGroundDAO, siteDAO, reservationDAO);
            nationParkCLI.RunProgram();
        }
    }
}
