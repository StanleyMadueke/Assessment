using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Viagogo
{
    public class Event
    {
        public string Name { get; set; }
        public string City { get; set; }

    }
    public class Customer
    {
        public string Name { get; set; }
        public string City { get; set; }
    }

   

    public class Solution
    {
        static void Main(string[] args)
        {
            var events = new List<Event>{
                new Event{ Name = "Phantom of the Opera", City = "New York"},
                new Event{ Name = "Metallica", City = "Los Angeles"},
                new Event{ Name = "Metallica", City = "New York"},
                new Event{ Name = "Metallica", City = "Boston"},
                new Event{ Name = "LadyGaGa", City = "New York"},
                new Event{ Name = "LadyGaGa", City = "Boston"},
                new Event{ Name = "LadyGaGa", City = "Chicago"},
                new Event{ Name = "LadyGaGa", City = "San Francisco"},
                new Event{ Name = "LadyGaGa", City = "Washington"}
            };


            //1. find out all events that are in cities of customer
            // then add to email.
            var customer = new Customer { Name = "Mr. Fake", City = "New York" };
            var query = from result in events
                        where result.City.Equals(customer.City)
                        select result;
            // 1. TASK
            foreach (var item in query)
            {
                AddToEmail(customer, item);
            }
            /*
            *	We want you to send an email to this customer with all events in their city
            *	Just call AddToEmail(customer, event) for each event you think they should get
            */

            
             //Q2.Approach to geting the distance between customer's city and other cities on the list. 
            
            var closestevents = new SortedDictionary<int,Event>();
            foreach (var item in events)
            {
                try
                {
                  
                    //Q3.If the GetDistance method is an API call, implement caching. 
                    
                    var distance = GetOrSetCache($"{customer.City}-{item.City}", () => GetDistance(customer.City, item.City));

                    // If customers city and event city are different, add the distance(Key) and event(value) to the sorted dictionary
                    if (distance > 0 )
                    {
                        closestevents.Add(distance, item);
                    }
                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            // 2.2 Send the 5 closest city to the customer
            int NoOfEvents = 0;
            foreach (var item in closestevents)
            {
                if( NoOfEvents == 5)
                {
                    break;
                }
                AddToEmail(customer, item.Value);
                NoOfEvents++;

            }


            /*Q.5 
             * 
             * sort the events by other fields like price, by calling the GetPrice Method
             */
            var SortedEventPrice = new SortedDictionary<int, Event>();
            foreach (var item in events)
            {
                try
                {
                    //Get the price of events
                    var EventPrice = GetPrice(item);

                    SortedEventPrice.Add(EventPrice, item);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

        }


        // You do not need to know how these methods work
        static void AddToEmail(Customer c, Event e, int? price = null)
        {
            var distance = GetDistance(c.City, e.City);
            Console.Out.WriteLine($"{c.Name}: {e.Name} in {e.City}"
            + (distance > 0 ? $" ({distance} miles away)" : "")
            + (price.HasValue ? $" for ${price}" : ""));
        }


        static int GetPrice(Event e)
        {
            return (AlphebiticalDistance(e.City, "") + AlphebiticalDistance(e.Name, "")) / 10;
        }

        static int GetDistance(string fromCity, string toCity)
        {
            return AlphebiticalDistance(fromCity, toCity);
        }
        private static int AlphebiticalDistance(string s, string t)
        {
            var result = 0;
            var i = 0;

            /*
             Q4. Preventing the Get Distance method from failing. 
             */
            try
            {
                for (i = 0; i < Math.Min(s.Length, t.Length); i++)
                {
                    // Console.Out.WriteLine($"loop 1 i={i} {s.Length} {t.Length}");
                    result += Math.Abs(s[i] - t[i]);
                }
                for (; i < Math.Max(s.Length, t.Length); i++)
                {
                    // Console.Out.WriteLine($"loop 2 i={i} {s.Length} {t.Length}");
                    result += s.Length > t.Length ? s[i] : t[i];
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            return result;
        }

        private static T GetOrSetCache<T>(string key, Func<T> apiCall)
        {
            //check if cache exist with key and return value
            //or call func and save response in cache
            return default;
        }
    }
}



