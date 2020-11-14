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
    /// User entity class.
    /// </summary>
    public class UserEntity
    {
        /// <summary>
        /// User ID.
        /// </summary>
        public decimal userId { get; set; }

        /// <summary>
        /// Phone number.
        /// </summary>
        public string phoneNumber { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Sex.
        /// </summary>
        public decimal? sex { get; set; }

        /// <summary>
        /// Birthday.
        /// </summary>
        public DateTime? birthDay { get; set; }

        public ImageInfoEntity avatar { get; set; }

        ///// <summary>
        ///// Introduce.
        ///// </summary>
        //public string Introduce { get; set; }

        /// <summary>
        /// Token.
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// Mode user default.
        /// </summary>
        public decimal? modeDefault { get; set; }

        ///// <summary>
        ///// Account type.
        ///// </summary>
        //public decimal? AccountType { get; set; }

        ///// <summary>
        ///// Member type.
        ///// </summary>
        //public decimal? MemberType { get; set; }

        ///// <summary>
        ///// Member type color.
        ///// </summary>
        //public string MemberTypeColor { get; set; }

        ///// <summary>
        ///// Member type name.
        ///// </summary>
        //public string MemberTypeName { get; set; }

        ///// <summary>
        ///// Account type name.
        ///// </summary>
        //public string AccountTypeName { get; set; }

        /// <summary>
        /// Mode user.
        /// </summary>
        public decimal? modeUser { get; set; }

        /// <summary>
        /// Language type.
        /// </summary>
        //public string LanguageType { get; set; }

        /// <summary>
        /// Allow receive job.
        /// </summary>
        //public decimal AllowReceiveJob { get; set; }

        /// <summary>
        /// Company Id.
        /// </summary>
        //public decimal? CompanyId { get; set; }

        /// <summary>
        /// Allow update infomation.
        /// </summary>
        //public decimal AllowUpdateInfo { get; set; }

        /// <summary>
        /// Score.
        /// </summary>
        public decimal? score { get; set; }

        public decimal? likeNum { get; set; }

        ///// <summary>
        ///// Worker score.
        ///// </summary>
        //public decimal? WorkerScore { get; set; }

        ///// <summary>
        ///// Number Job complete.
        ///// </summary>
        //public decimal? WorkerCompleteCnt { get; set; }

        ///// <summary>
        ///// Number Job receive.
        ///// </summary>
        //public decimal? WorkerReceiveCnt { get; set; }

        ///// <summary>
        ///// Number Job cancel.
        ///// </summary>
        //public decimal? WorkerCancelCnt { get; set; }

        ///// <summary>
        ///// Hirer's Number Job post.
        ///// </summary>
        //public decimal? HirerPostCnt { get; set; }

        ///// <summary>
        ///// Hirer's Number Job cancel.
        ///// </summary>
        //public decimal? HirerCancelCnt { get; set; }

        ///// <summary>
        ///// Hirer's number Job complete.
        ///// </summary>
        //public decimal? HirerCompleteCnt { get; set; }

        ///// <summary>
        ///// Hirer score.
        ///// </summary>
        //public decimal? HirerScore { get; set; }

        ///// <summary>
        ///// Worker entity list.
        ///// </summary>
        //public List<PartnerCatalogEntity> WorkerEntityList { get; set; }

        ///// <summary>
        ///// Worker info entity.
        ///// </summary>
        //public PartnerInfoEntity WorkerInfoEntity { get; set; }

        ///// <summary>
        ///// Hirer info entity list.
        ///// </summary>
        //public HirerInfoEntity HirerInfoEntity { get; set; }

        ///// <summary>
        ///// Image info list.
        ///// </summary>
        //public List<ImageInfoEntity> ImageInfoList { get; set; }
    }

    public class User
    {
        /// <summary>
        /// User ID.
        /// </summary>
        public decimal userId { get; set; }

        /// <summary>
        /// Phone number.
        /// </summary>
        public string phoneNumber { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        public string name { get; set; }
    }
}