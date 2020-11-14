//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;

namespace Aloai.Entity
{
    /// <summary>
    /// Estimation Entity class.
    /// </summary>
    public class ReviewEntity
    {
        /// <summary>
        /// Estimation Id.
        /// </summary>
        public decimal reviewId { get; set; }

        /// <summary>
        /// Exchange Id.
        /// </summary>
        public decimal contactId { get; set; }

        /// <summary>
        /// Estimator user Id.
        /// </summary>
        public decimal reviewUserId { get; set; }

        /// <summary>
        /// Estimator user name.
        /// </summary>
        public string reviewUserName { get; set; }

        /// <summary>
        /// Estimator's mode user.
        /// </summary>
        public decimal reviewModeUser { get; set; }

        /// <summary>
        /// Catalog Cd.
        /// </summary>
        public decimal catalogCd { get; set; }
        public bool isSended { get; set; }

        /// <summary>
        /// Estimation date.
        /// </summary>
        public DateTime? reviewDate { get; set; }

        /// <summary>
        /// Score.
        /// </summary>
        public decimal? score { get; set; }

        /// <summary>
        /// Comment.
        /// </summary>
        public string comment { get; set; }
        public bool isReceived { get; set; }

        /// <summary>
        /// Estimation date received.
        /// </summary>
        public DateTime? reviewDateReceive { get; set; }

        /// <summary>
        /// Score received.
        /// </summary>
        public decimal? scoreReceive { get; set; }

        /// <summary>
        /// Comment received.
        /// </summary>
        public string commentReceive { get; set; }
    }
}