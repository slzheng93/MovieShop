﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PurchaseDetailsResponseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid PurchaseNumber { get; set; }
        public Decimal TotalPrice { get; set; }
        public DateTime PurchaseDateTime { get; set; }
    }
}
