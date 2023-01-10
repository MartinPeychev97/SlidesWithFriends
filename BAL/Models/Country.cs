namespace BAL.Models
{
    public abstract class Country
    {
        public string Name { get; set; }
    }

    public class CountryWithFlag : Country
    {
        public string Flag { get; set; }
    }

    public class CountryWithCapital : Country
    {
        public string Capital { get; set; }
    }
}
