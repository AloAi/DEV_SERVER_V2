﻿//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;

namespace Aloai.Entity {
    /// <summary>
    /// Worker entity class.
    /// </summary>
    public class PartnerCatalogEntity {
        /// <summary>
        /// User ID.
        /// </summary>
        public decimal userId { get; set; }

        public Catalog catalog { get; set; }

        /// <summary>
        /// Cost.
        /// </summary>
        public decimal? cost { get; set; }

        public Unit unit { get; set; }
    }

    public class PartnerInfoEntity {

        public decimal userId { get; set; }
        public decimal score { get; set; }
        public string introduce { get; set; }
        public decimal fitLocationFlg { get; set; }

        public Location location { get; set; }

        public decimal verifyFlg { get; set; }

        public DateTime? verifyDate { get; set; }

        public DateTime? verifyDateFrom { get; set; }

        public DateTime? verifyDateTo { get; set; }

        public decimal? likeNum { get; set; }
        public decimal status { get; set; }

        public DateTime regDatetime { get; set; }

        public DateTime? updDatetime { get; set; }

    }
}