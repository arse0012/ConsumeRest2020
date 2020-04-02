using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using Newtonsoft.Json;

namespace ConsumeRest2020
{
    public class Customer
    {
        private const string URI = "http://localhost:51273/api/guest";
        public void Start()
        {
            //List<Guest> gæster = GetGuests();
            //gæster = GetGuests();
            //foreach (Guest guest in gæster)
            //{
            //    Console.WriteLine("Guest::" + guest);
            //}
            //Console.WriteLine("Hent guest no 3 :" + GetOneGuest(3));
            //Console.WriteLine("Delete guest nr 100");
            //Console.WriteLine("Guest nr 100 deleted:" + Delete( 100));
            //Guest newGuest = new Guest(101, "ZoomGuest", "ZoomStreet 123");
            //bool ok = Post(newGuest);
            //Console.WriteLine("Har oprettet nr 101 " + ok);

            Guest newOpdateretGuest = new Guest(50, "ZoomOpdateretGuest", "OldRoad 456");
            bool ok = Put(50, newOpdateretGuest);
            Console.WriteLine("Har opdateret guest nr 50 " + ok);

            List<Guest> gæster = GetGuests();
            gæster = GetGuests();
            foreach (Guest guest in gæster)
            {
                Console.WriteLine("Guest: " + guest);
            }
        }

        public List<Guest> GetGuests()
        {
            List<Guest> gæster = new List<Guest>();
            using (HttpClient client = new HttpClient())
            {
                Task<string> stringAsync = client.GetStringAsync(URI);
                string jsonString = stringAsync.Result;
                gæster = JsonConvert.DeserializeObject<List<Guest>>(jsonString);
            }

            return gæster;
        }

        public Guest GetOneGuest(int id)
        {
            Guest guest = new Guest();
            using (HttpClient client = new HttpClient())
            {
                Task<string> stringAsync = client.GetStringAsync(URI + "/" + id);
                string jsonString = stringAsync.Result;
                guest = JsonConvert.DeserializeObject<Guest>(jsonString);
            }

            return guest;
        }

        public bool Delete(int id)
        {
            bool ok= false;
            using (HttpClient client = new HttpClient())
            {
                Task<HttpResponseMessage> deleteAsync = client.DeleteAsync(URI + "/" + id);
                HttpResponseMessage resp = deleteAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    string jsonStr = resp.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonStr);
                }
                else
                {
                    ok = false;
                }
            }
            return ok;
        }
        public bool Post(Guest guest)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(guest);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                Task<HttpResponseMessage> postAsync = client.PostAsync(URI, content);
                HttpResponseMessage resp = postAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    string jsonStr = resp.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonStr);
                }
                else
                {
                    ok = false;
                }
            }

            return ok;
        }

        public bool Put(int id, Guest guest)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(guest);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                Task<HttpResponseMessage> putAsync = client.PutAsync(URI + "/" + id, content);
                HttpResponseMessage resp = putAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    string jsonStr = resp.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonStr);
                }
                else
                {
                    ok = false;
                }
            }

            return ok;
        }
    }
}
