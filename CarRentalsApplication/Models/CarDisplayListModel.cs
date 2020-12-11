using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRentalsApplication.Models
{
    public class CarDisplayListModel
    {
        public int CarID_PK { get; set; }
        public string CarName { get; set; }
        public string CarNumber { get; set; }
        public Nullable<int> HourlyRate { get; set; }
    }
}