using AutoMapper;
using InfosAboutNba.Logic;
using InfosAboutNba.Data;
using InfosAboutNba.Repository;
using InfosAboutNba.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace InfosAboutNba.Web.Controllers
{
    public class TeamsController : Controller
    {
        ITeamLogic logic;
        IMapper mapper;
        TeamsViewModel vm;

        public TeamsController()
        {
            NBA_DatabaseEntities db = new NBA_DatabaseEntities();
            TeamRepository repo = new TeamRepository(db);
            logic = new TeamLogic(repo);

            mapper = MapperFactory.CreateMapper();

            vm = new TeamsViewModel();
            vm.EditedTeam = new Team();
            var teams = logic.GetAll();
            vm.ListOfTeams = mapper.Map<IList<Data.Teams>, List<Models.Team>>(teams);
        }

        private Team GetTeamModel(int id)
        {
            Teams oneTeam = logic.GetOne(id);

            return mapper.Map<Data.Teams, Models.Team>(oneTeam);
        }

        // GET: Teams
        public ActionResult Index()
        {
            ViewData["editAction"] = "AddNew";
            return View("TeamIndex", vm);
        }

        // GET: Teams/Details/5
        public ActionResult Details(int id)
        {
            return View("TeamDetails", GetTeamModel(id));
        }

        public ActionResult Remove(int id)
        {
            TempData["editResult"] = "Delete failed!";

            if (logic.Delete(id))
            {
                TempData["editResult"] = "Delete Ok!";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ViewData["editAction"] = "Edit";
            vm.EditedTeam = GetTeamModel(id);

            return View("TeamIndex", vm);
        }

        [HttpPost]
        public ActionResult Edit(Team team, string editAction)
        {
            if(ModelState.IsValid && team != null)
            {
                TempData["editResult"] = "Edit Ok!";
                if( editAction == "AddNew")
                {
                    logic.Add(new Teams()
                    {
                        TName = team.Name,
                        HomeTown = team.HomeTown,
                        Found = team.Found,
                        WinPercentageSinceFounded = team.WinPercentageSinceFounded,
                        WinPercentageInSeason = team.WinPercentageInSeason,
                        NumberOfChampionships = team.NumberOfChampionships
                    });
                }
                else
                {
                    bool succes = logic.ModTeam(new Teams()
                    {
                        idTeams = team.ID,
                        TName = team.Name,
                        HomeTown = team.HomeTown,
                        Found = team.Found,
                        WinPercentageSinceFounded = team.WinPercentageSinceFounded,
                        WinPercentageInSeason = team.WinPercentageInSeason,
                        NumberOfChampionships = team.NumberOfChampionships
                    });  

                    if(!succes)
                    {
                        TempData["editResult"] = "Edit Failed!";
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["editAction"] = "Edit";
                vm.EditedTeam = team;

                return View("TeamIndex", vm);
            }
        }
    }
}
