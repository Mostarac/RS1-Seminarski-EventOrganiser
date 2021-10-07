using System;
using System.ComponentModel.DataAnnotations;

namespace webapp.Models
{
    public class Base
    {
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? UpdatedDate { get; set; }

    }
}