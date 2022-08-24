using Final_Project.Database;
using Final_Project.Dtos;
using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    [ApiController]
    [Route("BoardGameStatusSearch")]
    public class BoardGameStatusControllers : ControllerBase
    {

        private readonly ILogger<BoardGameStatusControllers> _logger;
        private static List<BoardGameProperties> boardGameList;


        public BoardGameStatusControllers(ILogger<BoardGameStatusControllers> logger)
        {
            _logger = logger;
        }

        [HttpPost("addBoardGame")]

        public async Task<BoardGameProperties> CreateBG([FromBody] CreateBoardGame request)
        {
            boardGameList = DatabaseMethods.GetAllBoardGames().Result;

            var newBoardGame = new BoardGameProperties
            {
                Name = request.Name,
                YearPublished = request.YearPublished,
                MinPlayers = request.MinPlayers,
                MaxPlayers = request.MaxPlayers,
                PlayTime = request.PlayTime,
                MinAge = request.MinAge,
                UsersRated = request.UsersRated,
                RatingAverage = request.RatingAverage,
                BggRank = request.BggRank,
                ComplexityAverage = request.ComplexityAverage,
                OwnedUsers = request.OwnedUsers,
                Mechanics = request.Mechanics,
                Domains = request.Domains

            };

            return DatabaseMethods.InsertBoardGame(newBoardGame).Result;
        }

        [HttpGet("name")]
        public async Task<ActionResult<BoardGameProperties>> Get([FromQuery] string name)
        {
            boardGameList = DatabaseMethods.GetAllBoardGames().Result;

            if (!string.IsNullOrWhiteSpace(name))
            {
                var searchedBoardGame = boardGameList
                    .Where(x => x.Name == name)
                    .FirstOrDefault();

                if (searchedBoardGame == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(searchedBoardGame);
                }

            }
            return NotFound();

        }

        [HttpPut("updateBoardGame")]
        public async Task<ActionResult<BoardGameProperties>> Update([FromBody] CreateBoardGame request)
        {
            boardGameList = DatabaseMethods.GetAllBoardGames().Result;

            var updateBoardGame = new BoardGameProperties
            {
                Name = request.Name,
                YearPublished = request.YearPublished,
                MinPlayers = request.MinPlayers,
                MaxPlayers = request.MaxPlayers,
                PlayTime = request.PlayTime,
                MinAge = request.MinAge,
                UsersRated = request.UsersRated,
                RatingAverage = request.RatingAverage,
                BggRank = request.BggRank,
                ComplexityAverage = request.ComplexityAverage,
                OwnedUsers = request.OwnedUsers,
                Mechanics = request.Mechanics,
                Domains = request.Domains

            };

            return DatabaseMethods.UpdateBoardGame(updateBoardGame).Result;
        }

        [HttpDelete("deleteBoardGame")]
        public async Task<ActionResult<BoardGameProperties>> ExcludeBoardGame([FromQuery] string name)
        {
            boardGameList = DatabaseMethods.GetAllBoardGames().Result;

                var searchedBoardGame = boardGameList
                    .Where(x => x.Name == name)
                    .FirstOrDefault();

            if (searchedBoardGame == null)
            {
                return NotFound();
            }
            else
            {
                return DatabaseMethods.DeleteBoardGame(searchedBoardGame).Result;
            }
            
        }

        [HttpGet("filterByYears")]
        public async Task<ActionResult<BoardGameProperties>> Get([FromQuery] int year, [FromQuery] int page = 1)
        {
            boardGameList = DatabaseMethods.GetAllBoardGames().Result;

            var searchedBoardGame = boardGameList
                .Where(x => x.YearPublished == year)
                .Skip((page - 1) * 10)
                .Take(10)
                .OrderBy(x => x.Name).ToList();

            return Ok(searchedBoardGame);
        }
    }

}