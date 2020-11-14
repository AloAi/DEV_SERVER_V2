//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;

namespace Aloai.Entity
{
    public class V_ContactEntity
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
        /// Contact user name.
        /// </summary>
        public string contactUserName { get; set; }

        /// <summary>
        /// Contact date.
        /// </summary>
        public DateTime contactDate { get; set; }

        /// <summary>
        /// Catalog Cd.
        /// </summary>
        public decimal catalogCd { get; set; }

        /// <summary>
        /// Call type. 1: Call, 2: Recieve
        /// </summary>
        public decimal callType { get; set; }
    }
}