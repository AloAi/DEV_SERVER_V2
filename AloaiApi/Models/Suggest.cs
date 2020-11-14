using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
    public class Suggest
    {
        [Key]
        public decimal SuggestId { get; set; }
        public decimal CatalogCd { get; set; }
        public string TemplateCd { get; set; }
        public DateTime RegDatetime { get; set; }
        public DateTime? UpdDatetime { get; set; }
    }
}