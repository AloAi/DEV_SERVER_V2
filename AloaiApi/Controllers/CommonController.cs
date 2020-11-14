using System;
using System.Collections.Generic;
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

namespace Aloai.Controllers {
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class CommonController : ControllerBase {
        private readonly AloaiDataContext _context;
        public CommonController(AloaiDataContext context) {
            _context = context;
        }

        /// <summary>
        /// Get request timeout value.
        /// </summary>
        /// <returns>Request timeout value</returns>
        [HttpGet("GetRequestTimeOut")]
        public decimal GetRequestTimeOut() {
            return decimal.Parse(Utility.GetDefineValue(Constant.REQUEST_TIME).value);
        }

        /// <summary>
        /// Get limit area value.
        /// </summary>
        /// <returns>Limit area value</returns>
        [HttpGet("GetLimitArea")]
        public decimal GetLimitArea() {
            return decimal.Parse(Utility.GetDefineValue(Constant.LIMIT_AREA).value);
        }

        /// <summary>
        /// Get notify number maximum.
        /// </summary>
        /// <returns>Notify number maximum</returns>
        [HttpGet("GetNotifyNumMax")]
        public decimal GetNotifyNumMax() {
            return decimal.Parse(Utility.GetDefineValue(Constant.NOTIFY_NUMBER_MAX).value);
        }

        /// <summary>
        /// Get comment number maximum.
        /// </summary>
        /// <returns>Comment number maximum</returns>
        [HttpGet("GetCommentNumMax")]
        public decimal GetCommentNumMax() {
            return decimal.Parse(Utility.GetDefineValue(Constant.COMMENT_NUMBER_MAX).value);
        }

        /// <summary>
        /// Get history detail number maximum.
        /// </summary>
        /// <returns>history detail number maximum</returns>
        [HttpGet("GetHistoryNumMax")]
        public decimal GetHistoryNumMax() {
            return decimal.Parse(Utility.GetDefineValue(Constant.COMMENT_NUMBER_MAX).value);
        }

        /// <summary>
        /// Get common info list.
        /// </summary>
        /// <returns>List DefineEntity.</returns>
        [HttpGet("GetCommonList")]
        public ActionResult GetCommonList() {
            List<M_DEFINE> emps = _context.M_DEFINES.ToList();

            List<DefineEntity> list = new List<DefineEntity>();

            foreach (M_DEFINE denfine in emps) {
                DefineEntity entity = new DefineEntity();

                entity.controlName = denfine.CONTROL_NAME;
                entity.dataType = denfine.DATA_TYPE;
                entity.value = denfine.VALUE;
                entity.memo = denfine.MEMO;
                list.Add(entity);
            }

            return Ok(new Result {
                Status = 200,
                    Message = string.Empty,
                    Data = list
            });
        }

        /// <summary>
        /// Get common info list.
        /// </summary>
        /// <returns>List DefineEntity.</returns>
        [HttpGet("GetCommonList/{language}")]
        public ActionResult GetCommonList(string language) {
            CommonEntity commonEntity = new CommonEntity();
            List<M_DEFINE> emps = _context.M_DEFINES.ToList();

            List<DefineEntity> list = new List<DefineEntity>();

            foreach (M_DEFINE denfine in emps) {
                DefineEntity entity = new DefineEntity();

                entity.controlName = denfine.CONTROL_NAME;
                entity.dataType = denfine.DATA_TYPE;
                entity.value = denfine.VALUE;
                entity.memo = denfine.MEMO;
                list.Add(entity);
            }

            commonEntity.commonList = list;

            var query2 = from d in _context.M_UNITS
            where d.DELETE_FLG == 0 &&
                d.SHOW_FLG == (int) ShowFlg.Show
            orderby d.DISP_ORDER ascending
            select d;

            List<M_UNIT> empsUnit = query2.ToList();
            List<UnitEntity> unitList = new List<UnitEntity>();

            foreach (M_UNIT unit in empsUnit) {
                UnitEntity unitEntity = new UnitEntity();

                unitEntity.cd = unit.UNIT_CD;

                if (string.IsNullOrEmpty(language) || language.Equals(Constant.LANGUAGE_VN)) {
                    unitEntity.name = unit.UNIT_NAME;
                } else {
                    unitEntity.name = unit.UNIT_NAME_EN;
                }

                unitEntity.dispOrder = unit.DISP_ORDER;

                unitList.Add(unitEntity);
            }

            commonEntity.unitList = unitList;

            var query3 = from d in _context.M_CATALOGS
            where d.DELETE_FLG == Constant.USING_FLG &&
                d.SHOW_FLG == (int) ShowFlg.Show
            orderby d.DISP_ORDER ascending
            select d;

            List<M_CATALOG> empsCatalog = query3.ToList();
            List<CatalogEntity> catalogList = new List<CatalogEntity>();

            foreach (M_CATALOG catalog in empsCatalog) {
                CatalogEntity entity = new CatalogEntity();

                entity.cd = catalog.CATALOG_CD;

                if (string.IsNullOrEmpty(language) || language.Equals(Constant.LANGUAGE_VN)) {
                    entity.name = catalog.CATALOG_NAME;
                } else {
                    entity.name = catalog.CATALOG_NAME_EN;
                }

                entity.dispOrder = catalog.DISP_ORDER;

                catalogList.Add(entity);
            }

            commonEntity.catalogList = catalogList;

            return Ok(new Result {
                Status = 200,
                    Message = string.Empty,
                    Data = commonEntity
            });
        }
    }
}