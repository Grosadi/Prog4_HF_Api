using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InfosAboutNba.Wpf
{
    class MainLogic
    {
        string url = "http://localhost:62204/api/TeamsApi/";
        HttpClient client = new HttpClient();

        void SendMessage(bool succes)
        {
            string msg = succes ? "Operation completed!" : "Operation failed";

            Messenger.Default.Send(msg, "TeamResult");
        }

        public List<TeamVM> ApiGetTeams()
        {
            string json = client.GetStringAsync(url + "all").Result;
            var list = JsonConvert.DeserializeObject<List<TeamVM>>(json);
            //SendMessage(true);
            return list;
        }

        public void ApiDelTeam(TeamVM team)
        {
            bool succes = false;
            if(team != null)
            {
                string json = client.GetStringAsync(url + "del/" + team.ID).Result;
                JObject obj = JObject.Parse(json);
                succes = (bool)obj["OperationResult"];
            }
            SendMessage(succes);
        }

        bool ApiEditTeam(TeamVM team, bool isEditing)
        {
            if (team == null) return false;
            string myurl = isEditing ? url + "mod" : url + "add";
            Dictionary<string, string> postData = new Dictionary<string, string>();
            if(isEditing)
            {
                postData.Add(nameof(TeamVM.ID), team.ID.ToString());
            }
            postData.Add(nameof(TeamVM.Name), team.Name);
            postData.Add(nameof(TeamVM.HomeTown), team.HomeTown);
            postData.Add(nameof(TeamVM.Found), team.Found.ToString());
            postData.Add(nameof(TeamVM.WinPercentageInSeason), team.WinPercentageInSeason.ToString());
            postData.Add(nameof(TeamVM.WinPercentageSinceFounded), team.WinPercentageSinceFounded.ToString());
            postData.Add(nameof(TeamVM.NumberOfChampionships), team.NumberOfChampionships.ToString());

            string json = client.PostAsync(myurl, new FormUrlEncodedContent(postData))
                .Result.Content.ReadAsStringAsync().Result;
            JObject obj = JObject.Parse(json);

            return (bool)obj["OperationResult"];
        }

        public void EditTeam(TeamVM team, Func<TeamVM,bool> editor)
        {
            TeamVM clone = new TeamVM();
            if (team != null) clone.CopyFrom(team);
            bool? succes = editor?.Invoke(clone);
            if(succes == true)
            {
                if (team != null) succes = ApiEditTeam(clone, true);
                else  succes = ApiEditTeam(clone, false);
            }

            SendMessage(succes == true);
        }
    }
}
