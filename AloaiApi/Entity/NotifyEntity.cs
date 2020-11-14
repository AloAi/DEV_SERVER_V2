//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;

namespace Aloai.Entity
{
    /// <summary>
    /// Notify entity class.
    /// </summary>
    public class NotifyEntity
    {
        /// <summary>
        /// Notify ID.
        /// </summary>
        public decimal notifyId { get; set; }

        /// <summary>
        /// Notify type.
        /// </summary>
        public decimal notifyType { get; set; }

        /// <summary>
        /// Object ID.
        /// </summary>
        public decimal objectId { get; set; }

        /// <summary>
        /// User send ID.
        /// </summary>
        public decimal userSendId { get; set; }

        /// <summary>
        /// User receive ID.
        /// </summary>
        public decimal userReceiveId { get; set; }

        /// <summary>
        /// Receive mode user.
        /// </summary>
        public decimal receiveModeUser { get; set; }

        /// <summary>
        /// Notify date.
        /// </summary>
        public DateTime notifyDate { get; set; }

        /// <summary>
        /// Content.
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// Readed flag.
        /// </summary>
        public decimal readedFlg { get; set; }
    }
}