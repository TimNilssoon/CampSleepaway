﻿using CampSleepaway.Data;
using CampSleepaway.Menus;

namespace CampSleepaway
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Checks if any db tables are empty, if they are they will be seeded.
            using CampSleepawayContext context = new();

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