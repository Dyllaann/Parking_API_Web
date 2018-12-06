using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParKing.Data.Engine;

namespace ParKing.Data.Repository
{
    public class AvailabilityRepository
    {
        private ParKingContext Context { get; }

        public AvailabilityRepository(ParKingContext context)
        {
            Context = context;
        }

        public UpdateAvailability GetAvailabilityByLotId(Guid lotId)
        {
            return Context.Set<UpdateAvailability>().FirstOrDefault(ua => ua.Id == lotId);
        }

        public UpdateAvailability UpdateLot(UpdateAvailability lot)
        {
            var trackedLot = GetAvailabilityByLotId(lot.Id);
            if (trackedLot == null) return CreateLot(lot);

            trackedLot.Availability = lot.Availability;
            trackedLot.UpdatedAt = DateTime.UtcNow;
            
            Context.Update(trackedLot);
            Context.SaveChanges();
            
            return trackedLot;
        }

        private UpdateAvailability CreateLot(UpdateAvailability lot)
        {
            if (lot.Id == Guid.Empty)
            {
                lot.Id = Guid.NewGuid();
            }

            lot.CreatedAt = DateTime.UtcNow;
            lot.UpdatedAt = DateTime.MinValue;

            Context.Add(lot);
            Context.SaveChanges();
            return lot;
        }
    }
}
