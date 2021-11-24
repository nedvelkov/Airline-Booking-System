namespace Facade.Models
{
    using Facade.Interfaces;

    class Airport:IAirport
    {
        public string Name { get; init; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Airport airport = (Airport)obj;
                return Name == airport.Name;
            }
        }

        public override int GetHashCode()
        {
            var hash = 0;
            foreach (var letter in Name)
            {
                hash += (int)letter * 4;
            }
            return hash/2;
        }

        public override string ToString()
        {
            return $"Wellcome to airport {Name}";
        }

    }
}
