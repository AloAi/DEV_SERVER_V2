using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
    public class V_SUGGEST_JOB
    {
        [Key]
        public decimal SUGGEST_ID { get; set; }
        public decimal CATALOG_CD { get; set; }
        public string CATALOG_NAME { get; set; }
        public string CATALOG_NAME_EN { get; set; }
        public decimal TEMPLATE_CD { get; set; }
        public string TEMPLATE_TITLE { get; set; }
        public string TEMPLATE_TITLE_EN { get; set; }
        public decimal DISP_ORDER { get; set; }
    }
}