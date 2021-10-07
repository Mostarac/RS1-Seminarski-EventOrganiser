using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.ViewModels
{
    public class RentGearIndexVM
    {
        public string Mode { get; set; }
        public List<Row> Rows { get; set; }

        public class Row
        {
            public int? EventGearId { get; set; }
            public int GearId { get; set; }
            public int GearTypeId { get; set; }           
            public string GearName { get; set; }
            public double Price { get; set; }
            public int EventId { get; set; }
        }
    }
}
