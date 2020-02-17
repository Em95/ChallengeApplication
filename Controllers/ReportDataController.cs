using ChallengeApplication.Models.DTO;
using ChallengeApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChallengeApplication.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Produces("Application/json")]
    [Consumes("Application/json")]
    public class ReportDataController : ControllerBase
    {
        private readonly IDataManipulationService _dataManipulationService;

        public ReportDataController(IDataManipulationService dataManipulationService)
        {
            _dataManipulationService = dataManipulationService;
        }

        /// <summary>
        /// Aggregates retrieved data by week and 
        /// shows how many impressions were recorded each week of the year.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ImpressionsDTO), 200)]
        [ProducesResponseType(400)] //Error declaration missing
        [ProducesResponseType(401)]
        public async Task<ActionResult> Retrieve()
        {
            var response = await _dataManipulationService.AggregateImpressionsDataAsync();
            return Ok(response);
        }

        /// <summary>
        /// Finds data anomalies where bidrequests increased or 
        /// decreased 3 or more times compared to the previous day.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(AnomaliesDatesDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Analyse()
        {
            var response = await _dataManipulationService.FindAnomaliesDatesAsync();
            return Ok(response);
        }
    }
}
