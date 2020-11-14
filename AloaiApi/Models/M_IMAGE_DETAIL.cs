using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
    public class M_IMAGE_DETAIL
    {
		[Key]
		public decimal ID { get; set; }

		public decimal OBJECT_TYPE { get; set; }

		public decimal OBJECT_ID { get; set; }

		public decimal ROW_NO { get; set; }

		public string IMAGE_NAME { get; set; }

		public string IMAGE_PATH { get; set; }
	}
}
