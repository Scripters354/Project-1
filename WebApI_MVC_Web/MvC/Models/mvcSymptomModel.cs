using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvC.Models
{
    public class mvcSymptomModel
    {
        public int Symptom_Sign_ID { get; set; }
        public string Symptom_Name { get; set; }
        public string Symptom_Description { get; set; }
        public string Sign_Name { get; set; }
        public string Sign_Description { get; set; }
    }
}