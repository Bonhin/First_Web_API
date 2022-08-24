using System;
using System.Text.Json.Serialization;

namespace Final_Project.Models
{
    public class BoardGameProperties
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonPropertyName("Year_Published")]
        public int? YearPublished { get; set; }

        [JsonPropertyName("Min_Players")]
        public int? MinPlayers { get; set; }

        [JsonPropertyName("Max_Players")]
        public int? MaxPlayers { get; set; }

        [JsonPropertyName("Play_Time")]
        public int? PlayTime { get; set; }

        [JsonPropertyName("Min_Age")]
        public int? MinAge { get; set; }

        [JsonPropertyName("Users_Rated")]
        public int? UsersRated { get; set; }

        [JsonPropertyName("Rating_Average")]
        public double? RatingAverage { get; set; }

        [JsonPropertyName("BGG_Rank")]
        public int? BggRank { get; set; }

        [JsonPropertyName("Complexty_Average")]
        public double? ComplexityAverage { get; set; }

        [JsonPropertyName("Owned_Users")]
        public int? OwnedUsers { get; set; }

        public string Mechanics { get; set; }

        public string Domains { get; set; }
    }
}
