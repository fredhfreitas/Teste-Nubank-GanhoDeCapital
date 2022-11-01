using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GanhoDeCapital.Model
{
    public class Taxa
    {
        [JsonPropertyName("tax")]
        public string Tax { get; set; }
    }
}
