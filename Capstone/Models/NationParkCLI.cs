using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone
{
    public class NationParkCLI
    {
        private IParkDAO parkDAO;
        private ICampGroundDAO campGroundDAO;
        private IReservationDAO reservationDAO;
        private ISiteDAO siteDAO;

        private int parkId;

        public NationParkCLI(IParkDAO parkDAO, ICampGroundDAO campGroundDAO, ISiteDAO siteDAO, IReservationDAO reservationDAO)
        {
            this.parkDAO = parkDAO;
            this.campGroundDAO = campGroundDAO;
            this.reservationDAO = reservationDAO;
            this.siteDAO = siteDAO;
        }

        public void RunProgram()
        {
            while (true)
            {
                Menu();

                const string Command_ListAvailableParks = "1";
                const string Command_Quit = "q";

                string command = Console.ReadLine();

                Console.Clear();
                //Initial menu to start the program
                switch (command.ToLower())
                {
                    case Command_ListAvailableParks:
                        GetParks();
                        GetParkDetails();
                        break;

                    case Command_Quit:
                        Console.WriteLine("Thank you for using the program.");
                        return;

                    default:
                        Console.ForegroundColor = (ConsoleColor.Red);
                        Console.WriteLine("Invalid command, try again");
                        Console.ForegroundColor = (ConsoleColor.White);
                        break;
                }
            }
        }
        //Gets the list of parks and displays them for the user to select
        private void GetParks()
        {
            IList<Park> parks = parkDAO.GetParks();

            foreach (Park park in parks)
            {
                Console.WriteLine($"{park.park_id.ToString()}) - {park.name.PadLeft(5)}");
            }
        }
        //Gets the park details for the park selected by the user
        private void GetParkDetails()
        {
            this.parkId = CliHelper.GetInteger("Please choose a park for more details: ");
            IList<Park> parks = parkDAO.GetParkDetails(this.parkId);

            Console.Clear();

            foreach (Park info in parks)
            {
                Console.WriteLine($"{info.name} National Park");
                Console.WriteLine($"Location: {info.location}");
                Console.WriteLine($"Established: {info.establish_date}");
                Console.WriteLine($"Area: {info.area:N0} sq km");
                Console.WriteLine($"Annual Visitors: {info.visitors:N0}");
                Console.WriteLine();
                Console.WriteLine($"{info.description}");
                Console.WriteLine();

                CampGroundMenu();
            }
        }
        //displays the menu to either view campgrounds or search for reservation
        private void CampGroundMenu()
        {
            while (true)
            {
                Console.ForegroundColor = (ConsoleColor.Yellow);
                Console.WriteLine("Select a command");
                Console.WriteLine("");
                Console.WriteLine("1) View Campgrounds");
                Console.WriteLine("2) Search for Reservation");
                Console.WriteLine("3) Return to Previous Screen");

                string selection = Console.ReadLine();

                switch (selection.ToString())
                {
                    //if user selects to view campgrounds
                    case ("1"):
                        Console.Clear();
                        GetCampGrounds();
                        break;
                    //if user selects to search for reservations
                    case ("2"):
                        Console.Clear();
                        IList<CampGround> campGrounds = GetCampGrounds();
                        SearchReservation(campGrounds);
                        break;
                    //if user selects to return home
                    case ("3"):
                        Console.Clear();                        
                        return;
                }
            }
        }

        //displays the campgrounds within the park selected by the user
        private IList<CampGround> GetCampGrounds()
        {
            IList<CampGround> campGrounds = campGroundDAO.GetCampGrounds(this.parkId);
            Console.ForegroundColor = (ConsoleColor.Blue);
            Console.WriteLine($"".PadLeft(8)+"Name".PadRight(40) + "Open".PadRight(20) + "Close".PadRight(20) + "Daily Fee" );
            Console.WriteLine();
            Console.ForegroundColor = (ConsoleColor.White);

            foreach (CampGround cg in campGrounds)
            {
                Console.ForegroundColor = (ConsoleColor.Blue);
                Console.WriteLine($"#{cg.campground_id}\t{cg.name.PadRight(40)}{new DateTime(2001, cg.open_from_mm, 1).ToString("MMMM").PadRight(20)}{new DateTime(2001, cg.open_to_mm, 1).ToString("MMMMM").PadRight(20)}{cg.daily_fee:C2}");
                Console.ForegroundColor = (ConsoleColor.White);
                Console.WriteLine("");
            }
            return campGrounds;
        }

        //searches for available reservation based on the dates and siteId requested by the user
        private void SearchReservation(IList<CampGround> campGrounds)
        {
            Console.ForegroundColor = (ConsoleColor.Magenta);
            Console.WriteLine("Search for Available Campground Sites");
            int campgroundId = CliHelper.GetInteger("Which campground (enter 0 to cancel)?: ");

            //if statement to return user if no available sites for the specified dates
            if (campgroundId == 0)
            {
                Console.Clear();
                return;
            }
            
            //if statement that calls method to have user try again if their choice is not one of the displayed campgrounds
            if(!ValidateCampGroundId(campgroundId, campGrounds))
            {
                Console.ForegroundColor = (ConsoleColor.Red);
                Console.WriteLine("Invalid choice. Please try again.");
                Console.WriteLine("");
                SearchReservation(campGrounds);
                return;
            }

            //allows user to input their desired dates
            Console.ForegroundColor = (ConsoleColor.Magenta);
            DateTime arriveDate = CliHelper.GetDateTime("What is the arrival date? (MM/DD/YYYY): ");
            DateTime departDate = CliHelper.GetDateTime("What is the departure date? (MM/DD/YYYY): ");
                        
            IList<Site> sites = siteDAO.SearchReservation(campgroundId, arriveDate, departDate);
            //if statement that lets user try again because there is nothing available
            if (sites.Count == 0)
            {
                Console.Clear();
                Console.ForegroundColor = (ConsoleColor.Red);
                Console.WriteLine("There are no sites available for those dates, try again");
                return;
            }
            //else statement proceeds with available reservation
            else
            {
                Console.ForegroundColor = (ConsoleColor.Magenta);
                CampGround campGround = campGroundDAO.GetSingleCampGround(campgroundId);

                decimal cost = campGround.daily_fee * (decimal)(departDate - arriveDate).TotalDays;

                Console.WriteLine();
                Console.WriteLine("Site NO.".PadRight(20) + "MAX OCCUPANCY".PadRight(20) + "ACCESSIBLE".PadRight(20) + "MAX RV LENGTH".PadRight(20) + "UTILITY".PadRight(20) + "COST");
                Console.WriteLine();

                //foreach loops displays the available sites
                foreach (Site site in sites)
                {
                    Console.ForegroundColor = (ConsoleColor.Magenta);
                    Console.WriteLine($"#{site.siteId}".PadRight(20)+ $"{site.maxOccupancy}".PadRight(20)+ $"{site.accessible}".PadRight(20)+ $"{site.maxRvLength}".PadRight(20)+ $"{site.utilities}".PadRight(20)+ $"{cost:C2}");
                }
                Console.ForegroundColor = (ConsoleColor.Magenta);
                Console.WriteLine();
                CreateReservation(arriveDate, departDate);
            }
        }

        //method that validates the user's choice was one of the campgrounds displayed
        private bool ValidateCampGroundId(int campGroundId, IList<CampGround> campGrounds)
        {            
            foreach (CampGround cg in campGrounds)
            {
                if (cg.campground_id == campGroundId)
                {
                    return true;
                }
            }
            return false;
        }

        //creates a reservation based on the user's chosen dates and siteId
        private void CreateReservation(DateTime arriveDate, DateTime departDate)
        {
            Console.ForegroundColor = (ConsoleColor.Magenta);
            int siteIdSelection = CliHelper.GetInteger("Which site should be reserved (enter 0 to cancel)?: ");
            //if user chooses 0 then return to original menu
            if (siteIdSelection == 0)
            {
                Console.Clear();
                return;
            }
            //else statement proceeds with good reservation
            else
            {
                Console.ForegroundColor = (ConsoleColor.Magenta);
                string reservedName = CliHelper.GetString("What name should the reservation be made under?: ");

                Reservation reservation = new Reservation
                {
                    siteID = siteIdSelection,
                    name = reservedName,
                    fromDate = arriveDate,
                    toDate = departDate
                };

                int reservationId = reservationDAO.CreateReservation(reservation);

                if (reservationId > 0)
                {
                    Console.ForegroundColor = (ConsoleColor.Magenta);
                    Console.WriteLine("Your reservation has been added");
                    Console.WriteLine();
                }
                else
                {
                    Console.ForegroundColor = (ConsoleColor.Red);
                    Console.WriteLine("Error, try again");
                    Console.WriteLine();
                }
            }
        }

        public void Menu()
        {
            Console.ForegroundColor = (ConsoleColor.Green);
            Console.WriteLine("Welcome to the National Parks Reservation Program");
            Console.WriteLine("1) Show a list of all parks");
            Console.WriteLine("Q) - Quit");
        }
    }
}



