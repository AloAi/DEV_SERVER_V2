using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
	public class V_JOB
	{
		[Key]
		public decimal JOB_ID { get; set; }

		public decimal USER_ID { get; set; }

		public string NAME { get; set; }

		public string PHONE_NUMBER { get; set; }

		public string AVATAR { get; set; }

		public decimal? SCORE { get; set; }

		public decimal? LIKE_NUM { get; set; }

		public decimal TEMPLATE_CD { get; set; }

		public string TEMPLATE_TITLE { get; set; }

		public string TEMPLATE_TITLE_EN { get; set; }

		public string LATITUDE { get; set; }

		public string LONGITUDE { get; set; }

		public decimal? CATALOG_CD { get; set; }

		public string CATALOG_NAME { get; set; }

		public string CATALOG_NAME_EN { get; set; }
		public decimal CANCEL_FLG { get; set; }

		public DateTime REG_DATETIME { get; set; }

		public DateTime? UPD_DATETIME { get; set; }
	}
}