//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

namespace Aloai.Entity
{
    /// <summary>
    /// Hirer info entity class.
    /// </summary>
    public class HirerInfoEntity
    {
        /// <summary>
        /// User Id.
        /// </summary>
        public decimal userId { get; set; }

        public string name { get; set; }
        ///// <summary>
        ///// Company Id.
        ///// </summary>
        //public decimal CompanyId { get; set; }

        /// <summary>
        /// PhoneNumber
        /// </summary>
        public string phoneNumber { get; set; }

        public ImageInfoEntity avatar { get; set; }

        /// <summary>
        /// Token.
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// Score.
        /// </summary>
        public decimal? score { get; set; }

        ///// <summary>
        ///// Post count.
        ///// </summary>
        //public decimal PostCnt { get; set; }

        ///// <summary>
        ///// Cancel count.
        ///// </summary>
        //public decimal CancelCnt { get; set; }

        ///// <summary>
        ///// Complete count.
        ///// </summary>
        //public decimal CompleteCnt { get; set; }
        public decimal? likeNum { get; set; }

        /// <summary>
        /// Status.
        /// </summary>
        public decimal status { get; set; }
    }
}