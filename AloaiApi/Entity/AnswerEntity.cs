//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;

namespace Aloai.Entity
{
    /// <summary>
    /// Answer Entity class
    /// </summary>
    public class AnswerEntity
    {
        /// <summary>
        /// Answer Id
        /// </summary>
        public decimal answerId { get; set; }

        /// <summary>
        /// Faq Id
        /// </summary>
        public decimal faqId { get; set; }

        /// <summary>
        /// Answer date
        /// </summary>
        public DateTime answerDate { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public string content { get; set; }
    }
}