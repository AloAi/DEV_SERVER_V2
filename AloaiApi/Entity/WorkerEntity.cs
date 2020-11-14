//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

namespace Aloai.Entity
{
    /// <summary>
    /// Worker entity class.
    /// </summary>
    public class PartnerCatalogEntity
    {
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
}