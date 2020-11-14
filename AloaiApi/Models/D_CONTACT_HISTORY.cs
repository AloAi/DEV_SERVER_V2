using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
    public class D_CONTACT
    {
		[Key]
		public decimal CONTACT_ID { get; set; }

		public decimal USER_RECIEVE_ID { get; set; }

		public decimal REG_MODE_USER { get; set; }

		public decimal CONTACT_USER_ID { get; set; }

		public decimal CATALOG_CD { get; set; }

		public DateTime CONTACT_DATE { get; set; }
	}
}