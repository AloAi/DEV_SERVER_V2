using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
    public class T_PARTNER_CATALOG_UNIT
    {
        [Key]
        public decimal USER_ID { get; set; }

        [Key]
        public decimal? CATALOG_CD { get; set; }

        [Key]
        public decimal? UNIT_CD { get; set; }

        public decimal? COST { get; set; }

        public DateTime? REG_DATETIME { get; set; }

        public DateTime? UPD_DATETIME { get; set; }
    }
}
