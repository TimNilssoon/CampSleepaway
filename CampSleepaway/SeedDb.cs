using CampSleepaway.Data;
using CampSleepaway.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CampSleepaway
{
    public class SeedDb
    {
        public static void SeedCabins()
        {
            using StreamReader reader = new(@"Seed Data\Cabins.json");
            var cabins = JsonConvert.DeserializeObject<List<Cabin>>(reader.ReadToEnd());

            using CampSleepawayContext context = new();

            context.AddRange(cabins);

            context.SaveChanges();
        }

        public static void SeedCampers()
        {
            using StreamReader reader = new(@"Seed Data\Campers.json");
            var campers = JsonConvert.DeserializeObject<List<Camper>>(reader.ReadToEnd(),
                new IsoDateTimeConverter { DateTimeFormat = "yy-MM-dd" });

            using CampSleepawayContext context = new();

            context.AddRange(campers);
            context.SaveChanges();
        }

        public static void SeedCouncelors()
        {
            using StreamReader reader = new(@"Seed Data\Councelors.json");
            var councelors = JsonConvert.DeserializeObject<List<Councelor>>(reader.ReadToEnd(),
                new IsoDateTimeConverter { DateTimeFormat = "yy-MM-dd" });

            using CampSleepawayContext context = new();

            context.AddRange(councelors);
            context.SaveChanges();
        }

        public static void SeedNextOfKins()
        {
            using StreamReader reader = new(@"Seed Data\NextOfKin.json");
            var nextOfKin = JsonConvert.DeserializeObject<List<NextOfKin>>(reader.ReadToEnd(),
                new IsoDateTimeConverter { DateTimeFormat = "yy-MM-dd" });

            using (CampSleepawayContext context = new())
            {
                context.AddRange(nextOfKin);

                context.SaveChanges();
            }

            using (CampSleepawayContext context = new())
            {
                // Fetch four campers and next of kin and assign them in order
                var campersDb = context.Campers.Take(4).ToArray();
                var nextOfKinDb = context.NextOfKins.Take(4).ToArray();
                for (int i = 0; i < 4; i++)
                {
                    context.Visits.Add(new Visit() { Camper = campersDb[i] });
                    context.CamperNextOfKins.Add(new CamperNextOfKin() { Camper = campersDb[i], NextOfKin = nextOfKinDb[i] });
                }

                context.SaveChanges();
            }

            using (CampSleepawayContext context = new())
            {
                // Seed visits for the four campers with next of kin
                var visits = context.Visits.Take(4).ToArray();
                var nextOfKinDb = context.NextOfKins.Take(4).ToArray();

                for (int i = 0; i < 4; i++)
                {
                    nextOfKinDb[i].Visit = visits[i];
                }

                context.SaveChanges();
            }
        }

        public static void SeedCabinConnections()
        {
            using (CampSleepawayContext context = new())
            {
                var cabinsDb = context.Cabins.ToArray();
                var councelorsDb = context.Councelors.ToArray();
                var campersDb = context.Campers.ToArray();

                // Something similar to a sliding window search to seed the campers into cabins
                // Seed a councelor into cabins and assign 4 campers to each cabin
                int max = 4;
                int min = 0;
                for (int i = 0; i < cabinsDb.Length; i++)
                {
                    cabinsDb[i].Campers = new List<Camper>();
                    for (int j = min; j < max; j++)
                    {
                        cabinsDb[i].Campers.Add(campersDb[j]);
                    }

                    min = max;
                    max += 4;

                    cabinsDb[i].Councelor = councelorsDb[i];
                }
                context.SaveChanges();
            }
        }
    }
}