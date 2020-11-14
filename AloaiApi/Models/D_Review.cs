using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
    public class D_REVIEW
	{
		[Key]
		public decimal REVIEW_ID { get; set; }

		public decimal CONTACT_ID { get; set; }

		public decimal REVIEW_USER_ID { get; set; }

		public decimal REVIEW_MODE_USER { get; set; }

		public decimal CATALOG_CD { get; set; }
		public DateTime REVIEW_DATE { get; set; }

		public decimal SCORE { get; set; }

		public string COMMENT { get; set; }

		public DateTime? REG_DATETIME { get; set; }

		public DateTime? UPD_DATETIME { get; set; }
	}
}
