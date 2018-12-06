using System;

namespace ParKing.Data.Engine
{
    public class UpdateAvailability : BaseEntity
    {
        public Guid Id { get; set; }
        public bool Availability { get; set; }
        
    }
}
