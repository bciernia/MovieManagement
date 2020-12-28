using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace MovieManagement.Domain.Common
{
    public class BaseEntity : AuditableModel
    {
        [JsonProperty("ID")]
        public int Id { get; set; }
    }
}
