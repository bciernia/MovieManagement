using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace MovieManagement.Domain.Common
{
    public class AuditableModel
    {
        [JsonIgnore]
        public int CreatedById { get; set; }
        [JsonIgnore]
        public DateTime CreatedDateTime { get; set; }
        [JsonIgnore]
        public int? ModifiedById { get; set; }
        [JsonIgnore]
        public DateTime? ModifiedDateTime { get; set; }
    }
}
