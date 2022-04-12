using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LINQ
{
    public class Lab09
    {
        public class RootObject
        {
            public string type { get; set; }
            public Feature[] features { get; set; }
        }

        public class Feature
        {
            public string type { get; set; }
            public Geometry geometry { get; set; }
            public Properties properties { get; set; }
        }

        public class Geometry
        {
            public string type { get; set; }
            public float[] coordinates { get; set; }
        }

        public class Properties
        {
            public string zip { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string address { get; set; }
            public string borough { get; set; }
            public string neighborhood { get; set; }
            public string county { get; set; }
        }

        public static void Filter()
        {
            // Here, I tried to get a relative path, but I couldn't get one, so I used my absolute path.
            // Note: to test my solution, just uncomment the foreach to see the output for each question.
            using (StreamReader reader = File.OpenText(@"../../../../data.json"))

            {   // Read data.json file.
                JObject dataFile = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                // Deserialize Object and convert it to string.
                var result = JsonConvert.DeserializeObject<RootObject>(dataFile.ToString());

                // Q1: Output all of the neighborhoods in this data list (Final Total: 147 neighborhoods).
                var neighborhoods = from n in result.features
                                    select n.properties.neighborhood;
                Console.WriteLine($"All neighborhoods by using LINQ query statements: { neighborhoods.Count()}.");
                //foreach (var item in neighborhoods)
                //{
                //    Console.WriteLine($"Neighborhood: {item}");
                //}

                // Q2: Filter out all the neighborhoods that do not have any names (Final Total: 143).
                var filteredNeighborhoods = from n in neighborhoods
                                            where (n != "")
                                            select n;
                Console.WriteLine($"Filtered neighborhoods by using LINQ query statements: { filteredNeighborhoods.Count()}.");
                //foreach (var item in filteredNeighborhoods)
                //{
                //    Console.WriteLine($"Neighborhood: {item}");
                //}

                // Q3: Remove the duplicates (Final Total: 39 neighborhoods).
                // You can use GroupBy or Distinct but I used Distinct.
                var uniqueNeighborhoods = filteredNeighborhoods.Distinct().ToList();
                //var uniqueNeighborhoods = filteredNeighborhoods.GroupBy(x => x).Select(x => x.First());
                Console.WriteLine($"Distinct neighborhoods after filtering: { uniqueNeighborhoods.Count()}.");
                //foreach (var item in uniqueNeighborhoods)
                //{
                //    Console.WriteLine($"Neighborhood: {item}");
                //}

                // Q4: Rewrite the queries from above and consolidate all into one single query.
                var finalNeighborhoods = result.features.Where(s => s.properties.neighborhood != "").Select(s => s.properties.neighborhood).Distinct().ToList();
                Console.WriteLine($"Distinct neighborhoods by using LINQ method calls: { finalNeighborhoods.Count()}.");
                //foreach (var item in finalNeighborhoods)
                //{
                //    Console.WriteLine($"Neighborhood: {item}");
                //}

                /* Q5: Rewrite at least one of these questions only using the opposing method
                   (example: Use LINQ Query statements instead of LINQ method calls and vice versa.)
                Final Total: 147 neighborhoods but by using LINQ method instead of LINQ Query statements. */
                var NeighborhoodsUsingMethod = result.features.Select(s => s.properties.neighborhood);
                Console.WriteLine($"All neighborhoods by using LINQ method calls: { NeighborhoodsUsingMethod.Count()}.");
                //foreach (var item in NeighborhoodsUsingMethod)
                //{
                //    Console.WriteLine($"Neighborhood: {item}");
                //}
            }
        }

        static void Main(string[] args)
        {
            Filter();
        }
    }
}