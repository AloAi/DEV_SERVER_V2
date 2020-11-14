using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
    public class M_LOCATION
    {
		[Key]
		public decimal CD { get; set; }

		public string NAME { get; set; }

		public string LONGITUDE_EAST { get; set; }

		public string LONGITUDE_WEST { get; set; }

		public string LATITUDE_SOUTH { get; set; }

		public string LATITUDE_NORTH { get; set; }

		public DateTime REG_DATETIME { get; set; }

		public decimal REG_USER_ID { get; set; }

		public DateTime? UPD_DATETIME { get; set; }

		public decimal? UPD_USER_ID { get; set; }
	}
}