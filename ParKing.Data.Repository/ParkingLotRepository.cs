﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParKing.Data.Engine;

namespace ParKing.Data.Repository
{
    public class ParkingLotRepository
    {
        private ParKingContext Context { get; }

        public ParkingLotRepository(ParKingContext context)
        {
            Context = context;
        }

        public ParkingLot GetParkingLotById(Guid lotId)
        {
            return Context.ParkingLots.FirstOrDefault(p => p.Id == lotId);
        }

        public bool UpdateParkingLot(ParkingLot lot)
        {
            Context.ParkingLots.Update(lot);
            Context.SaveChanges();
            return true;
        }
    }
}
