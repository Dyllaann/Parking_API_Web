using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParKing.Data.Engine
{
    public class ParkingAvailability : BaseEntity
    {
        [Key]
        public Guid Id { get;set; }
        public bool Available { get; set; }

        public ParkingLot Lot { get; set; }
    }
}
