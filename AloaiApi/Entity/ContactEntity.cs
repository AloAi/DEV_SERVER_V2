//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;

namespace Aloai.Entity
{
    /// <summary>
    /// Contact Entity.
    /// </summary>
    public class ContactEntity
    {
        /// <summary>
        /// Id.
        /// </summary>
        public decimal contactId { get; set; }

        /// <summary>
        /// Contacter's mode user.
        /// </summary>
        public decimal contactModeUser { get; set; }

        /// <summary>
        /// Contact user Id.
        /// </summary>
        public decimal contactUserId { get; set; }

        /// <summary>
        /// User Id.
        /// </summary>
        public decimal userRecieveId { get; set; }

        /// <summary>
        /// Catalog Cd.
        /// </summary>
        public decimal catalogCd { get; set; }

        /// <summary>
        /// Contact date.
        /// </summary>
        public DateTime contactDate { get; set; }

        /// <summary>
        /// Content.
        /// </summary>
        public string content { get; set; }
    }
}