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
        private AvailabilityRepository AvailabilityRepository { get; }
        private Config Config { get; }

        public AvailabilityService(AvailabilityRepository availabilityRepository, Config config)
        {
            AvailabilityRepository = availabilityRepository;
            Config = config;
        }

        #region Public Methods

        public bool UpdateSingleAvailability(UpdateAvailability update)
        {
            return true;
        }

        public bool UpdateMultipleAvailabilities(List<UpdateAvailability> updates)
        {
            var success = true;
            foreach (var update in updates)
            {
                var singleUpdateSuccess = UpdateSingleAvailability(update);
                if (!singleUpdateSuccess)
                {
                    success = false;
                }
            }

            return success;
        }
        

        #endregion

    }
}
