//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

namespace Aloai.Entity
{
    /// <summary>
    /// Unit entity class.
    /// </summary>
    public class UnitEntity
    {
        /// <summary>
        /// Unit code.
        /// </summary>
        public decimal cd { get; set; }

        /// <summary>
        /// Unit name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Display order.
        /// </summary>
        public decimal dispOrder { get; set; }

        /// <summary>
        /// Show flag.
        /// </summary>
        public decimal showFlg { get; set; }

        /// <summary>
        /// Delete flag.
        /// </summary>
        public decimal deleteFlg { get; set; }
    }

    public class Unit
    {
        /// <summary>
        /// Unit code.
        /// </summary>
        public decimal? unitCd { get; set; }

        /// <summary>
        /// Unit name.
        /// </summary>
        public string unitName { get; set; }
    }
}