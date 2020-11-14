using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
	public class D_HISTORY
	{
		[Key]
		public decimal HISTORY_ID { get; set; }

		public decimal EXCHANGE_ID { get; set; }

		public decimal JOB_ID { get; set; }

		public decimal HIRER_ID { get; set; }

		public string TITLE { get; set; }

		public decimal? COST { get; set; }

		public decimal? UNIT_CD { get; set; }

		public string ADDRESS { get; set; }

		public string CONTENT { get; set; }

		public decimal? CATALOG_CD { get; set; }

		public decimal WORKER_ID { get; set; }

		public System.DateTime EXCHANGE_DATE { get; set; }

		public decimal? SEND_MODE_USER { get; set; }

		public string INTRODUCTION { get; set; }

		public string REQUEST_CONTENT { get; set; }

		public System.DateTime COMPLETE_DATE { get; set; }

		public decimal STATUS { get; set; }

		public decimal? COMPANY_ID { get; set; }

		public decimal REG_USER_ID { get; set; }

		public decimal REG_MODE_USER { get; set; }
	}
}