using AutoMapper;
using InfosAboutNba.Data;
using InfosAboutNba.Logic;
using InfosAboutNba.Repository;
using InfosAboutNba.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InfosAboutNba.Web.Controllers
{
    public class TeamsApiController : ApiController
    {
        public class ApiResult
        {
            public bool OperationResult { get; set; }
        }

        ITeamLogic logic;
        IMapper mapper;

        public TeamsApiController()
        {
            NBA_DatabaseEntities db = new NBA_DatabaseEntities();
            TeamRepository repo = new TeamRepository(db);
            logic = new TeamLogic(repo);

            mapper = MapperFactory.CreateMapper();
        }

        // GET api/TeamsApi/all
        [ActionName("all")]
        [HttpGet]
        public IEnumerable<Models.Team> GetAll()
        {
            var teams = logic.GetAll();
            return mapper.Map<IList<Data.Teams>, List<Models.Team>>(teams);
        }

        // GET api/TeamsApi/del/id
        [ActionName("del")]
        [HttpGet]
        public ApiResult DelOneTeam(int id)
        {
            bool succes = logic.Delete(id);
            return new ApiResult() { OperationResult = succes };
        }

        // GET api/TeamsApi/add
        [ActionName("add")]
        [HttpPost]
        public ApiResult AddOneTeam(Team team)
        {
            logic.Add(new Teams()
            {
                TName = team.Name,
                HomeTown = team.HomeTown,
                idTeams = team.ID,
                Found = team.Found,
                WinPercentageInSeason = team.WinPercentageInSeason,
                WinPercentageSinceFounded = team.WinPercentageSinceFounded,
                NumberOfChampionships = team.NumberOfChampionships
            });

            return new ApiResult() { OperationResult = true };
        }

        // GET api/TeamsApi/mod
        [ActionName("mod")]
        [HttpPost]
        public ApiResult ModOneTeam(Team team)
        {
            bool succes = logic.ModTeam(new Teams()
            {
                TName = team.Name,
                HomeTown = team.HomeTown,
                idTeams = team.ID,
                Found = team.Found,
                WinPercentageInSeason = team.WinPercentageInSeason,
                WinPercentageSinceFounded = team.WinPercentageSinceFounded,
                NumberOfChampionships = team.NumberOfChampionships
            });

            return new ApiResult() { OperationResult = succes };
        }

    }
}
