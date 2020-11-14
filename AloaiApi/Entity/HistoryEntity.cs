//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;

namespace Aloai.Entity
{
    /// <summary>
    /// History entity class.
    /// </summary>
    public class HistoryEntity
    {
        /// <summary>
        /// History Id.
        /// </summary>
        public decimal HistoryId { get; set; }

        /// <summary>
        /// Exchange Id.
        /// </summary>
        public decimal ExchangeId { get; set; }

        /// <summary>
        /// Hirer Id.
        /// </summary>
        public decimal HirerId { get; set; }

        /// <summary>
        /// Hirer name.
        /// </summary>
        public string HirerName { get; set; }

        /// <summary>
        /// Job Id.
        /// </summary>
        public decimal JobId { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Cost.
        /// </summary>
        public decimal? Cost { get; set; }

        /// <summary>
        /// Unit code.
        /// </summary>
        public decimal? UnitCd { get; set; }

        /// <summary>
        /// Unit name.
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Catalog code.
        /// </summary>
        public decimal? CatalogCd { get; set; }

        /// <summary>
        /// Worker Id.
        /// </summary>
        public decimal WorkerId { get; set; }

        /// <summary>
        /// Worker name.
        /// </summary>
        public string WorkerName { get; set; }

        /// <summary>
        /// Exchange date.
        /// </summary>
        public DateTime ExchangeDate { get; set; }

        /// <summary>
        /// Complete date.
        /// </summary>
        public DateTime CompleteDate { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public decimal Status { get; set; }

        /// <summary>
        /// Register user Id.
        /// </summary>
        public decimal RegUserId { get; set; }

        /// <summary>
        /// Register mode user.
        /// </summary>
        public decimal RegModeUser { get; set; }

        /// <summary>
        /// Score.
        /// </summary>
        public decimal? Score { get; set; }

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

        /// <summary>
        /// Favourite flag.
        /// </summary>
        public decimal FavouriteFlag { get; set; }

        /// <summary>
        /// Company Id.
        /// </summary>
        public decimal? CompanyId { get; set; }

        /// <summary>
        /// Worker's member type.
        /// </summary>
        public decimal? WorkerMemberType { get; set; }

        /// <summary>
        /// Worker's Company Id.
        /// </summary>
        public decimal? WorkerCompanyId { get; set; }

        /// <summary>
        /// Company Id.
        /// </summary>
        public decimal? HirerCompanyId { get; set; }
    }
}