using CampSleepaway.Data;
using CampSleepaway.Menus;

namespace CampSleepaway
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Checks if any db tables are empty, if they are they will be seeded.
            using CampSleepawayContext context = new();

            // Check if DB returns any data from our main tables
            var dbCamper = context.Campers.FirstOrDefault();
            var dbCouncelor = context.Councelors.FirstOrDefault();
            var dbCabin = context.Cabins.FirstOrDefault();

            if (dbCouncelor is null)
            {
                SeedDb.SeedCouncelors();
            }

            if (dbCabin is null)
            {
                SeedDb.SeedCabins();
            }

            // Camper is seeded last since it is dependant on there being data in the other two tables to work properly. 
            // It is done none linearly to be able to test Camper data. This way we dont have to drop the entire Db, instead we can just clear the Camper table
            if (dbCamper is null)
            {
                SeedDb.SeedCampers();
                SeedDb.SeedNextOfKins();
                SeedDb.SeedCabinConnections();
            }

            MainMenu.Menu();
        }
    }
}