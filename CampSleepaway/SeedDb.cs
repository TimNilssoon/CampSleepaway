using CampSleepaway.Data;
using CampSleepaway.Model;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text.Json.Nodes;
using System.Xml.Linq;

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
    }
}