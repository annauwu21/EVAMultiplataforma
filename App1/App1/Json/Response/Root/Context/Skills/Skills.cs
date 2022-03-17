using Newtonsoft.Json;

namespace App1
{
    public class Skills
    {
        [JsonProperty("main skill")]
        public MainSkill MainSkill { get; set; }
    }
}