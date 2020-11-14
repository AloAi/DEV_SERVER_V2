using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
	public class M_QUESTION
	{
		[Key]
		public decimal FAQ_ID { get; set; }

		public decimal USER_ID { get; set; }

		public DateTime QUESTION_DATE { get; set; }

		public string SUBJECT { get; set; }

		public string CONTENT { get; set; }

		public DateTime? REG_DATETIME { get; set; }

		public DateTime? UPD_DATETIME { get; set; }
	}
}