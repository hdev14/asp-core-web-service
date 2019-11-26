using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_service.Models;
using web_service.ModelsView;
using web_service.Repositories;

namespace web_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamManagerController : ControllerBase
    {
        private readonly SportRepository sportRepository;
        private readonly TeamRepository teamRepository;
        private readonly AthleteRepository athleteRepository;

        private Stack<Athlete> stackOfAthletes;

        public TeamManagerController(
            SportRepository sportRepository,
            TeamRepository teamRepository,
            AthleteRepository athleteRepository)
        {
            this.sportRepository = sportRepository;
            this.teamRepository = teamRepository;
            this.athleteRepository = athleteRepository;
        }

        [HttpPost("generate-teams/{peladaId}/{sportId}")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GenerateTeams(
                        int peladaId, int sportId, List<Athlete> athletes)
        {
            int numberAthletes = athletes.Count;
            var sport = await sportRepository.FindSportAsync(sportId);

            if (sport == null || this.checkNumberOfAthletes(numberAthletes, sport.NumberPlayers))
            {
                return NotFound(new { message = "Parametros inválidos" });
            }
                

            string[] arrayOfQuantity =
                teamRepository.getArrayQuantityTeams(sport, numberAthletes);

            int quantityOfTeams = Convert.ToInt32(arrayOfQuantity[0]);

            if (arrayOfQuantity.Length > 1)
            {
                int numberAfterComma = Convert.ToInt32(arrayOfQuantity[1]);
                teamRepository.CheckReserveBank(numberAfterComma);
            }

            try
            {
                List<TeamAthletesView> teamAthletes =
                    await this.CreateTeamAndAthletes(
                        athletes, quantityOfTeams, numberAthletes, 
                                        sport.NumberPlayersTeam, peladaId);

                return StatusCode(200, new
                {
                    teamAthletes = teamAthletes,
                    listReserve = this.getListReserve()
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error =
                        String.Format(
                            "Não foi possível efetuar a operação devido -> {0}", e.Message)
                });
            }
        }

        private async Task<List<TeamAthletesView>> CreateTeamAndAthletes(
            List<Athlete> athletes, 
            int quantityOfTeams, 
            int numberAthletes, 
            int numberPlayersTeam, 
            int peladaId)
        {
            List<TeamAthletesView> teamAthletes = new List<TeamAthletesView>();
            this.stackOfAthletes = athleteRepository.CreateStackOfAthletes(athletes);

            int numberAthletesPerTeam = 
                    this.GetNumberAthletesPerTeam(
                        numberAthletes, quantityOfTeams, numberPlayersTeam);

            for (int i = 0; i < quantityOfTeams; i++)
            {
                var team = await teamRepository.CreateAndReturnTeam(new Team
                {
                    Name = String.Format("Time {0}", i),
                    PeladaId = peladaId
                });

                
                List<string> athleteNames =
                        await CreateAthletes(athletes, numberAthletesPerTeam, team.Id);

                teamAthletes.Add(new TeamAthletesView
                {
                    TeamId = team.Id,
                    TeamName = team.Name,
                    AthleteNames = athleteNames
                });
            }

            return teamAthletes;
        }

        private int GetNumberAthletesPerTeam(
            int numberAthletes, int quantityOfTeams, int numberPlayersTeam)
        {
            int numberAthletesPerTeam = numberAthletes / quantityOfTeams;

            while (numberAthletesPerTeam > numberPlayersTeam)
            {
                numberAthletesPerTeam--;
            }

            return numberAthletesPerTeam;
        }

        private async Task<List<string>> CreateAthletes(
                List<Athlete> athletes, int numberAthletes, int teamId)
        {
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
            int quantityBankReserve = stackOfAthletes.Count;
            if (teamRepository.IsReserve)
            {
                for (int h = 0; h < quantityBankReserve; h++)
                    listReserve.Add(stackOfAthletes.Pop().Name);
            }

            return listReserve;
        }

        private bool checkNumberOfAthletes(int numberAthletes, int numberPlayers)
        {
            return !(numberAthletes != 0 && numberAthletes >= numberPlayers);
        }
    }
}