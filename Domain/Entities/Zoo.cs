namespace Domain.Entities
{
    public class Zoo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int LocationId { get; set; }
        public Location? Location { get; set; }

        public IEnumerable<Animal>? Animals { get; set; }
    }
}
