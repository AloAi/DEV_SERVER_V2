//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2017 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Aloai.Entity
{
    public class V_WorkerEntity
    {
        /// <summary>
        /// User ID.
        /// </summary>
        public decimal UserId { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        public string Name { get; set; }

        ///// <summary>
        ///// Sex.
        ///// </summary>
        //public decimal? Sex { get; set; }

        ///// <summary>
        ///// Birthday.
        ///// </summary>
        //public DateTime? BirthDay { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Introduce.
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// Token.
        /// </summary>
        //public string Token { get; set; }

        ///// <summary>
        ///// Mode user default.
        ///// </summary>
        //public decimal? ModeDefault { get; set; }

        ///// <summary>
        ///// Account type.
        ///// </summary>
        //public decimal? AccountType { get; set; }

        ///// <summary>
        ///// Member type.
        ///// </summary>
        //public decimal? MemberType { get; set; }

        /// <summary>
        /// Mode user.
        /// </summary>
        public decimal? ModeUser { get; set; }

        /// <summary>
        /// Catalog code.
        /// </summary>
        public decimal? CatalogCd { get; set; }

        /// <summary>
        /// Catalog name.
        /// </summary>
        public string CatalogName { get; set; }

        /// <summary>
        /// Catalog Detail code.
        /// </summary>
        public decimal? CatalogDetailCd { get; set; }

        /// <summary>
        /// Catalog Detail name.
        /// </summary>
        public string CatalogDetailName { get; set; }

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
        /// Score.
        /// </summary>
        public decimal? Score { get; set; }

        /// <summary>
        /// Image info list.
        /// </summary>
        public List<ImageInfoEntity> ImageInfoList { get; set; }
    }
}