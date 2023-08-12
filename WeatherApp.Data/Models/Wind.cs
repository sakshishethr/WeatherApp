﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Data
{
    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }

        public override string ToString()
        {
            return $"speed: {speed} deg {deg}";
        }
    }
}
