using System;
using System.ComponentModel.DataAnnotations;

namespace Aloai.Models
{
    public class Template
    {
        [Key]
        public decimal TemplateCd { get; set; }
        public string TemplateTitle { get; set; }
        public string TemplateTitleEn { get; set; }
        public decimal DispOrder { get; set; }
        public decimal DeleteFlg { get; set; }
        public DateTime RegDatetime { get; set; }
        public DateTime? UpdDatetime { get; set; }
    }
}