namespace GoGolf.Api.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        /// <summary>Indicative rate: 1 player, 60 minutes, 1 bay. Superseded by the rate table.</summary>
        public decimal FromPrice { get; set; }
        public decimal Rating { get; set; }
        public ICollection<Bay> Bays { get; set; } = new List<Bay>();
    }
}