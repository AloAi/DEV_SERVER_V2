using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
    public class M_SYSTEM_MESSAGE
    {
		[Key]
		public decimal MESSAGE_ID { get; set; }

		public decimal MESSAGE_CD { get; set; }

		public string LANGUAGE_TYPE { get; set; }

		public string MESSAGE { get; set; }

		public string MESSAGE_CONTENT { get; set; }

		public decimal REG_USER_ID { get; set; }

		public DateTime REG_DATETIME { get; set; }
	}
}