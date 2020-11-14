//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

namespace Aloai.Entity
{
    /// <summary>
    /// Name entity class.
    /// </summary>
    public class NameEntity
    {
        /// <summary>
        /// Type name.
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// Name code.
        /// </summary>
        public decimal cd { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Display order.
        /// </summary>
        public decimal dispOrder { get; set; }

        /// <summary>
        /// Delete flag.
        /// </summary>
        public decimal deleteFlg { get; set; }
    }
}