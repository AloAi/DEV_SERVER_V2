using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
	public class V_CONTACT_HISTORY
	{
		[Key]
		public decimal CONTACT_ID { get; set; }

		public decimal USER_RECIEVE_ID { get; set; }

		public decimal REG_MODE_USER { get; set; }

		public decimal CONTACT_USER_ID { get; set; }

		public decimal CATALOG_CD { get; set; }

		public System.DateTime CONTACT_DATE { get; set; }

		public string RECIEVE_USER_NAME { get; set; }

		public string CONTACT_USER_NAME { get; set; }
	}
}