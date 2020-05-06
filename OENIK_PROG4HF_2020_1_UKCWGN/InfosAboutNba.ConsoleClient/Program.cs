using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InfosAboutNba.ConsoleClient
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string HomeTown { get; set; }
        public int Found { get; set; }
        public double WinPercentageSinceFounded { get; set; }
        public double WinPercentageInSeason { get; set; }
        public int NumberOfChampionships { get; set; }

        public override string ToString()
        {
            return $"Id={ID} {HomeTown} {Name} Found={Found}\tWin % in Season={WinPercentageInSeason} Win % Since Founded={WinPercentageSinceFounded}\tNumber of Championships={NumberOfChampionships}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting...");
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Press ENTER");
            Console.ReadLine();

                bool showMenu = true;
                while (showMenu)
                {
                    showMenu = MainMenu();
                }            
        }

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) List Teams");
            Console.WriteLine("2) Add Team");
            Console.WriteLine("3) Mod Team");
            Console.WriteLine("4) Del Team");
            Console.WriteLine("5) Exit");
            Console.Write("\r\nSelect an option: ");

            string url = "http://localhost:62204/api/TeamsApi/";

            HttpClient client = new HttpClient();

            string json = client.GetStringAsync(url + "all").Result;
            var list = JsonConvert.DeserializeObject<List<Team>>(json);

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("All team:");
                    foreach (var item in list)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("\nPress ENTER");
                    Console.ReadLine();

                    return true;

                case "2":
                    Dictionary<string, string> postData;
                    string response;

                    Console.WriteLine("Name:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Hometown:");
                    string ht = Console.ReadLine();
                    Console.WriteLine("Found");
                    int found = int.Parse(Console.ReadLine());
                    Console.WriteLine("Number of Championships:");
                    int num = int.Parse(Console.ReadLine());

                    postData = new Dictionary<string, string>();
                    
                    postData.Add(nameof(Team.Name), name);
                    postData.Add(nameof(Team.HomeTown), ht);
                    postData.Add(nameof(Team.Found), found.ToString());
                    postData.Add(nameof(Team.WinPercentageInSeason), "0.325");
                    postData.Add(nameof(Team.WinPercentageSinceFounded), "0.295");
                    postData.Add(nameof(Team.NumberOfChampionships), num.ToString());

                    response = client.PostAsync(url + "add", new FormUrlEncodedContent(postData))
                        .Result.Content.ReadAsStringAsync().Result;
                    //json = client.GetStringAsync(url + "all").Result;

                    Console.WriteLine("Added: " + response);
                    Console.ReadLine();

                    return true;

                case "3":
                    Console.WriteLine("Name:");
                    string nameMod = Console.ReadLine();
                    int id = JsonConvert.DeserializeObject<List<Team>>(json).Single(x => x.Name == nameMod).ID;
                    postData = new Dictionary<string, string>();

                    Console.WriteLine("New Name:");
                    string newName = Console.ReadLine();
                    Console.WriteLine("New number of championships:");
                    int newNum = int.Parse(Console.ReadLine());
                    postData.Add(nameof(Team.ID), id.ToString());
                    postData.Add(nameof(Team.Name), newName);
                    //postData.Add(nameof(Team.HomeTown), "Albuquerque");
                    //postData.Add(nameof(Team.Found), "200");
                    //postData.Add(nameof(Team.WinPercentageInSeason), "0.325");
                    //postData.Add(nameof(Team.WinPercentageSinceFounded), "0.295");
                    postData.Add(nameof(Team.NumberOfChampionships), newNum.ToString());

                    response = client.PostAsync(url + "mod", new FormUrlEncodedContent(postData))
                        .Result.Content.ReadAsStringAsync().Result;
                    //json = client.GetStringAsync(url + "all").Result;
                    Console.WriteLine("Modded: " + response);
                    Console.ReadLine();
                    
                    return true;

                case "4":
                    Console.WriteLine("Name:");
                    string delName = Console.ReadLine();
                    int iD = JsonConvert.DeserializeObject<List<Team>>(json).Single(x => x.Name == delName).ID;
                    response = client.GetStringAsync(url + "del/" + iD).Result;
                    //json = client.GetStringAsync(url + "all").Result;
                    Console.WriteLine("DEL: " + response);
                    Console.ReadLine();

                    return true;

                case "5":
                    return false;
                default:
                    return true;
            }
        }
    }
}
