using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CowinTrackerApp.Models;
using CowinTrackerApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CowinTrackerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CowinDataController : ControllerBase
    {
        private readonly ICowinClientService _http;

        public CowinDataController(ICowinClientService http)
        {
            _http = http;
        }

        [HttpGet, Route("states")]
        public async Task<List<StateItem>> GetStatesAsync()
        {
            var result = await _http.GetStatesAsync();
            if (result != null) return result.States;
            else return new List<StateItem>();
        }

        [HttpGet, Route("states/{stateId}/districts")]
        public async Task<List<DistrictItem>> GetDistrictsByStateIdAsync(int stateId)
        {
            var result = await _http.GetDistrictsByStateIdAsync(stateId);
            if (result != null) return result.Districts;
            else return new List<DistrictItem>();
        }

        [HttpGet, Route("sessions/{districtId}/startDate/{date}")]
        public async Task<List<CenterItem>> GetDataForDistrictIdAndDate(string districtId, string date)
        {
            var result = await _http.GetSessionsByDistrictIdForStartDate(districtId, date);
            if (result != null) return result.Centers;
            else return new List<CenterItem>();
        }
     }
}