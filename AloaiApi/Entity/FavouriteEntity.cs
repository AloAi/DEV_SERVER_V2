//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Aloai.Entity
{
    public class FavouriteEntity
    {
        /// <summary>
        ///  Favourite ID.
        /// </summary>
        public decimal favouriteId { get; set; }

        /// <summary>
        /// User ID.
        /// </summary>
        public decimal userId { get; set; }

        /// <summary>
        /// Favourite User ID.
        /// </summary>
        public decimal favouriteUserId { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        public string name { get; set; }

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
        public string avatar { get; set; }

        /// <summary>
        /// Introduce.
        /// </summary>
        public string introduce { get; set; }

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
        //public String MemberTypeColor { get; set; }

        /// <summary>
        /// Mode user.
        /// </summary>
        public decimal? modeUser { get; set; }

        public Catalog catalog { get; set; }

        /// <summary>
        /// Cost.
        /// </summary>
        public decimal? cost { get; set; }

        public Unit unit { get; set; }

        /// <summary>
        /// Score.
        /// </summary>
        public decimal? score { get; set; }

        /// <summary>
        /// Receive count.
        /// </summary>
        public decimal? likeNumber { get; set; }

        /// <summary>
        /// Favourite flag.
        /// </summary>
        public decimal favouriteFlag { get; set; }

        /// <summary>
        /// Worker entity list.
        /// </summary>
        public PartnerCatalogEntity partnerCatalog { get; set; }
    }
}