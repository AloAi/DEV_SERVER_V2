using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
    public class M_NAME
    {
		[Key]
		public string TYPE_NAME { get; set; }

		[Key]
		public decimal CD { get; set; }

		public string NAME { get; set; }

		public string NAME_EN { get; set; }

		public decimal DISP_ORDER { get; set; }

		public decimal DELETE_FLG { get; set; }

		public DateTime? REG_DATETIME { get; set; }

		public decimal? REG_USER_ID { get; set; }

		public DateTime? UPD_DATETIME { get; set; }

		public decimal? UPD_USER_ID { get; set; }
	}
}