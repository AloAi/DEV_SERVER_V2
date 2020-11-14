using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
    public class V_CONTACT_INFO
    {
		[Key]
		public decimal CONTACT_ID { get; set; }

		public DateTime CONTACT_DATE { get; set; }

		public decimal WORKER_ID { get; set; }

		public string WORKER_NAME { get; set; }

		public decimal HIRER_ID { get; set; }

		public string HIRER_NAME { get; set; }

		public decimal MODE_USER { get; set; }

	}
}