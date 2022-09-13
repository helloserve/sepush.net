using helloserve.SePush.Converters;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace helloserve.SePush.Models
{
    public class AreaInformation
    {
        [JsonPropertyName("events")]
        public List<Event> Events { get; set; }

        [JsonPropertyName("info")]
        public Info Info { get; set; }

        [JsonPropertyName("schedule")]
        public Schedule Schedule { get; set; }
    }

    public class Event
    {
        [JsonPropertyName("note")]
        public string Note { get; set; }

        [JsonPropertyName("start")]
        public DateTime Start { get; set; }

        [JsonPropertyName("end")]
        public DateTime End { get; set; }
    }

    public class Info
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }
    }

    public class Schedule
    {
        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("days")]
        public List<ScheduleDay> Days { get; set; }
    }

    public class ScheduleDay
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("stages")]        
        public List<ScheduleDaySlots> Stages { get; set; }
    }

    [JsonConverter(typeof(TimeStringSlotConverter))]
    public class ScheduleDaySlots : List<Slot>
    {

    }

    /// <summary>
    /// This is a custom interpretation of the SePush "start-end" string value. Add these TimeSpan values to the Date of the ScheduleDay object.
    /// </summary>
    public class Slot
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public TimeSpan Duration => End - Start;
    }
}
