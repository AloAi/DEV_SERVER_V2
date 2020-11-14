//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Aloai.Entity;
using Aloai.Enum;
using Aloai.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;
using Nancy;
using Nancy.Json;
using NHibernate.Linq;

namespace Aloai.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    /// <summary>
    /// Unit controller class.
    /// </summary>
    public class UnitController : ControllerBase
    {
        private readonly AloaiDataContext _context;
        public UnitController(AloaiDataContext context)
        {
            _context = context;
        }

        ///// <summary>
        ///// Get unit list.
        ///// </summary>
        ///// <param name="id">Language type</param>
        ///// <returns>List UnitEntity</returns>
        //[HttpGet("GetUnitList/{languageType}")]
        //public ActionResult GetUnitList(string languageType)
        //{
        //    var query = from d in _context.M_UNITS
        //                where d.DELETE_FLG == 0
        //                    && d.SHOW_FLG == (int)ShowFlg.Show
        //                orderby d.DISP_ORDER ascending
        //                select d;

        //    List<M_UNIT> emps = query.ToList();
        //    List<UnitEntity> list = new List<UnitEntity>();

        //    foreach (M_UNIT unit in emps)
        //    {
        //        UnitEntity entity = new UnitEntity();

        //        entity.cd = unit.UNIT_CD;

        //        if (string.IsNullOrEmpty(languageType) || languageType.Equals(Constant.LANGUAGE_VN))
        //        {
        //            entity.name = unit.UNIT_NAME;
        //        }
        //        else
        //        {
        //            entity.name = unit.UNIT_NAME_EN;
        //        }

        //        entity.dispOrder = unit.DISP_ORDER;

        //        list.Add(entity);
        //    }

        //    return Ok(new Result
        //    {
        //        Status = 200,
        //        Message = string.Empty,
        //        Data = list
        //    });
        //}

        /// <summary>
        /// Get unit info.
        /// </summary>
        /// <param name="id">Unit code</param>
        /// <returns>UnitEntity</returns>
        [HttpGet("GetUnit/{cd}/{languageType}")]
        public ActionResult GetUnit(decimal cd, string languageType)
        {
            M_UNIT unit = _context.M_UNITS.FirstOrDefault(x => x.UNIT_CD == cd);

            if (unit == null)
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = string.Empty,
                    Data = null
                });
            }

            UnitEntity entity = new UnitEntity();
            entity.cd = unit.UNIT_CD;

            if (string.IsNullOrEmpty(languageType) || languageType.Equals(Constant.LANGUAGE_VN))
            {
                entity.name = unit.UNIT_NAME;
            }
            else
            {
                entity.name = unit.UNIT_NAME_EN;
            }

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = entity
            });
        }

        /// <summary>
        /// Get unit list.
        /// </summary>
        /// <param name="id">Language type</param>
        /// <returns>List UnitEntity</returns>
        [HttpGet("GetUnitList/{languageType}")]
        public ActionResult GetUnitList(string languageType)
        {
            var query = from d in _context.M_UNITS
                        where d.DELETE_FLG == 0
                            && d.SHOW_FLG == (int)ShowFlg.Show
                        orderby d.DISP_ORDER ascending
                        select d;

            List<M_UNIT> emps = query.ToList();
            List<UnitEntity> list = new List<UnitEntity>();

            foreach (M_UNIT unit in emps)
            {
                UnitEntity entity = new UnitEntity();

                entity.cd = unit.UNIT_CD;

                if (string.IsNullOrEmpty(languageType) || languageType.Equals(Constant.LANGUAGE_VN))
                {
                    entity.name = unit.UNIT_NAME;
                }
                else
                {
                    entity.name = unit.UNIT_NAME_EN;
                }

                entity.dispOrder = unit.DISP_ORDER;

                list.Add(entity);
            }

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = list
            });
        }
    }
}