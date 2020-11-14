//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;

namespace Aloai.Entity
{
    /// <summary>
    /// Question entity class.
    /// </summary>
    public class QuestionEntity
    {
        /// <summary>
        /// Faq ID.
        /// </summary>
        public decimal faqId { get; set; }

        /// <summary>
        /// User ID create.
        /// </summary>
        public decimal userId { get; set; }

        /// <summary>
        /// User name create.
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// Question date created.
        /// </summary>
        public DateTime questionDate { get; set; }

        /// <summary>
        /// Subject.
        /// </summary>
        public string subject { get; set; }

        /// <summary>
        /// Content.
        /// </summary>
        public string content { get; set; }
    }
}