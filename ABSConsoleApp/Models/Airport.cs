namespace Models
{
    using System;
    public class Airport
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
                if (value.Length != 3)
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

    }
}
