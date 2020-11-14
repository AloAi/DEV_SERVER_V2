using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Entity
{
    public class PartnerEntity
    {
        /// <summary>
        /// User ID.
        /// </summary>
        public decimal userId { get; set; }

        /// <summary>
        /// Phone Number.
        /// </summary>
        public string phoneNumber { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Introduce.
        /// </summary>
        public string introduce { get; set; }

        public ImageInfoEntity avatar { get; set; }

        public bool fixLocationFlg { get; set; }
        public Location location { get; set; }
        public bool verifyFlg { get; set; }
        public decimal? score { get; set; }
        public decimal? likeNum { get; set; }

        public string token { get; set; }

        /// <summary>
        /// Worker entity list.
        /// </summary>
        public PartnerCatalogEntity partnerCatalog { get; set; }

        public List<ImageInfoEntity> imageInfoList { get; set; }
    }
}