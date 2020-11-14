using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Models
{
    public class M_USER
    {
        [Key]
        /// <summary>
        /// User ID.
        /// </summary>
        public decimal USER_ID { get; set; }

        /// <summary>
        /// Phone number.
        /// </summary>
        public string PHONE_NUMBER { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public string AVATAR { get; set; }

        /// <summary>
        /// Mode user default.
        /// </summary>
        public decimal? MODE_DEFAULT { get; set; }

        /// <summary>
        /// Mode user.
        /// </summary>
        public decimal? MODE_USER { get; set; }

        /// <summary>
        /// Language type.
        /// </summary>
        public DateTime? SIGNIN_LAST { get; set; }

        /// <summary>
        /// Language type.
        /// </summary>
        public string LANGUAGE_TYPE { get; set; }

        public decimal BLOCK_FLG { get; set; }

        public decimal DELETE_FLG { get; set; }

        public DateTime REG_DATETIME { get; set; }
        public DateTime? UPD_DATETIME { get; set; }

        public string TOKEN;
    }
}