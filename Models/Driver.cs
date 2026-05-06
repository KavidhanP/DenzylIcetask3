namespace LogiTech.Models
{
    public class Driver
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int OnTimeRate { get; set; }
        public double Rating { get; set; }
        public int Deliveries { get; set; }
        public double FuelEfficiency { get; set; }
        public int EfficiencyScore { get; set; }
    }
}