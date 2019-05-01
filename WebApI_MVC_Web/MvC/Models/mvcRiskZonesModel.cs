using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvC.Models
{
    public class mvcRiskZonesModel
    {
        public int Malaria_Risk_Zone_ID { get; set; }
        public string Risk_Zone_Location { get; set; }
        public string Risk_Zone { get; set; }
        public string Severity { get; set; }
    }
}