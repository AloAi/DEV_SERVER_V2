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
    /// <summary>
    /// Favourite controller class.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FavouriteController : ControllerBase
    {
        private readonly AloaiDataContext _context;
        public FavouriteController(AloaiDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get Favourite by User ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>IHttpActionResult</returns>
        [HttpGet("GetFavouriteList/{id}")]
        public ActionResult GetFavouriteList([FromRoute] decimal id)
        {
            var query = from d in _context.V_FAVOURITES
                        where d.USER_ID == id
                            && d.MODE_USER == (int)Mode.Hirer
                        orderby d.REG_DATETIME descending
                        select d;

            if (!query.Any())
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = "Not found",
                    Data = false
                });
            }

            List<V_FAVOURITE> favouriteList = query.ToList();
            List<FavouriteEntity> entityList = new List<FavouriteEntity>();
            decimal favouriteUserId = 0;

            foreach (V_FAVOURITE favourite in favouriteList)
            {
                if (favouriteUserId == favourite.FAVOURITE_USER_ID)
                {
                    continue;
                }

                favouriteUserId = favourite.FAVOURITE_USER_ID;

                FavouriteEntity entity = new FavouriteEntity();
                entity.favouriteId = favourite.FAVOURITE_ID;
                entity.userId = favourite.USER_ID;
                entity.favouriteUserId = favourite.FAVOURITE_USER_ID;
                entity.name = favourite.NAME;
                //entity.Sex = favourite.SEX;
                //entity.BirthDay = favourite.BIRTHDAY;
                entity.avatar = favourite.AVATAR;
                entity.introduce = favourite.INTRODUCE;
                entity.modeDefault = favourite.MODE_DEFAULT;
                //entity.AccountType = favourite.ACCOUNT_TYPE;
                //entity.MemberType = favourite.MEMBER_TYPE;
                //entity.MemberTypeColor = Utility.GetNameInfo(Constant.LANGUAGE_VN, Constant.MEMBER_COLOR, favourite.MEMBER_TYPE.Value).Name;
                entity.modeUser = favourite.MODE_USER;

                Catalog catalog = new Catalog();
                catalog.catalogCd = favourite.CATALOG_CD;
                catalog.catalogName = favourite.CATALOG_NAME;
                entity.catalog = catalog;

                entity.cost = favourite.COST;

                Unit unit = new Unit();
                unit.unitCd = favourite.UNIT_CD;
                unit.unitName = favourite.UNIT_NAME;
                entity.unit = unit;

                entity.score = favourite.SCORE.Value;
                entity.likeNumber = favourite.LIKE_NUM.GetValueOrDefault(0);
                //entity.CompleteCnt = Utility.CountJobComplete(favourite.FAVOURITE_USER_ID, Mode.Worker , 0);
                entity.favouriteFlag = 1;

                entityList.Add(entity);
            }

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = entityList
            });
        }

        /// <summary>
        /// Get Favourite by User ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="DetailNum">Favourite detail number</param>
        /// <returns>IHttpActionResult</returns>
        [HttpGet("GetFavouriteList/{id}/{detailNum}")]
        public ActionResult GetFavouriteList([FromRoute] decimal id, [FromRoute] int detailNum)
        {
            var query = (from d in _context.V_FAVOURITES
                         where d.USER_ID == id
                             && d.MODE_USER == (int)Mode.Hirer
                         orderby d.REG_DATETIME descending
                         select d).Take(detailNum);

            if (!query.Any())
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = "Not found",
                    Data = false
                });
            }

            List<V_FAVOURITE> favouriteList = query.ToList();
            List<FavouriteEntity> entityList = new List<FavouriteEntity>();
            decimal favouriteUserId = 0;

            foreach (V_FAVOURITE favourite in favouriteList)
            {
                if (favouriteUserId == favourite.FAVOURITE_USER_ID)
                {
                    continue;
                }

                favouriteUserId = favourite.FAVOURITE_USER_ID;

                FavouriteEntity entity = new FavouriteEntity();
                entity.favouriteId = favourite.FAVOURITE_ID;
                entity.userId = favourite.USER_ID;
                entity.favouriteUserId = favourite.FAVOURITE_USER_ID;
                entity.name = favourite.NAME;
                //entity.Sex = favourite.SEX;
                entity.avatar = favourite.AVATAR;
                entity.introduce = favourite.INTRODUCE;
                entity.modeDefault = favourite.MODE_DEFAULT;
                //entity.AccountType = favourite.ACCOUNT_TYPE;
                //entity.MemberType = favourite.MEMBER_TYPE;
                entity.modeUser = favourite.MODE_USER;
                Catalog catalog = new Catalog();
                catalog.catalogCd = favourite.CATALOG_CD;
                catalog.catalogName = favourite.CATALOG_NAME;
                entity.catalog = catalog;

                entity.cost = favourite.COST;

                Unit unit = new Unit();
                unit.unitCd = favourite.UNIT_CD;
                unit.unitName = favourite.UNIT_NAME;
                entity.unit = unit;
                entity.score = favourite.SCORE.Value;
                entity.likeNumber = favourite.LIKE_NUM.GetValueOrDefault(0);
                entity.favouriteFlag = 1;

                var query3 = from d in _context.V_PARTNERS
                             where d.USER_ID == favourite.FAVOURITE_USER_ID
                             select d;

                PartnerCatalogEntity workerEntity = new PartnerCatalogEntity();

                var user = from d in _context.M_USERS
                           where d.USER_ID == favourite.FAVOURITE_USER_ID
                           select d;

                if (query3.Any())
                {
                    foreach (V_PARTNER worker in query3.ToList())
                    {
                        workerEntity.userId = worker.USER_ID;

                        Catalog catalog2 = new Catalog();
                        catalog2.catalogCd = worker.CATALOG_CD;
                        workerEntity.cost = worker.COST;

                        Unit unit2 = new Unit();
                        unit2.unitCd = worker.UNIT_CD;

                        string languageType = user.Single().LANGUAGE_TYPE;

                        if (string.IsNullOrEmpty(languageType) || languageType.Equals(Constant.LANGUAGE_VN))
                        {
                            catalog2.catalogName = worker.CATALOG_NAME;
                            unit2.unitName = worker.UNIT_NAME;
                        }
                        else
                        {
                            catalog2.catalogName = worker.CATALOG_NAME_EN;
                            unit2.unitName = worker.UNIT_NAME_EN;
                        }

                        workerEntity.catalog = catalog2;
                        workerEntity.unit = unit2;
                        break;
                    }
                }

                entity.partnerCatalog = workerEntity;
                entityList.Add(entity);
            }

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = entityList
            });
        }

        /// <summary>
        /// Update Favourite
        /// </summary>
        /// <param name="favouriteEntity">Favourite Entity</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpPost("UpdateFavourite")]
        public ActionResult UpdateFavourite(FavouriteEntity favouriteEntity)
        {
            IDbContextTransaction tran = _context.Database.BeginTransaction();

            try
            {
                System.Web.Http.HttpError errorHttp = null;

                // Favouriteの場合
                if (favouriteEntity.favouriteFlag == 1)
                {
                    var query = from d in _context.D_FAVOURITES
                                where d.USER_ID == favouriteEntity.userId
                                    && d.MODE_USER == (int)Mode.Hirer
                                    && d.FAVOURITE_USER_ID == favouriteEntity.favouriteUserId
                                select d;

                    if (!query.Any())
                    {
                        D_FAVOURITE favoutire = new D_FAVOURITE();
                        favoutire.USER_ID = favouriteEntity.userId;
                        favoutire.MODE_USER = (int)Mode.Hirer;
                        favoutire.FAVOURITE_USER_ID = favouriteEntity.favouriteUserId;
                        favoutire.CATALOG_CD = favouriteEntity.catalog.catalogCd.GetValueOrDefault(0);
                        favoutire.REG_DATETIME = Utility.GetSysDateTime();

                        _context.D_FAVOURITES.Add(favoutire);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    var query = from d in _context.D_FAVOURITES
                                where d.USER_ID == favouriteEntity.userId
                                    && d.MODE_USER == (int)Mode.Hirer
                                    && d.FAVOURITE_USER_ID == favouriteEntity.favouriteUserId
                                select d;

                    if (query.Any())
                    {
                        D_FAVOURITE favoutire = query.Single();
                        _context.D_FAVOURITES.Remove(favoutire);
                        _context.SaveChanges();
                    }
                }

                // Commit transaction.
                tran.Commit();

                errorHttp = new System.Web.Http.HttpError("Update is success!");

                return Ok(new Result
                {
                    Status = 200,
                    Message = errorHttp.Message,
                    Data = true
                });
            }
            catch (Exception ex)
            {
                // Rollback transaction.
                tran.Rollback();
                System.Web.Http.HttpError error = new System.Web.Http.HttpError("Error system!");

                return Ok(new Result
                {
                    Status = 404,
                    Message = error.Message,
                    Data = false
                });
            }
        }
    }
}