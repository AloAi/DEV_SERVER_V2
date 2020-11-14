//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2017 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Aloai.Entity
{
    /// <summary>
    /// Job entity class.
    /// </summary>
    public class JobEntity
    {
        /// <summary>
        /// Job Id.
        /// </summary>
        public decimal jobId { get; set; }

        /// <summary>
        /// Hirer Id.
        /// </summary>
        public decimal userId { get; set; }

        ///// <summary>
        ///// Company Id.
        ///// </summary>
        //public decimal CompanyId { get; set; }

        ///// <summary>
        ///// Title.
        ///// </summary>
        //public string Title { get; set; }

        ///// <summary>
        ///// Cost.
        ///// </summary>
        //public decimal? Cost { get; set; }

        ///// <summary>
        ///// Unit code.
        ///// </summary>
        //public decimal? UnitCd { get; set; }

        ///// <summary>
        ///// Unit name.
        ///// </summary>
        //public string UnitName { get; set; }

        ///// <summary>
        ///// Address.
        ///// </summary>
        //public string Address { get; set; }

        ///// <summary>
        ///// Latitude.
        ///// </summary>
        //public string Latitude { get; set; }

        ///// <summary>
        ///// Longitude.
        ///// </summary>
        //public string Longitude { get; set; }

        public Location location { get; set; }

        ///// <summary>
        ///// Physical address.
        ///// </summary>
        //public string PhysicalAddress { get; set; }

        ///// <summary>
        ///// Content.
        ///// </summary>
        //public string Content { get; set; }

        ///// <summary>
        ///// Catalog code.
        ///// </summary>
        //public decimal? CatalogCd { get; set; }

        ///// <summary>
        ///// Catalog name.
        ///// </summary>
        //public string CatalogName { get; set; }

        /// <summary>
        /// Status.
        /// </summary>
        public decimal suggestId { get; set; }

        public bool cancelFlg { get; set; }

        public decimal renewNum { get; set; }

        /// <summary>
        /// Update datetime.
        /// </summary>
        public DateTime? updDatetime { get; set; }

        ///// <summary>
        ///// Member type color.
        ///// </summary>
        //public string HirerMemberTypeColor { get; set; }

        ///// <summary>
        ///// Avatar.
        ///// </summary>
        //public string Avatar { get; set; }

        ///// <summary>
        ///// Hirer name.
        ///// </summary>
        //public string HirerName { get; set; }

        ///// <summary>
        ///// Hirer's phone number.
        ///// </summary>
        //public string HirerPhoneNumber { get; set; }

        ///// <summary>
        ///// Image info list.
        ///// </summary>
        //public List<ImageInfoEntity> ImageInfoList { get; set; }
    }
}