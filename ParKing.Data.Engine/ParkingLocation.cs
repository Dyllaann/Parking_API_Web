using System;
using System.ComponentModel.DataAnnotations;

namespace ParKing.Data.Engine
{
    public class ParkingLocation
    {
        [Key]
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        //EF Relations
        public ParkingLot Lot { get;set; }
    }
}
