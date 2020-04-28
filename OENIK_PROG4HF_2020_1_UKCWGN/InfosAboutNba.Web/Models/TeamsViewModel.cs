using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfosAboutNba.Web.Models
{
    public class TeamsViewModel
    {
        public Team EditedTeam { get; set; }
        public List<Team> ListOfTeams { get; set; }
    }
}