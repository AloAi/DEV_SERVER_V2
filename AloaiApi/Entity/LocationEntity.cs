using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aloai.Entity
{
    public class LocationEntity
    {
        /// <summary>
        /// Location cd.
        /// </summary>
        public decimal cd { get; set; }

        /// <summary>
        /// Location name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Longitude east.
        /// </summary>
        public string longitudeEast { get; set; }

        /// <summary>
        /// Longitude west.
        /// </summary>
        public string longitudeWest { get; set; }

        /// <summary>
        /// Latitude south.
        /// </summary>
        public string latitudeSouth { get; set; }

        /// <summary>
        /// Latitude north.
        /// </summary>
        public string latitudeNorth { get; set; }

        /// <summary>
        /// Update time.
        /// </summary>
        public DateTime? updDatetime { get; set; }
    }
}