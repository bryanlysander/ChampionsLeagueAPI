using System;

namespace ChampionsLeagueAPI.Models
{
    public class Response<T>
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public List<T> Items { get; set; } = new List<T>();

    }
}
