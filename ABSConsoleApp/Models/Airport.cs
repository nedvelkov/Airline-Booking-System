namespace Models
{
    using Models.Contracts;
    using System;
    public class Airport:IAirport
    {
        private string name;

        public Airport(string name)
        {
            this.Name = name;
        }
        public string Name
        {
            get { return this.name; }
            init
            {
                if (string.IsNullOrEmpty(value) || value.Length != 3)
                {
                    throw new ArgumentException("Airport name must be 3 characters in length");
                }
                this.name = value;
            }
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Airport airport = (Airport)obj;
                return this.name == airport.name;
            }
        }

        public override int GetHashCode()
        {
            var hash = 0;
            foreach (var letter in this.Name)
            {
                hash += (int)letter * 4;
            }
            return hash/2;
        }

        public override string ToString()
        {
            return $"Wellcome to airport {this.Name}";
        }

    }
}
