using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
    public class M_HIRER_INFO
    {
        [Key]
        public decimal USER_ID { get; set; }
        public decimal VERIFY_FLG { get; set; }
        public DateTime? VERIFY_DATE { get; set; }
        public DateTime? VERIFY_DATE_FROM { get; set; }
        public DateTime? VERIFY_DATE_TO { get; set; }
        public decimal? SCORE { get; set; }
        public decimal? LIKE_NUM { get; set; }
        public decimal STATUS { get; set; }
        public DateTime REG_DATETIME { get; set; }
        public DateTime? UPD_DATETIME { get; set; }
    }
}
