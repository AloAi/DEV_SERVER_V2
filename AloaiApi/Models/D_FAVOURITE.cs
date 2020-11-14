using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
    public class D_FAVOURITE
    {
		[Key]
		public decimal FAVOURITE_ID { get; set; }

		public decimal USER_ID { get; set; }

		public decimal MODE_USER { get; set; }

		public decimal FAVOURITE_USER_ID { get; set; }

		public decimal CATALOG_CD { get; set; }

		public DateTime? REG_DATETIME { get; set; }
	}
}