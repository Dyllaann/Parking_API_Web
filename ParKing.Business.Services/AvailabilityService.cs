using System;
using System.Collections.Generic;
using System.Text;
using ParKing.Data.Engine;
using ParKing.Data.Repository;
using ParKing.Utils.Configuration;

namespace ParKing.Business.Services
{
    public class AvailabilityService
    {
        private ParkingLotRepository ParkingLotRepository { get; }
        private ParkingAvailabilityRepository ParkingAvailabilityRepository { get; }
        private Config Config { get; }

        public AvailabilityService(ParkingLotRepository parkingLotRepository, ParkingAvailabilityRepository parkingAvailabilityRepository, Config config)
        {
            ParkingLotRepository = parkingLotRepository;
            ParkingAvailabilityRepository = parkingAvailabilityRepository;
            Config = config;
        }

        #region Public Methods

        public UpdateStatus UpdateSingleAvailability(UpdateAvailability update)
        {
            var currentLot = ParkingLotRepository.GetParkingLotById(update.Id);
            if (currentLot == null)
            {
                return UpdateStatus.NonExistent;
            }

            var availability = ParkingAvailabilityRepository.GetAvailabilityByLotId(currentLot.Id);
            if (availability == null)
            {
                var newAvailability = new ParkingAvailability()
                {
                    Id = Guid.NewGuid(),
                    Available = update.Availability,
                    Lot = currentLot,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.MinValue
                };
                newAvailability.Lot = currentLot;
                currentLot.Availability = newAvailability;

                ParkingLotRepository.UpdateParkingLot(currentLot);
                ParkingAvailabilityRepository.AddAvailability(newAvailability);
            }
            else
            {
                currentLot.Availability.Available = update.Availability;
                currentLot.Availability.UpdatedAt = DateTime.UtcNow;
                ParkingLotRepository.UpdateParkingLot(currentLot);
            }

            return UpdateStatus.Ok;
        }

        public bool UpdateMultipleAvailabilities(List<UpdateAvailability> updates)
        {
            var success = true;
            foreach (var update in updates)
            {
                var singleUpdateSuccess = UpdateSingleAvailability(update);
                if (singleUpdateSuccess != UpdateStatus.Ok)
                {
                    success = false;
                }
            }

            return success;
        }


        #endregion

    }
}
