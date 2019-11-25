using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_service.Models;
using web_service.ModelsView;
using web_service.Repositories;

namespace web_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        private readonly SportRepository sportRepository;
        private readonly TeamRepository teamRepository;
        private readonly AthleteRepository athleteRepository;

        private Stack<Athlete> stackOfAthletes;

        public MainController(
            SportRepository sportRepository,
            TeamRepository teamRepository,
            AthleteRepository athleteRepository)
        {
            this.sportRepository = sportRepository;
            this.teamRepository = teamRepository;
            this.athleteRepository = athleteRepository;
        }

        [HttpPost("generate-teams/{peladaId}/{sportId}")]
        public async Task<ActionResult<dynamic>> GenerateTeams(
                                            int peladaId, int sportId, List<Athlete> athletes)
        {
            var sport = await sportRepository.FindSportAsync(sportId);
            if (sport == null)
                return NotFound(new { message = "Esporte não encotrado !" });

            int numberAthletes = athletes.Count;

            string[] arrayOfQuantity =
                sportRepository.getArrayQuantityTeams(sport, numberAthletes);

            int quantityOfTeams = Convert.ToInt32(arrayOfQuantity[0]);
            int numberAfterComma = Convert.ToInt32(arrayOfQuantity[1]);

            sportRepository.CheckReserve(numberAfterComma);

            try
            {
                List<TeamAthletesView> teamAthletes = await this.CreateTeamAndAthletes(
                                            athletes, quantityOfTeams, numberAthletes, peladaId);

                return StatusCode(200, new
                {
                    teamAthletes = teamAthletes,
                    listReserve = this.getListReserve()
                });

            }catch (Exception e)
            {
                return BadRequest(new {
                    error = 
                        String.Format("Não foi possível efetuar a operação devido -> {0}", e.Message)
                });
            }
        }

        private async Task<List<TeamAthletesView>> CreateTeamAndAthletes(
                List<Athlete> athletes, int quantityOfTeams, int numberAthletes, int peladaId)
        {
            List<TeamAthletesView> teamAthletes = new List<TeamAthletesView>();

            for (int i = 0; i < quantityOfTeams; i++)
            {
                var team = await teamRepository.CreateAndReturnTeam(new Team
                {
                    Name = String.Format("Time {0}", i),
                    PeladaId = peladaId
                });

                List<string> athleteNames = await CreateAthletes(athletes, numberAthletes, team.Id);

                teamAthletes.Add(new TeamAthletesView
                {
                    TeamId = team.Id,
                    TeamName = team.Name,
                    AthleteNames = athleteNames
                });
            }

            return teamAthletes;
        }

        private async Task<List<string>> CreateAthletes(
                List<Athlete> athletes, int numberAthletes, int teamId)
        {
            this.stackOfAthletes = athleteRepository.CreateStackOfAthletes(athletes);
            List<string> athleteNames = new List<string>();

            for (int j = 0; j < numberAthletes; j++)
            {
                var athlete = stackOfAthletes.Pop();
                athlete.TeamId = teamId;
                await athleteRepository.CreateAthleteAsync(athlete);
                athleteNames.Add(athlete.Name);
            }

            return athleteNames;
        }

        private List<string> getListReserve()
        {
            List<string> listReserve = new List<string>();

            if (sportRepository.IsReserve)
            {
                for (int h = 0; h < stackOfAthletes.Count; h++)
                    listReserve.Add(stackOfAthletes.Pop().Name);
            }

            return listReserve;
        }
    }
}