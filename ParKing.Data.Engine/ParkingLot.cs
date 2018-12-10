using System;
using System.ComponentModel.DataAnnotations;

namespace ParKing.Data.Engine
{
    public class ParkingLot : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string PricingZone { get; set; }
        public bool HasChargingStation { get; set; }
        

        //EF Relations
        public ParkingLocation Location { get; set; }
        
        public Guid AvailabilityId { get; set; }
        public ParkingAvailability Availability { get; set; }
    }
}
