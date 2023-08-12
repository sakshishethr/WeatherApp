using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace WeatherApp.Data
{
    public class WeatherData
    {
        public string cod { get; set; }
        public int message { get; set; }
        public int cnt { get; set; }
        public List<List> list { get; set; }
        public List<Weather> weather { get; set; }
        public City city { get; set; }
        public Wind Wind { get; set; }
        public Main main { get; set; }
        public Coords coords { get; set; }
        [Required]
        public string name { get; set; }

    }
}
