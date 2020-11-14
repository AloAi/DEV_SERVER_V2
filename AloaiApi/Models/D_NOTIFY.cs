using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
	public class D_NOTIFY
	{
		[Key]
		public decimal NOTIFY_ID { get; set; }

		public decimal NOTIFY_TYPE { get; set; }

		public decimal OBJECT_ID { get; set; }

		public decimal USER_SEND_ID { get; set; }

		public decimal USER_RECIEVE_ID { get; set; }

		public decimal RECEIVE_MODE_USER { get; set; }

		public System.DateTime NOTIFY_DATE { get; set; }

		public string CONTENT { get; set; }

		public decimal READED_FLG { get; set; }

		public DateTime? REG_DATETIME { get; set; }

		public DateTime? UPD_DATETIME { get; set; }
	}
}