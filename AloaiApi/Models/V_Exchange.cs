using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
    public class V_EXCHANGE
    {
		[Key]
		public decimal JOB_ID { get; set; }

		public decimal? EXCHANGE_ID { get; set; }

		public string TITLE { get; set; }

		public decimal? COST { get; set; }

		public decimal? UNIT_CD { get; set; }

		public string UNIT_NAME { get; set; }

		public decimal? CATALOG_CD { get; set; }

		public string CATALOG_NAME { get; set; }

		public string CONTENT { get; set; }

		public decimal HIRER_ID { get; set; }

		public decimal? HIRER_COMPANY_ID { get; set; }

		public string HIRER_NAME { get; set; }

		public string HIRER_PHONE_NUMBER { get; set; }

		public decimal? POST_CNT { get; set; }

		public decimal? HIRER_CANCEL_CNT { get; set; }

		public decimal? HIRER_SCORE { get; set; }

		public decimal? WORKER_ID { get; set; }

		public decimal? WORKER_COMPANY_ID { get; set; }

		public string WORKER_NAME { get; set; }

		public string WORKER_PHONE_NUMBER { get; set; }

		public decimal? RECEIVE_CNT { get; set; }

		public decimal? WORKER_CANCEL_CNT { get; set; }

		public decimal? WORKER_SCORE { get; set; }

		public decimal? EXCHANGE_STATUS { get; set; }

		public decimal JOB_STATUS { get; set; }

		public DateTime? EXCHANGE_DATE { get; set; }

		public string ADDRESS { get; set; }

		public string LATITUDE { get; set; }

		public string LONGITUDE { get; set; }

		public string PHYSICAL_ADDRESS { get; set; }

		public decimal? SEND_MODE_USER { get; set; }

		public string REQUEST_CONTENT { get; set; }

		public string INTRODUCTION { get; set; }
	}
}
