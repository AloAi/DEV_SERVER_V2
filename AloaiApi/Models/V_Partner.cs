using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
	public class V_PARTNER
	{
		[Key]
		public decimal USER_ID { get; set; }

		public string NAME { get; set; }

		public string AVATAR { get; set; }

		public string INTRODUCE { get; set; }

		public decimal CATALOG_CD { get; set; }

		public string CATALOG_NAME { get; set; }

		public string CATALOG_NAME_EN { get; set; }

		public decimal? UNIT_CD { get; set; }

		public string UNIT_NAME { get; set; }

		public string UNIT_NAME_EN { get; set; }

		public decimal? COST { get; set; }

		public decimal? SCORE { get; set; }

		public decimal MODE_USER { get; set; }
	}
}