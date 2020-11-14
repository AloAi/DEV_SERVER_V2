using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aloai.Entity
{
    /// <summary>
    /// Image info entity.
    /// </summary>
    public class ImageInfoEntity
    {
        /// <summary>
        /// Image path.
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// Image.
        /// </summary>
        public string image { get; set; }
    }
}