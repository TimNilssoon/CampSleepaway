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

        public static void SeedNextOfKin()
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
                var campersDb = context.Campers.Take(4).ToArray();
                var nextOfKinDb = context.NextOfKins.Take(4).ToArray();
                for (int i = 0; i < 4; i++)
                {
                    context.CamperNextOfKins.Add(new CamperNextOfKin() { Camper = campersDb[i], NextOfKin = nextOfKinDb[i] });
                }

                context.SaveChanges();
            }
        }
    }
}