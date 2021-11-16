namespace Facade
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Models;
    public class SystemManager
    {
        private List<Airline> airlines;
        private List<Airport> airports;
        public SystemManager()
        {
            this.airlines = new List<Airline>();
            this.airports = new List<Airport>();
        }
        public void CreateAirpot(string name)
        {
            var getAirport = this.airports.FirstOrDefault(x => x.Name == name);
            if (getAirport!=null)
            {
                throw new ArgumentException("Airport with this name already exist.");
            }
            var airport = new Airport(name);
            this.airports.Add(airport);
        }

        public void CreateAirline(string name)
        {
            var getAirline = this.airlines.FirstOrDefault(x => x.Name == name);
            if (getAirline != null)
            {
               throw new ArgumentException("Airline with this name already exist.");
            }
            var airline = new Airline(name);
            this.airlines.Add(airline);

        }

        public void CreatFlight(string name,string orig,string dest, int year,int month,int day,string id)
        {

            //TODO: validate Date -> throw error if date is not valid or date is in past!!!
        }

        private void AddToList<T>(List<T>list,string name,Func<T,bool>func)
        {
            var getItem = list.FirstOrDefault(func);
            if (getItem != null)
            {
                throw new ArgumentException($"{getItem.GetType().Name} with this name already exist.");
            }
             
           // var item = new T(name);
           // list.Add(item);
        }
    }
}
