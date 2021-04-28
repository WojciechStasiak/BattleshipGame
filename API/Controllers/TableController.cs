using Microsoft.AspNetCore.Mvc;
using API.Entities;
using System.Collections.Generic;
using System;
using API._Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class TableController : Controller
    {
        private GameService gameService = new GameService();

        [HttpGet]
        [Route("tables/table1")]
        public ActionResult<ListOfCells> GetTable1()
        {
            return gameService.setUpNewGameForLeftPlayer();
        }

        [HttpGet]
        [Route("tables/table2")]
        public ActionResult<ListOfCells> GetTable2()
        {
            return gameService.setUpNewGameForRightPlayer();
        }
        [HttpGet]
        [Route("tables/shotAtLeftPlayer")]
        public ActionResult<ListOfCells> shotAtLeftPlayer()
        {
            return gameService.oneShotLeftPlayer();
        }

        [HttpGet]
        [Route("tables/shotAtRightPlayer")]
        public ActionResult<ListOfCells> shotAtRightPlayer()
        {
            return gameService.oneShotRightPlayer();
        }

        [HttpGet]
        [Route("tables/giveWinner")]
        public ActionResult<List<int>> giveWinner()
        {
            return gameService.getWinnerOfTheGame();
        }

    }
}