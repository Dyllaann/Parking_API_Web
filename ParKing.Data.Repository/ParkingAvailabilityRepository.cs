using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParKing.Data.Engine;

namespace ParKing.Data.Repository
{
    public class ParkingAvailabilityRepository
    {
        private ParKingContext Context { get; }

        public ParkingAvailabilityRepository(ParKingContext context)
        {
            Context = context;
        }

        public ParkingAvailability GetAvailabilityByLotId(Guid lotId)
        {
            return Context.ParkingAvailabilities.FirstOrDefault(pa => pa.Lot.Id == lotId);
        }

        public bool AddAvailability(ParkingAvailability availability)
        {
            Context.ParkingAvailabilities.Add(availability);
            Context.SaveChanges();
            return true;
        }

        public bool UpdateAvailability(ParkingAvailability availability)
        {
            Context.ParkingAvailabilities.Update(availability);
            Context.SaveChanges();
            return true;
        }
    }
}
