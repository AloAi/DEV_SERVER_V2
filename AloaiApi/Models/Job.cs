using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
    public class D_JOB
    {
        [Key]
        public decimal JOB_ID { get; set; }
        public decimal USER_ID { get; set; }
        public decimal SUGGEST_ID { get; set; }
        public decimal CANCEL_FLG { get; set; }
        public decimal RENEW_NUM { get; set; }

        /// <summary>
        /// Latitude.
        /// </summary>
        public string LONGITUDE { get; set; }

        /// <summary>
        /// Longitude.
        /// </summary>
        public string LATITUDE { get; set; }

        public DateTime REG_DATETIME { get; set; }
        public DateTime? UPD_DATETIME { get; set; }
    }
}