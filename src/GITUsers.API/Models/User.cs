using System.Text.Json.Serialization;

namespace GITUsers.API.Models
{
    public class User
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("company")]
        public string Company { get; set; }
        [JsonPropertyName("followers")]
        public int Followers { get; set; }
        [JsonPropertyName("public_repos")]
        public int PublicRepos { get; set; }
        [JsonPropertyName("average_followers")]
        public double AverageFollowers { get => PublicRepos > 0 ? (Followers / PublicRepos) : 0; }
    }
}
