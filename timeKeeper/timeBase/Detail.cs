
namespace timeBase
{
    public class Detail
    {
        public int Id { get; set; }
        public double Time { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }

        public virtual Project Project { get; set; }
        public virtual Day Day { get; set; }
    }
}
