using System;
using System.Collections.Generic;
using System.Text;
using ParKing.Data.Engine;
using ParKing.Data.Repository;
using ParKing.Utils.Configuration;
using Serilog;

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
                Log.Logger.Warning("Received update for unknown parking lot.");
                return UpdateStatus.NonExistent;
            }

            var availability = ParkingAvailabilityRepository.GetAvailabilityByLotId(currentLot.Id);
            if (availability == null)
            {
                Log.Logger.Information($"Current availability for lot {currentLot.Id} is null. Creating new one.");
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

                Log.Logger.Information($"Inserting new availability for lot {currentLot.Id}");
                ParkingLotRepository.UpdateParkingLot(currentLot);
                ParkingAvailabilityRepository.AddAvailability(newAvailability);
            }
            else
            {
                Log.Logger.Information($"Updating lot {currentLot.Id}. New availability: {currentLot.Availability.Available}");
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

        public bool AddLot(Guid id)
        {
            var lot = new ParkingLot()
            {
                Id = id,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
                HasChargingStation = false,
                PricingZone = "",
                Availability = new ParkingAvailability()
                {
                    Id = Guid.NewGuid(),
                    Available = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.MinValue
                }
            };
            ParkingAvailabilityRepository.AddLot(lot);
            return true;
        }

        public List<MapLocation> GetAvailabilities()
        {
            var lots = ParkingLotRepository.GetAllLots();
            var locations = new List<MapLocation>();
            foreach (var lot in lots)
            {
                locations.Add(new MapLocation()
                {
                    Id = lot.Id,
                    Available = lot.Availability.Available,
                    Latitude = lot.Location.Latitude,
                    Longitude = lot.Location.Longitude
                });
            }

            return locations;
        }
    }
}
