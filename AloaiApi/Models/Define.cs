using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
    public class M_DEFINE
    {
		[Key]
		public string CONTROL_NAME { get; set; }

		public decimal DATA_TYPE { get; set; }

		public string VALUE { get; set; }

		public string MEMO { get; set; }

		public DateTime? REG_DATETIME { get; set; }

		public decimal? REG_USER_ID { get; set; }

		public DateTime? UPD_DATETIME { get; set; }

		public decimal? UPD_USER_ID { get; set; }
	}
}