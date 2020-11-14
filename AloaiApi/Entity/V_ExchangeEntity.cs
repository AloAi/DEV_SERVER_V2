//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aloai.Entity
{
    /// <summary>
    /// View exchange entity class.
    /// </summary>
    public class V_ExchangeEntity
    {
        /// <summary>
        /// Exchange ID.
        /// </summary>
        public decimal? ExchangeId { get; set; }

        /// <summary>
        /// Job ID.
        /// </summary>
        public decimal JobId { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Cost.
        /// </summary>
        public decimal? Cost { get; set; }

        /// <summary>
        /// Worker ID.
        /// </summary>
        public decimal? WorkerId { get; set; }

        /// <summary>
        /// Worker name.
        /// </summary>
        public string WorkerName { get; set; }

        /// <summary>
        /// Worker's avatar.
        /// </summary>
        public string WorkerAvatar { get; set; }

        /// <summary>
        /// Worker member type color.
        /// </summary>
        public string WorkerMemberTypeColor { get; set; }

        /// <summary>
        /// Worker's phone number.
        /// </summary>
        public string WorkerPhoneNumber { get; set; }

        /// <summary>
        /// Worker's score.
        /// </summary>
        public decimal? WorkerScore { get; set; }

        /// <summary>
        /// Receive count.
        /// </summary>
        public decimal? ReceiveCnt { get; set; }

        /// <summary>
        /// Worker cancel count.
        /// </summary>
        public decimal? WorkerCancelCnt { get; set; }

        /// <summary>
        /// Worker complete count.
        /// </summary>
        public decimal? CompleteCnt { get; set; }

        /// <summary>
        /// Exchange date.
        /// </summary>
        public DateTime? ExchangeDate { get; set; }

        /// <summary>
        /// Hirer ID.
        /// </summary>
        public decimal HirerId { get; set; }

        /// <summary>
        /// Hirer name.
        /// </summary>
        public string HirerName { get; set; }

        /// <summary>
        /// Hirer's phone number.
        /// </summary>
        public string HirerPhoneNumber { get; set; }

        /// <summary>
        ///  Hirer's score.
        /// </summary>
        public decimal? HirerScore { get; set; }

        /// <summary>
        /// Hirer's avatar.
        /// </summary>
        public string HirerAvatar { get; set; }

        /// <summary>
        /// Hirer member type color.
        /// </summary>
        public string HirerMemberTypeColor { get; set; }

        /// <summary>
        /// Post count.
        /// </summary>
        public decimal? PostCnt { get; set; }

        /// <summary>
        /// Hirer cancel count.
        /// </summary>
        public decimal? HirerCancelCnt { get; set; }

        /// <summary>
        /// Hirer complete count.
        /// </summary>
        public decimal? HirerCompleteCnt { get; set; }

        /// <summary>
        /// Catalog code.
        /// </summary>
        public decimal? CatalogCd { get; set; }

        /// <summary>
        /// Catalog name.
        /// </summary>
        public string CatalogName { get; set; }

        /// <summary>
        /// Unit code.
        /// </summary>
        public decimal? UnitCd { get; set; }

        /// <summary>
        /// Unit name.
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// Exchange status.
        /// </summary>
        public decimal? ExchangeStatus { get; set; }

        /// <summary>
        /// Job status.
        /// </summary>
        public decimal JobStatus { get; set; }

        /// <summary>
        /// Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Latitude.
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Longitude.
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// Physical address.
        /// </summary>
        public string PhysicalAddress { get; set; }

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