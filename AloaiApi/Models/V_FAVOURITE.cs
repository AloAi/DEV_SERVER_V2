using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
	public class V_FAVOURITE
	{
		[Key]
		public decimal FAVOURITE_ID { get; set; }

		public decimal USER_ID { get; set; }

		public decimal MODE_USER { get; set; }

		public decimal FAVOURITE_USER_ID { get; set; }

		public DateTime REG_DATETIME { get; set; }

		public string PHONE_NUMBER { get; set; }

		public string NAME { get; set; }

		//public decimal SEX { get; set; }

		//public DateTime BIRTHDAY { get; set; }

		public string AVATAR { get; set; }

		public string INTRODUCE { get; set; }

		public decimal MODE_DEFAULT { get; set; }

		//public decimal ACCOUNT_TYPE { get; set; }

		//public decimal MEMBER_TYPE { get; set; }

		public decimal? CATALOG_CD { get; set; }

		public string CATALOG_NAME { get; set; }

		public decimal? COST { get; set; }

		public decimal? UNIT_CD { get; set; }

		public string UNIT_NAME { get; set; }

		public decimal? SCORE { get; set; }

		public decimal? LIKE_NUM { get; set; }
	}
}