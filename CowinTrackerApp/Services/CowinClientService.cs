using CowinTrackerApp.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CowinTrackerApp.Services
{
    public interface ICowinClientService
    {
        Task<StatesListResponse> GetStatesAsync();
        Task<DistrictsListResponse> GetDistrictsByStateIdAsync(int stateId);
        Task<DistrictWiseSessionsResponse> GetSessionsByDistrictIdForStartDate(string districtId, string date);
    }

    public class CachedCowinClientService : ICowinClientService
    {
        private readonly IMemoryCache _cache;
        private readonly ICowinClientService _http;

        private const string _cowinStatesListKey = "COWIN_STATES";
        private const string _cowinDistrictsListKey = "COWIN_DISTRICTS";

        public CachedCowinClientService(IMemoryCache cache, ICowinClientService http)
        {
            _cache = cache;
            _http = http;
        }

        public async Task<DistrictsListResponse> GetDistrictsByStateIdAsync(int stateId)
        {
            string key = $"{_cowinDistrictsListKey}_{stateId}";

            DistrictsListResponse data;

            if (!_cache.TryGetValue(key, out data))
            {
                var response = await _http.GetDistrictsByStateIdAsync(stateId);

                if (response != null)
                {
                    _cache.Set(key, response, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(response.Ttl == 0 ? 24 : response.Ttl)
                    });

                    data = response;
                }
            }

            return data;
        }

        public async Task<StatesListResponse> GetStatesAsync()
        {
            string key = _cowinStatesListKey;

            StatesListResponse data;

            if (!_cache.TryGetValue(key, out data))
            {
                var response = await _http.GetStatesAsync();

                if (response != null)
                {
                    _cache.Set(key, response, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(response.Ttl == 0 ? 24 : response.Ttl)
                    });

                    data = response;
                }
            }

            return data;
        }

        public async Task<DistrictWiseSessionsResponse> GetSessionsByDistrictIdForStartDate(string districtId, string date)
        {
            return await _http.GetSessionsByDistrictIdForStartDate(districtId, date);
        }
    }

    public class CowinClientService : ICowinClientService
    {
        private readonly string _cowinApi = "https://cdn-api.co-vin.in/api";
        private readonly HttpClient _client;

        public CowinClientService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("myClient");
        }

        public async Task<StatesListResponse> GetStatesAsync()
        {
            var api = $"{_cowinApi}/v2/admin/location/states";
            return await GetAsync<StatesListResponse>(api);
        }

        public async Task<DistrictsListResponse> GetDistrictsByStateIdAsync(int stateId)
        {
            var api = $"{_cowinApi}/v2/admin/location/districts/{stateId}";
            return await GetAsync<DistrictsListResponse>(api);
        }

        private async Task<T> GetAsync<T>(string api)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, api);
            request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
            request.Headers.TryAddWithoutValidation("Accept", "en-IN");

            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(content);
            }

            return default;
        }

        public async Task<DistrictWiseSessionsResponse> GetSessionsByDistrictIdForStartDate(string districtId, string date)
        {
            var api = $"{_cowinApi}/v2/appointment/sessions/public/calendarByDistrict?district_id={districtId}&date={date}";
            return await GetAsync<DistrictWiseSessionsResponse>(api);
        }
    }
}
