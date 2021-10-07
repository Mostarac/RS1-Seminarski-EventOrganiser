using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace webapp.Models
{
    public class TopUpCard
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public int Amount { get; set; }

        public TopUp TopUp { get; set; }

    }
}
