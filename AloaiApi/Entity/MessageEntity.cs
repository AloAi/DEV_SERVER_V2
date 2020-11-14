//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;

namespace Aloai.Entity
{
    public class MessageEntity
    {
        /// <summary>
        /// Message Id.
        /// </summary>
        public decimal messageId { get; set; }

        /// <summary>
        /// Message code.
        /// </summary>
        public decimal messageCd { get; set; }

        /// <summary>
        /// Language type.
        /// </summary>
        public string manguageType { get; set; }

        /// <summary>
        /// Message content.
        /// </summary>
        public string messageContent { get; set; }
    }
}