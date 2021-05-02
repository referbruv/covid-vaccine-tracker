using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CowinTrackerApp.Models
{
    public class BaseResponse
    {
        [JsonPropertyName("ttl")]
        public int Ttl { get; set; }
    }

    public class StatesListResponse : BaseResponse
    {
        [JsonPropertyName("states")]
        public List<StateItem> States { get; set; }
    }

    public class DistrictsListResponse : BaseResponse
    {
        [JsonPropertyName("districts")]
        public List<DistrictItem> Districts { get; set; }
    }

    public class StateItem
    {
        [JsonPropertyName("state_id")]
        public int StateId { get; set; }

        [JsonPropertyName("state_name")]
        public string StateName { get; set; }
    }

    public class DistrictItem
    {
        [JsonPropertyName("district_id")]
        public int DistrictId { get; set; }

        [JsonPropertyName("district_name")]
        public string DistrictName { get; set; }
    }

    public class DistrictWiseSessionsResponse
    {
        [JsonPropertyName("centers")]
        public List<CenterItem> Centers { get; set; }
    }

    public class CenterItem
    {
        [JsonPropertyName("center_id")]
        public int CenterId { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("state_name")]
        public string State { get; set; }
        
        [JsonPropertyName("district_name")]
        public string District { get; set; }
        
        [JsonPropertyName("block_name")]
        public string Block { get; set; }
        
        [JsonPropertyName("pincode")]
        public int Pincode { get; set; }
        
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }
        
        [JsonPropertyName("long")]
        public double Longitude { get; set; }
        
        [JsonPropertyName("from")]
        public string StartTime { get; set; }
        
        [JsonPropertyName("to")]
        public string EndTime { get; set; }
        
        [JsonPropertyName("fee_type")]
        public string FeeType { get; set; }
        
        [JsonPropertyName("vaccine_fees")]
        public List<VaccineFee> VaccineFees { get; set; }
        
        [JsonPropertyName("sessions")]
        public List<Session> Sessions { get; set; }
    }

    public class Session
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
        
        [JsonPropertyName("date")]
        public string Date { get; set; }
        
        [JsonPropertyName("available_capacity")]
        public double AvailableCapacity { get; set; }
        
        [JsonPropertyName("min_age_limit")]
        public int MinAgeLimit { get; set; }
        
        [JsonPropertyName("vaccine")]
        public string Vaccine { get; set; }
        
        [JsonPropertyName("slots")]
        public List<string> Slots { get; set; }
    }

    public class VaccineFee
    {
        [JsonPropertyName("vaccine")]
        public string Vaccine { get; set; }

        [JsonPropertyName("fee")]
        public string Fee { get; set; }
    }
}