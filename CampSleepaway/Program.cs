﻿using CampSleepaway.Data;

namespace CampSleepaway
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
                SeedDb.SeedNextOfKin();
                SeedDb.SeedCabinConnections();
            }

            MainMenu.Menu();
        }
    }
}