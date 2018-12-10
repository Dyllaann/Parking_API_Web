using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ParKing.Data.Engine
{
    public class BaseEntity
    {
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get;set; }
    }
}
