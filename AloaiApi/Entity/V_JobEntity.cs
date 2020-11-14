using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Entity
{
    public class V_JobEntity
    {
        /// <summary>
        /// Job Id.
        /// </summary>
        public decimal jobId { get; set; }

        /// <summary>
        /// Hirer Id.
        /// </summary>
        public decimal userId { get; set; }

        /// <summary>
        /// Hirer Id.
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// Hirer Id.
        /// </summary>
        public string phoneNumber { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public ImageInfoEntity avatar { get; set; }
        public Location location { get; set; }


        public TemplateEntity template { get; set; }
        ///// <summary>
        ///// TemplateCd.
        ///// </summary>
        //public decimal templateCd { get; set; }

        ///// <summary>
        ///// TemplateTitle.
        ///// </summary>
        //public string templateTitle { get; set; }

        /// <summary>
        /// Content.
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// Status.
        /// </summary>
        public decimal suggestId { get; set; }

        public Catalog catalog  { get; set; }

        public decimal? score { get; set; }

        public decimal? likeNum { get; set; }

        public DateTime dateTime { get; set; }

    }
}