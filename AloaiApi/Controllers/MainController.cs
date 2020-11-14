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
    [Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        private readonly AloaiDataContext _context;
        public MainController(AloaiDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get job list by catalog code.
        /// </summary>
        /// <param name="id">Catalog code.</param>
        /// <returns>List job entity</returns>
        [HttpGet("GetJobListByCatalog/{id}")]
        public ActionResult GetJobListByCatalog(decimal id)
        {

            var query = from d in _context.V_JOBS
                        where d.CATALOG_CD == id
                        && d.CANCEL_FLG == (int)JobStatus.New
                        select d;

            if (!query.Any())
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = "Data not exists.",
                    Data = null
                });
            }

            List<V_JOB> jobList = query.ToList();
            List<JobEntity> entityList = new List<JobEntity>();

            foreach (V_JOB job in jobList)
            {
                JobEntity entity = new JobEntity();
                entity.jobId = job.JOB_ID;

                Location loc = new Location();
                loc.longitude = job.LONGITUDE;
                loc.latitude = job.LATITUDE;
                entity.location = loc;
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
        /// Get job list by filter.
        /// </summary>
        /// <param name="id">Filter string</param>
        /// <returns>List job entity</returns>
        [HttpGet("GetJobListByFilter/{id}")]
        public ActionResult GetJobListByFilter(string id)
        {
            var query = from d in _context.V_JOBS
                        where 1 == 1
                            //d.TITLE.Contains(@"" + id + "")
                            && d.CANCEL_FLG == (int)JobStatus.New
                            && SqlMethods.Like(d.TEMPLATE_TITLE, @"%" + id + "%")
                        select d;

            if (!query.Any())
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = "Data not exists.",
                    Data = null
                });
            }

            List<V_JOB> jobList = query.ToList();
            List<JobEntity> entityList = new List<JobEntity>();

            foreach (V_JOB job in jobList)
            {
                JobEntity entity = new JobEntity();
                entity.jobId = job.JOB_ID;
                Location loc = new Location();
                loc.longitude = job.LONGITUDE;
                loc.latitude = job.LATITUDE;
                entity.location = loc;
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
        /// Get worker by catalog code.
        /// </summary>
        /// <param name="id">Catalog code.</param>
        /// <returns>List worker entity</returns>
        [HttpGet("GetWorkerListByCatalog/{id}")]
        public ActionResult GetWorkerListByCatalog(decimal id)
        {
            var query = from d in _context.V_PARTNERS
                        where d.CATALOG_CD == id
                        select d;

            if (!query.Any())
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = "Data not exists.",
                    Data = null
                });
            }

            List<V_PARTNER> workerList = query.ToList();
            List<V_WorkerEntity> entityList = new List<V_WorkerEntity>();

            foreach (V_PARTNER worker in workerList)
            {
                V_WorkerEntity entity = new V_WorkerEntity();
                entity.UserId = worker.USER_ID;
                entity.Name = worker.NAME;
                entity.Avatar = worker.AVATAR;
                entity.Introduce = worker.INTRODUCE;
                //entity.Token = worker.TOKEN;
                entity.CatalogCd = worker.CATALOG_CD;
                entity.CatalogName = worker.CATALOG_NAME;
                entity.Cost = worker.COST;
                entity.UnitCd = worker.UNIT_CD;
                entity.UnitName = worker.UNIT_NAME;
                entity.Score = worker.SCORE.Value;

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
        /// Get worker list by filter.
        /// </summary>
        /// <param name="id">Filter string</param>
        /// <returns>List worker entity</returns>
        [HttpGet("GetWorkerListByFilter/{id}")]
        public ActionResult GetWorkerListByFilter(string id)
        {
            var query = from d in _context.V_PARTNERS
                        where SqlMethods.Like(d.CATALOG_NAME, @"%" + id + "%")
                        select d;

            if (!query.Any())
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = "Data not exists.",
                    Data = null
                });
            }

            List<V_PARTNER> workerList = query.ToList();
            var distinctList = workerList.Aggregate(new Dictionary<decimal, V_PARTNER>(),
                                                    (d, e) => { d[e.USER_ID] = e; return d; }, d => d.Values);

            //var distinctList = Utility.DistinctBy(workerList, p => p.USER_ID);
            List<V_WorkerEntity> entityList = new List<V_WorkerEntity>();

            foreach (V_PARTNER worker in distinctList)
            {
                V_WorkerEntity entity = new V_WorkerEntity();
                entity.UserId = worker.USER_ID;
                entity.Name = worker.NAME;
                entity.Avatar = worker.AVATAR;
                entity.Introduce = worker.INTRODUCE;
                entity.ModeUser = worker.MODE_USER;
                entity.CatalogCd = worker.CATALOG_CD;
                entity.CatalogName = worker.CATALOG_NAME;
                entity.Cost = worker.COST;
                entity.UnitCd = worker.UNIT_CD;
                entity.UnitName = worker.UNIT_NAME;
                entity.Score = worker.SCORE;

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
        /// Get values of enum Mode.
        /// </summary>
        /// <returns>Enum Mode values</returns>
        [HttpGet("GetMode")]
        //[Route("Mode/all")]
        public ActionResult GetMode()
        {
            return Ok(EnumExtensions.GetDisplayName(Mode.Hirer));
        }

        ///// <summary>
        ///// Get values of enum AccountType.
        ///// </summary>
        ///// <returns>Enum AccountType values</returns>
        //[HttpGet]
        //public ActionResult GetAccountType()
        //{
        //    return Ok(EnumExtensions.GetValues<AccountType>());
        //}

        ///// <summary>
        ///// Get all EnumEntity in project.
        ///// </summary>
        ///// <returns>List EnumEntity</returns>
        //[HttpGet]
        ////[Route("AccountType/all")]
        //public ActionResult GetAllEnum()
        //{
        //    return Ok(EnumExtensions.GetAllValues());
        //}

        ///// <summary>
        ///// Check book is seccess.
        ///// </summary>
        ///// <param name="entity">ExchangeEntity</param>
        ///// <returns>Success: EXCHANGE_ID; Not seccess: NULL</returns>
        //[HttpPut]
        //public decimal? CheckBookIsSuccess(ExchangeEntity entity)
        //{
        //    PartTimeDataClassesDataContext db = new PartTimeDataClassesDataContext();

        //    var query = from d in db.V_EXCHANGE
        //                where d.WORKER_ID == entity.WorkerId
        //                    && d.HIRER_ID == entity.HirerId
        //                    && d.EXCHANGE_STATUS == (int)ExchangeStatus.New
        //                select d;

        //    if (query.Any())
        //    {
        //        V_EXCHANGE exchange = query.First();
        //        return exchange.EXCHANGE_ID;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// Check book is seccess.
        ///// </summary>
        ///// <param name="entity">ExchangeEntity</param>
        ///// <returns>Success: EXCHANGE_ID; Not seccess: NULL</returns>
        //[HttpGet]
        //public decimal? CheckBookIsSuccess(decimal id, [FromUri] decimal HirerId)
        //{
        //    PartTimeDataClassesDataContext db = new PartTimeDataClassesDataContext();

        //    var query = from d in db.V_EXCHANGE
        //                where d.WORKER_ID == id
        //                    && d.HIRER_ID == HirerId
        //                    && d.EXCHANGE_STATUS == (int)ExchangeStatus.New
        //                select d;

        //    if (query.Any())
        //    {
        //        V_EXCHANGE exchange = query.First();
        //        return exchange.EXCHANGE_ID;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //[HttpGet]
        ////[Route("AccountType/all")]
        //public ActionResult GetFireBase()
        //{
        //    TokenGenerator.ConnectFirebase();

        //    return Ok();
        //}

        //[HttpPost]
        ////[Route("AccountType/all")]
        //public ActionResult GetFireBaseIdByToken(AuthorEntity author)
        //{
        //    AuthorUtility.Author auth = new AuthorUtility.Author();
        //    string token = auth.GetFirebaseIdToken(author.Token).Result;

        //    return Ok(new JavaScriptSerializer().Serialize(token));
        //}

        //[HttpPost]
        ////[Route("AccountType/all")]
        //public ActionResult ValidatiTokenId(AuthorEntity author)
        //{
        //    AuthorUtility.Author auth = new AuthorUtility.Author();

        //    bool result = auth.ValidatiTokenId(author.Token, author.PhoneNumber).Result;

        //    if (result)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
    }
}