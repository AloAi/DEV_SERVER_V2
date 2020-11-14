//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aloai.Entity;
using Aloai.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nancy;

namespace Aloai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    /// <summary>
    /// Location controller class.
    /// </summary>
    public class LocationController : ControllerBase
    {
        private readonly AloaiDataContext _context;

        public LocationController(AloaiDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get location list.
        /// </summary>
        /// <returns>List LocationEntity.</returns>
        [HttpGet("GetLocationList/{id}")]
        public ActionResult GetLocationList([FromRoute] DateTime? id)
        {
            List<M_LOCATION> locationList = _context.M_LOCATIONS.ToList();
            List<LocationEntity> list = new List<LocationEntity>();

            if (id != null)
            {
                var location = from d in _context.M_LOCATIONS
                               where d.UPD_DATETIME == id
                               select d;

                if (location.Any())
                {
                    return StatusCode((int)HttpStatusCode.Created);
                }
            }

            foreach (M_LOCATION location in locationList)
            {
                LocationEntity entity = new LocationEntity();
                entity.cd = location.CD;
                entity.name = location.NAME;
                entity.latitudeSouth = location.LATITUDE_SOUTH;
                entity.latitudeNorth = location.LATITUDE_NORTH;
                entity.longitudeEast = location.LONGITUDE_EAST;
                entity.longitudeWest = location.LONGITUDE_WEST;
                entity.updDatetime = location.UPD_DATETIME;

                list.Add(entity);
            }

            return Ok(list);
        }

        /// <summary>
        /// Get location by CD.
        /// </summary>
        /// <param name="id">Location CD</param>
        /// <returns>LocationEntity</returns>
        [HttpGet("GetLocationByCd/{id}")]
        public ActionResult GetLocationByCd([FromRoute] decimal id)
        {
            M_LOCATION location = _context.M_LOCATIONS.FirstOrDefault(x => x.CD == id);

            if (location == null)
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = "Data not exists.",
                    Data = null
                });
            }

            LocationEntity entity = new LocationEntity();
            entity.cd = location.CD;
            entity.name = location.NAME;
            entity.latitudeSouth = location.LATITUDE_SOUTH;
            entity.latitudeNorth = location.LATITUDE_NORTH;
            entity.longitudeEast = location.LONGITUDE_EAST;
            entity.longitudeWest = location.LONGITUDE_WEST;
            entity.updDatetime = location.UPD_DATETIME;

            return Ok(entity);
        }
    }
}