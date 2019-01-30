using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminLayer.Models
{
    public class IncomingCallModel
    {
        public string phoneNumber { get; set; }
        public int? customerId { get; set; }
        public string customerName { get; set; }
        public string customerAddress { get; set; }
        public string customerOrder { get; set; }
        public string customerAddressDesc { get; set; }
        public string lastCallDate { get; set; }
        public List<CourierType> courierList { get; set; }
        public int courier { get; set; }
    }


    public class CourierType
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }



}