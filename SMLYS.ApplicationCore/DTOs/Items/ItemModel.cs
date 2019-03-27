using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.Items
{
     public class ItemModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }

        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}
