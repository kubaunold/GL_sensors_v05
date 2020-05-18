using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GL_sensors_v0_4.Models
{
    public class Measurement
    {
        public int Id { get; set; }             //PK
        public int sensorId { get; set; }       //corresponding sensor id
        public int pm_1_0 { get; set; }         //pm_1_0
        public int pm_2_5 { get; set; }         //pm_2_5    -       PM2.5 is particulate matter 2.5 micrometers or less in diameter.
        public int pm_10 { get; set; }          //pm_10     -       PM10 is particulate matter 10 micrometers or less in diameter
        public double temp { get; set; }        //temperature (°C)
        public double hum { get; set; }         //humidity (%)
        public DateTime time { get; set; }      //time stamp of the measurment of format ("%Y-%m-%d %H:%M:%S")
    }
}
