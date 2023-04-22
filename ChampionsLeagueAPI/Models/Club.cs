using System;
namespace ChampionsLeagueAPI.Models
{
    public class Club
    {
        public int ClubId { get; set; }

        public string ClubName { get; set; }

        public string CoachName { get; set; }

        public string Location { get; set; }

        public string TeamStatus { get; set; }
    }
}
