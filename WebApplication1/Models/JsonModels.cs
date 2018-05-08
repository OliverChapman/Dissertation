using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class LocationsClass
    {
        public ICollection<Location> Locations { get; set; }
    }
    public class Location
    {
        public string LocationName { get; set; }
        public ICollection<Seat> Seats { get; set; }
    }

    public class Seat
    {
        public string Ip { get; set; }
        public string SeatNo { get; set; }
    }
}
