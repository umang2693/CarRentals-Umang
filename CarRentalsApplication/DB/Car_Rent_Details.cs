//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRentalsApplication.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Car_Rent_Details
    {
        public int RentID_PK { get; set; }
        public Nullable<int> CarID_FK { get; set; }
        public string CustomerName { get; set; }
        public string CarNumber { get; set; }
        public Nullable<System.DateTime> CarTakenDate { get; set; }
        public Nullable<System.DateTime> CarReturnDate { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> RentedTime { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
