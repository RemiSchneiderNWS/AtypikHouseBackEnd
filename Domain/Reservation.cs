﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Reservation
    {
        public int Id { get; set; }
        public User User { get; set; } = null!       
        public int AdvId { get; set; }
        public string AdvName { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime CreatedAt { get;set; }
        public int AdvPrice { get; set; }
        public Boolean Payment { get; set; }
        public string PaymentTimer {get; set; }
        public int AdvTenants { get; set; }
        public Boolean DelTenant { get; set; }
        public Boolean DelOwner { get; set; }
    }
}
