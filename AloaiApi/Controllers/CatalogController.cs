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

namespace Aloai.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    /// <summary>
    /// Catalog controller class.
    /// </summary>
    public class CatalogController : ControllerBase
    {
        private readonly AloaiDataContext _context;
        public CatalogController(AloaiDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get catalog list.
        /// </summary>
        /// <param name="id">Language type</param>
        /// <returns>List CatalogEntity</returns>
        [HttpGet("GetCatalogList/{language}")]
        public ActionResult GetCatalogList(string language)
        {
            var query = from d in _context.M_CATALOGS
                        where d.DELETE_FLG == Constant.USING_FLG
                            && d.SHOW_FLG == (int)ShowFlg.Show
                        orderby d.DISP_ORDER ascending
                        select d;

            List<M_CATALOG> emps = query.ToList();
            List<CatalogEntity> list = new List<CatalogEntity>();

            foreach (M_CATALOG catalog in emps)
            {
                CatalogEntity entity = new CatalogEntity();

                entity.cd = catalog.CATALOG_CD;

                if (string.IsNullOrEmpty(language) || language.Equals(Constant.LANGUAGE_VN))
                {
                    entity.name = catalog.CATALOG_NAME;
                }
                else
                {
                    entity.name = catalog.CATALOG_NAME_EN;
                }

                entity.dispOrder = catalog.DISP_ORDER;

                list.Add(entity);
            }

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = list
            });
        }

        /// <summary>
        /// Get catalog by code.
        /// </summary>
        /// <param name="id">Catalog code</param>
        /// <returns>CatalogEntity</returns>
        [HttpGet("GetCatalog/{cd}/{languageType}")]
        public ActionResult GetCatalog([FromRoute] decimal cd, [FromRoute] string language)
        {
            M_CATALOG catalog = _context.M_CATALOGS.FirstOrDefault(x => x.CATALOG_CD == cd);

            if (catalog == null)
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = "Data not exists.",
                    Data = null
                });
            }

            CatalogEntity entity = new CatalogEntity();
            entity.cd = catalog.CATALOG_CD;

            if (string.IsNullOrEmpty(language) || language.Equals(Constant.LANGUAGE_VN))
            {
                entity.name = catalog.CATALOG_NAME;
            }
            else
            {
                entity.name = catalog.CATALOG_NAME_EN;
            }

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = entity
            });
        }
    }
}