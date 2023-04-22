using System;
namespace ChampionsLeagueAPI.Models
{
    public class Player
    {
        public int PlayerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int JerseyNumber { get; set; }

        public string Position { get; set; }

        public int Goals { get; set; }

        public int ClubId { get; set; }


    }
}
