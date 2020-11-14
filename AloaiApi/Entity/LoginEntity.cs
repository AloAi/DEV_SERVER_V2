//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;

namespace Aloai.Entity
{
    /// <summary>
    /// Login entity class.
    /// </summary>
    public class LoginEntity
    {
        /// <summary>
        /// User Id.
        /// </summary>
        //public decimal UserId { get; set; }

        /// <summary>
        /// Phone number.
        /// </summary>
        public string phoneNumber { get; set; }

        /// <summary>
        /// Token.
        /// </summary>
        public string token { get; set; }

        ///// <summary>
        ///// User name.
        ///// </summary>
        //public string Name { get; set; } = string.Empty;

        ///// <summary>
        ///// Sex.
        ///// </summary>
        //public decimal? Sex { get; set; }

        ///// <summary>
        ///// BirthDay.
        ///// </summary>
        //public DateTime? BirthDay { get; set; }

        ///// <summary>
        ///// Mode user default.
        ///// </summary>
        //public decimal ModeDefault { get; set; }

        public decimal modeUser { get; set; }

        /// <summary>
        /// Language type.
        /// </summary>
        public string languageType { get; set; }
    }
}