//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;

namespace Aloai.Entity
{
    /// <summary>
    /// Exchange Entity
    /// </summary>
    public class ExchangeEntity
    {
        /// <summary>
        /// Exchange Id
        /// </summary>
        public decimal ExchangeId { get; set; }

        /// <summary>
        /// Job Id
        /// </summary>
        public decimal JobId { get; set; }

        /// <summary>
        /// Worker Id
        /// </summary>
        public decimal WorkerId { get; set; }

        /// <summary>
        /// Exchange Date
        /// </summary>
        public DateTime ExchangeDate { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public decimal Status { get; set; }

        /// <summary>
        /// Hirer Id
        /// </summary>
        public decimal HirerId { get; set; }

        /// <summary>
        /// Catalog Cd
        /// </summary>
        public decimal? CatalogCd { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Mode user send request.
        /// </summary>
        public decimal? SendModeUser { get; set; }

        /// <summary>
        /// Hirer's content request.
        /// </summary>
        public string RequestContent { get; set; }

        /// <summary>
        /// Worker's introduction.
        /// </summary>
        public string Introduction { get; set; }
    }
}