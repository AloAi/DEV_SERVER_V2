//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

namespace Aloai.Entity
{
    /// <summary>
    /// Catalog Entity class.
    /// </summary>
    public class CatalogEntity
    {
        /// <summary>
        /// Cd.
        /// </summary>
        public decimal? cd { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Disp Order.
        /// </summary>
        public decimal dispOrder { get; set; }

        /// <summary>
        /// Show flg.
        /// </summary>
        public decimal showFlg { get; set; }

        /// <summary>
        /// Delete flg.
        /// </summary>
        public decimal deleteFlg { get; set; }
    }

    public class Catalog
    {
        /// <summary>
        /// Cd.
        /// </summary>
        public decimal? catalogCd { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string catalogName { get; set; }
    }
}