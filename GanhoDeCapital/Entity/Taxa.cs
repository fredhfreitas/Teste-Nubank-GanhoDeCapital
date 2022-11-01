using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GanhoDeCapital.Entity
{
    public class Taxa
    {
        [JsonPropertyName("tax")]
        public string Tax { get; set; }
    }
}
