namespace LolData
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Info
    {
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Magic { get; set; }
        public int Difficulty { get; set; }
    }


}
