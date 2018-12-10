using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParKing.Data.Engine
{
    public class ParkingLocation : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        //EF Relations
        [ForeignKey("ParkingLot")]
        public Guid ParkingLotId { get; set; }
        public ParkingLot Lot { get;set; }
    }
}
