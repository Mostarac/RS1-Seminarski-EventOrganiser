using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class GearImage
    {

        public int Id { get; set; }

        public string ImageLink { get; set; }

        public float ContainerW { get; set; }

        public float ContainerH { get; set; }

        public float TranslateX { get; set; }

        public float TranslateY { get; set; }

        public float ScaleX { get; set; }

        public float ScaleY { get; set; }

        public float SkewX { get; set; }

        public float SkewY { get; set; }

        public int GearId { get; set; }

        [ForeignKey("GearId")]
        public Gear Gear { get; set; }

    }
}
